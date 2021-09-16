using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_referente_contribuente_light
    {
        public int id_join_referente_contribuente { get; set; }
        public decimal id_anag_contribuente { get; set; }
        public int id_tab_referente { get; set; }
        public string referenteDisplay { get; set; }
        public string importoMaxObbligazione { get; set; }
        public decimal coobbligazione_percentuale { get; set; }
        public string desc_tipo_relazione { get; set; }
        public string descrizione_parentela { get; set; }
        public string coobbligato { get; set; }
        public string data_inizio_validita_String { get; set; }
        public string data_fine_validita_String { get; set; }
    }
}
