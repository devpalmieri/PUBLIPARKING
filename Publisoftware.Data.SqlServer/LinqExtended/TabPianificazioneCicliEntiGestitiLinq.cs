using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPianificazioneCicliEntiGestitiLinq
    {
        /// <summary>
        /// Filtro per pianificazione ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idPianificazioneCiclo"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_enti_gestiti> WhereByIdPianificazioneCiclo(this IQueryable<tab_pianificazione_cicli_enti_gestiti> p_query, int p_idPianificazioneCiclo)
        {
            return p_query.Where(w => w.id_pianificazione_ciclo == p_idPianificazioneCiclo);
        }
    }
}
