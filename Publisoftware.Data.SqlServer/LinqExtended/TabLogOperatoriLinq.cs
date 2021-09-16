﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabLogOperatoriLinq
    {
        public static IQueryable<tab_log_operatori> OrderByDefault(this IQueryable<tab_log_operatori> p_query)
        {
            return p_query.OrderByDescending(o => o.inizio_attivita);
        }
    }
}
