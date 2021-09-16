using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TabMenuTerzoLivelloBD : EntityBD<tab_menu_terzo_livello>
    {
        public TabMenuTerzoLivelloBD()
        {

        }
        /// <summary>
        /// Restituisce la voce
        /// di menu di terzo livello
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_menu_terzo_livello GetNodeMenu(DbParkContext p_dbContext, int p_idmenu_master)
        {
            tab_menu_terzo_livello node = GetList(p_dbContext).Where(x => x.tipo_menu.Equals("W") && x.id_tab_menu_secondo_livello == p_idmenu_master).SingleOrDefault();

            return node;
        }
        /// <summary>
        /// Restituisce le voci
        /// di menu di terzo livello
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static List<tab_menu_terzo_livello> GetNodesMenu(DbParkContext p_dbContext, int p_idmenu_master)
        {
            List<tab_menu_terzo_livello> nodes = GetList(p_dbContext).Where(x => x.tipo_menu.Equals("W") && x.id_tab_menu_secondo_livello == p_idmenu_master).ToList();

            return nodes;
        }

    }
}
