using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_letture_light : BaseEntity<tab_letture_light>
    {
        public int id_lettura { get; set; }
        public string DataLettura { get; set; }
        public string Lettura { get; set; }
        public string TipoLettura { get; set; }
    }
}
