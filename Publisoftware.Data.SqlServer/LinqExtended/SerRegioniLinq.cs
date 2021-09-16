using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class SerRegioniLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<ser_regioni> OrderByDefault(this IQueryable<ser_regioni> p_query)
        {
            return p_query.OrderBy(o => o.des_regione);
        }
    }
}
