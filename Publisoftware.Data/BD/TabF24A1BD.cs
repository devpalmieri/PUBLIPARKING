using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabF24A1BD : EntityBD<tab_f24_a1>
    {
        public TabF24A1BD()
        {

        }

        public static IQueryable<tab_f24_a1> GetListByIdentificativo(string p_identificativo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.identificativo_file == p_identificativo);
        }
    }
}
