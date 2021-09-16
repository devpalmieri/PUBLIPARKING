using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_categoria_tariffaria_cosap_light : BaseEntity<tab_categoria_tariffaria_cosap_light>
    {
        public int id_categoria_tariffaria { get; set; }
        public int anno { get; set; }
        public string entrata { get; set; }
        public string Tariffa { get; set; }
    }
}
