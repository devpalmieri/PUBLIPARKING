using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinSezioniStampaTipoAvvPagBD : EntityBD<join_sezioni_stampa_tipo_avv_pag>
    {
        //Priorità di selezione del testo della sezione
        //          ID_TIPO_AVV_PAG     ID_ENTE     ID_ENTE_GESTITO
        //      1        VAL              VAL             VAL
        //      2        VAL              VAL             NULL
        //      3        NULL             VAL             VAL
        //      4        NULL             VAL             NULL
        //      5        VAL              NULL            NULL
        //      6        NULL             NULL            NULL

        private static string SezioneRaccomandataLogo = "RaccomandataLogo";
        private static string SezioneDestinatarioRaccomandata = "DestinatarioRaccomandata";
        private static string NOME_ENTE = "#ENTE#";
        //private static string COGNOME_NOME_RAGIONE_SOCIALE = "#COGNOME_NOME_RAGIONE_SOCIALE#";
        private static string INDIRIZZO = "#INDIRIZZO#";
        //private static string CAP_COMUNE_PROV = "#CAP_COMUNE_PROV#";
        private static string SezioneModalitaPagamento = "ModalitaPagamento";
        private static string NUMERO_CC = "#NUMERO_CC#";
        private static string COGNOME_NOME_RISORSA_RESPONSABILE = "#COGNOME_NOME_RISORSA_RESPONSABILE#";
        private static string RUOLO_MANSIONE = "#RUOLO_MANSIONE#";
        private static string ENTE_RISORSA = "#ENTE_RISORSA#";
        private static string SezioneInformativaAntiriciclaggio = "InformativaAntiriciclaggio";
        private static string SezioneInformativaPrivacy = "InformativaPrivacy";
        private static string SezioneSoggettoDebitore = "SoggettoDebitore";
        private static string SezioneResponsabili = "SezioneResponsabili";
        private static string SezioneResponsabiliSmall = "SezioneResponsabiliSmall";
        private static string InformazioniNotificatoreSmall = "InformazioniNotificatoreSmall";
        private static string SezioneIdentificativoAvviso = "IdentificativoAvviso";
        private static string SezioneTestataConcessionario = "TestataConcessionario";
        private static string SezioneDestinatarioRevocaFermo = "DestinatarioRevocaFermo";
        private static string TestoSollecitoPagamentoPI = "TestoSollecitoPagamentoPI"; //pre ingiunzione
        private static string TestoDichiarazioniMendaci = "TestoDichiarazioniMendaci";
        private static string TestoPrivacyPiccolo = "TestoPrivacyPiccolo";
        //private static string SezioneTestoRicorsoTributario = "TestoRicorsoTributario";
        //private static string SezioneTestoRicorsoExtraTributario = "TestoRicorsoExtraTributario";
        private static string SezioneTestoRicorsoTributarioBivalente = "TestoRicorsoExtra_E_Tributario";
        private static string SezioneTestoPremessaPreavvisoFermo = "TestoPremessaPreavvisoFermo";
        private static string TestoIntimazioneFront = "TestoIntimazioneFront";
        private static string TestoIngiunzione = "TestoIngiunzione";
        private static string TestoTrattamentoDati = "TrattamentoDati";
        private static string SezioneInfoSportello = "InfoSportello";
        private static string SezioneDestinatarioTerzo = "DestinatarioTerzo";
        private static string SezioneOggettoLiberatoriaTerzo = "OggettoLiberatoriaTerzo";
        private static string SezioneOggettoRinuncia = "TestoOggettoRinunciaDNterzo"; //rinuncia per dichiarazione negativa del terzo
        private static string SezioneTestoAnnullamentoInAutotutela = "TestoEsitoAnnAutotutela";
        private static string SezioneTestoRetroBollettino = "InfoeAvvertenzeRetroBollettino";
        private static string SezioneTestoPremessaAvvRateizzazione = "TestoPremessaAvvRateizzazione";
        private static string SezioneTestoComePagare = "ComePagare";
        private static string SezioneTestoRate = "TestoRate";
        private static string INTESTAZIONE_CC = "#INTESTAZIONE_CC#";
        private static string IBAN = "#IBAN#";
        private static string CAUSALE_BONIFICO = "#CAUSALE_BONIFICO#";
        private static string NOME_TERZO = "#NOME_TERZO#";
        private static string INDIRIZZO_TERZO = "#INDIRIZZO_TERZO#";
        private static string COD_LINE = "#COD_LINE#";
        //private static string RESP_APPROVAZIONE = "#RESP_APPROVAZIONE#";
        //private static string RESP_NOTIFICA = "#COGNOME_NOME_RISORSA_RESPONSABILE#";
        private static string RESP_EMISSIONE = "#RESPONSABILE_EMISSIONE#";
        //private static string DIR_SERVIZIO = "#DIR_SERVIZIO#";
        private static string RIFERIMENTO_AVVISO_REVOCA_FERMO = "#RIFERIMENTO_AVVISO_REVOCA_FERMO#";
        private static string SezioneOggettoRevocaFermo = "OggettoRevocaFermo";
        private static string SezioneLeggeRevocaFermo = "RifLeggeRevocaFermo";
        private static string SezioneOggettoSospensioneFermo = "OggettoSospensioneFermo";
        private static string SezioneResponsabileRevocaFermo = "RisorsaResponsabileRevocaFermo";
        private static string SezioneTestoPreavvisofermoAmministrativo = "TestoPremessaPreavvisoFermo";
        private static string TestoAvvertenzePreavvisoFermo = "AvvertenzePreavvisoFermo";
        private static string SezioneTestoFermoAmministrativo = "TestoPremessaFermoAmm";
        private static string SezioneTestoPremessaSollecito = "TestoPremessaSollecito";
        //private static string SezioneTestoPremessaSollecitoT = "TestoPremessaSollecitoT";
        //private static string SezioneTestoPremessaSollecitoG = "TestoPremessaSollecitoG";
        private static string SezioneTestoPremessaIntimazione = "SezioneTestoPremessaIntimazione";
        private static string SezioneTestoPremessaIntimazioneT = "SezioneTestoPremessaIntimazioneT";
        private static string SezioneTestoPremessaIntimazioneG = "SezioneTestoPremessaIntimazioneG";
        private static string TestoPignoramentoVSTerzi = "PremessaPignoramentiVSTerzi";
        private static string TestoTerminiPagamentoPignoramentoVSTerzi = "TerminiPagamentoPignoramenti";
        private static string TestoAvvertenzePignoramentiVSTerzo = "AvvertenzePignoramentiVSTerzo";
        private static string TestoIstanzaInAutotutela = "TestoIstanzaInAutotutela";
        private static string TestoIstanzaDiRevisione = "IstanzaDiRevisione";
        private static string TestoPignoramentoVSTerziTO = "PignoramentoVSTerziTO";
        private static string EURO_SPESE_FERMO = "#EURO_SPESE_FERMO#";
        private static string SezioneRichiestaAnnullamentoRettifica = "RichiestaAnnRet";
        private static string SezioneInformazioniNotificatore = "InformazioniNotificatore";
        private static string COGNOME_NOME_RISORSA_NOTIFICATORE = "#COGNOME_NOME_RISORSA_NOTIFICATORE#";
        private static string RUOLO_MANSIONE_NOTIFICATORE = "#RUOLO_MANSIONE_NOTIFICATORE#";
        private static string DESCRIZIONE_AVVISO_LIBERATORIA_TERZO = "#DESCRIZIONE_AVVISO_LIBERATORIA_TERZO#";
        private static string RIFERIMENTO_LIBERATORIA_TERZO = "#RIFERIMENTO_LIBERATORIA_TERZO#";
        private static string IDENTIFICATIVO_ATTO = "#IDENTIFICATIVO_ATTO#";
        private static string TestoNoteIstanza = "TestoNoteIstanzaRate";
        private static string TestoNoteRate = "TestoNoteRate";
        private static string TestoNoteIstanzaAnnRet = "TestoNoteIstanzaAnnRet";
        private static string PremessaAvvisoDiIpoteca = "PremessaAvvisoDiIpoteca";
        private static string AvvertenzePreavvisoIpoteca = "AvvertenzePreavvisoIpoteca";
        private static string TestoEsitoIstanzaAnnRet = "TestoEsitoIstanzaAnnRet";
        private static string PremessaPignoramentiConCitazioneAlTerzo = "PremessaCitazioneAlTerzo";
        private static string PremessaPignoramentiConOrdineAlTerzo = "PremessardineAlTerzo";
        private static string TestoCitazione_Cita = "TestoCitazione_Cita";
        private static string TestoCitazioneUffRisc = "TestoCitazioneUR";
        private static string SezioneTestoComePagareSollecitoFI = "ModalitaPagamentoSinteticoFI";
        private static string SezioneTestoPremessaAvvRottamazione = "PremessaAvvisoRottamazione";
        private static string SezioneTestoPremessaAvvRottamazioneLR7_2019 = "PremessaAvvisoRotLR7-2019"; //legge regionale Lombardia 
        private static string SezioneTestoPremessaComRottamazioneLR7_2019 = "PremessaComunicaRotLR7-2019"; // 
        public JoinSezioniStampaTipoAvvPagBD()
        {
        }

        private static string GetSezione(tab_avv_pag p_avviso, string p_nomeSezione)
        {
            //Ricerca per ente e tipo avviso
            string v_sezione = p_avviso.anagrafica_tipo_avv_pag.join_sezioni_stampa_tipo_avv_pag_view
                .Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione
                && j.id_ente == p_avviso.id_ente && j.id_tipo_avv_pag == p_avviso.id_tipo_avvpag)
                .Select(s => s.testo).FirstOrDefault();

            if (v_sezione == null)
            {
                v_sezione = p_avviso.anagrafica_tipo_avv_pag.join_sezioni_stampa_tipo_avv_pag_view
                    .Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione)
                    .Select(s => s.testo).FirstOrDefault();
            }
            return v_sezione;

        }
        private static string GetSezione(tab_avv_pag_fatt_emissione p_avviso, string p_nomeSezione)
        {
            //Ricerca per ente gestito e tipo avviso
            string v_sezione = p_avviso.anagrafica_tipo_avv_pag.join_sezioni_stampa_tipo_avv_pag_view.Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione && j.id_ente == p_avviso.id_ente).Select(s => s.testo).FirstOrDefault();
            if (v_sezione == null)
            {
                v_sezione = p_avviso.anagrafica_tipo_avv_pag.join_sezioni_stampa_tipo_avv_pag_view
                .Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione).
                Select(s => s.testo).SingleOrDefault();
            }
            return v_sezione;
        }
        private static string GetSezioneFlash(dbEnte p_dbContext, string p_nomeSezione, tab_denunce_contratti p_denunce = null)
        {
            //Ricerca per ente gestito e tipo avviso
            string v_sezione = string.Empty;
            if (p_denunce != null)
            {
                v_sezione = JoinSezioniStampaTipoAvvPagBD.GetList(p_dbContext).Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione && j.id_ente == p_denunce.id_ente).Select(s => s.testo).SingleOrDefault();
            }

            if (v_sezione == null || v_sezione == string.Empty)
            {
                v_sezione = JoinSezioniStampaTipoAvvPagBD.GetList(p_dbContext).Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione && j.id_ente == null).Select(s => s.testo).SingleOrDefault();
            }
            return v_sezione;
        }
        private static string GetSezione(join_tab_avv_pag_tab_doc_input p_istanza, string p_nomeSezione)
        {
            //Ricerca per ente gestito e tipo avviso


            join_sezioni_stampa_tipo_avv_pag_view v_sezione = p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.join_sezioni_stampa_tipo_avv_pag_view.Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione && j.id_ente == p_istanza.tab_doc_input.id_ente && j.id_ente_gestito == null && j.id_tipo_avv_pag == null).FirstOrDefault();
            if (v_sezione == null)
            {
                v_sezione = p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.join_sezioni_stampa_tipo_avv_pag_view.Where(j => j.anagrafica_sezioni_stampa.nome_sezione == p_nomeSezione).FirstOrDefault();
            }

            return v_sezione.testo;
        }

        public static string GetRaccomandataLogo(tab_sped_not p_avviso)
        {
            string v_testo = GetSezione(p_avviso.tab_avv_pag, SezioneRaccomandataLogo);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                switch (p_avviso.id_tipo_spedizione_notifica)
                {
                    case anagrafica_tipo_spedizione_notifica.ID_NEXIVE_RACCOMANDATA_AR:
                    case anagrafica_tipo_spedizione_notifica.ID_NEXIVE_RACCOMANDATA_SEMPLICE:
                    case anagrafica_tipo_spedizione_notifica.ID_POSTA_RACCOMANDATA:
                        v_testo = v_testo.Replace("#MANCATO_RECAPITO#", GetSezione(p_avviso.tab_avv_pag, "MancatorecapitoPostaAR"));
                        break;
                    case anagrafica_tipo_spedizione_notifica.SIGLA_NAG_RACCOMANDATA_AG_ID:
                    case anagrafica_tipo_spedizione_notifica.SIGLA_NUR_UFFICIALE_RISCOSSIONE_ID:
                    case anagrafica_tipo_spedizione_notifica.SIGLA_NUG_UFFICIALE_GIUDIZIARIO_ID:
                        v_testo = v_testo.Replace("#MANCATO_RECAPITO#", GetSezione(p_avviso.tab_avv_pag, "MancatorecapitoCitazioni"));
                        break;
                    default:
                        v_testo = v_testo.Replace("#MANCATO_RECAPITO#", "");
                        break;
                }
            }

            return v_testo;
        }
        public static string GetRaccomandataLogoEnte(tab_sped_not p_avviso)
        {
            string v_testo = GetSezione(p_avviso.tab_avv_pag, "RaccomandataLogoEnte");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                switch (p_avviso.id_tipo_spedizione_notifica)
                {
                    case anagrafica_tipo_spedizione_notifica.ID_NEXIVE_RACCOMANDATA_AR:
                    case anagrafica_tipo_spedizione_notifica.ID_NEXIVE_RACCOMANDATA_SEMPLICE:
                    case anagrafica_tipo_spedizione_notifica.ID_POSTA_RACCOMANDATA:
                        v_testo = v_testo.Replace("#MANCATO_RECAPITO#", GetSezione(p_avviso.tab_avv_pag, "MancatorecapitoPostaAR"));
                        break;
                    case anagrafica_tipo_spedizione_notifica.SIGLA_NAG_RACCOMANDATA_AG_ID:
                    case anagrafica_tipo_spedizione_notifica.SIGLA_NUR_UFFICIALE_RISCOSSIONE_ID:
                    case anagrafica_tipo_spedizione_notifica.SIGLA_NUG_UFFICIALE_GIUDIZIARIO_ID:
                        v_testo = v_testo.Replace("#MANCATO_RECAPITO#", GetSezione(p_avviso.tab_avv_pag, "MancatorecapitoCitazioni"));
                        break;
                    default:
                        v_testo = v_testo.Replace("#MANCATO_RECAPITO#", "");
                        break;
                }
            }

            return v_testo;
        }
        public static string GetRaccomandataLogo(tab_avv_pag_fatt_emissione p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneRaccomandataLogo);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                //v_testo = v_testo.Replace(NOME_ENTE, p_avviso.anagrafica_ente.descrizione_ente);
            }

            return v_testo;
        }
        public static string GetDestinatarioRaccomandata(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneDestinatarioRaccomandata);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni

            }

            return v_testo;
        }
        public static string GetDestinatarioRaccomandata(tab_avv_pag_fatt_emissione p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneDestinatarioRaccomandata);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni

            }

            return v_testo;
        }


        public static string GetModalitaPagamento(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneModalitaPagamento);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(NUMERO_CC, "");
                v_testo = v_testo.Replace(INTESTAZIONE_CC, "INTESTAZIONE_CC");
                v_testo = v_testo.Replace(IBAN, "IBAN");
            }

            return v_testo;
        }

        public static string GetInformativaAntiriciclaggio(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneInformativaAntiriciclaggio);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(NUMERO_CC, "VALORE CC");
            }

            return v_testo;
        }

        public static string GetInformativaAntiriciclaggio(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, SezioneInformativaAntiriciclaggio);

            return v_testo;
        }

        public static string GetTestataConcessionario(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestataConcessionario);
            v_testo = v_testo.Replace(NOME_ENTE, p_avviso.anagrafica_ente.descrizione_ente);

            return v_testo;
        }

        public static string GetLogoEntePagoPA(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "GetLogoEntePagoPA");
            //v_testo = v_testo.Replace(NOME_ENTE, p_avviso.anagrafica_ente.descrizione_ente);

            return v_testo;
        }
        public static string GetTestataConcessionario(dbEnte p_dbContext, tab_denunce_contratti p_denunce)
        {
            string v_testo = GetSezioneFlash(p_dbContext, SezioneTestataConcessionario, p_denunce);
            v_testo = v_testo.Replace(NOME_ENTE, p_denunce.anagrafica_ente.descrizione_ente);

            return v_testo;
        }
        public static string GetTestataProcedente(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestataProcedente");
            v_testo = v_testo.Replace(NOME_ENTE, p_avviso.anagrafica_ente.descrizione_ente);

            return v_testo;
        }

        public static string GetTestatoComunicazioneRiemissione(tab_sped_not p_sped)
        {
            string v_testo = GetSezione(p_sped.tab_avv_pag, "TestoComunicazioneRiemissione");
            v_testo = v_testo.Replace("#DESCRIZIONE_ATTO#", p_sped.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n." + p_sped.tab_avv_pag.tab_avv_pag_riemesso.FirstOrDefault().identificativo_avv_pag);
            v_testo = v_testo.Replace("#DESCRIZIONE_NUOVO_ATTO#", p_sped.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n." + p_sped.tab_avv_pag.identificativo_avv_pag);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", p_sped.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", GetSoggettoDebitore(p_sped));

            return v_testo;
        }
        public static string GetTestataConcessionarioSmall(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestataEnteCreditore");
            return v_testo;
        }
        public static string GetTestataConcessionario(tab_avv_pag_fatt_emissione p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestataConcessionario);
            v_testo = v_testo.Replace(NOME_ENTE, p_avviso.anagrafica_ente.descrizione_ente);

            return v_testo;
        }
        public static string GetTestataConcessionario(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            if (p_istanza.tab_avv_pag == null)
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag_fatt_emissione, SezioneTestataConcessionario);
            }
            else
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag, SezioneTestataConcessionario);
            }
            v_testo = v_testo.Replace(NOME_ENTE, p_istanza.tab_doc_input.anagrafica_ente.descrizione_ente);

            return v_testo;
        }
        //GetTestoTrattamentoDati
        public static string GetTestoTrattamentoDati(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, TestoTrattamentoDati);
            return v_testo;
        }
        //TestoDichiarazioniMendaci
        public static string GetTestoDichiarazioniMendaci(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            if (p_istanza.tab_avv_pag == null)
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag_fatt_emissione, TestoDichiarazioniMendaci);
            }
            else
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag, TestoDichiarazioniMendaci);
            }
            return v_testo;
        }
        public static string GetTestoDichiarazioniMendaci(dbEnte p_dbContext)
        {
            string v_testo = string.Empty;

            v_testo = GetSezioneFlash(p_dbContext, TestoDichiarazioniMendaci);

            return v_testo;
        }
        // GetTestoPrivacyPiccolo
        public static string GetTestoPrivacyPiccolo(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            string sDatiComune = string.Empty;

            if (p_istanza.tab_avv_pag == null)
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag_fatt_emissione, TestoPrivacyPiccolo);
                sDatiComune = p_istanza.tab_avv_pag_fatt_emissione.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_istanza.tab_avv_pag_fatt_emissione.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_istanza.tab_avv_pag_fatt_emissione.anagrafica_ente.cap + " " + p_istanza.tab_avv_pag_fatt_emissione.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_istanza.tab_avv_pag_fatt_emissione.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            }
            else
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag, TestoPrivacyPiccolo);
                sDatiComune = p_istanza.tab_avv_pag.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_istanza.tab_avv_pag.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_istanza.tab_avv_pag.anagrafica_ente.cap + " " + p_istanza.tab_avv_pag.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_istanza.tab_avv_pag.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);

            }
            return v_testo;
        }

        public static string GetTestoPrivacyIstanze(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            string sDatiComune = string.Empty;
            if (p_istanza.tab_avv_pag == null)
            {

                v_testo = GetSezione(p_istanza.tab_avv_pag_fatt_emissione, "PrivacyIstanze");
            }
            else
            {
                v_testo = GetSezione(p_istanza.tab_avv_pag, "PrivacyIstanze");
            }

            return v_testo;
        }
        public static string GetTestoPrivacyIstanze(dbEnte p_dbContext)
        {
            string v_testo = string.Empty;
            string sDatiComune = string.Empty;
            if (p_dbContext != null)
            {
                v_testo = GetSezioneFlash(p_dbContext, "PrivacyIstanze");
            }

            return v_testo;
        }
        public static string GetTestoNuovaPrivacyIstanze(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, "NuovaPrivacy");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                string sDatiComune = p_istanza.tab_avv_pag.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_istanza.tab_avv_pag.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_istanza.tab_avv_pag.anagrafica_ente.cap + " " + p_istanza.tab_avv_pag.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_istanza.tab_avv_pag.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                if (p_istanza.tab_avv_pag.anagrafica_ente.cod_fiscale != null)
                {
                    sDatiComune = " " + sDatiComune + " - C.F.:" + p_istanza.tab_avv_pag.anagrafica_ente.cod_fiscale;
                }
                if (p_istanza.tab_avv_pag.anagrafica_ente.pec != null)
                {
                    sDatiComune = " " + sDatiComune + " pec:" + p_istanza.tab_avv_pag.anagrafica_ente.pec;
                }
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            }

            return v_testo;
        }
        public static string GetTestoPrivacyPiccolo(tab_avv_pag p_avviso)
        {
            string v_testo = string.Empty;
            string sDatiComune = string.Empty;

            if (p_avviso == null)
            {
                v_testo = GetSezione(p_avviso, TestoPrivacyPiccolo);
                sDatiComune = p_avviso.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.cap + " " + p_avviso.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_avviso.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            }
            else
            {
                v_testo = GetSezione(p_avviso, TestoPrivacyPiccolo);
                sDatiComune = p_avviso.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.cap + " " + p_avviso.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_avviso.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);

            }
            return v_testo;
        }
        public static string GetDestinatarioRevocaFermo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneDestinatarioRevocaFermo);
            v_testo = v_testo.Replace(NOME_TERZO, p_avviso.tab_contribuente.nominativoDisplay);
            v_testo = v_testo.Replace(INDIRIZZO_TERZO, p_avviso.tab_contribuente.indirizzoRaccomandata);
            return v_testo;
        }
        public static string GetDestinatarioTerzo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneDestinatarioTerzo);
            v_testo = v_testo.Replace(NOME_TERZO, p_avviso.tab_contribuente.nominativoDisplay);
            v_testo = v_testo.Replace(INDIRIZZO_TERZO, p_avviso.tab_contribuente.indirizzoRaccomandata);
            return v_testo;

        }
        public static string GetResponsabileRevocaFermo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneResponsabileRevocaFermo);

            v_testo = v_testo.Replace(COGNOME_NOME_RISORSA_RESPONSABILE, p_avviso.tab_sped_not.Select(s => s.anagrafica_risorse1.CognomeNome).FirstOrDefault());
            v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_sped_not.Select(s => s.anagrafica_risorse1.anagrafica_ente.descrizione_ente).FirstOrDefault());
            v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione);

            return v_testo;
        }
        public static string GetInformativaPrivacy(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "NuovaPrivacy");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                string sDatiComune = p_avviso.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.cap + " " + p_avviso.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_avviso.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                if (p_avviso.anagrafica_ente.cod_fiscale != null)
                {
                    sDatiComune = " " + sDatiComune + " - C.F.:" + p_avviso.anagrafica_ente.cod_fiscale;
                }
                if (p_avviso.anagrafica_ente.pec != null)
                {
                    sDatiComune = " " + sDatiComune + " pec:" + p_avviso.anagrafica_ente.pec;
                }
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            }

            //if (v_testo != null)
            //{
            //    //Se ha trovato la sezione, effettua le sostituzioni
            //    string sDatiComune = p_avviso.anagrafica_ente.descrizione_ente + " ";
            //    sDatiComune = sDatiComune + p_avviso.anagrafica_ente.indirizzo + " - ";
            //    sDatiComune = sDatiComune + p_avviso.anagrafica_ente.cap + " " + p_avviso.anagrafica_ente.ser_comuni.des_comune
            //        + " (" + p_avviso.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
            //    v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);



            return v_testo;
        }

        public static string GetNuovaInformativaPrivacy(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "NuovaPrivacy");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                string sDatiComune = p_avviso.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.cap + " " + p_avviso.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_avviso.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                if (p_avviso.anagrafica_ente.cod_fiscale != null)
                {
                    sDatiComune = " " + sDatiComune + " - C.F.:" + p_avviso.anagrafica_ente.cod_fiscale;
                }
                if (p_avviso.anagrafica_ente.pec != null)
                {
                    sDatiComune = " " + sDatiComune + " pec:" + p_avviso.anagrafica_ente.pec;
                }
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            }

            return v_testo;
        }

        public static string GetNuovaInformativaPrivacySmall(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "NuovaPrivacySmall");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                string sDatiComune = p_avviso.anagrafica_ente.descrizione_ente + " ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.indirizzo + " - ";
                sDatiComune = sDatiComune + p_avviso.anagrafica_ente.cap + " " + p_avviso.anagrafica_ente.ser_comuni.des_comune
                    + " (" + p_avviso.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
                if (p_avviso.anagrafica_ente.cod_fiscale != null)
                {
                    sDatiComune = " " + sDatiComune + " - C.F.:" + p_avviso.anagrafica_ente.cod_fiscale;
                }
                if (p_avviso.anagrafica_ente.pec != null)
                {
                    sDatiComune = " " + sDatiComune + " pec:" + p_avviso.anagrafica_ente.pec;
                }
                v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            }

            return v_testo;
        }
        public static string RichiestaAnnullamentoRettifica(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneRichiestaAnnullamentoRettifica);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                // v_testo = v_testo.Replace(NUMERO_CC, "VALORE CC");
            }

            return v_testo;
        }

        public static string GetInformativaPrivacy(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, SezioneInformativaPrivacy);
            string sDatiComune = p_istanza.tab_avv_pag.anagrafica_ente.descrizione_ente + " ";
            sDatiComune = sDatiComune + p_istanza.tab_avv_pag.anagrafica_ente.indirizzo + " - ";
            sDatiComune = sDatiComune + p_istanza.tab_avv_pag.anagrafica_ente.cap + " " + p_istanza.tab_avv_pag.anagrafica_ente.ser_comuni.des_comune
                + " (" + p_istanza.tab_avv_pag.anagrafica_ente.ser_comuni.ser_province.sig_provincia + ")";
            v_testo = v_testo.Replace("#DATI_ANAG_ENTE#", sDatiComune);
            return v_testo;
        }
        public static string GetTestoResponsabili(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneResponsabili);


            if (v_testo != null && p_avviso.id_ente != anagrafica_ente.ID_ENTE_COMUNE_DI_FIRENZE)
            {

                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.CognomeNome);
                v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione);
                v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ente.descrizione_ente);
            }
            return v_testo;
        }

        public static string GetTestoResponsabiliIntimazioni(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "ResponsabiliIntimazioni");


            //if (v_testo != null)
            //{

            //    //Se ha trovato la sezione, effettua le sostituzioni
            //    v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.CognomeNome).FirstOrDefault());
            //    v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.anagrafica_ruolo_mansione.descr_ruolo_mansione).FirstOrDefault());
            //    v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.anagrafica_ente.descrizione_ente).FirstOrDefault());
            //}
            return v_testo;
        }
        public static string GetTestoResponsabiliEmissioneAttiCautelari(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoResponsabiliAttiCautelari");

            if (v_testo != null)
            {
                ////Se ha trovato la sezione, effettua le sostituzioni
                //v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.CognomeNome).FirstOrDefault());
                //v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.anagrafica_ruolo_mansione.descr_ruolo_mansione).FirstOrDefault());
                //v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.anagrafica_ente.descrizione_ente).FirstOrDefault());
            }

            return v_testo;
        }

        public static string GetTestoResponsabiliNotificaAttiCautelari(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoNotificatoriAttiCautelari");

            if (v_testo != null)
            {
                ////Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.CognomeNome);
                v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione);
                v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ente.descrizione_ente);
            }

            return v_testo;
        }

        public static string GetTestoResponsabiliSmall(tab_avv_pag p_avviso, dbEnte p_dbContext)
        {
            string v_testo = GetSezione(p_avviso, SezioneResponsabiliSmall);

            if (v_testo != null && (p_avviso.id_ente != anagrafica_ente.ID_ENTE_COMUNE_DI_SIGNA && p_avviso.id_ente != anagrafica_ente.ID_ENTE_COMUNE_DI_PONTASSIEVE))
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                if (p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1 != null)
                {
                    v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.CognomeNome);
                    v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione);
                    v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.anagrafica_ente.descrizione_ente);
                }
                else
                {
                    string v_risorsa = AnagraficaRisorseBD.GetById(p_avviso.tab_sped_not.FirstOrDefault().id_risorsa_resp_emissione_avvpag.Value, p_dbContext).CognomeNome;
                    int v_id_ruolo = AnagraficaRisorseBD.GetById(p_avviso.tab_sped_not.FirstOrDefault().id_risorsa_resp_emissione_avvpag.Value, p_dbContext).id_ruolo_mansione.Value;
                    int v_id_ente = AnagraficaRisorseBD.GetById(p_avviso.tab_sped_not.FirstOrDefault().id_risorsa_resp_emissione_avvpag.Value, p_dbContext).id_ente_appartenenza.Value;
                    string v_ruolo = AnagraficaRuoloMansioneBD.GetById(v_id_ruolo, p_dbContext).descr_ruolo_mansione;
                    string v_ente = AnagraficaEnteBD.GetById(v_id_ente, p_dbContext).descrizione_ente;
                    v_testo = v_testo.Replace(RESP_EMISSIONE, v_risorsa);
                    v_testo = v_testo.Replace(RUOLO_MANSIONE, v_ruolo);
                    v_testo = v_testo.Replace(ENTE_RISORSA, v_ente);
                }
            }

            return v_testo;
        }
        public static string GetTestoResponsabiliEsiti(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoResponsabiliEsiti");

            if (v_testo != null)
            {
                //Verifica se si tratta di una rateizzazione
                if (p_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA)
                {
                    tab_modalita_rate_avvpag_view v_modalita = p_avviso.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault();
                    //Se ha trovato la sezione, effettua le sostituzioni
                    v_testo = v_testo.Replace(RESP_EMISSIONE, v_modalita.anagrafica_risorse1.CognomeNome); // .Select(s => s.anagrafica_risorse.CognomeNome).FirstOrDefault());
                    v_testo = v_testo.Replace(RUOLO_MANSIONE, v_modalita.anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione);
                    v_testo = v_testo.Replace(ENTE_RISORSA, v_modalita.anagrafica_risorse1.anagrafica_ente.descrizione_ente);
                }
                else
                {
                    //Se ha trovato la sezione, effettua le sostituzioni
                    if (p_avviso.tab_liste.anagrafica_risorse2 != null)
                    {
                        v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.tab_liste.anagrafica_risorse2.CognomeNome); // .Select(s => s.anagrafica_risorse.CognomeNome).FirstOrDefault());
                        v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.tab_liste.anagrafica_risorse2.anagrafica_ruolo_mansione.descr_ruolo_mansione);
                        v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.tab_liste.anagrafica_risorse2.anagrafica_ente.descrizione_ente);
                    }
                    else
                    {
                        v_testo = v_testo.Replace(RESP_EMISSIONE, "Luigi Monti");
                        v_testo = v_testo.Replace(RUOLO_MANSIONE, "Legale Rappresentante p.t.");
                        v_testo = v_testo.Replace(ENTE_RISORSA, "Publiservizi srl");
                    }
                }
            }

            return v_testo;
        }
        public static string GetTestoResponsabiliAttiSuccessivi(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoResposabiliAttiSuccessivi");

            v_testo = v_testo.Replace("#TIPO_AVVISO#", p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);


            return v_testo;
        }

        public static string GetTestoResponsabiliOrdinarioPaolisi(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoResposabiliOrdinarioPaolisi");

            v_testo = v_testo.Replace("#TIPO_AVVISO#", p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);


            return v_testo;
        }
        public static string GetTestoResponsabiliAccoglimentoMediazione(tab_sped_not p_sped_not)
        {
            //string v_testo = GetSezione(p_sped_not, "TestoResposabiliAttiSuccessivi");
            string v_testo = "";
            string v_testo_firma = "La firma autografa del Responsabile del provvedimento è sostituita  dall’indicazione a stampa del nominativo  " +
                    "dello stesso  ai sensi  dell’art.3 comma 2 D.lgs 39/93  e dell’art.1 comma 87, Legge 549 del 28/12/1995.";
            string v_responsabile = "";

            if (p_sped_not.tab_doc_output.id_tipo_doc_entrate != tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ANNULLAMENTO_RETTIFICA_AUTOTUTELA_SU_RICORSO)
            {
                string v_titolo = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input
                    .anagrafica_risorse2.join_risorse_strutture.Where(w => w.anagrafica_strutture_aziendali.codice_struttura_aziendale == anagrafica_strutture_aziendali.COD_STRUTTURA_SERVIZI_LEGALI)
                    .FirstOrDefault().anagrafica_strutture_aziendali.anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione;
                v_responsabile = v_titolo + " " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input
                    .anagrafica_risorse2.join_risorse_strutture.Where(w => w.anagrafica_strutture_aziendali.codice_struttura_aziendale == anagrafica_strutture_aziendali.COD_STRUTTURA_SERVIZI_LEGALI)
                    .FirstOrDefault().anagrafica_strutture_aziendali.anagrafica_risorse1.CognomeNome;
                //if (p_sped_not.id_tipo_spedizione_notifica == anagrafica_tipo_spedizione_notifica.ID_SPEDIZIONE_PEC)
                //{
                //    v_testo_firma = "Firmato con firma digitale da";
                //}
            }
            else
            {
                string v_titolo = string.Empty;

                if (p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input
                    .FirstOrDefault().tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault() != null)
                {
                    v_titolo = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().anagrafica_risorse11.anagrafica_ruolo_mansione.descr_ruolo_mansione;
                    v_responsabile = v_titolo + " " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().anagrafica_risorse11.CognomeNome;
                }
                else if (p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault() != null)
                {
                    v_titolo = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione;
                    v_responsabile = v_titolo + " " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().anagrafica_risorse1.CognomeNome;
                }
                else
                {
                    v_titolo = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.anagrafica_tipo_avv_pag.anagrafica_tipo_servizi.tab_modalita_rate_avvpag.FirstOrDefault().anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione;
                    v_responsabile = v_titolo + " " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().tab_avv_pag.anagrafica_tipo_avv_pag.anagrafica_tipo_servizi.tab_modalita_rate_avvpag.FirstOrDefault().anagrafica_risorse1.CognomeNome;
                }
            }

            v_testo = "<html><body><font face = 'Times New Roman' size = '3'><p align=left>" + v_responsabile
                + "<br></p><p align=justify>" + v_testo_firma + " </p></font></body></html>";
            return v_testo;
        }
        public static string GetTestoResponsabiliCitazioni(tab_sped_not p_sped_not)
        {
            //string v_testo = GetSezione(p_sped_not, "TestoResposabiliAttiSuccessivi");
            string v_testo = "";
            string v_testo_firma = "La firma autografa del Responsabile del provvedimento è sostituita  dall’indicazione a stampa del nominativo  " +
                    "dello stesso  ai sensi  dell’art.3 comma 2 D.lgs 39/93  e dell’art.1 comma 87, Legge 549 del 28/12/1995.";
            string v_responsabile = "";

            string v_titolo = p_sped_not.tab_avv_pag.tab_citazioni.FirstOrDefault().anagrafica_risorse.anagrafica_ruolo_mansione.descr_ruolo_mansione;
            v_responsabile = v_titolo + " " + p_sped_not.tab_avv_pag.tab_citazioni.FirstOrDefault().anagrafica_risorse.CognomeNome;
            //string v_titolo = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input
            //    .anagrafica_risorse2.join_risorse_strutture.Where(w => w.anagrafica_strutture_aziendali.codice_struttura_aziendale == anagrafica_strutture_aziendali.COD_STRUTTURA_SERVIZI_LEGALI)
            //    .FirstOrDefault().anagrafica_strutture_aziendali.anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione;
            //v_responsabile = v_titolo + " " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input
            //    .anagrafica_risorse2.join_risorse_strutture.Where(w => w.anagrafica_strutture_aziendali.codice_struttura_aziendale == anagrafica_strutture_aziendali.COD_STRUTTURA_SERVIZI_LEGALI)
            //    .FirstOrDefault().anagrafica_strutture_aziendali.anagrafica_risorse1.CognomeNome;

            v_testo = "<html><body><font face = 'Times New Roman' size = '3'><p align=left>" + v_responsabile
                + "<br></p><p align=justify>" + v_testo_firma + " </p></font></body></html>";
            return v_testo;
        }
        public static string GetTestoResponsabiliSmallRateizzazione(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneResponsabiliSmall);

            if (v_testo != null && p_avviso.id_ente != anagrafica_ente.ID_ENTE_COMUNE_DI_FIRENZE)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(RESP_EMISSIONE, p_avviso.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Select(s => s.anagrafica_risorse1.CognomeNome).FirstOrDefault());
                v_testo = v_testo.Replace(RUOLO_MANSIONE, p_avviso.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Select(s => s.anagrafica_risorse1.anagrafica_ruolo_mansione.descr_ruolo_mansione).FirstOrDefault());
                v_testo = v_testo.Replace(ENTE_RISORSA, p_avviso.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Select(s => s.anagrafica_risorse1.anagrafica_ente.descrizione_ente).FirstOrDefault());
            }

            return v_testo;

        }
        public static string GetTestoInfoSportello(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneInfoSportello);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                // v_testo = v_testo.Replace(NUMERO_CC, "VALORE CC");
            }

            return v_testo;
        }

        //public static string GetTestoComePagare(tab_avv_pag p_avviso)
        //{
        //    string v_testo = GetSezione(p_avviso, SezioneTestoComePagare);

        //    if (v_testo != null)
        //    {
        //        //Se ha trovato la sezione, effettua le sostituzioni
        //        v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
        //        v_testo = v_testo.Replace(NUMERO_CC, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.num_cc_new).FirstOrDefault());
        //        v_testo = v_testo.Replace(IBAN, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.IBAN).FirstOrDefault());
        //        v_testo = v_testo.Replace(COD_LINE, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.barcode).FirstOrDefault());
        //        v_testo = v_testo.Replace(INTESTAZIONE_CC, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_cc_riscossione.intestazione_cc).FirstOrDefault());
        //        v_testo = v_testo.Replace(INDIRIZZO, p_avviso.anagrafica_ente.indirizzo);
        //        v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_avv_pag.identificativo_avv_pag).FirstOrDefault());
        //        v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_avviso.anagrafica_ente.descrizione_ente);

        //    }
        //    return v_testo;
        //}
        public static string GetTestoComePagare(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, SezioneTestoComePagare);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace("#DES_COMUNE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace(NUMERO_CC, p_sped_not.tab_cc_riscossione.num_cc_new.Trim());
                v_testo = v_testo.Replace(IBAN, p_sped_not.tab_cc_riscossione.IBAN.Trim());
                v_testo = v_testo.Replace(COD_LINE, p_sped_not.barcode.Trim());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_sped_not.tab_cc_riscossione.intestazione_cc.Trim());
                v_testo = v_testo.Replace(INDIRIZZO, p_sped_not.tab_avv_pag.anagrafica_ente.indirizzo?.Trim());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
            }
            return v_testo;
        }
        public static string GetTestoComePagareOmessaDenuncia(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "TestoPagamentoOmessaDen");

            if (v_testo != null)

            {
                string v_identificativo_1 = string.Empty;
                string v_identificativo_2 = string.Empty;
                if (p_sped_not.tab_avv_pag.tab_rata_avv_pag
                        .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.importo_ridotto)
                        .FirstOrDefault().codice_pagamento_pagopa != null)
                {
                    v_identificativo_1 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.importo_ridotto)
                                    .FirstOrDefault().codice_pagamento_pagopa;
                    if ((p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI
                         || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI
                         || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI_ESECUTIVO)
                         && (p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == null
                         || p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == "0"))
                    {
                        v_identificativo_2 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                        .Where(w => w.imp_tot_rata == Math.Round(p_sped_not.tab_avv_pag.imp_tot_avvpag.Value
                                        + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value))
                                        .FirstOrDefault().codice_pagamento_pagopa;
                    }

                }
                else
                {
                    v_identificativo_1 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.importo_ridotto).Count() > 0 ?
                 p_sped_not.tab_avv_pag.tab_rata_avv_pag.Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.importo_ridotto)
                 .FirstOrDefault().Iuv_identificativo_pagamento : "";
                    if ((p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI
                        || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI
                        || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI_ESECUTIVO)
                        && (p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == null
                        || p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == "0"))
                    {
                        v_identificativo_2 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == Math.Round(p_sped_not.tab_avv_pag.imp_tot_avvpag.Value +
                                    p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value))
                                    .FirstOrDefault().Iuv_identificativo_pagamento;
                    }

                }
                if (p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == null
                    || p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == "0")
                {
                    v_testo = v_testo.Replace("#TESTO_F24#", "•    " + "a mezzo F24 utilizzando l’apposito modello avendo accortezza di indicare nel campo identificativo operazione del modello F24 l’identificativo sopra indicato corrispondente all'importo pagato.  In tal modo potrà essere effettuato il corretto accoppiamento del pagamento con il presente avviso.");
                }
                else
                {
                    v_testo = v_testo.Replace("#TESTO_F24#", "");
                }
                Utilities.WordyFormatProviderITA v_class = new WordyFormatProviderITA();
                string v_impo_in_lettere = v_class.Format("W", p_sped_not.tab_avv_pag.importo_ridotto.Value, CultureInfo.InvariantCulture.NumberFormat);
                string v_impo_magg_lettere = v_class
                    .Format("W", p_sped_not.tab_avv_pag.importo_tot_da_pagare.Value
                    + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value, CultureInfo.InvariantCulture.NumberFormat);
                //Se ha trovato la sezione, effettua le sostituzioni
                //Decimal v_imp_oneri_tre_perc = p_sped_not.tab_avv_pag.importo_tot_da_pagare- oner
                v_testo = v_testo.Replace("#DES_COMUNE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace(NUMERO_CC, p_sped_not.tab_cc_riscossione.num_cc_new.Trim());
                v_testo = v_testo.Replace(IBAN, p_sped_not.tab_cc_riscossione.IBAN.Trim());
                v_testo = v_testo.Replace(COD_LINE, p_sped_not.barcode.Trim());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_sped_not.tab_cc_riscossione.intestazione_cc.Trim());
                v_testo = v_testo.Replace(INDIRIZZO, p_sped_not.tab_avv_pag.anagrafica_ente.indirizzo?.Trim());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace("#IMP_TOT_RIDOTTO_AVVPAG#", p_sped_not.tab_avv_pag.importo_ridotto.Value.ToString("N2"));
                v_testo = v_testo.Replace("#IMP_TOT_AVVPAG#", p_sped_not.tab_avv_pag.imp_tot_avvpag.Value.ToString("N2"));
                v_testo = v_testo.Replace("#IMPORTO_RIDOTTO_IN_LETTERE#", v_impo_in_lettere);
                v_testo = v_testo.Replace("#IMPORTO_MAGGIORATO_1#",
                    (p_sped_not.tab_avv_pag.importo_tot_da_pagare + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90).Value.ToString("N2"));
                v_testo = v_testo.Replace("#IMPORTO_MAG1_IN_LETTERE#", v_impo_magg_lettere);
                v_testo = v_testo.Replace("#IMPORTO_MAGGIORAZIONE#", p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value.ToString("N2"));
                v_testo = v_testo.Replace("#INTERESSE_MORA#", p_sped_not.tab_avv_pag.imp_tot_interesse_mora_giornaliero.ToString());
                v_testo = v_testo.Replace("#IMPORTO_ONERI_120#", p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_121.Value.ToString("N2"));
                v_testo = v_testo.Replace("#IDENTIFICATIVO_BOL_1#", v_identificativo_1);
                v_testo = v_testo.Replace("#IDENTIFICATIVO_BOL_2#", v_identificativo_2);
            }
            return v_testo;
        }

        public static string GetTestoComePagareAccertamento(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "TestoPagamentoAccEsec");

            if (v_testo != null)

            {
                string v_identificativo_1 = string.Empty;
                string v_identificativo_2 = string.Empty;
                if (p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.imp_tot_avvpag)
                                    .FirstOrDefault() != null &&
                                    p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.imp_tot_avvpag)
                                    .FirstOrDefault().codice_pagamento_pagopa != null &&
                                    p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.imp_tot_avvpag)
                                    .FirstOrDefault().codice_pagamento_pagopa != "")
                {
                    v_identificativo_1 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.imp_tot_avvpag).FirstOrDefault().codice_pagamento_pagopa;
                    if ((p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI
                          || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI
                          || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI_ESECUTIVO
                          || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACCERTAMENTO_ESECUTIVO_IMU)
                          && (p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == null
                          || p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == "0"))
                    {
                        v_identificativo_2 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == Math.Round(p_sped_not.tab_avv_pag.imp_tot_avvpag.Value
                                    + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value, MidpointRounding.AwayFromZero)).FirstOrDefault().codice_pagamento_pagopa;
                    }
                }
                else
                {
                    v_identificativo_1 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                .Where(w => w.imp_tot_rata == p_sped_not.tab_avv_pag.imp_tot_avvpag).FirstOrDefault().Iuv_identificativo_pagamento;
                    if ((p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI
                        || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_INFEDELE_ESEC_TARI
                        || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI_ESECUTIVO
                        || p_sped_not.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACCERTAMENTO_ESECUTIVO_IMU)
                        && (p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == null
                        || p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == "0"))
                    {
                        v_identificativo_2 = p_sped_not.tab_avv_pag.tab_rata_avv_pag
                                    .Where(w => w.imp_tot_rata == Math.Round(p_sped_not.tab_avv_pag.imp_tot_avvpag.Value + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value)).FirstOrDefault().Iuv_identificativo_pagamento;
                    }
                }
                if (p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == null
                  || p_sped_not.tab_avv_pag.tab_liste.tab_validazione_approvazione_liste.FirstOrDefault().flag_stampa_F24 == "0")
                {
                    v_testo = v_testo.Replace("#TESTO_F24#", "•    " + "a mezzo F24 utilizzando l’apposito modello avendo accortezza di indicare nel campo identificativo operazione del modello F24 l’identificativo sopra indicato corrispondente all'importo pagato.  In tal modo potrà essere effettuato il corretto accoppiamento del pagamento con il presente avviso.");
                }
                else
                {
                    v_testo = v_testo.Replace("#TESTO_F24#", "");
                }
                Utilities.WordyFormatProviderITA v_class = new WordyFormatProviderITA();
                string v_impo_in_lettere = v_class.Format("W", p_sped_not.tab_avv_pag.imp_tot_avvpag.Value, CultureInfo.InvariantCulture.NumberFormat);
                string v_impo_magg_lettere = v_class
                    .Format("W", (p_sped_not.tab_avv_pag.imp_tot_avvpag.Value
                    + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value).Arrotonda(), CultureInfo.InvariantCulture.NumberFormat);
                //Se ha trovato la sezione, effettua le sostituzioni
                //Decimal v_imp_oneri_tre_perc = p_sped_not.tab_avv_pag.importo_tot_da_pagare- oner
                v_testo = v_testo.Replace("#DES_COMUNE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace(NUMERO_CC, p_sped_not.tab_cc_riscossione.num_cc_new.Trim());
                v_testo = v_testo.Replace(IBAN, p_sped_not.tab_cc_riscossione.IBAN.Trim());
                v_testo = v_testo.Replace(COD_LINE, p_sped_not.barcode.Trim());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_sped_not.tab_cc_riscossione.intestazione_cc.Trim());
                v_testo = v_testo.Replace(INDIRIZZO, p_sped_not.tab_avv_pag.anagrafica_ente.indirizzo?.Trim());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace("#IMP_TOT_AVVPAG#", p_sped_not.tab_avv_pag.imp_tot_avvpag.Value.ToString("N2"));
                v_testo = v_testo.Replace("#IMP_TOT_RIDOTTO_AVVPAG#", p_sped_not.tab_avv_pag.imp_tot_avvpag.Value.ToString("N2"));
                v_testo = v_testo.Replace("#IMPORTO_RIDOTTO_IN_LETTERE#)", v_impo_in_lettere);
                v_testo = v_testo.Replace("#IMPORTO_IN_LETTERE#", v_impo_in_lettere);
                v_testo = v_testo.Replace("#IMPORTO_MAGGIORATO_1#",
                    (p_sped_not.tab_avv_pag.imp_tot_avvpag + p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90).Value.Arrotonda().ToString("N2"));
                v_testo = v_testo.Replace("#IMPORTO_MAG1_IN_LETTERE#", v_impo_magg_lettere);
                v_testo = v_testo.Replace("#IMPORTO_MAGGIORAZIONE#", p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_61_90.Value.ToString("N2"));
                v_testo = v_testo.Replace("#INTERESSE_MORA#", p_sped_not.tab_avv_pag.imp_tot_interesse_mora_giornaliero.ToString());
                v_testo = v_testo.Replace("#IMPORTO_ONERI_120#", p_sped_not.tab_avv_pag.imp_maggiorazione_onere_riscossione_121.Value.Arrotonda().ToString("N2"));
                v_testo = v_testo.Replace("#IDENTIFICATIVO_BOL_1#", v_identificativo_1);
                v_testo = v_testo.Replace("#IDENTIFICATIVO_BOL_2#", v_identificativo_2);
            }
            return v_testo;
        }

        public static string GetTestoComePagareNoRate(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "ModalitaPagamentoIpoteca");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace("#DES_COMUNE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace(NUMERO_CC, p_sped_not.tab_cc_riscossione.num_cc_new.Trim());
                v_testo = v_testo.Replace(IBAN, p_sped_not.tab_cc_riscossione.IBAN.Trim());
                v_testo = v_testo.Replace(COD_LINE, p_sped_not.barcode.Trim());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_sped_not.tab_cc_riscossione.intestazione_cc.Trim());
                //v_testo = v_testo.Replace(INDIRIZZO, p_sped_not.tab_avv_pag.anagrafica_ente.indirizzo.Trim());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
            }
            return v_testo;
        }
        public static string GetTestoComePagareIngFi(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "ModalitàPagamentoIngFI");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace("#DES_COMUNE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace(NUMERO_CC, p_sped_not.tab_cc_riscossione.num_cc_new.Trim());
                v_testo = v_testo.Replace(IBAN, p_sped_not.tab_cc_riscossione.IBAN.Trim());
                v_testo = v_testo.Replace(COD_LINE, p_sped_not.barcode.Trim());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_sped_not.tab_cc_riscossione.intestazione_cc.Trim());
                //v_testo = v_testo.Replace(INDIRIZZO, p_sped_not.tab_avv_pag.anagrafica_ente.indirizzo.Trim());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
            }
            return v_testo;
        }
        public static string GetTestoComePagarePostIngFi(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "ModalitàPagamentoPostFI");

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace("#DES_COMUNE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());
                v_testo = v_testo.Replace(NUMERO_CC, p_sped_not.tab_cc_riscossione.num_cc_new.Trim());
                v_testo = v_testo.Replace(IBAN, p_sped_not.tab_cc_riscossione.IBAN.Trim());
                v_testo = v_testo.Replace(COD_LINE, p_sped_not.barcode.Trim());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_sped_not.tab_cc_riscossione.intestazione_cc.Trim());
                //v_testo = v_testo.Replace(INDIRIZZO, p_sped_not.tab_avv_pag.anagrafica_ente.indirizzo.Trim());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente.Trim());

            }
            return v_testo;
        }

        public static string GetTestoComePagareSollecitoFI(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoComePagareSollecitoFI);

            if (v_testo != null)
            {
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_avv_pag.identificativo_avv_pag).FirstOrDefault());
                v_testo = v_testo.Replace(NUMERO_CC, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.num_cc_new).FirstOrDefault());
                v_testo = v_testo.Replace(IBAN, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.IBAN).FirstOrDefault());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_cc_riscossione.intestazione_cc).FirstOrDefault());

            }
            return v_testo;
        }

        public static string GetPremessaAvvisoRateizzazione(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoPremessaAvvRateizzazione);
            v_testo = v_testo.Replace("#IDENTIFICATIVO_ISTANZA#", p_avviso.join_tab_avv_pag_tab_doc_input1.Select(s => s.tab_doc_input.identificativo_doc_input.Trim()).FirstOrDefault());

            return v_testo;
        }
        //public static string GetPremessaAvvisoRottamazione(tab_avv_pag p_avviso)
        //{
        //    string v_testo = GetSezione(p_avviso, SezioneTestoPremessaAvvRottamazioneLR7_2019);
        //    //v_testo = v_testo.Replace("#IDENTIFICATIVO_ISTANZA#", p_avviso.join_tab_avv_pag_tab_doc_input1.Select(s => s.tab_doc_input.identificativo_doc_input.Trim()).FirstOrDefault());

        //    return v_testo;
        //}

        //public static string GetPremessaComunicazioneRottamazione(tab_avv_pag p_avviso)
        //{
        //    string v_testo = GetSezione(p_avviso, SezioneTestoPremessaAvvRottamazioneLR7_2019);
        //    //v_testo = v_testo.Replace("#IDENTIFICATIVO_ISTANZA#", p_avviso.join_tab_avv_pag_tab_doc_input1.Select(s => s.tab_doc_input.identificativo_doc_input.Trim()).FirstOrDefault());

        //    return v_testo;
        //}
        public static string GetTestoComunicazioneRottamazioneRL7_2019_P1(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoComunicazRotLR7_1");
            //TODO Sandro: parametrizzare indirizzo
            v_testo = v_testo.Replace("#ENTE_CREDITORE#", "PUBLISERVIZI srl, Via Pantano n.15 , 20122 – Milano (MI)");
            v_testo = v_testo.Replace("#INDIRIZZO_PEC#", "publiservizi.lombardia.istanze@pec.it");
            return v_testo;
        }
        public static string GetTestoComunicazioneRottamazioneRL7_2019_P2(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoComunicazRotLR7_2");


            return v_testo;
        }

        public static string GetPremessaAvvisoRottamazioneLR7_2019(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoPremessaAvvRottamazioneLR7_2019);
            v_testo = v_testo.Replace("#IDENTIFICATIVO_ISTANZA#", "");// p_avviso.join_tab_avv_pag_tab_doc_input.Select(s => s.tab_doc_input.identificativo_doc_input.Trim()).FirstOrDefault());

            return v_testo;
        }
        public static string GetPremessaComunicazioneRottamazioneLR7_2019(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoPremessaComRottamazioneLR7_2019);
            v_testo = v_testo.Replace("#IDENTIFICATIVO_ISTANZA#", "");// p_avviso.join_tab_avv_pag_tab_doc_input.Select(s => s.tab_doc_input.identificativo_doc_input.Trim()).FirstOrDefault());

            return v_testo;
        }
        public static string GetSoggettoDebitore(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneSoggettoDebitore);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                // v_testo = v_testo.Replace(NUMERO_CC, "VALORE CC");
            }
            return v_testo;
        }

        public static string GetTestoPreavvisofermoAmm(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoPreavvisofermoAmministrativo);

            if (v_testo != null)
            {
                //TODO SANDRO: verificare dove recuperare le spese di iscrizione del fermo
                //p_avviso.importo_spese_coattive_sospese_su_preavvisi==null ? "€ ?" : p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString()
                string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", p_avviso.anagrafica_ente.descrizione_ente);
                v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
            }
            return v_testo;
        }


        public static string GetTestoPignoramentoVSTerzi(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoPignoramentoVSTerzi);

            if (v_testo != null)
            {

            }

            return v_testo;
        }

        public static string GetTestoTerminiPagamentoPignoramentoVSTerzi(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoTerminiPagamentoPignoramentoVSTerzi);

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                v_testo = v_testo.Replace(NUMERO_CC, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.num_cc_new).FirstOrDefault());
                v_testo = v_testo.Replace(IBAN, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.IBAN).FirstOrDefault());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_cc_riscossione.intestazione_cc).FirstOrDefault());
                v_testo = v_testo.Replace(CAUSALE_BONIFICO, "Pagamento Atto Numero: " + p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_avv_pag.identificativo_avv_pag).FirstOrDefault());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_avv_pag.identificativo_avv_pag).FirstOrDefault());

            }

            return v_testo;
        }
        public static string GetTestoTerminiPagamentoPignoramentoVSTerziFI(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "PagamentoPignVSTerziFI");

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                v_testo = v_testo.Replace(NUMERO_CC, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.num_cc_new).FirstOrDefault());
                v_testo = v_testo.Replace(IBAN, p_avviso.tab_sped_not.Where(w => w.id_cc_riscossione == w.tab_cc_riscossione.id_tab_cc_riscossione).Select(s => s.tab_cc_riscossione.IBAN).FirstOrDefault());
                v_testo = v_testo.Replace(INTESTAZIONE_CC, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_cc_riscossione.intestazione_cc).FirstOrDefault());
                v_testo = v_testo.Replace(CAUSALE_BONIFICO, "Pagamento Atto Numero: " + p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_avv_pag.identificativo_avv_pag).FirstOrDefault());
                v_testo = v_testo.Replace(IDENTIFICATIVO_ATTO, p_avviso.tab_sped_not.Where(w => w.id_avv_pag == p_avviso.id_tab_avv_pag && w.tab_cc_riscossione.id_tab_cc_riscossione == w.id_cc_riscossione).Select(s => s.tab_avv_pag.identificativo_avv_pag).FirstOrDefault());

            }

            return v_testo;
        }
        public static string GetTestoAvvertenzePignoramentiVSTerzo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoAvvertenzePignoramentiVSTerzo);

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                //v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
            }

            return v_testo;
        }

        public static string GetTestoAvvertenzeOrdinario(tab_avv_pag p_avviso)
        {
            string v_testo = "";
            if (p_avviso.tab_unita_contribuzione.Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_TIPO_TARI).Any())
            {
                string v_anno_rif = p_avviso.tab_unita_contribuzione
                                    .Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_TIPO_TARI).FirstOrDefault().anno_rif?.Trim();
                int v_anno = Convert.ToInt32(v_anno_rif);
                if (v_anno != 2020)
                {
                    v_testo = GetSezione(p_avviso, "TestoAvvertenzeOrdinario");
                }
                else
                {
                    v_testo = GetSezione(p_avviso, "TestoAvvertenzeTARI2021");
                }
            }

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                //v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
            }

            return v_testo;
        }

        public static string GetTestoAvvertenzeAccertamento(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoAvvertenzeAccertamento");

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                //v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
            }

            return v_testo;
        }
        public static string GetTestoAvvertenzeOmessaDenuncia(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoAvvertenzeOmessaDen");

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                //v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
            }

            return v_testo;
        }
        public static string GetTestoAvvertenzePreavvisoFermo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoAvvertenzePreavvisoFermo);

            if (v_testo != null)
            {
                string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));

            }

            return v_testo;
        }

        public static string GetTestoAvvertenzeFermoAmm(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "AvvertenzeAvvisoDiFermo");

            if (v_testo != null)
            {
                //string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                //v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
            }

            return v_testo;
        }
        public static string GetTestoPremessaTARI(tab_avv_pag p_avviso)
        {
            string v_testo = "";
            if (p_avviso.tab_unita_contribuzione.Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_TIPO_TARI).Any())
            {
                string v_anno_rif = p_avviso.tab_unita_contribuzione
                                    .Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_TIPO_TARI).FirstOrDefault().anno_rif?.Trim();
                int v_anno = Convert.ToInt32(v_anno_rif);
                if (v_anno != 2020)
                {
                    v_testo = GetSezione(p_avviso, "TestoPremessaOrdinario");
                }
                else
                {
                    v_testo = GetSezione(p_avviso, "TestoPremessaTARI2021");
                }
            }

            if (v_testo != null)
            {
                ////TODO SANDRO: verificare dove recuperare le spese di iscrizione del fermo
                //TAB_SUPERVISIONE_FINALE_V2 v_tabSupVisFinale = p_avviso.TAB_SUPERVISIONE_FINALE_V2.Single();

                //v_testo = v_testo.Replace("#IDENTIFICATIVO_AVV#", v_tabSupVisFinale.tab_avv_pag2.identificativo_avv_pag.Trim());
                //v_testo = v_testo.Replace("#DATA_EMISSIONE#", v_tabSupVisFinale.tab_avv_pag2.dt_emissione_String);
                ////TODO Sandro: data ricezione vuota
                //v_testo = v_testo.Replace("#DATA_RICEZIONE#", v_tabSupVisFinale.tab_avv_pag2.data_ricezione_String);
                //string stTmp = GetSezione(p_avviso, "TestataEnteCreditore");
                //stTmp = stTmp.Replace("#COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                //v_testo = v_testo.Replace("#DESC_ENTE#", stTmp);
                //v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
            }
            return v_testo;
        }

        public static string GetTestoAvvertenzeAffisioni(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoAvvertenzeAffissioni");
            return v_testo;
        }
        public static string GetTestoPremessaAffisioni(tab_avv_pag p_avviso)
        {
            string v_testo = "";
            if (p_avviso.tab_unita_contribuzione.Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.AFFISSIONE_MANIFESTI).Any())
            {
                string v_anno_rif = p_avviso.tab_unita_contribuzione
                                    .Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.AFFISSIONE_MANIFESTI).FirstOrDefault().anno_rif?.Trim();
                int v_anno = Convert.ToInt32(v_anno_rif);
                if (v_anno != 2020)
                {
                    v_testo = GetSezione(p_avviso, "TestoPremessaAffissioni");
                }
                else
                {
                    v_testo = GetSezione(p_avviso, "TestoPremessaAffissioni");
                }
            }

            if (v_testo != null)
            {
                ////TODO SANDRO: verificare dove recuperare le spese di iscrizione del fermo
                //TAB_SUPERVISIONE_FINALE_V2 v_tabSupVisFinale = p_avviso.TAB_SUPERVISIONE_FINALE_V2.Single();

                //v_testo = v_testo.Replace("#IDENTIFICATIVO_AVV#", v_tabSupVisFinale.tab_avv_pag2.identificativo_avv_pag.Trim());
                //v_testo = v_testo.Replace("#DATA_EMISSIONE#", v_tabSupVisFinale.tab_avv_pag2.dt_emissione_String);
                ////TODO Sandro: data ricezione vuota
                //v_testo = v_testo.Replace("#DATA_RICEZIONE#", v_tabSupVisFinale.tab_avv_pag2.data_ricezione_String);
                //string stTmp = GetSezione(p_avviso, "TestataEnteCreditore");
                //stTmp = stTmp.Replace("#COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                //v_testo = v_testo.Replace("#DESC_ENTE#", stTmp);
                //v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
            }
            return v_testo;
        }
        public static string GetTestoPremessaAccertamentoTARI(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoPremessaAccertamento");
            return v_testo;
        }
        public static string GetTestoPremessaOmessaDenuncia(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoPremessaOmessaDen");

            return v_testo;
        }
        public static string GetTestoPremessaOmessoVersIMU(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoPremessaOmessoVerIMU");

            return v_testo;
        }
        public static string GetTestoAvvertenzeOmessoVersIMU(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoAvvertenzeOmVerIMU");

            return v_testo;
        }
        public static string GetTestoPagamentoOmessoVersIMU(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoPagamentoIMU");

            return v_testo;
        }
        public static string GetTestoPremessaInfedeleDenuncia(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoPremessaInfedeleDen");

            return v_testo;
        }
        public static string GetTestoPremessaFermoAmm(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoFermoAmministrativo);
            if (v_testo != null)
            {
                //TODO SANDRO: verificare dove recuperare le spese di iscrizione del fermo
                TAB_SUPERVISIONE_FINALE_V2 v_tabSupVisFinale = p_avviso.TAB_SUPERVISIONE_FINALE_V2.Single();

                v_testo = v_testo.Replace("#IDENTIFICATIVO_AVV#", v_tabSupVisFinale.tab_avv_pag2.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#DATA_EMISSIONE#", v_tabSupVisFinale.tab_avv_pag2.dt_emissione_String);
                //TODO Sandro: data ricezione vuota
                v_testo = v_testo.Replace("#DATA_RICEZIONE#", v_tabSupVisFinale.tab_avv_pag2.data_ricezione_String);
                string stTmp = GetSezione(p_avviso, "TestataEnteCreditore");
                stTmp = stTmp.Replace("#COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                v_testo = v_testo.Replace("#DESC_ENTE#", stTmp);
                v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
            }
            return v_testo;
        }

        public static string GetModaitaPagamento(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneModalitaPagamento);
            return v_testo;
        }
        public static string GetTestoLeggiPreavvisoFermo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneTestoRetroBollettino);

            if (v_testo != null)
            {
                //TODO SANDRO: verificare dove recuperare le spese di iscrizione del fermo
                //p_avviso.importo_spese_coattive_sospese_su_preavvisi==null ? "€ ?" : p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString()
                //v_testo = v_testo.Replace(EURO_SPESE_FERMO, p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString());
            }

            return v_testo;
        }


        public static string GetTestoSollecitoPagamento(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoSollecitoPagamentoPI);

            return v_testo;
        }

        public static string GetTestoIstanzaInAutotutela(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoIstanzaInAutotutela);
            string v_testo_cds = "Nota bene: per informazioni e chiarimenti attinenti il contenuto, " +
                "la notifica e quanto altro riferito ai verbali di infrazione al CDS che costituiscono " +
                "il presupposto della presente ingiunzione, dovrà rivolgersi direttamente al Comando di Polizia Municipale " +
                "del Comune nei giorni e negli orari di apertura al pubblico";
            if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.ING_FISC_SANZIONE_CDS)
            {
                v_testo = v_testo.Replace("#TESTO_CDS#", v_testo_cds);
            }
            else
            {
                v_testo = v_testo.Replace("#TESTO_CDS#", "");
            }

            return v_testo;
        }

        public static string GetTestoIstanzaAvvisoOrdinario(tab_avv_pag p_avviso)
        {
            string v_anno_rif = p_avviso.tab_unita_contribuzione
                                   .Where(w => w.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_TIPO_TARI).FirstOrDefault().anno_rif?.Trim();
            string v_testo = string.Empty;
            int v_anno = Convert.ToInt32(v_anno_rif);
            if (v_anno != 2020)
            {
                v_testo = GetSezione(p_avviso, "TestoIstanzeOrdinario");
            }
            else
            {
                v_testo = GetSezione(p_avviso, "TestoIstanzeOrdinarioCovid");
            }

            return v_testo;
        }
        public static string GetTestoRicorsiAvvisoOrdinario(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoRicorsiOrdinario");

            return v_testo;
        }
        public static string GetTestoIstanzaDiRevisione(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoIstanzaDiRevisione);

            return v_testo;
        }
        public static string GetTestoRicorsoTributario_o_Extra(tab_avv_pag p_avviso)
        {
            string v_testo = string.Empty;

            switch (p_avviso.id_tipo_avvpag)
            {
                ////INTIMAZIONE: RICORSO TRIBUTARIO ED EXTRA TRIBUTARIO
                //case anagrafica_tipo_avv_pag.SOLLECITO_POST_INGIUNZIONE_G_CON_ONERI:
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);
                //    break;
                //case anagrafica_tipo_avv_pag.ING_FISC_SANZIONE_CDS:
                //case anagrafica_tipo_avv_pag.ING_FISC_SANZIONE_689:
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);
                //    break;
                //case 209:
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);
                //    break;
                ////PREAVVISO DI FERMO: RICORSO TRIBUTARIO ED EXTRA TRIBUTARIO
                //case anagrafica_tipo_avv_pag.PRE_FERMO_AMM:
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);

                //    break;
                //case anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO:
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);

                //    break;
                ////INGIUNZIONE: RICORSO TRIBUTARIO ED EXTRA TRIBUTARIO
                //case 207: //EXTRA TRIBUTARIO
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoExtraTributario);
                //    break;

                //case 208://SOLLECITO Generico
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);
                //    break;
                //case anagrafica_tipo_avv_pag.SOLLECITO_POST_INGIUNZIONE_T://SOLLECITO tributario
                //case anagrafica_tipo_avv_pag.SOLLECITO_POST_INGIUNZIONE_G:
                //case anagrafica_tipo_avv_pag.PRIMO_SOLLECITO_POST_INGIUNZIONE_E:
                //case anagrafica_tipo_avv_pag.PRIMO_SOLLECITO_POST_INGIUNZIONE_T:
                //case anagrafica_tipo_avv_pag.PRIMO_SOLLECITO_POST_INGIUNZIONE_G:
                //    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);
                //    break;
                case anagrafica_tipo_avv_pag.PIGN_IMMOB:
                case anagrafica_tipo_avv_pag.PIGN_MOB:
                case anagrafica_tipo_avv_pag.PIGN_TERZI:
                case anagrafica_tipo_avv_pag.PIGN_TERZI_PEN:
                    v_testo = GetSezione(p_avviso, "RicorsoPignVsTerzi");
                    break;
                case anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI:
                case anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI_NOT:
                    v_testo = GetSezione(p_avviso, "TestoRicorsoTributario");
                    break;
                default:
                    v_testo = GetSezione(p_avviso, SezioneTestoRicorsoTributarioBivalente);
                    break;
            }
            return v_testo;
        }
        //Utilizzo come concordato con il Dottore questa sezione anche per i ricorsi GDP e TRIB
        public static string GetTestoSezioneSoggettoResistenteCTP(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            v_testo = GetSezione(p_istanza, "SezioneSoggettoResistenteCTP");

            v_testo = v_testo.Replace("#TRIBUNALE#", p_istanza.tab_doc_input.tab_autorita_giudiziaria.descrizione_autorita);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_istanza.tab_doc_input.anagrafica_ente.descrizione_ente);
            //v_testo = v_testo.Replace("#RESPONSABILE_EMISSIONE#", p_istanza.anagrafica_risorse.CognomeNome);// .tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse11.CognomeNome).FirstOrDefault());
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            string ContribuenteComposedHTML = string.Empty;
            ContribuenteComposedHTML = myTI.ToTitleCase(p_istanza.tab_doc_input.tab_contribuente.nominativoDisplay.ToLower());
            v_testo = v_testo.Replace("#NATO_A#", p_istanza.tab_doc_input.tab_contribuente.comune_nas);
            v_testo = v_testo.Replace("#DATA_NASCITA#", p_istanza.tab_doc_input.tab_contribuente.data_nas_String);
            v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", p_istanza.tab_doc_input.tab_contribuente.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                + p_istanza.tab_doc_input.tab_contribuente.indirizzoTotaleDisplay + ", " + p_istanza.tab_doc_input.tab_contribuente.codFiscalePivaDisplay
                + " in qualità di Contribuente ");

            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_CTPro
                || p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_TRIBORD)
            {
                v_testo = v_testo.Replace("#TIPO_CONTRODEDUZIONI#", "Atto di costituzione in giudizio e controdeduzioni");
                //sostituzione tipo soggetto (resistente)
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", "Resistente");
            }
            else if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_GDP)
            {
                v_testo = v_testo.Replace("#TIPO_CONTRODEDUZIONI#", "Comparsa di costituzione e risposta");
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", "Convenuta");
            }


            v_testo = v_testo.Replace("#DESTINATARIO#", "Spett.le " + p_istanza.tab_doc_input.tab_autorita_giudiziaria.descrizione_autorita);
            if (p_istanza.tab_doc_input.tab_ricorsi.Single().sezione_giudicante != null)
            {
                v_testo = v_testo.Replace("#SEZIONE#", p_istanza.tab_doc_input.tab_ricorsi.Single().sezione_giudicante);
            }
            else
            {
                v_testo = v_testo.Replace("#SEZIONE#", "");
            }

            if (p_istanza.tab_doc_input.tab_ricorsi.Single().rgr != null)
            {
                v_testo = v_testo.Replace("#RIFERIMENTI#", "R.G. " + p_istanza.tab_doc_input.tab_ricorsi.Single().rgr);
            }
            else
            {
                v_testo = v_testo.Replace("#RIFERIMENTI#", "");
            }
            //Provvisorio
            v_testo = v_testo.Replace("#LEGALE_RAPPRESENTANTE#", "Luigi Monti");
            v_testo = v_testo.Replace("#CF_CDA#", "MNTLGU66H05I483S");
            v_testo = v_testo.Replace("#ADDETTO_LAVORAZIONE#", p_istanza.tab_doc_input.anagrafica_risorse.CognomeNome);
            v_testo = v_testo.Replace("#TIPO_PROCURA#", p_istanza.tab_doc_input.tab_procure.desc_tipo_procura);
            v_testo = v_testo.Replace("#DATA_PROCURA#", p_istanza.tab_doc_input.tab_procure.data_procura.ToShortDateString());
            if (!String.IsNullOrEmpty(p_istanza.tab_doc_input.tab_procure.redattore_procura))
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", p_istanza.tab_doc_input.tab_procure.redattore_procura);
                v_testo = v_testo.Replace("#REPERTORIO#", p_istanza.tab_doc_input.tab_procure.reportorio_raccolta);
            }
            else
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", "");
                v_testo = v_testo.Replace("#REPERTORIO#", "");
            }
            v_testo = v_testo.Replace("#ENTE_CREDITORE#", p_istanza.tab_avv_pag.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#DOMICILIO_ELETTIVO#", p_istanza.tab_doc_input.tab_procure.domicilio_elettivo_procuratore);
            v_testo = v_testo.Replace("#PEC_ADDETTO_LAV#", p_istanza.tab_doc_input.tab_procure.pec_rif_procura);
            v_testo = v_testo.Replace("#TIPO_ATTO#", p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);
            return v_testo;
        }
        public static string GetTestoSezioneSoggettoRicorrenteCTP(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;

            v_testo = GetSezione(p_istanza, "SezioneSoggettoRicorrenteCTP");
            if (p_istanza.tab_doc_input.tab_ricorsi.Single().id_contribuente != null)
            {
                v_testo = v_testo.Replace("#NOMINATIVO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().tab_contribuente.nominativoDisplay);
                v_testo = v_testo.Replace("#CF_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().tab_contribuente.codFiscalePivaDisplay);
                v_testo = v_testo.Replace("#INDIRIZZO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().tab_contribuente.indirizzoTotaleDisplay);
            }
            else if (p_istanza.tab_doc_input.tab_ricorsi.Single().id_referente_destinatario != null)
            {
                v_testo = v_testo.Replace("#NOMINATIVO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().tab_referente.referenteNominativoDisplay);
                v_testo = v_testo.Replace("#CF_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().tab_referente.cod_fiscaleDisplay);
                v_testo = v_testo.Replace("#INDIRIZZO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().tab_referente.indirizzoTotaleDisplay);
            }
            else
            {
                v_testo = v_testo.Replace("#NOMINATIVO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().nominativo_ricorrente);
                //TODO Sandro: gesti
                v_testo = v_testo.Replace("#CF_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().cf_piva_ricorrente);
                v_testo = v_testo.Replace("#INDIRIZZO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().indirizzo_completo_ricorrente);
            }
            if (p_istanza.tab_doc_input.tab_ricorsi.Single().nominativo_avvocato_ricorrente_ricorso != null)
            {
                v_testo = v_testo.Replace("#AVVOCATO_RICORRENTE#", " rappresentato e difeso dall' Avv. " + p_istanza.tab_doc_input.tab_ricorsi.Single().nominativo_avvocato_ricorrente_ricorso);
                v_testo = v_testo.Replace("#PEC_AVVOCATO_RICORRENTE#", p_istanza.tab_doc_input.tab_ricorsi.Single().pec_avvocato_ricorrente);
            }
            else
            {
                v_testo = v_testo.Replace("#AVVOCATO_RICORRENTE#", "");
                v_testo = v_testo.Replace("#PEC_AVVOCATO_RICORRENTE#", "");
            }
            if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_CTPro
    || p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_TRIBORD)
            {
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_RICO#", "Ricorrente");
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", "Resistente");

            }
            else if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_GDP)
            {
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_RICO#", "Attore");
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", "Convenuta");
            }

            return v_testo;
        }
        public static string GetSezioneOggettoRicorsi(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;

            v_testo = GetSezione(p_istanza, "SezioneOggettoRicorsi");

            v_testo = v_testo.Replace("#DESTINATARIO_RICORSO#", " Spett.le " + p_istanza.tab_doc_input.tab_autorita_giudiziaria.descrizione_autorita);
            v_testo = v_testo.Replace("#IDENTIFICATIVO_ATTO#", p_istanza.tab_avv_pag.identificativo_avv_pag);
            v_testo = v_testo.Replace("#DESCR_TIPO_AVVPAG#", p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);
            if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_CTPro
  || p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_TRIBORD)
            {
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_RICO#", "Ricorrente");
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", "Resistente");

            }
            else if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_GDP)
            {
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_RICO#", "Attore");
                v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", "Convenuta");

            }
            v_testo = v_testo.Replace("#DESCRIZIONE_TIPO_RICORSO#", p_istanza.tab_doc_input.tab_tipo_doc_entrate.descr_doc);
            v_testo = v_testo.Replace("#DATA_NOTIFICA_RICORSO#", p_istanza.tab_doc_input.data_presentazione_String);
            return v_testo;
        }
        public static string GetSezioneConcludeRicorsi(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            string v_resistente = string.Empty;
            string v_ricorrente = string.Empty;
            v_testo = GetSezione(p_istanza, "SezioneConclusioneRicorsi");
            v_testo = v_testo.Replace("#DATA#", DateTime.Now.ToShortDateString());
            if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_CTPro
 || p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_TRIBORD)
            {
                v_resistente = "Resistente";
                v_ricorrente = "Ricorrente";
            }
            else if (p_istanza.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_GDP)
            {
                v_resistente = "Convenuta";
                v_ricorrente = "Attore";
            }
            if (p_istanza.tab_avv_pag1.fonte_emissione == tab_avv_pag.FONTE_IMPORTATA)
            {
                string v_testo_add = "Dichiarare la carenza di legittimazione passiva del " + v_resistente + " sulle eccezioni proposte dal Ricorrente attinenti la corretta formazione del titolo esecutivo." +
                   "Disporre in via gradata, ma sempre preliminarmente, ai sensi di ex art. 14 del D.lgs 546 / 92 l’integrazione del contraddittorio nei confronti dell’Ente " + p_istanza.tab_avv_pag.anagrafica_ente.descrizione_ente + ",";
                v_testo = v_testo.Replace("#CONCLUSIONI_ATTI_ENTE#", v_testo_add);
            }
            else
            {
                v_testo = v_testo.Replace("#CONCLUSIONI_ATTI_ENTE#", "");
            }
            v_testo = v_testo.Replace("#TIPO_SOGGETTO_RICO#", v_ricorrente);
            v_testo = v_testo.Replace("#TIPO_SOGGETTO_CONTRO#", v_resistente);
            return v_testo;
        }

        public static string GetTestoImprocedibilitaRicorso(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            v_testo = GetSezione(p_istanza, "TestoImprocedibilita");
            return v_testo;
        }
        //carenza legittimità
        public static string GetTestoControdeduzioniENTE(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            v_testo = GetSezione(p_istanza, "TestoControDeduzioniENTE");

            return v_testo;
        }

        public static string GetTestoControdeduzioniAttiPropri(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            v_testo = GetSezione(p_istanza, "TestoControdeduzioneAttiPropri");
            v_testo = v_testo + Environment.NewLine + " causale della motivazione: " + p_istanza.anagrafica_causale.descr_causale;
            v_testo = v_testo + Environment.NewLine + " esito della lavorazione: " + p_istanza.annotazioni_esito;
            return v_testo;
        }

        public static string GetTestoCessataMateria(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = string.Empty;
            v_testo = GetSezione(p_istanza, "RicorsiCessataMateria");
            v_testo = v_testo.Replace("#DESTINATARIO_RICORSO#", " Spett.le " + p_istanza.tab_doc_input.tab_autorita_giudiziaria.descrizione_autorita);
            v_testo = v_testo.Replace("#LEGALE_RAPPRESENTANTE#", "Luigi Monti");
            v_testo = v_testo.Replace("#IDENTIFICATIVO_AVVPAG#", p_istanza.tab_avv_pag.identificativo_avv_pag);
            v_testo = v_testo.Replace("#DESCR_AVVPAG#", p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);
            v_testo = v_testo.Replace("#IDENTIFICATIVO_DOC_OUTPUT#", p_istanza.tab_doc_input.join_tab_doc_input_tab_doc_output.Single().tab_doc_output.NumDoc);
            return v_testo;
        }

        public static string GetTestoPremessaStragiudizialeCC(tab_avv_pag p_avviso)
        {
            string v_testo = string.Empty;
            v_testo = GetSezione(p_avviso, "PremessaRichiestaStragCC");
            v_testo = v_testo.Replace("#DATA_EMISSIONE_LISTA_DICH#", p_avviso.tab_liste.data_lista_String);
            v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#SOGGETTO_RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#INDIRIZZO_RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.IndirizzoCompleto);
            v_testo = v_testo.Replace("#PEC_RICHIEDENTE#", p_avviso.anagrafica_ente.tab_pec_configurazioni
                        .Where(w => w.id_ente == p_avviso.id_ente && w.id_tipo_servizio == anagrafica_tipo_servizi.SERVIZI_DICHIARAZIONE_STRAGIUDIZIALE)
                        .Where(w => w.flag_input_output == tab_pec_configurazioni.FLAG_INVIO_PEC_USCITA)
                        .Select(s => s.indirizzo_email).FirstOrDefault());
            v_testo = v_testo.Replace("#DESC_TIPO_AVV_PAG#", p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", p_avviso.tab_contribuente.nominativoDisplay);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
            v_testo = v_testo.Replace("#IDENTIFICATIVO_AVVISO#", p_avviso.anagrafica_ente.cod_ente + "/" + p_avviso.identificativo_avv_pag.Trim());
            return v_testo;
        }
        public static string GetTestoPremessa(tab_avv_pag p_avviso)

        {
            string v_testo = string.Empty;
            switch (p_avviso.anagrafica_tipo_avv_pag.id_servizio)
            {
                case anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA:
                case anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_NON_COATTIVI:
                    v_testo = GetSezione(p_avviso, SezioneTestoPremessaAvvRateizzazione);
                    break;
                //case anagrafica_tipo_servizi.INTIM:
                //    v_testo = GetSezione(p_avviso, SezioneTestoPremessaIntimazioneT);
                //    break;
                //PREAVVISO DI FERMO: RICORSO TRIBUTARIO ED EXTRA TRIBUTARIO
                case anagrafica_tipo_servizi.AVVISI_CAUTELARI:
                    if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM)
                    {
                        v_testo = GetSezione(p_avviso, SezioneTestoPremessaPreavvisoFermo);
                        string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                        v_testo = v_testo.Replace("#ANAG_CONCESSIONARIO#", GetSezione(p_avviso, "TestataEnteCreditore")); //p_avviso.anagrafica_ente.descrizione_ente);
                        v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                        v_testo = v_testo.Replace("#IDENTIFICATIVO_AVV#", p_avviso.identificativo_avv_pag);
                        v_testo = v_testo.Replace(EURO_SPESE_FERMO, sImporto.Substring(0, sImporto.Length - 2));
                    }
                    else if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA)
                    {
                        v_testo = GetSezione(p_avviso, PremessaAvvisoDiIpoteca);
                        v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                    }
                    else if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.IPOTECA)
                    {
                        v_testo = GetSezione(p_avviso, "PremessaIscrizioneIpoteca");
                        v_testo = v_testo.Replace("#DESC_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
                        v_testo = v_testo.Replace("#IDENTIFICATIVO_AVV#", p_avviso.TAB_SUPERVISIONE_FINALE_V2.Single().tab_avv_pag2.identificativo_avv_pag);
                        v_testo = v_testo.Replace("#DATA_EMISSIONE#", p_avviso.TAB_SUPERVISIONE_FINALE_V2.Single().tab_avv_pag2.dt_emissione_String);
                        v_testo = v_testo.Replace("#DATA_RICEZIONE#", p_avviso.TAB_SUPERVISIONE_FINALE_V2.Single().tab_avv_pag2.data_ricezione_String);
                    }
                    else if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO)
                    {
                        v_testo = GetTestoPremessaFermoAmm(p_avviso);
                    }
                    break;
                case anagrafica_tipo_servizi.INTIM:
                    v_testo = GetTestoIntimazioneFront(p_avviso);
                    break;
                case anagrafica_tipo_servizi.SERVIZI_DICHIARAZIONE_STRAGIUDIZIALE:
                    v_testo = GetSezione(p_avviso, "PremessaRichiestaStraGAlTerzo");
                    v_testo = v_testo.Replace("#DATA_EMISSIONE_LISTA_DICH#", p_avviso.tab_liste.data_lista_String);
                    v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                    v_testo = v_testo.Replace("#SOGGETTO_RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.descrizione_ente);
                    v_testo = v_testo.Replace("#INDIRIZZO_RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.IndirizzoCompleto);
                    v_testo = v_testo.Replace("#PEC_RICHIEDENTE#", p_avviso.anagrafica_ente.tab_pec_configurazioni
                        .Where(w => w.id_ente == p_avviso.id_ente && w.id_tipo_servizio == anagrafica_tipo_servizi.SERVIZI_DICHIARAZIONE_STRAGIUDIZIALE)
                        .Where(w => w.flag_input_output == tab_pec_configurazioni.FLAG_INVIO_PEC_USCITA)
                        .Select(s => s.indirizzo_email).FirstOrDefault());
                    v_testo = v_testo.Replace("#DESC_TIPO_AVV_PAG#", p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);


                    v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
                    v_testo = v_testo.Replace("#IDENTIFICATIVO_AVVISO#", p_avviso.anagrafica_ente.cod_ente + "/" + p_avviso.identificativo_avv_pag.Trim());
                    if (p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.id_ente == anagrafica_ente.ID_ENTE_PUBLISERVIZI)
                    {
                        v_testo = v_testo.Replace("#TESTO_PUBLISERVIZI#", "società iscritta all'Albo ministeriale " +
                            "di cui all'articolo 53 del decreto legislativo 15 dicembre 1997, " +
                            "n. 446, che include i soggetti abilitati ad effettuare attività di riscossione dei tributi " +
                            "e di altre entrate.");
                    }
                    else
                    {
                        v_testo = v_testo.Replace("#TESTO_PUBLISERVIZI#", "");
                    }
                    //v_testo = v_testo.Replace("#INFORMATIVA_PRIVACY#", GetNuovaInformativaPrivacy(p_avviso));
                    //v_testo = v_testo.Replace("#COD_CONTRIBUENTE#", p_avviso.anagrafica_ente.cod_ente.ToString() + "-" + p_avviso.id_anag_contribuente.ToString());
                    //v_testo = v_testo.Replace("#IDENTIFICATIVO#", p_avviso.identificativo_avv_pag);

                    break;
                //    case anagrafica_tipo_avv_pag.SOLLECITO_POST_INGIUNZIONE_E://SOLLECITO Extra Tributario

                case anagrafica_tipo_servizi.SOLL_PRECOA:
                case anagrafica_tipo_servizi.RISC_PRECOA:
                    v_testo = GetSezione(p_avviso, SezioneTestoPremessaSollecito);
                    v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                    //Hanno deciso di mettere una data fissa (nello specifico 31/08/2018)
                    //DateTime v_demissione = DateTime.Now.AddDays(60);
                    //while (cVerificaGiorniFestivi.CheckFestivi(v_demissione, true) != "")
                    //{
                    //    v_demissione= v_demissione.AddDays(1);
                    //}
                    //if (v_demissione.Month == 8)
                    //{
                    //    v_demissione.AddDays(31);
                    //}

                    v_testo = v_testo.Replace("#DATA_PAGAMENTO#", "31/08/2018");

                    break;
                case anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO:
                    v_testo = GetTestoPremessaOrdine(p_avviso);
                    break;
                //case anagrafica_tipo_avv_pag.PIGN_TERZI_ORD:
                case anagrafica_tipo_servizi.ING_FISC:
                    v_testo = GetTestoIngiunzione(p_avviso);
                    break;
                case anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO:
                    v_testo = GetSezione(p_avviso, PremessaPignoramentiConCitazioneAlTerzo);
                    v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
                    v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
                    v_testo = v_testo.Replace("#RESPONSABILE_EMISSIONE#", p_avviso.tab_sped_not.FirstOrDefault().anagrafica_risorse1.CognomeNome);
                    TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
                    string ContribuenteComposedHTML = string.Empty;
                    tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
                    switch (sp_not.flag_destinatario_spednot)
                    {
                        case "T":
                        case "M":
                            ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                            v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());
                            if (sp_not.tab_contribuente.id_tipo_contribuente == 1)
                            {
                                v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_contribuente.comune_nas);
                                v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_contribuente.data_nas_String);
                                v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                            }
                            else if (sp_not.tab_contribuente.id_tipo_contribuente > 1)
                            {
                                v_testo = v_testo.Replace("#NATO_A#", "");
                                v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                                v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                            }
                            //v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                            //v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                            //v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                            //    + sp_not.tab_contribuente.indirizzoTotaleDisplay + ", " + sp_not.codfis_piva_contribuente
                            //    + " in qualità di Contribuente ");



                            break;
                        case "X":
                        case "Y":
                            ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower());
                            //" " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                            //sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                            v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                                ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());
                            if (sp_not.tab_referente.idTipoReferente == 1)
                            {
                                v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_referente.ComuneNas);
                                v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_referente.dataNas.Value.ToShortDateString());
                                v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_referente.indirizzoTotaleDisplay);

                            }
                            else if (sp_not.tab_referente.idTipoReferente > 1)
                            {
                                v_testo = v_testo.Replace("#NATO_A#", "");
                                v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                                v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                            }
                            //v_testo = v_testo.Replace("#TESTATA_PROCEDENTE#", );
                            //v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_referente.indirizzoTotaleDisplay);
                            //v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " " +
                            //    sp_not.tab_referente.indirizzoTotaleDisplay + " " +
                            //    sp_not.codfis_piva_referente + " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                            //    sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);


                            break;
                    }

                    v_testo = v_testo.Replace("#RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.descrizione_ente);
                    v_testo = v_testo.Replace("#INDIRIZZO_RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.indirizzo ?? "");
                    v_testo = v_testo.Replace("#PEC_RICHIEDENTE#", p_avviso.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.pec ?? "");
                    v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente ?? "");
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
                    v_testo = v_testo.Replace("#IDENTIFICATIVO_AVVISO#", p_avviso.identificativo_avv_pag);
                    v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo);
                    v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
                    v_testo = v_testo.Replace("#DATA_EMISSIONE#", p_avviso.dt_emissione_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_PEC#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.pec_rif_procura ?? "");
                    v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.rif_legislvativo_procura ?? "");
                    //v_testo = v_testo.Replace("#TIPO_RAPPORTO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.tipo_rapporto);
                    v_testo = v_testo.Replace("#TITOLO_PROCURATORE#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.titolo_procuratore ?? "");
                    v_testo = v_testo.Replace("#PROCURATORE#", p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.nome + " "
                        + p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.cognome + " ");
                    v_testo = v_testo.Replace("#TIPO_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.desc_tipo_procura);
                    v_testo = v_testo.Replace("#CF_PROCURATORE#", p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.cod_fiscale ?? "");
                    if (p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.flag_tipo_procura == "PN")
                    {
                        v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "" + p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.data_procura ?? "");
                    }
                    System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                    rtBox.Rtf = GetTestataProcedente(p_avviso);
                    v_testo = v_testo.Replace("#CREDITORE_PROCEDENTE#", rtBox.Text);
                    v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
                    v_testo = v_testo.Replace("#DATA_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.data_procura.ToShortDateString());
                    v_testo = v_testo.Replace("#REDATTORE_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura);
                    v_testo = v_testo.Replace("#DOMICILIO_ELETTIVO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.domicilio_elettivo_procuratore ?? "");
                    v_testo = v_testo.Replace("#INDIRIZZO_PEC#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.pec_rif_procura ?? "");
                    v_testo = v_testo.Replace("#ABI#", p_avviso.tab_citazioni.Single().tab_terzo == null ? "" : p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().abi_cc ?? "");
                    v_testo = v_testo.Replace("#CAB#", p_avviso.tab_citazioni.Single().tab_terzo == null ? "" : p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().cab_cc ?? "");
                    v_testo = v_testo.Replace("#TIPO_ATTO#", p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_avviso.identificativo_avv_pag);
                    v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "");

                    break;
                case anagrafica_tipo_servizi.GEST_ORDINARIA:
                    if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ORD_TARI_POP
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI_POP
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ORDINARIO_TARI_NOT
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_SUPPLETIVO_TARI_NOT)
                    {
                        v_testo = GetTestoPremessaTARI(p_avviso);
                    }
                    else if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_POP_TARI
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI_ESECUTIVO)
                    {
                        v_testo = GetTestoPremessaAccertamentoTARI(p_avviso);
                    }
                    else if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI
                        || p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_TARI)
                    {
                        v_testo = GetTestoPremessaOmessaDenuncia(p_avviso);
                    }
                    break;

                case anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO:
                    if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_AOP_TARI_ESECUTIVO)
                    {
                        v_testo = GetTestoPremessaAccertamentoTARI(p_avviso);
                    }
                    else if (p_avviso.id_tipo_avvpag == anagrafica_tipo_avv_pag.AVVISO_ACC_OMESSA_ESEC_TARI)
                    {
                        v_testo = GetTestoPremessaOmessaDenuncia(p_avviso);
                    }
                    break;
            }

            return v_testo;
        }

        public static string GetTestoPremessaCitaII(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "PremessaCitazioneAlTerzoII");
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());
                    if (sp_not.tab_contribuente.id_tipo_contribuente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a  a, " + sp_not.tab_contribuente.comune_nas ?? "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_contribuente.data_nas_String);
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_contribuente.id_tipo_contribuente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower() +
                        " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());
                    if (sp_not.tab_referente.idTipoReferente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_referente.ComuneNas ?? "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_referente.dataNas.Value.ToShortDateString() ?? "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_referente.idTipoReferente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }

                    break;
            }
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo ?? "");
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_avviso.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
            v_testo = v_testo.Replace("#ABI#", p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().abi_cc);
            v_testo = v_testo.Replace("#CAB#", p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().cab_cc);
            return v_testo;
        }
        public static string GetTestoPremessaCitaIII(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "PremessaCitazioneAlTerzoIII");
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());
                    if (sp_not.tab_contribuente.id_tipo_contribuente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_contribuente.comune_nas);
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_contribuente.data_nas_String);
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_contribuente.id_tipo_contribuente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower() +
                        " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());
                    if (sp_not.tab_referente.idTipoReferente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_referente.ComuneNas ?? "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_referente.dataNas.Value.ToShortDateString() ?? "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_referente.idTipoReferente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }
                    break;
            }
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_avviso.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#FIRMA_AUTOGRAFA#", "La firma autografa legale è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
                   "comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.");
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
            v_testo = v_testo.Replace("#ABI#", p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().abi_cc);
            v_testo = v_testo.Replace("#CAB#", p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().cab_cc);
            return v_testo;
        }
        public static string GetTestoPremessaOrdine(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "PremessaCitazioneAlTerzoIV");
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());
                    if (sp_not.tab_contribuente.id_tipo_contribuente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_contribuente.comune_nas);
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_contribuente.data_nas_String);
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_contribuente.id_tipo_contribuente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower() +
                        " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());

                    if (sp_not.tab_referente.idTipoReferente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_referente.ComuneNas);
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_referente.dataNas.Value.ToShortDateString());
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_referente.idTipoReferente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    break;

            }
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo ?? "");
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_avviso.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
            if (p_avviso.tab_citazioni.Single().tab_avv_pag1 != null)
            {
                v_testo = v_testo.Replace("#IDENTIFICATIVO_ORDINE#", p_avviso.tab_citazioni.Single().tab_avv_pag1.identificativo_avv_pag.Trim());
                v_testo = v_testo.Replace("#DATA_NOTIFICA#", p_avviso.tab_citazioni.Single().tab_avv_pag1.data_ricezione_String.Trim());
            }
            else
            {
                v_testo = v_testo.Replace("#IDENTIFICATIVO_ORDINE#", "");
                v_testo = v_testo.Replace("#DATA_NOTIFICA#", "");
            }
            v_testo = v_testo.Replace("#FIRMA_AUTOGRAFA#", "La firma autografa è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
       "comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.");
            v_testo = v_testo.Replace("#ABI#", p_avviso.tab_sped_not.FirstOrDefault().tab_cc_riscossione.ABI_CC ?? "");
            v_testo = v_testo.Replace("#CAB#", p_avviso.tab_sped_not.FirstOrDefault().tab_cc_riscossione.CAB_CC ?? "");
            return v_testo;
        }

        //TestoIntimazione Pagina 1
        public static string GetTestoIntimazioneFront(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoIntimazioneFront);
            v_testo = v_testo.Replace("#DES_COMUNE#", p_avviso.anagrafica_ente.descrizione_ente);
            return v_testo;
        }

        public static string GetTestoCitazione_Cita(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoCitazione_Cita);
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());


                    if (sp_not.tab_contribuente.id_tipo_contribuente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_contribuente.comune_nas ?? "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_contribuente.data_nas_String);
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_contribuente.id_tipo_contribuente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }

                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower() +
                        " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());
                    if (sp_not.tab_referente.idTipoReferente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_referente.ComuneNas ?? "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_referente.dataNas.Value.ToShortDateString());
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_referente.idTipoReferente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }

                    break;
            }
            string v_impo_magg = string.Empty;
            decimal? impo_iscriz = p_avviso.importo_iscrizione_ruolo;
            if (p_avviso.importo_iscrizione_ruolo == null || p_avviso.importo_iscrizione_ruolo == 0)
            {
                v_impo_magg = ((p_avviso.importo_tot_da_pagare / 2) + p_avviso.importo_tot_da_pagare).Value.ToString("N2");
            }
            else
            {
                v_impo_magg = (impo_iscriz + p_avviso.importo_tot_da_pagare).Value.ToString("N2");
            }
            v_testo = v_testo.Replace("#TESTATA_PROCEDENTE#", GetSezione(p_avviso, "TestataProcedente"));
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_avviso.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#CF_TERZO#", sp_not.codfis_piva_terzo);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", " € " + p_avviso.importoTotDaPagare.ToString("N2"));
            v_testo = v_testo.Replace("#IMPORTO_DEBITO_MAGG#", " € " + v_impo_magg);
            v_testo = v_testo.Replace("#INDIRIZZO_PEC#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.pec_rif_procura ?? "");
            v_testo = v_testo.Replace("#PROCURATORE#", p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.nome + " "
                + p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.cognome);
            v_testo = v_testo.Replace("#TITOLO_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.titolo_procuratore ?? "");

            //DateTime v_demissione = DateTime.Now.AddDays(60);
            //while (cVerificaGiorniFestivi.CheckFestivi(v_demissione, true) != "")
            //{
            //    v_demissione.AddDays(1);
            //}
            //if (v_demissione.Month==8)
            //{
            //    v_demissione.AddDays(31);
            //}
            v_testo = v_testo.Replace("#DATA_UDIENZA#", p_avviso.tab_citazioni.Single().data_prima_udienza.Value.ToShortDateString());
            v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "");
            v_testo = v_testo.Replace("#DATA_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.data_procura.ToShortDateString());
            v_testo = v_testo.Replace("#REDATTORE_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura ?? "");
            v_testo = v_testo.Replace("#DATA_EMISSIONE#", p_avviso.dt_emissione_String ?? "");

            //     v_testo = v_testo.Replace("#FIRMA_AUTOGRAFA#", "La firma autografa è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
            //"comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.");
            if (p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().id_tipo_bene == 5)
            {
                v_testo = v_testo.Replace("#ABI#", " ABI: " + p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().abi_cc);
                v_testo = v_testo.Replace("#CAB#", " CAB: " + p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().cab_cc);
            }
            else
            {
                v_testo = v_testo.Replace("#ABI#", "");
                v_testo = v_testo.Replace("#CAB#", "");
            }

            v_testo = v_testo.Replace("#TIPO_ATTO#", p_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag ?? "");
            if (!String.IsNullOrEmpty(p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura))
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura ?? "");
                v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "");
                v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.rif_legislvativo_procura ?? "");
            }
            else
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", "");
                v_testo = v_testo.Replace("#REPERTORIO#", "");
                v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.rif_legislvativo_procura ?? "");
            }

            return v_testo;
        }
        private static string GetSoggettoDebitore(tab_sped_not p_Sped)
        {
            switch (p_Sped.flag_destinatario_spednot)
            {
                case "C":
                    return p_Sped.tab_contribuente.nominativoDisplay + " " + p_Sped.tab_contribuente.codFiscalePivaDisplay;
                case "R":
                    return p_Sped.tab_referente.nome + " " + p_Sped.tab_referente.cognome + " " + p_Sped.tab_referente.cod_fiscaleDisplay;
                default:
                    return p_Sped.tab_contribuente.nominativoDisplay + " " + p_Sped.tab_contribuente.codFiscalePivaDisplay;
            }
            return "";
        }
        public static string GetTestoCitazione_UffRisc(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoCitazioneUffRisc);
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            //TODO: 27/03/2018 prendere da tab_citazioni quando verrà implementato il consolidamento
            string v_AnnoCronologico = p_avviso.tab_registro_ufficiali_riscossione
                .Where(w => w.id_risorsa == w.tab_avv_pag.tab_citazioni.Single().id_ufficiale_riscossione).FirstOrDefault().anno_esercizio.ToString();
            v_testo = v_testo.Replace("#NUMERO_CRONOLOGICO#", p_avviso.tab_registro_ufficiali_riscossione
                .Where(w => w.id_risorsa == w.tab_avv_pag.tab_citazioni.Single().id_ufficiale_riscossione).FirstOrDefault().numero_cronologico.ToString() + "/" +
                v_AnnoCronologico);
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());
                    if (sp_not.tab_contribuente.id_tipo_contribuente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_contribuente.comune_nas);
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_contribuente.data_nas_String);
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_contribuente.id_tipo_contribuente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    }
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                               + sp_not.tab_contribuente.indirizzoTotaleDisplay + ", C.F./P.Iva: " + sp_not.codfis_piva_contribuente
                               + " in qualità di Contribuente ");
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower());
                    //" " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                    //sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    if (sp_not.tab_referente.idTipoReferente == 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", " nato/a a, " + sp_not.tab_referente.ComuneNas);
                        v_testo = v_testo.Replace("#DATA_NASCITA#", " il " + sp_not.tab_referente.dataNas.Value.ToShortDateString());
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " residente in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }
                    else if (sp_not.tab_referente.idTipoReferente > 1)
                    {
                        v_testo = v_testo.Replace("#NATO_A#", "");
                        v_testo = v_testo.Replace("#DATA_NASCITA#", "");
                        v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", " con sede in " + sp_not.tab_referente.indirizzoTotaleDisplay);
                    }
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " " +
                        sp_not.tab_referente.indirizzoTotaleDisplay + " " +
                        sp_not.codfis_piva_referente + " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    break;
            }
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataProcedente(p_avviso);
            string v_impo_magg = string.Empty;
            decimal? impo_iscriz = p_avviso.importo_iscrizione_ruolo;
            if (p_avviso.importo_iscrizione_ruolo == null || p_avviso.importo_iscrizione_ruolo == 0)
            {
                v_impo_magg = ((p_avviso.importo_tot_da_pagare / 2) + p_avviso.importo_tot_da_pagare).Value.ToString("N2");
            }
            else
            {
                v_impo_magg = (impo_iscriz + p_avviso.importo_tot_da_pagare).Value.ToString("N2");
            }
            v_testo = v_testo.Replace("#TESTATA_PROCEDENTE#", rtBox.Text);
            v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "");
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_avviso.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#CF_TERZO#", sp_not.codfis_piva_terzo);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", p_avviso.importoTotDaPagare.ToString("C2"));
            v_testo = v_testo.Replace("#IMPORTO_DEBITO_MAGG#", v_impo_magg);
            v_testo = v_testo.Replace("#UFFICIALE_RISCOSSIONE#", p_avviso.tab_citazioni.Single().anagrafica_risorse3.CognomeNome);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
            //     v_testo = v_testo.Replace("#FIRMA_AUTOGRAFA#", "La firma autografa è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
            //"comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.");
            if (p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().id_tipo_bene == 5)
            {
                v_testo = v_testo.Replace("#ABI#", " ABI: " + p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().abi_cc);
                v_testo = v_testo.Replace("#CAB#", " CAB: " + p_avviso.tab_citazioni.Single().tab_terzo.tab_terzo_debitore.FirstOrDefault().cab_cc);
            }
            else
            {
                v_testo = v_testo.Replace("#ABI#", "");
                v_testo = v_testo.Replace("#CAB#", "");
            }
            int id_risorsa = p_avviso.tab_citazioni.Single().anagrafica_risorse3.id_risorsa;
            if (!String.IsNullOrEmpty(p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura ?? ""))
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", p_avviso.tab_citazioni.Single().anagrafica_risorse3.tab_procure.FirstOrDefault().redattore_procura ?? "");
                v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "");
                v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", v_testo = p_avviso.tab_citazioni.Single().anagrafica_risorse3.tab_procure.FirstOrDefault().rif_legislvativo_procura ?? "");
            }
            else
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", "");
                v_testo = v_testo.Replace("#REPERTORIO#", "");
                v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_avviso.tab_citazioni.Single().anagrafica_risorse3.tab_procure.FirstOrDefault().rif_legislvativo_procura ?? "");
            }


            return v_testo;
        }


        public static string GetTestoCitazioneNotifica(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "CitazioneNotifica");
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            //tab_sped_not sp_not = p_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE);
            //TODO: 27/03/2018 prendere da tab_citazioni quando verrà implementato il consolidamento
            v_testo = v_testo.Replace("#NUMERO_CRONOLOGICO#", p_sped_not.tab_avv_pag.tab_registro_ufficiali_riscossione
                .Where(w => w.id_risorsa == w.tab_avv_pag.tab_citazioni.Single().id_ufficiale_riscossione).OrderByDescending(o => o.tab_avv_pag.tab_citazioni.Single().id_citazione).First().numero_cronologico.ToString());
            switch (p_sped_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(p_sped_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", p_sped_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + p_sped_not.codfis_piva_contribuente.Trim() : " C.F.:" + p_sped_not.codfis_piva_contribuente.Trim());
                    v_testo = v_testo.Replace("#NATO_A#", p_sped_not.tab_contribuente.comune_nas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", p_sped_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", p_sped_not.tab_contribuente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                               + p_sped_not.tab_contribuente.indirizzoTotaleDisplay + ", C.F./P.Iva: " + p_sped_not.codfis_piva_contribuente
                               + " in qualità di Contribuente ");
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(p_sped_not.cognome_ragsoc_referente.ToLower());
                    //" " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                    //sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", p_sped_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + p_sped_not.codfis_piva_referente.Trim() : " C.F.:" + p_sped_not.codfis_piva_referente.Trim());
                    v_testo = v_testo.Replace("#NATO_A#", p_sped_not.tab_referente.ComuneNas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", p_sped_not.tab_referente.dataNas.Value.ToShortDateString());
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", p_sped_not.tab_referente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " " +
                        p_sped_not.tab_referente.indirizzoTotaleDisplay + " " +
                        p_sped_not.codfis_piva_referente + " " + p_sped_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        p_sped_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + p_sped_not.codfis_piva_contribuente);
                    break;
            }
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", p_sped_not.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_sped_not.tab_avv_pag.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#CF_TERZO#", p_sped_not.codfis_piva_terzo);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", p_sped_not.tab_avv_pag.importoTotDaPagare.ToString("C2"));
            v_testo = v_testo.Replace("#UFFICIALE_RISCOSSIONE#", p_sped_not.tab_avv_pag.tab_citazioni.Single().anagrafica_risorse3.CognomeNome);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#DATA_NOTIFICA#", p_sped_not.data_esito_notifica_String);
            v_testo = v_testo.Replace("#TRIBUNALE#", p_sped_not.tab_avv_pag.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
            if (!string.IsNullOrEmpty(p_sped_not.numero_raccomandata))
            {
                v_testo = v_testo.Replace("#DATA_SPE_NOTIFICA#", "in data" + p_sped_not.dt_spedizione_notifica_String);
                v_testo = v_testo.Replace("#NUMERO_RACCOMANDATA#", " con raccomandata AG n.: " + p_sped_not.numero_raccomandata);
            }
            else
            {
                v_testo = v_testo.Replace("#DATA_SPE_NOTIFICA#", "");
                v_testo = v_testo.Replace("#NUMERO_RACCOMANDATA#", "");
                v_testo = v_testo.Replace("#MODALITA_NOTIFICA#", "");
                //v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_sped_not.tab_citazioni.Single().tab_doc_input.tab_procure.rif_legislvativo_procura);
            }

            v_testo = v_testo.Replace("#FIRMA_AUTOGRAFA#", "La firma autografa è sostituita dall'indicazione a stampa del nominativo dello stesso ai sensi dell'art 3 " +
       "comma 2 D.L. 39/93 e dell'art 1 comma 87, Legge 28/12/1995.");


            return v_testo;
        }
        public static string GetTestoCitazioneAsseverazione(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "AsseverazioneConformita");
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            //TODO: 27/03/2018 prendere da tab_citazioni quando verrà implementato il consolidamento
            v_testo = v_testo.Replace("#NUMERO_CRONOLOGICO#", p_avviso.tab_citazioni.Single().cronologico_registro_ufficiale_giudiziario);
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_contribuente.Trim() : " C.F.:" + sp_not.codfis_piva_contribuente.Trim());
                    v_testo = v_testo.Replace("#NATO_A#", sp_not.tab_contribuente.comune_nas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                               + sp_not.tab_contribuente.indirizzoTotaleDisplay + ", C.F./P.Iva: " + sp_not.codfis_piva_contribuente
                               + " in qualità di Contribuente ");
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower());
                    //" " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                    //sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente.Trim().Length < 16
                        ? " p.iva:" + sp_not.codfis_piva_referente.Trim() : " C.F.:" + sp_not.codfis_piva_referente.Trim());
                    v_testo = v_testo.Replace("#NATO_A#", sp_not.tab_referente.ComuneNas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_referente.dataNas.Value.ToShortDateString());
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_referente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " " +
                        sp_not.tab_referente.indirizzoTotaleDisplay + " " +
                        sp_not.codfis_piva_referente + " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    break;
            }
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", sp_not.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#INDIRIZZO_TERZO#", p_avviso.tab_citazioni.Single().tab_terzo.indirizzoTotaleDisplay);
            v_testo = v_testo.Replace("#CF_TERZO#", sp_not.codfis_piva_terzo);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", p_avviso.importoTotDaPagare.ToString("C2"));
            v_testo = v_testo.Replace("#UFFICIALE_RISCOSSIONE#", p_avviso.tab_citazioni.Single().anagrafica_risorse3.CognomeNome);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#DATA_EMISSIONE#", p_avviso.dt_emissione_String);
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());
            v_testo = v_testo.Replace("#PROCURATORE#", p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.CognomeNome);
            v_testo = v_testo.Replace("#CF_PROCURATORE#", p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.cod_fiscale);
            v_testo = v_testo.Replace("#IDENTIFICATIVO_AVV_PAG#", p_avviso.identificativo_avv_pag.Trim());
            v_testo = v_testo.Replace("#PROCURATORE_W_TITOLO#", p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.titolo + " " + p_avviso.tab_citazioni.Single().tab_doc_input.anagrafica_risorse2.CognomeNome);
            if (!String.IsNullOrEmpty(p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura))
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.redattore_procura ?? "");
                v_testo = v_testo.Replace("#REPERTORIO#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.reportorio_raccolta ?? "");
                v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.rif_legislvativo_procura ?? "");
            }
            else
            {
                v_testo = v_testo.Replace("#REDATTORE_PROCURA#", "");
                v_testo = v_testo.Replace("#REPERTORIO#", "");
                v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", p_avviso.tab_citazioni.Single().tab_doc_input.tab_procure.rif_legislvativo_procura ?? "");
            }
            return v_testo;
        }

        public static string GetTestoPremessaInsinuazione(dbEnte p_dbContext, tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "PremessaInsinuazioneAlPassivo");
            string ContribuenteComposedHTML = string.Empty;
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            //TODO: 27/03/2018 prendere da tab_citazioni quando verrà implementato il consolidamento
            //v_testo = v_testo.Replace("#NUMERO_PROCEDIMENTO#", p_avviso.tab_registro_ufficiali_riscossione
            //    .Where(w => w.id_risorsa == w.tab_avv_pag.tab_citazioni.Single().id_risorsa_procuratore_1).OrderByDescending(o => o.tab_avv_pag.tab_citazioni.Single().id_citazione).First().numero_cronologico.ToString() + "/" + DateTime.Now.Year.ToString());
            switch (sp_not.flag_destinatario_spednot)
            {
                case "T":
                case "M":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#NATO_A#", sp_not.tab_contribuente.comune_nas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                               + sp_not.tab_contribuente.indirizzoTotaleDisplay + ", C.F./P.Iva: " + sp_not.codfis_piva_contribuente
                               + " in qualità di Contribuente ");
                    break;
                case "X":
                case "Y":
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_referente.ToLower());
                    //" " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                    //sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_referente);
                    v_testo = v_testo.Replace("#NATO_A#", sp_not.tab_referente.ComuneNas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_referente.dataNas.Value.ToShortDateString());
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_referente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " " +
                        sp_not.tab_referente.indirizzoTotaleDisplay + " " +
                        sp_not.codfis_piva_referente + " " + sp_not.tab_referente.join_referente_contribuente.Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() + " del Contribuente " +
                        sp_not.cognome_ragsoc_contribuente + " Cod. Fiscale /P.Iva " + sp_not.codfis_piva_contribuente);
                    break;
                default:
                    ContribuenteComposedHTML = myTI.ToTitleCase(sp_not.cognome_ragsoc_contribuente.ToLower());
                    v_testo = v_testo.Replace("#COD_FISCALE_SOGG_DEBITORE#", sp_not.codfis_piva_contribuente);
                    v_testo = v_testo.Replace("#NATO_A#", sp_not.tab_contribuente.comune_nas);
                    v_testo = v_testo.Replace("#DATA_NASCITA#", sp_not.tab_contribuente.data_nas_String);
                    v_testo = v_testo.Replace("#INDIRIZZO_SOGG_DEBITORE#", sp_not.tab_contribuente.indirizzoTotaleDisplay);
                    v_testo = v_testo.Replace("#SOGGETTO_DEBITORE_COMPLETO#", ContribuenteComposedHTML + " "
                               + sp_not.tab_contribuente.indirizzoTotaleDisplay + ", C.F./P.Iva: " + sp_not.codfis_piva_contribuente
                               + " in qualità di Contribuente ");
                    break;

            }
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.tab_autorita_giudiziaria.descrizione_autorita);
            v_testo = v_testo.Replace("#DESC_PROCEDURA#", p_avviso.tab_contribuente.contribuenteNominativoDisplay + "  - " + p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.anagrafica_procedure_concorsuali.descr_proc_concorsuale);
            v_testo = v_testo.Replace("#DESC_PROCEDURA_RID#", p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.anagrafica_procedure_concorsuali.descr_proc_concorsuale);
            v_testo = v_testo.Replace("#GIUDICE_DELEGATO#", p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.nome_giudice_delegato + " " + p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.cognome_giudice_delegato);
            v_testo = v_testo.Replace("#CURATORE_COMMISSARIO#", sp_not.nominativo_recapito);
            if (String.IsNullOrEmpty(p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.nome_giudice_delegato))
            {
                v_testo = v_testo.Replace("#ILLUSTRISSIMO#", "");
            }
            else
            {
                v_testo = v_testo.Replace("#ILLUSTRISSIMO#", "Preg.mo Giudice Delegato");
            }
            if (!string.IsNullOrEmpty(p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.numero_proc_concorsuale))
            {
                string v_numprocedura = p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.numero_proc_concorsuale;
                string v_sottotipo = string.Empty;
                if (!string.IsNullOrEmpty(p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.descr_sottotipo_proc_concorsuale)) ;
                {
                    v_sottotipo = p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.descr_sottotipo_proc_concorsuale;
                }

                v_testo = v_testo.Replace("#NUM_PROCEDURA#", p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.numero_proc_concorsuale + " " + v_sottotipo);

            }
            else
            {
                v_testo = v_testo.Replace("#NUM_PROCEDURA#", "");
            }
            DateTime? v_data_verifica_stato_passivo = (DateTime?)p_avviso.join_contribuenti_procedure_concorsuali
                            .Single().tab_procedure_concorsuali.data_verifica_stato_passivo;

            DateTime? data_deposito_esecutivita_stato_passivo = (DateTime?)p_avviso.join_contribuenti_procedure_concorsuali
                            .Single().tab_procedure_concorsuali.data_deposito_esecutivita_stato_passivo;
            string v_titolo = string.Empty;
            //secondo specifiche del 18/01/2021 verifico se si tratta di una domanda di ammissione 
            //o di una richiesta
            //per default è una domanda
            v_titolo = "DOMANDA DI AMMISSIONE AL PASSIVO";
            if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
                .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "TRIFAL")
            //|| p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //.tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "PROFAL")
            {
                //v_testo = v_testo.Replace("#TITOLO#", "DOMANDA DI AMMISSIONE AL PASSIVO");
                v_titolo = "DOMANDA DI AMMISSIONE AL PASSIVO";
                if (v_data_verifica_stato_passivo != null
                      && DateTime.Now > v_data_verifica_stato_passivo.Value.Subtract(new TimeSpan(30, 0, 0, 0))
                      && (data_deposito_esecutivita_stato_passivo == null || DateTime.Now <= data_deposito_esecutivita_stato_passivo.Value.AddMonths(12)))
                {
                    v_titolo = v_titolo + " TARDIVA";
                }
                else if (v_data_verifica_stato_passivo != null
                    && DateTime.Now > v_data_verifica_stato_passivo.Value.Subtract(new TimeSpan(30, 0, 0, 0))
                    && (data_deposito_esecutivita_stato_passivo == null || DateTime.Now > data_deposito_esecutivita_stato_passivo.Value.AddMonths(12)))
                {
                    v_titolo = v_titolo + " ULTRATARDIVA";
                }
            }
            //else if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //    .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "AMMCOM")
            //{
            //    //v_testo = v_testo.Replace("#TITOLO#", "RICHIESTA DI AMMISSIONE ALLA MASSA PASSIVA");
            //    v_titolo = "RICHIESTA DI AMMISSIONE ALLA MASSA PASSIVA";
            //}

            else if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
                .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "LIQPAT")
            {
                //v_testo = v_testo.Replace("#TITOLO#", "RICHIESTA DI AMMISSIONE ALLA MASSA PASSIVA");
                v_titolo = "Ricorso ex art. 14 septies legge 3/2012";
            }
            else if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
                .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "PRECRE")
            {
                //v_testo = v_testo.Replace("#TITOLO#", "RICHIESTA DI AMMISSIONE ALLA MASSA PASSIVA");
                v_titolo = "Precisazione del credito";
            }
            v_testo = v_testo.Replace("#TITOLO#", v_titolo);
            //if (v_data_verifica_stato_passivo != null
            //    && DateTime.Now <= v_data_verifica_stato_passivo.Value.Subtract(new TimeSpan(30, 0, 0, 0)))

            //{
            //    if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //    .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "TRIFAL"
            //    || p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //    .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "PROFAL")
            //    {
            //        v_testo = v_testo.Replace("#TITOLO#", "DOMANDA DI AMMISSIONE AL PASSIVO");
            //    }
            //    else
            //    {
            //        v_testo = v_testo.Replace("#TITOLO#", "RICHIESTA DI AMMISSIONE ALLA MASSA PASSIVA");
            //    }

            //}

            //else if (v_data_verifica_stato_passivo != null
            //  && DateTime.Now > v_data_verifica_stato_passivo.Value.Subtract(new TimeSpan(30, 0, 0, 0))
            //  && (data_deposito_esecutivita_stato_passivo == null || DateTime.Now <= data_deposito_esecutivita_stato_passivo.Value.AddMonths(12)))


            //    if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //        .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "TRIFAL"
            //        || p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //        .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "PROFAL")
            //    {
            //        v_testo = v_testo.Replace("#TITOLO#", "DOMANDA TARDIVA DI AMMISSIONE AL PASSIVO");
            //    }
            //    else
            //    {
            //        v_testo = v_testo.Replace("#TITOLO#", "RICHIESTA TARDIVA DI AMMISSIONE ALLA MASSA PASSIVA");
            //    }

            //else if (v_data_verifica_stato_passivo != null
            // && DateTime.Now > v_data_verifica_stato_passivo.Value.Subtract(new TimeSpan(30, 0, 0, 0))
            // && (data_deposito_esecutivita_stato_passivo == null || DateTime.Now > data_deposito_esecutivita_stato_passivo.Value.AddMonths(12)))


            //{
            //    if (p_avviso.join_contribuenti_procedure_concorsuali.Single()
            //   .tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "TRIFAL")
            //    {
            //        v_testo = v_testo.Replace("#TITOLO#", "DOMANDA ULTRATARDIVA DI AMMISSIONE AL PASSIVO");
            //    }
            //    else
            //    {
            //        v_testo = v_testo.Replace("#TITOLO#", "RICHIESTA ULTRATARDIVA DI AMMISSIONE ALLA MASSA PASSIVA");
            //    }

            //}

            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", ContribuenteComposedHTML);
            v_testo = v_testo.Replace("#IMPORTO_DEBITO#", p_avviso.importoTotDaPagare.ToString("N2"));
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", p_avviso.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#DATA_NOTIFICA#", sp_not.data_esito_notifica_String);
            //            v_testo = v_testo.Replace("#DATA_PROCEDURA#", sp_not.tab_contribuente.data_nas_String);
            v_testo = v_testo.Replace("#TRIBUNALE#", p_avviso.tab_citazioni.Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault());

            tab_procure v_procura = new tab_procure();
            v_procura = TabProcureBD.GetList(p_dbContext).Where(w => w.id_risorsa == 149).FirstOrDefault();
            //il dottore ha deciso di cablare momentaneamente la risorsa
            //del responsabile dell'ufficio legale
            //v_procura = p_avviso.join_contribuenti_procedure_concorsuali.FirstOrDefault().tab_doc_input.anagrafica_risorse
            //    .join_risorse_ser_comuni
            //    .Select(s => s.tab_procure).Where(w => w.id_risorsa == 149).FirstOrDefault();
            v_testo = v_testo.Replace("#PROCURATORE#", v_procura.anagrafica_risorse.CognomeNome);
            v_testo = v_testo.Replace("#DESC_CURATORE#", p_avviso.join_contribuenti_procedure_concorsuali
                .FirstOrDefault().tab_procedure_concorsuali.anagrafica_procedure_concorsuali.anagrafica_tipo_relazione.desc_tipo_relazione);
            v_testo = v_testo.Replace("#REDATTORE_PROCURA#", v_procura.redattore_procura);
            v_testo = v_testo.Replace("#DOMICILIO_ELETTIVO#", v_procura.domicilio_elettivo_procuratore);
            v_testo = v_testo.Replace("#RIFERIMENTI_LGS#", v_procura.rif_legislvativo_procura);
            v_testo = v_testo.Replace("#TIPO_PROCURA#", v_procura.desc_tipo_procura);
            v_testo = v_testo.Replace("#REPERTORIO#", v_procura.reportorio_raccolta);
            v_testo = v_testo.Replace("#DATA_PROCEDURA#", p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.data_avviso_apertura_procedura.Value.ToShortDateString());
            v_testo = v_testo.Replace("#INDIRIZZO_PEC#", v_procura.pec_rif_procura);


            //v_testo = v_testo.Replace("#DATA_SPE_NOTIFICA#", p_avviso.data tab_sped_not.Where(w => w.id_tipo_esito_notifica != null).Single().data_esito_notifica_String);
            //v_testo = v_testo.Replace("#MODALITA_NOTIFICA#", p_avviso.tab_sped_not.Where(w => w.id_tipo_esito_notifica != null).Single().anagrafica_tipo_esito_notifica.descr_tipo_esito_notifica);

            return v_testo;
        }

        public static string GetTestoInsinuazioneChiede(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "InsinuazioneAlPassivoChiede");
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            tab_sped_not sp_not = p_avviso.tab_sped_not.Where(w => w.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB && w.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE).Single();
            //if (p_avviso.join_contribuenti_procedure_concorsuali.Single().tab_procedure_concorsuali.anagrafica_procedure_concorsuali.cod_tipo_proc_concorsuale == "TRIFAL")
            //{
            v_testo = v_testo.Replace("#TIPO_AMMISSIONE_AL_PASSIVO#", "di essere ammessa alla procedura in epigrafe indicata");
            //}
            //else
            //{
            //    v_testo = v_testo.Replace("TIPO_AMMISSIONE_AL_PASSIVO#", "di essere ammesso/a al passivo della liquidazione coatta amministrativa");
            //}
            Utilities.WordyFormatProviderITA v_class = new WordyFormatProviderITA();
            v_testo = v_testo.Replace("#IMPORTO_AVVISO#", p_avviso.importoTotDaPagare.ToString("N2"));
            decimal v_crediti_privilegiati = p_avviso.join_contribuenti_procedure_concorsuali.Single().tot_crediti_privilegiati_domanda == null ?
                0 : p_avviso.join_contribuenti_procedure_concorsuali.Single().tot_crediti_privilegiati_domanda.Value;
            decimal v_crediti_chirografati = p_avviso.join_contribuenti_procedure_concorsuali.Single().tot_crediti_chirografari_domanda == null ?
                0 : p_avviso.join_contribuenti_procedure_concorsuali.Single().tot_crediti_chirografari_domanda.Value;
            v_testo = v_testo.Replace("#DEBITI_PRIVILEGIATI#", v_crediti_privilegiati.ToString("N2"));
            v_testo = v_testo.Replace("#DEBITI_CHIROGRAFARI#", v_crediti_chirografati.ToString("N2"));
            v_testo = v_testo.Replace("#IMPORTO_AVVISO_LET#", v_class.Format("W", p_avviso.importoTotDaPagare, CultureInfo.InvariantCulture.NumberFormat));
            v_testo = v_testo.Replace("#DATA_ODIERNA#", DateTime.Now.ToShortDateString());

            return v_testo;
        }
        public static string GetTestoIngiunzione(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoIngiunzione);

            return v_testo;
        }
        public static string GetAvvertenzePreavvisoIpoteca(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, AvvertenzePreavvisoIpoteca);

            if (v_testo != null)
            {
                string sImporto = p_avviso.importo_spese_coattive_sospese_su_preavvisi.ToString();
                v_testo = v_testo.Replace("#EURO_SPESE#", sImporto.Substring(0, sImporto.Length - 2));
            }
            return v_testo;
        }

        public static string GetTestoPignoramentoVSTerziTO(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoPignoramentoVSTerziTO);
            v_testo = v_testo.Replace("#TERZO#", p_avviso.TAB_SUPERVISIONE_FINALE_V2.Select(s => s.join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new.tab_terzo_debitore.tab_terzo.terzoNominativoDisplay_Stampa).FirstOrDefault());

            return v_testo;
        }

        public static string GetOggettoSospensioneFermo(tab_sped_not p_spednot)
        {
            string v_testo = string.Empty; //GetSezione(p_avviso, SezioneOggettoRevocaFermo);
            string v_notificato = string.Empty;
            string v_testo_preavv = string.Empty;

            int? id_d_o = p_spednot.id_doc_output;  // p_avviso.tab_sped_not.Where(w => w.id_doc_output == w.tab_doc_output.id_tab_doc_output).FirstOrDefault().id_doc_output;
            if (p_spednot.tab_avv_pag.data_ricezione_String.Trim().Length > 0)
            {
                v_notificato = " notificato il " + p_spednot.tab_avv_pag.data_ricezione_String;
            }
            string v_DescOggetto = "<html><body><font face= 'Calibri' size='2'><p align='justify'><b>Oggetto: " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.NumDoc +
                " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString() +
                " con riferimento a " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc + " acquisita con n. " +
                p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.NumDoc + " del " +
                p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String +
                " relativa a comunicazione di " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag;
            if (p_spednot.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO)
            {
                v_testo_preavv = " ed a " + p_spednot.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.FirstOrDefault().tab_avv_pag2.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                " notificato il " + p_spednot.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22.FirstOrDefault().tab_avv_pag2.data_ricezione_String + "</b></p></Font></Body></Html>";
            }
            else
            {
                v_testo_preavv = "</b></p></Font></Body></Html>";
            }

            v_testo = "<html><body><font face= 'Calibri' size='2'><p align='justify'>Si rilascia il Provvedimento " +
                "in oggetto con il quale si autorizza il proprietario dei veicoli sotto indicati a richiedere all'ufficio dell'ACI " +
                "competente territorialmente la sospensione delle iscrizioni di fermo amministrativo  di seguito riportate:</p></Font></Body></Html>";

            //string DescAvvisoPrincipale = "Riferimento: " + p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag.Trim();
            //DescAvvisoPrincipale = DescAvvisoPrincipale + " n." + p_istanza.tab_avv_pag.identificativo_avv_pag.Trim();

            //DescAvvisoPrincipale = DescAvvisoPrincipale + v_notificato;

            v_testo = v_DescOggetto + v_testo;
            // v_testo = v_testo.Replace(RIFERIMENTO_AVVISO_REVOCA_FERMO, DescAvvisoPrincipale);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";

        }

        public static string GetOggettoRevocaFermo(tab_sped_not p_spednot)
        {
            string v_testo = string.Empty; //GetSezione(p_avviso, SezioneOggettoRevocaFermo);
            string v_notificato = string.Empty;
            string v_testo_preavv = string.Empty;

            int? id_d_o = p_spednot.id_doc_output;  // p_avviso.tab_sped_not.Where(w => w.id_doc_output == w.tab_doc_output.id_tab_doc_output).FirstOrDefault().id_doc_output;
            if (p_spednot.tab_avv_pag.data_ricezione_String.Trim().Length > 0)
            {
                v_notificato = " notificato il " + p_spednot.tab_avv_pag.data_ricezione_String;
            }
            string v_DescOggetto = "<html><body><font face= 'Calibri' size='2'><p align='justify'><b>Oggetto: " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.NumDoc +
                " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString() +
                " con riferimento a " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc + " acquisita con n. " +
                p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.NumDoc + " del " +
                p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String +
                " relativa a comunicazione di " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag;
            if (p_spednot.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO)
            {
                v_testo_preavv = " ed a " + p_spednot.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V21.tab_avv_pag2.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                " notificato il " + p_spednot.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V21.tab_avv_pag2.data_ricezione_String + "</b></p></Font></Body></Html>";
            }
            else
            {
                v_testo_preavv = "</b></p></Font></Body></Html>";
            }

            v_testo = "<html><body><font face= 'Calibri' size='2'><p align='justify'>Si rilascia il Provvedimento " +
                "in oggetto con il quale si attesta che ai sensi del art.2 comma 7 del d.lgs. n.98/2017 " +
                "si procederà in modalità telematica alla cancellazione della iscrizione di fermo amministrativo " +
                "sui veicoli di seguito elencati.</p></Font></Body></Html>";

            //string DescAvvisoPrincipale = "Riferimento: " + p_istanza.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag.Trim();
            //DescAvvisoPrincipale = DescAvvisoPrincipale + " n." + p_istanza.tab_avv_pag.identificativo_avv_pag.Trim();

            //DescAvvisoPrincipale = DescAvvisoPrincipale + v_notificato;

            v_testo = v_DescOggetto + v_testo_preavv + v_testo;
            // v_testo = v_testo.Replace(RIFERIMENTO_AVVISO_REVOCA_FERMO, DescAvvisoPrincipale);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetTestoNoteRate(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, TestoNoteIstanza);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetTestoNoteRate(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoNoteRate);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetTestoMancatorecapito(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoNoteRate);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetOggettoLiberatoriaTerzo(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, SezioneOggettoLiberatoriaTerzo);
            string v_DescAvviso = p_istanza.tab_avv_pag.join_avv_pag_soggetto_debitore.FirstOrDefault().tab_terzo.terzoNominativoDisplay_Stampa;
            string v_DescRiferimento = "Riferimento: " + p_istanza.tab_doc_input.tab_tipo_doc_entrate.descr_doc.Trim();

            v_testo = v_testo.Replace(DESCRIZIONE_AVVISO_LIBERATORIA_TERZO, v_DescAvviso);
            v_testo = v_testo.Replace(RIFERIMENTO_LIBERATORIA_TERZO, v_DescRiferimento);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetOggettoLiberatoriaTerzo(tab_sped_not p_spednot)
        {
            string v_testo = GetSezione(p_spednot.tab_avv_pag, SezioneOggettoLiberatoriaTerzo);

            string v_DescOggetto = p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc;

            string v_stRif = "con riferimento a " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag
                + " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag.Trim();
            string v_tipo_VISTO = string.Empty;
            if (p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_LIBERATORIA_TERZO)
            {
                v_tipo_VISTO = "vista la documentazione acquisita attestante il pagamento a saldo dell’atto in oggetto";
            }
            else if (p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_ORDINE_TERZO)
            {
                v_tipo_VISTO = "vista la dichiarazione reesa dal Terzo Debitore";
            }
            string v_DescIstanza = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.identificativo_doc_input.Trim() +
                " del " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String;
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataConcessionarioSmall(p_spednot.tab_avv_pag);
            rtBox.Rtf = rtBox.Rtf.Replace("#ANAGRAFICA_ENTE#", p_spednot.tab_avv_pag.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace(DESCRIZIONE_AVVISO_LIBERATORIA_TERZO, v_DescOggetto);
            v_testo = v_testo.Replace("#RIF_AVVISO#", v_stRif);
            v_testo = v_testo.Replace("#DESC_ISTANZA#", v_DescIstanza);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            v_testo = v_testo.Replace("\r", "");
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", p_spednot.cognome_ragsoc_contribuente + " - Cod.Fiscale/P.Iva: " + p_spednot.codfis_piva_contribuente);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", p_spednot.cognome_ragsoc_terzo);
            v_testo = v_testo.Replace("#BARCODE_DOC_OUTPUT#", p_spednot.tab_doc_output.barcode);
            v_testo = v_testo.Replace("#DATA_PROVVEDIMENTO#", p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString());
            v_testo = v_testo.Replace("#VISTO#", v_tipo_VISTO);

            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        //Utilizzato anche per la liv
        public static string GetOggettoRinunciaEsecuzione(tab_sped_not p_sped_not)
        {
            string v_testo_iscrizione = "e tenuto conto che il pignoramento di cui all'oggetto risulta già iscritto nei ruoli esecutivi del Tribunale di #DESC_AUTORITA_GIUD#";
            string v_motivo_rinuncia = "vista la dichiarazione resa dal Terzo Debitore";
            v_testo_iscrizione = v_testo_iscrizione.Replace("#DESC_AUTORITA_GIUD#", p_sped_not.tab_avv_pag.tab_citazioni
                .Where(w => w.id_avv_pag_citazione == p_sped_not.tab_avv_pag.id_tab_avv_pag).Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault()); 
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, SezioneOggettoRinuncia);
            string v_DescOggetto = p_sped_not.tab_doc_output.tab_tipo_doc_entrate.descr_doc;
            string numero_provv = "Numero provvedimento: " + p_sped_not.tab_doc_output.barcode;
            string data_provv = " del " + p_sped_not.tab_doc_output.data_emissione_doc.Value.ToShortDateString();

            if (p_sped_not.tab_avv_pag.importoTotDaPagare == 0
                || p_sped_not.tab_doc_output.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_COMUNICAZIONE_RINUNCIA_ESECUZIONE_OUTPUT)
            {
                v_motivo_rinuncia = "visto il pagamento effettuato ";
            }
            string v_stRif = "con riferimento a " + p_sped_not.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim();

            string v_DescIstanza = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.identificativo_doc_input.Trim() +
                " del " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String;
            string v_testo_rinuncia_iscrizione = "all'azione ed agli atti esecutivi relativi all'Atto di pignoramento " +
                "in oggetto e si impegna a inoltrare al Tribunale Competente la richiesta di estinzione del pignoramento oggetto del presente Atto.";
            string v_testo_rinuncia_no_iscrizione = "all'azione ed agli atti esecutivi relativi all'Atto di pignoramento " +
                "in oggetto liberando il Debitore esecutato ed il Terzo Pignorato sopraccitati da ogni obbligo ed onere con riferimento all'Atto medesimo,"
                + " autorizzando lo svincolo delle somme pignorate.";

            string v_ai_sensi = "Ai sensi del combinato disposto di cui agli artt. 164 ter disp. att. al c.p.c. n.543 c.p.c., il creditore procedente dichiara "
            + " di non aver depositato la nota di iscrizione a ruolo e che la stessa non sarà depositata nei termini stabiliti dall'art. n.543 c.p.c.,"
            + " con conseguente perdita dell'efficacia del pignoramento ex art. n.543 comma 4 c.p.c..";

            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataConcessionarioSmall(p_sped_not.tab_avv_pag);
            rtBox.Rtf = rtBox.Rtf.Replace("#ANAGRAFICA_ENTE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#OGGETTO_RINUNCIA#", v_DescOggetto + " " + v_stRif);

            v_testo = v_testo.Replace("#DESC_ISTANZA#", v_DescIstanza);
            v_testo = v_testo.Replace("#MOTIVO_RINUNCIA#", v_motivo_rinuncia);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            v_testo = v_testo.Replace("\r", "");
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", p_sped_not.cognome_ragsoc_contribuente + " - Cod.Fiscale/P.Iva: " + p_sped_not.codfis_piva_contribuente);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", p_sped_not.cognome_ragsoc_terzo + " - Cod.Fiscale/P.Iva: " + p_sped_not.codfis_piva_terzo);
            bool v_iscritta_a_ruolo = p_sped_not.tab_avv_pag.tab_citazioni
                .Where(w => w.id_avv_pag_citazione == p_sped_not.tab_avv_pag.id_tab_avv_pag && w.flag_iscrizione_ruolo == "1").Any();
            if (!v_iscritta_a_ruolo)
            {
                v_testo = v_testo.Replace("#TESTO_RINUNCIA#", v_testo_rinuncia_no_iscrizione);
                v_testo = v_testo.Replace("#AI_SENSI#", v_ai_sensi);
                v_testo = v_testo.Replace("#TESTO_ISCRIZIONE_RUOLO#", "");
            }
            else
            {
                v_testo = v_testo.Replace("#TESTO_RINUNCIA#", v_testo_rinuncia_iscrizione);
                v_testo = v_testo.Replace("#TESTO_ISCRIZIONE_RUOLO#", v_testo_iscrizione);
                v_testo = v_testo.Replace("#AI_SENSI#", "");
            }
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetOggettoLiberatoriaSuCitazione(tab_sped_not p_sped_not)
        {
            string v_testo_iscrizione = " e tenuto conto che il pignoramento di cui all'oggetto risulta già iscritto nei ruoli esecutivi del Tribunale di #DESC_AUTORITA_GIUD#   ";
            v_testo_iscrizione = v_testo_iscrizione.Replace("#DESC_AUTORITA_GIUD#", p_sped_not.tab_avv_pag.tab_citazioni
                .Where(w => w.id_avv_pag_citazione == p_sped_not.tab_avv_pag.id_tab_avv_pag).Select(s => s.tab_autorita_giudiziaria.descrizione_autorita).FirstOrDefault()); ;
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, SezioneOggettoRinuncia);
            string v_DescOggetto = p_sped_not.tab_doc_output.tab_tipo_doc_entrate.descr_doc;
            string numero_provv = "Numero provvedimento: " + p_sped_not.tab_doc_output.barcode;
            string data_provv = " del " + p_sped_not.tab_doc_output.data_emissione_doc.Value.ToShortDateString();

            string v_stRif = "con riferimento a " + p_sped_not.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim();

            string v_DescIstanza = p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.identificativo_doc_input.Trim() +
                " del " + p_sped_not.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String;
            string v_testo_rinuncia_iscrizione = "all'azione ed agli atti esecutivi relativi all'Atto di pignoramento " +
                "in oggetto e si impegna a inoltrare al Tribunale Competente la richiesta di estinzione del pignoramento oggetto del presente Atto.";
            string v_testo_rinuncia_no_iscrizione = "all'azione ed agli atti esecutivi relativi all'Atto di pignoramento " +
                "in oggetto liberando il Debitore Esecutato ed il Terzo Pignorato sopraccitati da ogni obbligo ed onere con riferimento all'Atto medesimo.";
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataConcessionarioSmall(p_sped_not.tab_avv_pag);
            rtBox.Rtf = rtBox.Rtf.Replace("#ANAGRAFICA_ENTE#", p_sped_not.tab_avv_pag.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#OGGETTO_LIBERATORIA#", v_DescOggetto + " " + v_stRif);
            v_testo = v_testo.Replace("#RIF_AVVISO#", v_stRif);
            v_testo = v_testo.Replace("#DESC_ISTANZA#", v_DescIstanza);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            v_testo = v_testo.Replace("\r", "");
            v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", p_sped_not.cognome_ragsoc_contribuente + " - Cod.Fiscale/P.Iva: " + p_sped_not.codfis_piva_contribuente);
            v_testo = v_testo.Replace("#TERZO_DEBITORE#", p_sped_not.cognome_ragsoc_terzo + p_sped_not.codfis_piva_terzo);


            bool v_iscritta_a_ruolo = p_sped_not.tab_avv_pag.tab_citazioni
                .Where(w => w.id_avv_pag_citazione == p_sped_not.tab_avv_pag.id_tab_avv_pag).Select(s => s.flag_iscrizione_ruolo == "1").Any();
            if (!v_iscritta_a_ruolo)
            {
                v_testo = v_testo.Replace("#TESTO_RINUNCIA#", v_testo_rinuncia_no_iscrizione);
                v_testo = v_testo.Replace("#TESTO_ISCRIZIONE_RUOLO#", "");
            }
            else
            {
                v_testo = v_testo.Replace("#TESTO_RINUNCIA#", v_testo_rinuncia_iscrizione);
                v_testo = v_testo.Replace("#TESTO_ISCRIZIONE_RUOLO#", v_testo_iscrizione);
            }
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetOggettoRimborso(tab_sped_not p_spednot)
        {
            string v_testo = GetSezione(p_spednot.tab_avv_pag, "TestoOggettoRimborsi");

            string v_DescOggetto = p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc;
            string numero_provv = "Numero provvedimento: " + p_spednot.tab_doc_output.barcode;
            string data_provv = " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString();
            string v_stRif = p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag.Trim();

            string v_DescIstanza = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.identificativo_doc_input.Trim() +
                " del " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String;
            //System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            //rtBox.Rtf = GetTestataConcessionarioSmall(p_spednot.tab_avv_pag);
            //v_DescOggetto = v_DescOggetto + v_DescIstanza;
            string stEsito = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.join_tab_avv_pag_tab_doc_input.FirstOrDefault().cod_stato;
            string stEsitoDesc = string.Empty;
            string stComunicazione = string.Empty;
            string stTipobonifico = string.Empty;
            string riga1 = "";
            string riga2 = "";
            string riga3 = "";
            string riga4 = "";
            string sParolaChiave = "";
            string stAvvertenze = string.Empty;
            if (stEsito == join_tab_avv_pag_tab_doc_input.DEF_ACC || stEsito == join_tab_avv_pag_tab_doc_input.DEF_PAG)
            {
                stEsito = " ACCOLTA ";
                //stEsitoDesc = " è stato ANNULLATO IN AUTOTUTELA ";
                stComunicazione = "Si Comunica pertanto che è stata emessa la Disposizione di Bonifico i cui riferimenti sono di seguito riportati : " + Environment.NewLine;
                stTipobonifico = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().tipo_bonifico == "D" ? "Tipo Bonifico : Domiciliato" : "Tipo Bonifico : Bonifico Ordinario";
                riga1 = "Data disposizione bonifico: " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().data_creazione_disposizione_rimborso.Value.ToShortDateString() + Environment.NewLine;
                riga2 = "Importo bonificato: € " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().importo.ArrotondaA2Decimali() + Environment.NewLine;
                riga3 = "Beneficiario: " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().join_tab_avv_pag_tab_doc_input.join_doc_input_pag_avvpag.FirstOrDefault().nominativo_rag_soc_beneficiario + Environment.NewLine;
                riga4 = "Cod Fiscale / P.Iva: " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().join_tab_avv_pag_tab_doc_input.join_doc_input_pag_avvpag.FirstOrDefault().codice_fiscale_piva_beneficiario + Environment.NewLine;

                if (p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().tipo_bonifico == "D")
                {
                    sParolaChiave = "Parola chiave per la riscossione del rimborso: " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().parola_chiave_bonifico_domiciliato;
                    stAvvertenze = "AVVERTENZA : Lei potra' riscuotere l'importo bonificato presso qualsiasi ufficio postale, a decorrere da " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().data_pagabilita_disposizione_rimborso.Value.ToShortDateString()
                        + " e fino all'ultimo giorno lavorativo del mese successivo, utilizzando la parola chiave sopra indicata ed esibendo un documento di riconoscimento in corso di validità";
                    v_testo = v_testo.Replace("#PAROLA_CHIAVE#", sParolaChiave);
                }
                else
                {
                    stAvvertenze = "IBAN su cui è accreditato l'importo bonificato: " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_rimborsi.Where(d => d.cod_stato.StartsWith(tab_rimborsi.VAL_DIS)).FirstOrDefault().join_tab_avv_pag_tab_doc_input.join_doc_input_pag_avvpag.FirstOrDefault().iban_beneficiario_bonifico;
                    v_testo = v_testo.Replace("#PAROLA_CHIAVE#", "");
                }

            }
            else if (stEsito == join_tab_avv_pag_tab_doc_input.DEF_RES)
            {
                stEsito = " RESPINTA ";
                stEsitoDesc = "";
                v_testo = v_testo.Replace("#PAROLA_CHIAVE#", "");
                //stEsitoDesc = " resta a tutti gli effetti VALIDO ed il relativo pagamento deve essere effettuato improrogabilmente entro 30 giorni dalla ricezione della presente comunicazione ";
            }
            v_testo = v_testo.Replace("#NUM_PROVVEDIMENTO#", numero_provv);
            v_testo = v_testo.Replace("#DATA_PROVVEDIMENTO#", data_provv);
            v_testo = v_testo.Replace("#SI_COMUNICA#", stComunicazione);
            v_testo = v_testo.Replace("#TIPO_BONIFICO#", stTipobonifico);
            v_testo = v_testo.Replace("#DATA_DISPOSIZIONE#", riga1);
            v_testo = v_testo.Replace("#IMPORTO#", riga2);
            v_testo = v_testo.Replace("#BENEFICIARIO#", riga3);
            v_testo = v_testo.Replace("#COD_FISCALE#", riga4);
            v_testo = v_testo.Replace("#PAROLA_CHIAVE#", sParolaChiave);
            v_testo = v_testo.Replace("#AVVERTENZE#", stAvvertenze);
            v_testo = v_testo.Replace("#OGGETTO_RIMBORSI#", v_DescOggetto);
            v_testo = v_testo.Replace("#RIF_AVVISO#", v_stRif);
            v_testo = v_testo.Replace("#DESC_ISTANZA#", v_DescIstanza);
            v_testo = v_testo.Replace("#ESITO_RIMBORSI#", stEsito);
            v_testo = v_testo.Replace("#NUM_ISTANZA#", p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.identificativo_doc_input.Trim());
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetTestoEsitoAnnAutotutela(tab_sped_not p_spednot, dbEnte p_dbContext)
        {
            string v_testo = GetSezione(p_spednot.tab_avv_pag, SezioneTestoAnnullamentoInAutotutela);

            string v_DescOggetto = p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc + " n. " + p_spednot.tab_doc_output.barcode +
                 " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString();

            //string data_provv = " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString();

            string v_stRif = p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag.Trim();

            string v_opt_string = "";// L'atto sopramenzionato è stato annullato in autotutela in seguito allo Sgravio di un Atto presupposto comunicato dall'Ente Creditore sopra emarginato. ";
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataConcessionarioSmall(p_spednot.tab_avv_pag);
            rtBox.Rtf = rtBox.Rtf.Replace("#ANAGRAFICA_ENTE#", p_spednot.tab_avv_pag.anagrafica_ente.descrizione_ente);
            v_testo = v_testo.Replace("#DESC_OGGETTO#", v_DescOggetto + ".");
            v_testo = v_testo.Replace("#DESCRIZIONE_ATTO#", v_stRif);
            string v_identificativo = string.Empty;
            bool v_isPagoPA = false;
            if (string.IsNullOrEmpty(p_spednot.tab_avv_pag.tab_rata_avv_pag.Where(w => w.cod_stato != tab_rata_avv_pag.ANN_ANN).FirstOrDefault().codice_pagamento_pagopa))
            {
                v_identificativo = p_spednot.tab_avv_pag.tab_rata_avv_pag.FirstOrDefault().Iuv_identificativo_pagamento;
            }
            else
            {
                v_identificativo = p_spednot.tab_avv_pag.tab_rata_avv_pag.Where(w => w.cod_stato != tab_rata_avv_pag.ANN_ANN).FirstOrDefault().codice_pagamento_pagopa;
                v_isPagoPA = true;
            }

            if (p_spednot.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.VALIDO_RET
                || p_spednot.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.VALIDO_RSG)
            {
                string v_num_cc = string.Empty;
                string v_IBAN = string.Empty;
                tab_modalita_rate_avvpag v_tm = TabModalitaRateAvvPagBD
                .GetByIdTipoAvvPagAndIdEnte(p_spednot.tab_avv_pag.id_tipo_avvpag, p_spednot.tab_avv_pag.id_ente, p_dbContext);
                if (v_tm != null)
                {
                    v_num_cc = v_tm.tab_cc_riscossione.num_cc_new;
                    v_IBAN = v_tm.tab_cc_riscossione.IBAN;
                }

                if (v_isPagoPA)
                {

                    v_opt_string = "L'importo del suddetto Atto è stato pertanto rideterminato in euro "
                   + p_spednot.tab_avv_pag.imp_tot_avvpag_rid.Value.ArrotondaA2Decimali()
                   + ";  Il suddetto importo dovrà essere pagato sul conto corrente n. " + v_num_cc + " indicando il seguente codice di pagamento PagoPA: "
                   + v_identificativo + "; il pagamento può essere effettuato anche accedendo al portale Fiscolocale.it,";
                    v_testo = v_testo.Replace("#IMP_RIDETERMINATO#", v_opt_string + ".");
                }
                else
                {
                    v_opt_string = "L'importo del suddetto Atto è stato pertanto rideterminato in euro " + p_spednot.tab_avv_pag.imp_tot_avvpag_rid.ToString()
                            + "; " + Environment.NewLine + "Il suddetto importo dovrà essere pagato sul conto corrente n. " + v_num_cc + " indicando il seguente codice IUV : "
                            + v_identificativo + "; il pagamento può essere effettuato con bollettino postale 123 oppure con bonifico bancario" +
                            " indicando il seguente IBAN: " + v_IBAN + ".";
                    v_testo = v_testo.Replace("#IMP_RIDETERMINATO#", v_opt_string + ".");
                }
                //string v_identificativo = p_spednot.tab_avv_pag.tab_rata_avv_pag.FirstOrDefault().codice_pagamento_pagopa ?? p_spednot.tab_avv_pag.tab_rata_avv_pag.FirstOrDefault().Iuv_identificativo_pagamento;
            }
            else
            {
                v_testo = v_testo.Replace("#IMP_RIDETERMINATO#", "");
            }

            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            v_testo = v_testo.Replace("#DESC_STATO#", p_spednot.tab_avv_pag.anagrafica_stato_avv_pag.desc_stato_riferimento + ".");
            //v_testo = v_testo.Replace("\r", "");
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetOggettoEsitoIstanzaAnnRet(tab_sped_not p_spednot, dbEnte p_dbContext)
        {
            string v_testo = string.Empty;// GetSezione(p_avviso, SezioneOggettoEsitoIstanze);
            int? id_d_o = p_spednot.id_doc_output;
            int? id_doc_input = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output
                .Where(w => w.id_tab_doc_output == id_d_o).FirstOrDefault().id_tab_doc_input;
            string v_DescOggetto = "<html><body><font face= 'Calibri' size='2'><div><b>Oggetto: " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n." + p_spednot.tab_doc_output.NumDoc +
                " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString() + "</div><br>";
            string sEsito = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.cod_stato;
            string v_stRif = string.Empty;
            string sEsitoRif = string.Empty;
            string v_riga = string.Empty;
            if (sEsito == anagrafica_stato_doc.STATO_ESITATA_ACCOLTA)
            {
                sEsito = "Accolta";
                sEsitoRif = "e conseguentemente è stato #ANNULLATO_IN_AUTOTUTELA# l'Atto " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " +
                    p_spednot.tab_avv_pag.identificativo_avv_pag + ".";
                sEsitoRif = sEsitoRif.Replace("#ANNULLATO_IN_AUTOTUTELA#", p_spednot.tab_avv_pag.anagrafica_stato_avv_pag.desc_stato_riferimento);
                List<tab_avv_pag> v_tab_avv_pag = p_spednot.tab_avv_pag.join_tab_avv_pag_tab_doc_input.Where(u => u.cod_stato != anagrafica_stato_doc.STATO_ANNULLATO
                 && u.id_avv_pag_collegato != u.id_avv_pag && u.id_avv_pag_collegato != null
                 && u.id_tab_doc_input == id_doc_input)
                  .Select(s => s.tab_avv_pag1).ToList();
                v_stRif = "</b><div align='justify'>Con la presente Le comunichiamo che la " +
                    p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc + " n. " +
                p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.NumDoc
                + " presentata con riferimento a " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                 " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag + " è stata " + sEsito + " " + sEsitoRif;

                if (p_spednot.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.VALIDO_RET
                    || p_spednot.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.VALIDO_RSG)
                {
                    string v_identificativo = string.Empty;
                    bool v_isPagoPA = false;
                    if (string.IsNullOrEmpty(p_spednot.tab_avv_pag.tab_rata_avv_pag.Where(w => w.cod_stato != tab_rata_avv_pag.ANN_ANN).FirstOrDefault().codice_pagamento_pagopa))
                    {
                        v_identificativo = p_spednot.tab_avv_pag.tab_rata_avv_pag.FirstOrDefault().Iuv_identificativo_pagamento;
                    }
                    else
                    {
                        v_identificativo = p_spednot.tab_avv_pag.tab_rata_avv_pag.Where(w => w.cod_stato != tab_rata_avv_pag.ANN_ANN).FirstOrDefault().codice_pagamento_pagopa;
                        v_isPagoPA = true;
                    }

                    string v_num_cc = string.Empty;
                    string v_IBAN = string.Empty;
                    tab_modalita_rate_avvpag v_tm = TabModalitaRateAvvPagBD
                    .GetByIdTipoAvvPagAndIdEnte(p_spednot.tab_avv_pag.id_tipo_avvpag, p_spednot.tab_avv_pag.id_ente, p_dbContext);
                    if (v_tm != null)
                    {
                        v_num_cc = v_tm.tab_cc_riscossione.num_cc_new;
                        v_IBAN = v_tm.tab_cc_riscossione.IBAN;
                    }

                    if (v_isPagoPA)
                    {
                        v_riga = "<br>L'importo del suddetto Atto è stato pertanto rideterminato in euro " + p_spednot.tab_avv_pag.imp_tot_avvpag_rid.Value.ArrotondaA2Decimali().ToString()
                            + "; " + Environment.NewLine + "Il suddetto importo dovrà essere pagato sul conto corrente n. " + v_num_cc + " indicando il seguente codice di pagamento PagoPA : "
                        + v_identificativo + "; il pagamento può essere effettuato anche accedendo al portale Fiscolocale.it,";
                    }
                    else
                    {
                        v_riga = "<br>L'importo del suddetto Atto è stato pertanto rideterminato in euro " + p_spednot.tab_avv_pag.imp_tot_avvpag_rid.Value.ArrotondaA2Decimali().ToString()
                                + "; " + Environment.NewLine + "Il suddetto importo dovrà essere pagato sul conto corrente n. " + v_num_cc + " indicando il seguente codice IUV : "
                                + v_identificativo + "; il pagamento può essere effettuato con bollettino postale 123 oppure con bonifico bancario" +
                                " indicando il seguente IBAN: " + v_IBAN + ".";

                    }

                    v_stRif = v_stRif + v_riga;
                }
                if (v_tab_avv_pag.Count() > 0)
                {
                    v_stRif = v_stRif + "<br>Il Provvedimento in oggetto è stato adottato per effetto dei seguenti Provvedimenti assunti relativamente ad Atti presupposti all'Atto di riferimento della Istanza:";
                }
            }
            else if (sEsito == anagrafica_stato_doc.STATO_ESITATA_RESPINTA)
            {
                sEsito = "Respinta";
                sEsitoRif = "e conseguentemente l'Atto cui essa è riferita resta a tutti gli effetti VALIDO ed il relativo pagamento deve essere regolarmente effettuato.";
                v_stRif = "</b><div align='justify'>Gent.mo Contribuente con la presente siamo spiacenti di comunicarLe che la " +
                    p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc + " n." +
                p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.NumDoc.Trim() +
                    " presentata con riferimento a " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                 " n." + p_spednot.tab_avv_pag.identificativo_avv_pag.Trim() + " è stata " + sEsito + " " + sEsitoRif;

            }
            v_testo = v_DescOggetto + v_stRif + "</p></Font></Body></Html>";

            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetOggettoAccoglimentoMediazione(tab_sped_not p_spednot)
        {
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            string v_testo = string.Empty;// GetSezione(p_avviso, SezioneOggettoEsitoIstanze);
            string v_descrizione = string.Empty;
            int? id_d_o = p_spednot.id_doc_output;
            string v_rgr = string.Empty;
            tab_ricorsi v_tab_ricorsi = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_ricorsi.FirstOrDefault();
            string v_DescOggetto = "<html><body><font face= 'Times New Roman' size='3'><div><b>Oggetto: " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n.  " + p_spednot.tab_doc_output.NumDoc +
                " con riferimento a ";

            string v_stRif = "<div><br><br>In seguito alla approfondita disamina effettuata in merito alle motivazioni esposte nel "
                + "Ricorso in oggetto, notificato in data " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String
                + " " + " #PRESENTATORE_RICORSO#, è stato adottato il " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.barcode
                + " con il quale si è proceduto ad annullare in autotutela i seguenti atti :<br></br>";
            if (v_tab_ricorsi.rgr != null)
            {
                v_rgr = " rg n. " + v_tab_ricorsi.rgr;
            }
            if (p_spednot.tab_doc_output.tab_tipo_doc_entrate.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ANNULLAMENTO_RETTIFICA_AUTOTUTELA_SU_RICORSO)
            {
                v_descrizione = "<br><b>Avviso di riferimento del ricorso</b></br>";
            }
            else
            {
                v_descrizione = "<br><b>Avviso di riferimento del ricorso/reclamo con mediazione:</b></br>";
            }
            v_stRif = v_stRif + v_descrizione;
            string v_DescIstanza = "ricorso"/*p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc*/ +
                " presso " + v_tab_ricorsi.tab_autorita_giudiziaria.descrizione_autorita + v_rgr + "</b></div><br>";
            //" del " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String + "</div><br>";

            string v_presentatore = string.Empty;
            string v_ricorrente = string.Empty;
            if (v_tab_ricorsi.nominativo_ricorrente != null)
            {
                v_ricorrente = v_tab_ricorsi.nominativo_ricorrente;
            }
            else
            {
                if (v_tab_ricorsi.flag_individuazione_ricorrente == tab_ricorsi.CONTRIBUENTE)
                {
                    if (v_tab_ricorsi.tab_contribuente != null)
                    {
                        v_ricorrente = v_tab_ricorsi.tab_contribuente.nominativoDisplay;
                    }
                    else
                    {
                        v_ricorrente = p_spednot.tab_doc_output
                                                .join_tab_doc_input_tab_doc_output.FirstOrDefault()
                                                .tab_doc_input
                                                .join_tab_avv_pag_tab_doc_input.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO)).FirstOrDefault()
                                                .tab_avv_pag
                                                .tab_contribuente.nominativoDisplay + " ed altri";
                    }
                }
                else if (v_tab_ricorsi.flag_individuazione_ricorrente == tab_ricorsi.REFERENTE)
                {
                    v_ricorrente = v_tab_ricorsi.tab_referente.nominativoDisplay;
                }
            }
            v_ricorrente = myTI.ToTitleCase(v_ricorrente.ToLower());
            if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso != null && v_tab_ricorsi.pec_avvocato_ricorrente != null)
            {
                v_presentatore = "dall'Avvocato " + myTI.ToTitleCase(v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso.ToLower()) + Environment.NewLine
                    + "in nome e per conto di " + v_ricorrente;
            }
            else if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso == null || v_tab_ricorsi.pec_avvocato_ricorrente == null)
            {
                v_presentatore = " da " + v_ricorrente + Environment.NewLine;
            }

            v_testo = v_DescOggetto + v_DescIstanza + v_stRif;

            v_testo = v_testo.Replace("#PRESENTATORE_RICORSO#", v_presentatore);
            v_testo = v_testo.Replace("#AVV_RIFERIMENTO#", v_descrizione);
            v_testo = v_testo + "</div></Font></Body></Html>";
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetOggettoAccoglimentoMediazioneAnteprima(tab_sped_not p_spednot)
        {
            string v_testo = string.Empty;// GetSezione(p_avviso, SezioneOggettoEsitoIstanze);
            int? id_d_o = p_spednot.id_doc_output;
            string v_rgr = string.Empty;
            tab_ricorsi v_tab_ricorsi = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_ricorsi.FirstOrDefault();
            string v_DescOggetto = "<html><body><font face= 'Times New Roman' size='3'><div><b>Oggetto: " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. ---" +
                " con riferimento a ";

            string v_stRif = "<div><br><br>In seguito alla approfondita disamina effettuata in merito alle motivazioni esposte nel "
                + "Ricorso in oggetto, notificato in data " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String
                + " " + " #PRESENTATORE_RICORSO#, è stato adottato il " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.barcode
                + " con il quale si è proceduto ad annullare in autotutela i seguenti atti :";
            if (v_tab_ricorsi.rgr != null)
            {
                v_rgr = " rg n. " + v_tab_ricorsi.rgr;
            }
            string v_DescIstanza = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc +
                " presso " + v_tab_ricorsi.tab_autorita_giudiziaria.descrizione_autorita + v_rgr + "</div><br>";
            //" del " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String + "</div><br>";

            string v_presentatore = string.Empty;
            string v_ricorrente = string.Empty;
            if (v_tab_ricorsi.nominativo_ricorrente != null)
            {
                v_ricorrente = v_tab_ricorsi.nominativo_ricorrente;
            }
            else
            {
                if (v_tab_ricorsi.flag_individuazione_ricorrente == tab_ricorsi.CONTRIBUENTE)
                {
                    v_ricorrente = v_tab_ricorsi.tab_contribuente.nominativoDisplay;
                }
                else if (v_tab_ricorsi.flag_individuazione_ricorrente == tab_ricorsi.REFERENTE)
                {
                    v_ricorrente = v_tab_ricorsi.tab_referente.nominativoDisplay;
                }
            }
            if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso != null && v_tab_ricorsi.pec_avvocato_ricorrente != null)
            {
                v_presentatore = "dall'Avvocato " + v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso + Environment.NewLine
                    + "in nome e per conto di " + v_ricorrente;
            }
            else if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso == null || v_tab_ricorsi.pec_avvocato_ricorrente == null)
            {
                v_presentatore = " da " + v_ricorrente + Environment.NewLine;
            }
            //if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso != null && v_tab_ricorsi.pec_avvocato_ricorrente != null)
            //{
            //    v_presentatore = "dall'Avvocato " + v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso + Environment.NewLine
            //        + "in nome e per conto di " + v_tab_ricorsi.nominativo_ricorrente;
            //}
            //else if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso == null || v_tab_ricorsi.pec_avvocato_ricorrente == null)
            //{
            //    v_presentatore = " da " + v_tab_ricorsi.nominativo_ricorrente + Environment.NewLine;
            //}
            v_testo = v_DescOggetto + v_DescIstanza + v_stRif;
            //System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            //rtBox.Rtf = GetTestataConcessionarioSmall(p_spednot.tab_avv_pag);
            //rtBox.Rtf = rtBox.Rtf.Replace("#ANAGRAFICA_ENTE#", p_spednot.tab_avv_pag.anagrafica_ente.descrizione_ente);
            //v_testo = v_testo.Replace("#PRESENTATORE_RICORSO#", v_DescOggetto);
            //v_testo = v_testo.Replace(DESCRIZIONE_AVVISO_LIBERATORIA_TERZO, v_DescOggetto);
            //v_testo = v_testo.Replace("#RIF_AVVISO#", v_stRif);
            //v_testo = v_testo.Replace("#DESC_ISTANZA#", v_DescIstanza);
            //v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            //v_testo = v_testo.Replace("\r", "");
            //v_testo = v_testo.Replace("#SOGGETTO_DEBITORE#", p_spednot.cognome_ragsoc_contribuente + " - Cod.Fiscale/P.Iva: " + p_spednot.codfis_piva_contribuente);
            //v_testo = v_testo.Replace("#TERZO_DEBITORE#", p_spednot.cognome_ragsoc_terzo);
            //v_testo = v_testo.Replace("#BARCODE_DOC_OUTPUT# ", p_spednot.tab_doc_output.barcode);
            v_testo = v_testo.Replace("#PRESENTATORE_RICORSO#", v_presentatore);
            v_testo = v_testo + "</div></Font></Body></Html>";
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetOggettoEsitoMediazione(tab_sped_not p_spednot)
        {
            string v_testo = string.Empty;// GetSezione(p_avviso, SezioneOggettoEsitoIstanze);
            int? id_d_o = p_spednot.id_doc_output;
            string v_DescOggetto = "<html><body><font face= 'Calibri' size='2'><div><b>Oggetto: " + p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.NumDoc +
                " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString() + "</div><br>";
            string sEsito = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.cod_stato;
            string v_stRif = string.Empty;
            string sEsitoRif = string.Empty;
            if (sEsito == anagrafica_stato_doc.STATO_ESITATA_ACCOLTA)
            {
                sEsito = "Accolta";
                sEsitoRif = "e conseguentemente è stato ANNULLATO IN AUTOTUTELA l' Atto " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " +
                    p_spednot.tab_avv_pag.identificativo_avv_pag;
                List<tab_avv_pag> v_tab_avv_pag = p_spednot.tab_avv_pag.join_tab_avv_pag_tab_doc_input.Where(u => u.cod_stato != anagrafica_stato_doc.STATO_ANNULLATO
                 && u.id_avv_pag_collegato != p_spednot.tab_avv_pag.id_tab_avv_pag && u.id_avv_pag_collegato != null)
                  .Select(s => s.tab_avv_pag1).ToList();
                v_stRif = "</b><div align='justify'>Con la presente si comunica, " +
                    "con riferimento a quanto in oggetto, è stato adottato in data " +
                    //p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.data_presentazione_String +
                    p_spednot.tab_doc_output.data_emissione_doc_String;
                //    p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc + " n. " +
                //p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.NumDoc
                //+ " presentata con riferimento a " + p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag +
                // " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag + " è stata " + sEsito + " " + sEsitoRif;

                if (v_tab_avv_pag.Count() > 0)
                {
                    v_stRif = v_stRif + "<br>Si comunica altresì l'adozione dei seguenti Provvedimenti assunti relativamente ad Atti presupposti all'Atto di riferimento della Istanza.";
                }
            }



            //
            v_testo = v_DescOggetto + v_stRif + "</p></Font></Body></Html>";
            if (sEsito == anagrafica_stato_doc.STATO_ESITATA_ACCOLTA)
            {
                if (p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == 8 || p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == 9)
                {
                    v_testo = v_testo + GetTestoEsitoIstanzaAnnRet(p_spednot.tab_avv_pag);
                }

            }
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetOggettoAnnullamentoIstanzaRateizzazione(tab_sped_not p_spednot)
        {
            string v_testo = string.Empty;// GetSezione(p_avviso, SezioneOggettoEsitoIstanze);
            int? id_d_o = p_spednot.id_doc_output;
            string v_rif_istanza = string.Empty;
            //tab_doc_output tdo = TabDocOutputBD.GetById(id_d_o, p_dbContext);

            if (p_spednot.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA)
            {
                v_rif_istanza = p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.tab_tipo_doc_entrate.descr_doc + " n. " + p_spednot.tab_doc_output.join_tab_doc_input_tab_doc_output.FirstOrDefault().tab_doc_input.NumDoc;
            }
            else
            {
                v_rif_istanza = p_spednot.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + p_spednot.tab_avv_pag.identificativo_avv_pag;
            }
            string v_DescOggetto = p_spednot.tab_doc_output.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_spednot.tab_doc_output.NumDoc +
                " del " + p_spednot.tab_doc_output.data_emissione_doc.Value.ToShortDateString();
            //creare la sezione del testo
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            //rtBox.Rtf = GetSezione(p_spednot.tab_avv_pag, "TestoAnnullamentoRate");
            v_testo = GetSezione(p_spednot.tab_avv_pag, "TestoAnnullamentoRate");
            v_testo = v_testo.Replace("#DESCRIZIONE_ISTANZA#", v_rif_istanza);
            v_testo = v_testo.Replace("#DESCRIZIONE_AVVISO#", v_DescOggetto);
            return v_testo;
        }

        public static string GetTestoEsitoIstanzaAnnRet(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoEsitoIstanzaAnnRet);
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataConcessionarioSmall(p_avviso);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetTestoLegendaTari(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoLegendaTari");
            return v_testo;
        }
        public static string GetTestoSollecitoRateScadute(tab_sped_not p_sped_not)
        {
            string v_testo = GetSezione(p_sped_not.tab_avv_pag, "TestoSollecitoRateScadute");
            string v_temp = string.Empty;
            v_testo = v_testo.Replace("#DESC_DOC_IDENTIFICATIVO#", p_sped_not.tab_doc_output.tab_tipo_doc_entrate.descr_doc
                                     + " n. " + p_sped_not.tab_doc_output.NumDoc
                                     + " del " + p_sped_not.tab_doc_output.data_emissione_doc.Value.ToShortDateString());
            if (p_sped_not.tab_avv_pag.anagrafica_tipo_avv_pag.anagrafica_tipo_servizi.id_tipo_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA)
            {
                v_temp = " Atto di rateizzazione n. " + p_sped_not.tab_avv_pag.identificativo_avv_pag;
            }
            else if (p_sped_not.tab_avv_pag.anagrafica_tipo_avv_pag.anagrafica_tipo_servizi.id_tipo_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA
                    && p_sped_not.tab_avv_pag.anagrafica_tipo_avv_pag.anagrafica_tipo_servizi.id_tipo_servizio == anagrafica_tipo_servizi.SERVIZI_DEFINIZIONE_AGEVOLATA_COA)
            {
                v_temp = " rateizzazione dell'Atto n. " + p_sped_not.tab_avv_pag.identificativo_avv_pag.Trim();
            }
            if (v_temp.Length > 0)
            {
                v_testo = v_testo.Replace("#ATTO_RATEIZZAZIONE#", v_temp);
            }
            v_testo = v_testo.Replace("#NUM_RATE#", p_sped_not.tab_avv_pag.num_rate.ToString());
            v_testo = v_testo.Replace("#IMP_TOT_RID#", p_sped_not.tab_avv_pag.imp_tot_avvpag_rid.Value.ToString("C2"));
            v_testo = v_testo.Replace("#DATA_CREAZIONE#", p_sped_not.tab_doc_output.data_emissione_doc.Value.ToShortDateString());

            switch (p_sped_not.tab_avv_pag.anagrafica_tipo_avv_pag.anagrafica_servizi.id_tipo_servizio)
            {
                case anagrafica_tipo_servizi.GEST_ORDINARIA:
                    v_testo = v_testo.Replace("#TIPO_ATTI_SUCCESSIVI#", "emissione del conseguente Atto di accertamento con addebito di interessi e sanzioni e di eventuali oneri e spese.");
                    break;
                case anagrafica_tipo_servizi.RISC_PRECOA:
                case anagrafica_tipo_servizi.ACCERTAMENTO:
                    v_testo = v_testo.Replace("#TIPO_ATTI_SUCCESSIVI#", "emissione dell'Atto di Ingiunzione Fiscale con addebito degli ulteriori interessi e di eventuali oneri e spese.");
                    break;
                case anagrafica_tipo_servizi.SOLL_PRECOA:
                case anagrafica_tipo_servizi.ING_FISC:
                case anagrafica_tipo_servizi.INTIM:
                    v_testo = v_testo.Replace("#TIPO_ATTI_SUCCESSIVI#", "emissione dei successivi Atti cautelari e/o Atti di pignoramento   con addebito degli ulteriori interessi e di eventuali oneri e spese.");
                    break;
                case anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI:
                    v_testo = v_testo.Replace("#TIPO_ATTI_SUCCESSIVI#", "iscrizione dei Fermo Amministrativo sul veicolo già indicato nel preavviso di fermo oggetto di rateizzazione con addebito degli ulteriori oneri e spese.");
                    break;
                //case anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA:

                //    v_testo = v_testo.Replace("#TIPO_ATTI_SUCCESSIVI#", "con la iscrizione dei Fermo Amministrativo sul veicolo già indicato nel preavviso di fermo oggetto di rateizzazione con addebito degli ulteriori oneri e spese.");
                //    break;
                default:
                    v_testo = v_testo.Replace("#TIPO_ATTI_SUCCESSIVI#", "emissione dei successivi Atti cautelari e/o Atti di pignoramento con addebito degli ulteriori interessi e di eventuali oneri e spese.");
                    break;
            }


            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetTestoEsitoMediazione(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, TestoEsitoIstanzaAnnRet);
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = GetTestataConcessionarioSmall(p_avviso);
            v_testo = v_testo.Replace("#ANAGRAFICA_ENTE#", rtBox.Text);
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetLeggeRevocaFermo(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneLeggeRevocaFermo);

            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }

        public static string GetTestoNoteIstanza(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, TestoNoteIstanza);

            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetTestoNoteIstanzaAnnRet(join_tab_avv_pag_tab_doc_input p_istanza)
        {
            string v_testo = GetSezione(p_istanza, TestoNoteIstanzaAnnRet);

            if (v_testo != null)
            {

                return v_testo;
            }
            return "";
        }

        public static string GetIdentificativoAvviso(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneIdentificativoAvviso);

            return v_testo;
        }

        public static string GetIdentificativoAvviso(tab_avv_pag_fatt_emissione p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneIdentificativoAvviso);

            return v_testo;
        }


        public static string GetInfoSportello(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneInfoSportello);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni

            }

            return v_testo;
        }

        public static string GetInformazioniNotificatore(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, SezioneInformazioniNotificatore);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(COGNOME_NOME_RISORSA_NOTIFICATORE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse21.CognomeNome).FirstOrDefault());
                try
                {
                    v_testo = v_testo.Replace(RUOLO_MANSIONE_NOTIFICATORE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse21.anagrafica_ruolo_mansione.descr_ruolo_mansione).FirstOrDefault());
                }
                catch
                {
                }
            }

            return v_testo;
        }
        public static string GetInformazioniNotificatoreSmall(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, InformazioniNotificatoreSmall);

            if (v_testo != null)
            {
                //Se ha trovato la sezione, effettua le sostituzioni
                v_testo = v_testo.Replace(COGNOME_NOME_RISORSA_NOTIFICATORE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse21.CognomeNome).FirstOrDefault());
                try
                {
                    v_testo = v_testo.Replace(RUOLO_MANSIONE_NOTIFICATORE, p_avviso.tab_liste.tab_validazione_approvazione_liste.Select(s => s.anagrafica_risorse21.anagrafica_ruolo_mansione.descr_ruolo_mansione).FirstOrDefault());
                }
                catch
                {
                }
            }

            return v_testo;
        }
        public static string GetTestoInformativaARERA(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "TestoInformativaARERA");
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        public static string GetPrintTheme(tab_avv_pag p_avviso)
        {
            string v_testo = GetSezione(p_avviso, "PrintTheme");

            return v_testo;
        }

        #region codice per anteprima esiti istanze
        public static string GetOggettoAccoglimentoMediazione(tab_doc_input p_docInput)
        {
            string v_testo = string.Empty;// GetSezione(p_avviso, SezioneOggettoEsitoIstanze);
            TextInfo myTI = new CultureInfo("it-IT", false).TextInfo;
            string v_DescOggetto = "<html><body><font face= 'Times New Roman' size='3'><div><b>Oggetto: ";
            bool isParziale = p_docInput.join_tab_avv_pag_tab_doc_input.Any(s => s.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO));
            string v_tipo_accoglimento = string.Empty;
            tab_ricorsi v_tab_ricorsi = p_docInput.tab_ricorsi.FirstOrDefault();
            if (isParziale)
            {
                v_tipo_accoglimento = "Provvedimento di accoglimento parziale del ricorso/reclamo con mediazione presso " +
                    v_tab_ricorsi.tab_autorita_giudiziaria.descrizione_autorita;
            }
            else
            {
                v_tipo_accoglimento = "Provvedimento di accoglimento totale del ricorso/reclamo con mediazione presso " +
                    v_tab_ricorsi.tab_autorita_giudiziaria.descrizione_autorita;
            }
            string v_stRif = "<div><br><br>In seguito alla approfondita disamina effettuata in merito alle motivazioni esposte nel "
                + "Ricorso/reclamo in oggetto, notificato in data " + p_docInput.data_presentazione_String + " " + "#PRESENTATORE_RICORSO#, è stato adottato il "
                + v_tipo_accoglimento
                + " con il quale si è proceduto ad annullare in autotutela i seguenti atti :";

            string v_DescIstanza = p_docInput.tab_tipo_doc_entrate.descr_doc +
                " n. " + p_docInput.identificativo_doc_input.Trim() +
                " del " + p_docInput.data_presentazione_String + "</div><br>";

            string v_presentatore = string.Empty;
            string v_ricorrente = string.Empty;
            if (v_tab_ricorsi.nominativo_ricorrente != null)
            {
                v_ricorrente = v_tab_ricorsi.nominativo_ricorrente;
            }
            else
            {
                if (v_tab_ricorsi.flag_individuazione_ricorrente == tab_ricorsi.CONTRIBUENTE)
                {
                    v_ricorrente = v_tab_ricorsi.tab_contribuente.nominativoDisplay;
                }
                else if (v_tab_ricorsi.flag_individuazione_ricorrente == tab_ricorsi.REFERENTE)
                {
                    v_ricorrente = v_tab_ricorsi.tab_referente.nominativoDisplay;
                }
            }
            v_ricorrente = myTI.ToTitleCase(v_ricorrente.ToLower());
            if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso != null && v_tab_ricorsi.pec_avvocato_ricorrente != null)
            {
                v_presentatore = "dall'Avvocato " + v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso + Environment.NewLine
                    + "in nome e per conto di " + v_ricorrente;
            }
            else if (v_tab_ricorsi.nominativo_avvocato_ricorrente_ricorso == null || v_tab_ricorsi.pec_avvocato_ricorrente == null)
            {
                v_presentatore = " da " + v_ricorrente + Environment.NewLine;
            }
            v_testo = v_DescOggetto + v_DescIstanza + v_stRif;

            v_testo = v_testo.Replace("#PRESENTATORE_RICORSO#", v_presentatore);
            v_testo = v_testo + "</div></Font></Body></Html>";
            if (v_testo != null)
            {
                return v_testo;
            }
            return "";
        }
        //public static string GetTestoControdeduzioniCTP(tab_doc_input p_docInput)
        //{
        //    string v_testata = "<html><body><font face = 'Times New Roman' size = '3'><div><b>" +
        //                        "All'On. " + p_docInput.tab_ricorsi.FirstOrDefault().tab_autorita_giudiziaria.descrizione_autorita +
        //                        "</br>" +
        //                        "Sezione: " + p_docInput.tab_ricorsi.FirstOrDefault().sezione_giudicante ?? "" +
        //                        "R.G. " + p_docInput.tab_ricorsi.FirstOrDefault().rgr ?? "" +
        //                        "</br>Atto di costituzione in giudizio e controdeduzioni</br></br></br>";

        //    string v_testo = string.Empty;

        //    if (p_docInput.
        //        "L'Ente " + p_docInput.anagrafica_ente.descrizione_ente + ", con partita iva: " +
        //                            p_docInput.anagrafica_ente.p_iva + " e con sede legale in " +
        //                            p_docInput.anagrafica_ente.IndirizzoCompleto + ", in persona " +
        //                            p_docInput.tab_ricorsi.FirstOrDefault().ra
        //}

        #endregion
    }
}
