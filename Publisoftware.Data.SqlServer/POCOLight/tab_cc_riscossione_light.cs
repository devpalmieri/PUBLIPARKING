using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_cc_riscossione_light : BaseEntity<tab_cc_riscossione_light>
    {
        public int id_tab_cc_riscossione { get; set; }
        public string Banca { get; set; }
        public string NumCC { get; set; }
        public string IntestazioneCC { get; set; }
    }
}
