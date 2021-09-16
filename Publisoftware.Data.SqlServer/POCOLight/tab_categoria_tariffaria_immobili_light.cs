using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_categoria_tariffaria_immobili_light : BaseEntity<tab_categoria_tariffaria_immobili_light>
    {
        public int id_categoria_tariffaria_immobili { get; set; }
        public int anno { get; set; }
        public string entrata { get; set; }
        public string utilizzo { get; set; }
        public string rivalutazione { get; set; }
        public string aliquotaridotta1 { get; set; }
        public string aliquotaridotta2 { get; set; }
        public string moltiplicatore { get; set; }
        public string esente { get; set; }
    }
}
