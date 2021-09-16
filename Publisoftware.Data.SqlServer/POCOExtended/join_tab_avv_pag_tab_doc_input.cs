using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_tab_avv_pag_tab_doc_input : ISoftDeleted, IGestioneStato
    {
        public const string ANN_ANN = "ANN-ANN";
        public const string ACQ_POS = "ACQ-POS";
        public const string DEF_ACC = "DEF-ACC";
        public const string DEF_RES = "DEF-RES";
        public const string DEF_PAG = "DEF-PAG";

        public const string PROCURATORE = "Procuratore speciale";
        public const string DIFENSORE = "Difensore con procura speciale";

        public const string SGRAVIO = "Esito per sgravio";
        public const string ENTE = "Motivazione Ente";
        public const string NOTIFICA_OK = "Notifica valida";
        public const string NOTIFICA_KO = "Notifica errata";
        public const string SOGGETTODEBITORE_OK = "Soggetto debitore corretto";
        public const string SOGGETTODEBITORE_KO = "Soggetto debitore errato";
        public const string BENE_OK = "Bene/disponibilità correttamente attribuiti";
        public const string BENE_KO = "Bene/disponibilità erroneamente attribuiti";
        public const string RIGA_OK = "Unità di contribuzione valida";
        public const string RIGA_KO = "Unità di contribuzione errata";
        public const string GENERICO_OK = "Motivazione non valida";
        public const string GENERICO_KO = "Motivazione valida";
        public const string SANZIONI_OK = "Sanzioni non eliminate";
        public const string SANZIONI_KO = "Sanzioni eliminate";

        //Il dottore ha voluto cambiare i flag dei pagamenti
        public const string PAGAMENTO_OK = "Motivazione valida";
        public const string PAGAMENTO_KO = "Motivazione non valida";
        public const string PAGAMENTO_ERRORE = "Pagamento erroneamente imputato";
        public const string PAGAMENTO_LAVORARE = "Pagamento da lavorare";
        public const string PAGAMENTO_NT = "Pagamento non trovato";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public tab_avv_pag tab_avv_pag_collegato
        {
            get { return this.tab_avv_pag1; }
            set { this.tab_avv_pag1 = value; }
        }

        public string NumDoc
        {
            get
            {
                if (tab_doc_input != null)
                {
                    return tab_doc_input.NumDoc;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string TipoPratica
        {
            get
            {
                if (tab_doc_input != null)
                {
                    return tab_doc_input.TipoPratica;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_presentazione_String
        {
            get
            {
                if (tab_doc_input != null && tab_doc_input.data_presentazione.HasValue)
                {
                    return tab_doc_input.data_presentazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string _esito;
        public string Esito
        {
            get { return _esito; }
            set { _esito = value; }
        }

        public string data_esito_String
        {
            get
            {
                if (data_esito.HasValue)
                {
                    return data_esito.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string AvvCollegato
        {
            get
            {
                if (tab_avv_pag_collegato != null)
                {
                    return tab_avv_pag_collegato.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string DescrizioneStatoDoc
        {
            get
            {
                return anagrafica_stato_doc.desc_stato;

            }
        }

        public string DescrizioneStatoDoc1
        {
            get
            {
                return anagrafica_stato_doc1 != null ? anagrafica_stato_doc1.desc_stato : string.Empty;

            }
        }

        public string DichiarazioneTerzoSommeDisponibili
        {
            get
            {
                if (dichiarazione_terzo_somme_disponibili.HasValue)
                {
                    return dichiarazione_terzo_somme_disponibili.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public anagrafica_causale anagCausale
        {
            get
            {
                return anagrafica_causale != null ? anagrafica_causale :
                             (join_doc_input_riga_avvpag.Count > 0 ? join_doc_input_riga_avvpag.FirstOrDefault().anagrafica_causale :
                             (join_doc_input_not_avvpag.Count > 0 ? join_doc_input_not_avvpag.FirstOrDefault().anagrafica_causale :
                             (join_doc_input_pag_avvpag.Count > 0 ? join_doc_input_pag_avvpag.FirstOrDefault().anagrafica_causale :
                             (join_doc_input_beni_avvpag.Count > 0 ? join_doc_input_beni_avvpag.FirstOrDefault().anagrafica_causale : null))));
            }
        }

        public string SetProperty
        {
            get
            {
                DateTime retDate = DateTime.Now;
                string sRet = string.Empty;
                List<join_doc_input_pag_avvpag> lstPagamenti = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).ToList();
                int numRighe = join_doc_input_riga_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count();
                int Vediamo = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count();
                if (Vediamo > 0)
                {
                    //if (join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().id_tab_mov_pag == null) //join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count() > 0 &&
                    //{
                    dataPagamentoDichiarata = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().data_operazione_pagamento.ToString();
                    dataValutaDichiarata = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().data_valuta_pagamento == null ? "" : join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().data_valuta_pagamento.ToString();
                    //ImportoPagatoDichiarato = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().importo_mov_pagato;
                    CodLinePagamento = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().code_line_pagamento;
                    NominativoPagante = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().cognome_rag_soc_pagante + " " + "" + join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().nome_pagante;
                    CodFiscalePartitaIva = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().codice_fiscale_pagante.ToUpper();
                    NoteIstanza = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().note_causale;
                    DescrizioneDocumento = join_documenti_pratiche.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Select(x => x.tab_documenti.anagrafica_documenti.descrizione_doc).FirstOrDefault();
                    sRet = "SP";
                    //}

                    //sRet = "CP";
                }
                else
                {
                    int Vediamo2 = join_doc_input_not_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count();
                    if (Vediamo2 > 0)
                    {

                        List<tab_sped_not> sp_not = join_doc_input_not_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Select(j => j.tab_sped_not).ToList();
                        if (sp_not.Where(x => x.id_stato_sped_not == 16 && x.id_stato_avvdep == 1).Count() > 0)
                        {
                            dataAvvenutaNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID && x.id_stato_avvdep == 1).Select(x => x.data_esito_notifica).FirstOrDefault();
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID && x.id_stato_avvdep == 1).Select(x => x.data_notifica_avvdep).FirstOrDefault();
                        }
                        else if (sp_not.Where(x => x.id_stato_sped_not == 28 && x.id_stato_avvdep == 1).Count() > 0)
                        {
                            dataAvvenutaNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.RIL_139_ID && x.id_stato_avvdep == 1).Select(x => x.data_esito_notifica.Value).FirstOrDefault();
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID && x.id_stato_avvdep == 1).Select(x => x.data_notifica_avvdep).FirstOrDefault();
                        }
                        else if (sp_not.Where(x => x.id_stato_sped_not == 14).Count() > 0)
                        {
                            dataAvvenutaNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.NOT_143_ID).Select(x => x.data_esito_notifica).FirstOrDefault();
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID && x.id_stato_avvdep == 1).Select(x => x.data_affissione_ap).FirstOrDefault();
                        }
                        else if (sp_not.Where(x => x.id_stato_sped_not == 1).Count() > 0)
                        {
                            dataAvvenutaNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.NOT_OKK_ID).Select(x => x.data_esito_notifica).FirstOrDefault();
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == anagrafica_stato_sped_not.NOT_OKK_ID).Select(x => x.data_esito_notifica).FirstOrDefault();
                        }

                        if (sp_not.Where(x => x.id_stato_sped_not == 16 && x.id_stato_avvdep == 2).Count() > 0)
                        {
                            dataAvvenutaNotifica = null;
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == 16 && x.id_stato_avvdep == 2).Select(x => x.data_esito_notifica).FirstOrDefault();
                        }
                        else if (sp_not.Where(x => x.id_stato_sped_not == 28 && x.id_stato_avvdep == 2).Count() > 0)
                        {
                            dataAvvenutaNotifica = null;
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == 28 && x.id_stato_avvdep == 2).Select(x => x.data_notifica_avvdep).FirstOrDefault();
                        }
                        else if (sp_not.Where(x => x.id_stato_sped_not == 13 && x.id_stato_avvdep == 2).Count() > 0)
                        {
                            dataAvvenutaNotifica = null;
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == 13 && x.id_stato_avvdep == 2).Select(x => x.data_esito_notifica).FirstOrDefault();
                        }
                        else if (sp_not.Where(x => x.id_stato_sped_not == 2).Count() > 0)
                        {
                            dataAvvenutaNotifica = null;
                            dataRicezioneNotifica = sp_not.Where(x => x.id_stato_sped_not == 2).Select(x => x.data_esito_notifica).FirstOrDefault();
                        }



                        DestinatarioNotifica = sp_not.Select(x => x.Destinatario).FirstOrDefault() + "  - " + sp_not.Select(x => x.cod_fiscale_recapito).FirstOrDefault();
                        NoteIstanza = join_doc_input_not_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().note_causale;

                        sRet = "NP";
                    }
                    sRet = "NP";
                }
                if (numRighe > 0)
                {
                    int id_ogg_contr = join_doc_input_riga_avvpag.Where(x => x.tab_unita_contribuzione.id_unita_contribuzione == x.id_unita_contribuzione).Select(x => x.tab_unita_contribuzione.id_unita_contribuzione).FirstOrDefault();
                    int? id_ana_contr = join_doc_input_riga_avvpag.Where(x => x.tab_unita_contribuzione.id_unita_contribuzione == x.id_unita_contribuzione).Select(x => x.tab_unita_contribuzione.id_anagrafica_voce_contribuzione).FirstOrDefault();
                    int id_causale = join_doc_input_riga_avvpag.Where(x => x.id_join_avv_pag_doc_input == id_join_avv_pag_doc_input).Select(j => j.id_causale).FirstOrDefault();
                    //MotivazioneIstanza= join_doc_input_riga_avvpag.Where(x => x.id_join_avv_pag_doc_input == id_join_avv_pag_doc_input).FirstOrDefault().note_causale;

                }
                return sRet;
            }
        }

        private string dtValuta;
        public string dataValutaDichiarata
        {
            get
            {
                return dtValuta;
            }
            set
            {
                dtValuta = value;
            }
        }

        private DateTime? _dtAvvenutaNotifica;
        public DateTime? dataAvvenutaNotifica
        {
            get
            {
                return _dtAvvenutaNotifica;
            }
            set
            {
                _dtAvvenutaNotifica = value;
            }
        }

        private DateTime? _dtRicezioneNotifica;
        public DateTime? dataRicezioneNotifica
        {
            get
            {
                return _dtRicezioneNotifica;
            }
            set
            {
                _dtRicezioneNotifica = value;
            }
        }

        private string _DestinatarioNotifica;
        public string DestinatarioNotifica
        {
            get
            {
                return _DestinatarioNotifica;
            }
            set
            {
                _DestinatarioNotifica = value;
            }
        }

        private string dtPag;
        public string dataPagamentoDichiarata
        {
            get
            {
                return dtPag == null ? "0" : dtPag.Substring(1, 10);
            }
            set
            {
                dtPag = value;
            }
        }

        private string _DescDocumento = "";
        public string DescrizioneDocumento
        {
            get
            {
                return _DescDocumento;
            }
            set
            {
                _DescDocumento = value;
            }
        }

        public decimal? ImportoPagatoDichiarato
        {
            get
            {
                int nRec = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count();
                if (nRec > 0)
                {
                    return join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).FirstOrDefault().importo_mov_pagato;
                }
                else if (join_doc_input_not_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count() > 0)
                {
                    return tab_avv_pag_collegato.imp_tot_avvpag;
                }
                return 0;
            }
        }

        public decimal? ImportoDaPagare
        {
            get
            {
                int nRec = join_doc_input_pag_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count();
                if (nRec > 0)
                {
                    return tab_avv_pag.importoTotDaPagare;
                }
                else if (join_doc_input_not_avvpag.Where(x => x.id_join_avv_pag_doc_input == this.id_join_avv_pag_doc_input).Count() > 0)
                {
                    return tab_avv_pag_collegato.imp_tot_avvpag;
                }
                return 0;
            }
        }



        private string CodLine = "";
        public string CodLinePagamento
        {
            get
            {
                return CodLine;
            }
            set
            {
                CodLine = value;
            }
        }
        private string _NominativoPagante = "";
        public string NominativoPagante
        {
            get
            {
                return _NominativoPagante;
            }
            set
            {
                _NominativoPagante = value;
            }
        }
        private string _NoteIstanza = "";
        public string NoteIstanza
        {
            get
            {
                return _NoteIstanza;
            }
            set
            {
                _NoteIstanza = value;
            }
        }
        private string codF_Piva = "";
        public string CodFiscalePartitaIva
        {
            get
            {
                return codF_Piva;
            }
            set
            {
                codF_Piva = value;
            }
        }

        public bool isMotivazioniAvvisoLavorate
        {
            get
            {
                return tab_doc_input.join_tab_avv_pag_tab_doc_input.Where(x => x.id_avv_pag_collegato == id_avv_pag_collegato && x.cod_stato != anagrafica_stato_doc.STATO_ANNULLATO).All(x => x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO));
            }
        }

        public bool isMotivazioniAvvisoRespinte
        {
            get
            {
                return tab_doc_input.join_tab_avv_pag_tab_doc_input.Where(x => x.id_avv_pag_collegato == id_avv_pag_collegato && x.cod_stato != anagrafica_stato_doc.STATO_ANNULLATO).All(x => x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAV_RES));
            }
        }

        public string importo_totale_vincolato_Euro
        {
            get
            {
                return importo_totale_vincolato_decimal.ToString("C");
            }
        }

        public decimal importo_totale_vincolato_decimal
        {
            get
            {
                if (importo_totale_vincolato.HasValue)
                {
                    return importo_totale_vincolato.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string data_scadenza_importo_vincolato_String
        {
            get
            {
                if (data_scadenza_importo_vincolato.HasValue)
                {
                    return data_scadenza_importo_vincolato.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_decorrenza_disponibilita_titoli_String
        {
            get
            {
                if (data_decorrenza_disponibilita_titoli.HasValue)
                {
                    return data_decorrenza_disponibilita_titoli.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_decorrenza_disponibilita_cc_depositi_String
        {
            get
            {
                if (data_decorrenza_disponibilita_cc_depositi.HasValue)
                {
                    return data_decorrenza_disponibilita_cc_depositi.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_scadenza_disponibilita_dichiarato_String
        {
            get
            {
                if (data_scadenza_disponibilita_dichiarato.HasValue)
                {
                    return data_scadenza_disponibilita_dichiarato.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_decorrenza_disponibilita_dichiarato_String
        {
            get
            {
                if (data_decorrenza_disponibilita_dichiarato.HasValue)
                {
                    return data_decorrenza_disponibilita_dichiarato.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_decorrenza_disponibilita_dichiarato_redditi_una_tantum_String
        {
            get
            {
                if (data_decorrenza_disponibilita_dichiarato_redditi_una_tantum.HasValue)
                {
                    return data_decorrenza_disponibilita_dichiarato_redditi_una_tantum.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_decorrenza_disponibilita_tfr_String
        {
            get
            {
                if (data_decorrenza_disponibilita_tfr.HasValue)
                {
                    return data_decorrenza_disponibilita_tfr.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
