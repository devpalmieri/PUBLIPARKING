using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_fattcont_fattconsumi_light
    {
        public int id_join_fattcont_fattconsumi { get; set; }
        public string matricola_contatore { get; set; }
        public int id_lettura_1 { get; set; }
        public string data_lettura_1_String { get; set; }
        public decimal qta_lettura_1 { get; set; }
        public int id_lettura_2 { get; set; }
        public string data_lettura_2_String { get; set; }
        public decimal qta_lettura_2 { get; set; }
        public string letturaRiferimento1 { get; set; }
        public string letturaRiferimento2 { get; set; }
        public int num_giorni_lettura { get; set; }
        public decimal qta_cons_lettura { get; set; }
        public decimal qta_prodie_lettura { get; set; }
    }
}
