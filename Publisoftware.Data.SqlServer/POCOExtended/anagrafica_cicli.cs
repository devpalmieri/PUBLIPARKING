using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_cicli.Metadata))]
    public partial class anagrafica_cicli : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        [DisplayName("Numero cicli collegati")]
        public int NumCicliDettaglio
        {
            get
            {
                return (anagrafica_cicli_dettaglio != null ? this.anagrafica_cicli_dettaglio.Count : 0);
            }
        }

        [DisplayName("Data Creazione")]
        public string data_creazione_String
        {
            get
            {
                return data_creazione.ToShortDateString();
            }
            set
            {
                data_creazione = DateTime.Parse(value);
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID Ciclo")]
            public int id_ciclo { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [Required(ErrorMessage = "Selezionare tipo servizio")]
            [DisplayName("Tipo Servizio")]
            public int id_tipo_servizio { get; set; }

            [Required(ErrorMessage = "Selezionare ente")]
            [DisplayName("Tipo Ente")]
            public int id_ente { get; set; }

            [Required(ErrorMessage = "Selezionare tipologia ciclo")]
            [DisplayName("Tipo Ciclo")]
            public int id_tipo_ciclo { get; set; }

            [DisplayName("Tipo Entrata")]
            public int id_entrata { get; set; }

            [Required(ErrorMessage = "Selezionare tipologia avviso")]
            [DisplayName("Tipo Avviso")]
            public int id_tipo_avvpag { get; set; }

            [DisplayName("Data creazione")]
            public System.DateTime data_creazione { get; set; }
        }
    }
}
