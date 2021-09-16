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
    public class TabEstrattoContoBD : EntityBD<tab_estratto_conto>
    {
        public static IQueryable<tab_estratto_conto> GetListByIdCcRiscossione(int p_idConto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(x => x.id_cc_riscossione == p_idConto);
        }
    }
}
