using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class SerComuniLinq
    {
        /// <summary>
        /// Restituisce l'elenco delle province appartenenti alla regione indicata dal Codice
        /// </summary>
        /// <param name="p_codProvincia">Codice Provincia di ricerca</param>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<ser_comuni> WhereByCodProvincia(this IQueryable<ser_comuni> p_query, int p_codProvincia)
        {
            return p_query.Where(c => c.cod_provincia == p_codProvincia);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<ser_comuni> OrderByDefault(this IQueryable<ser_comuni> p_query)
        {
            return p_query.OrderBy(o => o.des_comune);
        }
    }
}
