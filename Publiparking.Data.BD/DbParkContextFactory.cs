using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Text;

namespace Publiparking.Data.BD
{
    public class DbParkContextFactory
    {
        private static string m_metadata = "res://*/Entities.DbParkCtxModel.csdl|res://*/Entities.DbParkCtxModel.ssdl|res://*/Entities.DbParkCtxModel.msl";
        public static string Metadata { get { return m_metadata; } }

        /// <summary>
        /// Provider di default per l'accesso Database
        /// </summary>
        public static string Provider { get; set; }

        /// <summary>
        /// Timeout di default per la connessione di accesso al Database
        /// </summary>
        public static Int32 Timeout { get; set; }

        /// <summary>
        /// MaxPoolSize: must be set BEFORE any getConnectionString/getContext...
        /// minimum 100 
        /// </summary>
        public static int MaxPoolSize { get { return _maxPoolSize; } set { _maxPoolSize = value > 100 ? 100 : value; } }
        private static int _maxPoolSize = 1000;

        private static SqlConnectionStringBuilder GetSqlConnectionStringBuilder(string p_server, string p_dbName, string p_userName, string p_password, bool p_pooling = true)
        {
            SqlConnectionStringBuilder sqlConnString = new SqlConnectionStringBuilder();

            sqlConnString.DataSource = p_server;
            sqlConnString.InitialCatalog = p_dbName;
            sqlConnString.UserID = p_userName;
            sqlConnString.Password = p_password;

            sqlConnString.MultipleActiveResultSets = true;
            sqlConnString.MaxPoolSize = _maxPoolSize;
            //sqlConnString.Pooling = false;
            sqlConnString.Pooling = p_pooling;

            // Addons 2017-02-22: TODO: Pietro Cercare: ConnectionLifetime e impostarlo a 30 (il default è 15)

            return sqlConnString;
        }

        /// <summary>
        /// Costruisce una stringa di connessione Entityframework a partire dai parametri passati
        /// </summary>
        /// <param name="p_server">Server che ospita il Database</param>
        /// <param name="p_dbName">>Nome del Database</param>
        /// <param name="p_userName">UserName di accesso al Database</param>
        /// <param name="p_password">Password di accesso al Database</param>
        /// <param name="p_metadata">Stringa dei metadati per l'accesso al model</param>
        /// <param name="p_provider">Provider di accesso al database (Default = System.Data.SqlClient)</param>
        /// <param name="p_timeout">Timeout per l'accesso al Database</param>
        /// <returns>Stringa di connessione EntityFramework</returns>
        public static string getConnectionString(string p_server, string p_dbName, string p_userName, string p_password, string p_metadata, string p_provider, Int32 p_timeout = -1, bool p_pooling = true)
        {
            string retval = string.Empty;

            if (p_dbName != null && p_dbName.Length > 0 && p_server != null && p_server.Length > 0 && p_userName != null && p_userName.Length > 0 && p_password != null && p_password.Length > 0 && p_metadata != null && p_metadata.Length > 0)
            {
                SqlConnectionStringBuilder sqlConnString = GetSqlConnectionStringBuilder(p_server, p_dbName, p_userName, p_password, p_pooling);
                if (p_timeout >= 0)
                {
                    sqlConnString.ConnectTimeout = p_timeout;
                }

                // Addons 2017-02-22: rimosso
                // EntityConnectionStringBuilder entityConnString = new EntityConnectionStringBuilder();

                EntityConnectionStringBuilder entityConnectionString = new EntityConnectionStringBuilder();
                entityConnectionString.Metadata = m_metadata;
                entityConnectionString.Provider = (string.IsNullOrEmpty(p_provider) ? "System.Data.SqlClient" : p_provider);
                entityConnectionString.ProviderConnectionString = sqlConnString.ConnectionString;
                
                //if (!entityConnectionString.ContainsKey("ConnectionLifetime"))
                //{
                //    entityConnectionString["ConnectionLifetime"] = 30;
                //}

                Provider = "System.Data.SqlClient";
                retval = entityConnectionString.ConnectionString;

            }

            return retval;
        }

        /// <summary>
        /// Costruisce una stringa di connessione Sql Server, da utilizzare per query dirette al db senza usare EntityFramework
        /// </summary>
        /// <param name="p_server">Server che ospita il Database</param>
        /// <param name="p_dbName">>Nome del Database</param>
        /// <param name="p_userName">UserName di accesso al Database</param>
        /// <param name="p_password">Password di accesso al Database</param>
        /// <param name="p_timeout">Timeout per l'accesso al Database</param>
        /// <returns>Stringa di connessione - non usare con EF</returns>
        public static string getSqlConnString(string p_server, string p_dbName, string p_userName, string p_password, Int32 p_timeout = -1)
        {
            string retval = string.Empty;

            if (p_dbName != null && p_dbName.Length > 0 && p_server != null && p_server.Length > 0 && p_userName != null && p_userName.Length > 0 && p_password != null && p_password.Length > 0)
            {
                SqlConnectionStringBuilder sqlConnString = GetSqlConnectionStringBuilder(p_server, p_dbName, p_userName, p_password);


                if (p_timeout >= 0)
                {
                    sqlConnString.ConnectTimeout = p_timeout;
                }

                retval = sqlConnString.ConnectionString;
            }

            return retval;
        }

        /// <summary>
        /// Restituisce un Context con i parametri di connessione indicati
        /// </summary>
        /// <param name="p_server">Server che ospita il Database</param>
        /// <param name="p_dbName">Nome del Database</param>
        /// <param name="p_userName">UserName di accesso al Database</param>
        /// <param name="p_password">Password di accesso al Database</param>
        /// <param name="p_timeout">Timeout per l'accesso al Database</param>
        /// <returns></returns>
        public static DbParkCtx getContext(string p_server, string p_dbName, string p_userName, string p_password,Int32 p_timeout = -1, bool p_pooling = true)
        {
            Int32 v_timeout = Timeout;

            if (p_timeout >= 0)
                v_timeout = p_timeout;

            // var p = new dbEnte();

            string connectionString = getConnectionString(p_server, p_dbName, p_userName, p_password, m_metadata, Provider, v_timeout, p_pooling);
            if (connectionString == String.Empty)
            {
                throw new Exception($"Errore: impossibile costruire una connection string");
                //TODO: loggare parametri
            }
            DbParkCtx context = new DbParkCtx(connectionString);

            if (v_timeout >= 0)
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = v_timeout;
            }

            return context;
        }




        // Crea il contesto a aprtire dalla stringa di connessione ottenuta tramite getConnectionString
        public static DbParkCtx getContextFromConnStr(string connectionString,Int32 p_timeout = -1)
        {
            Int32 v_timeout = Timeout;
            if (p_timeout >= 0)
                v_timeout = p_timeout;
            // ??? var p = new dbEnte();
            DbParkCtx context = new DbParkCtx(connectionString);
            if (v_timeout >= 0)
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = v_timeout;
            }

            return context;
        }
    }
}
