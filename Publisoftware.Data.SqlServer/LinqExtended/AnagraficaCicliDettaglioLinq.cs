using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaCicliDettaglioLinq
    {
        /// <summary>
        /// Filtro se esiste la sequenza
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_sequenza"></param>
        /// <returns></returns>
        public static bool ExistSequenza(this IQueryable<anagrafica_cicli_dettaglio> p_query, int p_sequenza)
        {
            return p_query.Any(c => c.sequenza == p_sequenza);
        }

        /// <summary>
        /// Filtro per sequenza massima
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static int MaxSequenza(this IQueryable<anagrafica_cicli_dettaglio> p_query)
        {
            return p_query.Max(x => x.sequenza);
        }

        /// <summary>
        /// Filtro per ID Ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idCiclo">ID Ciclo</param>
        /// <returns></returns>
        public static IQueryable<anagrafica_cicli_dettaglio> WhereIdCiclo(this IQueryable<anagrafica_cicli_dettaglio> p_query, int p_idCiclo)
        {
            return p_query.Where(c => c.id_ciclo == p_idCiclo);
        }

        /// <summary>
        /// Filtro per ID Ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idCiclo">ID Ciclo</param>
        /// <returns></returns>
        public static IEnumerable<anagrafica_cicli_dettaglio> WhereIdCiclo(this IEnumerable<anagrafica_cicli_dettaglio> p_query, int p_idCiclo)
        {
            return p_query.Where(c => c.id_ciclo == p_idCiclo);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_cicli_dettaglio> OrderByDefault(this IQueryable<anagrafica_cicli_dettaglio> p_query)
        {
            return p_query.OrderBy(e => e.sequenza);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IEnumerable<anagrafica_cicli_dettaglio> OrderByDefault(this IEnumerable<anagrafica_cicli_dettaglio> p_query)
        {
            return p_query.OrderBy(e => e.sequenza);
        }
    }
}
