using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabTerzoStoBD : EntityBD<tab_terzo_sto>
    {
        public TabTerzoStoBD()
        {

        }

        public static tab_terzo_sto GetLastTerzoStoByIdTerzo(int p_idTerzo, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_terzo == p_idTerzo).OrderByDescending(d => d.id_terzo_sto).FirstOrDefault();
        }

        /// <summary>
        /// Lista dei referenti STO Attivi per ente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_terzo_sto> GetListTerzoSto(int p_idEnte, string p_codStato, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_stato.StartsWith(p_codStato));
        }
    }
}
