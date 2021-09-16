using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaParentelaLinq
    {


        public static IQueryable<anagrafica_parentela> OrderByDefault(this IQueryable<anagrafica_parentela> p_query)
        {
            return p_query.OrderBy(o => o.descrizione_parentela);
        }
    }
}
