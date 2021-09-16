using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabModalitaRateAvvPagLinq
    {
        /// <summary>
        /// Filtro per ID Ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Ente</param>
        /// <returns></returns>
        public static IQueryable<tab_modalita_rate_avvpag> WhereByIdEnte(this IQueryable<tab_modalita_rate_avvpag> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte);
        }

        public static IQueryable<tab_modalita_rate_avvpag> WhereByIdEnteGestito(this IQueryable<tab_modalita_rate_avvpag> p_query, int p_idEnteGestito)
        {
            return p_query.Where(ac => ac.id_ente_gestito == p_idEnteGestito);
        }

        public static IQueryable<tab_modalita_rate_avvpag> WhereByIdTipoAvvPag(this IQueryable<tab_modalita_rate_avvpag> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.id_tipo_avv_pag == p_idTipoAvvPag);
        }

        public static IQueryable<tab_modalita_rate_avvpag> WhereByIdTipoServizio(this IQueryable<tab_modalita_rate_avvpag> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio == p_idServizio);
        }

        public static IQueryable<tab_modalita_rate_avvpag> WhereByIdServizio(this IQueryable<tab_modalita_rate_avvpag> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == p_idServizio);
        }

        public static IQueryable<tab_modalita_rate_avvpag> WhereByRangeValiditaOdierno(this IQueryable<tab_modalita_rate_avvpag> p_query)
        {
            return p_query.Where(d => d.periodo_validita_da <= DateTime.Now
                                   && d.periodo_validita_a >= DateTime.Now);
        }
    }
}
