using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaVeicoliMarcheLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_veicoli_marche> OrderByDefault(this IQueryable<anagrafica_veicoli_marche> p_query)
        {
            return p_query.OrderBy(e => e.descrizione);
        }
    }
}
