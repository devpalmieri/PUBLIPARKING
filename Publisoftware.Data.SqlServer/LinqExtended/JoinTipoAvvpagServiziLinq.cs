using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTipoAvvpagServiziLinq
    {
        /// <summary>
        /// Filtro per tipo avviso
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idTipoAvvpag"></param>
        /// <returns></returns>
        public static IQueryable<join_tipo_avv_pag_servizi> WhereByIdTipoAvvpag(this IQueryable<join_tipo_avv_pag_servizi> p_query, int p_idTipoAvvpag)
        {
            return p_query.Where(j => j.id_tipo_avv_pag == p_idTipoAvvpag);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_tipo_avv_pag_servizi> OrderByDefault(this IQueryable<join_tipo_avv_pag_servizi> p_query)
        {
            return p_query.OrderBy(o => o.id_join_servizi_tipo_avv_pag);
        }
    }
}
