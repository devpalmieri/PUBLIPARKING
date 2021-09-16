using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class ANAGRAFICA_ABI_CAB_light : BaseEntity<ANAGRAFICA_ABI_CAB_light>
    {
        public int ID_ABI_CAB { get; set; }
        public string ABI { get; set; }
        public string CAB { get; set; }
        public string BANCA { get; set; }
        public string AGENZIA { get; set; }
    }
}
