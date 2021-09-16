using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabInputconsuntiviRendicontiBD : EntityBD<tab_input_consuntivi_rendiconti>
    {
        public TabInputconsuntiviRendicontiBD() 
        { 

        }

        /// <summary>
        /// Filtro per l'id di pianificazione ciclo
        /// </summary>
        /// <param name="idPianificazioneCiclo"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static tab_input_consuntivi_rendiconti GetByPianificazioneCicloID(int idPianificazioneCiclo, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(i => i.id_pianificazione_ciclo == idPianificazioneCiclo); 
        }
    }
}
