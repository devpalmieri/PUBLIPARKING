using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinIngfisSupervisioneLinq
    {
        public static IQueryable<join_ingfis_supervisione> OrderByDefault(this IQueryable<join_ingfis_supervisione> p_query)
        {
            return p_query.OrderBy(o => o.id_join_ingfis_supervisione);
        }
    }
}
