using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTabCarrelloTabRateLinq
    {
        public static IQueryable<join_tab_carrello_tab_rate> WhereByIdCarrello(this IQueryable<join_tab_carrello_tab_rate> p_query, int p_idCarrello)
        {
            return p_query.Where(d => d.id_carrello == p_idCarrello);
        }

        public static IQueryable<join_tab_carrello_tab_rate> WhereByIdRata(this IQueryable<join_tab_carrello_tab_rate> p_query, int p_idRata)
        {
            return p_query.Where(d => d.id_rata == p_idRata);
        }

        public static IQueryable<join_tab_carrello_tab_rate> WhereByCodStato(this IQueryable<join_tab_carrello_tab_rate> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_tab_carrello_tab_rate> WhereByCodStatoNot(this IQueryable<join_tab_carrello_tab_rate> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_tab_carrello_tab_rate> WhereByAncoraDaPagare(this IQueryable<join_tab_carrello_tab_rate> p_query)
        {
            //return p_query.Where(d => !d.importo_pagato_rata.HasValue ||
            //                          (d.importo_pagato_rata.HasValue && d.importo_pagato_rata.Value < d.importo_da_pagare_rata.Value));
            return p_query.Where(d => d.importo_da_pagare_rata.HasValue && d.importo_da_pagare_rata.Value > 0);
        }
        public static IQueryable<join_tab_carrello_tab_rate_light> ToLight(this IQueryable<join_tab_carrello_tab_rate> iniziale)
        {
            DateTime? dtNullable = null;
            return iniziale.ToList().Select(d => new join_tab_carrello_tab_rate_light
            {
                id_carrello = d.id_carrello,
                bic_addebito = d.tab_carrello.bic_addebito,
                bic_appoggio = d.tab_carrello.bic_appoggio,
                cap_debitore = d.cap_debitore,
                cap_versante = d.tab_carrello.cap_versante,
                cf_piva_contribuente_versante = d.tab_carrello.cf_piva_contribuente_versante,
                cf_piva_debitore = d.cf_piva_debitore,
                cf_piva_dominio_ente_creditore = d.tab_carrello.cf_piva_dominio_ente_creditore,
                cf_piva_referente_versante = d.tab_carrello.cf_piva_referente_versante,
                cf_piva_terzo_versante = d.tab_carrello.cf_piva_terzo_versante,
                cf_piva_versante = d.tab_carrello.cf_piva_versante,
                codice_carrello = d.tab_carrello.codice_carrello,
                //Modifica del 19/10/2020
                //Per la chiamata a GovPay il campo "codice_contesto_pagamento_rpt" è stato rimosso
                //codice_contesto_pagamento_rpt = d.tab_carrello.codice_contesto_pagamento_rpt,
                codice_ente_pagopa = d.tab_carrello.codice_ente_pagopa,
                cod_stato_carrello = d.tab_carrello.cod_stato,
                cod_stato_rata = d.cod_stato,
                cognome_ragsoc_debitore = d.cognome_ragsoc_debitore,
                cognome_ragsoc_versante = d.tab_carrello.cognome_ragsoc_versante,
                comune_debitore = d.comune_debitore,
                comune_versante = d.tab_carrello.comune_versante,
                denominazione_ente_creditore = d.tab_carrello.denominazione_ente_creditore,
                fonte_carrello = d.tab_carrello.fonte_carrello,
                data_esecuzione_pagamento_psp = d.tab_carrello.data_esecuzione_pagamento_psp.HasValue ? d.tab_carrello.data_esecuzione_pagamento_psp.Value : dtNullable,
                //Modifica del 19/10/2020
                //Per la chiamata a GovPay il campo "data_ora_messaggio_richiesta_rpt" è stato rimosso
                //data_ora_messaggio_richiesta_rpt = d.tab_carrello.data_ora_messaggio_richiesta_rpt.HasValue ? d.tab_carrello.data_ora_messaggio_richiesta_rpt.Value : dtNullable,
                hashDocumento_digitale = d.hashDocumento_digitale,
                iban_addebito = d.tab_carrello.iban_addebito,
                iban_appoggio = d.tab_carrello.iban_appoggio,
                identificativo_carrello = d.tab_carrello.identificativo_carrello,
                //Modifica del 19/10/2020
                //Per la chiamata a GovPay i campi "identificativo_messaggio_richiesta_rpt" e "identificativo_PSP" sono stati rimossi
                //data_ora_messaggio_richiesta_rpt = d.tab_carrello.data_ora_messaggio_richiesta_rpt.HasValue ? d.tab_carrello.data_ora_messaggio_richiesta_rpt.Value : dtNullable,
                //identificativo_messaggio_richiesta_rpt = d.tab_carrello.identificativo_messaggio_richiesta_rpt,
                //identificativo_PSP=d.tab_carrello.identificativo_PSP,
                id_contribuente_debitore = d.id_contribuente_debitore,
                id_contribuente_versante = d.tab_carrello.id_contribuente_versante.HasValue ? d.tab_carrello.id_contribuente_versante.Value : 0,
                id_join_carrello_rate = d.id_join_carrello_rate,
                id_rata = d.id_rata,
                id_referente_versante = d.tab_carrello.id_referente_versante.HasValue ? d.tab_carrello.id_referente_versante.Value : 0,
                id_terzo_versante = d.tab_carrello.id_terzo_versante.HasValue ? d.tab_carrello.id_terzo_versante.Value : 0,
                importo_da_pagare_rata = d.importo_da_pagare_rata.HasValue ? d.importo_da_pagare_rata.Value : 0,
                importo_rata_maggiorato_interessi = d.importo_rata_maggiorato_interessi.HasValue ? d.importo_rata_maggiorato_interessi.Value : 0,
                importo_rata_maggiorato_sanzioni = d.importo_rata_maggiorato_sanzioni.HasValue ? d.importo_rata_maggiorato_sanzioni.Value : 0,
                importo_totale_da_pagare = d.tab_carrello.importo_totale_da_pagare.HasValue ? d.tab_carrello.importo_totale_da_pagare.Value : 0,
                indirizzo_debitore = d.indirizzo_debitore,
                indirizzo_versante = d.tab_carrello.indirizzo_versante,
                nazione_debitore = d.nazione_debitore,
                nazione_versante = d.tab_carrello.nazione_versante,
                nome_debitore = d.nome_debitore,
                nome_versante = d.tab_carrello.nome_versante,
                note_su_pagamento = d.tab_carrello.note_su_pagamento,
                sigla_autonom_prov_soggetto_debitore = d.sigla_autonom_prov_soggetto_debitore,
                //Modifica del 19/10/2020
                //Per la chiamata a GovPay il campo "stazione_PSP" è stato rimosso
                //stazione_PSP = d.tab_carrello.stazione_PSP,
                tipo_carrello = d.tab_carrello.tipo_carrello,
                tipo_marca_da_bollo = d.tipo_marca_da_bollo,
                tipo_soggetto_debitore = d.tipo_soggetto_debitore,
                tipo_soggetto_versante = d.tab_carrello.tipo_soggetto_versante,
                tipo_versamento = d.tab_carrello.tipo_versamento,
                id_tab_avv_pag = d.tab_rata_avv_pag.id_tab_avv_pag,
                IUV = d.tab_rata_avv_pag.Iuv_identificativo_pagamento,
                Descr_Avviso = d.tab_rata_avv_pag.tab_avv_pag.identificativo_avv_pag + " - " + d.tab_rata_avv_pag.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag,
                Identificativo_Avviso = d.tab_rata_avv_pag.tab_avv_pag.identificativo_avv_pag,
                num_rata = d.tab_rata_avv_pag.num_rata,
                identificativo_rt = d.identificativo_rt,
                importo_rata_pagato = d.importo_da_pagare_rata.HasValue ? d.importo_da_pagare_rata.Value : 0,
                iban_accredito = d.iban_accredito,




            }).AsQueryable();
        }

        public static IQueryable<join_tab_carrello_tab_rate> OrderByDefault(this IQueryable<join_tab_carrello_tab_rate> p_query)
        {
            return p_query.OrderBy(o => o.id_rata);
        }

    }
}
