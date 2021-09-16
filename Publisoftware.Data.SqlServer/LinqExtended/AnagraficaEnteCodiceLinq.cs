using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaEnteCodiceLinq
    {
        public static IQueryable<anagrafica_ente_codici> OrderByDefault(this IQueryable<anagrafica_ente_codici> p_query)
        {
            return p_query.OrderBy(o => o.codice_ente).ThenBy(o => o.anagrafica_ente.descrizione_ente);
        }
    }
}
