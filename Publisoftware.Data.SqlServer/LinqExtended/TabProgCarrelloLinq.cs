using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabProgCarrelloLinq
    {
        public static IQueryable<tab_prog_carrello> WhereByAnno(this IQueryable<tab_prog_carrello> p_query, int p_anno)
        {
            return p_query.Where(d => d.anno == p_anno);
        }

        public static IQueryable<tab_prog_carrello> WhereByIdEnte(this IQueryable<tab_prog_carrello> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }
    }
}
