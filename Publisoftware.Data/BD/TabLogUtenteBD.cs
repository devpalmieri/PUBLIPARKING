using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabLogUtenteBD : EntityBD<tab_log_utenti>
    {
        public TabLogUtenteBD()
        {

        }

        /// <summary>
        /// restituisce la lista delle sessioni degli utenti filtrati per diversi parametri
        /// </summary>
        /// <param name="p_idLog"></param>
        /// <param name="p_idRisorsa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_idUtente"></param>
        /// <param name="p_da"></param>
        /// <param name="p_a"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_log_utenti> GetLogByRicerca(int p_idLog, int p_idRisorsa, int p_idStruttura, int p_idUtente, string p_da, string p_a, dbEnte p_dbContext)
        {
            IQueryable<tab_log_utenti> v_logList = GetListInternal(p_dbContext);

            if (p_idLog > 0)
            {
                v_logList = v_logList.Where(d => d.id_log_utenti == p_idLog);
            }

            if (p_idRisorsa > 0)
            {
                v_logList = v_logList.Where(d => d.id_risorsa == p_idRisorsa);
            }

            if (p_idStruttura > 0)
            {
                v_logList = v_logList.Where(d => d.id_struttura_aziendale == p_idStruttura);
            }

            if (p_idUtente > 0)
            {
                v_logList = v_logList.Where(d => d.id_utente == p_idUtente);
            }

            if (p_da != string.Empty)
            {
                DateTime v_da = Convert.ToDateTime(p_da);
                v_logList = v_logList.Where(d => d.inizio_attivita >= v_da);
            }

            if (p_a != string.Empty)
            {
                DateTime v_a = Convert.ToDateTime(p_a);
                v_logList = v_logList.Where(d => d.inizio_attivita <= v_a);
            }

            return v_logList;
        }

        public static tab_log_utenti GetLogByIdLog(int p_idLog, dbEnte p_dbContext)
        {
            IQueryable<tab_log_utenti> v_logList = GetListInternal(p_dbContext);
            tab_log_utenti user = v_logList.Where(x => x.id_log_utenti == p_idLog ).FirstOrDefault();
            return user;
        }
        
    }
}
