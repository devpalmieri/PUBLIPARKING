using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFunzionalitaLinq
    {
        /// <summary>
        /// Filtro per ID Procedura
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEntrata">ID </param>
        /// <returns></returns>
        public static IQueryable<tab_funzionalita> WhereByIdProcedura(this IQueryable<tab_funzionalita> p_query, int p_idProcedura)
        {
            return p_query.Where(w => w.id_tab_procedure == p_idProcedura);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_funzionalita> OrderByDefault(this IQueryable<tab_funzionalita> p_query)
        {
            return p_query.OrderBy(o => o.ordine);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IEnumerable<tab_funzionalita> OrderByDefault(this IEnumerable<tab_funzionalita> p_query)
        {
            return p_query.OrderBy(o => o.ordine);
        }
    }
}
