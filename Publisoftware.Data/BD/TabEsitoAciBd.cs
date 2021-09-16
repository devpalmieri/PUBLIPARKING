using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabEsitoAciBd : EntityBD<tab_esito_aci>
    {
        public TabEsitoAciBd()
        {
        }
        public static IQueryable<tab_esito_aci> GetListByIdJoinSuperVisProf(int p_idJoin, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(x => x.id_join_tab_supervisione_profili == p_idJoin);
        }
    }
}
