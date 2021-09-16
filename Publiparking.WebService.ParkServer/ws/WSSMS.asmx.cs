using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Utility.Log;


namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSSMS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSSMS : System.Web.Services.WebService
    {

        protected ILogger logger;

        [ScriptMethod(UseHttpGet = true)]
        public bool SMSReceived(string sender, string body, string recipient, string code, string PID)
        {

            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSSMS));
            
            logger.LogMessage("WSSMS SMSReceived", EnLogSeverity.Debug);
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                SMSIn v_SMSIn = new SMSIn();

                v_SMSIn.dataRicezione = DateTime.Now;
                v_SMSIn.numeroMittente = sender;
                v_SMSIn.testo = body;
                v_SMSIn.numeroDestinatario = recipient;

                return SMSInBD.SaveSMSInFromWebService(v_SMSIn, v_context);                     
               
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSSMS:SMSReceived", ex, EnLogSeverity.Error);
                return false;
            }
        }

    }
}
