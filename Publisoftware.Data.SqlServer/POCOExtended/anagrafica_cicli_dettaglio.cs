using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_cicli_dettaglio.Metadata))]
    public partial class anagrafica_cicli_dettaglio : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
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
            [Required(ErrorMessage = "Selezionare ciclo")]
            [DisplayName("ID")]
            public int id_ciclo { get; set; }

            [DisplayName("ID Ciclo Dettaglio")]
            public int id_ciclo_dettaglio { get; set; }

            [Required(ErrorMessage = "Selezionare applicazione")]
            [DisplayName("Applicazione")]
            public int id_tab_applicazioni { get; set; }

            [Required(ErrorMessage = "Selezionare un numero maggiore di zero")]
            [Range(1, int.MaxValue, ErrorMessage = "Selezionare un numero maggiore di zero")]
            [DisplayName("Sequenza")]
            public int sequenza { get; set; }

            [DisplayName("Data creazione")]
            public System.DateTime data_creazione { get; set; }
        }
    }
}
