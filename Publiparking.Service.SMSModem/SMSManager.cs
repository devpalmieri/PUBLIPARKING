using Publiparking.Data;
using Publiparking.Data.BD;
using Publiparking.Service.Base;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Service.SMSModem
{
    public partial class SMSManager : ServiceBaseWithLogging
    {
        List<DBInfos> v_Entilist = new List<DBInfos>();
        //Non fa propagare le eccezioni => Alternativa System.Threading
        System.Timers.Timer m_timer;

        static void Main()
        {
            if (System.Environment.UserInteractive)
            {
                //utilizzato per il debug
                SMSManager v_service = new SMSManager();
                v_service.TestStartupAndStop();
            }
            else
            {
                System.ServiceProcess.ServiceBase.Run(new SMSManager());
            }

        }
        public SMSManager()
        {
            this.ServiceName = "SMSModem";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;


        }

        public override void OnStartTask(string[] args)
        {
            m_timer = new System.Timers.Timer();
            m_timer.Interval = Int32.Parse(ConfigurationManager.AppSettings["minIntervalloEsecuzione"]) * 60000; // 60 seconds
            m_timer.Elapsed += (sender, e) =>
            {
                try
                {
                    OnTimer(sender, e);
                }
                catch (Exception ex)
                {
                    this.logger.LogException("Errore nell'esecuzione del servizio: ", ex as Exception, EnLogSeverity.Fatal);
                    throw;
                }
            };
            m_timer.AutoReset = false;
            m_timer.Start();
            this.logger.LogMessage(String.Format("Timer istanziato [Intervallo:{0} ms]", m_timer.Interval), EnLogSeverity.Debug);
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            logger.LogMessage(String.Format("Avvio del servizio SMS in corso ..."), EnLogSeverity.Info);

            //Carica la lista dei database solo la prima volta. Per ricaricare la lista, fermare e riavviare
            if (v_Entilist.Count() == 0)
            {
                string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
                string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
                string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
                string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();


                string v_smsUserName = ConfigurationManager.AppSettings["smsUserName"].ToString();
                string v_smsPassword = ConfigurationManager.AppSettings["smsPassword"].ToString();
                string v_mittenteNotificheDaWS = ConfigurationManager.AppSettings["mittenteNotificheDaWS"].ToString();
                string v_numeroNotifiche = ConfigurationManager.AppSettings["numeroNotifiche"].ToString();


                Telecom.SMSSoapClient v_smsSend = new Telecom.SMSSoapClient();
                Telecom.EsitiInviaSMSList risp = v_smsSend.InviaSMS(v_smsUserName, v_smsPassword, 0, v_mittenteNotificheDaWS,
                                                                "Servizio Avviato in modalità WS", v_numeroNotifiche, false);
                if (risp.error_code == 0)
                {
                    logger.LogMessage(String.Format("Servizio Avviato in modalità WS"), EnLogSeverity.Info);
                }
                else
                {
                    logger.LogMessage(String.Format("Errore nell'avvio del servizio in modalità WS"), EnLogSeverity.Error);
                    return;
                }

                AdoHelper v_helper = new AdoHelper();
                try
                {


                    v_Entilist = v_helper.getDbSettorialiParkInfo(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);
                }
                catch (Exception exception)
                {
                    logger.LogException("Errore nella lettura della lista degli enti", exception, EnLogSeverity.Error);
                }
            }

            logger.LogMessage(String.Format("numero enti da elaborare {0}", v_Entilist.Count()), EnLogSeverity.Info);
            DbParkCtx ctx = null;
            foreach (DBInfos dbInfo in v_Entilist)
            {
                try
                {
                    logger.LogMessage(String.Format("DBNAME ---> {0}", dbInfo.DbName), EnLogSeverity.Info);
                    ctx = dbInfo.GetParkCtx(false);
                    if (ctx == null)
                    {
                        logger.LogMessage("impossibile connettersi al DB", EnLogSeverity.Error);
                    }
                    else
                    {
                        logger.LogMessage(String.Format("Servizio Avviato in modalità WS"), EnLogSeverity.Info);
                        string v_numSmsModemTelecomm = ConfigurazioneBD.GetList(ctx).SingleOrDefault().numSmsModemTelecom;
                        string v_userSmsModemTelecom = ConfigurazioneBD.GetList(ctx).SingleOrDefault().userSmsModemTelecom;
                        string v_pwdSmsModemTelecom = ConfigurazioneBD.GetList(ctx).SingleOrDefault().pwdSmsModemTelecom;
                        int v_minutiScadenzaMessaggioOut = Int32.Parse(ConfigurationManager.AppSettings["minutiScadenzaMessaggioOut"].ToString());
                        inviaSMSOut(v_minutiScadenzaMessaggioOut, v_numSmsModemTelecomm, v_userSmsModemTelecom, v_pwdSmsModemTelecom, ctx);
                        ctx.SaveChanges();

                    }
                }
                catch (Exception exc)
                {
                    logger.LogException("Errore nell'invio sms", exc, EnLogSeverity.Error);
                    ctx.Dispose();
                }
                finally
                {
                    ctx.Dispose();
                }
            }

            logger.LogMessage(String.Format("Fine del servizio SMS in corso ..."), EnLogSeverity.Info);
            m_timer.Start();
        }

        #region PRIVATE METHOD

        internal void TestStartupAndStop()
        {

            logger.LogMessage(String.Format("Avvio del servizio SMS in corso ..."), EnLogSeverity.Info);

            //Carica la lista dei database solo la prima volta. Per ricaricare la lista, fermare e riavviare
            if (v_Entilist.Count() == 0)
            {
                string v_dbServer = ConfigurationManager.AppSettings["dbServer"].ToString();
                string v_dbName = ConfigurationManager.AppSettings["dbName"].ToString();
                string v_dbUserName = ConfigurationManager.AppSettings["dbUserName"].ToString();
                string v_dbPassWord = ConfigurationManager.AppSettings["dbPassWord"].ToString();


                string v_smsUserName = ConfigurationManager.AppSettings["smsUserName"].ToString();
                string v_smsPassword = ConfigurationManager.AppSettings["smsPassword"].ToString();
                string v_mittenteNotificheDaWS = ConfigurationManager.AppSettings["mittenteNotificheDaWS"].ToString();
                string v_numeroNotifiche = ConfigurationManager.AppSettings["numeroNotifiche"].ToString();
                

                Telecom.SMSSoapClient v_smsSend = new Telecom.SMSSoapClient();
                Telecom.EsitiInviaSMSList risp = v_smsSend.InviaSMS(v_smsUserName, v_smsPassword, 0, v_mittenteNotificheDaWS,
                                                                "Servizio Avviato in modalità WS", v_numeroNotifiche, false);
                if (risp.error_code == 0)
                {
                    logger.LogMessage(String.Format("Servizio Avviato in modalità WS"), EnLogSeverity.Info);
                }
                else
                {
                    logger.LogMessage(String.Format("Errore nell'avvio del servizio in modalità WS"), EnLogSeverity.Error);
                    return;
                }

                AdoHelper v_helper = new AdoHelper();
                try
                {

                  
                    v_Entilist = v_helper.getDbSettorialiParkInfo(v_dbServer, v_dbName, v_dbUserName, v_dbPassWord);
                }
                catch (Exception exception)
                {
                    logger.LogException("Errore nella lettura della lista degli enti", exception, EnLogSeverity.Error);
                }
            }
            
            logger.LogMessage(String.Format("numero enti da elaborare {0}", v_Entilist.Count()), EnLogSeverity.Info);
            DbParkCtx ctx = null;
            foreach (DBInfos dbInfo in v_Entilist)
            {
                try
                {
                    logger.LogMessage(String.Format("DBNAME ---> {0}", dbInfo.DbName), EnLogSeverity.Info);
                    ctx = dbInfo.GetParkCtx();
                    if (ctx == null)
                    {
                        logger.LogMessage("impossibile connettersi al DB", EnLogSeverity.Error);
                    }
                    else
                    {                                               
                            logger.LogMessage(String.Format("Servizio Avviato in modalità WS"), EnLogSeverity.Info);
                            string v_numSmsModemTelecomm = ConfigurazioneBD.GetList(ctx).SingleOrDefault().numSmsModemTelecom;
                            string v_userSmsModemTelecom = ConfigurazioneBD.GetList(ctx).SingleOrDefault().userSmsModemTelecom;
                            string v_pwdSmsModemTelecom = ConfigurazioneBD.GetList(ctx).SingleOrDefault().pwdSmsModemTelecom;
                            int v_minutiScadenzaMessaggioOut = Int32.Parse(ConfigurationManager.AppSettings["minutiScadenzaMessaggioOut"].ToString());
                            inviaSMSOut(v_minutiScadenzaMessaggioOut, v_numSmsModemTelecomm, v_userSmsModemTelecom, v_pwdSmsModemTelecom, ctx);
                            ctx.SaveChanges();
                                                
                    }
                }
                catch (Exception exc)
                {
                    logger.LogException("Errore nell'invio sms", exc, EnLogSeverity.Error);
                    ctx.Dispose();
                }
                finally
                {
                    ctx.Dispose();
                }
            }

            logger.LogMessage(String.Format("Fine del servizio SMS in corso ..."), EnLogSeverity.Info);
            m_timer.Start();
        }


        private void inviaSMSOut(int p_minutiScadenzaMessaggioOut,string p_numSmsModemTelecomm,string p_userSmsModemTelecom,string p_pwdSmsModemTelecom, DbParkCtx v_context)
        {
            logger.LogMessage(String.Format("inviaSMSOut"), EnLogSeverity.Debug);
            IList<SMSOut> v_listaSMSOut;
            try
            {
                v_listaSMSOut = SMSOutBD.GetListSMSNonElaborati(p_minutiScadenzaMessaggioOut, v_context).ToList();
                if (!v_listaSMSOut.Any())
                {
                    logger.LogMessage("non vi sono sms da elaborare", EnLogSeverity.Debug);
                }

                foreach (SMSOut v_SMSOut in v_listaSMSOut)
                {
                    bool v_result = false;
                    logger.LogMessage(String.Format("vi sono {0} sms da elaborare",v_listaSMSOut.Count()), EnLogSeverity.Debug);
                    if (v_SMSOut.numeroDestinatario != null && v_SMSOut.numeroDestinatario.Length >= 10)
                    {
                        if (p_numSmsModemTelecomm.Length > 0 & p_userSmsModemTelecom.Length > 0 & p_pwdSmsModemTelecom.Length > 0)
                        {                            
                            Telecom.SMSSoapClient v_smsSend = new Telecom.SMSSoapClient();
                            string v_destinatario = v_SMSOut.numeroDestinatario;
                            string v_mittente = v_SMSOut.numeroMittente;

                            if (v_destinatario.Length > 10 & v_destinatario.StartsWith("39"))
                                v_destinatario = "+" + v_destinatario;

                            if (v_mittente.Length > 10 & v_mittente.StartsWith("39"))
                                v_mittente = "+" + v_mittente;
                            else if (v_mittente.Length == 10)
                                v_mittente = "+39" + v_mittente;
                            logger.LogMessage(String.Format("Invio SMS al numero {0}",v_destinatario), EnLogSeverity.Info);
                            Telecom.EsitiInviaSMSList risp = v_smsSend.InviaSMS(p_userSmsModemTelecom, p_pwdSmsModemTelecom, v_SMSOut.idSMSOut, v_mittente, v_SMSOut.testo, v_destinatario, false);

                            if (risp.error_code == 0)
                            {
                                v_result = true;
                                logger.LogMessage(String.Format("SMS inviato con successo"), EnLogSeverity.Info);
                            }
                        }
                        //Commentato l'invio con il vecchio modem non più supportato
                        //else
                        //    v_result = m_phone.SendSMS(v_SMSOut.numeroDestinatario, v_SMSOut.testo).Length > 0;

                        if (v_result)
                        {
                            v_SMSOut.dataInvio = DateTime.Now;
                            v_SMSOut.dataElaborazione = DateTime.Now;
                            v_context.SaveChanges();
                        }
                        else
                        {
                            logger.LogMessage(String.Format("Messaggio non inviato {0}", v_result), EnLogSeverity.Info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel metodo inviaSMSOut", ex, EnLogSeverity.Error);
            }
        }

        #endregion

    }
}
