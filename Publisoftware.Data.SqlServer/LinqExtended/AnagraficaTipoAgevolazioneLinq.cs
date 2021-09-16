using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoAgevolazioneLinq
    {
        /// <summary>
        /// Ricerca per Sigla.
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_abi">ABI ricercato</param>
        /// <param name="p_cab">CAB ricercato</param>
        /// <returns></returns>
        public static anagrafica_tipo_agevolazione SingleOrDefaultSigla(this IQueryable<anagrafica_tipo_agevolazione> p_query, string p_sigla)
        {
            return p_query.SingleOrDefault(ac => ac.sigla_tipo_agevolazione == p_sigla);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_agevolazione> OrderByDefault(this IQueryable<anagrafica_tipo_agevolazione> p_query)
        {
            return p_query.OrderBy(o => o.des_tipo_agevolazione);
        }
    }
}
