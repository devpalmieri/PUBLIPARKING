using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabProfiloContribuenteNewLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_profilo_contribuente_new> OrderByDefault(this IQueryable<tab_profilo_contribuente_new> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_profilo_contribuente);
        }

        public static IQueryable<tab_profilo_contribuente_new> WhereIdProfilo(this IQueryable<tab_profilo_contribuente_new> p_query, int p_idProfilo)
        {
            return p_query.Where(d => d.id_tab_profilo_contribuente == p_idProfilo);
        }

        /// <summary>
        /// Seleziona i profili relativi all'ispezione in input
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idIspezione"></param>
        /// <returns></returns>
        public static IQueryable<tab_profilo_contribuente_new> WhereIdIspezione(this IQueryable<tab_profilo_contribuente_new> p_query, decimal p_idIspezione)
        {
            return p_query.Where(pc => pc.id_tab_ispezione_coattivo == p_idIspezione);
        }

        /// <summary>
        /// Seleziona i profili con stato "valido"
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_profilo_contribuente_new> Validi(this IQueryable<tab_profilo_contribuente_new> p_query)
        {
            return p_query.Where(pc => pc.cod_stato.CompareTo(tab_profilo_contribuente_new.ATT_ATT) == 0);
        }

        public static IQueryable<tab_profilo_contribuente_new_light> ToLight(this IQueryable<tab_profilo_contribuente_new> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_profilo_contribuente_new_light
            {
                id_tab_profilo_contribuente = d.id_tab_profilo_contribuente,
                descrizione = d.DescrizioneBeneEstesa
            }).AsQueryable();
        }
    }
}
