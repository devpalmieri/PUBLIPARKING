using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinEnteEnteGestitoLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_ente_ente_gestito> OrderByDefault(this IQueryable<join_ente_ente_gestito> p_query)
        {
            return p_query.OrderBy(e => e.anagrafica_ente_gestito.descrizione_ente);
        }

        /// <summary>
        /// Filtro per record Attivi
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_ente_ente_gestito> WhereAttivo(this IQueryable<join_ente_ente_gestito> p_query)
        {
            return p_query.Where(d => d.cod_stato == join_ente_ente_gestito.ATT_ATT);
        }

        /// <summary>
        /// Filtro per idEnte
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_ente_ente_gestito> WhereByIdEnte(this IQueryable<join_ente_ente_gestito> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }
    }
}
