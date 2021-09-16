using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_path_portale
    {
        [Key]
        public long id_tab_path_portale { get; set; }
        public string path_download { get; set; }
        public string path_upload { get; set; }
        public string path_image { get; set; }
        public string descrizione { get; set; }
        public int id_tab_ambiente { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? data_stato { get; set; }

        [ForeignKey(nameof(id_tab_ambiente))]
        [InverseProperty(nameof(tab_ambiente.tab_path_portale))]
        public virtual tab_ambiente id_tab_ambienteNavigation { get; set; }
    }
}
