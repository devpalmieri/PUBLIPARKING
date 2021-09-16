using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_fascicolo_avvpag_allegati_light : BaseEntity<tab_fascicolo_avvpag_allegati_light>
    {
        public int id_fascicolo_allegati_avvpag { get; set; }
        public int id_fascicolo { get; set; }
        public int id_avvpag_rif_documenti { get; set; }
        public string barcodeNotifica { get; set; }
        public string destinatarioNotifica { get; set; }
        public string dataNotifica { get; set; }
        public string numeroRaccomandata { get; set; }
        public string NumeroAttoGiudiziario { get; set; }
        public bool IsAvvisoImmagine { get; set; }
        public bool IsNotificaImmagine { get; set; }
        public bool IsNotificaImmagineNotificata { get; set; }
        public bool IsConNotifica { get; set; }
        public string identificativo_doc_input { get; set; }
        public string identificativo_avv_pag_rif { get; set; }
        public string identificativo_avv_pag { get; set; }
        public bool isButtonAcquisisciNotifica { get; set; }
    }
}
