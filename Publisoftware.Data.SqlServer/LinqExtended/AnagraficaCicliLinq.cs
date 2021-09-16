using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaCicliLinq
    {
        /// <summary>
        /// Filtro per ID Entrata o Entrata Generica
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Entrata</param>
        /// <returns></returns>
        public static IQueryable<anagrafica_cicli> WhereByIdEnteOrGenerico(this IQueryable<anagrafica_cicli> p_query, int? p_idEnte)
        {
            return p_query.Where(w => w.id_ente == anagrafica_ente.ID_ENTE_GENERICO || w.id_ente == p_idEnte);
        }

        /// <summary>
        /// Filtro se l'anagrafica ciclo ha dettaglio
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_cicli> WhereHasDettaglio(this IQueryable<anagrafica_cicli> p_query)
        {
            return p_query.Where(w => w.anagrafica_cicli_dettaglio.Count > 0);
        }

        /// <summary>
        /// Ordine per descrizione ed ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_cicli> OrderByDescrizioneAndEnte(this IQueryable<anagrafica_cicli> p_query)
        {
            return p_query.OrderBy(o => o.descrizione).ThenBy(o => o.anagrafica_ente.descrizione_ente);
        }
    }
}
