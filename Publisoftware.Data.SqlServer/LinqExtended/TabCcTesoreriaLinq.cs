using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCcTesoreriaLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_cc_tesoreria> OrderByDefault(this IQueryable<tab_cc_tesoreria> p_query)
        {
            return p_query.OrderBy(o => o.num_cc);
        }
    }
}
