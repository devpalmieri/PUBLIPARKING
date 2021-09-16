using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_vincoli_beni_pignorati_light : BaseEntity<tab_vincoli_beni_pignorati_light>
    {
        public int id_tab_vincoli_beni { get; set; }
        public string descrizione { get; set; }
        public string DataAccensione { get; set; }
        public string nominativoRagSoc { get; set; }
        public string codicefiscPIva { get; set; }
        public string note { get; set; }
        public string DataScadenza { get; set; }
        public string importo_vincolato_Euro { get; set; }
    }
}
