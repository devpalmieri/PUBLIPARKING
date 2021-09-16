using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class SerStatiEsteriNewLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<ser_stati_esteri_new> OrderByDefault(this IQueryable<ser_stati_esteri_new> p_query)
        {
            return p_query.OrderBy(o => o.denominazione_stato);
        }
    }
}
