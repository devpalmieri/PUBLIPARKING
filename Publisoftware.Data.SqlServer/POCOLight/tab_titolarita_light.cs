using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_titolarita_light : BaseEntity<tab_titolarita_light>
    {
        public int id_tab_titolarita { get; set; }
        public string Proprietario { get; set; }
        public string Diritto { get; set; }
        public string DataInizio { get; set; }
        public string DataFine { get; set; }
        public string Quota { get; set; }
    }
}
