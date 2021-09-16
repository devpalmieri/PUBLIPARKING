using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_veicolo.Metadata))]
    public partial class anagrafica_tipo_veicolo : ISoftDeleted
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
            public int id_tipo_veicolo { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string cod { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,100}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }
        }
    }
}
