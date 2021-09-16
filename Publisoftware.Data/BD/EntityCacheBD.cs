using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    /// <summary>
    /// Estensione della classe base EntityBD. Aggiunge una cache sulla chiamata GetById per aumentare le prestazioni.
    /// Da utilizzare come base per tutte le entità che variano raramente, come l'elenco degli Enti
    /// </summary>
    /// <typeparam name="EntityType">Entity da gestire</typeparam>
    public class EntityCacheBD<EntityType> : EntityBD<EntityType> where EntityType : class
    {
        private static Dictionary<Int32, EntityType> m_cache = new Dictionary<Int32, EntityType>();
        /// <summary>
        /// restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">ID da ricercare nel campo chiave primaria</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static new EntityType GetById(Int32 p_id, dbEnte p_dbContext)
        {
            if(!m_cache.ContainsKey(p_id))
            {             
                m_cache[p_id] = EntityBD<EntityType>.GetById(p_id, p_dbContext);
            }

            return m_cache[p_id];
        }

        /// <summary>
        /// Svuota la cache
        /// </summary>
        public static void ClearCache()
        {
            m_cache = new Dictionary<Int32, EntityType>();
        }
    }
}
