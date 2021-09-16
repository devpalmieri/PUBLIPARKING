using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_agevolazione.Metadata))]
    public partial class anagrafica_tipo_agevolazione : ISoftDeleted
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

            [DisplayName("ID")]
            public int id_tipo_agevolazione { get; set; }

            [Required]
            [DisplayName("Sigla")]
            public string sigla_tipo_agevolazione { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string des_tipo_agevolazione { get; set; }
        }
    }
}
