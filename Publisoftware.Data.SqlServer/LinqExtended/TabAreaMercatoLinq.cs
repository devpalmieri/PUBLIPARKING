using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAreaMercatoLinq
    {
        public static IQueryable<tab_area_mercato> WhereByCodStatoListNot(this IQueryable<tab_area_mercato> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => !p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_area_mercato> WhereByCodStatoList(this IQueryable<tab_area_mercato> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_area_mercato> WhereByCodStatoNot(this IQueryable<tab_area_mercato> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_area_mercato> WhereByCodStato(this IQueryable<tab_area_mercato> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_codStato));
        }
    }
}