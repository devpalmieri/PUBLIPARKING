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
using Publiparking.Data.dto;
using Publiparking.Data.LinqExtended;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSOperatore
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSOperatore : System.Web.Services.WebService
    {
        protected ILogger logger;

        [WebMethod]
        public bool VerifyPassword(string pUserName, string pPassword)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore VerifyPassword", EnLogSeverity.Debug);

            bool risp = false;
            bool v_attivo = false;
            bool v_password = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_attivo = OperatoriBD.isAttivo(pUserName, v_context);
                v_password = OperatoriBD.verifyPassword(pUserName, pPassword, v_context);

                if (v_attivo & v_password)
                    risp = true;

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:VerifyPassword", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool ChangePassword(string pUserName, string pOldPassword, string pNewPassword)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore ChangePassword", EnLogSeverity.Debug);

            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();

                if (OperatoriBD.verifyPassword(pUserName, pOldPassword, v_context)) // ridondante
                {
                    risp = OperatoriBD.changePassword(pUserName, pOldPassword, pNewPassword, v_context);
                }

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:ChangePassword", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isExpiredPassword(Int32 pIDOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore isExpiredPassword", EnLogSeverity.Debug);

            bool risp = false;
            Operatori vOperatore = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                vOperatore = OperatoriBD.GetById(pIDOperatore, v_context);
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();
                int numGiorniValiditaPassword = Decimal.ToInt32(v_configurazione.giorniScadenzaPassword);
                if (vOperatore != null)
                    risp = OperatoriBD.isExpiredPassword(vOperatore.username, numGiorniValiditaPassword, v_context);

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:isExpiredPassword", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public OperatoreDTO loadByUserName(string pUserName)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore loadByUserName", EnLogSeverity.Debug);
            OperatoreDTO v_operatore = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_operatore = OperatoriBD.GetList(v_context).Where(o => o.username.Equals(pUserName))
               .OrderByDescending(o => o.idOperatore)
               .FirstOrDefault().ToOperatoreDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:loadByUserName", ex, EnLogSeverity.Error);
            }

            return v_operatore;
        }
        [WebMethod]
        public OperatoreDTO loadById(int pId)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore loadById --> id: " + pId, EnLogSeverity.Debug);
            OperatoreDTO v_operatore = new OperatoreDTO();
            if (pId == 0) return v_operatore;
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_operatore = OperatoriBD.GetById(pId, v_context).ToOperatoreDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:loadById", ex, EnLogSeverity.Error);
            }

            return v_operatore;
        }

        [WebMethod]
        public List<OperatoreDTO> loadListWithoutPassword()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore loadListWithoutPassword", EnLogSeverity.Debug);
            List<OperatoreDTO> v_listaOperatori = new List<OperatoreDTO>();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_listaOperatori = OperatoriBD.GetList(v_context)
                    .Where(o => string.IsNullOrEmpty(o.password)).ToDTO().ToList();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:loadById", ex, EnLogSeverity.Error);
            }

            return v_listaOperatori;
        }

        [WebMethod]
        public OperazioneDTO getLastOperazione(int pIdOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperatore));
            logger.LogMessage("WSOperatore getLastOperazione", EnLogSeverity.Debug);
            OperazioneDTO v_operazione = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_operazione = OperatoriBD.loadLastByIdOperatore(pIdOperatore, v_context);

                Global.DbInfo.DisposeParkctx(v_context);

            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperatore:loadById", ex, EnLogSeverity.Error);
            }

            return v_operazione;         

        }
    }
}
