using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPianificazioneCicliInputEmissioneLinq
    {
        /// <summary>
        /// Filtro per pianificazione ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_id_pianificazione_ciclo"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_input_emissione> WhereByIdPianificazioneCiclo(this IQueryable<tab_pianificazione_cicli_input_emissione> p_query, int p_id_pianificazione_ciclo)
        {
            return p_query.Where(c => c.id_pianificazione_ciclo == p_id_pianificazione_ciclo);
        }
     
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_input_emissione> OrderByDefault(this IQueryable<tab_pianificazione_cicli_input_emissione> p_query)
        {
            return p_query.OrderBy(o => o.id_pianificazione_ciclo_input_emissione);
        }


        /// <summary>
        /// Filtro per id lista
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_id_lista"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_input_emissione> WhereByIdListaGenerata(this IQueryable<tab_pianificazione_cicli_input_emissione> p_query, int p_id_lista)
        {
            return p_query.Where(w => w.id_lista_generata.Value == p_id_lista);
        }
    }
}
