using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabIterSupervisioneAttiCoaLinq
    {
        public static IQueryable<tab_iter_supervisione_atti_coa> GroupByFasce(this IQueryable<tab_iter_supervisione_atti_coa> p_query)
        {
            return p_query.GroupBy(p => new { p.fascia_imp_minimo, p.fascia_imp_massimo }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<tab_iter_supervisione_atti_coa> OrderByDefault(this IQueryable<tab_iter_supervisione_atti_coa> p_query)
        {
            return p_query.OrderBy(d => d.fascia_imp_minimo);
        }

        public static IQueryable<tab_iter_supervisione_atti_coa> WhereByRangeValiditaOdierno(this IQueryable<tab_iter_supervisione_atti_coa> p_query)
        {
            return p_query.Where(d => d.data_inizio_validita <= DateTime.Now
                                   && d.data_fine_validita >= DateTime.Now);
        }
    }
}