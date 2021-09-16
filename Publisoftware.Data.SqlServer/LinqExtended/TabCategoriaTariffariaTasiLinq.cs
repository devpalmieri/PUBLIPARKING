using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCategoriaTariffariaTasiLinq
    {
        /// <summary>
        /// Filtro per anno
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_anno"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria_tasi> WhereAnno(this IQueryable<tab_categoria_tariffaria_tasi> p_query, int p_anno)
        {
            return p_query.Where(c => c.anno == p_anno);
        }

        /// <summary>
        /// Filtro per anno, id categoria e id utilizzo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_Idcategoria"></param>
        /// <param name="p_Idutilizzo"></param>
        /// <param name="p_anno"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria_tasi> WhereByIdCategoriaAndIdUtilizzoAndAnno(this IQueryable<tab_categoria_tariffaria_tasi> p_query, int? p_Idcategoria, int? p_Idutilizzo, int p_anno)
        {
            return p_query.Where(c => (c.id_anagrafica_categoria == p_Idcategoria) && (c.id_utilizzo == p_Idutilizzo) && c.anno == p_anno);
        }

        public static IQueryable<tab_categoria_tariffaria_tasi> WhereByPeriodoOggettoContribuzione(this IQueryable<tab_categoria_tariffaria_tasi> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.anagrafica_categoria.tab_oggetti_contribuzione.Any(x => x.id_oggetto_contribuzione == p_idOggettoContribuzione && d.anno >= x.data_inizio_contribuzione.Year && d.anno <= (x.data_fine_contribuzione.HasValue ? x.data_fine_contribuzione.Value.Year : DateTime.MaxValue.Year)));
        }

        public static IQueryable<tab_categoria_tariffaria_tasi> OrderByDefault(this IQueryable<tab_categoria_tariffaria_tasi> p_query)
        {
            return p_query.OrderBy(o => o.anno).ThenBy(d => d.id_utilizzo);
        }

        public static IQueryable<tab_categoria_tariffaria_tasi_light> ToLight(this IQueryable<tab_categoria_tariffaria_tasi> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_categoria_tariffaria_tasi_light
            {
                id_categoria_tariffaria_tasi = d.id_categoria_tariffaria_tasi,
                anno = d.anno,
                utilizzo = d.anagrafica_utilizzo != null ? d.anagrafica_utilizzo.des_utilizzo : string.Empty,
                rivalutazione = d.Rivaluatazione,
                aliquotaridotta = d.AliquotaRidotta,
                moltiplicatore = d.Moltiplicatore,
                esente = d.isEsente
            }).AsQueryable();
        }
    }
}
