using Publiparking.Core.Data.SqlServer;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Configuration
{
    public static class StartDatabaseSetting
    {


        private static readonly string _dbNameGenerale;
        private static readonly string _dbPassword;
        private static readonly string _dbServerGenerale;
        private static readonly string _dbUserName;
        private static readonly int _idRisorsa;
        private static readonly int _idStruttura;

        public static string DbNameGenerale { get { return _dbNameGenerale; } }
        public static string DbPassword { get { return _dbPassword; } }
        public static string DbServerGenerale { get { return _dbServerGenerale; } }
        public static string DbUserName { get { return _dbUserName; } }
        public static int IdRisorsa { get { return _idRisorsa; } }
        public static int IdStruttura { get { return _idStruttura; } }

        static StartDatabaseSetting()
        {
            _dbServerGenerale = AppSettings.Instance.Get<string>("DatabaseConfig:DbServerGenerale");
            _dbNameGenerale = AppSettings.Instance.Get<string>("DatabaseConfig:DbNameGenerale");
            _dbUserName = AppSettings.Instance.Get<string>("DatabaseConfig:DbUserName");
            _dbPassword = CryptMD5.Decrypt(AppSettings.Instance.Get<string>("DatabaseConfig:DbPassword"));
            _idStruttura = Int32.Parse(AppSettings.Instance.Get<string>("DatabaseConfig:IdStruttura") != null ? AppSettings.Instance.Get<string>("DatabaseConfig:IdStruttura") : "99");
            _idRisorsa = Int32.Parse(AppSettings.Instance.Get<string>("DatabaseConfig:IdRisorsa") != null ? AppSettings.Instance.Get<string>("DatabaseConfig:IdRisorsa") : "2035");
        }
        public static string GetGeneraleConnectionString()
        {
            return DbParkContextFactory.getSqlConnString(
                        _dbServerGenerale,
                        _dbNameGenerale,
                        _dbUserName,
                        _dbPassword);
        }
        public static DbParkContext GetGeneraleCtx()
        {
            DbParkContext ctx = DbParkContextFactory.getContext(
                        _dbServerGenerale,
                        _dbNameGenerale,
                        _dbUserName,
                        _dbPassword,
                        _idStruttura,
                        _idRisorsa
                        );

            //if (_logQueries)
            //{
            //    ctx.Database.Log = DbQueryLogger;
            //}
            return ctx;
        }

        public static DbParkContext GetGeneraleReadOnlyCtx()
        {
            DbParkContext ctx = DbParkContextFactory.getContext(
                        _dbServerGenerale,
                        _dbNameGenerale,
                        _dbUserName,
                        _dbPassword,
                        _idStruttura,
                        _idRisorsa,
                        -1,
                        true);
            //if (_logQueries)
            //{
            //    ctx.Database.Log = DbQueryLogger;
            //}
            return ctx;
        }
    }
    public class DatabaseConfig
    {
        private readonly string _dbNameGenerale;
        private readonly string _dbPassword;
        private readonly string _dbServerGenerale;
        private readonly string _dbUserName;
        private readonly int _idRisorsa;
        private readonly int _idStruttura;

        public string DbNameGenerale { get { return _dbNameGenerale; } }
        public string DbPassword { get { return _dbPassword; } }
        public string DbServerGenerale { get { return _dbServerGenerale; } }
        public string DbUserName { get { return _dbUserName; } }
        public int IdRisorsa { get { return _idRisorsa; } }
        public int IdStruttura { get { return _idStruttura; } }

        public DatabaseConfig()
        {
            _dbServerGenerale = AppSettings.Instance.Get<string>("DatabaseConfig:DbServerGenerale");
            _dbNameGenerale = AppSettings.Instance.Get<string>("DatabaseConfig:DbNameGenerale");
            _dbUserName = AppSettings.Instance.Get<string>("DatabaseConfig:DbUserName");
            _dbPassword = CryptMD5.Decrypt(AppSettings.Instance.Get<string>("DatabaseConfig:DbPassword"));
            _idStruttura = Int32.Parse(AppSettings.Instance.Get<string>("DatabaseConfig:IdStruttura") != null ? AppSettings.Instance.Get<string>("DatabaseConfig:IdStruttura") : "99");
            _idRisorsa = Int32.Parse(AppSettings.Instance.Get<string>("DatabaseConfig:IdRisorsa") != null ? AppSettings.Instance.Get<string>("DatabaseConfig:IdRisorsa") : "2035");
        }

        public string GetGeneraleConnectionString()
        {
            return DbParkContextFactory.getSqlConnString(
                        _dbServerGenerale,
                        _dbNameGenerale,
                        _dbUserName,
                        _dbPassword);
        }
        public DbParkContext GetGeneraleCtx()
        {
            DbParkContext ctx = DbParkContextFactory.getContext(
                        _dbServerGenerale,
                        _dbNameGenerale,
                        _dbUserName,
                        _dbPassword,
                        _idStruttura,
                        _idRisorsa
                        );

            //if (_logQueries)
            //{
            //    ctx.Database.Log = DbQueryLogger;
            //}
            return ctx;
        }

        public DbParkContext GetGeneraleReadOnlyCtx()
        {
            DbParkContext ctx = DbParkContextFactory.getContext(
                        _dbServerGenerale,
                        _dbNameGenerale,
                        _dbUserName,
                        _dbPassword,
                        _idStruttura,
                        _idRisorsa,
                        -1,
                        true);
            //if (_logQueries)
            //{
            //    ctx.Database.Log = DbQueryLogger;
            //}
            return ctx;
        }
    }
}
