using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_nota.Metadata))]
    public partial class tab_tipo_nota
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_tipo_nota { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string codice { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,100}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }
        }
    }
}
