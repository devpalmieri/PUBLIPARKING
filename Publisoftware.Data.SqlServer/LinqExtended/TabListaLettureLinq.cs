using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabListaLettureLinq
    {
        public static IQueryable<tab_lista_letture> WhereByIdLista(this IQueryable<tab_lista_letture> p_query, int p_idLista)
        {
            return p_query.Where(w => w.id_lista == p_idLista);
        }

        public static IQueryable<tab_lista_letture> OrderByDefault(this IQueryable<tab_lista_letture> p_query)
        {
            return p_query.OrderBy(o => o.id_lista).ThenBy(o => o.data_lista);
        }
    }
}
