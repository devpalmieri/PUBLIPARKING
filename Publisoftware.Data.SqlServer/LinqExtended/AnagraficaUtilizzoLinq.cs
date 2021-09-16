using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaUtilizzoLinq
    {
        /// <summary>
        /// Filtro per entrata
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEntrata"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> WhereByIdEntrata(this IQueryable<anagrafica_utilizzo> p_query, int p_idEntrata)
        {
            return p_query.Where(ac => ac.id_entrata == p_idEntrata);
        }

        /// <summary>
        /// Filtro per ICI
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> WhereEntrataIsICI(this IQueryable<anagrafica_utilizzo> p_query)
        {
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.ICI);
        }
        
        /// <summary>
        /// Filtro per IMU
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> WhereEntrataIsIMU(this IQueryable<anagrafica_utilizzo> p_query)
        {
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.IMU);
        }
        
        /// <summary>
        /// Filtro per TASI
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> WhereEntrataIsTASI(this IQueryable<anagrafica_utilizzo> p_query)
        {
            //si prende l'ID IMU poichè gli oggetti di contribuzione imu sono comuni alla tasi
            return p_query.Where(ac => ac.id_entrata == anagrafica_entrate.IMU);
        }

        /// <summary>
        /// Filtro per l'ente (nullable)
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> WhereByIdEnteOrNull(this IQueryable<anagrafica_utilizzo> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte || ac.id_ente == null);
        }

        /// <summary>
        /// Filtro per l'ente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEnte"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> WhereByIdEnte(this IQueryable<anagrafica_utilizzo> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_utilizzo> OrderByDefault(this IQueryable<anagrafica_utilizzo> p_query)
        {
            return p_query.OrderBy(e => e.des_utilizzo);
        }
    }
}
