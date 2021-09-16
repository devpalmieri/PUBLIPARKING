using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabReferenteLinq
    {
        public static tab_referente WhereByIdContribuente(this IQueryable<tab_referente> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_anag_contribuente == p_idContribuente).FirstOrDefault();
        }

        public static IQueryable<tab_referente> WhereToponimoDaNormalizzare(this IQueryable<tab_referente> p_query)
        {
            return p_query.Where(c => c.id_toponimo_normalizzato.HasValue);
        }
    }
}
