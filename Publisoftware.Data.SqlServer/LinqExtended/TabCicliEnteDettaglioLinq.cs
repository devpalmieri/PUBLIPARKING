using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCicliEnteDettaglioLinq
    {
        /// <summary>
        /// Filtro per ciclo ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idCicloEnte"></param>
        /// <returns></returns>
        public static IQueryable<tab_cicli_ente_dettaglio> WhereIdCicloEnte(this IQueryable<tab_cicli_ente_dettaglio> p_query, int p_idCicloEnte)
        {
            return p_query.Where(c => c.id_ciclo_ente == p_idCicloEnte);
        }
    }
}
