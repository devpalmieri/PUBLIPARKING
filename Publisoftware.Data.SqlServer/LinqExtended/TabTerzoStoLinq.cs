using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTerzoStoLinq
    {
        public static IQueryable<tab_terzo_sto> WhereByIdTerzo(this IQueryable<tab_terzo_sto> p_query, int p_idTerzo)
        {
            return p_query.Where(d => d.id_terzo == p_idTerzo);
        }

        public static IQueryable<tab_terzo_sto> OrderByStorico(this IQueryable<tab_terzo_sto> p_query)
        {
            return p_query.OrderByDescending(d => d.id_terzo_sto);
        }
    }
}