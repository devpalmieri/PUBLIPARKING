using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_applicazioni_light : BaseEntity<tab_applicazioni_light>
    {
        public int id_tab_applicazioni { get; set; }
        public string codice { get; set; }
        public string descrizione { get; set; }
        public int ordine { get; set; }
        public string Abilitato { get; set; }
    }
}
