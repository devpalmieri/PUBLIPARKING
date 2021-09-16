using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_lettura.Metadata))]
    public partial class anagrafica_tipo_lettura : ISoftDeleted
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

            public int id_tipo_lettura { get; set; }
            [Required]
            [DisplayName("Codice Tipo Lettura")]
            [StringLength(6)]
            public string cod_tipo_lettura { get; set; }
            [Required]
            [DisplayName("Descrizione Tipo Lettura")]
            public string descrizione_tipo_lettura { get; set; }
            [Required]
            [DisplayName("IOF")]
            [RegularExpression("[I|O|F]{1}", ErrorMessage="Formato non valido: flag ammesse I, O, F")]
            public string flag_iof { get; set; }
        }
    }
}
