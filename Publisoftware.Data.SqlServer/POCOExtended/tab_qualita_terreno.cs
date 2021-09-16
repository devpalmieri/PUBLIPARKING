using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_qualita_terreno.Metadata))]
    public partial class tab_qualita_terreno
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_qualita_terreno { get; set; }

            [Required]            
            [RegularExpression("[0-9]{1,5}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string codice { get; set; }

            [Required]            
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [Required]
            [DisplayName("Utilizzo IMU")]
            public int id_utilizzo_ente_imu { get; set; }

            [Required]
            [DisplayName("Utilizzo ICI")]
            public int id_utilizzo_ente_ici { get; set; }
        }
    }
}
