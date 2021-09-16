using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoServiziLinq
    {
        public static IQueryable<anagrafica_tipo_servizi> OrderByDefault(this IQueryable<anagrafica_tipo_servizi> p_query)
        {
            return p_query.OrderBy(e => e.descr_tiposervizio);
        }
    }
}
