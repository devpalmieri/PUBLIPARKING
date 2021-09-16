using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCategoriaTariffariaTosapLinq
    {
        public static IQueryable<tab_categoria_tariffaria_tosap> WhereByPeriodoOggettoContribuzione(this IQueryable<tab_categoria_tariffaria_tosap> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.anagrafica_categoria.tab_oggetti_contribuzione.Any(x => x.id_oggetto_contribuzione == p_idOggettoContribuzione && x.id_ente_gestito == d.id_ente_gestito && d.anno >= x.data_inizio_contribuzione.Year && d.anno <= (x.data_fine_contribuzione.HasValue ? x.data_fine_contribuzione.Value.Year : DateTime.MaxValue.Year)));
        }

        public static IQueryable<tab_categoria_tariffaria_tosap> OrderByDefault(this IQueryable<tab_categoria_tariffaria_tosap> p_query)
        {
            return p_query.OrderBy(o => o.anno);
        }

        public static IQueryable<tab_categoria_tariffaria_tosap_light> ToLight(this IQueryable<tab_categoria_tariffaria_tosap> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_categoria_tariffaria_tosap_light
            {
                id_categoria_tariffaria = d.id_categoria_tariffaria,
                anno = d.anno,
                entrata = d.anagrafica_entrate != null ? d.anagrafica_entrate.descrizione_entrata : string.Empty,
                Tariffa = d.Tariffa
            }).AsQueryable();
        }
    }
}
