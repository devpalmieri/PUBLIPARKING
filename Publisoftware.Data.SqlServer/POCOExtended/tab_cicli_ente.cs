using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_cicli_ente.Metadata))]
    public partial class tab_cicli_ente : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Selezionare un Ciclo Contrattuale")]
        public string descCicloContrattualeEnte
        {
            get
            {
                return (anagrafica_ente != null ? this.descrizione_ciclo + "   -   " + this.anagrafica_ente.descrizione_ente : string.Empty);
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_ciclo_ente { get; set; }

            [Required(ErrorMessage = "Selezionare ente")]
            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [Required(ErrorMessage = "Selezionare ciclo")]
            [DisplayName("Ciclo")]
            public int id_ciclo { get; set; }
             
            [DisplayName("Note")]
            [MaxLength(100)]
            public string descrizione_ciclo { get; set; }

            [RegularExpression("^([01][0-9]|2[0-3]|[1-9]):([0-5][0-9]|[0-9]):([0-5][0-9]|[0-9])$", ErrorMessage = "Formato Ora non valido (Es: 19:00:00)")]
            [DisplayName("Esecuzione da")]
            public TimeSpan esecuzione_da { get; set; }

            [DisplayName("Esecuzione a")]
            [RegularExpression("^([01][0-9]|2[0-3]|[1-9]):([0-5][0-9]|[0-9]):([0-5][0-9]|[0-9])$", ErrorMessage = "Formato Ora non valido (Es: 08:00:00)")]
            public TimeSpan esecuzione_a { get; set; }
        }
    }
}
