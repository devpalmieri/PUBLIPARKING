using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_profilo_contribuente_new_light : BaseEntity<anagrafica_causale_light>
    {
        public int id_tab_profilo_contribuente { get; set; }
        public string descrizione { get; set; }
    }
}
