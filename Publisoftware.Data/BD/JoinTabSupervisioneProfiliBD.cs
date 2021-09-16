using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinTabSupervisioneProfiliBD : EntityBD<join_tab_supervisione_profili>
    {
        public JoinTabSupervisioneProfiliBD()
        {

        }

        public static IQueryable<join_tab_supervisione_profili> GetListBySupervizioneAnnOrSsp(int p_idSupervisioneCoattiva, dbEnte p_context)
        {
            IQueryable<join_tab_supervisione_profili> v_list = GetList(p_context).Where(c => c.id_tab_supervisione_finale == p_idSupervisioneCoattiva)
                                                         .Where(c => c.cod_stato == join_tab_supervisione_profili.ANN_REV || c.cod_stato == join_tab_supervisione_profili.ANN_UFF || c.cod_stato == join_tab_supervisione_profili.SSP_IST);                                                         
            return v_list;
        }
    }
}
