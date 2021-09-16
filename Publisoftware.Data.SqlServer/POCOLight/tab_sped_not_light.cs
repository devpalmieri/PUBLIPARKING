using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_sped_not_light : BaseEntity<tab_sped_not_light>
    {
        public int id_sped_not { get; set; }
        public string barcode { get; set; }
        public string TipoSpedizione { get; set; }
        public string TipoLista { get; set; }
        public string Stato { get; set; }
        public string EsitoNotifica { get; set; }
        public string data_esito_notifica_String { get; set; }
        public string Indirizzo { get; set; }
        public string Destinatario { get; set; }
        public string flag_spedizione_notifica { get; set; }
        public bool isAvvisoImmaginePresente { get; set; }
        public int id_stato_sped_not { get; set; }
        public bool isAvvisoImmagineVisibile { get; set; }
    }
}
