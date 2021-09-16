using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_rateizzazioni_light : BaseEntity<tab_rateizzazioni_light>
    {
        public int id_rateizzazioni { get; set; }
        public string desc_tipo_rateizzazione { get; set; }
        public string cod_tipo_rateizzazione { get; set; }

        public int numero_rate_min { get; set; }
        public int numero_rate_max { get; set; }
        public int periodicita_consentita { get; set; }
        public string desc_periodicita_consentita { get; set; }
        public string importo_min_rata_string { get; set; }
        public string importo_max_rata_string { get; set; }

        public string importo_min_da_pagare_string { get; set; }
        public string importo_max_da_pagare_string { get; set; }
        public string importo_min_reddito_string { get; set; }
        public string importo_max_reddito_string { get; set; }
        public DateTime data_inizio_validita_rateizzazione { get; set; }
        public DateTime data_fine_validita_rateizzazione { get; set; }

        public string data_inizio_validita_rateizzazione_String { get; set; }
        public string data_fine_validita_rateizzazione_String { get; set; }
        public int numero_rate_min_interessi { get; set; }

        public int id_tab_avvpag { get; set; }
        public string descr_tipo_avv_pag { get; set; }
        public string descr_tipo_bene { get; set; }
    }
}
