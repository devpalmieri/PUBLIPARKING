using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoIspezioneLinq
    {
        public static IQueryable<tab_tipo_ispezione> OrderByDefault(this IQueryable<tab_tipo_ispezione> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_tipo_ispezione);
        }
    }
}
