using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_cc.Metadata))]
    public partial class anagrafica_tipo_cc
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tipo_cc { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }
        }
    }
 
}
