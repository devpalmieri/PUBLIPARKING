using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public class join_tab_carrello_tab_rate_light : BaseEntity<join_tab_carrello_tab_rate_light>
    {
        public int id_carrello { get; set; }
        public string tipo_carrello { get; set; }
        public string fonte_carrello { get; set; }
        public string tipo_versamento { get; set; }
        public string codice_carrello { get; set; }
        public string identificativo_carrello { get; set; }
        public string codice_contesto_pagamento_rpt { get; set; }

        public DateTime? data_ora_messaggio_richiesta_rpt { get; set; }
        public string identificativo_messaggio_richiesta_rpt { get; set; }
        public DateTime? data_esecuzione_pagamento_psp { get; set; }
        public decimal importo_totale_da_pagare { get; set; }

        public string importo_totale_da_pagare_Euro
        {
            get
            {
                return importo_totale_da_pagare.ToString("C");
            }
        }
        public string note_su_pagamento { get; set; }
        public string cf_piva_dominio_ente_creditore { get; set; }
        public string codice_ente_pagopa { get; set; }
        public string denominazione_ente_creditore { get; set; }
        public string identificativo_PSP { get; set; }
        public string stazione_PSP { get; set; }
        public string iban_appoggio { get; set; }
        public string bic_appoggio { get; set; }
        public string iban_addebito { get; set; }
        public string bic_addebito { get; set; }

        public string tipo_soggetto_versante { get; set; }
        public string cf_piva_versante { get; set; }
        public string cognome_ragsoc_versante { get; set; }
        public string nome_versante { get; set; }
        public string indirizzo_versante { get; set; }
        public string cap_versante { get; set; }
        public string comune_versante { get; set; }
        public string nazione_versante { get; set; }
        public string cf_piva_contribuente_versante { get; set; }
        public decimal id_contribuente_versante { get; set; }

        public string cf_piva_referente_versante { get; set; }

        public decimal id_referente_versante { get; set; }

        public string cf_piva_terzo_versante { get; set; }
        public decimal id_terzo_versante { get; set; }

        public string cod_stato_carrello { get; set; }
        //----------------------------------------------------------

        public string Descr_Avviso { get; set; }
        public string Identificativo_Avviso { get; set; }
        public int id_join_carrello_rate { get; set; }
        public int id_rata { get; set; }
        public int num_rata { get; set; }
        public string num_rata_String
        {
            get
            {
                if (num_rata == 0)
                {
                    return "UNICA";
                }
                else
                {
                    return num_rata.ToString();
                }
            }
        }

        public string IUV { get; set; }
        public int id_tab_avv_pag { get; set; }
        public decimal importo_da_pagare_rata { get; set; }

        public string importo_da_pagare_rata_Euro
        {
            get
            {
                return importo_da_pagare_rata.ToString("C");
            }
        }
        public decimal importo_rata_maggiorato_interessi { get; set; }

        public string importo_rata_maggiorato_interessi_Euro
        {
            get
            {
                return importo_rata_maggiorato_interessi.ToString("C");
            }
        }
        public decimal importo_rata_maggiorato_sanzioni { get; set; }

        public string importo_rata_maggiorato_sanzioni_Euro
        {
            get
            {
                return importo_rata_maggiorato_sanzioni.ToString("C");
            }
        }
        public decimal id_contribuente_debitore { get; set; }
        public string tipo_soggetto_debitore { get; set; }
        public string cf_piva_debitore { get; set; }
        public string cognome_ragsoc_debitore { get; set; }
        public string nome_debitore { get; set; }
        public string indirizzo_debitore { get; set; }
        public string cap_debitore { get; set; }
        public string comune_debitore { get; set; }

        public string nazione_debitore { get; set; }

        public string tipo_marca_da_bollo { get; set; }

        public string hashDocumento_digitale { get; set; }
        public string sigla_autonom_prov_soggetto_debitore { get; set; }

        public string cod_stato_rata { get; set; }
        public string descr_stato_rata
        {
            get
            {
                if (cod_stato_rata == anagrafica_stato_carrello.ANN_ANN)
                {
                    return "ANNULLATO";
                }
                else if (cod_stato_rata == anagrafica_stato_carrello.ANN_ERR)
                {
                    return "FALLITO";
                }
                else if (cod_stato_rata == anagrafica_stato_carrello.ATT_PGT || cod_stato_rata == anagrafica_stato_carrello.ATT_REN || cod_stato_rata == anagrafica_stato_carrello.SSP_REN)
                {
                    return "PAGATO";
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public decimal importo_rata_pagato { get; set; }

        public string importo_rata_pagato_Euro
        {
            get
            {
                return importo_rata_pagato.ToString("C");
            }
        }
        public string identificativo_rt { get; set; }
        public string iban_accredito { get; set; }

    }

}
