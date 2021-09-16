using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.POCOLight;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAssegnazioneRilevazioniLinq
    {
        /// <summary>
        /// Filtro ID Assegnazione
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idAssegnazione">p_idAssegnazione Ricercato</param>
        /// <returns></returns>
        public static IQueryable<tab_assegnazione_rilevazioni> WhereByIdAssegnazione(this IQueryable<tab_assegnazione_rilevazioni> p_query, int p_idAssegnazione)
        {
            return p_query.Where(w => w.id_tab_assegnazione_rilevazioni == p_idAssegnazione);
        }

        /// <summary>
        /// Filtro ID Rilevatore
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idAssegnazione">p_idAssegnazione Ricercato</param>
        /// <returns></returns>
        public static IQueryable<tab_assegnazione_rilevazioni> WhereByIdRilevatore(this IQueryable<tab_assegnazione_rilevazioni> p_query, int p_idRilevatore)
        {
            return p_query.Where(w => w.id_rilevatore == p_idRilevatore);
        }

        /// <summary>
        /// Filtro per cod_stato = VAL-ASS
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idAssegnazione">p_idAssegnazione Ricercato</param>
        /// <returns></returns>
        public static IQueryable<tab_assegnazione_rilevazioni> WhereStatoVAL_ASS(this IQueryable<tab_assegnazione_rilevazioni> p_query)
        {
            return p_query.Where(w => w.cod_stato == "VAL-ASS");
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_assegnazione_rilevazioni> OrderByDefault(this IQueryable<tab_assegnazione_rilevazioni> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_assegnazione_rilevazioni);
        }

        /// <summary>
        /// Transforma nella versione light
        /// </summary>
        /// <param name="iniziale"></param>
        /// <returns></returns>
        public static IQueryable<tab_assegnazione_rilevazioni_light> ToLight(this IQueryable<tab_assegnazione_rilevazioni> iniziale)
        {
            return iniziale.Select(x => new tab_assegnazione_rilevazioni_light
            {
                id_tab_assegnazione_rilevazioni = x.id_tab_assegnazione_rilevazioni,
                id_tab_zone = x.id_tab_zone,
                id_rilevatore = x.id_rilevatore,
                id_lista_letture = x.id_lista_letture,
                cod_stato = x.cod_stato,
                id_ente = x.id_ente,
                id_ente_gestito = x.id_ente_gestito
            });
        }
    }
}
