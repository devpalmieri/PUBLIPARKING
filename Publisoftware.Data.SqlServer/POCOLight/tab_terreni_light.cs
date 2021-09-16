using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_terreni_light : BaseEntity<tab_terreni_light>
    {
        public int id_tab_terreni { get; set; }
        public int progressivo { get; set; }
        public string Descrizione { get; set; }
        public string classe { get; set; }
        public int? ettari { get; set; }
        public int? are { get; set; }
        public int? centiare { get; set; }
        public string RedditoDominicale { get; set; }
        public string RedditoAgrario { get; set; }
        public string DataInizio { get; set; }
        public string DataFine { get; set; }
    }
}
