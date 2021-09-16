using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPianificazioneCicliInputRendicontazioneLinq
    {
        /// <summary>
        /// Filtro per pianificazione ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_id_pianificazione_ciclo"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_input_rendicontazione> WhereByIdPianificazioneCiclo(this IQueryable<tab_pianificazione_cicli_input_rendicontazione> p_query, int p_id_pianificazione_ciclo)
        {
            return p_query.Where(c => c.id_pianificazione_ciclo == p_id_pianificazione_ciclo);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_input_rendicontazione> OrderByDefault(this IQueryable<tab_pianificazione_cicli_input_rendicontazione> p_query)
        {
            return p_query.OrderBy(o => o.id_pianificazione_ciclo_input_rendicontazione);
        }
    }
}
