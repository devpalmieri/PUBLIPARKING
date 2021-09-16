using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_agevolazioni_light
    {
        public int id_tab_agevolazioni { get; set; }
        public string codice { get; set; }
        public string descrizione { get; set; }
        public string tipo { get; set; }
        public string macrocategoria { get; set; }
        public string percentuale { get; set; }
        public string periodo_validita_String { get; set; }
        public string stato { get; set; }
        public string riduzione { get; set; }
        public string rinnovo { get; set; }
        public string occupante { get; set; }
    }
}
