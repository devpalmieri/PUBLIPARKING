//using Publiparking.Service.Base;
using Publiparking.Data.BD;
using Publisoftware.Utility;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Publiparking.WebService.ParkTickets
{
    public class Global : System.Web.HttpApplication
    {       

        private static bool _logAllCreateDesRequests;
        public static bool LogAllCreateDesRequests
        {
            get
            {
                return _logAllCreateDesRequests;
            }
            private set
            {
                _logAllCreateDesRequests = value;
            }
        }


        private static string _keyDes;
        public static string KeyDes
        {
            get
            {
                return _keyDes;
            }
            private set
            {
                _keyDes = value;
            }
        }

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
            string v_idEnte = ConfigurationManager.AppSettings["IdEnte"].ToString();
            string v_DbLogDaysToRetain = ConfigurationManager.AppSettings["DbLogDaysToRetain"].ToString();
            bool v_DbLogRemoveOldExceptions = ConfigurationManager.AppSettings["DbLogRemoveOldExceptions"].ToString() == "True" ? true : false;

            KeyDes = ConfigurationManager.AppSettings["chiaveDesEnte"].ToString(); 
            LogAllCreateDesRequests = ConfigurationManager.AppSettings["LogAllCreateDesRequests"].ToString() == "True" ? true : false;
            //Logger = LoggerFactory.getInstance().getLogger<NLogger>(this);
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(Global));
            FlowBird.Cript.Lib.ParkLog.DBLogger.Init(v_dbServer,v_dbUserName, CryptMD5.Decrypt(v_dbPassWord),v_dbName, Int32.Parse(v_idEnte),false);

            FlowBird.Cript.Lib.ParkLog.DBLogger.ClearLogTable(Int32.Parse(v_DbLogDaysToRetain), v_DbLogRemoveOldExceptions);         

            DbInfo = new DBInfos(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);

            logger.LogMessage(String.Format("DBNAME = {0} Avvio del servizio...",v_dbName), EnLogSeverity.Info);

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