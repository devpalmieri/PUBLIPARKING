using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPosteStradaLinq
    {
        public static IQueryable<tab_poste_strada> OrderByDefault(this IQueryable<tab_poste_strada> p_query)
        {
            return p_query.OrderBy(d => d.DugStd + " " + d.DugComplStd + " " + d.NomeStd);
        }

        public static bool AnyByCodComune(this IQueryable<tab_poste_strada> p_query, string p_codComune)
        {
            p_codComune = p_codComune.PadLeft(6, '0');
            return p_query.Any(d => d.tab_poste_comune.CodISTAT.Equals(p_codComune));
        }

        public static IQueryable<tab_poste_strada> WhereByCodComune(this IQueryable<tab_poste_strada> p_query, string p_codComune)
        {
            p_codComune = p_codComune.PadLeft(6, '0');
            return p_query.Where(d => d.tab_poste_comune.CodISTAT.Equals(p_codComune));
        }

        public static IQueryable<tab_poste_strada> FiltroAutocompletamento(this IQueryable<tab_poste_strada> p_query, string p_iniziali)
        {
            string v_testo = p_iniziali.Replace("'", " ").Trim().ToUpper();

            //return p_query.Where(t => (t.DugStd != null && t.DugComplStd != null) ? (t.DugStd + " " + t.DugComplStd + " " + t.NomeStd).ToUpper().Contains(p_iniziali.ToUpper()) :
            //                          (t.DugStd != null) ? (t.DugStd + " " + t.NomeStd).ToUpper().Contains(p_iniziali.ToUpper()) :
            //                          (t.DugComplStd != null) ? (t.DugComplStd + " " + t.NomeStd).ToUpper().Contains(p_iniziali.ToUpper()) :
            //                           t.NomeStd.ToUpper().Contains(p_iniziali.ToUpper()));

            return p_query.Where(t => (t.DugStd != null && t.DugStd.Contains(v_testo)) || 
                                      (t.NomeStd != null && t.NomeStd.Contains(v_testo)) || 
                                      (t.DenomAbbrevStd != null && t.DenomAbbrevStd.Contains(v_testo)));
        }
    }
}
