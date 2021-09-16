using Publisoftware.Data;
using Publisoftware.Data.BD;
using Publisoftware.Data.BD.Helper;
using Publisoftware.PagoPA;
using Publisoftware.PagoPA.Consts;
using Publisoftware.PagoPA.Nodo;
using Publisoftware.Utility.Log;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace Publisoftware.PagoPASvc
{
    public class PagoPA : PagamentiTelematiciCCP, PagamentiTelematiciRT
    {

        private ILogger log;

        PagoPA()
        {
            log = LoggerFactory.getInstance().getLogger<NLogger>(this);
        }

        public paaAttivaRPTResponse paaAttivaRPT(paaAttivaRPTRequest request)
        {
            string _dbServer = WebConfigurationManager.AppSettings["dbServer"];
            string _dbName = WebConfigurationManager.AppSettings["dbName"];
            string _dbUserName = WebConfigurationManager.AppSettings["dbUserName"];
            string _dbPassWord = WebConfigurationManager.AppSettings["dbPassWord"];
            var endPoint = new EndpointAddress(WebConfigurationManager.AppSettings["nodoPagoPAAddress"]);
            String codIntermediario = request.intestazionePPT.identificativoIntermediarioPA;
            String codStazione = request.intestazionePPT.identificativoStazioneIntermediarioPA;
            String codDominio = request.intestazionePPT.identificativoDominio;
            String iuv = request.intestazionePPT.identificativoUnivocoVersamento;
            String ccp = request.intestazionePPT.codiceContestoPagamento;
            var dbHelper = new DbHelper();
            dbEnte context;
            anagrafica_ente ente;
            anagrafica_ente ente_publiservizi;
            var response = new paaAttivaRPTResponse();
            response.paaAttivaRPTRisposta = new paaAttivaRPTRisposta();
            response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1 = new esitoAttivaRPT();
            try
            {
                try
                {
                    if (request.intestazionePPT.identificativoDominio == "03218060659") // publiservizi ente
                    {
                        var auxDigit = iuv[0];
                        string dominioEnte = iuv.Substring(1, 3);
                        context = dbHelper.getEnteContextCodPagoPA(_dbServer, _dbName, _dbUserName, _dbPassWord, dominioEnte);
                        ente_publiservizi = AnagraficaEnteBD.GetList(context).Where(e => e.p_iva == request.intestazionePPT.identificativoDominio).First();
                        ente = AnagraficaEnteBD.GetList(context).Where(e => e.codice_ente_pagopa == dominioEnte).First();
                        codDominio = ente.p_iva;
                    }
                    else // publiservizi intermediario
                    {
                        ente_publiservizi = null;
                        context = dbHelper.getEnteContextPIVA(_dbServer, _dbName, _dbUserName, _dbPassWord, codDominio);
                        ente = AnagraficaEnteBD.GetList(context).Where(e => e.codice_ente_pagopa == codDominio).First();
                    }
                }
                catch (Exception e)
                {
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_ID_DOMINIO_ERRATO;
                    fault.faultString = "La PAA non corrisponde al Dominio indicato.";
                    fault.description = "";
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                var intermediario = AnagraficaPagoPAIntermediariBD.GetList(context).Where(i => i.cod_intermediario == codIntermediario).FirstOrDefault();
                if (intermediario == null)
                {
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_ID_INTERMEDIARIO_ERRATO;
                    fault.faultString = "Identificativo intermediario non corrispondente.";
                    fault.description = "";
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                var stazione = AnagraficaPagoPAStazioniBD.GetList(context).Where(s => s.id_intermediario == intermediario.id_intermediario && s.cod_stazione == codStazione).FirstOrDefault();
                if (intermediario == null)
                {
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_STAZIONE_INT_ERRATA;
                    fault.faultString = "Stazione intermediario non corrispondente.";
                    fault.description = "";
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }
                // gestione errori PAA_STAZIONE_INT_ERRATA e PAA_ID_DOMINIO_ERRATO
                // ...
                PagoPAHelper.RequestControllo req = new PagoPAHelper.RequestControllo();
                req.CodiceContesto = ccp;
                req.IUV = iuv;
                req.ibanAddebito = request.paaAttivaRPT.datiPagamentoPSP.ibanAddebito;
                req.ibanAppoggio = request.paaAttivaRPT.datiPagamentoPSP.ibanAppoggio;
                req.bicAddebito = request.paaAttivaRPT.datiPagamentoPSP.bicAddebito;
                req.bicAppoggio = request.paaAttivaRPT.datiPagamentoPSP.bicAppoggio;
                req.Importo = request.paaAttivaRPT.datiPagamentoPSP.importoSingoloVersamento;
                req.cognome_ragsoc_versante = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.anagraficaVersante;
                req.tipo_soggetto_versante = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.identificativoUnivocoVersante?.tipoIdentificativoUnivoco.ToString();
                req.cf_piva_versante = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.identificativoUnivocoVersante?.codiceIdentificativoUnivoco;
                req.cap_versante = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.capVersante;
                req.comune_versante = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.localitaVersante;
                req.nazione_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.nazioneVersante;
                req.indirizzo_versante = request.paaAttivaRPT.datiPagamentoPSP?.soggettoVersante?.indirizzoVersante;
                req.cognome_ragsoc_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.anagraficaPagatore;
                req.tipo_soggetto_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.identificativoUnivocoPagatore?.tipoIdentificativoUnivoco.ToString();
                req.cf_piva_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.identificativoUnivocoPagatore?.codiceIdentificativoUnivoco;
                req.cap_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.capPagatore;
                req.comune_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.localitaPagatore;
                req.nazione_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.nazionePagatore;
                req.indirizzo_debitore = request.paaAttivaRPT.datiPagamentoPSP?.soggettoPagatore?.indirizzoPagatore;
                req.IdDominioCFEnte = codDominio;
                req.IdentificativoPSP = request.paaAttivaRPT.identificativoPSP;
                req.TipoOperazione = tab_carrello.OPERAZIONE_RPT;

                var verificationResponse = PagoPAHelper.VerificaPDCreaRPT(req, context);



                if (!verificationResponse.Esito)
                {
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = verificationResponse.faultCode == PagoPAHelper.PAA_SYSTEM_ERROR ? "KO" : "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = verificationResponse.faultCode;
                    fault.faultString = verificationResponse.faultString;
                    fault.description = verificationResponse.description;
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault = fault;
                    // TODO: scrivere log in giornale eventi inviaRPT
                    return response;
                }
                else
                {
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = "OK";
                    tab_carrello carrello = TabCarrelloBD.GetById(verificationResponse.IdCarrello.Value, context);
                    var datiPagamentoPA = new paaTipoDatiPagamentoPA();
                    if (carrello.join_tab_carrello_tab_rate.Count > 1)
                    {
                        response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = "KO";
                        var fault = new Publisoftware.PagoPA.faultBean();
                        fault.faultCode = PagoPAHelper.PAA_SEMANTICA;
                        fault.faultString = "Errore semantico.";
                        fault.description = "Il versamento contiente piu' di un singolo versamento, non ammesso per pagamenti ad iniziativa psp.";
                        response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault = fault;
                        // TODO: scrivere log in giornale eventi inviaRPT
                        return response;
                    }
                    var rata = carrello.join_tab_carrello_tab_rate.First();
                    datiPagamentoPA.bicAccredito = rata.bic_accredito;
                    datiPagamentoPA.ibanAccredito = rata.iban_accredito;
                    datiPagamentoPA.importoSingoloVersamento = rata.importo_da_pagare_rata.Value;
                    ctEnteBeneficiario enteBeneficiario = new ctEnteBeneficiario();
                    if (ente_publiservizi != null)
                    {
                        enteBeneficiario.denominazioneBeneficiario = ente_publiservizi.descrizione_ente;
                        enteBeneficiario.identificativoUnivocoBeneficiario = new ctIdentificativoUnivocoPersonaG();
                        enteBeneficiario.identificativoUnivocoBeneficiario.codiceIdentificativoUnivoco = ente_publiservizi.p_iva;
                        enteBeneficiario.identificativoUnivocoBeneficiario.tipoIdentificativoUnivoco = stTipoIdentificativoUnivocoPersG.G;
                        enteBeneficiario.capBeneficiario = ente_publiservizi.cap;
                        enteBeneficiario.indirizzoBeneficiario = ente_publiservizi.indirizzo;
                        enteBeneficiario.localitaBeneficiario = ente_publiservizi.ser_comuni.descrizione;
                        enteBeneficiario.provinciaBeneficiario = ente_publiservizi.ser_province.des_provincia;
                    }
                    else
                    {
                        enteBeneficiario.denominazioneBeneficiario = ente.descrizione_ente;
                        enteBeneficiario.identificativoUnivocoBeneficiario = new ctIdentificativoUnivocoPersonaG();
                        enteBeneficiario.identificativoUnivocoBeneficiario.codiceIdentificativoUnivoco = ente.p_iva;
                        enteBeneficiario.identificativoUnivocoBeneficiario.tipoIdentificativoUnivoco = stTipoIdentificativoUnivocoPersG.G;
                        enteBeneficiario.capBeneficiario = ente.cap;
                        enteBeneficiario.indirizzoBeneficiario = ente.indirizzo;
                        enteBeneficiario.localitaBeneficiario = ente.ser_comuni.descrizione;
                        enteBeneficiario.provinciaBeneficiario = ente.ser_province.des_provincia;
                    }
                    datiPagamentoPA.enteBeneficiario = enteBeneficiario;
                    response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.datiPagamentoPA = datiPagamentoPA;
                    HostingEnvironment.QueueBackgroundWorkItem(_ =>
                    {
                        try
                        {
                            var binding = new BasicHttpBinding();
                            PagamentiTelematiciRPTClient client = new PagamentiTelematiciRPTClient(binding, endPoint);
                            nodoInviaRPT1 inviaRPTReq = new nodoInviaRPT1
                            {
                                intestazionePPT = new Publisoftware.PagoPA.Nodo.intestazionePPT
                                {
                                    codiceContestoPagamento = ccp,
                                    identificativoIntermediarioPA = codIntermediario,
                                    identificativoStazioneIntermediarioPA = codStazione
                                },
                                nodoInviaRPT = new nodoInviaRPT
                                {
                                    identificativoCanale = request.paaAttivaRPT.identificativoCanalePSP,
                                    identificativoIntermediarioPSP = request.paaAttivaRPT.identificativoIntermediarioPSP,
                                    identificativoPSP = request.paaAttivaRPT.identificativoPSP
                                }
                            };
                            inviaRPTReq.nodoInviaRPT.password = stazione.password;
                            ctRichiestaPagamentoTelematico rpt = new ctRichiestaPagamentoTelematico();
                            //Modifica del 19/10/2020
                            //Modifica per la nuova chiamata a GovPay
                            //rpt.dataOraMessaggioRichiesta = carrello.data_ora_messaggio_richiesta_rpt.HasValue ? carrello.data_ora_messaggio_richiesta_rpt.Value : DateTime.Now;
                            //rpt.identificativoMessaggioRichiesta = carrello.identificativo_messaggio_richiesta_rpt;
                            rpt.soggettoPagatore = new ctSoggettoPagatore
                            {
                                anagraficaPagatore = rata.cognome_ragsoc_debitore + (string.IsNullOrEmpty(rata.nome_debitore) ? "" : " " + rata.nome_debitore),
                                identificativoUnivocoPagatore = new ctIdentificativoUnivocoPersonaFG
                                {
                                    codiceIdentificativoUnivoco = rata.cf_piva_debitore,
                                    tipoIdentificativoUnivoco = rata.tipo_soggetto_debitore == "F" ? stTipoIdentificativoUnivocoPersFG.F : stTipoIdentificativoUnivocoPersFG.G
                                },
                                capPagatore = rata.cap_debitore,
                                indirizzoPagatore = rata.indirizzo_debitore,
                                localitaPagatore = rata.comune_debitore,
                                provinciaPagatore = rata.sigla_autonom_prov_soggetto_debitore
                            };

                            rpt.soggettoVersante = new ctSoggettoVersante
                            {
                                anagraficaVersante = carrello.cognome_ragsoc_versante + (string.IsNullOrEmpty(carrello.nome_versante) ? "" : " " + carrello.nome_versante),
                                identificativoUnivocoVersante = new ctIdentificativoUnivocoPersonaFG
                                {
                                    codiceIdentificativoUnivoco = carrello.cf_piva_versante,
                                    tipoIdentificativoUnivoco = carrello.tipo_soggetto_versante == "F" ? stTipoIdentificativoUnivocoPersFG.F : stTipoIdentificativoUnivocoPersFG.G
                                },
                                capVersante = carrello.cap_versante,
                                indirizzoVersante = carrello.indirizzo_versante,
                                localitaVersante = carrello.comune_versante
                            };

                            rpt.dominio = new ctDominio()
                            {
                                identificativoDominio = codDominio,
                                identificativoStazioneRichiedente = codStazione
                            };
                            rpt.enteBeneficiario = enteBeneficiario;
                            rpt.autenticazioneSoggetto = stAutenticazioneSoggetto.OTH;
                            rpt.versioneOggetto = PagoPAConsts_Old.RPT_VERSIONE;
                            rpt.datiVersamento = new ctDatiVersamentoRPT
                            {
                                bicAddebito = carrello.bic_addebito,
                                ibanAddebito = carrello.iban_addebito,
                                identificativoUnivocoVersamento = iuv,
                                importoTotaleDaVersare = carrello.importo_totale_da_pagare.Value,
                                tipoVersamento = (Publisoftware.PagoPA.stTipoVersamento)Enum.Parse(typeof(Publisoftware.PagoPA.stTipoVersamento), carrello.tipo_versamento),
                                datiSingoloVersamento = new ctDatiSingoloVersamentoRPT[1]
                            };
                            rpt.datiVersamento.datiSingoloVersamento[0] = new ctDatiSingoloVersamentoRPT
                            {
                                bicAccredito = rata.bic_accredito,
                                ibanAccredito = rata.bic_accredito,
                                importoSingoloVersamento = rata.importo_da_pagare_rata.Value,
                                causaleVersamento = ""
                            };
                            rpt.datiVersamento.codiceContestoPagamento = ccp;

                            XmlSerializer bf = new XmlSerializer(typeof(ctRichiestaPagamentoTelematico));
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bf.Serialize(ms, rpt);
                                inviaRPTReq.nodoInviaRPT.rpt = ms.ToArray();
                            }
                            var inviaRPTResponse = client.nodoInviaRPT(inviaRPTReq);
                            // TODO: scrivere log in giornale eventi inviaRPT
                        }
                        catch (Exception e)
                        {
                            log.LogMessage(String.Format("Impossibile inviare la RPT per IUV {0}: {1}", iuv, e.ToString()), EnLogSeverity.Error);
                            // TODO: scrivere log in giornale eventi inviaRPT
                        }
                    });
                }
            }
            catch (Exception e)
            {
                response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.esito = "FAIL";
                response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault = new Publisoftware.PagoPA.faultBean();
                response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault.faultCode = PagoPAHelper.PAA_SYSTEM_ERROR;
                response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault.faultString = "Errore generico";
                response.paaAttivaRPTRisposta.paaAttivaRPTRisposta1.fault.description = "Si è verificata un'eccezione: " + e.ToString();
                log.LogMessage(String.Format("Si è verificato un errore per paaAttivaRPT IUV: {0} - Error: {1}", iuv, e.ToString()), EnLogSeverity.Error);
                // TODO: scrivere log in giornale eventi paaAttivaRPT
            }

            return response;
        }

        public paaVerificaRPTResponse paaVerificaRPT(paaVerificaRPTRequest request)
        {
            string _dbServer = WebConfigurationManager.AppSettings["dbServer"];
            string _dbName = WebConfigurationManager.AppSettings["dbName"];
            string _dbUserName = WebConfigurationManager.AppSettings["dbUserName"];
            string _dbPassWord = WebConfigurationManager.AppSettings["dbPassWord"];
            String codIntermediario = request.intestazionePPT.identificativoIntermediarioPA;
            String codStazione = request.intestazionePPT.identificativoStazioneIntermediarioPA;
            String codDominio = request.intestazionePPT.identificativoDominio;
            String iuv = request.intestazionePPT.identificativoUnivocoVersamento;
            String ccp = request.intestazionePPT.codiceContestoPagamento;
            var dbHelper = new DbHelper();
            dbEnte context;
            anagrafica_ente ente;
            anagrafica_ente ente_publiservizi;
            var response = new paaVerificaRPTResponse();
            response.paaVerificaRPTRisposta = new paaVerificaRPTRisposta();
            response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1 = new esitoVerificaRPT();
            try
            {
                try
                {
                    if (request.intestazionePPT.identificativoDominio == "03218060659") // publiservizi ente
                    {
                        var auxDigit = iuv[0];
                        string dominioEnte = iuv.Substring(1, 3);
                        context = dbHelper.getEnteContextCodPagoPA(_dbServer, _dbName, _dbUserName, _dbPassWord, dominioEnte);
                        ente_publiservizi = AnagraficaEnteBD.GetList(context).Where(e => e.p_iva == request.intestazionePPT.identificativoDominio).First();
                        ente = AnagraficaEnteBD.GetList(context).Where(e => e.codice_ente_pagopa == dominioEnte).First();
                        codDominio = ente.p_iva;
                    }
                    else // publiservizi intermediario
                    {
                        ente_publiservizi = null;
                        context = dbHelper.getEnteContextPIVA(_dbServer, _dbName, _dbUserName, _dbPassWord, codDominio);
                        ente = AnagraficaEnteBD.GetList(context).Where(e => e.codice_ente_pagopa == codDominio).First();
                    }
                }
                catch (Exception e)
                {
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_ID_DOMINIO_ERRATO;
                    fault.faultString = "La PAA non corrisponde al Dominio indicato.";
                    fault.description = "";
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                var intermediario = AnagraficaPagoPAIntermediariBD.GetList(context).Where(i => i.cod_intermediario == codIntermediario).FirstOrDefault();
                if (intermediario == null)
                {
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_ID_INTERMEDIARIO_ERRATO;
                    fault.faultString = "Identificativo intermediario non corrispondente.";
                    fault.description = "";
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                var stazione = AnagraficaPagoPAStazioniBD.GetList(context).Where(s => s.id_intermediario == intermediario.id_intermediario && s.cod_stazione == codStazione).FirstOrDefault();
                if (intermediario == null)
                {
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_STAZIONE_INT_ERRATA;
                    fault.faultString = "Stazione intermediario non corrispondente.";
                    fault.description = "";
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }
                // gestione errori PAA_STAZIONE_INT_ERRATA e PAA_ID_DOMINIO_ERRATO
                // ...
                PagoPAHelper.RequestControllo req = new PagoPAHelper.RequestControllo();
                req.CodiceContesto = ccp;
                req.IUV = iuv;
                req.IdDominioCFEnte = codDominio;
                req.IdentificativoPSP = request.paaVerificaRPT.identificativoPSP;
                req.TipoOperazione = tab_carrello.OPERAZIONE_VER;

                var verificationResponse = PagoPAHelper.VerificaPDCreaRPT(req, context);



                if (!verificationResponse.Esito)
                {
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = verificationResponse.faultCode == PagoPAHelper.PAA_SYSTEM_ERROR ? "KO" : "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = verificationResponse.faultCode;
                    fault.faultString = verificationResponse.faultString;
                    fault.description = verificationResponse.description;
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault = fault;
                    // TODO: scrivere log in giornale eventi inviaRPT
                    return response;
                }
                else
                {
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = "OK";
                    tab_carrello carrello = TabCarrelloBD.GetById(verificationResponse.IdCarrello.Value, context);
                    var datiPagamentoPA = new paaTipoDatiPagamentoPA();
                    if (carrello.join_tab_carrello_tab_rate.Count > 1)
                    {
                        response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = "KO";
                        var fault = new Publisoftware.PagoPA.faultBean();
                        fault.faultCode = PagoPAHelper.PAA_SEMANTICA;
                        fault.faultString = "Errore semantico.";
                        fault.description = "Il versamento contiente piu' di un singolo versamento, non ammesso per pagamenti ad iniziativa psp.";
                        response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault = fault;
                        // TODO: scrivere log in giornale eventi inviaRPT
                        return response;
                    }
                    var rata = carrello.join_tab_carrello_tab_rate.First();
                    datiPagamentoPA.bicAccredito = rata.bic_accredito;
                    datiPagamentoPA.ibanAccredito = rata.iban_accredito;
                    datiPagamentoPA.importoSingoloVersamento = rata.importo_da_pagare_rata.Value;
                    ctEnteBeneficiario enteBeneficiario = new ctEnteBeneficiario();
                    if (ente_publiservizi != null)
                    {
                        enteBeneficiario.denominazioneBeneficiario = ente_publiservizi.descrizione_ente;
                        enteBeneficiario.identificativoUnivocoBeneficiario = new ctIdentificativoUnivocoPersonaG();
                        enteBeneficiario.identificativoUnivocoBeneficiario.codiceIdentificativoUnivoco = ente_publiservizi.p_iva;
                        enteBeneficiario.identificativoUnivocoBeneficiario.tipoIdentificativoUnivoco = stTipoIdentificativoUnivocoPersG.G;
                        enteBeneficiario.capBeneficiario = ente_publiservizi.cap;
                        enteBeneficiario.indirizzoBeneficiario = ente_publiservizi.indirizzo;
                        enteBeneficiario.localitaBeneficiario = ente_publiservizi.ser_comuni.descrizione;
                        enteBeneficiario.provinciaBeneficiario = ente_publiservizi.ser_province.des_provincia;
                    }
                    else
                    {
                        enteBeneficiario.denominazioneBeneficiario = ente.descrizione_ente;
                        enteBeneficiario.identificativoUnivocoBeneficiario = new ctIdentificativoUnivocoPersonaG();
                        enteBeneficiario.identificativoUnivocoBeneficiario.codiceIdentificativoUnivoco = ente.p_iva;
                        enteBeneficiario.identificativoUnivocoBeneficiario.tipoIdentificativoUnivoco = stTipoIdentificativoUnivocoPersG.G;
                        enteBeneficiario.capBeneficiario = ente.cap;
                        enteBeneficiario.indirizzoBeneficiario = ente.indirizzo;
                        enteBeneficiario.localitaBeneficiario = ente.ser_comuni.descrizione;
                        enteBeneficiario.provinciaBeneficiario = ente.ser_province.des_provincia;
                    }
                    datiPagamentoPA.enteBeneficiario = enteBeneficiario;
                    response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.datiPagamentoPA = datiPagamentoPA;
                }
            }
            catch (Exception e)
            {
                response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.esito = "FAIL";
                response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault = new Publisoftware.PagoPA.faultBean();
                response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault.faultCode = PagoPAHelper.PAA_SYSTEM_ERROR;
                response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault.faultString = "Errore generico";
                response.paaVerificaRPTRisposta.paaVerificaRPTRisposta1.fault.description = "Si è verificata un'eccezione: " + e.ToString();
                log.LogMessage(String.Format("Si è verificato un errore per paaAttivaRPT IUV: {0} - Error: {1}", iuv, e.ToString()), EnLogSeverity.Error);
                // TODO: scrivere log in giornale eventi paaAttivaRPT
            }

            return response;
        }

        public paaInviaEsitoStornoResponse paaInviaEsitoStorno(paaInviaEsitoStornoRequest request)
        {
            throw new NotImplementedException();
        }

        public paaInviaRichiestaRevocaResponse paaInviaRichiestaRevoca(paaInviaRichiestaRevocaRequest request)
        {
            throw new NotImplementedException();
        }

        public paaInviaRTResponse paaInviaRT(paaInviaRTRequest request)
        {
            string _dbServer = WebConfigurationManager.AppSettings["dbServer"];
            string _dbName = WebConfigurationManager.AppSettings["dbName"];
            string _dbUserName = WebConfigurationManager.AppSettings["dbUserName"];
            string _dbPassWord = WebConfigurationManager.AppSettings["dbPassWord"];
            String codIntermediario = request.intestazionePPT.identificativoIntermediarioPA;
            String codStazione = request.intestazionePPT.identificativoStazioneIntermediarioPA;
            String codDominio = request.intestazionePPT.identificativoDominio;
            String iuv = request.intestazionePPT.identificativoUnivocoVersamento;
            String ccp = request.intestazionePPT.codiceContestoPagamento;
            var dbHelper = new DbHelper();
            dbEnte context;
            anagrafica_ente ente;
            anagrafica_ente ente_publiservizi;
            var response = new paaInviaRTResponse();
            response.paaInviaRTRisposta = new paaInviaRTRisposta();
            response.paaInviaRTRisposta.paaInviaRTRisposta1 = new esitoPaaInviaRT();
            try
            {
                try
                {
                    if (request.intestazionePPT.identificativoDominio == "03218060659") // publiservizi ente
                    {
                        var auxDigit = iuv[0];
                        string dominioEnte = iuv.Substring(1, 3);
                        context = dbHelper.getEnteContextCodPagoPA(_dbServer, _dbName, _dbUserName, _dbPassWord, dominioEnte);
                        ente_publiservizi = AnagraficaEnteBD.GetList(context).Where(e => e.p_iva == request.intestazionePPT.identificativoDominio).First();
                        ente = AnagraficaEnteBD.GetList(context).Where(e => e.codice_ente_pagopa == dominioEnte).First();
                        codDominio = ente.p_iva;
                    }
                    else // publiservizi intermediario
                    {
                        ente_publiservizi = null;
                        context = dbHelper.getEnteContextPIVA(_dbServer, _dbName, _dbUserName, _dbPassWord, codDominio);
                        ente = AnagraficaEnteBD.GetList(context).Where(e => e.codice_ente_pagopa == codDominio).First();
                    }
                }
                catch (Exception e)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_ID_DOMINIO_ERRATO;
                    fault.faultString = "La PAA non corrisponde al Dominio indicato.";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                var intermediario = AnagraficaPagoPAIntermediariBD.GetList(context).Where(i => i.cod_intermediario == codIntermediario).FirstOrDefault();
                if (intermediario == null)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "KO";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_ID_INTERMEDIARIO_ERRATO;
                    fault.faultString = "Identificativo intermediario non corrispondente.";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                var stazione = AnagraficaPagoPAStazioniBD.GetList(context).Where(s => s.id_intermediario == intermediario.id_intermediario && s.cod_stazione == codStazione).FirstOrDefault();
                if (intermediario == null)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "KO";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_STAZIONE_INT_ERRATA;
                    fault.faultString = "Stazione intermediario non corrispondente.";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                //Salva RT in rata
                ctRicevutaTelematica rt;
                try
                {
                    using (MemoryStream stringReader = new MemoryStream(request.paaInviaRT.rt))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(ctRicevutaTelematica));
                        rt = (ctRicevutaTelematica)xmlSerializer.Deserialize(stringReader);
                    }
                }
                catch (Exception ex)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "FAIL";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_SINTASSI_XSD;
                    fault.faultString = "Errore di sintassi XSD.";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }
                //Modifica del 19/10/2020
                //Modifica per la nuova chiamata a GovPay
                //var carrello = TabCarrelloBD.GetList(context).Where(c => c.identificativo_messaggio_richiesta_rpt == rt.identificativoMessaggioRicevuta).FirstOrDefault();
                var carrello = TabCarrelloBD.GetList(context).Where(c => c.id_carrello == 1).FirstOrDefault();
                if (carrello == null)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "KO";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_RPT_SCONOSCIUTA;
                    fault.faultString = "La RPT risulta sconosciuta..";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }
                var rata = carrello.join_tab_carrello_tab_rate.Where(r => r.tab_rata_avv_pag.Iuv_identificativo_pagamento == request.intestazionePPT.identificativoUnivocoVersamento).FirstOrDefault();
                if (rata == null)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "KO";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_RPT_SCONOSCIUTA;
                    fault.faultString = "La RPT risulta sconosciuta..";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }

                if (rata.identificativo_rt != null)
                {
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "KO";
                    var fault = new Publisoftware.PagoPA.faultBean();
                    fault.faultCode = PagoPAHelper.PAA_RT_DUPLICATA;
                    fault.faultString = "RT già acquisita.";
                    fault.description = "";
                    response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = fault;

                    // TODO: log event journal

                    return response;
                }


                XmlSerializer bf = new XmlSerializer(typeof(ctRicevutaTelematica));
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, rt);
                    rata.identificativo_rt = ms.ToString();
                    context.SaveChanges();
                }




                response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "OK";
                return response;
            }
            catch (Exception e)
            {
                response.paaInviaRTRisposta.paaInviaRTRisposta1.esito = "FAIL";
                response.paaInviaRTRisposta.paaInviaRTRisposta1.fault = new Publisoftware.PagoPA.faultBean();
                response.paaInviaRTRisposta.paaInviaRTRisposta1.fault.faultCode = PagoPAHelper.PAA_SYSTEM_ERROR;
                response.paaInviaRTRisposta.paaInviaRTRisposta1.fault.faultString = "Errore generico";
                response.paaInviaRTRisposta.paaInviaRTRisposta1.fault.description = "Si è verificata un'eccezione: " + e.ToString();
                log.LogMessage(String.Format("Si è verificato un errore per paaAttivaRPT IUV: {0} - Error: {1}", iuv, e.ToString()), EnLogSeverity.Error);

                // log to journal

                return response;
            }
        }


    }
}
