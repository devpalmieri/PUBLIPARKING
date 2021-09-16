using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCategoriaTariffariaImuLinq
    {
        /// <summary>
        /// Filtro per anno
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_anno"></param>
        /// <returns></returns>    
        public static IQueryable<tab_categoria_tariffaria_imu> WhereAnno(this IQueryable<tab_categoria_tariffaria_imu> p_query, int p_anno)
        {
            return p_query.Where(c => c.anno == p_anno);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria_imu> OrderByDefault(this IQueryable<tab_categoria_tariffaria_imu> p_query)
        {
            return p_query.OrderBy(o => o.id_categoria_tariffaria_imu);
        }

        /// <summary>
        /// Filtro per anno, id utilizzo e id categoria
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_Idcategoria"></param>
        /// <param name="p_Idutilizzo"></param>
        /// <param name="p_anno"></param>
        /// <returns></returns>
        public static IQueryable<tab_categoria_tariffaria_imu> WhereByIdCategoriaAndIdUtilizzoAndAnno(this IQueryable<tab_categoria_tariffaria_imu> p_query, int? p_Idcategoria, int? p_Idutilizzo, int p_anno)
        {
            return p_query.Where(c => (c.id_anagrafica_categoria == p_Idcategoria) && (c.id_utilizzo == p_Idutilizzo) && c.anno == p_anno);
        }
    }
}
