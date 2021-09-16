using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabZoneLinq
    {
        /// <summary>
        /// Filtro ID Ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Ente Ricercata</param>
        /// <returns></returns>
        public static IQueryable<tab_zone> WhereByIdZona(this IQueryable<tab_zone> p_query, int p_idZona)
        {
            return p_query.Where(w => w.id_ente == p_idZona);
        }

        /// <summary>
        /// Filtro ID Entrata
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEntrata">ID Entrata Ricercata</param>
        /// <returns></returns>
        public static IQueryable<tab_zone> WhereByIdEntrata(this IQueryable<tab_zone> p_query, int p_idEntrata)
        {
            return p_query.Where(w => w.id_entrata == p_idEntrata);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_zone> OrderByDefault(this IQueryable<tab_zone> p_query)
        {
            return p_query.OrderBy(o => o.cod_comune).ThenBy(o => o.id_entrata).ThenBy(o => o.descrizione_zona);
        }
    }
}
