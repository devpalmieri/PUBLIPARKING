using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(ser_regioni.Metadata))]
    public partial class ser_regioni
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int cod_regione { get; set; }

            [Required]
            [DisplayName("Regione")]
            public string des_regione { get; set; }
        }
    }
}
