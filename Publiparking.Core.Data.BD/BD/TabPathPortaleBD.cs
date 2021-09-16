using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TabPathPortaleBD : EntityBD<tab_path_portale>
    {
        public TabPathPortaleBD()
        {

        }
        /// <summary>
        /// Restituisce la configurazione richiesta
        /// </summary>
        /// <param name="p_mode">Modalità richiesta</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_path_portale GetByMode(DbParkContext p_dbContext, string p_mode)
        {
            return GetList(p_dbContext).Where(p => p.id_tab_ambienteNavigation.descrizione != null && p.id_tab_ambienteNavigation.descrizione.Equals(p_mode)).SingleOrDefault();
        }
        public static async Task<tab_path_portale> GetByModeAsync(DbParkContext p_dbContext, string p_mode)
        {
            return GetList(p_dbContext).Where(p => p.id_tab_ambienteNavigation.descrizione != null && p.id_tab_ambienteNavigation.descrizione.Equals(p_mode)).SingleOrDefault();
        }
    }
}
