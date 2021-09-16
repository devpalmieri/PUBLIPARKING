using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCategoriaTariffariaLinq
    {
        /// <summary>
        /// Filtro per anno
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_anno"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria> WhereAnno(this IQueryable<tab_categoria_tariffaria> p_query, int p_anno)
        {
            return p_query.Where(c => c.anno == p_anno);
        }

        public static IQueryable<tab_categoria_tariffaria> WhereByEnteGestito(this IQueryable<tab_categoria_tariffaria> p_query, int p_idEnte)
        {
            return p_query.Where(c => c.id_ente_gestito == p_idEnte);
        }

        public static IQueryable<tab_categoria_tariffaria> WhereByPeriodoContribuzione(this IQueryable<tab_categoria_tariffaria> p_query, DateTime p_dataInizio, DateTime? p_dataFine)
        {
            return p_query.Where(c => c.anno >= p_dataInizio.Year && c.anno <= (p_dataFine.HasValue ? p_dataFine.Value.Year : DateTime.MaxValue.Year));
        }

        /// <summary>
        /// Filtro per TARSU
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria> WhereEntrataIsTARSU(this IQueryable<tab_categoria_tariffaria> p_query)
        {
            //si prende l'ID IMU poichè gli oggetti di contribuzione imu sono comuni alla tasi
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.TARES_TARSU);
        }

        /// <summary>
        /// Filtro per anno e id categoria
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_Idcategoria"></param>
        /// <param name="p_anno"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria> WhereByIdCategoriaAndAnno(this IQueryable<tab_categoria_tariffaria> p_query, int p_Idcategoria, int p_anno)
        {
            return p_query.Where(c => (c.id_anagrafica_categoria == p_Idcategoria) && c.anno == p_anno);
        }

        public static IQueryable<tab_categoria_tariffaria> WhereByPeriodoOggettoContribuzione(this IQueryable<tab_categoria_tariffaria> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.anagrafica_categoria.tab_oggetti_contribuzione.Any(x => x.id_oggetto_contribuzione == p_idOggettoContribuzione && 
                                                                                                x.id_ente_gestito == d.id_ente_gestito && 
                                                                                                d.anno >= x.data_inizio_contribuzione.Year && 
                                                                                                d.anno <= (x.data_fine_contribuzione.HasValue ? x.data_fine_contribuzione.Value.Year : DateTime.MaxValue.Year)));
        }

        public static IQueryable<tab_categoria_tariffaria> OrderByDefault(this IQueryable<tab_categoria_tariffaria> p_query)
        {
            return p_query.OrderBy(o => o.anno);
        }

        public static IQueryable<tab_categoria_tariffaria_light> ToLight(this IQueryable<tab_categoria_tariffaria> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_categoria_tariffaria_light
            {
                id_categoria_tariffaria = d.id_categoria_tariffaria,
                anno = d.anno,
                entrata = d.tab_macroentrate.descrizione,
                TariffaFissa = d.TariffaFissa,
                TariffaVariabile = d.TariffaVariabile
            }).AsQueryable();
        }
    }
}
