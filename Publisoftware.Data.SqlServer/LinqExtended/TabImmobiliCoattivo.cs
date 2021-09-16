using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabImmobiliCoattivo
    {
        /// <summary>
        /// Filtro per Codice Fiscale/Partita IVA soggetto Ispezione
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_CfPivaSoggettoIspezione">Codice Fiscale/Partita IVA soggetto Ispezione</param>
        /// <returns></returns>
        public static IQueryable<tab_immobili_coattivo> WhereByCfPivaImmobili(this IQueryable<tab_immobili_coattivo> p_query, string p_CfPivaSoggettoIspezione)
        {
            return p_query.Where(w => w.join_proprietari_immobili_coattivo.Any(i => i.cf_piva_proprietario == p_CfPivaSoggettoIspezione));
        }
        /// <summary>
        /// Seleziona quelli Attivi
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_immobili_coattivo> WhereAttivi(this IQueryable<tab_immobili_coattivo> p_query)
        {
            return p_query.Where(w => w.cod_stato == tab_immobili_coattivo.ATT_ATT);
        }
    }
}
