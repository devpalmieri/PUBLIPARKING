using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabDocOutputLinq
    {
        public static IQueryable<tab_doc_output> OrderByDataEmissione(this IQueryable<tab_doc_output> p_query)
        {
            return p_query.OrderByDescending(d => d.data_emissione_doc);
        }
    }
}
