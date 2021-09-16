using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_tab_supervisione_profili_light : BaseEntity<join_tab_supervisione_profili_light>
    {
        public int id_join_tab_supervisione_profili { get; set; }
        public string descrizione { get; set; }
        public int id_tab_profilo_contribuente { get; set; }
        public string protocollo_registro { get; set; }
        public string data_registro_String { get; set; }
    }
}
