using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabLettureLinq
    {
        public static IQueryable<tab_letture> WhereByInOverlapPeriodoContribuzione(this IQueryable<tab_letture> p_query, tab_oggetti_contribuzione p_oggettoContribuzione)
        {
            return p_query.Where(d => d.data_lettura >= p_oggettoContribuzione.data_inizio_contribuzione &&
                                      d.data_lettura <= (p_oggettoContribuzione.data_fine_contribuzione.HasValue ? p_oggettoContribuzione.data_fine_contribuzione.Value : DateTime.MaxValue));
        }

        public static IQueryable<tab_letture> WhereByIdContatore(this IQueryable<tab_letture> p_query, int p_idContatore)
        {
            return p_query.Where(d => d.id_contatore == p_idContatore);
        }

        public static IQueryable<tab_letture> OrderByDefault(this IQueryable<tab_letture> p_query)
        {
            return p_query.OrderBy(d => d.data_lettura);
        }

        public static IQueryable<tab_letture_light> ToLight(this IQueryable<tab_letture> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_letture_light
            {
                id_lettura = d.id_lettura,
                DataLettura = d.data_lettura_String,
                Lettura = d.Lettura,
                TipoLettura = d.TipoLettura
            }).AsQueryable();
        }
    }
}
