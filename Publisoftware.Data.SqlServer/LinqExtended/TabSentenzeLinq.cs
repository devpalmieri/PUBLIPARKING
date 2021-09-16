using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabSentenzeLinq
    {
        public static IQueryable<tab_sentenze> OrderByDataDeposito(this IQueryable<tab_sentenze> p_query)
        {
            return p_query.OrderBy(d => d.data_deposito_sentenza);
        }

        public static IQueryable<tab_sentenze> OrderByDataScadenzaAppello(this IQueryable<tab_sentenze> p_query)
        {
            return p_query.OrderBy(d => d.data_scadenza_appello);
        }

        public static IQueryable<tab_sentenze> WhereByIdRicorso(this IQueryable<tab_sentenze> p_query, int p_idIdRicorso)
        {
            return p_query.Where(d => d.id_tab_ricorso == p_idIdRicorso);
        }

        public static IQueryable<tab_sentenze> WhereByNumeroSentenza(this IQueryable<tab_sentenze> p_query, string p_numeroSentenza)
        {
            return p_query.Where(d => d.numero_sentenza.Equals(p_numeroSentenza));
        }
    }
}