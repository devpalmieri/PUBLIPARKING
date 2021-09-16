using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using Publisoftware.Utility.Log;

namespace Publisoftware.Data.BD
{
    public class TabRendicontoNewBD : EntityBD<tab_rendiconto_NEW>
    {
        public static IQueryable<tab_rendiconto_NEW> GetListByIdCcRiscossione(int id_tab_cc_riscossione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(x=>x.id_tab_cc_riscossione == id_tab_cc_riscossione);
        }
    }
}
