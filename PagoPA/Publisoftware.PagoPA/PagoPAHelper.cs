using Publisoftware.Data;
using Publisoftware.Data.BD;
using Publisoftware.Data.LinqExtended;
using Publisoftware.Data.POCOLight;
using Publisoftware.PagoPA.Consts;
using Publisoftware.PagoPA.Nodo;
using Publisoftware.Utility;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Publisoftware.PagoPA
{
    public static class PagoPAHelper
    {
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.PagoPA.PagoPAHelper");

        #region Public Methods
        /// <summary>
        /// /Prepara e valorizza il carrello
        /// PagoPA
        /// </summary>
        /// <param name="listCarrello"></param>
        /// <param name="ente"></param>
        /// <param name="modalitaOp"></param>
        /// <param name="contribuente"></param>
        /// <param name="dbContext"></param>
        /// <param name="errorDescription"></param>
        /// <param name="referente"></param>
        /// <returns></returns>
        public static tab_carrello GenerateCarrello(List<tab_rata_avv_pag_light> listCarrello, anagrafica_ente ente, string modalitaOp, tab_contribuente contribuente, dbEnte dbContext,
            out string errorDescription, tab_referente referente = null)
        {
            tab_carrello carrello = new tab_carrello();
            errorDescription = string.Empty;
            int progrCarrello = 0;
            bool IsValid = true;
            try
            {

                if (listCarrello != null)
                {
                    //Verifico la validità del carrello
                    IsValid = IsValidCarrello(listCarrello, out errorDescription);
                    if (!IsValid)
                    {
                        return null;
                    }
                    //Recupero l'esadecimale progressivo da utilizzare per
                    //l'Identificativo del carrello
                    progrCarrello = TabProgCarrelloBD.ReturnProgressivoIncrementatoByIdEnteAnno(ente.id_ente, DateTime.Now.Year, dbContext);
                    //PSP=Sportello; WEB=Portale
                    carrello.fonte_carrello = ConfigurationManager.AppSettings["fonte_carrello"] != null ? ConfigurationManager.AppSettings["fonte_carrello"].ToString() : tab_carrello.FONTE_CARRELLO_WEB;
                    //Se il carrello è costituito da una sola rata in pagamento--> TIPO=S

                    if (listCarrello.Count() == 1)
                        carrello.tipo_carrello = tab_carrello.TIPO_CARRELLO_S;
                    else
                    {
                        List<decimal> idsContribuente = listCarrello.Select(x => x.Id_Contribuente).Distinct().ToList();
                        if (idsContribuente.Count() == 1)//Più rate con stesso contribuente=M
                            carrello.tipo_carrello = tab_carrello.TIPO_CARRELLO_M;
                        else if (idsContribuente.Count() > 1)//Più rate con diversi contribuenti=C
                            carrello.tipo_carrello = tab_carrello.TIPO_CARRELLO_C;

                    }
                    //TODO: Verificare Tipo_Versamento:
                    carrello.tipo_versamento = "BBT";
                    //IDENTIFICATIVO CARRELLO ------------------------------
                    string identificativoCarrello = string.Empty;
                    if (carrello.tipo_carrello == tab_carrello.TIPO_CARRELLO_S &&
                         (carrello.fonte_carrello == tab_carrello.FONTE_CARRELLO_PSP || carrello.fonte_carrello == tab_carrello.FONTE_CARRELLO_WEB))
                    {
                        identificativoCarrello = listCarrello.FirstOrDefault().IUV;
                    }
                    else if (carrello.tipo_carrello != tab_carrello.TIPO_CARRELLO_S &&
                         carrello.fonte_carrello == tab_carrello.FONTE_CARRELLO_WEB)
                    {
                        string anno = DateTime.Now.ToString("yyyy");
                        string pIva_EC = ente.p_iva;
                        string cod_Segregazione = string.Empty;
                        if (ente.aux_digit_pagopa == "3")
                            cod_Segregazione = ente.codice_segregazione_pagopa;
                        else if (ente.aux_digit_pagopa == "1" || ente.aux_digit_pagopa == "2")
                            cod_Segregazione = "00";

                        //string prog = maxCarrello.ToString().PadLeft(18, '0');
                        //TODO: Verificare
                        identificativoCarrello = anno + pIva_EC + cod_Segregazione + progrCarrello.ToString("X18"); ;
                    }
                    carrello.identificativo_carrello = identificativoCarrello;
                    //IDENTIFICATIVO CARRELLO ------------------------------

                    string contesto_pag_rpt = string.Empty;
                    if (carrello.fonte_carrello == tab_carrello.FONTE_CARRELLO_PSP)
                    {
                        //TODO: Verificare fonte carrello per PSP
                        //TODO: Verificare data_esecuzione_pagamento_psp --> Solo Fonte PSP?
                        carrello.data_esecuzione_pagamento_psp = DateTime.Now;
                    }
                    else
                    {
                        //Fonte WEB
                        //Modifica del 19/10/2020
                        //La chiamata a GovPay ha comportato la rimozione del campo "codice_contesto_pagamento_rpt"
                        //carrello.codice_contesto_pagamento_rpt = GenerateCodContestoRPT(progrCarrello);
                        carrello.fonte_carrello = tab_carrello.FONTE_CARRELLO_WEB;
                    }
                    //Modifica del 19/10/2020
                    //La chiamata a GovPay ha comportato la rimozione dei campi "data_ora_messaggio_richiesta_rpt" e "identificativo_messaggio_richiesta_rpt"
                    //carrello.codice_contesto_pagamento_rpt = GenerateCodContestoRPT(progrCarrello);
                    //carrello.data_ora_messaggio_richiesta_rpt = DateTime.Now;
                    //TODO: Verificare identificativo_messaggio_richiesta_rpt
                    //carrello.identificativo_messaggio_richiesta_rpt = progrCarrello.ToString().PadLeft(35, '0');

                    //TODO: Totale carrello??
                    carrello.importo_totale_da_pagare = listCarrello.Sum(c => c.Importo_Pagato);
                    carrello.note_su_pagamento = string.Format("Carrello {0} del {1} con {2} pagamenti.", identificativoCarrello, DateTime.Now.ToString("dd/mm/yyyy hh:MM:ss"), listCarrello.Count());
                    //TODO: Nel caso di TipoCarrello=S e Fonte PSP ????
                    carrello.cf_piva_dominio_ente_creditore = ente.p_iva;
                    //TODO: Solo per PSP??
                    carrello.data_esecuzione_pagamento_psp = DateTime.Now.AddDays(1);
                    carrello.denominazione_ente_creditore = ente.descrizione_ente;
                    //Modifica del 19/10/2020
                    //La chiamata a GovPay ha comportato la rimozione dei campi "identificativo_PSP" e "stazione_PSP"
                    //carrello.identificativo_PSP = null;
                    //carrello.stazione_PSP = null;
                    carrello.iban_appoggio = null;
                    carrello.bic_appoggio = null;
                    carrello.bic_addebito = null;

                    //PER I DATI SEGUENTI BISOGNA VERIFICARE SE CHI OPERA E' IL CONTRIBUENTE OPPURE IL REFERENTE
                    if (modalitaOp == "C")
                    {
                        //tab_contribuente contribuente = Sessione.getCurrentContribuente();
                        carrello.tipo_soggetto_versante = contribuente.anagrafica_tipo_contribuente != null ? contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente : string.Empty;
                        carrello.cf_piva_versante = contribuente.codFiscalePivaDisplay;
                        carrello.cognome_ragsoc_versante = contribuente.contribuenteNominativoDisplay;
                        carrello.nome_versante = contribuente.nome != null ? contribuente.nome : string.Empty;
                        carrello.indirizzo_versante = contribuente.indirizzoDisplay;
                        carrello.cap_versante = contribuente.cap;
                        carrello.comune_versante = contribuente.comune_nas;
                        carrello.nazione_versante = contribuente.stato_nas;

                        carrello.id_contribuente_versante = contribuente.id_anag_contribuente;
                        carrello.cf_piva_contribuente_versante = contribuente.codFiscalePivaDisplay;
                    }
                    else if (modalitaOp == "R" ||
                        modalitaOp == "RC")
                    {

                        if (referente.anagrafica_tipo_contribuente != null)
                            carrello.tipo_soggetto_versante = referente.anagrafica_tipo_contribuente != null ? referente.anagrafica_tipo_contribuente.sigla_tipo_contribuente : "";
                        else
                            carrello.tipo_soggetto_versante = referente.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente;

                        carrello.cf_piva_versante = referente.codFiscalePivaDisplay;
                        carrello.cognome_ragsoc_versante = referente.referenteNominativoDisplay;
                        carrello.nome_versante = referente.nome != null ? referente.nome : string.Empty;
                        carrello.indirizzo_versante = referente.indirizzoDisplay;
                        carrello.cap_versante = referente.cap;
                        carrello.comune_versante = referente.comune_nas;
                        carrello.nazione_versante = referente.stato_nas;

                        carrello.id_contribuente_versante = contribuente.id_anag_contribuente;
                        carrello.cf_piva_contribuente_versante = contribuente.codFiscalePivaDisplay;
                        carrello.id_referente_versante = referente.id_tab_referente;
                        carrello.cf_piva_referente_versante = referente.codFiscalePivaDisplay;

                    }
                    //TODO: Verificare per terzo
                    carrello.cf_piva_terzo_versante = string.Empty;
                    carrello.id_terzo_versante = null;
                    carrello.cod_stato = tab_carrello.ATT_RPT;
                    return carrello;
                }
                else
                {
                    errorDescription = "Il carrello è vuoto";
                    return null;
                }
            }
            catch (Exception ex)
            {
                errorDescription = "Errore in fase di preparazione del carrello dei pagamenti. " + ex.Message;
                return null;
            }

        }
        private static bool IsValidCarrello(List<tab_rata_avv_pag_light> listCarrello, out string errorDescription)
        {
            errorDescription = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (tab_rata_avv_pag_light rata in listCarrello)
            {
                if ((rata.Importo_Pagato == null) ||
                    (rata.Importo_Pagato <= 0))
                {
                    sb.Append(string.Format("La rata {0} ha un importo di pagamento nullo o uguale  zero", rata.id_rata_avv_pag));
                    sb.Append(Environment.NewLine);
                }
                else if (rata.Cod_Stato.StartsWith(tab_rata_avv_pag.ATT_PAG))
                {
                    sb.Append(string.Format("Il pagamento della rata {0} risulta concluso all'Ente Creditore", rata.id_rata_avv_pag));
                    sb.Append(Environment.NewLine);
                }
                else if (rata.Cod_Stato.StartsWith(tab_rata_avv_pag.ATT_REN))
                {
                    sb.Append(string.Format("Il pagamento della rata {0} risulta concluso all'Ente Creditore", rata.id_rata_avv_pag));
                    sb.Append(Environment.NewLine);
                }

            }
            if (string.IsNullOrEmpty(sb.ToString()))
                return true;
            else
            {
                errorDescription = sb.ToString();
                return false;
            }
        }

        /// <summary>
        /// Valorizza l'elenco delle rate
        /// associate al carrello
        /// </summary>
        /// <param name="listCarrello"></param>
        /// <param name="contribuente"></param>
        /// <param name="errorDescription"></param>
        /// <returns></returns>
        /// <summary>
        /// Valorizza l'elenco delle rate
        /// associate al carrello
        /// </summary>
        /// <param name="listCarrello"></param>
        /// <param name="errorDescription"></param>
        /// <returns></returns>
        public static List<join_tab_carrello_tab_rate> GenerateRateCarrello(List<tab_rata_avv_pag_light> listCarrello, out string errorDescription)
        {
            errorDescription = string.Empty;
            try
            {
                List<join_tab_carrello_tab_rate> results = null;
                //tab_contribuente contribuente = Sessione.getCurrentContribuente();
                results = (from c in listCarrello
                           select new join_tab_carrello_tab_rate()
                           {
                               importo_da_pagare_rata = c.Importo_Pagato,
                               bic_accredito = c.Bic_Accredito,
                               iban_accredito = c.Iban_Accredito,
                               importo_rata_maggiorato_interessi = 0,//TODO: Verificare
                               importo_rata_maggiorato_sanzioni = 0,//TODO: Verificare
                               cod_stato = join_tab_carrello_tab_rate.ATT_RPT,
                               id_contribuente_debitore = c.contribuente != null ? c.contribuente.id_anag_contribuente : 0,
                               tipo_soggetto_debitore = c.contribuente.anagrafica_tipo_contribuente != null ? c.contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente : string.Empty,
                               cf_piva_debitore = c.contribuente != null ? c.contribuente.codFiscalePivaDisplay : string.Empty,
                               cognome_ragsoc_debitore = c.contribuente != null ? c.contribuente.contribuenteNominativoDisplay : string.Empty,
                               nome_debitore = c.contribuente != null ? c.contribuente.nome : string.Empty,
                               indirizzo_debitore = c.contribuente != null ? c.contribuente.indirizzoDisplay : string.Empty,
                               cap_debitore = c.contribuente != null ? c.contribuente.cap : string.Empty,
                               comune_debitore = c.contribuente != null ? c.contribuente.comune_nas : string.Empty,
                               nazione_debitore = c.contribuente != null ? c.contribuente.stato_nas : string.Empty,
                               tipo_marca_da_bollo = null,
                               hashDocumento_digitale = null,
                               sigla_autonom_prov_soggetto_debitore = null,
                               id_rata = c.id_rata_avv_pag,
                               identificativo_rt = c.identificativo_rt

                           }

                         ).ToList();

                return results;
            }
            catch (Exception ex)
            {
                errorDescription = "Errore in fase di preparazione delle rate del carrello. " + ex.Message;
                return null;
            }
        }

        #region Prior Method
        //public static List<join_tab_carrello_tab_rate> GenerateRateCarrello(List<tab_rata_avv_pag_light> listCarrello, tab_contribuente contribuente, out string errorDescription)
        //{
        //    errorDescription = string.Empty;
        //    try
        //    {
        //        List<join_tab_carrello_tab_rate> results = null;
        //        //tab_contribuente contribuente = Sessione.getCurrentContribuente();
        //        results = (from c in listCarrello
        //                   select new join_tab_carrello_tab_rate()
        //                   {
        //                       importo_da_pagare_rata = c.Importo_Pagato,
        //                       bic_accredito = c.Bic_Accredito,
        //                       iban_accredito = c.Iban_Accredito,
        //                       importo_rata_maggiorato_interessi = 0,//TODO: Verificare
        //                       importo_rata_maggiorato_sanzioni = 0,//TODO: Verificare
        //                       cod_stato = join_tab_carrello_tab_rate.ATT_RPT,

        //                       id_contribuente_debitore = contribuente.id_anag_contribuente,
        //                       tipo_soggetto_debitore = contribuente.anagrafica_tipo_contribuente != null ? contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente : string.Empty,
        //                       cf_piva_debitore = contribuente.codFiscalePivaDisplay,
        //                       cognome_ragsoc_debitore = contribuente.contribuenteNominativoDisplay,
        //                       nome_debitore = contribuente.nome,
        //                       indirizzo_debitore = contribuente.indirizzoDisplay,
        //                       cap_debitore = contribuente.cap,
        //                       comune_debitore = contribuente.comune_nas,
        //                       nazione_debitore = contribuente.stato_nas,

        //                       tipo_marca_da_bollo = null,
        //                       hashDocumento_digitale = null,
        //                       sigla_autonom_prov_soggetto_debitore = null,
        //                       id_rata = c.id_rata_avv_pag,
        //                       identificativo_rt = c.identificativo_rt

        //                   }

        //                 ).ToList();

        //        return results;
        //    }
        //    catch (Exception ex)
        //    {
        //        errorDescription = "Errore in fase di preparazione delle rate del carrello. " + ex.Message;
        //        return null;
        //    }
        //}
        #endregion Prior Method
        #endregion Public Methods

        #region Private Methods
        public static X509Certificate2 GetCertificateFromStore(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue, bool validOnly)
        {
            X509Certificate2 certificate = null;

            if (findValue == null)
            {
                throw new ArgumentNullException("The findValue parameter can't be null.");
            }

            try
            {
                X509Store store = new X509Store(storeName, storeLocation);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection coll = store.Certificates.Find(findType, findValue.ToString(), validOnly);

                if (coll.Count > 0)
                {
                    certificate = coll[0];
                }
                store.Close();

                if (certificate != null)
                {
                    return certificate;
                }
                else
                {
                    throw new FileNotFoundException("Unable to locate certificate");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string GenerateCodContestoRPT(int progrCarrello)
        {
            string data = DateTime.Now.ToString("ddmmyyyy");
            char pad = '0';
            string progr = data + tab_carrello.FONTE_CARRELLO_WEB + progrCarrello.ToString();
            return progr.ToString().PadLeft(35, pad).ToUpper();
        }
        private static string GetNextHex(int p_value)
        {
            return p_value.ToString("X18");
        }
        #endregion Private Methods

        public class RequestControllo
        {
            //public string dbServer { get; set; } //10.1.1.110
            //public string dbName { get; set; } //db_generale_sviluppo
            //public string dbUserName { get; set; } //sa
            //public string dbPassWord { get; set; } //jpF/0DwDkWTxY6P8CayRxg==
            public string TipoOperazione { get; set; } //RPT
            public string IdentificativoPSP { get; set; } //prova1
            public string IdDominioCFEnte { get; set; } //00100110618
            public string CodiceContesto { get; set; } //prova2
            public string IUV { get; set; } //557821600000340281
            public decimal Importo { get; set; } //484
            public string ibanAppoggio { get; set; }
            public string bicAppoggio { get; set; }
            public string ibanAddebito { get; set; }
            public string bicAddebito { get; set; }
            public string tipo_soggetto_versante { get; set; }
            public string cf_piva_versante { get; set; }
            public string cognome_ragsoc_versante { get; set; }
            public string nome_versante { get; set; }
            public string indirizzo_versante { get; set; }
            public string cap_versante { get; set; }
            public string comune_versante { get; set; }
            public string nazione_versante { get; set; }
            public string tipo_soggetto_debitore { get; set; }
            public string cf_piva_debitore { get; set; }
            public string cognome_ragsoc_debitore { get; set; }
            public string nome_debitore { get; set; }
            public string indirizzo_debitore { get; set; }
            public string cap_debitore { get; set; }
            public string comune_debitore { get; set; }
            public string nazione_debitore { get; set; }
        }


        public class ReturnControllo
        {
            public string TipoOperazione;
            public int? IdCarrello;
            public bool Esito;
            public string faultCode;
            public string faultString;
            public string description;
        }

        public const string PAA_SYSTEM_ERROR = "PAA_SYSTEM_ERROR";
        public const string PAA_SEMANTICA = "PAA_SEMANTICA";
        public const string PAA_ID_INTERMEDIARIO_ERRATO = "PAA_ID_INTERMEDIARIO_ERRATO";
        public const string PAA_STAZIONE_INT_ERRATA = "PAA_STAZIONE_INT_ERRATA";
        public const string PAA_ID_DOMINIO_ERRATO = "PAA_ID_DOMINIO_ERRATO";
        public const string PAA_PAGAMENTO_SCONOSCIUTO = "PAA_PAGAMENTO_SCONOSCIUTO";
        public const string PAA_PAGAMENTO_IN_CORSO = "PAA_PAGAMENTO_IN_CORSO";
        public const string PAA_PAGAMENTO_DUPLICATO = "PAA_PAGAMENTO_DUPLICATO";
        public const string PAA_PAGAMENTO_SCADUTO = "PAA_PAGAMENTO_SCADUTO";
        public const string PAA_PAGAMENTO_ANNULLATO = "PAA_PAGAMENTO_ANNULLATO";
        public const string PAA_ATTIVA_RPT_IMPORTO_NON_VALIDO = "PAA_ATTIVA_RPT_IMPORTO_NON_VALIDO";

        public const string PAA_SINTASSI_XSD = "PAA_SINTASSI_XSD";
        public const string PAA_RPT_SCONOSCIUTA = "PAA_RPT_SCONOSCIUTA";
        public const string PAA_RT_DUPLICATA = "PAA_RT_DUPLICATA";



        public static void CheckChiave(RequestControllo p_request, string p_proprieta, ref ReturnControllo v_return)
        {
            Type myType = typeof(RequestControllo);
            PropertyInfo myPropInfo = myType.GetProperty(p_proprieta);

            if (p_proprieta != "Importo")
            {
                string p_valore = (string)myPropInfo.GetValue(p_request, null);

                if (string.IsNullOrEmpty(p_valore))
                {
                    v_return.faultCode = PAA_SYSTEM_ERROR;
                    v_return.faultString = "Errore generico";
                    v_return.description = v_return.description + "Dato assente: " + p_proprieta + ", ";
                }
            }
            else if (p_proprieta == "Importo" &&
                     p_request.TipoOperazione == tab_carrello.OPERAZIONE_RPT)
            {
                decimal? p_valore = (decimal?)myPropInfo.GetValue(p_request, null);

                if (!p_valore.HasValue ||
                     p_valore <= 0)
                {
                    v_return.faultCode = PAA_SYSTEM_ERROR;
                    v_return.faultString = "Errore generico";
                    v_return.description = v_return.description + "Dato assente: " + p_proprieta + ", ";
                }
            }
        }

        //Metodo Obsoleto: il metodo vero sta in Publisoftware.GovPay\Helpers
        public static ReturnControllo VerificaPDCreaRPT(RequestControllo p_request, dbEnte dbContext)
        {
            ReturnControllo v_return = new ReturnControllo()
            {
                TipoOperazione = null,
                Esito = false,
                IdCarrello = null,
                faultCode = null,
                faultString = null,
                description = null
            };

            CheckChiave(p_request, nameof(p_request.TipoOperazione), ref v_return);
            CheckChiave(p_request, nameof(p_request.IdentificativoPSP), ref v_return);
            CheckChiave(p_request, nameof(p_request.IdDominioCFEnte), ref v_return);
            CheckChiave(p_request, nameof(p_request.CodiceContesto), ref v_return);
            CheckChiave(p_request, nameof(p_request.IUV), ref v_return);
            CheckChiave(p_request, nameof(p_request.Importo), ref v_return);
            //CheckChiave(p_request, nameof(p_request.ibanAppoggio), ref v_return);
            //CheckChiave(p_request, nameof(p_request.bicAppoggio), ref v_return);
            //CheckChiave(p_request, nameof(p_request.ibanAddebito), ref v_return);
            //CheckChiave(p_request, nameof(p_request.bicAddebito), ref v_return);
            //CheckChiave(p_request, nameof(p_request.tipo_soggetto_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.cf_piva_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.cognome_ragsoc_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.nome_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.indirizzo_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.cap_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.comune_versante), ref v_return);
            //CheckChiave(p_request, nameof(p_request.nazione_versante), ref v_return);

            if (!string.IsNullOrEmpty(v_return.faultCode))
            {
                return v_return;
            }

            anagrafica_ente v_anagraficaEnte = new anagrafica_ente();

            //using (dbEnte enteTempContextGenerale = EnteContextFactory.getContext(p_request.dbServer,
            //                                                                      p_request.dbName,
            //                                                                      p_request.dbUserName,
            //                                                                      CryptMD5.Decrypt(p_request.dbPassWord),
            //                                                                      99,
            //                                                                      2037,
            //                                                                      isReadOnly: false))
            //{
            v_anagraficaEnte = AnagraficaEnteBD.GetList(dbContext)
                                               .WhereByPIVA(p_request.IdDominioCFEnte)
                                               .FirstOrDefault();

            if (v_anagraficaEnte == null)
            {
                v_return.faultCode = PAA_SYSTEM_ERROR;
                v_return.faultString = "Errore generico";
                v_return.description = "Non esiste anagrafica_ente per la p.Iva: " + p_request.IdDominioCFEnte;

                return v_return;
            }
            //}

            v_return.TipoOperazione = p_request.TipoOperazione;

            //using (dbEnte enteTempContext = EnteContextFactory.getContext(v_anagraficaEnte.indirizzo_ip_db, 
            //                                                              v_anagraficaEnte.nome_db, 
            //                                                              v_anagraficaEnte.user_name_db, 
            //                                                              v_anagraficaEnte.password_dbD, 
            //                                                              99, 
            //                                                              2037, 
            //                                                              isReadOnly: false))
            //{
            using (DbContextTransaction v_trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    tab_rata_avv_pag v_rata = TabRataAvvPagBD.GetList(dbContext)
                                                             .WhereByIUV(p_request.IUV)
                                                             .FirstOrDefault();

                    if (v_rata == null)
                    {
                        v_return.Esito = true;
                        v_return.IdCarrello = null;
                        v_return.faultCode = PAA_PAGAMENTO_SCONOSCIUTO;
                        v_return.faultString = "Pagamento in attesa risulta sconosciuto all'Ente Creditore";
                        v_return.description = v_return.faultString;
                    }
                    else
                    {
                        if (v_rata.cod_stato.StartsWith(tab_rata_avv_pag.ATT_INP))
                        {
                            v_return.Esito = false;
                            v_return.IdCarrello = null;
                            v_return.faultCode = PAA_PAGAMENTO_IN_CORSO;
                            v_return.faultString = "Pagamento in attesa risulta in corso all'Ente Creditore";
                            v_return.description = v_return.faultString;
                        }
                        else if (v_rata.cod_stato.StartsWith(tab_rata_avv_pag.ATT_PAG))
                        {
                            v_return.Esito = false;
                            v_return.IdCarrello = null;
                            v_return.faultCode = PAA_PAGAMENTO_DUPLICATO;
                            v_return.faultString = "Pagamento effettuato con pagoPA";
                            v_return.description = v_return.faultString;
                        }
                        else if (v_rata.cod_stato.StartsWith(tab_rata_avv_pag.ATT_REN))
                        {
                            v_return.Esito = false;
                            v_return.IdCarrello = null;
                            v_return.faultCode = PAA_PAGAMENTO_DUPLICATO;
                            v_return.faultString = "Pagamento effettuato già rendicontato";
                            v_return.description = v_return.faultString;
                        }
                        else if (v_rata.cod_stato.StartsWith(tab_rata_avv_pag.SSP_REN))
                        {
                            v_return.Esito = false;
                            v_return.IdCarrello = null;
                            v_return.faultCode = PAA_PAGAMENTO_IN_CORSO;
                            v_return.faultString = "Pagamento sospeso in attesa di notifica da pagoPA";
                            v_return.description = v_return.faultString;
                        }
                        else if (v_rata.cod_stato.StartsWith(tab_rata_avv_pag.ATT_ATT))
                        {
                            if (v_rata.imp_pagato > 0)
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_PAGAMENTO_DUPLICATO;
                                v_return.faultString = "Pagamento già effettuato con modalità differenti da pagoPA";
                                v_return.description = v_return.faultString;
                            }
                            else if (v_rata.tab_avv_pag == null)
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_SYSTEM_ERROR;
                                v_return.faultString = "Errore generico";
                                v_return.description = v_return.faultString;
                            }
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile != "1" &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA &&
                            //         v_rata.tab_avv_pag.dt_emissione.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.dt_emissione.Value.AddDays(120)))
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento indicato dal PSP è riferito ad un sollecito di pagamento i cui termini di pagamento sono scaduti";
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile != "1" &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                            //         v_rata.tab_avv_pag.data_ricezione.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.data_ricezione.Value.AddDays(30)))
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento indicato dal PSP è riferito ad una intimazione di pagamento i cui termini di pagamento sono scaduti";
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile != "1" &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                            //        !v_rata.tab_avv_pag.data_ricezione.HasValue &&
                            //         v_rata.tab_avv_pag.dt_emissione.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.dt_emissione.Value.AddDays(120)))
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento indicato dal PSP è riferito ad una intimazione di pagamento i cui termini di pagamento sono scaduti";
                            //}
                            else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                                    (v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
                                     v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM) &&
                                     v_rata.tab_avv_pag.tab_unita_contribuzione.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                                            x.id_avv_pag_collegato.HasValue &&
                                                                                            x.tab_avv_pag1.tab_unita_contribuzione1.Any(y => !y.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                                                                                             !y.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) &&
                                                                                                                                              y.tab_avv_pag.dt_emissione > v_rata.tab_avv_pag.dt_emissione))
                                                                                .Count() > 0)
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                                if (v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA)
                                {
                                    v_return.faultString = "Pagamento riferito ad un sollecito i cui termini di pagamento sono scaduti";
                                }
                                else
                                {
                                    v_return.faultString = "Pagamento riferito ad una intimazione i cui termini di pagamento sono scaduti";
                                }
                                v_return.description = v_return.faultString;
                            }
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile != "1" &&
                            //         v_rata.tab_avv_pag.id_entrata != anagrafica_entrate.IMU &&
                            //         v_rata.tab_avv_pag.anagrafica_entrate.flag_natura_entrata == anagrafica_entrate.NaturaEntrateTributariaT &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA &&
                            //         v_rata.tab_avv_pag.flag_spedizione_notifica == "1" &&
                            //         v_rata.tab_avv_pag.num_rate == 1 &&
                            //         v_rata.tab_avv_pag.data_ricezione.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.data_ricezione.Value.AddDays(60)))
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento indicato dal PSP è riferito ad un atto di natura tributaria per cui essendo scaduti i termini di pagamento è possibile effettuare il pagamento con Ravvedimento Operoso";
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile != "1" &&
                            //         v_rata.tab_avv_pag.id_entrata != anagrafica_entrate.IMU &&
                            //         v_rata.tab_avv_pag.anagrafica_entrate.flag_natura_entrata == anagrafica_entrate.NaturaEntrateTributariaT &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA &&
                            //         v_rata.tab_avv_pag.flag_spedizione_notifica == "1" &&
                            //         v_rata.tab_avv_pag.num_rate > 1 &&
                            //         v_rata.tab_avv_pag.data_ricezione.HasValue &&
                            //         v_rata.tab_avv_pag.data_scadenza_1_rata.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.data_scadenza_1_rata.Value.AddDays(30 * (v_rata.tab_avv_pag.num_rate.Value - 1) + 10)))
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento indicato dal PSP è riferito ad un atto di natura tributaria per cui essendo scaduti i termini di pagamento è possibile effettuare il pagamento con Ravvedimento Operoso";
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile == "1" &&
                            //         v_rata.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento non può essere effettuato in quanto è già in corso iscrizione di ipoteca";
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile == "1" &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento non può essere effettuato in quanto è già stato emesso Atto di Pignoramento con citazione del terzo";
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile == "1" &&
                            //         v_rata.tab_avv_pag.id_tipo_avvpag != anagrafica_tipo_avv_pag.PRE_FERMO_AMM &&
                            //         v_rata.tab_avv_pag.id_tipo_avvpag != anagrafica_tipo_avv_pag.PRE_IPOTECA &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = null;
                            //}
                            //else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                            //         v_rata.tab_avv_pag.flag_atto_non_pagabile == "1" &&
                            //         v_rata.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM &&
                            //         v_rata.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).FirstOrDefault() != null)
                            //{
                            //    tab_avv_pag v_avvisoEmesso = v_rata.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).FirstOrDefault().tab_avv_pag;

                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Il pagamento indicato dal PSP è riferito al Preavviso di Fermo Amministrativo n. " + v_avvisoEmesso.identificativo_avv_pag +
                            //                           " che è già oggetto di Fermo iscritto – i pagamenti devono essere effettuati con riferimento allo IUV "
                            //                           + v_avvisoEmesso.tab_rata_avv_pag.FirstOrDefault().Iuv_identificativo_pagamento + " con importo " + v_avvisoEmesso.tab_rata_avv_pag.FirstOrDefault().imp_tot_rata_Euro;
                            //}
                            else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME) &&
                                    (v_rata.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM ||
                                     v_rata.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA ||
                                     v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO) &&
                                     v_rata.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.Where(x => (!x.COD_STATO.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)) &&
                                                                                                 x.ID_AVVPAG_EMESSO.HasValue &&
                                                                                                !x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                                   .Count() > 0)
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                                v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                                if (v_rata.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM)
                                {
                                    v_return.description = "Il pagamento riferito ad un preavviso di fermo a fronte del quale è stato già iscritto fermo amministrativo - " +
                                                           "il pagamento deve essere effettuato con riferimento al fermo iscritto";
                                }
                                else if (v_rata.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA)
                                {
                                    v_return.description = "Il pagamento riferito ad un preavviso di ipoteca a fronte del quale è stata già iscritta la successiva ipoteca - " +
                                                           "il pagamento deve essere effettuato con riferimento all'ipoteca iscritta";
                                }
                                else
                                {
                                    v_return.description = "Il pagamento riferito ad un atto di pignoramento con ordine a terzo a fronte del quale è stato già emesso un successivo atto di pignoramento con citazione del terzo - " +
                                                           "il pagamento deve essere effettuato con riferimento all'atto di pignoramento con citazione del terzo";
                                }
                            }
                            else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) ||
                                     v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE))
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_PAGAMENTO_ANNULLATO;
                                v_return.faultString = "Pagamento in attesa risulta annullato all'Ente creditore";
                                v_return.description = "Il pagamento indicato dal PSP è riferito all'atto di pagamento n. " + v_rata.tab_avv_pag.identificativo_avv_pag + " che risulta annullato in autotutela";
                            }
                            else if (v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                                    !v_rata.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME))
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                                v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                                v_return.description = "Il pagamento indicato dal PSP è riferito all'atto di pagamento n. " + v_rata.tab_avv_pag.identificativo_avv_pag + " che risulta già oggetto di successivo atto di riscossione a cui il pagamento da effettuare deve essere riferito";
                            }
                            else if (v_rata.tab_avv_pag.flag_rateizzazione_bis == "1")
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_SYSTEM_ERROR;
                                v_return.faultString = "Errore generico";
                                v_return.description = "Il pagamento indicato dal PSP è riferito all'atto di pagamento n. " + v_rata.tab_avv_pag.identificativo_avv_pag + " che è oggetto di una rateizzazione in corso – i pagamenti devono essere effettuati con riferimento all'atto di rateizzazione";
                            }
                            //else if (p_request.TipoOperazione == tab_carrello.OPERAZIONE_VER)
                            //{
                            //    v_return.Esito = true;
                            //}
                            //else if (v_rata.tab_avv_pag.importo_ridotto.HasValue &&
                            //         v_rata.tab_avv_pag.importo_ridotto.Value > 0 &&
                            //         v_rata.tab_avv_pag.importo_ridotto.Value == Convert.ToDecimal(p_request.Importo) &&
                            //         v_rata.tab_avv_pag.data_ricezione.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.data_ricezione.Value.AddDays(60)) &&
                            //         v_rata.tab_avv_pag.tab_rata_avv_pag.Where(d => d.id_rata_avv_pag != v_rata.id_rata_avv_pag && d.imp_tot_rata == v_rata.tab_avv_pag.imp_tot_avvpag).FirstOrDefault() == null)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = null;
                            //}
                            //else if (v_rata.tab_avv_pag.importo_ridotto.HasValue &&
                            //         v_rata.tab_avv_pag.importo_ridotto.Value > 0 &&
                            //         v_rata.tab_avv_pag.importo_ridotto.Value == Convert.ToDecimal(p_request.Importo) &&
                            //         v_rata.tab_avv_pag.data_ricezione.HasValue &&
                            //         DbFunctions.TruncateTime(DateTime.Now) > DbFunctions.TruncateTime(v_rata.tab_avv_pag.data_ricezione.Value.AddDays(60)) &&
                            //         v_rata.tab_avv_pag.tab_rata_avv_pag.Where(d => d.id_rata_avv_pag != v_rata.id_rata_avv_pag && d.imp_tot_rata == v_rata.tab_avv_pag.imp_tot_avvpag).FirstOrDefault() != null)
                            //{
                            //    tab_rata_avv_pag v_rataCorretta = v_rata.tab_avv_pag.tab_rata_avv_pag.Where(d => d.id_rata_avv_pag != v_rata.id_rata_avv_pag && d.imp_tot_rata == v_rata.tab_avv_pag.imp_tot_avvpag).FirstOrDefault();

                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                            //    v_return.faultString = "Pagamento in attesa risulta scaduto all'Ente creditore";
                            //    v_return.description = "Alla data odierna essendo trascorsi più di 60 giorni dalla notifica dell’Atto di pagamento," +
                            //                           " il pagamento deve essere effettuato con riferimento allo IUV " + v_rataCorretta.Iuv_identificativo_pagamento + " con importo " + v_rataCorretta.imp_tot_rata + "";
                            //}
                            //else if (Convert.ToDecimal(p_request.Importo) > v_rata.tab_avv_pag.importo_tot_da_pagare)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_ATTIVA_RPT_IMPORTO_NON_VALIDO;
                            //    v_return.faultString = "L'importo del pagamento in attesa non è congruente con il dato indicato dal PSP";
                            //    v_return.description = null;
                            //}
                            else if (v_rata.tab_avv_pag.importoTotDaPagare < v_rata.imp_tot_rata &&
                                     v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.ACCERT_ESECUTIVO &&
                                     v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO &&
                                   !(v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO &&
                                     v_rata.tab_avv_pag.id_entrata == anagrafica_entrate.CDS))
                            {
                                v_return.Esito = false;
                                v_return.IdCarrello = null;
                                v_return.faultCode = PAA_ATTIVA_RPT_IMPORTO_NON_VALIDO;
                                v_return.faultString = "L'importo del pagamento indicato è riferito ad un atto il cui residuo da pagare è minore dell'importo del pagamento effettuato";
                                v_return.description = v_return.faultString;
                            }
                            else if (v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO ||
                                     v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO ||
                                    (v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO &&
                                     v_rata.tab_avv_pag.id_entrata == anagrafica_entrate.CDS))
                            {
                                string v_ritorno = tab_rata_avv_pag.GetRataPagabile(null, v_rata.tab_avv_pag);

                                tab_rata_avv_pag v_rataAccertamento = new tab_rata_avv_pag();

                                //if (v_ritorno == "0")
                                //{
                                //    v_rataAccertamento = v_rata.tab_avv_pag.tab_rata_avv_pag.OrderBy(o => o.imp_tot_rata).FirstOrDefault();

                                //    if (v_rata.id_rata_avv_pag != v_rataAccertamento.id_rata_avv_pag)
                                //    {
                                //        v_return.Esito = false;
                                //        v_return.IdCarrello = null;
                                //        v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                                //        v_return.faultString = "Pagamento non effettuabile ";
                                //        v_return.description = v_return.faultString;
                                //    }
                                //}
                                //else 
                                if (v_ritorno == "1")
                                {
                                    v_rataAccertamento = v_rata.tab_avv_pag.tab_rata_avv_pag.OrderByDescending(o => o.imp_tot_rata).FirstOrDefault();

                                    if (v_rata.id_rata_avv_pag != v_rataAccertamento.id_rata_avv_pag)
                                    {
                                        v_return.Esito = false;
                                        v_return.IdCarrello = null;
                                        v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                                        v_return.faultString = "Pagamento non effettuabile per lo IUV indicato. Lo IUV da pagare è " + v_rataAccertamento.Iuv_identificativo_pagamento + " con importo " + v_rataAccertamento.imp_tot_rata_Euro;
                                        v_return.description = v_return.faultString;
                                    }
                                }
                                else
                                {
                                    v_rataAccertamento = null;

                                    v_return.Esito = false;
                                    v_return.IdCarrello = null;
                                    v_return.faultCode = PAA_PAGAMENTO_SCADUTO;
                                    v_return.faultString = "Pagamento non effettuabile perchè per l'atto oggetto di pagamento è stato avviato l'iter di riscossione coattiva";
                                    v_return.description = v_return.faultString;
                                }
                            }
                            //else if (Convert.ToDecimal(p_request.Importo) > v_rata.imp_da_pagare)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_ATTIVA_RPT_IMPORTO_NON_VALIDO;
                            //    v_return.faultString = "L'importo del pagamento in attesa non è congruente con il dato indicato dal PSP";
                            //    v_return.description = "L'importo del pagamento indicato dal PSP è maggiore dell'importo residuo da pagare del pagamento in attesa - " +
                            //                           "L'importo da pagare con il medesimo IUV è pari a " + v_rata.imp_da_pagare_Euro + "";
                            //}
                            //else if (v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI &&
                            //         v_rata.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI &&
                            //         Convert.ToDecimal(p_request.Importo) < v_rata.imp_da_pagare)
                            //{
                            //    v_return.Esito = false;
                            //    v_return.IdCarrello = null;
                            //    v_return.faultCode = PAA_ATTIVA_RPT_IMPORTO_NON_VALIDO;
                            //    v_return.faultString = "L'importo del pagamento in attesa non è congruente con il dato indicato dal PSP";
                            //    v_return.description = "L'importo del pagamento indicato dal PSP è minore dell'importo residuo da pagare del pagamento in attesa - " +
                            //                           "L'importo da pagare con il medesimo IUV è pari a " + v_rata.imp_da_pagare_Euro + "";
                            //}
                            else
                            {
                                v_return.Esito = true;
                            }
                        }
                    }

                    if (!v_return.Esito ||
                        (v_return.Esito && v_return.TipoOperazione == tab_carrello.OPERAZIONE_VER))
                    {
                        return v_return;
                    }
                    else
                    {
                        tab_carrello v_carrello = new tab_carrello();

                        v_carrello.tipo_carrello = tab_carrello.TIPO_CARRELLO_S;
                        v_carrello.fonte_carrello = tab_carrello.FONTE_CARRELLO_PSP;
                        v_carrello.tipo_versamento = tab_carrello.TIPO_VERSAMENTO_PO;
                        //Modifica del 19/10/2020
                        //La chiamata a GovPay a determinato la rimozione dei campi "codice_contesto_pagamento_rpt" e "data_ora_messaggio_richiesta_rpt"
                        //v_carrello.codice_contesto_pagamento_rpt = p_request.CodiceContesto;
                        //v_carrello.data_ora_messaggio_richiesta_rpt = DateTime.Now;

                        string v_anno = DateTime.Now.Year.ToString();
                        string p_iva = p_request.IdDominioCFEnte;
                        string p_codiceSegregazione = v_anagraficaEnte.id_ente == anagrafica_ente.ID_ENTE_PUBLISERVIZI ? "00" : v_rata.Iuv_identificativo_pagamento.Substring(0, 2);
                        string v_progressivoAsHex = TabProgCarrelloBD.ReturnProgressivoIncrementatoByIdEnteAnno(v_anagraficaEnte.id_ente, DateTime.Now.Year, dbContext).ToString("X18");
                        //Modifica del 19/10/2020
                        //La chiamata a GovPay a determinato la rimozione dei campo "identificativo_messaggio_richiesta_rpt" 
                        //v_carrello.identificativo_messaggio_richiesta_rpt = v_anno + p_iva + p_codiceSegregazione + v_progressivoAsHex;

                        v_carrello.data_esecuzione_pagamento_psp = DateTime.Now.AddDays(1);
                        v_carrello.importo_totale_da_pagare = Convert.ToDecimal(p_request.Importo);
                        v_carrello.cf_piva_dominio_ente_creditore = p_request.IdDominioCFEnte;
                        v_carrello.codice_ente_pagopa = v_anagraficaEnte.codice_ente_pagopa;
                        v_carrello.denominazione_ente_creditore = v_anagraficaEnte.descrizione_ente;
                        //Modifica del 19/10/2020
                        //La chiamata a GovPay a determinato la rimozione dei campo "identificativo_PSP" 
                        //v_carrello.identificativo_PSP = p_request.IdentificativoPSP;
                        v_carrello.iban_appoggio = p_request.ibanAppoggio;
                        v_carrello.bic_appoggio = p_request.bicAppoggio;
                        v_carrello.iban_addebito = p_request.ibanAddebito;
                        v_carrello.bic_addebito = p_request.bicAddebito;
                        v_carrello.tipo_soggetto_versante = p_request.tipo_soggetto_versante;
                        v_carrello.cf_piva_versante = p_request.cf_piva_versante;
                        v_carrello.cognome_ragsoc_versante = p_request.cognome_ragsoc_versante;
                        v_carrello.nome_versante = p_request.nome_versante;
                        v_carrello.indirizzo_versante = p_request.indirizzo_versante;
                        v_carrello.cap_versante = p_request.cap_versante;
                        v_carrello.comune_versante = p_request.comune_versante;
                        v_carrello.nazione_versante = p_request.nazione_versante;
                        v_carrello.cod_stato = tab_carrello.ATT_RPT;

                        join_tab_carrello_tab_rate v_joinCarrelloRata = new join_tab_carrello_tab_rate();

                        v_joinCarrelloRata.tab_carrello = v_carrello;
                        v_joinCarrelloRata.id_rata = v_rata.id_rata_avv_pag;
                        v_joinCarrelloRata.importo_da_pagare_rata = v_rata.imp_pagato;
                        v_joinCarrelloRata.bic_accredito = v_rata.tab_cc_riscossione.bic_swift;
                        v_joinCarrelloRata.iban_accredito = v_rata.tab_cc_riscossione.IBAN;
                        v_joinCarrelloRata.id_contribuente_debitore = v_rata.tab_avv_pag.id_anag_contribuente;
                        v_joinCarrelloRata.tipo_soggetto_debitore = v_rata.tab_avv_pag.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente;
                        v_joinCarrelloRata.cf_piva_debitore = v_rata.tab_avv_pag.tab_contribuente.codFiscalePivaDisplay;
                        v_joinCarrelloRata.cognome_ragsoc_debitore = v_rata.tab_avv_pag.tab_contribuente.cognomeRagSoc;
                        v_joinCarrelloRata.nome_debitore = v_rata.tab_avv_pag.tab_contribuente.nome;
                        v_joinCarrelloRata.indirizzo_debitore = v_rata.tab_avv_pag.tab_contribuente.indirizzoDisplay;
                        v_joinCarrelloRata.cap_debitore = v_rata.tab_avv_pag.tab_contribuente.cap;
                        v_joinCarrelloRata.comune_debitore = v_rata.tab_avv_pag.tab_contribuente.cittaDisplay;
                        v_joinCarrelloRata.nazione_debitore = v_rata.tab_avv_pag.tab_contribuente.stato;
                        v_joinCarrelloRata.cod_stato = join_tab_carrello_tab_rate.ATT_RPT;

                        dbContext.join_tab_carrello_tab_rate.Add(v_joinCarrelloRata);

                        dbContext.SaveChanges();
                        v_trans.Commit();

                        v_return.Esito = true;
                        v_return.IdCarrello = v_carrello.id_carrello;
                    }

                    return v_return;
                }
                catch (Exception ex)
                {
                    logger.LogException("Errore in ControlloPagabilitaRata", ex, EnLogSeverity.Error);
                    v_trans.Rollback();

                    return v_return;
                }
            }
            //}
        }

        public static String InviaCarrelloRPT(dbEnte context, int id_carrello, string pagoPaNodeAddress, out bool isError, out string errorDescription)
        {
            isError = false;
            errorDescription = string.Empty;
            try
            {

                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>OK>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                var binding = new BasicHttpsBinding();
                binding.Security.Mode = BasicHttpsSecurityMode.Transport;

                binding.Security.Transport = new HttpTransportSecurity
                {

                    ClientCredentialType = HttpClientCredentialType.Certificate,
                    ProxyCredentialType = HttpProxyCredentialType.None
                    //ProxyCredentialType = HttpProxyCredentialType.Basic
                };
                HttpTransportBindingElement http = new HttpTransportBindingElement();
                http.AuthenticationScheme = System.Net.AuthenticationSchemes.Basic;
                string accept = @"text/html, application/xhtml+xml, */*";
                AddressHeader headerAccept = AddressHeader.CreateAddressHeader("accept", accept, 1);
                AddressHeader headerContentType = AddressHeader.CreateAddressHeader("content-type", "application/xml", 2);
                AddressHeader[] addressHeaders = new AddressHeader[2] { headerAccept, headerContentType };
                EndpointAddress endPoint = new EndpointAddress(new Uri(pagoPaNodeAddress), addressHeaders);

                PagamentiTelematiciRPTClient client = new PagamentiTelematiciRPTClient(binding, endPoint);
                client.ClientCredentials.ClientCertificate.SetCertificate(
                    StoreLocation.LocalMachine,
                    StoreName.My,
                    X509FindType.FindBySubjectName,
                    "addestramento.fiscolocale.it"
                    );


                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>OK>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                //            WSHttpBinding myBinding = new WSHttpBinding();
                //            myBinding.Security.Mode = SecurityMode.Message;
                //            myBinding.Security.Message.ClientCredentialType =
                //                MessageCredentialType.Certificate;

                //            // Disable credential negotiation and the establishment of
                //            // a security context.
                //            myBinding.Security.Message.NegotiateServiceCredential = false;
                //            myBinding.Security.Message.EstablishSecurityContext = false;

                //            EndpointAddress ea = new
                //EndpointAddress(pagoPaNodeAddress);
                //            PagamentiTelematiciRPTClient client = new PagamentiTelematiciRPTClient(myBinding, ea);
                //            client.ClientCredentials.ClientCertificate.SetCertificate(
                //StoreLocation.CurrentUser,
                //StoreName.My,
                //X509FindType.FindBySubjectName,
                //"addestramento.fiscolocale.it");

                //            // Specify a default certificate for the service.
                //            client.ClientCredentials.ServiceCertificate.SetDefaultCertificate(
                //                StoreLocation.CurrentUser,
                //                StoreName.TrustedPeople,
                //                X509FindType.FindBySubjectName,
                //                "gad.test.pagopa.gov.it");

                //------------------------------------------------------------------------------------------------
                //gad.test.pagopa.gov.it
                //var certificate = GetCertificateFromStore(
                //     StoreLocation.LocalMachine, StoreName.My,
                //     X509FindType.FindBySubjectName,
                //     "addestramento.fiscolocale.it",
                //     validOnly: false);

                //client.ClientCredentials.ClientCertificate.Certificate = certificate;
                //client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerOrChainTrust;
                //------------------------------------------------------------------------------------------------

                //WSHttpBinding binding = new WSHttpBinding();
                //binding.Security.Mode = SecurityMode.Transport;
                //binding.Security.Message.ClientCredentialType =
                //    MessageCredentialType.Certificate;
                //binding.Security.Message.NegotiateServiceCredential = true;
                //binding.Security.Message.EstablishSecurityContext = true;
                //var endPoint = new EndpointAddress(pagoPaNodeAddress);

                //PagamentiTelematiciRPTClient client = new PagamentiTelematiciRPTClient(binding, endPoint);
                //client.ClientCredentials.ClientCertificate.SetCertificate(
                //    StoreLocation.LocalMachine,
                //    StoreName.My,
                //    X509FindType.FindBySubjectName,
                //    "addestramento.fiscolocale.it"
                //    );


                var carrello = TabCarrelloBD.GetList(context).Where(c => c.id_carrello == id_carrello).Include(c => c.join_tab_carrello_tab_rate.Select(j => j.tab_rata_avv_pag)).SingleOrDefault();
                var ente = AnagraficaEnteBD.GetList(context).Where(e => e.p_iva == carrello.cf_piva_dominio_ente_creditore).FirstOrDefault();
                anagrafica_ente ente_publiservizi = null;
                if (ente.flag_tipo_gestione_pagopa.Equals(anagrafica_ente.FLAG_TIPO_GESTIONE_PAGOPA_PUBLISERVIZI))
                {
                    ente_publiservizi = AnagraficaEnteBD.GetList(context).Where(e => e.p_iva == "03218060659").SingleOrDefault();
                }
                if (ente == null)
                {
                    isError = true;
                    errorDescription = "Impossibile trovare l'ente.";
                    return "";
                    //throw new Exception("Impossibile trovare l'ente");
                }
                var intermediario = AnagraficaPagoPAIntermediariBD.GetList(context).Where(i => i.id_ente == ente.id_ente).FirstOrDefault();
                if (intermediario == null)
                {
                    isError = true;
                    errorDescription = "Impossibile trovare l'intermediario.";
                    return "";
                    //throw new Exception("Impossibile trovare l'intermediario");
                }
                var stazione = AnagraficaPagoPAStazioniBD.GetList(context).Where(s => s.id_intermediario == intermediario.id_intermediario).FirstOrDefault();
                if (stazione == null)
                {
                    isError = true;
                    errorDescription = "Impossibile trovare la stazione.";
                    return "";
                    //throw new Exception("Impossibile trovare la stazione");
                }

                nodoInviaCarrelloRPT1 inviaRPTReq = new nodoInviaCarrelloRPT1
                {
                    intestazioneCarrelloPPT = new intestazioneCarrelloPPT
                    {
                        identificativoCarrello = carrello.identificativo_carrello,
                        identificativoIntermediarioPA = intermediario.cod_intermediario,
                        identificativoStazioneIntermediarioPA = stazione.cod_stazione
                    },
                    nodoInviaCarrelloRPT = new nodoInviaCarrelloRPT()
                };
                inviaRPTReq.nodoInviaCarrelloRPT.password = stazione.password;

                var rate = carrello.join_tab_carrello_tab_rate;
                var rptList = new List<tipoElementoListaRPT>(rate.Count);
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
                foreach (var rata in rate)
                {
                    ctRichiestaPagamentoTelematico rpt = new ctRichiestaPagamentoTelematico();
                    //Modifica del 19/10/2020
                    //La chiamata a GovPay a determinato la rimozione dei campi "data_ora_messaggio_richiesta_rpt"  e "identificativo_messaggio_richiesta_rpt"
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
                        identificativoDominio = ente.p_iva,
                        identificativoStazioneRichiedente = stazione.cod_stazione
                    };
                    rpt.enteBeneficiario = enteBeneficiario;
                    rpt.autenticazioneSoggetto = stAutenticazioneSoggetto.OTH;
                    rpt.versioneOggetto = PagoPAConsts_Old.RPT_VERSIONE;
                    rpt.datiVersamento = new ctDatiVersamentoRPT
                    {
                        bicAddebito = carrello.bic_addebito,
                        ibanAddebito = carrello.iban_addebito,
                        identificativoUnivocoVersamento = rata.tab_rata_avv_pag.Iuv_identificativo_pagamento,
                        importoTotaleDaVersare = rata.importo_da_pagare_rata.Value,
                        tipoVersamento = (stTipoVersamento)Enum.Parse(typeof(stTipoVersamento), carrello.tipo_versamento),
                        datiSingoloVersamento = new ctDatiSingoloVersamentoRPT[1]
                    };
                    rpt.datiVersamento.datiSingoloVersamento[0] = new ctDatiSingoloVersamentoRPT
                    {
                        bicAccredito = rata.bic_accredito,
                        ibanAccredito = rata.bic_accredito,
                        importoSingoloVersamento = rata.importo_da_pagare_rata.Value,
                        causaleVersamento = ""
                    };
                    //Modifica del 19/10/2020
                    //La chiamata a GovPay a determinato la rimozione del campo "codice_contesto_pagamento_rpt"  
                    //rpt.datiVersamento.codiceContestoPagamento = carrello.codice_contesto_pagamento_rpt;
                    var tipoElementoLista = new tipoElementoListaRPT();
                    //tipoElementoLista.codiceContestoPagamento = carrello.codice_contesto_pagamento_rpt;
                    tipoElementoLista.identificativoDominio = ente.p_iva;
                    tipoElementoLista.identificativoUnivocoVersamento = rata.tab_rata_avv_pag.Iuv_identificativo_pagamento;
                    XmlSerializer bf = new XmlSerializer(typeof(ctRichiestaPagamentoTelematico));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bf.Serialize(ms, rpt);
                        tipoElementoLista.rpt = ms.ToArray();
                    }
                    rptList.Add(tipoElementoLista);
                }
                inviaRPTReq.nodoInviaCarrelloRPT.listaRPT = rptList.ToArray();

                var inviaRPTResponse = client.nodoInviaCarrelloRPT(inviaRPTReq);
                // TODO: scrivere log in giornale eventi inviaRPT
                if (inviaRPTResponse.nodoInviaCarrelloRPTRisposta.esitoComplessivoOperazione == "OK")
                {
                    return inviaRPTResponse.nodoInviaCarrelloRPTRisposta.url;
                }
                else
                {
                    isError = true;
                    errorDescription = "Impossibile inviare il carrello RPT al nodo PagoPA.";
                    return "";
                    //throw new Exception("Impossibile inviare il carrello RPT al nodo PagoPA");
                }
            }
            catch (Exception e)
            {
                logger.LogMessage(String.Format("Impossibile inviare la carrello RPT: {0}", e.ToString()), EnLogSeverity.Error);
                // TODO: scrivere log in giornale eventi inviaRPT
                isError = true;
                errorDescription = String.Format("Impossibile inviare la carrello RPT: {0}", e.ToString());
                return ""; ;
            }
        }

    }


}
