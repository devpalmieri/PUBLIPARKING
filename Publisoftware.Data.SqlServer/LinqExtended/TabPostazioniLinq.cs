using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPostazioniLinq
    {
        public static IQueryable<tab_postazioni> WhereByIP(this IQueryable<tab_postazioni> p_query, string p_ip)
        {
            return p_query.Where(d => d.ip == p_ip);
        }
    }
}