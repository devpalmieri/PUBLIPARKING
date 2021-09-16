using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPianificazioneCicliDettaglioLinq
    {
        /// <summary>
        /// Filtro per pianificazione ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idPianificazioneCiclo"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_dettaglio> WhereByIdPianificazioneCiclo(this IQueryable<tab_pianificazione_cicli_dettaglio> p_query, int p_idPianificazioneCiclo)
        {
            return p_query.Where(w => w.id_pianificazione_ciclo == p_idPianificazioneCiclo);
        }

        /// <summary>
        /// Filtro per codice stato
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_codstato"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_dettaglio> WhereByCodStato(this IQueryable<tab_pianificazione_cicli_dettaglio> p_query, string p_codstato)
        {
            return p_query.Where(w => w.cod_stato == p_codstato);
        }
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_dettaglio> OrderByDefault(this IQueryable<tab_pianificazione_cicli_dettaglio> p_query)
        {
            return p_query.OrderBy(o => o.sequenza);
        }

        /// <summary>
        /// Filtro per id lista
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_id_lista"></param>
        /// <returns></returns>
        public static IQueryable<tab_pianificazione_cicli_dettaglio> WhereByIdLista(this IQueryable<tab_pianificazione_cicli_dettaglio> p_query, int p_id_lista)
        {
            return p_query.Where(w => w.id_lista.Value == p_id_lista);
        }
    }
}
