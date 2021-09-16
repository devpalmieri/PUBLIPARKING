using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabEsecuzioneApplicazioniLinq
    {
        /// <summary>
        /// Filtro per pianificazione ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idPianificazioneCiclo"></param>
        /// <returns></returns>
        public static IQueryable<tab_esecuzione_applicazioni> WhereByIdPianificazioneCiclo(this IQueryable<tab_esecuzione_applicazioni> p_query, int p_idPianificazioneCiclo)
        {
            return p_query.Where(w => w.id_pianificazione_ciclo == p_idPianificazioneCiclo);
        }

        /// <summary>
        /// Filtro per applicazione
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idApplicazione"></param>
        /// <returns></returns>
        public static IQueryable<tab_esecuzione_applicazioni> WhereByIdApplicazione(this IQueryable<tab_esecuzione_applicazioni> p_query, int p_idApplicazione)
        {
            return p_query.Where(w => w.tab_pianificazione_cicli_dettaglio.id_tab_applicazioni == p_idApplicazione);
        }
    }
}
