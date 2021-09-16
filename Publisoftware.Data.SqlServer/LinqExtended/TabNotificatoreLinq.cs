using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabNotificatoreLinq
    {
        public static IQueryable<tab_notificatore> WhereByIdEnte(this IQueryable<tab_notificatore> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_notificatore> OrderByDefault(this IQueryable<tab_notificatore> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_ente.descrizione_ente).ThenBy(d => d.cognome).ThenBy(d => d.nome);
        }
    }
}
