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
using Publiparking.Data.dto;
using Publiparking.Data.LinqExtended;
using Publiparking.Data.dto.type;
using System.Configuration;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSStalli
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSStalli : System.Web.Services.WebService
    {
        protected ILogger logger;

        [WebMethod]
        public StalloLightDTO loadStalloByNumero(Int32 pNumero)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSStalli));            
            logger.LogMessage("WSStalli loadStalloByNumero", EnLogSeverity.Debug);
            StalloLightDTO risp = null;
            Stalli v_stallo = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_stallo =  StalliBD.getStalloByNumero(pNumero.ToString(), v_context);

                if (v_stallo != null)
                    risp = v_stallo.toStalloLightDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSStalli:loadStalloByNumero", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public StalloLightDTO loadStallo(Int32 pStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSStalli));
            logger.LogMessage("WSStalli loadStallo", EnLogSeverity.Debug);
            StalloLightDTO risp = null;
            Stalli v_stallo = null;
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_stallo = StalliBD.GetById(pStallo,v_context);

                if (v_stallo != null)
                    risp = v_stallo.toStalloLightDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSStalli:loadStalloByNumero", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public StatoStalloExt getStatoCorrente(Int32 pIDStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSStalli));
            logger.LogMessage("WSStalli getStatoCorrente idstallo = " + pIDStallo, EnLogSeverity.Debug);

            StatoStalloExt risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = StalliBD.getStatoCorrente(pIDStallo, v_context);
                logger.LogMessage(string.Format("stato: {0}, targa: {1}, note: {2}, data: {3}",risp.Stato,risp.Targa,risp.Note,risp.Data.ToString()), EnLogSeverity.Debug);

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSStalli:getStatoCorrente", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public StatoStalloExt getUltimoStatoAbusivo(Int32 pIDStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSStalli));
            logger.LogMessage("WSStalli getUltimoStatoAbusivo", EnLogSeverity.Debug);

            StatoStalloExt risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = StalliBD.getUltimoStatoAbusivo(pIDStallo, v_context);
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSStalli:getUltimoStatoAbusivo", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isScadutoOltreTolleranza(Int32 pIDStallo)
        {

            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSStalli));
            logger.LogMessage("WSStalli isScadutoOltreTolleranza", EnLogSeverity.Debug);

            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_minutiTolleranzaVerbale = ConfigurationManager.AppSettings["minutiTolleranzaVerbale"].ToString();
                risp = StalliBD.isScadutoOltreTolleranza(pIDStallo, Int32.Parse(v_minutiTolleranzaVerbale), v_context);
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSStalli:getUltimoStatoAbusivo", ex, EnLogSeverity.Error);
            }

            return risp;
        
        }


        [WebMethod]
        public List<StalloLightDTO> loadList(Int32 pIDGiro)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSStalli));
            logger.LogMessage("WSStalli loadList", EnLogSeverity.Debug);

            List<StalloLightDTO> risp = new List<StalloLightDTO>();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = StalliBD.loadStalliByIdGiro(pIDGiro, v_context).ToDTO().ToList();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSStalli:loadList", ex, EnLogSeverity.Error);
            }

            return risp;
        }
    }
}
