using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_ente_gestito.Metadata))]
    public partial class anagrafica_ente_gestito : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        
        public string data_ultima_acquisizione_toponomastica_String
        {
            get
            {
                if (data_ultima_acquisizione_toponomastica.HasValue)
                    return data_ultima_acquisizione_toponomastica.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultima_acquisizione_toponomastica = DateTime.Parse(value);
            }
        }

        public string data_ultimo_aggiornamento_toponomastica_String
        {
            get
            {
                if (data_ultimo_aggiornamento_toponomastica.HasValue)
                    return data_ultimo_aggiornamento_toponomastica.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultimo_aggiornamento_toponomastica = DateTime.Parse(value);
            }
        }

        public string data_ultima_acquisizione_archivio_anagrafe_String
        {
            get
            {
                if (data_ultima_acquisizione_archivio_anagrafe.HasValue)
                    return data_ultima_acquisizione_archivio_anagrafe.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultima_acquisizione_archivio_anagrafe = DateTime.Parse(value);
            }
        }

        public string data_ultimo_aggiornamento_anagrafico_contribuenti_String
        {
            get
            {
                if (data_ultimo_aggiornamento_anagrafico_contribuenti.HasValue)
                    return data_ultimo_aggiornamento_anagrafico_contribuenti.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultimo_aggiornamento_anagrafico_contribuenti = DateTime.Parse(value);
            }
        }

        public string data_ultima_acquisizione_archivio_infocamere_String
        {
            get
            {
                if (data_ultima_acquisizione_archivio_infocamere.HasValue)
                    return data_ultima_acquisizione_archivio_infocamere.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultima_acquisizione_archivio_infocamere = DateTime.Parse(value);
            }
        }

        public string data_ultimo_aggiornamento_infocamere_contribuenti_String
        {
            get
            {
                if (data_ultimo_aggiornamento_infocamere_contribuenti.HasValue)
                    return data_ultimo_aggiornamento_infocamere_contribuenti.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultimo_aggiornamento_infocamere_contribuenti = DateTime.Parse(value);
            }
        }

        public string data_ultima_acquisizione_archivio_contribuenti_String
        {
            get
            {
                if (data_ultima_acquisizione_archivio_contribuenti.HasValue)
                    return data_ultima_acquisizione_archivio_contribuenti.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultima_acquisizione_archivio_contribuenti = DateTime.Parse(value);
            }
        }

        public string data_ultimo_aggiornamento_contribuenti_String
        {
            get
            {
                if (data_ultimo_aggiornamento_contribuenti.HasValue)
                    return data_ultimo_aggiornamento_contribuenti.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultimo_aggiornamento_contribuenti = DateTime.Parse(value);
            }
        }


        public string data_ultimo_aggiornamento_anagrafe_String
        {
            get
            {
                if (data_ultimo_aggiornamento_anagrafe.HasValue)
                    return data_ultimo_aggiornamento_anagrafe.Value.ToShortDateString();
                else
                    return string.Empty;
            }
            set
            {
                data_ultimo_aggiornamento_anagrafe = DateTime.Parse(value);
            }
        }


        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_ente { get; set; }

            [Required]
            [StringLength(12)]
            [DisplayName("Codice Ente")]
            public string codice_ente { get; set; }

            [Required]
            [DisplayName("Descrizione Ente")]
            public string descrizione_ente { get; set; }

            [DisplayName("Codice Regione")]
            public string cod_regione { get; set; }

            [DisplayName("Codice Provincia")]
            public string cod_provincia { get; set; }

            [DisplayName("Codice Comune")]
            public string cod_comune { get; set; }

            [RegularExpression("[0-9]{5}", ErrorMessage = "CAP non valido (es. 80100)")]
            [DisplayName("CAP")]
            public string cap { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [StringLength(50)]
            [DisplayName("Telefono 1")]
            public string tel1 { get; set; }

            [StringLength(50)]
            [DisplayName("Telefono 2")]
            public string tel2 { get; set; }

            [Required]
            [DisplayName("Tipo Ente")]
            public int id_tipo_ente { get; set; }

            
            [DisplayName("Stato Contatto")]
            public int id_stato_contatto { get; set; }

            
            [DisplayName("Data Ultimo Aggiornamento Anagrafe")]
            public DateTime data_ultimo_aggiornamento_anagrafe { get; set; }

            [StringLength(50)]
            [DisplayName("Impianto depurazione")]
            public string flag_impianto_depurazione { get; set; }


            [DisplayName("Data Ultima acquisizione Archivio Anagrafe")]
            public DateTime data_ultima_acquisizione_archivio_anagrafe { get; set; }
            [DisplayName("Data riferimento Ultima anagrafe acquisita")]
            public DateTime data_riferimento_ultima_anagrafe_acquisita { get; set; }
            [DisplayName("Data Ultimo Aggiornamento Anagrafico Contribienti")]
            public DateTime data_ultimo_aggiornamento_anagrafico_contribuenti { get; set; }
            [DisplayName("Data riferimento Ultimo Aggiornamento Anagrafico Contribuenti")]
            public DateTime data_riferimento_ultimo_aggiornamento_anagrafico_contribuenti { get; set; }

            [DisplayName("Data Ultima acquisizione Archivio Infocamere")]
            public DateTime data_ultima_acquisizione_archivio_infocamere { get; set; }
            [DisplayName("Data riferimento Ultimo Infocamere acquisito")]
            public DateTime data_riferimento_ultimo_infocamere_acquisito { get; set; }
            [DisplayName("Data Ultimo Aggiornamento Infocamere Contribuenti")]
            public DateTime data_ultimo_aggiornamento_infocamere_contribuenti { get; set; }
            [DisplayName("Data riferimento Ultimo Aggiornamento Infocamere Contribuenti")]
            public DateTime data_riferimento_ultimo_aggiornamento_infocamere_contribuenti { get; set; }

            [DisplayName("Data Ultima acquisizione Archivio Contribuenti")]
            public DateTime data_ultima_acquisizione_archivio_contribuenti { get; set; }
            [DisplayName("Data riferimento Ultimo Archivio Contribuenti acquisito")]
            public DateTime data_riferimento_ultimo_contribuenti_acquisito { get; set; }
            [DisplayName("Data Ultimo Aggiornamento Contribuenti")]
            public DateTime data_ultimo_aggiornamento_contribuenti { get; set; }
            [DisplayName("Data riferimento Ultimo Aggiornamento Contribuenti")]
            public DateTime data_riferimento_ultimo_aggiornamento_contribuenti { get; set; }

            [DisplayName("Data Ultima acquisizione Archivio Toponomastica")]
            public DateTime data_ultima_acquisizione_toponomastica { get; set; }
            [DisplayName("Data riferimento Ultima toponomastica acquisita")]
            public DateTime data_riferimento_ultima_toponomastica_acquisita { get; set; }
            [DisplayName("Data Ultimo Aggiornamento Toponomastica")]
            public DateTime data_ultimo_aggiornamento_toponomastica { get; set; }
            [DisplayName("Data riferimento Ultimo Aggiornamento Toponomastica")]
            public DateTime data_riferimento_ultimo_aggiornamento_toponomastica { get; set; }

        }
    }
}
