using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public interface Itab_dett_estratto_conto_mensile
    {
         int id_tab_dett_estratto_conto_mensile { get; set; }
         int id_tab_cc_riscossione { get; set; }
         int id_tab_estratto_conto { get; set; }
         Nullable<System.DateTime> data_operazione { get; set; }
         System.DateTime data_accredito { get; set; }
         System.DateTime data_valuta { get; set; }
         int id_anagrafica_casuale_estrconto { get; set; }
         Nullable<decimal> importo_dare { get; set; }
         Nullable<decimal> importo_avere { get; set; }
         Nullable<int> id_tab_cuas { get; set; }
         string modalita_pagamento { get; set; }
         int qta_bollettini { get; set; }
         string descrizione_completa { get; set; }
         string flag_smarrimento { get; set; }
         Nullable<int> id_stato { get; set; }
         string cod_stato { get; set; }
         System.DateTime data_stato { get; set; }
         int id_struttura_stato { get; set; }
         int id_risorsa_stato { get; set; }
         string flag_pagopa { get; set; }
         string stato_verifica_pagopa { get; set; }
         string nome_flusso_pagopa { get; set; }

    }



}
