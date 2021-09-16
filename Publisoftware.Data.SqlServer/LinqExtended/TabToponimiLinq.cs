using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabToponimiLinq
    {
        public static IQueryable<tab_toponimi> OrderByDefault(this IQueryable<tab_toponimi> p_query)
        {
            return p_query.OrderBy(o => o.descrizione_toponimo);
        }

        public static bool AnyByCodComune(this IQueryable<tab_toponimi> p_query, int p_codComune)
        {
            return p_query.Any(t => t.cod_comune_toponimo == p_codComune);
        }

        public static IQueryable<tab_toponimi> WhereByCodComune(this IQueryable<tab_toponimi> p_query, int p_codComune)
        {
            return p_query.Where(t => t.cod_comune_toponimo == p_codComune);
        }

        public static IQueryable<tab_toponimi> FiltroAutocompletamento(this IQueryable<tab_toponimi> p_query, string p_iniziali)
        {
            return p_query.Where(t => t.descrizione_toponimo.ToUpper().Contains(p_iniziali.ToUpper()));
        }
        
        public static IQueryable<tab_toponimi_light> ToLight(this IQueryable<tab_toponimi> iniziale)
        {
            return iniziale.Select(l => new tab_toponimi_light { cap_toponimo = l.cap_toponimo, cod_comune_toponimo = l.cod_comune_toponimo, cod_toponimo = l.cod_toponimo,
                                                descrizione_toponimo = l.descrizione_toponimo, desc_toponimo = l.desc_toponimo, frazione_toponimo = l.frazione_toponimo,
                                                id_tipo_toponimo = l.id_tipo_toponimo, id_toponimo = l.id_toponimo});
        }
    }
}
