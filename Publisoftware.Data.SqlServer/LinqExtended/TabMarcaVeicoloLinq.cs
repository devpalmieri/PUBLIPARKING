using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabMarcaVeicoloLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_marca_veicolo> OrderByDefault(this IQueryable<tab_marca_veicolo> p_query)
        {
            return p_query.OrderBy(e => e.descr_marca);
        }
    }
}
