using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRateizzazioniLinq
    {
        public static IQueryable<tab_rateizzazioni> WhereByIdEntrata(this IQueryable<tab_rateizzazioni> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdEntrataNull(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => d.id_entrata == null);
        }

        public static IQueryable<tab_rateizzazioni> WhereByTipoContribuente(this IQueryable<tab_rateizzazioni> p_query, string p_siglaTipoContribuente)
        {
            return p_query.Where(d => d.tipo_contribuente == p_siglaTipoContribuente);
        }

        public static IQueryable<tab_rateizzazioni> WhereByTipoContribuenteNull(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => d.tipo_contribuente == null);
        }

        public static IQueryable<tab_rateizzazioni> WhereByRangeValiditaOdierno(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => (d.data_inizio_validita_rateizzazione.HasValue ? d.data_inizio_validita_rateizzazione.Value : DateTime.MinValue) <= DateTime.Now
                                   && (d.data_fine_validita_rateizzazione.HasValue ? d.data_fine_validita_rateizzazione.Value : DateTime.MaxValue) >= DateTime.Now);
        }

        public static IQueryable<tab_rateizzazioni> WhereByRangeDataApprovazioneLista(this IQueryable<tab_rateizzazioni> p_query, DateTime p_dataApprovazione)
        {
            return p_query.Where(d => (d.data_inizio_validita_rateizzazione.HasValue ? d.data_inizio_validita_rateizzazione.Value : DateTime.MinValue) <= p_dataApprovazione
                                   && (d.data_fine_validita_rateizzazione.HasValue ? d.data_fine_validita_rateizzazione.Value : DateTime.MaxValue) >= p_dataApprovazione);
        }

        public static IQueryable<tab_rateizzazioni> WhereByRangeImportiDaPagare(this IQueryable<tab_rateizzazioni> p_query, decimal p_valore)
        {
            return p_query.Where(d => d.importo_min_da_pagare <= p_valore
                                        && (d.importo_max_da_pagare.HasValue ? d.importo_max_da_pagare.Value : decimal.MaxValue) >= p_valore);
        }

        public static IQueryable<tab_rateizzazioni> WhereByRangeReddito(this IQueryable<tab_rateizzazioni> p_query, decimal p_valore)
        {
            return p_query.Where(d => (d.importo_min_reddito.HasValue ? d.importo_min_reddito.Value : decimal.MinValue) <= p_valore &&
                                      (d.importo_max_reddito.HasValue ? d.importo_max_reddito.Value : decimal.MaxValue) >= p_valore);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdTipoServizio(this IQueryable<tab_rateizzazioni> p_query, int p_id)
        {
            return p_query.Where(d => d.id_tipo_servizio == p_id);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdTipoServizioNull(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => d.id_tipo_servizio == null);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdTipoServizioAttoRateizzato(this IQueryable<tab_rateizzazioni> p_query, int p_id)
        {
            return p_query.Where(d => d.id_tipo_servizio_atto_rateizzato == p_id);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdTipoServizioAttoRateizzatoNull(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => d.id_tipo_servizio_atto_rateizzato == null);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdTipoAvvPag(this IQueryable<tab_rateizzazioni> p_query, int p_id)
        {
            return p_query.Where(d => d.id_tipo_avvpag == p_id);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdTipoAvvPagNull(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => d.id_tipo_avvpag == null);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdAnagraficaCausale(this IQueryable<tab_rateizzazioni> p_query, int p_id)
        {
            return p_query.Where(d => d.id_anagrafica_causale == p_id);
        }

        public static IQueryable<tab_rateizzazioni> WhereByIdAnagraficaCausaleNull(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.Where(d => d.id_anagrafica_causale == null);
        }

        public static IQueryable<tab_rateizzazioni> OrderByDefault(this IQueryable<tab_rateizzazioni> p_query)
        {
            return p_query.OrderBy(d => d.id_rateizzazioni);
        }

        public static IQueryable<tab_rateizzazioni_light> ToLight(this IQueryable<tab_rateizzazioni> iniziale)
        {
            return iniziale.Select(d => new tab_rateizzazioni_light
            {
                id_rateizzazioni = d.id_rateizzazioni,
                desc_tipo_rateizzazione = d.desc_tipo_rateizzazione
            }).AsQueryable();
        }
    }
}
