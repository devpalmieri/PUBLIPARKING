using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoVeicoloLinq
    {
        public static IQueryable<anagrafica_tipo_veicolo> OrderByDefault(this IQueryable<anagrafica_tipo_veicolo> p_query)
        {
            return p_query.OrderBy(e => e.descrizione);
        }
    }
}
