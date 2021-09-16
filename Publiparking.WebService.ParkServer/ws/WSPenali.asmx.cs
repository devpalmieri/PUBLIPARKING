using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Utility.Log;
using Publisoftware.Data;
using Publisoftware.Data.BD;
using Publiparking.Data;
using Publiparking.Data.dto;
using Publiparking.Data.LinqExtended;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSPenali
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSPenali : System.Web.Services.WebService
    {
        protected ILogger logger;

        [WebMethod]
        public PenaleDTO loadLast(Int32 pIDOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSPenali));           
            logger.LogMessage("WSPenali loadlist", EnLogSeverity.Debug);

            PenaleDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                IQueryable<Penali> v_penaliQrbLst = PenaliBD.GetList(v_context)
                                                .Where(p => p.idOperatore.Equals(pIDOperatore));
                if (v_penaliQrbLst.Any())
                {
                    risp = v_penaliQrbLst.OrderByDescending(p => p.idPenale).FirstOrDefault().ToPenaleDTO();
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSPenali:loadlist", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isValid(string pCodice, Int32 pIDStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSPenali));
            logger.LogMessage("WSPenali isValid", EnLogSeverity.Debug);
            bool risp = false;
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp =   PenaliBD.isValid(pCodice, pIDStallo, v_context);
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSPenali:isValid", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool IsPenaliEnabled()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSPenali));
            logger.LogMessage("WSPenali IsPenaliEnabled", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();                    
                risp = v_configurazione.penali;
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSPenali:IsPenaliEnabled", ex, EnLogSeverity.Error);
            }

            return risp;
        }

    }
}
