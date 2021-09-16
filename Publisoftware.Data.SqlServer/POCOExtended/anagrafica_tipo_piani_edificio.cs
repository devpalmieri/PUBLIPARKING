using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_piani_edificio.Metadata))]
    public partial class anagrafica_tipo_piani_edificio
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tipo_piano { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string desc_piano { get; set; }

            [Required]
            [RegularExpression(@"[\d]{1,10}([.,][\d]{1,4})?", ErrorMessage = "Formato non valido")]
            [DisplayName("Ordinamento")]
            public decimal flag_ordinamento { get; set; }
        }
    }
}
