using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinEntrateMacroentrateBD : EntityBD<join_entrate_macroentrate>
    {
        public JoinEntrateMacroentrateBD()
        {

        }

        public static IQueryable<join_entrate_macroentrate> GetListContains(String p_text, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            if (p_text == null) p_text = "";

            return GetList(p_dbContext, p_includeEntities).Where(a => p_text == ""
                || a.id_join_entrate_macroentrate.ToString().Contains(p_text)
                || a.tab_macroentrate.descrizione.ToUpper().Contains(p_text.ToUpper())
                );
        }

        public static join_entrate_macroentrate GetByJoinValues(int p_idEntrata, int p_idMacroEntrata, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(j => j.id_entrata == p_idEntrata && j.id_tab_macroentrate == p_idMacroEntrata);
        }
    }
}
