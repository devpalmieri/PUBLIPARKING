using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFabbricatiLinq
    {
        public static IQueryable<tab_fabbricati> WhereByIdImmobile(this IQueryable<tab_fabbricati> p_query, int p_idImmobile)
        {
            return p_query.Where(d => d.id_tab_immobili == p_idImmobile);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_fabbricati> OrderByDefault(this IQueryable<tab_fabbricati> p_query)
        {
            return p_query.OrderBy(e => e.progressivo);
        }

        public static IQueryable<tab_fabbricati_light> ToLight(this IQueryable<tab_fabbricati> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_fabbricati_light
            {
                id_tab_fabbricati = d.id_tab_fabbricati,
                progressivo = d.progressivo,
                Descrizione = d.tab_categorie_fabbricati.descrizione,
                classe = d.classe,
                Superficie = d.Superficie,
                Rendita = d.Rendita,
                SuperficieTarsu = d.SuperficieTarsu,
                Ubicazione = d.Ubicazione,
                DataInizio = d.data_inizio_String,
                DataFine = d.data_fine_String
            }).AsQueryable();
        }
    }
}
