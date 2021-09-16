using Publisoftware.Data;
using Publisoftware.Utility;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class DBInfos
    {
        public DBInfos(bool validateEntities)
        {
            ValidateEntities = validateEntities;
        }

        public DBInfos(string p_server, string p_dbName, string p_userName, string p_passwordEncrypted)
        {
            Server = p_server;
            DbName = p_dbName;
            UserName = p_userName;
            Password = CryptMD5.Decrypt(p_passwordEncrypted); //password in chiaro           
        }
        public bool ValidateEntities { get; set; } = true;

        public string Server { get; set; } = "10.1.1.110";// "10.2.1.196";
        public string DbName { get; set; } = "db_lombardia_anagrafice"; // "db_lombardia_sviluppo";// "db_lombardia_anagrafice"; // "db_lombardia_sviluppo_da_produzione"; 
        public string UserName { get; set; } = "sa";
        // TODO: usare pass criptata
        public string Password { get; set; } = "Admin1234"; // "H6f4s2l0";

        public static DBInfos CreateSviluppo(bool validateEntities, string dbName = "db_caserta_sviluppo")
        {
            return new DBInfos(validateEntities)
            {
                Server = "10.1.1.110",
                DbName = dbName, 
                UserName = "sa",
                Password = "Admin1234"
            };
        }
        
        [Obsolete("Usare CreateFormazione")]
        public static DBInfos CreateFormazion(bool validateEntities, string dbName = "db_caserta_formazione")
        {
            return CreateFormazione(validateEntities, dbName);
        }
        public static DBInfos CreateFormazione(bool validateEntities, string dbName = "db_caserta_formazione")
        {
            return new DBInfos(validateEntities)
            {
                Server = "10.2.1.196",
                DbName = dbName,
                UserName = "sa",
                Password = "H6f4s2l0"
            };
        }

        // Per effettuare switch DB a runtime
        public static DBInfos CreateFromEnte(anagrafica_ente ente, bool validateEntities)
        {
            return new DBInfos(validateEntities)
            {
                Server = ente.indirizzo_ip_db,
                DbName = ente.nome_db,
                UserName = ente.user_name_db,
                Password = ente.password_dbD // dec.
            };
        }

        public Func<dbEnte> Get_getCtxCb(int idStruttura, int idRisorsa)
        {
            return () => EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.Password, idStruttura, idRisorsa, -1, false, null, ValidateEntities);
        }

        public Func<dbEnte> Get_getReadOnlyCtxCb(int idStruttura, int idRisorsa)
        {
            return () => EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.Password, idStruttura, idRisorsa, -1, true, null, ValidateEntities);
        }

        public Func<dbEnte> Get_getCtxCbLogAlQueries(int idStruttura, int idRisorsa, ILogger logger)
        {
            return () =>
            {
                var ctx = EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.Password, idStruttura, idRisorsa, -1, false, null, ValidateEntities);
                ctx.Database.Log = (msg) => DbQueryLogger(msg, logger);
                return ctx;
            };
        }
        public Func<dbEnte> Get_getReadOnlyCtxCbLogAlQueries(int idStruttura, int idRisorsa, ILogger logger)
        {
            return () =>
            {
                var ctx = EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.Password, idStruttura, idRisorsa, -1, true, null, ValidateEntities);
                ctx.Database.Log = (msg) => DbQueryLogger(msg, logger);
                return ctx;
            };
        }

        public dbEnte GetCtx(int idStruttura, int idRisorsa)
        {
            return EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.Password, idStruttura, idRisorsa, -1, false, null, ValidateEntities);
        }
        public dbEnte GetReadOnlyCtx(int idStruttura, int idRisorsa)
        {
            return EnteContextFactory.getContext(this.Server, this.DbName, this.UserName, this.Password, idStruttura, idRisorsa, -1, true, null, ValidateEntities);
        }

        public dbEnte GetCtx(int idStruttura, int idRisorsa, bool proxyCreationEnabled)
        {
            var ctx = GetCtx(idStruttura, idRisorsa);
            ctx.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            return ctx;
        }
        public dbEnte GetReadOnlyCtx(int idStruttura, int idRisorsa, bool proxyCreationEnabled)
        {
            var ctx = GetReadOnlyCtx(idStruttura, idRisorsa);
            ctx.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            return ctx;
        }

        public string GetSqlConnectionString(int timeout = -1)
        {
            return EnteContextFactory.getSqlConnString(this.Server, this.DbName, this.UserName, this.Password, timeout);
        }

        public static string GetSqlConnectionString(DBInfos dbInfos, int timeout = -1)
        {
            return EnteContextFactory.getSqlConnString(dbInfos.Server, dbInfos.DbName, dbInfos.UserName, dbInfos.Password, timeout);
        }


        public static Func<dbEnte> Get_getCtxCb(DBInfos dbInfos, int idStruttura, int idRisorsa)
        {
            return () => EnteContextFactory.getContext(dbInfos.Server, dbInfos.DbName, dbInfos.UserName, dbInfos.Password, idStruttura, idRisorsa, -1, false, null, dbInfos.ValidateEntities);
        }
        public static Func<dbEnte> Get_getReadOnlyCtxCb(DBInfos dbInfos, int idStruttura, int idRisorsa)
        {
            return () => EnteContextFactory.getContext(dbInfos.Server, dbInfos.DbName, dbInfos.UserName, dbInfos.Password, idStruttura, idRisorsa, -1, true, null, dbInfos.ValidateEntities);
        }

#if true || DEBUG
        private static int _queryLoggerCount = 0;
        public static void DbQueryLogger(string msg, ILogger logger)
        {
            const EnLogSeverity querylogSeverity = EnLogSeverity.Debug;
            if (String.IsNullOrWhiteSpace(msg)) { return; }
            //System.Diagnostics.Debug.WriteLine(msg);
            //logger.LogMessage("Query: ", EnLogSeverity.Debug);

            if (!msg.StartsWith("--") && !msg.StartsWith("Connessione a"))
            {
                logger.LogMessage($"({_queryLoggerCount++}) ----------------------------------------", querylogSeverity);
            }
            logger.LogMessage(msg, querylogSeverity);
        }
#else
        public static void DbQueryLogger(string msg, ILogger logger)
        {
        }
#endif
    }
}
