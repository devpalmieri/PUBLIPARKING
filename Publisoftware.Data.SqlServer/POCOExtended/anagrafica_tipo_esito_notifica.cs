using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_esito_notifica.Metadata))]
    public partial class anagrafica_tipo_esito_notifica
    {
        public const Decimal ID_DESTINATARIO_SCONOSCIUTO = 13;
        public const Decimal ID_DESTINATARIO_DECEDUTO = 14;

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tipo_esito_notifica { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descr_tipo_esito_notifica { get; set; }

            [Required]
            [RegularExpression("[0-9]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Flag Esito")]
            public string flag_esito { get; set; }

            [Required]
            [RegularExpression("[0-9]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Flag Messo")]
            public string fl_rr_messo { get; set; }

            [Required]
            [RegularExpression("[0-9]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Flag Notificato")]
            public string fl_not_ok { get; set; }

            [Required]
            [RegularExpression("[0-9]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Flag Non Notificato")]
            public string fl_not_nok { get; set; }
        }
    }
}
