using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinEnteStrutture
    {
        /// <summary>
        /// Filtro per Id Strutture
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idStruttura"></param>
        /// <returns></returns>
        public static IQueryable<join_ente_strutture> WhereByIdStruttura(this IQueryable<join_ente_strutture> p_query, int p_idStruttura)
        {
            return p_query.Where(w => w.id_struttura_aziendale == p_idStruttura);
        }

        public static IQueryable<join_ente_strutture> WhereByIdEnte(this IQueryable<join_ente_strutture> p_query, int p_idEnte)
        {
            return p_query.Where(w => w.id_ente == p_idEnte);
        }

        /// <summary>
        /// Ordina per enti
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_ente_strutture> OrderByEnti(this IQueryable<join_ente_strutture> p_query)
        {
            return p_query.OrderBy(o => o.anagrafica_ente.descrizione_ente);
        }
    }
}
