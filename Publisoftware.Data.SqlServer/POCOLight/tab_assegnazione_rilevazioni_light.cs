using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_assegnazione_rilevazioni_light
    {
      
            public int id_tab_assegnazione_rilevazioni { get; set; }
            public Nullable<int> id_tab_zone { get; set; }
            public Nullable<int> id_rilevatore { get; set; }
            public Nullable<int> id_lista_letture { get; set; }
            public Nullable<System.DateTime> data_assegnazione { get; set; }
            public Nullable<System.DateTime> data_inizio_rilevazione { get; set; }
            public Nullable<System.DateTime> data_fine_rilevazione { get; set; }
            public string cod_stato { get; set; }
            public Nullable<int> id_stato { get; set; }
            public Nullable<int> id_risorsa_stato { get; set; }
            public Nullable<int> id_struttura_stato { get; set; }
            public Nullable<int> id_ente { get; set; }
            public Nullable<int> id_ente_gestito { get; set; }
        }
}
