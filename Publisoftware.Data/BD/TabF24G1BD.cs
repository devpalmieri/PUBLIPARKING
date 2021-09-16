using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabF24G1BD : EntityBD<tab_f24_g1>
    {
        public TabF24G1BD()
        {

        }

        public static IQueryable<tab_f24_g1> GetListByListId(List<int> p_idList, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => p_idList.Contains(c.id_g1));
        }
    }
}
