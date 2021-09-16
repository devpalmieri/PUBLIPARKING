using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_parentela.Metadata))]
    public partial class anagrafica_tipo_parentela : ISoftDeleted
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
            public int id_tipo_parentela { get; set; }
            [Required]
            [DisplayName("Codice Tipo Parentela")]
            public string cod_tipo_parentela { get; set; }

            [DisplayName("Descrizione Tipo Parentela")]
            public string desc_tipo_parentela { get; set; }

        }
    }
}
