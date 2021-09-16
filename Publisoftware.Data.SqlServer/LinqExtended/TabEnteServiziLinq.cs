using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabEnteServiziLinq
    {
        public static IQueryable<tab_ente_servizi> OrderByDefault(this IQueryable<tab_ente_servizi> p_query)
        {
            return p_query.OrderBy(es => es.id_servizi_contrattuali);
        }
    }
}
