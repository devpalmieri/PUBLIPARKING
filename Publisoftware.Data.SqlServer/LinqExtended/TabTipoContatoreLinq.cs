using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoContatoreLinq
    {
        public static IQueryable<tab_tipo_contatore> WhereByMarca(this IQueryable<tab_tipo_contatore> p_query, string p_marca)
        {
            return p_query.Where(d => d.marca == p_marca);
        }

        public static IQueryable<tab_tipo_contatore> OrderByMarca(this IQueryable<tab_tipo_contatore> p_query)
        {
            return p_query.OrderBy(d => d.marca);
        }
    }
}
