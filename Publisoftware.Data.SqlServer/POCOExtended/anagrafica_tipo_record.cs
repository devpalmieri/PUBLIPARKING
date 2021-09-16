using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_record.Metadata))]
    public partial class anagrafica_tipo_record : ISoftDeleted
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
            public int id_tipo_record { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{1,50}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string cod_tipo_record { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,50}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string descrizione_tipo_record { get; set; }
        }
    }
}
