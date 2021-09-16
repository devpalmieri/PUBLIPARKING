using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_agevolazione_light : BaseEntity<anagrafica_causale_light>
    {
        public int id_anagrafica_agevolazione { get; set; }
        public string cod_agevolazione { get; set; }
        public string des_agevolazione { get; set; }
        public string tipo_base_calcolo { get; set; }
    }
}
