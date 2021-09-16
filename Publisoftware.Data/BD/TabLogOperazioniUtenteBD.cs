using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class TabLogOperazioniUtenteBD : EntityBD<tab_log_operazioni_utente>
    {
        public TabLogOperazioniUtenteBD()
        {

        }

        /// <summary>
        /// restituisce la lista delle operazioni di sessione dell'utente filtrando per l'id di sessione
        /// </summary>
        /// <param name="p_idLogUtente"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_log_operazioni_utente> GetLogByRicerca(int p_idLogUtente, dbEnte p_dbContext)
        {
            IQueryable<tab_log_operazioni_utente> v_logList = GetListInternal(p_dbContext);

            v_logList = v_logList.Where(d => d.id_log_utenti == p_idLogUtente);

            return v_logList; 
        }
       
    }
}
