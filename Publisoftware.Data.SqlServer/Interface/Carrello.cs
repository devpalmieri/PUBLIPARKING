using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public interface Itab_carrello
    {
        int id_carrello { get; set; }
        string tipo_carrello { get; set; }
        string fonte_carrello { get; set; }
        string tipo_versamento { get; set; }
        string codice_carrello { get; set; }
        string identificativo_carrello { get; set; }        
        Nullable<System.DateTime> data_esecuzione_pagamento_psp { get; set; }
        Nullable<decimal> importo_totale_da_pagare { get; set; }
        string note_su_pagamento { get; set; }
        string cf_piva_dominio_ente_creditore { get; set; }
        string codice_ente_pagopa { get; set; }
        string denominazione_ente_creditore { get; set; }       
        string iban_appoggio { get; set; }
        string bic_appoggio { get; set; }
        string iban_addebito { get; set; }
        string bic_addebito { get; set; }
        string tipo_soggetto_versante { get; set; }
        string cf_piva_versante { get; set; }
        string cognome_ragsoc_versante { get; set; }
        string nome_versante { get; set; }
        string indirizzo_versante { get; set; }
        string cap_versante { get; set; }
        string comune_versante { get; set; }
        string nazione_versante { get; set; }
        string cf_piva_contribuente_versante { get; set; }
        Nullable<decimal> id_contribuente_versante { get; set; }
        string cf_piva_referente_versante { get; set; }
        Nullable<int> id_referente_versante { get; set; }
        string cf_piva_terzo_versante { get; set; }
        Nullable<int> id_terzo_versante { get; set; }
        string cod_stato { get; set; }
        DateTime data_stato { get; set; }
        int id_struttura_stato { get; set; }
        int id_risorsa_stato { get; set; }

        string id_pagamento_session { get; set; }
        string id_pagamento_govpay { get; set; }

    }


    public interface Ijoin_tab_carrello_tab_rate
    {
        int id_join_carrello_rate { get; set; }
        int id_carrello { get; set; }
        int id_rata { get; set; }
        Nullable<decimal> importo_da_pagare_rata { get; set; }
        string bic_accredito { get; set; }
        string iban_accredito { get; set; }
        Nullable<decimal> importo_rata_maggiorato_interessi { get; set; }
        Nullable<decimal> importo_rata_maggiorato_sanzioni { get; set; }
        Nullable<decimal> id_contribuente_debitore { get; set; }
        string tipo_soggetto_debitore { get; set; }
        string cf_piva_debitore { get; set; }
        string cognome_ragsoc_debitore { get; set; }
        string nome_debitore { get; set; }
        string indirizzo_debitore { get; set; }
        string cap_debitore { get; set; }
        string comune_debitore { get; set; }
        string nazione_debitore { get; set; }
        string tipo_marca_da_bollo { get; set; }
        string hashDocumento_digitale { get; set; }
        string sigla_autonom_prov_soggetto_debitore { get; set; }
        string cod_stato { get; set; }
        DateTime data_stato { get; set; }
        int id_struttura_stato { get; set; }
        int id_risorsa_stato { get; set; }
    }

}
