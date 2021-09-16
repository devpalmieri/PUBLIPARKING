using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_fatt_consumi_light
    {
        public int id_tab_fatt_consumi { get; set; }
        public string periodo_contribuzione_da_String { get; set; }
        public string periodo_contribuzione_a_String { get; set; }
        public string CategoriaTariffaria { get; set; }
        public int num_giorni_contribuzione { get; set; }
        public decimal qta_contribuzione { get; set; }
        public string tipoAddebito { get; set; }        
    }
}
