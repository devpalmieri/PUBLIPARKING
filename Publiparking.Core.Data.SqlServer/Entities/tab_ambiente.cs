using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_ambiente
    {
        public tab_ambiente()
        {
            tab_path_portale = new HashSet<tab_path_portale>();
        }

        [Key]
        public int id_tab_ambiente { get; set; }
        [StringLength(3)]
        public string cod_ambiente { get; set; }
        [StringLength(50)]
        public string descrizione { get; set; }

        [InverseProperty("id_tab_ambienteNavigation")]
        public virtual ICollection<tab_path_portale> tab_path_portale { get; set; }
    }
}
