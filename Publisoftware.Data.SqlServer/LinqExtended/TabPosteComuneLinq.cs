using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPosteComuneLinq
    {
        public static IQueryable<tab_poste_comune> OrderByDefault(this IQueryable<tab_poste_comune> p_query)
        {
            return p_query.OrderBy(o => o.DenominazioneStd);
        }

        public static IQueryable<tab_poste_comune> WhereByCodComune(this IQueryable<tab_poste_comune> p_query, string p_codComune)
        {
            return p_query.Where(d => d.CodISTAT.Contains(p_codComune));
        }

        public static IQueryable<tab_poste_comune> FiltroAutocompletamento(this IQueryable<tab_poste_comune> p_query, string p_iniziali)
        {
            return p_query.Where(t => t.DenominazioneStd.ToUpper().Contains(p_iniziali.ToUpper()));
        }
    }
}
