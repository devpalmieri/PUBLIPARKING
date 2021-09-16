using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.POCOLight;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinOggettiImmobiliLinq
    {
        public static IQueryable<join_oggetti_immobili> WhereByIdOggetto(this IQueryable<join_oggetti_immobili> p_query, decimal p_idOggetto)
        {
            return p_query.Where(d => d.id_oggetto == p_idOggetto);
        }

        public static IQueryable<join_oggetti_immobili> WhereByIdImmobile(this IQueryable<join_oggetti_immobili> p_query, decimal p_idImmobile)
        {
            return p_query.Where(d => d.id_tab_immobili == p_idImmobile);
        }
    }
}
