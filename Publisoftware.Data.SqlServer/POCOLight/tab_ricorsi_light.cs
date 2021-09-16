using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_ricorsi_light : BaseEntity<tab_ricorsi_light>
    {
        public int id_tab_ricorso { get; set; }
        public int id_tab_doc_input { get; set; }
        public int id_fascicolo { get; set; }
        public string nominativoContribuente { get; set; }
        public string data_presentazione_String { get; set; }
        public string identificativo_ricorso { get; set; }
        public string identificativo_avviso { get; set; }
        public string descrizione_avviso { get; set; }
        public string rgr { get; set; }
        public int id_stato { get; set; }
        public string stato { get; set; }
    }
}
