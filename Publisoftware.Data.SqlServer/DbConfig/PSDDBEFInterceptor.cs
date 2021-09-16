#if false
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.DbConfig
{
    // https://cmatskas.com/logging-and-tracing-with-entity-framework-6/
    public class PSDDBEFInterceptor : IDbCommandInterceptor
    {
        static readonly ILogger _log;
        private static ILogger Log { get { return _log; } }

        // Callback
        private static Func<string> _logStringCb = null;
        public static void SetOnCommandLoggedCB(Func<string> cb)
        {
            if (cb == null)
            {
                throw new ArgumentException(nameof(cb));
            }

            _logStringCb = cb;
        }

        static PSDDBEFInterceptor()
        {
            _log = LoggerFactory.getInstance().getLogger<NLogger>("PSDDBEFInterceptor");
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            //LogSql(command, interceptionContext);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogSql(command, interceptionContext);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            //LogSql(command, interceptionContext);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogSql(command, interceptionContext);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            //LogSql(command, interceptionContext);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogSql(command, interceptionContext);
        }

        public void LogSql<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            WriteLog($" IsAsync: {interceptionContext.IsAsync}, Command Text:{Environment.NewLine}-----------------------{Environment.NewLine}{command.CommandText}-----------------------{Environment.NewLine}");
        }

        public static void WriteLog(string command)
        {
            Log.LogMessage(command, EnLogSeverity.Debug);
            if (_logStringCb != null)
            {
                Log.LogMessage(_logStringCb(), EnLogSeverity.Debug);
            }
        }

        public static void WriteLogNCB(string command)
        {
            Log.LogMessage(command, EnLogSeverity.Debug);
        }

    }
}
#endif
