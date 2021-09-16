using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_zone.Metadata))]
    public partial class tab_zone : ISoftDeleted
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

            [Required]
            [DisplayName("ID")]
            public int id_zona { get; set; }

            [Required]
            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [Required]
            [DisplayName("Comune")]
            public int cod_comune { get; set; }

            [Required]
            [DisplayName("Codice Zona")]
            public string cod_zona { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione_zona { get; set; }

            [Required]
            [DisplayName("Tipo")]
            [StringLength(3)]
            public string tipo_zona { get; set; }

            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [DisplayName("CAP")]
            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato non valido (Es: 12345)")]
            public string cap { get; set; }
        }
    }
}
