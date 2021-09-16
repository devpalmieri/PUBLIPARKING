﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    [Serializable()]
    public partial class tab_menu_terzo_livello
    {
        [Key]
        public int id_tab_menu_terzo_livello { get; set; }
        public int? id_tab_menu_secondo_livello { get; set; }
        [Required]
        [StringLength(100)]
        public string descrizione { get; set; }
        public int ordine { get; set; }
        [StringLength(1)]
        public string flag_link { get; set; }
        [Required]
        [StringLength(100)]
        public string controller { get; set; }
        [Required]
        public string action { get; set; }
        public string tooltip { get; set; }
        [StringLength(150)]
        public string tipo_menu { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime data_stato { get; set; }
        public int? id_risorsa_stato { get; set; }
        [StringLength(1)]
        public string flag_visibile { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }

        [ForeignKey(nameof(id_tab_menu_secondo_livello))]
        [InverseProperty(nameof(tab_menu_secondo_livello.tab_menu_terzo_livello))]
        public virtual tab_menu_secondo_livello id_tab_menu_secondo_livelloNavigation { get; set; }
    }

}
