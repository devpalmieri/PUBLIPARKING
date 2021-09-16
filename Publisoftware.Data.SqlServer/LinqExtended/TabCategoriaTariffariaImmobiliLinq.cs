using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCategoriaTariffariaImmobiliLinq
    {
        public static IQueryable<tab_categoria_tariffaria_immobili> WhereByPeriodoContribuzione(this IQueryable<tab_categoria_tariffaria_immobili> p_query, DateTime p_dataInizio, DateTime? p_dataFine)
        {
            return p_query.Where(c => c.anno >= p_dataInizio.Year && c.anno <= (p_dataFine.HasValue ? p_dataFine.Value.Year : DateTime.MaxValue.Year));
        }

        public static IQueryable<tab_categoria_tariffaria_immobili> WhereByPeriodoOggettoContribuzione(this IQueryable<tab_categoria_tariffaria_immobili> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.anagrafica_categoria.tab_oggetti_contribuzione.Any(x => x.id_oggetto_contribuzione == p_idOggettoContribuzione && d.anno >= x.data_inizio_contribuzione.Year && d.anno <= (x.data_fine_contribuzione.HasValue ? x.data_fine_contribuzione.Value.Year : DateTime.MaxValue.Year)));
        }

        public static IQueryable<tab_categoria_tariffaria_immobili> OrderByDefault(this IQueryable<tab_categoria_tariffaria_immobili> p_query)
        {
            return p_query.OrderBy(o => o.anno).ThenBy(d => d.id_utilizzo);
        }

        public static IQueryable<tab_categoria_tariffaria_immobili_light> ToLight(this IQueryable<tab_categoria_tariffaria_immobili> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_categoria_tariffaria_immobili_light
            {
                id_categoria_tariffaria_immobili = d.id_categoria_tariffaria_immobili,
                anno = d.anno,
                entrata = d.anagrafica_entrate != null ? d.anagrafica_entrate.descrizione_entrata : string.Empty,
                utilizzo = d.anagrafica_utilizzo != null ? d.anagrafica_utilizzo.des_utilizzo : string.Empty,
                rivalutazione = d.Rivaluatazione,
                aliquotaridotta1 = d.AliquotaRidotta1,
                aliquotaridotta2 = d.AliquotaRidotta2,
                moltiplicatore = d.Moltiplicatore,
                esente = d.isEsente
            }).AsQueryable();
        }
    }
}
