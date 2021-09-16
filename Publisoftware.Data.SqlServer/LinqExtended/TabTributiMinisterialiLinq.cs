using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTributiMinisterialiLinq
    {
        public static IQueryable<tab_tributi_ministeriali> WhereByCodiceTributoMinisteriale(this IQueryable<tab_tributi_ministeriali> p_query, string p_codice)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codice));
        }

        public static IQueryable<tab_tributi_ministeriali> OrderByDefault(this IQueryable<tab_tributi_ministeriali> p_query)
        {
            return p_query.OrderBy(e => e.codice_tributo);
        }
    }
}
