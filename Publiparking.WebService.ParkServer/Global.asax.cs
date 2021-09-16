using Publiparking.Data.BD;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Publiparking.WebService.ParkServer
{
    public class Global : System.Web.HttpApplication
    {
        private static DBInfos _dbInfo;
        public static DBInfos DbInfo
        {
            get
            {
                return _dbInfo;
            }
            set
            {
                _dbInfo = value;
            }
        }
        protected ILogger logger;
        protected void Application_Start(object sender, EventArgs e)
        {
            string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
            string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
            string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
            string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(Global));

            _dbInfo = new DBInfos(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);

            logger.LogMessage(String.Format("DBNAME = {0} Avvio del servizio...", v_dbName), EnLogSeverity.Info);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}