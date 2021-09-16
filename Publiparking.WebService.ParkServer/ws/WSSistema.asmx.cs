using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSSistema
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSSistema : System.Web.Services.WebService
    {
        protected ILogger logger;


        [WebMethod]
        public DateTime getDateTime()
        {
            return DateTime.Now;
        }
        [WebMethod()]
        public bool isValidVersion(string pVersione)
        {

            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSSistema));
            logger.LogMessage("WSSistema isValidVersion: " + pVersione, EnLogSeverity.Debug);
            bool risp = false;
            string[] vLivelliMinimi = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();
                vLivelliMinimi = v_configurazione.versioneMinimaTerminali.Split('.');
                if (pVersione.Length > 0)
                {
                    string[] vLivelliVersione = pVersione.Split('.');

                    risp = true;
               
                    for (var i = 0; i <= vLivelliMinimi.Length - 1; i++)
                    {
                        int vLivelloVersione;

                        if (!Int32.TryParse(vLivelliVersione[i], out vLivelloVersione))
                        {
                            risp = false;
                            break;
                        }

                        if (Int32.Parse(vLivelliMinimi[i]) > vLivelloVersione)
                        {
                            risp = false;
                            break;
                        }
                    }
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSSistema:isValidVersion", ex, EnLogSeverity.Error);
                risp = false;
            }

            return risp;
        }

        [WebMethod]
        public bool sendFile(string p_nomeFile, byte[] p_image)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSSistema));
            logger.LogMessage("WSSistema sendFile", EnLogSeverity.Debug);

            bool risp = false;

            try
            {
                string v_netDrive = ConfigurationManager.AppSettings["netDrive"].ToString();
                string v_netPath = ConfigurationManager.AppSettings["netPath"].ToString();
                string v_netUser = ConfigurationManager.AppSettings["netUser"].ToString();
                string v_netPassword = ConfigurationManager.AppSettings["netPassword"].ToString();
                string v_netSubPath = ConfigurationManager.AppSettings["netSubPath"].ToString();

                string v_destinazione = v_netDrive + @":\" + v_netSubPath + @"\";
                v_destinazione = v_destinazione + p_nomeFile.Substring(0, 4) + @"\" + p_nomeFile.Substring(4, 2) + @"\" + p_nomeFile;

                System.IO.MemoryStream v_memoryStream = new System.IO.MemoryStream(p_image);

                using (FileStream v_file = new FileStream(v_destinazione, FileMode.Create, System.IO.FileAccess.Write))
                {
                    v_memoryStream.WriteTo(v_file);
                    //byte[] bytes = new byte[v_memoryStream.Length + 1];
                    //v_memoryStream.Read(bytes, 0, (int)v_memoryStream.Length);
                    //v_file.Write(bytes, 0, bytes.Length);
                    //v_memoryStream.Close();                   
                    risp = true;                                    
                }
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSSistema:isValidVersion", ex, EnLogSeverity.Error);

            }

            return risp;
        }

        [WebMethod]
        public bool pagamentoConTarga()
        {

            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSSistema));
            logger.LogMessage("WSSistema pagamentoConTarga", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();
                risp = v_configurazione.pagamentoConTarga;
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSSistema:pagamentoConTarga", ex, EnLogSeverity.Error);
            }

            return risp;
        }


        [WebMethod]
        public bool emissioneTitoliOperatore()
        {

            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSSistema));
            logger.LogMessage("WSSistema emissioneTitoliOperatore", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();
                risp = v_configurazione.emissioneTitoliOperatore;
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSSistema:emissioneTitoliOperatore", ex, EnLogSeverity.Error);
            }

            return risp;           
        }

    }
}
