using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabMacroentrateLinq
    {
        public static IQueryable<tab_macroentrate> WhereByNotIMUICIeTASI(this IQueryable<tab_macroentrate> p_query)
        {
            return p_query.Except(p_query.WhereIsICI()).Except(p_query.WhereIsIMU()).Except(p_query.WhereIsTASI());
        }

        public static IQueryable<tab_macroentrate> OrderByDefault(this IQueryable<tab_macroentrate> p_query)
        {
            return p_query.OrderBy(d => d.ordine);
        }

        public static IQueryable<tab_macroentrate> WhereIsICI(this IQueryable<tab_macroentrate> p_query)
        {
            return p_query.Where(d => d.join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.ICI));
        }

        public static IQueryable<tab_macroentrate> WhereIsIMU(this IQueryable<tab_macroentrate> p_query)
        {
            return p_query.Where(d => d.join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.IMU));
        }

        public static IQueryable<tab_macroentrate> WhereIsTASI(this IQueryable<tab_macroentrate> p_query)
        {
            return p_query.Where(d => d.join_entrate_macroentrate.Any(j => j.id_entrata == anagrafica_entrate.TASI));
        }
    }
}
