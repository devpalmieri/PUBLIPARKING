using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabEsclusioneRateizzazioneLinq
    {
        public static IQueryable<tab_esclusione_rateizzazione> WhereByIdEnte(this IQueryable<tab_esclusione_rateizzazione> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_esclusione_rateizzazione> WhereByIdEntrata(this IQueryable<tab_esclusione_rateizzazione> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_esclusione_rateizzazione> WhereByIdTipoServizio(this IQueryable<tab_esclusione_rateizzazione> p_query, int p_idTipoServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio == p_idTipoServizio);
        }

        public static IQueryable<tab_esclusione_rateizzazione> WhereByRangeValiditaOdierno(this IQueryable<tab_esclusione_rateizzazione> p_query)
        {
            return p_query.Where(d => (d.data_inizio_validita.HasValue ? d.data_inizio_validita.Value : DateTime.MinValue) <= DateTime.Now
                                   && (d.data_fine_validita.HasValue ? d.data_fine_validita.Value : DateTime.MaxValue) >= DateTime.Now);
        }

        public static IQueryable<tab_esclusione_rateizzazione> WhereByRangeDataApprovazioneLista(this IQueryable<tab_esclusione_rateizzazione> p_query, DateTime p_dataApprovazione)
        {
            return p_query.Where(d => (d.data_inizio_validita.HasValue ? d.data_inizio_validita.Value : DateTime.MinValue) <= p_dataApprovazione
                                   && (d.data_fine_validita.HasValue ? d.data_fine_validita.Value : DateTime.MaxValue) >= p_dataApprovazione);
        }
    }
}
