using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_strutture_aziendali_light : BaseEntity<anagrafica_strutture_aziendali_light>
    {
        public int id_struttura_aziendale { get; set; }
        public string descr_struttura { get; set; }
        public string codice_struttura_aziendale { get; set; }
        public string provincia { get; set; }
    }
}
