using Publiparking.Core.Data.BD;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publiparking.Web.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes.Helper
{
    public static class MenuHelper
    {
        public static List<MenuNodeFirstLevel> GetMenu(DbParkContext p_context, int id_ente)
        {
            List<tab_menu_primo_livello> apps = TabMenuPrimoLivelloBD.GetAppAbilitate(p_context, id_ente);
            List<MenuNodeFirstLevel> menu = new List<MenuNodeFirstLevel>();
            if (apps != null)
            {
                menu = (from c in apps
                        select new MenuNodeFirstLevel()
                        {
                            IdMenuFirst = c.id_tab_menu_primo_livello,
                            IsLink = c.flag_link.Equals("1") ? true : false,
                            IsVisible = c.flag_visibile.Equals("1") ? true : false,
                            Action = c.action,
                            Codice = c.codice,
                            controller = c.controller,
                            Descrizione = c.descrizione,
                            Ordine = c.ordine,
                            TipoMenu = c.tipo_menu,
                            Tooltip = c.tooltip,
                            ListMenuSecondLevel = (from c2 in c.tab_menu_secondo_livello
                                                   where c2.id_tab_menu_primo_livello == c.id_tab_menu_primo_livello
                                                   select new MenuNodeSecondLevel()
                                                   {
                                                       IdMenuSecond = c2.id_tab_menu_secondo_livello,
                                                       IsLink = c2.flag_link.Equals("1") ? true : false,
                                                       IsVisible = c2.flag_visibile.Equals("1") ? true : false,
                                                       Action = c2.action,
                                                       Codice = c2.codice,
                                                       controller = c2.controller,
                                                       Descrizione = c2.descrizione,
                                                       Ordine = c2.ordine,
                                                       TipoMenu = c2.tipo_menu,
                                                       Tooltip = c2.tooltip,
                                                       ListMenuThirdLevel = (from c3 in c2.tab_menu_terzo_livello
                                                                             where c3.id_tab_menu_secondo_livello == c2.id_tab_menu_secondo_livello
                                                                             select new MenuNodeThirdLevel()
                                                                             {
                                                                                 IdMenuThird = c3.id_tab_menu_terzo_livello,
                                                                                 IsLink = c3.flag_link.Equals("1") ? true : false,
                                                                                 IsVisible = c3.flag_visibile.Equals("1") ? true : false,
                                                                                 Action = c3.action,
                                                                                 //Codice = c3.codice,
                                                                                 controller = c3.controller,
                                                                                 Descrizione = c3.descrizione,
                                                                                 Ordine = c3.ordine,
                                                                                 TipoMenu = c3.tipo_menu,
                                                                                 Tooltip = c3.tooltip
                                                                             }).ToList()
                                                   }).ToList()


                        }
                      ).ToList();
            }
            Sessione.SetCurrentNodesMenu(menu);
            return menu;
        }
    }
}
