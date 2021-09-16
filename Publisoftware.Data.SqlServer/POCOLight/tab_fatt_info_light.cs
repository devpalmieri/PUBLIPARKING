using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_fatt_info_light
    {
        public int id_tab_fatt_info { get; set; }
        public int id_lettura { get; set; }
        public decimal quantita_lettura { get; set; }
        public string data_lettura { get; set; }
    }
}
