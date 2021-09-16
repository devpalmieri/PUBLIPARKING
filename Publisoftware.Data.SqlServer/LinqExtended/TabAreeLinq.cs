using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAreeLinq
    {
        public static IQueryable<tab_aree> WhereByToponimoCodCitta(this IQueryable<tab_aree> p_query, int? p_idTipoToponimo, int? p_codCitta)
        {
            return p_query.Where(d => d.id_toponimo == p_idTipoToponimo.Value && d.tab_zone.cod_comune == p_codCitta.Value);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_aree> OrderByDefault(this IQueryable<tab_aree> p_query)
        {
            return p_query.OrderBy(o => o.id_zona).ThenBy(o => o.id_area ).ThenBy(o => o.id_toponimo);
        }
    }
}
