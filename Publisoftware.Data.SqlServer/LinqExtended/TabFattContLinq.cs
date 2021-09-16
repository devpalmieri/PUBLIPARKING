using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFattContLinq
    {
        public static IQueryable<tab_fatt_cont> WhereByIdEntrata(this IQueryable<tab_fatt_cont> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_fatt_cont> OrderByPeriodoContribuzioneDa(this IQueryable<tab_fatt_cont> p_query)
        {
            return p_query.OrderBy(d => d.periodo_contribuzione_da);
        }
    }
}
