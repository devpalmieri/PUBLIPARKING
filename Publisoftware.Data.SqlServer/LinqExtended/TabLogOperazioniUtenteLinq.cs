using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabLogOperazioniUtenteLinq
    {
        public static IQueryable<tab_log_operazioni_utente> OrderByDefault(this IQueryable<tab_log_operazioni_utente> p_query)
        {
            return p_query.OrderByDescending(o => o.data);
        }
    }
}
