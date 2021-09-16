using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_pianificazione_cicli_dettaglio.Metadata))]
    public partial class tab_pianificazione_cicli_dettaglio : ISoftDeleted, IGestioneStato
    {
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
        }

        public string cod_stato_Descrizione
        {
            get
            {
                string retval = string.Empty;
                switch (this.cod_stato)
                {
                    case "ATT-PRE":
                        retval = "Applicazione in Pianificazione";
                        break;
                    case "ATT-SCH":
                        retval = "Applicazione Schedulata";
                        break;
                    case "ATT-PIA":
                        retval = "Applicazione Pianificata";
                        break;
                    case "ATT-ESE":
                        retval = "Applicazione eseguita correttamente";
                        break;
                    case "ATT-SPS":
                        retval = "Applicazione sospesa";
                        break;
                    case "ATT-LAN":
                        retval = "Applicazione in esecuzione";
                        break;
                    case "ATT-ERR":
                        retval = "Applicazione terminata con errore";
                        break;
                    case "ANN-":
                    case "ANN-ANN":
                    case "ATT-ANN":
                        retval = "Applicazione annullata";
                        break;
                    default:
                        break;
                }
                return retval;
            }
        }

        [DisplayName("Data Scadenza")]
        public string data_scadenza_String
        {
            get
            {
                return this.data_scadenza.ToShortDateString();
            }
            set
            {
                this.data_scadenza = DateTime.Parse(value);
            }
        }


        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }

        }
}
