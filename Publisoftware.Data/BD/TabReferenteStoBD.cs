using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabReferenteStoBD : EntityBD<tab_referente_sto>
    {
        public TabReferenteStoBD()
        {

        }

        public static tab_referente_sto GetLastReferenteStoByIdReferente(int p_idReferente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_tab_referente == p_idReferente).OrderByDescending(d => d.id_tab_referente_sto).FirstOrDefault();
        }

        /// <summary>
        /// Lista dei referenti STO Attivi per ente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_referente_sto> GetListReferentiSto(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_stato.StartsWith(anagrafica_stato_contribuente.ATT));
        }
    }
}
