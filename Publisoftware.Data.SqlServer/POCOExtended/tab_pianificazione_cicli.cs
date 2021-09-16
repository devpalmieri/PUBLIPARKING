using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_pianificazione_cicli.Metadata))]
    public partial class tab_pianificazione_cicli : ISoftDeleted, IGestioneStato
    {

        public tab_pianificazione_cicli(tab_pianificazione_cicli oldCiclo)
        {
            this.id_ciclo_ente = oldCiclo.id_ciclo_ente;
            this.descrizione = oldCiclo.descrizione;
            this.data_creazione = DateTime.Now;
            //this.data_pianificata_inizio_esecuzione = da calcolare
            //this.data_pianificata_scadenza_esecuzione = da calcolare
            this.periodicita = oldCiclo.periodicita;
            this.priorita = oldCiclo.priorita;
        }
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsGenerica { get; set; }
     
      
        public int numDettaglio 
        { 
            get
            {
                return (this.tab_pianificazione_cicli_dettaglio != null ? tab_pianificazione_cicli_dettaglio.Count : 0);
            } 
        }

        public int numInput
        {
            get
            {
                return (this.tab_pianificazione_cicli_input_emissione != null ? tab_pianificazione_cicli_input_emissione.Count : 0);
            }
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


        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        [DisplayName("Periodicità")]
        public PeriodicitaEnum periodicita_enum
        {
            get
            {
            if (this.periodicita == null)
                return PeriodicitaEnum.NO;
            else
                return (PeriodicitaEnum)Enum.Parse(typeof(PeriodicitaEnum), periodicita.ToUpper());
            }
            set
            {
                periodicita = (value).ToString();
            }
        }

        [DisplayName("Data Creazione")]
        public string data_creazione_String
        {
            get
            {
                return this.data_creazione.ToShortDateString();
            }
            set
            {                                
                this.data_creazione = DateTime.Parse(value);               
            }
        }

        [DisplayName("Data Fine Elaborazione")]
        public string data_fine_elaborazione_String
        {
            get
            {
                return this.data_fine_elaborazione.HasValue ? this.data_fine_elaborazione.Value.ToShortDateString() : string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.data_fine_elaborazione = DateTime.Parse(value);
                }
                else
                {
                    this.data_fine_elaborazione = null;
                }
            }
        }

        [DisplayName("Previsione inizio esecuzione")]
        public string data_pianificata_inizio_esecuzione_String
        {
            get
            {
                return this.data_pianificata_inizio_esecuzione.ToShortDateString();
            }
            set
            {                
                    this.data_pianificata_inizio_esecuzione = DateTime.Parse(value);              
            }
        }

        [DisplayName("Previsione fine esecuzione")]
        public string data_pianificata_scadenza_esecuzione_String
        {
            get
            {
                return this.data_pianificata_scadenza_esecuzione.ToShortDateString();
            }
            set
            {
                this.data_pianificata_scadenza_esecuzione = DateTime.Parse(value);
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_pianificazione_ciclo { get; set; }

            [DisplayName("Tipo ciclo")]
            [Required(ErrorMessage = "Selezionare un Tipo ciclo")]
            public string id_tipo_ciclo { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Selezionare un Ciclo Contrattuale")]
            [DisplayName("Ciclo contrattuale")]
            public int id_ciclo_ente { get; set; }

            [Range(1, int.MaxValue, ErrorMessage = "Selezionare un Ente")]
            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [Required(ErrorMessage = "inserire una descrizione")]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [DisplayName("Data Creazione")]
            public DateTime data_creazione { get; set; }

            [DisplayName("Data Fine Elaborazione")]
            public DateTime? data_fine_elaborazione { get; set; }
            
            [DisplayName("Data inizio esecuzione")]
            [Required(ErrorMessage = "inserire una data di fine elaborazione")]            
            public DateTime data_pianificata_inizio_esecuzione { get; set; }
            
            [DisplayName("Data scadenza esecuzione")]
            [Required(ErrorMessage = "inserire una data di fine elaborazione")]            
            public DateTime data_pianificata_scadenza_esecuzione { get; set; }

            [DisplayName("Periodicità")]
            [Required(ErrorMessage = "inserire periodicita")]            
            public string periodicita { get; set; }

            [Required]
            [DisplayName("Priorità")]
            [Range(1, 5, ErrorMessage = "Selezionare un valore da 1 a 5")]            
            public int priorita { get; set; }

            [DisplayName("Stato")]
            [Required(ErrorMessage = "inserire Codice Stato")]            
            public string cod_stato { get; set; }            
            
            //[DisplayName("Flag Ente GEstito")]
            //public string flag_ente_gestito { get; set; }

        }
    }
}
