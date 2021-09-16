using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabProcedureLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_procedure> OrderByDefault(this IQueryable<tab_procedure> p_query)
        {
            return p_query.OrderBy(o => o.descrizione);
        }
    }
}
