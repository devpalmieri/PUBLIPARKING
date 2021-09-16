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
    public class TabAbilitazioneMenu : EntityBD<tab_abilitazione_menu>
    {
        public TabAbilitazioneMenu()
        {

        }
        /// <summary>
        /// Restituisce le voci di
        /// menu abilitate
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_idapp"></param>
        /// <returns></returns>
        public static bool IsEnabled(DbParkContext p_dbContext, int p_idapp)
        {
            tab_abilitazione_menu app = GetList(p_dbContext).Where(x => x.id_tab_nodo_menu == p_idapp).SingleOrDefault();
            if (app != null)
                return true;


            return false;
        }
        /// <summary>
        /// Restituisce le voci di
        /// menu abilitate per ente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_idapp"></param>
        /// <returns></returns>
        public static bool IsEnabledByEnte(DbParkContext p_dbContext, int p_idapp, int p_idente, int p_livello_menu)
        {
            tab_abilitazione_menu app = GetList(p_dbContext).Where(x => x.id_tab_nodo_menu == p_idapp
                                                                   && x.id_ente == p_idente
                                                                   && x.livello_menu == p_livello_menu).SingleOrDefault();
            if (app != null)
                return true;

            return false;
        }

    }
}
