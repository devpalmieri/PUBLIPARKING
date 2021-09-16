using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_udienza.Metadata))]
    public partial class anagrafica_tipo_udienza : ISoftDeleted
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
            public int id_tipo_udienza { get; set; }

            [Required]
            [DisplayName("Tipo Documento Entrata")]
            public int id_tipo_doc_entrata { get; set; }

            [Required]
            [StringLength(5)]
            [DisplayName("Sigla")]
            public string sigla { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }
        }
    }
}
