using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_documento.Metadata))]
    public partial class anagrafica_tipo_documento : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
            
            [DisplayName("Id")]
            public int id_tipo_documento { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{2,2}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string codice { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{2,2}", ErrorMessage = "Formato non valido")]
            [DisplayName("Sigla")]
            public string sigla { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,50}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }
        }
    }
}
