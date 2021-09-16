using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinEntrateMacroentrateLinq
    {
        public static IQueryable<join_entrate_macroentrate> OrderByDefault(this IQueryable<join_entrate_macroentrate> p_query)
        {
            return p_query.OrderBy(o => o.id_join_entrate_macroentrate);
        }

        public static IQueryable<join_entrate_macroentrate> WhereIdEntrata(this IQueryable<join_entrate_macroentrate> p_query, int p_idEntrata)
        {
            return p_query.Where(o => o.id_entrata == p_idEntrata);
        }

        public static IQueryable<join_entrate_macroentrate> WhereIdMacroentrata(this IQueryable<join_entrate_macroentrate> p_query, int p_idMacroEntrata)
        {
            return p_query.Where(j => j.id_tab_macroentrate == p_idMacroEntrata);
        }
    }
}
