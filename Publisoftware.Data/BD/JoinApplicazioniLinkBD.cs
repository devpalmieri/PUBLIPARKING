using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinApplicazioniLinkBD :EntityBD<join_applicazioni_link>
    {
        public JoinApplicazioniLinkBD()
        {

        }

        public static IQueryable<join_applicazioni_link> GetByIdApplicazione(int p_idApplicazione, dbEnte p_dbContext)
        {
                return GetList(p_dbContext).Where(x => x.id_tab_applicazioni == p_idApplicazione).OrderBy(o => o.ordine);
        }
    }
}
