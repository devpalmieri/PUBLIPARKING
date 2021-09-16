using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabInterventoLinq
    {
        public static IQueryable<tab_intervento> WhereByIdContribuente(this IQueryable<tab_intervento> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_intervento> OrderByDataPresentazione(this IQueryable<tab_intervento> p_query)
        {
            return p_query.OrderBy(d => d.data_presentazione);
        }
    }
}
