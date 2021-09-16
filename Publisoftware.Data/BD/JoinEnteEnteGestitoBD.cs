using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinEnteEnteGestitoBD : EntityBD<join_ente_ente_gestito>
    {
        public JoinEnteEnteGestitoBD()
        {
            

        }
        /// <summary>
        /// Ottiene la lista degli enti collegati
        /// </summary>
        /// <param name="p_id_ente"></param>
        /// <param name="p_context"></param>
        /// <returns></returns>
        public static IQueryable<join_ente_ente_gestito> GetListEntiGestityByIdEnte(int p_id_ente, dbEnte p_context)
        {
            return GetList(p_context).Where(d => d.id_ente==p_id_ente).OrderByDescending(o=>o.id_ente_gestito);
        }
    }
}
