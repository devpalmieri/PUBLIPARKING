using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabDomicilioLinq
    {
        /// <summary>
        /// Controlla se esistono domicili attivi
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static bool ExistAttivo(this IQueryable<tab_domicilio> p_query)
        {
            return p_query.Any(d => d.data_fine == null);
        }

        /// <summary>
        /// Controlla se esistono domicili attivi
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static bool ExistAttivo(this IEnumerable<tab_domicilio> p_query)
        {
            return p_query.Any(d => d.data_fine == null);
        }

        /// <summary>
        /// Filtro per Data Fine null
        /// </summary>
        /// <param name="p_query"></param>        
        /// <returns></returns>
        public static tab_domicilio SingleOrDefaultAttivo(this IQueryable<tab_domicilio> p_query)
        {
            return p_query.SingleOrDefault(d => d.data_fine == null);
        }

        /// <summary>
        /// Filtro per Data Fine null
        /// </summary>
        /// <param name="p_query"></param>        
        /// <returns></returns>
        public static tab_domicilio SingleOrDefaultAttivo(this IEnumerable<tab_domicilio> p_query)
        {
            return p_query.SingleOrDefault(d => d.data_fine == null);
        }

        public static IQueryable<tab_domicilio> WhereByCodStato(this IQueryable<tab_domicilio> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.ToUpper() == p_codStato.ToUpper());
        }

        public static IEnumerable<tab_domicilio> WhereByCodStato(this IEnumerable<tab_domicilio> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.ToUpper() == p_codStato.ToUpper());
        }

        public static IQueryable<tab_domicilio> WhereByIdContribuente(this IQueryable<tab_domicilio> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_anag_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_domicilio> OrderByDataFine(this IQueryable<tab_domicilio> p_query)
        {
            return p_query.OrderByDescending(e => e.data_fine);
        }
    }
}
