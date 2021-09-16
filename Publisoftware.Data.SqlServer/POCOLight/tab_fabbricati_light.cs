using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_fabbricati_light : BaseEntity<tab_fabbricati_light>
    {
        public int id_tab_fabbricati { get; set; }
        public int progressivo { get; set; }
        public string Descrizione { get; set; }
        public string classe { get; set; }
        public string Superficie { get; set; }
        public string Rendita { get; set; }
        public string SuperficieTarsu { get; set; }
        public string Ubicazione { get; set; }
        public string DataInizio { get; set; }
        public string DataFine { get; set; }
    }
}
