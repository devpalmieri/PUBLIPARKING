using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabStoricoAnagrafeBD : EntityBD<tab_storico_anagrafe> 
    {
        public TabStoricoAnagrafeBD()
        {

        }
        /// <summary>
        /// Lista dei records STO Attivi per ente, ente gestito, cf, data_aggiornamento decrescente e id_toponimo
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_storico_anagrafe> GetListRecsDescDtAgg(int p_idEnte, int p_idEnteG, int? p_idToponimo, string p_codFiscale, dbEnte p_dbContext)
        {
            try
            {
                return GetList(p_dbContext).Where(c => c.id_ente==p_idEnte && c.id_ente_gestito==p_idEnteG && c.codice_fiscale==p_codFiscale ).OrderByDescending(c => c.data_aggiornamento_anagrafe);
            }
            catch (Exception e) { return null; }
        }
    }
}
