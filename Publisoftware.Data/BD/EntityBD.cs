using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;

using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Collections.Generic;
using Publisoftware.Utility.Log;
using System.Globalization;
using System.Threading;

namespace Publisoftware.Data.BD
{
    /// <summary>
    /// Classe di base per tutte le BD.
    /// Implementa i metodi di base getById per l'accesso con chiave primaria e getList per l'accesso a tutti i record
    /// </summary>
    /// <typeparam name="EntityType">Entity da gestire</typeparam>
    public abstract class EntityBD<EntityType> where EntityType : class
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>(typeof(EntityType).FullName);

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// E' protected, perchè può essere utilizzata solo nelle classi derivate principalmente per ridefinire la GetList generica
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        protected static IQueryable<EntityType> GetListInternal(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            m_logger.LogMessage("GetListInternal(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)", EnLogSeverity.Trace);

            DbQuery<EntityType> v_list = null;
            v_list = (DbQuery<EntityType>)
                     (from c in p_dbContext.Set<EntityType>()
                      select c);

            if (p_includeEntities != null)
            {
                // Se indicate, include le entity richieste
                v_list = p_includeEntities.Aggregate(v_list, (current, includePath) => current.Include(includePath));
            }

            return v_list;
        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static IQueryable<EntityType> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities);
        }

        /// <summary>
        /// Restituisce un elenco di entity a partire da una istruzione di select
        /// </summary>
        /// <param name="p_sql">Intruzione select da eseguire</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static DbRawSqlQuery<EntityType> GetList(string p_sql, dbEnte p_dbContext)
        {
            return GetList(p_sql, null, p_dbContext);
        }

        /// <summary>
        /// Restituisce un elenco di entity a partire da una istruzione di select ed un elenco di parametri
        /// </summary>
        /// <param name="p_sql">Intruzione select da eseguire</param>
        /// <param name="p_params">Parametri da usare nell'istruzione SQL</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static DbRawSqlQuery<EntityType> GetList(string p_sql, SqlParameter[] p_params, dbEnte p_dbContext)
        {
            DbRawSqlQuery<EntityType> v_list = null;
            try
            {
                v_list = p_dbContext.Database.SqlQuery<EntityType>(p_sql, p_params);
            }
            catch (Exception e) { }

            return v_list;
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria (Int32)</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static EntityType GetById(Int32 p_id, dbEnte p_dbContext)
        {
            m_logger.LogMessage("GetById(Int32 p_id: " + p_id.ToString() + ", dbEnte p_dbContext)", EnLogSeverity.Trace);

            EntityType risp = p_dbContext.Set<EntityType>().Find(p_id);

            return risp;
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// 
        /// </summary>
        /// <param name="p_id">Chiave primaria (Decimal)</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static EntityType GetById(Decimal p_id, dbEnte p_dbContext)
        {
            m_logger.LogMessage("GetById(Decimal p_id: " + p_id.ToString() + ", dbEnte p_dbContext)", EnLogSeverity.Trace);

            EntityType risp = p_dbContext.Set<EntityType>().Find(p_id);            

            return risp;
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria. L'entità restituita è scollegata dal context
        /// </summary>
        /// <param name="p_id">ID da ricercare nel campo chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static EntityType GetByIdDetached(Int32 p_id, dbEnte p_dbContext)
        {
            EntityType risp = null;

            try
            {
                risp = GetById(p_id, p_dbContext);
                if (risp != null)
                {
                    ((IObjectContextAdapter)p_dbContext).ObjectContext.Detach(risp);
                }
            }
            catch (Exception e) { }

            return risp;
        }
    }
}
