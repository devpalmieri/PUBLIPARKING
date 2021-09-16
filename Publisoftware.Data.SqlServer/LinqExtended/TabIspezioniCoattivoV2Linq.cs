using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabIspezioniCoattivoV2Linq
    {
        public static IQueryable<tab_ispezioni_coattivo_v2> OrderByDefault(this IQueryable<tab_ispezioni_coattivo_v2> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_ispezione_coattivo);
        }
    }
}
