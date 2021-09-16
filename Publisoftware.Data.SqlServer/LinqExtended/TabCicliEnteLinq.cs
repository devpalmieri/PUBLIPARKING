using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCicliEnteLinq
    {
        /// <summary>
        /// Filtra per ID Ciclo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idCiclo">ID Ciclo ricercato</param>
        /// <returns></returns>
        public static IQueryable<tab_cicli_ente> WhereIdCiclo(this IQueryable<tab_cicli_ente> p_query, int p_idCiclo)
        {
            return p_query.Where(c => c.id_ciclo == p_idCiclo);
        }

        /// <summary>
        /// Filtra per ID Ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Ente ricercato</param>
        /// <returns></returns>
        public static IEnumerable<tab_cicli_ente> WhereIdEnte(this IEnumerable<tab_cicli_ente> p_query, int p_idEnte)
        {
            return p_query.Where(c => c.id_ente == p_idEnte);
        }

        /// <summary>
        /// Filtra per ID Ciclo e ID Ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idCiclo">ID Ciclo ricercato</param>
        /// /// <param name="p_idEnte">ID Ente ricercato</param>
        /// <returns></returns>
        public static IQueryable<tab_cicli_ente> WhereIdCicloAndIdEnte(this IQueryable<tab_cicli_ente> p_query, int p_idCiclo, int p_idEnte)
        {
            return p_query.Where(c => c.id_ciclo == p_idCiclo && c.id_ente == p_idEnte);
        }

        /// <summary>
        /// Filtra per ID Ente oppure Ente generico
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Ente ricercato</param>
        /// <returns></returns>
        public static IEnumerable<tab_cicli_ente> WhereIdEnteOrGeneric(this IEnumerable<tab_cicli_ente> p_query, int p_idEnte)
        {
            return p_query.Where(c => c.id_ente == p_idEnte || c.id_ente == anagrafica_ente.ID_ENTE_GENERICO);
        }

        /// <summary>
        /// Ordine per descrizione
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_cicli_ente> OrderByDescrizione(this IQueryable<tab_cicli_ente> p_query)
        {
            return p_query.OrderBy(o => o.anagrafica_ente.descrizione_ente).ThenBy(o => o.anagrafica_cicli.descrizione);
        }
    }
}
