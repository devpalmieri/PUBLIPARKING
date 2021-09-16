using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinApplicazioniLinkLinq
    {
        /// <summary>
        /// Filtro per Id Applicazione
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idApplicazione"></param>
        /// <returns></returns>
        public static IQueryable<join_applicazioni_link> WhereByIdApplicazione(this IQueryable<join_applicazioni_link> p_query, int p_idApplicazione)
        {
            return p_query.Where(w => w.id_tab_applicazioni == p_idApplicazione);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_applicazioni_link> OrderByDefault(this IQueryable<join_applicazioni_link> p_query)
        {
            return p_query.OrderBy(o => o.ordine);
        }
    }
}
