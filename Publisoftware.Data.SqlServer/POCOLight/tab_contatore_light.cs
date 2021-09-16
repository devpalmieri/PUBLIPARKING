using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_contatore_light
    {
        public int id_contatore { get; set; }
        public string matricola { get; set; }
        public string cod_stato { get; set; }
        public DateTime data_stato { get; set; }
    }
}
