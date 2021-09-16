﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabIdpSpidBD : EntityBD<tab_idpspid> 
    {
        public TabIdpSpidBD()
        {

        }

        /// <summary>
        /// Restituisce la lista degli IDP abilitati allo SPID
        /// /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static List<tab_idpspid> GetSpidIDPList(List<string> p_listAmbienti, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(t => p_listAmbienti.Contains(t.ambiente) && t.flag_on_off=="1").ToList();
        }

        /// <summary>
        /// Restituisce la lista degli IDP abilitati allo SPID
        /// /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static List<tab_idpspid> GetUpdateableSpidIDPList(List<string> p_listAmbienti, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(t => t.flag_on_off == "1" && t.url_metadata !=null && p_listAmbienti.Contains(t.ambiente)).ToList();
        }

        //public static List<tab_idpspid> GetSpidIDPByName(List<string> p_listAmbienti, dbEnte p_dbContext)
        //{
        //    return GetList(p_dbContext).Where(t => t.flag_on_off == "1" && t.url_metadata != null && p_listAmbienti.Contains(t.ambiente)).ToList();
        //}

    }
}
