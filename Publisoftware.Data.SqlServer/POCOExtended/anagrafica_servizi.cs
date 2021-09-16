using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_servizi.Metadata))]
    public partial class anagrafica_servizi
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_servizio { get; set; }

            [Required]
            [Range(1, Int32.MaxValue, ErrorMessage = "E' necessario selezionare un'entrata")]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required]
            [DisplayName("Codice Servizio")]
            [StringLength(6)]
            public string cod_servizio { get; set; }

            [Required]
            [DisplayName("Descrizione Servizio")]
            [StringLength(100)]
            public string descr_servizio { get; set; }

            [Required]
            [Range(0, Int32.MaxValue, ErrorMessage = "E' necessario selezionare un tipo")]
            [DisplayName("Tipo Servizio")]
            public int id_tipo_servizio { get; set; }
        }
    }
}
