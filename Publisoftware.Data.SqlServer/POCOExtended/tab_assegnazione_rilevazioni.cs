using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_assegnazione_rilevazioni : ISoftDeleted
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
            public int id_tab_assegnazione_rilevazioni { get; set; }
            public int id_ente { get; set; }
            public int id_ente_gestito { get; set; }
            public int id_tab_zone { get; set; }
            public int id_rilevatore { get; set; }
            public int id_lista_letture { get; set; }
            public System.DateTime data_assegnazione { get; set; }
            public System.DateTime data_inizio_rilevazione { get; set; }
            public System.DateTime data_fine_rilevazione { get; set; }
            public string cod_stato { get; set; }
            public int id_stato { get; set; }
            public int id_risorsa_stato { get; set; }
            public int id_struttura_stato { get; set; }


        }
    }
}
