using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabLogOperazioniOperatoreLinq
    {
        public static IQueryable<tab_log_operazioni_operatore> OrderByDefault(this IQueryable<tab_log_operazioni_operatore> p_query)
        {
            return p_query.OrderByDescending(o => o.data);
        }
    }
}
