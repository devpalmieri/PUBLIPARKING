using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabContribuenteStoNewLinq
    {
        public static IQueryable<tab_contribuente_sto_new> WhereByIdContribuente(this IQueryable<tab_contribuente_sto_new> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_anag_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_contribuente_sto_new> OrderByStorico(this IQueryable<tab_contribuente_sto_new> p_query)
        {
            return p_query.OrderByDescending(d => d.id_anag_contribuente_sto);
        }
    }
}