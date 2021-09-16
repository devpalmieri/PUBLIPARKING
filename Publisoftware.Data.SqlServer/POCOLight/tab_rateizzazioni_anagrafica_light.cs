using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_rateizzazioni_anagrafica_light
    {
        public int id_rateizzazioni { get; set; }
        public int id_tipo_avvpag { get; set; }
        public string tipo_avvpag_codice { get; set; }
        public string tipo_avvpag_descrizione { get; set; }
        public string tipo_contribuente { get; set; }
        public string desc_tipo_rateizzazione { get; set; }
        public decimal importo_min_da_pagare { get; set; }
        public decimal? importo_max_da_pagare { get; set; }
        public int ? numero_rate_min { get; set; }
        public int numero_rate_max { get; set; }
        public decimal importo_min_rata { get; set; }
        public decimal? importo_min_reddito { get; set; }
        public decimal? importo_max_reddito { get; set; }

    }
}
