//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Publisoftware.Data.BD.BD
//{
//    class TabRimborsiBD
//    {
//    }
//}
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
    public class TabRimborsiBD : EntityBD<tab_rimborsi>
    {
        //public static IQueryable<tab_rimborsi> GetListByIdCcRiscossione(int p_idConto, dbEnte p_dbContext)
        //{
        //    return GetList(p_dbContext).Where(x => x.data.id_cc_riscossione == p_idConto);
        //}
    }
}

