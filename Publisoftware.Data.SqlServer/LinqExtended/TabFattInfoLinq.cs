using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFattInfoLinq
    {
        public static IQueryable<tab_fatt_info> WhereByIdContribuente(this IQueryable<tab_fatt_info> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_fatt_info> OrderByPeriodoFatturazioneDa(this IQueryable<tab_fatt_info> p_query)
        {
            return p_query.OrderBy(d => d.periodo_fatturazione_da);
        }
    }
}
