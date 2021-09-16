using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_categorie_fabbricati.Metadata))]
    public partial class tab_categorie_fabbricati
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_categorie_fabbricati { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9]{1,5}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string codice { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [Required]
            [StringLength(5, ErrorMessage = "Formato non valido")]            
            [DisplayName("Descrizione breve")]
            public string descrizione_breve { get; set; }

            [Required]
            [DisplayName("Categoria IMU")]
            public int id_categoria_contribuzione_imu { get; set; }

            [Required]
            [DisplayName("Categoria ICI")]
            public int id_categoria_contribuzione_ici { get; set; }

            [Required]
            [DisplayName("Utilizzo IMU")]
            public int id_utilizzo_imu { get; set; }

            [Required]
            [DisplayName("Utilizzo ICI")]
            public int id_utilizzo_ici { get; set; }
        }
    }
}
