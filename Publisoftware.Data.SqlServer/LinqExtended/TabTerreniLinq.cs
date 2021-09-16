using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTerreniLinq
    {
        public static IQueryable<tab_terreni> WhereByIdImmobile(this IQueryable<tab_terreni> p_query, int p_idImmobile)
        {
            return p_query.Where(d => d.id_tab_immobili == p_idImmobile);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_terreni> OrderByDefault(this IQueryable<tab_terreni> p_query)
        {
            return p_query.OrderBy(e => e.progressivo);
        }

        public static IQueryable<tab_terreni_light> ToLight(this IQueryable<tab_terreni> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_terreni_light
            {
                id_tab_terreni = d.id_tab_terreni,
                progressivo = d.progressivo,
                Descrizione = d.tab_qualita_terreno.descrizione,
                classe = d.classe,
                ettari = d.ettari,
                are = d.are,
                centiare = d.centiare,
                RedditoDominicale = d.RedditoDominicale,
                RedditoAgrario = d.RedditoAgrario,
                DataInizio = d.data_inizio_String,
                DataFine = d.data_fine_String
            }).AsQueryable();
        }
    }
}
