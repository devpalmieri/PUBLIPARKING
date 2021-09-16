using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class SerProvinceLinq
    {
        /// <summary>
        /// Restituisce l'elenco delle province appartenenti alla regione indicata dal Codice
        /// </summary>
        /// <param name="p_codRegione">Codice Regione di ricerca</param>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<ser_province> WhereByCodRegione(this IQueryable<ser_province> p_query, int p_codRegione)
        {
            return p_query.Where(p => p.cod_regione == p_codRegione);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<ser_province> OrderByDefault(this IQueryable<ser_province> p_query)
        {
            return p_query.OrderBy(o => o.des_provincia);
        }
    }
}
