using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoEnteLinq
    {
        public static IQueryable<tab_tipo_ente> OrderByDefault(this IQueryable<tab_tipo_ente> p_query)
        {
            return p_query.OrderBy(o => o.cod_tipo_ente);
        }
    }
}
