using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCarrelloLinq
    {
        public static IQueryable<tab_carrello> WhereByCodice(this IQueryable<tab_carrello> p_query, string p_codice)
        {
            return p_query.Where(d => d.codice_carrello == p_codice);
        }

        //il dottore ha tolto il campo id_tab_cc_riscossione dalla tab_carrello
        //public static IQueryable<tab_carrello> WhereByIdCCRiscossione(this IQueryable<tab_carrello> p_query, int p_idCCRiscossione)
        //{            
        //    return p_query.Where(d => d.id_tab_cc_riscossione == p_idCCRiscossione);
        //}

        public static IQueryable<tab_carrello> WhereByCodStato(this IQueryable<tab_carrello> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_carrello> WhereByCodStatoNot(this IQueryable<tab_carrello> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_carrello> WhereByCodStato(this IQueryable<tab_carrello> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_carrello> WhereByCodStatoNot(this IQueryable<tab_carrello> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => !p_codStatoList.Contains(d.cod_stato));
        }
        public static IQueryable<tab_carrello> WhereByIdRataAndStato(this IQueryable<tab_carrello> p_query, int p_id_rata)
        {
            return p_query.Where(d => d.cod_stato == tab_carrello.ATT_RPT && d.join_tab_carrello_tab_rate.FirstOrDefault() != null && d.join_tab_carrello_tab_rate.FirstOrDefault().id_rata == p_id_rata);

        }
        public static IQueryable<tab_carrello> WhereByIdRataAndStatoAndFonte(this IQueryable<tab_carrello> p_query, int p_id_rata)
        {
            return p_query.Where(d => d.cod_stato == tab_carrello.ATT_RPT && d.fonte_carrello == tab_carrello.FONTE_CARRELLO_PSP && d.join_tab_carrello_tab_rate.FirstOrDefault() != null && d.join_tab_carrello_tab_rate.FirstOrDefault().id_rata == p_id_rata);

        }
        public static IQueryable<tab_carrello> WhereByImportoPagato(this IQueryable<tab_carrello> p_query, decimal? p_importo)
        {
            if (p_importo.HasValue)
            {
                //Il dottore ha voluto filtrare per l'importo esatto
                //return p_query.Where(d => System.Math.Abs((System.Math.Round((d.importo_mov_pagato.HasValue ? d.importo_mov_pagato.Value : 0) - p_importo.Value))) <= 1);
                return p_query.Where(d => d.importo_totale_da_pagare.Value == p_importo);
            }
            else
            {
                return p_query;
            }
        }
        public static IList<tab_carrello_light> ToLight(this IQueryable<tab_carrello> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_carrello_light
            {
                identificativo_carrello = d.identificativo_carrello,
                cap_versante = d.cap_versante,
                cf_piva_contribuente_versante = d.cf_piva_contribuente_versante,
                cf_piva_ente_creditore = d.cf_piva_dominio_ente_creditore,
                cf_piva_referente_versante = d.cf_piva_referente_versante,
                cf_piva_terzo_versante = d.cf_piva_terzo_versante,
                cf_piva_versante = d.cf_piva_versante,
                codice_carrello = d.codice_carrello,
                cognome_ragsoc_versante = d.cognome_ragsoc_versante,
                comune_versante = d.comune_versante,
                data_esecuzione_pagamento = d.data_esecuzione_pagamento_psp,
                denominazione_ente_creditore = d.denominazione_ente_creditore,
                descrizione_stato = d.cod_stato == anagrafica_stato_carrello.ATT_PGT ? "PAGATO" : d.cod_stato == "ANN-ANN" ? "ANNULLATO" : d.cod_stato == "ANN-ERR" ? "FALLITO" : "",
                fonte_carrello = d.fonte_carrello == "WEB" ? "PORTALE" : "SPORTELLO",
                id_carrello = d.id_carrello,
                id_contribuente_versante = d.id_contribuente_versante.HasValue ? d.id_contribuente_versante.Value : 0,
                id_pagamento_gov_pay = d.id_pagamento_govpay,
                id_pagamento_session = d.id_pagamento_session,
                id_referente_versante = d.id_referente_versante.HasValue ? d.id_referente_versante.Value : 0,
                id_stato = d.id_stato.HasValue ? d.id_stato.Value : 0,
                id_terzo_versante = d.id_terzo_versante.HasValue ? d.id_terzo_versante.Value : 0,
                importo_totale_da_pagare = d.importo_totale_da_pagare.HasValue ? d.importo_totale_da_pagare.Value : 0,
                indirizzo_versante = d.indirizzo_versante,
                nazione_versante = d.nazione_versante,
                nome_versante = d.nome_versante,
                num_pagamenti = (from c in d.join_tab_carrello_tab_rate where c.id_carrello == d.id_carrello select c).Count(),
                tipo_carrello = d.tipo_carrello,
                tipo_versamento = d.tipo_versamento


            }).ToList();
        }

    }
}
