using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabReferenteStoLinq
    {
        public static IQueryable<tab_referente_sto> WhereByIdReferente(this IQueryable<tab_referente_sto> p_query, int p_idReferente)
        {
            return p_query.Where(d => d.id_tab_referente == p_idReferente);
        }

        public static IQueryable<tab_referente_sto> OrderByStorico(this IQueryable<tab_referente_sto> p_query)
        {
            return p_query.OrderByDescending(d => d.id_tab_referente_sto);
        }
    }
}