using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipi_diritti.Metadata))]
    public partial class tab_tipi_diritti
    {
        public string Diritto
        {
            get
            {
                return codice + " - " + descrizione;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_tipi_diritti { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{1,5}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string codice { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [Required]
            [DisplayName("Tipo Diritto Ente")]
            public int id_tipo_diritto_ente { get; set; }
        }
    }
}
