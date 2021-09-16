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
using System.Configuration;
using Publiparking.Data.dto.type;
using Publisoftware.Data;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSOperazioni
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSOperazioni : System.Web.Services.WebService
    {
        protected ILogger logger;
        /// <summary>
        /// Restituisce quali parametri sono richiesti per la prossima operazione
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIdStallo"></param>
        /// <returns></returns>
        [WebMethod]
        public OperazioneParamRequired getRequiredParams(Int32 pIDOperatore, Int32 pIdStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni getRequiredParams", EnLogSeverity.Debug);
            OperazioneParamRequired risp = new OperazioneParamRequired();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = OperatoriBD.nextOperazioneParamRequired(pIDOperatore, pIdStallo, v_context);
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:getRequiredParams", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Verifica se è possibile emettere il verbale di sosta
        /// </summary>
        /// <param name="pIDStallo"></param>
        /// <param name="pTarga"></param>
        /// <returns></returns>
        [WebMethod]
        public bool isVerbalizzabileSosta(Int32 pIDStallo, string pTarga)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni isVerbalizzabileSosta", EnLogSeverity.Debug);
            bool retval = false;
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_minutiTolleranzaVerbale = ConfigurationManager.AppSettings["minutiTolleranzaVerbale"].ToString();
                retval = StalliBD.isVerbalizzabileSosta(pIDStallo, pTarga, Int32.Parse(v_minutiTolleranzaVerbale), v_context);
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:isVerbalizzabileSosta", ex, EnLogSeverity.Error);
            }
            return retval;
        }

        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su libero
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToLibero(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pFileFoto)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToLibero", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp.eseguito = StalliBD.setStatoToLibero(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, v_context);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToLibero", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su Occupato Regolare
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pTarga"></param>
        /// <param name="pFileFoto"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoRegolare(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pTarga, string pFileFoto)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoRegolare", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp.eseguito = StalliBD.setStatoToOccupatoRegolare(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pTarga, pFileFoto, v_context);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoRegolare", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su Occupato con titolo
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pCodiceTitolo"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoConTitolo(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pCodiceTitolo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoConTitolo", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp.eseguito = StalliBD.setStatoToOccupatoConTitolo(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pCodiceTitolo, v_context);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoConTitolo", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su Occupato con titolo
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pCodiceTitolo"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoConTitoloTarga(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pCodiceTitolo, string pTarga)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoConTitolo", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp.eseguito = StalliBD.setStatoToOccupatoConTitolo(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pCodiceTitolo, v_context, pTarga);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoConTitolo", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        
        /// <summary>
                 /// Conferma o imposta lo stato corrente dello stallo su Occupato Gratuitamente
                 /// </summary>
                 /// <param name="pIDOperatore"></param>
                 /// <param name="pIDStallo"></param>
                 /// <param name="pIDGiro"></param>
                 /// <param name="pX"></param>
                 /// <param name="pY"></param>
                 /// <param name="pFileFoto"></param>
                 /// <param name="pCodicePermesso"></param>
                 /// <param name="pComune"></param>
                 /// <param name="pTarga"></param>
                 /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoGratuito(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pFileFoto, string pCodicePermesso, string pComune, string pTarga)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoGratuito", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();
            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();
                dbEnte v_context = Global.DbInfo.GetEnteCtx();
                risp.eseguito = StalliBD.setStatoToOccupatoGratuito(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, pCodicePermesso, pComune, pTarga, v_contextPark, v_context);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;


                Global.DbInfo.DisposeParkctx(v_contextPark);
                Global.DbInfo.DisposeEntectx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoGratuito", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        /// <summary>
        ///  Conferma o imposta lo stato corrente dello stallo su Occupato Abusivamente
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <param name="pTipoVeicolo"></param>
        /// <param name="pMarca"></param>
        /// <param name="pModello"></param>
        /// <param name="pTarga"></param>
        /// <param name="pTargaEstera"></param>
        /// <param name="pUbicazione"></param>
        /// <param name="pAssenzaTrasgressore"></param>
        /// <param name="pCodiceBollettino"></param>
        /// <param name="pListaCodici"></param>
        /// <param name="pNote"></param>
        /// <returns>
        /// Restituisce l'ID del verbale generato o -1 in caso di errore
        /// </returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoAbusivo(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, List<string> pFileFoto, string pTipoVeicolo, string pMarca, string pModello, string pTarga, bool pTargaEstera, string pUbicazione, bool pAssenzaTrasgressore, string pCodiceBollettino, Int32[] pListaCodici, string pNote) // , _
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoAbusivo", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();
            Int32 vIdVerbale;
            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();
                string v_primaSerie = ConfigurationManager.AppSettings["primaSerie"].ToString();

                vIdVerbale = StalliBD.setStatoToOccupatoAbusivo(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, pTipoVeicolo, pMarca, pModello, pTarga, pTargaEstera, pUbicazione, pAssenzaTrasgressore, pCodiceBollettino, v_primaSerie, pListaCodici.ToList(), pNote, v_contextPark);

                if ((vIdVerbale >= 0))
                {
                    risp.eseguito = true;
                    risp.risultato = vIdVerbale.ToString();
                }

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoAbusivo", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su Occupato Abusivamente
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <param name="pTipoVeicolo"></param>
        /// <param name="pMarca"></param>
        /// <param name="pModello"></param>
        /// <param name="pTarga"></param>
        /// <param name="pUbicazione"></param>
        /// <param name="pAssenzaTrasgressore"></param>
        /// <param name="pCodiceBollettino"></param>
        /// <param name="pListaCodici"></param>
        /// <param name="pNote"></param>
        /// <returns>
        ///  Restituisce l'ID del verbale generato o -1 in caso di errore
        /// </returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoAbusivoEx(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, List<string> pFileFoto, string pTipoVeicolo, string pMarca, string pModello, string pTarga, string pUbicazione, bool pAssenzaTrasgressore, string pCodiceBollettino, Int32[] pListaCodici, string pNote) // , _
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoAbusivoEx", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();
            Int32 vIdVerbale;

            try
            {

                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();
                string v_primaSerie = ConfigurationManager.AppSettings["primaSerie"].ToString();

                vIdVerbale = StalliBD.setStatoToOccupatoAbusivo(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, pTipoVeicolo, pMarca, pModello, pTarga, false, pUbicazione, pAssenzaTrasgressore, pCodiceBollettino, v_primaSerie, pListaCodici.ToList(), pNote, v_contextPark);

                if ((vIdVerbale >= 0))
                {
                    risp.eseguito = true;
                    risp.risultato = vIdVerbale.ToString();
                }

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoAbusivoEx", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su Occupato Abusivamente con penale
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <param name="pTipoVeicolo"></param>
        /// <param name="pMarca"></param>
        /// <param name="pModello"></param>
        /// <param name="pTarga"></param>
        /// <param name="pUbicazione"></param>
        /// <param name="pAssenzaTrasgressore"></param>
        /// <param name="pNote"></param>
        /// <returns>
        /// Restituisce l'ID della penale generata o -1 in caso di errore
        /// </returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoAbusivoPenale(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, List<string> pFileFoto, string pTipoVeicolo, string pMarca, string pModello, string pTarga, string pUbicazione, bool pAssenzaTrasgressore, string pNote) // , _
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoAbusivoPenale", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();
            Int32 v_IdPenale;

            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();

                v_IdPenale = StalliBD.setStatoToOccupatoAbusivoPenale(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, pTipoVeicolo, pMarca, pModello, pTarga, false, pUbicazione, pAssenzaTrasgressore, pNote, v_contextPark);

                if ((v_IdPenale >= 0))
                {
                    risp.eseguito = true;
                    risp.risultato = v_IdPenale.ToString();
                }

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoAbusivoPenale", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Conferma o imposta lo stato corrente dello stallo su Occupato Abusivamente con penale
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <param name="pTipoVeicolo"></param>
        /// <param name="pMarca"></param>
        /// <param name="pModello"></param>
        /// <param name="pTarga"></param>
        /// <param name="pUbicazione"></param>
        /// <param name="pAssenzaTrasgressore"></param>
        /// <param name="pNote"></param>
        /// <returns>
        /// Restituisce l'ID della penale generata o -1 in caso di errore
        /// </returns>
        [WebMethod]
        public WSReturn setStatoToOccupatoAbusivoPenaleEx(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, List<string> pFileFoto, string pTipoVeicolo, string pMarca, string pModello, string pTarga, bool pTargaEstera, string pUbicazione, bool pAssenzaTrasgressore, string pNote)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToOccupatoAbusivoPenaleEx", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();
            Int32 v_IdPenale;

            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();

                v_IdPenale = StalliBD.setStatoToOccupatoAbusivoPenale(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, pTipoVeicolo, pMarca, pModello, pTarga, pTargaEstera, pUbicazione, pAssenzaTrasgressore, pNote, v_contextPark);

                if ((v_IdPenale >= 0))
                {
                    risp.eseguito = true;
                    risp.risultato = v_IdPenale.ToString();
                }

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToOccupatoAbusivoPenaleEx", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Imposta lo stato corrente dello stallo su Preavvisato
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <param name="pTarga"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToPreavviso(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pFileFoto, string pTarga)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToPreavviso", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();
                risp.eseguito = StalliBD.setStatoToPreavviso(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, pTarga, v_contextPark);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToPreavviso", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        ///  Conferma o imposta lo stato corrente dello stallo su Verbalizzato
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pCodiceBollettino"></param>
        /// <param name="pFileFoto"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToVerbalizzato(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pCodiceBollettino, string pFileFoto)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToVerbalizzato", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();
                string v_primaSerie = ConfigurationManager.AppSettings["primaSerie"].ToString();

                risp.eseguito = StalliBD.setStatoToVerbalizzato(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pCodiceBollettino, v_primaSerie, pFileFoto, v_contextPark);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToVerbalizzato", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Conferma lo stato corrente dello stallo su Preavvisato
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pIDGiro"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="pFileFoto"></param>
        /// <returns></returns>
        [WebMethod]
        public WSReturn setStatoToGiaPreavvisato(Int32 pIDOperatore, Int32 pIDStallo, Int32 pIDGiro, double pX, double pY, string pFileFoto)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage("WSOperazioni setStatoToGiaPreavvisato", EnLogSeverity.Debug);
            WSReturn risp = new WSReturn();

            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();
                risp.eseguito = StalliBD.setStatoToGiaPreavvisato(pIDOperatore, pIDStallo, pIDGiro, pX, pY, pFileFoto, v_contextPark);

                if (!risp.eseguito)
                {
                    risp.messaggio = "Errore. Verificare la disponibilità GPS";
                    risp.continua = false;
                }
                else
                    risp.continua = true;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:setStatoToGiaPreavvisato", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Associa la targa allo stallo se esiste un titolo pagato con targa
        /// </summary>
        /// <param name="pIDOperatore"></param>
        /// <param name="pIDStallo"></param>
        /// <param name="pTarga"></param>
        /// <returns></returns>
        [WebMethod]
        public string associaStalloTarga(Int32 pIDOperatore, Int32 pIDStallo, string pTarga)
        {
            pTarga = pTarga.Trim();

            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSOperazioni));
            logger.LogMessage(string.Format("WSOperazioni associaStalloTarga --> idstallo {0}, targa{1}",pIDStallo,pTarga), EnLogSeverity.Debug);
            string risp = OperazioneDTO.statoSconosciuto;
            DateTime? v_scadenza = default(DateTime);

            try
            {
                DbParkCtx v_contextPark = Global.DbInfo.GetParkCtx();

                StalliTarghe v_ultimo = StalliTargheBD.getLastByIdStallo(pIDStallo, v_contextPark);

                //Serve a prevenire gli inserimenti multipli a distanza di pochi minuti
                if (v_ultimo == null || v_ultimo.targa != pTarga || v_ultimo.data.AddMinutes(5) <= DateTime.Now)
                {
                    StalliTargheBD.creaStalloTarga(pIDOperatore, pIDStallo, pTarga, v_contextPark);
                }

                // trova la massima scadenza (in giornata o superiore) associata alla targa                
                TitoloDTO v_titolo =TitoloBD.getUltimoPagatoByTarga(pTarga, pIDStallo, v_contextPark);
                if (v_titolo != null)
                {
                    if (v_titolo.scadenza.Date >= DateTime.Now.Date)
                    {
                        v_scadenza = v_titolo.scadenza;

                        // 'Associa targa-stallo e restituisce true
                        // StalloTargaBD.insert(pIDStallo, pTarga, pIDOperatore, Now)
                        // Aggiunge un minuto perchè le scadenze sono a 0 secondi e quindi nel minuto preciso, risulta già scaduto (10:20:00 < 10:20:15 = False)
                        if (v_scadenza < DateTime.Now.AddMinutes(1))
                            // Scaduto
                            risp = OperazioneDTO.statoPagamentoScaduto;
                        else
                            // Non Scaduto
                            risp = OperazioneDTO.statoRegolareConNumero;
                    }
                    else
                        risp = OperazioneDTO.statoAbusivo;
                }
                else
                    risp = OperazioneDTO.statoAbusivo;

                Global.DbInfo.DisposeParkctx(v_contextPark);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSOperazioni:associaStalloTarga", ex, EnLogSeverity.Error);
            }
            logger.LogMessage(string.Format("WSOperazioni associaStalloTarga --> stato {0}", risp), EnLogSeverity.Debug);
            return risp;
        }


    }
}
