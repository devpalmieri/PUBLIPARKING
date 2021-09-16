using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoServizioLinq
    {
        public static IQueryable<anagrafica_tipo_servizi> WhereByIdTipoServizio(this IQueryable<anagrafica_tipo_servizi> p_query, int p_idTipoServizio)
        {
            return p_query.Where(w => w.id_tipo_servizio == p_idTipoServizio);
        }
    }
}
