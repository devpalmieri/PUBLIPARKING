using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class TabLogOperazioniOperatoreBD : EntityBD<tab_log_operazioni_operatore>
    {
        public TabLogOperazioniOperatoreBD()
        {

        }

        /// <summary>
        /// restituisce la lista delle operazioni di sessione dell'operatore filtrando per l'id di sessione
        /// </summary>
        /// <param name="p_idLogOperatore"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_log_operazioni_operatore> GetLogByRicerca(int p_idLogOperatore, dbEnte p_dbContext)
        {
            IQueryable<tab_log_operazioni_operatore> v_logList = GetListInternal(p_dbContext);

            v_logList = v_logList.Where(d => d.id_log_operatori == p_idLogOperatore);

            return v_logList;
        }
    }
}
