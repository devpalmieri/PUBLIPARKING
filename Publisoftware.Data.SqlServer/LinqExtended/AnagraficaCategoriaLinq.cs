using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaCategoriaLinq
    {
        /// <summary>
        /// Filtro per ID Entrata
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEntrata">ID Entrata</param>
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> WhereByIdEntrata(this IQueryable<anagrafica_categoria> p_query, int p_idEntrata)
        {
            return p_query.Where(ac => ac.id_entrata == p_idEntrata);
        }

        /// <summary>
        /// Filtro per ID Entrata ICI
        /// </summary>
        /// <param name="p_query"></param>        
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> WhereEntrataIsICI(this IQueryable<anagrafica_categoria> p_query)
        {
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.ICI);
        }

        /// <summary>
        /// Filtro per ID Entrata TARSU
        /// </summary>
        /// <param name="p_query"></param>        
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> WhereEntrataIsTARSU(this IQueryable<anagrafica_categoria> p_query)
        {
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.TARES_TARSU);
        }

        /// <summary>
        /// Filtro per ID Entrata IMU
        /// </summary>
        /// <param name="p_query"></param>        
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> WhereEntrataIsIMU(this IQueryable<anagrafica_categoria> p_query)
        {
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.IMU);
        }

        /// <summary>
        /// Filtro per ID Entrata IMU
        /// </summary>
        /// <param name="p_query"></param>        
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> WhereEntrataIsTASI(this IQueryable<anagrafica_categoria> p_query)
        {
            //si prende l'ID IMU poichè gli oggetti di contribuzione imu sono comuni alla tasi
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.IMU);
        }

        /// <summary>
        /// Filtro per ID Ente o ente null
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte">ID Ente</param>
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> WhereByIdEnteOrNull(this IQueryable<anagrafica_categoria> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte || ac.id_ente == null);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_categoria> OrderByDefault(this IQueryable<anagrafica_categoria> p_query)
        {
            return p_query.OrderBy(e => e.des_cat_contr);
        }
    }
}
