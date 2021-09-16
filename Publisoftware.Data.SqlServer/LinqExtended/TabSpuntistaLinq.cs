using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabSpuntistaLinq
    {
        public static IQueryable<tab_spuntista> WhereByCodStatoListNot(this IQueryable<tab_spuntista> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => !p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_spuntista> WhereByCodStatoList(this IQueryable<tab_spuntista> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_spuntista> WhereByCodStatoNot(this IQueryable<tab_spuntista> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_spuntista> WhereByCodStato(this IQueryable<tab_spuntista> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_spuntista> WhereByIdContribuente(this IQueryable<tab_spuntista> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_spuntista> WhereByIdAreaMercato(this IQueryable<tab_spuntista> p_query, int p_idAreaMercato)
        {
            return p_query.Where(d => d.id_area_mercato == p_idAreaMercato);
        }
    }
}