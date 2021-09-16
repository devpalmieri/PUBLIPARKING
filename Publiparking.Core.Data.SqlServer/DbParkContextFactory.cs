using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer
{
    public class DbParkContextFactory
    {
        //private static string m_metadata = "res://*/Entities.DbParkCtxModel.csdl|res://*/Entities.DbParkCtxModel.ssdl|res://*/Entities.DbParkCtxModel.msl";
        //public static string Metadata { get { return m_metadata; } }

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

            sqlConnString.ConnectTimeout = 1000;
            sqlConnString.MultipleActiveResultSets = true;
            sqlConnString.MaxPoolSize = _maxPoolSize;
            //sqlConnString.Pooling = false;
            sqlConnString.Pooling = p_pooling;
            sqlConnString.IntegratedSecurity = false;
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
        public static string getConnectionString(string p_server, string p_dbName, string p_userName, string p_provider, string p_password)
        {
            string retval = string.Empty;

            if (p_dbName != null && p_dbName.Length > 0 && p_server != null && p_server.Length > 0 && p_userName != null && p_userName.Length > 0 && p_password != null && p_password.Length > 0)
            {
                SqlConnectionStringBuilder sqlConnString = GetSqlConnectionStringBuilder(p_server, p_dbName, p_userName, p_password);
                //if (p_timeout >= 0)
                //{
                //    sqlConnString.ConnectTimeout = p_timeout;
                //}
                sqlConnString.ConnectionString.Replace("Data Source", "Server").Replace("Initial Catalog", "database").Replace("Integrated Security", "Trusted_Connection");
                retval = sqlConnString.ConnectionString;

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
        public static DbParkContext getContext(string p_server, string p_dbName, string p_userName, string p_password,
             int p_struttura = 0, int p_risorsa = 0, Int32 p_timeout = -1, bool p_pooling = true, bool isReadOnly = false)
        {
            Int32 v_timeout = Timeout;

            if (p_timeout >= 0)
                v_timeout = p_timeout;

            // var p = new dbEnte();

            string connectionString = getConnectionString(p_server, p_dbName, p_userName, Provider, p_password);
            if (connectionString == String.Empty)
            {
                throw new Exception($"Errore: impossibile costruire una connection string");
            }
            DbParkContext context = new DbParkContext(connectionString);

            //if (v_timeout >= 0)
            //{
            //    ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = v_timeout;
            //}

            return context;
        }




        // Crea il contesto a aprtire dalla stringa di connessione ottenuta tramite getConnectionString
        public static DbParkContext getContextFromConnStr(string connectionString, Int32 p_timeout = -1)
        {
            Int32 v_timeout = Timeout;
            if (p_timeout >= 0)
                v_timeout = p_timeout;
            // ??? var p = new dbEnte();
            DbParkContext context = new DbParkContext(connectionString);
            if (v_timeout >= 0)
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = v_timeout;
            }

            return context;
        }
    }

}
