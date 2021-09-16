using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaCodStatoBaseLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_cod_stato_base> OrderByDefault(this IQueryable<anagrafica_cod_stato_base> p_query)
        {
            return p_query.OrderBy(e => e.descrizione);
        }
    }
}
