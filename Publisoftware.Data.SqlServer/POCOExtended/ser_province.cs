using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(ser_province.Metadata))]
    public partial class ser_province
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int cod_provincia { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string des_provincia { get; set; }

            [Required]
            [DisplayName("Sigla")]
            [RegularExpression("[A-Z]{2,2}", ErrorMessage = "Formato non valido")]
            public string sig_provincia { get; set; }

            [Required]
            [DisplayName("Regione")]
            public int cod_regione { get; set; }
        }
    }
}
