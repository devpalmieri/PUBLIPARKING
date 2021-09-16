using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_carrello_light : BaseEntity<tab_carrello_light>
    {
        public int id_carrello { get; set; }
        public string tipo_carrello { get; set; }
        public string fonte_carrello { get; set; }
        public string tipo_versamento { get; set; }
        public string codice_carrello { get; set; }
        public string identificativo_carrello { get; set; }
        public DateTime? data_esecuzione_pagamento { get; set; }
        public string data_esecuzione_pagamento_String
        {
            get
            {
                if (data_esecuzione_pagamento.HasValue)
                {
                    return data_esecuzione_pagamento.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public decimal importo_totale_da_pagare { get; set; }
        public string importo_totale_da_pagare_Euro
        {
            get
            {
                return importo_totale_da_pagare.ToString("C");
            }
        }
       
        public string cf_piva_ente_creditore { get; set; }
        public string denominazione_ente_creditore { get; set; }

        public string cf_piva_versante { get; set; }
        public string cognome_ragsoc_versante{ get; set; }
        public string nome_versante { get; set; }
        public string indirizzo_versante { get; set; }
        public string cap_versante { get; set; }
        public string comune_versante { get; set; }
        public string nazione_versante { get; set; }
        public string cf_piva_contribuente_versante { get; set; }
        public decimal id_contribuente_versante { get; set; }
        public int id_referente_versante { get; set; }
        public string cf_piva_referente_versante { get; set; }
        public int id_terzo_versante { get; set; }
        public string cf_piva_terzo_versante { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public string descrizione_stato { get; set; }
        public string id_pagamento_gov_pay { get; set; }
        public string id_pagamento_session { get; set; }
        public int num_pagamenti { get; set; }
        public string num_pagamenti_String
        {
            get
            {
                return num_pagamenti.ToString();
                
            }
        }

        //---------------------------------------------------
    }
}
