using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabLogOperatoreBD : EntityBD<tab_log_operatori>
    {
        public TabLogOperatoreBD()
        {

        }

        /// <summary>
        /// restituisce la lista delle sessioni degli operatori filtrati per diversi parametri
        /// </summary>
        /// <param name="p_idLog"></param>
        /// <param name="p_idRisorsa"></param>
        /// <param name="p_idStruttura"></param>
        /// <param name="p_modOp"></param>
        /// <param name="p_da"></param>
        /// <param name="p_a"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_log_operatori> GetLogByRicerca(int p_idLog, int p_idRisorsa, int p_idStruttura, string p_modOp, string p_da, string p_a, dbEnte p_dbContext)
        {
            IQueryable<tab_log_operatori> v_logList = GetListInternal(p_dbContext);

            if (p_idLog > 0)
            {
                v_logList = v_logList.Where(d => d.id_log_operatori == p_idLog);
            }

            if (p_idRisorsa > 0)
            {
                v_logList = v_logList.Where(d => d.id_risorsa == p_idRisorsa);
            }

            if (p_idStruttura > 0)
            {
                v_logList = v_logList.Where(d => d.id_struttura_aziendale == p_idStruttura);
            }

            if (p_modOp != string.Empty)
            {
                v_logList = v_logList.Where(d => d.modalita_operativa.Equals(p_modOp));
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
    }
}
