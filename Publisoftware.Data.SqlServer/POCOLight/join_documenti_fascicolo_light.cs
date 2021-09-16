using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_documenti_fascicolo_light : BaseEntity<join_documenti_fascicolo_light>
    {
        public int id_join_documenti_fascicolo { get; set; }
        public int id_tab_documenti { get; set; }
        public string tipo_documento { get; set; }
        public string identificativo_doc_input { get; set; }
        public string identificativo_avv_pag_rif { get; set; }
        public string identificativo_avv_pag { get; set; }
    }
}