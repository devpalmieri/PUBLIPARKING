using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_liste.Metadata))]
    public partial class tab_liste : ISoftDeleted, IGestioneStato
    {
        // Sia Liste Emesse che Trasmesse
        /// <summary>
        /// Emesse-Trasmesse: Lista Annullata 
        /// </summary>
        public const string ANN = "ANN-";

        /// <summary>
        /// Emesse-Trasmesse: Lista Annullata 
        /// </summary>
        public const string ANN_ANN = "ANN-ANN";

        /// <summary>
        /// Emesse-Trasmesse: Lista ancora in Fatt Emissione
        /// </summary>
        public const string PRE = "PRE-";

        /// <summary>
        /// Emesse-Trasmesse: Lista Importata/Calcolata con errori
        /// </summary>
        public const string PRE_ERR = "PRE-ERR";

        /// <summary>
        /// Emesse-Trasmesse: Lista con calcolo da approvare
        /// </summary>
        public const string PRE_VAL = "PRE-VAL";

        /// <summary>
        /// Emesse-Trasmesse: Lista approvata da validare
        /// </summary>
        public const string PRE_APP = "PRE-APP";

        /// <summary>
        /// Emesse: Lista validata da consolidate
        /// </summary>
        public const string PRE_DEF = "PRE-DEF";

        /// <summary>
        /// Emesse-Trasmesse: Lista spostata ad Fatt Emissione a definitivo
        /// </summary>
        public const string DEF = "DEF-";

        /// <summary>
        /// Emesse-Trasmesse: Lista stampata in attesa di pacchettizzazione (Emessa) / Lista trasmessa manuale da approvare (Trasmessa)
        /// </summary>
        public const string DEF_STA = "DEF-STA";

        /// <summary>
        /// Emesse-Trasmesse: Stampa massiva effettuata (Emessa) / Lista Presa in carico (Trasmessa)
        /// </summary>
        public const string DEF_SPE = "DEF-SPE";



        // Solo Liste Emesse
        /// <summary>
        /// Emesse: Tutti gli avvisi sono stati calcolati
        /// </summary>
        public const string PRE_CAL = "PRE-CAL";

        /// <summary>
        /// Emesse: Lista appena creata e senza avvisi o con calcolo avvisi non completato
        /// </summary>
        public const string PRE_PRE = "PRE-PRE";

        /// <summary>
        /// Emesse: Lista con stampa approvata
        /// </summary>
        public const string DEF_DEF = "DEF-DEF";

        /// <summary>
        /// Emesse: Lista consoludata
        /// </summary>
        public const string DEF_CON = "DEF-CON";



        // Solo Liste Trasmesse
        /// <summary>
        /// Trasmesse: Lista Massiva appena creata con l'invio di un file
        /// </summary>
        public const string PRE_ACQ = "PRE-ACQ";

        /// <summary>
        /// Trasmesse: Lista manuale in fase di caricamento
        /// </summary>
        public const string DEF_CAR = "DEF-CAR";

        /// <summary>
        /// Trasmesse: Lista i attesa di assegnazione
        /// </summary>
        public const string PRE_ASS = "PRE-ASS";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;

            //scrive i campi relativi alla creazione solo la prima volta
            if (id_struttura_creazione == null)
            {
                id_struttura_creazione = p_idStruttura;
                id_risorsa_creazione = p_idRisorsa;
                data_creazione = DateTime.Now;
            }
        }

        /// <summary>
        /// Preleva record modalita rateizzazione valido per liste il cui tipo avviso e ing. fiscale
        /// </summary>
        public tab_modalita_rate_avvpag_view custom_prop_modalita_rateizzazione_ing_fisc
        {
            get
            {
                //Verif. tipo servizio
                //if (this.anagrafica_tipo_avv_pag == null || this.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.ING_FISC)
                //  return null;

                //modalita rate con:
                //  --stessa tipo avviso della lista
                //  --stesso ente della lista
                //  --data lista compresa nel periodo di validità del record
                return this.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Where(mr => mr.id_ente == this.id_ente).AsQueryable()
                                                     .Where(mr => mr.periodo_validita_da.Date <= this.data_lista.Date && this.data_lista.Date <= mr.periodo_validita_a.Date)
                                                     .OrderByDescending(mr => mr.id_tipo_avv_pag)
                                                     .FirstOrDefault();
            }
        }

        public string DescrizioneIdentificativo
        {
            get
            {
                return descr_lista + " - " + identificativo_lista_ente;
            }
        }

        public string cod_stato_Descrizione
        {
            get
            {
                string retval = string.Empty;
                switch (this.cod_stato)
                {
                    case "DEF-CAR":
                        retval = "Lista caricata";
                        break;
                    case "PRE-CAL":
                        retval = "Lista da controllare";
                        break;
                    case "PRE-VAL":
                        retval = "Lista approvata internamente";
                        break;
                    case "PRE-DEF":
                        retval = "Lista da approvare (ENTE)";
                        break;
                    case PRE_APP:
                        retval = "Lista approvata (ENTE)";
                        break;
                    case "DEF-DEF":
                        retval = "Lista definitiva";
                        break;
                    case "DEF-SPE":
                        retval = "Lista spedita";
                        break;
                    case "ANN-":
                    case "ANN-ANN":
                        retval = "Lista annullata";
                        break;
                    default:
                        break;
                }
                return retval;
            }
        }


        [DisplayName("Importo Lista")]
        public string imp_tot_lista_Euro
        {
            get
            {
                if (imp_tot_lista.HasValue)
                    return imp_tot_lista.Value.ToString("C");
                else
                    return string.Empty;
            }
        }

        [DisplayName("Importo Lista Controllata")]
        public string imp_tot_spese_notifica_Euro
        {
            get
            {
                if (imp_tot_spese_notifica.HasValue)
                    return imp_tot_spese_notifica.Value.ToString("C");
                else
                    return string.Empty;
            }
        }

        public string imp_tot_imponibile_Euro
        {
            get
            {
                if (imp_tot_imponibile.HasValue)
                    return imp_tot_imponibile.Value.ToString("C");
                else
                    return string.Empty;
            }
        }

        public string imp_tot_iva_Euro
        {
            get
            {
                if (imp_tot_iva.HasValue)
                    return imp_tot_iva.Value.ToString("C");
                else
                    return string.Empty;
            }
        }

        public string imp_es_iva_Euro
        {
            get
            {
                if (imp_es_iva.HasValue)
                    return imp_es_iva.Value.ToString("C");
                else
                    return string.Empty;
            }
        }

        [DisplayName("Data Creazione Lista")]
        public string data_lista_String
        {
            get
            {
                return data_lista.ToShortDateString();
            }
            set
            {
                data_lista = DateTime.Parse(value);
            }
        }

        public string dt_approvazione_lista_String
        {
            get
            {
                if (data_approvazione.HasValue)
                {
                    return data_approvazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_lista { get; set; }

            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [DisplayName("Tipo Avviso")]
            public int id_tipo_avv_pag { get; set; }

            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [DisplayName("Descrizione Lista")]
            public string descr_lista { get; set; }

            [DisplayName("Stato")]
            public string cod_stato { get; set; }

            [DisplayName("Data Creazione Lista")]
            public System.DateTime data_lista { get; set; }

            [DisplayName("Importo Lista")]
            public decimal imp_tot_lista { get; set; }

            [DisplayName("Importo lista controllata")]
            public decimal imp_tot_spese_notifica { get; set; }
        }

    }
}
