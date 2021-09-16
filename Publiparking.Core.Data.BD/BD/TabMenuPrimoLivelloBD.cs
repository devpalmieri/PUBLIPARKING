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
    public class TabMenuPrimoLivelloBD : EntityBD<tab_menu_primo_livello>
    {
        public TabMenuPrimoLivelloBD()
        {

        }
        /// <summary>
        /// Restituisce l'utente in base nomeUtente e password (md5Hash) associate
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_password">Password da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static List<tab_menu_primo_livello> GetAppAbilitate(DbParkContext p_dbContext, int p_idente)
        {
            List<tab_menu_primo_livello> appsEnabled = null;
            List<tab_menu_primo_livello> apps = GetList(p_dbContext).Where(x => x.tipo_menu.Equals("W")).ToList();
            bool isEnabled = false;
            if (apps != null)
            {
                appsEnabled = new List<tab_menu_primo_livello>();
                foreach (tab_menu_primo_livello app in apps)
                {
                    isEnabled = TabAbilitazioneMenu.IsEnabledByEnte(p_dbContext, app.id_tab_menu_primo_livello, p_idente, tab_abilitazione_menu.MENU_PRIMO_LIVELLO);
                    if (isEnabled)
                        appsEnabled.Add(app);
                }
            }
            List<tab_menu_secondo_livello> apps2 = null;
            List<tab_menu_terzo_livello> apps3 = null;
            if (appsEnabled != null)
            {
                foreach (tab_menu_primo_livello app_enabled in appsEnabled)
                {
                    if (app_enabled.flag_link != "1")
                    {
                        apps2 = TabMenuSecondoLivelloBD.GetNodesMenu(p_dbContext, app_enabled.id_tab_menu_primo_livello);
                        if (apps2 != null)
                        {
                            foreach (tab_menu_secondo_livello app2 in apps2)
                            {
                                isEnabled = TabAbilitazioneMenu.IsEnabledByEnte(p_dbContext, app2.id_tab_menu_secondo_livello, p_idente, tab_abilitazione_menu.MENU_SECONDO_LIVELLO);
                                if (isEnabled)
                                {
                                    if (app2.flag_link != "1")
                                    {
                                        apps3 = TabMenuTerzoLivelloBD.GetNodesMenu(p_dbContext, app2.id_tab_menu_secondo_livello);
                                    }
                                    if (apps3 != null)
                                    {
                                        foreach (tab_menu_terzo_livello app3 in apps3)
                                        {
                                            isEnabled = TabAbilitazioneMenu.IsEnabledByEnte(p_dbContext, app3.id_tab_menu_terzo_livello, p_idente, tab_abilitazione_menu.MENU_TERZO_LIVELLO);
                                            if (isEnabled)
                                                app2.tab_menu_terzo_livello.Add(app3);
                                        }
                                    }

                                    app_enabled.tab_menu_secondo_livello.Add(app2);
                                }
                            }
                        }
                    }
                }
            }
            return appsEnabled;
        }


    }
}
