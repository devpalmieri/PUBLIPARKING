using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_abilitazione_menu
    {
        [Key]
        public int id_tab_abilitazione_menu { get; set; }
        public int? id_risorsa_stato { get; set; }
        public int? id_struttura_aziendale { get; set; }
        public int id_ente { get; set; }
        public int id_tab_nodo_menu { get; set; }
        public int livello_menu { get; set; }
        public string descrizione { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime data_stato { get; set; }
        public bool flag_abilitato { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }
    }
}
