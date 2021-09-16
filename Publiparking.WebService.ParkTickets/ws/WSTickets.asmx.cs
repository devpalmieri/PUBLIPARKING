using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using FlowBird.Cript.Lib;
using Publiparking.Data;
using Publiparking.Data.BD;
using Publisoftware.Utility.Log;

namespace Publiparking.WebService.ParkTickets
{
    /// <summary>
    /// Descrizione di riepilogo per WSTickets
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSTickets : System.Web.Services.WebService
    {
        protected ILogger logger;

        private translog SaveTicket(translog p_ticket)
        {

            return TranslogBD.SaveTicketFromWebService(p_ticket, Global.DbInfo);
        }


        //[WebMethod]
        private Int32 Create(translog p_ticket)
        {
            int risp = 0;
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTickets));
            try
            {
                translog tlog = SaveTicket(p_ticket);
                if (tlog != null)
                {
                    risp = 1;
                }

            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod Create", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Funzione per la creazione di un nuovo tiolo nella translog
        /// </summary>
        /// <param name="ePl"></param>
        /// <returns></returns>
        [WebMethod]
        public Int32 CreateEnc(FlowBird.Cript.Lib.EncryptedPayload ePl)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTickets));
            Int32 risp = 0;
            // Dim strDesKeyBytes As String = "17839778773fadde0066e4578710928988398877bb123789" REM 48
            logger.LogMessage("CreateEnc", EnLogSeverity.Debug);
            try
            {
                translog dto = null/* TODO Change to default(_) if this is not a reference type */;
                var tryNoPadding = false;
                try
                {
                    dto = FlowBird.Cript.Lib.PayloadDecrypter.DecryptPayloadTTL(ePl, Global.KeyDes, System.Security.Cryptography.PaddingMode.PKCS7);
                }
                catch (Exception ex)
                {
                    tryNoPadding = true;
                    logger.LogException("Errore nel webMethod CreateEnc", ex, EnLogSeverity.Error);
                }
                if ((tryNoPadding))
                    dto = FlowBird.Cript.Lib.PayloadDecrypter.DecryptPayloadTTL(ePl, Global.KeyDes, System.Security.Cryptography.PaddingMode.None);
                if ((dto == null))
                    risp = Create(dto);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nella lettura della lista degli enti", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        /// <summary>
        /// Funzione per la creazione di un nuovo tiolo nella translog
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Int32 CreateDes(string data)
        {
            logger = LoggerFactory.getInstance().getLogger<NLogger>(this);
            Int32 risp = 0;
            // Dim strDesKeyBytes As String = "17839778773fadde0066e4578710928988398877bb123789" REM 48
            logger.LogMessage("CreateDes1", EnLogSeverity.Debug);
            logger.LogMessage("CreateDes2", EnLogSeverity.Info);
            string us = null;
            try
            {
                try
                {
                    // ----------------------------------------------------------------------------------------------------------------------------
                    var tryNoPadding = false;

                    try
                    {
                        us = FlowBird.Cript.Lib.PayloadDecrypter.DecryptStringToString(data, Global.KeyDes, System.Security.Cryptography.PaddingMode.PKCS7);
                    }
                    catch (Exception ex)
                    {
                        tryNoPadding = true;
                        logger.LogException("Errore nel webMethod CreateDes 1", ex, EnLogSeverity.Error);
                    }

                    if (tryNoPadding)
                        // questo non è in try/catch innestato, quindi se gfallisce va ad eseguire "LogExceptionToDb"
                        us = FlowBird.Cript.Lib.PayloadDecrypter.DecryptStringToString(data, Global.KeyDes, System.Security.Cryptography.PaddingMode.None);

                    // ----------------------------------------------------------------------------------------------------------------------------
                    if ((Global.LogAllCreateDesRequests))
                        FlowBird.Cript.Lib.ParkLog.DBLogger.LogToDb(data, us);// N.B.: se "us = Nothing" salva null in Db
                }
                catch (Exception ex)
                {
                    FlowBird.Cript.Lib.ParkLog.DBLogger.LogExceptionToDb(ex, data, FlowBird.Cript.Lib.ParkLog.DBLogger.SERIALIZATION_EXCEPTION_MESSAGE);
                    logger.LogException("Errore nel webMethod CreateDes 2", ex, EnLogSeverity.Error);
                }
                // --------------------------------------------------------------------------------------------------------------------

                if (us != null)
                {
                    // ---- ----------------------------------------------------------------------------------------------
                    // ' Dim dtoNew As UnencryptedPayload = FlowBird.Cript.Lib.PayloadDecrypter.DecryptStringJJL(data, Global_asax.KeyDes)
                    // In reltà ho già decriptato, quindi potrei deserializzare direttamente:
                    UnencryptedPayload translogNew = FlowBird.Cript.Lib.PayloadDecrypter.FromUnencryptedJson(us);
                    // ---- ----------------------------------------------------------------------------------------------

                    translog trabslogOld = translogNew.ToTransLog();
                    translog v_ticket = SaveTicket(trabslogOld);

                    if (v_ticket != null)
                    {
                        try
                        {
                            //nel caso che si tratti di una ricarica abbonamento
                            if (!string.IsNullOrEmpty(v_ticket.tlLicenseNo) && v_ticket.tlLicenseNo.Length == 12 &&
                                v_ticket.tlPayType.HasValue && v_ticket.tlPayType.Value > 0 &&
                                v_ticket.tlAmount.HasValue && v_ticket.tlAmount.Value > 0)
                            {
                                try
                                {
                                    AbbonamentiRinnoviBD.rinnovaAbbonamentoFromWebService(v_ticket.tlRecordID, Global.DbInfo);
                                }
                                catch (Exception e)
                                {
                                    logger.LogException("Impossibile elaborare la ricarica", e, EnLogSeverity.Error);
                                    FlowBird.Cript.Lib.ParkLog.DBLogger.LogExceptionToDb(e, data, FlowBird.Cript.Lib.ParkLog.DBLogger.EXCEPTION_MESSAGE);
                                }
                            }
                        }
                        catch (Exception exNoSaveReturns0)
                        {
                            FlowBird.Cript.Lib.ParkLog.DBLogger.LogExceptionToDb(exNoSaveReturns0, data, FlowBird.Cript.Lib.ParkLog.DBLogger.EXCEPTION_MESSAGE);
                            logger.LogException("Impossibile salvare errore 'SaveTicket Returned 0'", exNoSaveReturns0, EnLogSeverity.Error);
                        }
                    }
                    else
                    {
                        FlowBird.Cript.Lib.ParkLog.DBLogger.LogToDb("SaveTicket Returned 0", data, FlowBird.Cript.Lib.ParkLog.DBLogger.SAVE_TICKET_ERROR);
                    }
                }
            }
            // ----------------------------------------------------------------------------------------------------------------------------
            catch (Exception ex)
            {
                logger.LogException("Impossibile salvare errore 'SaveTicket Returned 0'", ex, EnLogSeverity.Error);

                // Provo a salvare eccezione anche in DB...
                try
                {
                    FlowBird.Cript.Lib.ParkLog.DBLogger.LogExceptionToDb(ex, data, FlowBird.Cript.Lib.ParkLog.DBLogger.EXCEPTION_MESSAGE);
                }
                catch (Exception exNoLogInDB)
                {
                    logger.LogException("Errore nel webMethod CreateDes 3", exNoLogInDB, EnLogSeverity.Error);
                }
            }

            return risp;
        }

    }
}
