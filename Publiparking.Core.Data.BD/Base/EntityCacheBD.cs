using Publiparking.Core.Data.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD.Base
{
    // <summary>
    /// Estensione della classe base EntityBD. Aggiunge una cache sulla chiamata GetById per aumentare le prestazioni.
    /// Da utilizzare come base per tutte le entità che variano raramente, come l'elenco degli Enti
    /// </summary>
    /// <typeparam name="EntityType">Entity da gestire</typeparam>
    public class EntityCacheBD<TEntity>
    where TEntity : class
    {
        private static string Key_List = "List";
        private static Dictionary<Int32, TEntity> m_cache = new Dictionary<Int32, TEntity>();
        private static Dictionary<string, IQueryable<TEntity>> m_cacheList = new Dictionary<string, IQueryable<TEntity>>();
        /// <summary>
        /// restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">ID da ricercare nel campo chiave primaria</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static TEntity GetById(DbParkContext p_dbContext, Int32 p_id)
        {
            if (!m_cache.ContainsKey(p_id))
            {
                m_cache[p_id] = EntityBD<TEntity>.GetById(p_dbContext, p_id);
            }

            return m_cache[p_id];
        }
        public static IQueryable<TEntity> GetList(DbParkContext p_dbContext)
        {
            if (!m_cacheList.ContainsKey(Key_List))
            {
                m_cacheList[Key_List] = EntityBD<TEntity>.GetList(p_dbContext);
            }

            return m_cacheList[Key_List];

        }
    }

}
