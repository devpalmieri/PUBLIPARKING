using Publiparking.Data.BD;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Publiparking.Web.NinoServer
{
    public class MvcApplication : System.Web.HttpApplication
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

        protected void Application_Start()
        {
            string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
            string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
            string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
            string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();            

            DbInfo = new DBInfos(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
