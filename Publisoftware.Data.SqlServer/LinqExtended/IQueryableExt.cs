using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class IQueryableExt
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> p_query, int p_totPage, int p_idxPage) where T : class
        {
            int v_totSpedNot = p_query.Count();
            int v_numPerPagina = (int)Math.Ceiling(v_totSpedNot / (double)p_totPage);

            return p_query.Skip((p_idxPage - 1) * v_numPerPagina).Take(v_numPerPagina);
        }
    }
}
