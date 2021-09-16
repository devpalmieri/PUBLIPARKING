using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.Entities
{
    public partial class tab_applicazioni
    {
        [Key]
        public int id_tab_applicazioni { get; set; }
        public int id_tab_funzionalita { get; set; }
        public int id_tab_pagine { get; set; }
        [Required]
        [StringLength(100)]
        public string actionName { get; set; }
        public string parametri_url { get; set; }
        [Required]
        [StringLength(5)]
        public string codice { get; set; }
        [Required]
        [StringLength(100)]
        public string descrizione { get; set; }
        public string icona { get; set; }
        public int ordine { get; set; }
        public string tooltip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }
        public bool contribuente_required { get; set; }
        [Required]
        [StringLength(100)]
        public string label_menu { get; set; }
        [Required]
        [StringLength(2)]
        public string tipo_applicazione { get; set; }
        public bool flag_visualizazione { get; set; }
        public int livello_autorizzazione_interno { get; set; }
        public int? livello_autorizzazione_esterno { get; set; }
        public bool flag_sistema { get; set; }
        [Required]
        [StringLength(1)]
        public string flag_on_off { get; set; }
        public bool terzo_required { get; set; }

        [ForeignKey(nameof(id_tab_funzionalita))]
        [InverseProperty(nameof(tab_funzionalita.tab_applicazioni))]
        public virtual tab_funzionalita id_tab_funzionalitaNavigation { get; set; }
        [ForeignKey(nameof(id_tab_pagine))]
        [InverseProperty(nameof(tab_pagine.tab_applicazioni))]
        public virtual tab_pagine id_tab_pagineNavigation { get; set; }
    }

}
