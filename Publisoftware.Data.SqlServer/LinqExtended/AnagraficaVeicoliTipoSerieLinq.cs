using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaVeicoliTipoSerieLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_veicoli_tipo_serie> OrderByDefault(this IQueryable<anagrafica_veicoli_tipo_serie> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_veicoli_marche.descrizione).ThenBy(d => d.tipo).ThenBy(d => d.serie);
        }
    }
}
