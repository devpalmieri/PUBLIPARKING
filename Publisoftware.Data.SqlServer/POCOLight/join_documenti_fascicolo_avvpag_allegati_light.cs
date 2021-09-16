using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_documenti_fascicolo_avvpag_allegati_light : BaseEntity<join_documenti_fascicolo_avvpag_allegati_light>
    {
        public int id_join_documenti_fascicolo_avvpag_allegati { get; set; }
        public int id_tab_documenti { get; set; }
        public string tipo_documento { get; set; }
        public string barcodeNotifica { get; set; }
        public string destinatarioNotifica { get; set; }
        public string dataNotifica { get; set; }
        public string identificativo_doc_input { get; set; }
        public string identificativo_avv_pag_rif { get; set; }
        public string identificativo_avv_pag { get; set; }
    }
}