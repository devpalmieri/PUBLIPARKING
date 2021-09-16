using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabDettMovPagLinq
    {
        public static IQueryable<tab_dett_mov_pag> WhereByIdMovPag(this IQueryable<tab_dett_mov_pag> p_query, int p_idMovPag)
        {
            return p_query.Where(d => d.id_tab_mov_pag == p_idMovPag);
        }
    }
}
