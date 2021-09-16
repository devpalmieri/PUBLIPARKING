using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoBeneLinq
    {
        public static IQueryable<tab_tipo_bene> OrderByDefault(this IQueryable<tab_tipo_bene> p_query)
        {
            return p_query.OrderBy(d => d.descrizione);
        }

        public static IQueryable<tab_tipo_bene> WhereSiglaIspezione(this IQueryable<tab_tipo_bene> p_query, string p_siglaTipoIspezione)
        {
            return p_query.Where(w => w.tab_tipo_ispezione.sigla_tipo_ispezione == p_siglaTipoIspezione);
        }
    }
}
