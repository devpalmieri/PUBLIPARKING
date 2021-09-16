using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaControlliQualitaEmissioneAvvPagLinq
    {
        /// <summary>
        /// Filtro per id
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_controlli_qualita_emissione_avvpag> WhereById(this IQueryable<anagrafica_controlli_qualita_emissione_avvpag> p_query, int p_id)
        {
            return p_query.Where(ac => ac.id_anagrafica_controllo == p_id);
        }
    }
}
