using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabPosteArcostradaleLinq
    {
        public static IQueryable<tab_poste_arcostradale> WhereByIdStrada(this IQueryable<tab_poste_arcostradale> p_query, int p_idStrada)
        {
            return p_query.Where(d => d.idStrada == p_idStrada);
        }

        public static IQueryable<tab_poste_arcostradale> WhereSenzaColore(this IQueryable<tab_poste_arcostradale> p_query)
        {
            return p_query.Where(d => d.Colore == null);
        }

        public static IQueryable<tab_poste_arcostradale> WhereByColore(this IQueryable<tab_poste_arcostradale> p_query, string p_colore)
        {
            return p_query.Where(d => d.Colore.Equals(p_colore.ToUpper()));
        }

        public static IQueryable<tab_poste_arcostradale> WhereByRangeKm(this IQueryable<tab_poste_arcostradale> p_query, int p_m)
        {
            return p_query.Where(d => d.NDal <= p_m && d.NAl >= p_m && d.Parita == "K");
        }

        public static IQueryable<tab_poste_arcostradale> WhereByRangeNrCivicoPari(this IQueryable<tab_poste_arcostradale> p_query, int p_civico)
        {
            return p_query.Where(d => d.NDal <= p_civico && d.NAl >= p_civico && d.Parita == "P");
        }

        public static IQueryable<tab_poste_arcostradale> WhereByRangeNrCivicoDispari(this IQueryable<tab_poste_arcostradale> p_query, int p_civico)
        {
            return p_query.Where(d => d.NDal <= p_civico && d.NAl >= p_civico && d.Parita == "D");
        }
    }
}
