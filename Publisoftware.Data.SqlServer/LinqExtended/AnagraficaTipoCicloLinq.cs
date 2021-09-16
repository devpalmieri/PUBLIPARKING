using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoCicloLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_ciclo> OrderByDefault(this IQueryable<anagrafica_tipo_ciclo> p_query)
        {
            return p_query.OrderBy(e => e.descr_tipociclo);
        }
    }
}
