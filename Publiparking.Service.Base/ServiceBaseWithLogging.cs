using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Service.Base
{
    public partial class ServiceBaseWithLogging : System.ServiceProcess.ServiceBase
    {
        /// <summary>
        /// Istanza di logger da utilizzare
        /// </summary>
        protected ILogger logger;

        #region "COSTRUTTORE - INSTANCE INIT"
        public ServiceBaseWithLogging()
        {
            AppDomain.CurrentDomain.UnhandledException += Base_CurrentDomain_UnhandledException;
            logger = LoggerFactory.getInstance().getLogger<NLogger>(this);
        }

        public ServiceBaseWithLogging(ILogger loggerToUse)
        {
            AppDomain.CurrentDomain.UnhandledException += Base_CurrentDomain_UnhandledException;
            logger = loggerToUse;
        }

        //Handler generale: eccezioni non gestite
        protected void Base_CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.logger.LogException("Errore non gestito: ", e.ExceptionObject as Exception, EnLogSeverity.Fatal);
        }
        #endregion

        #region "START"
        protected override sealed void OnStart(string[] args)
        {
            try
            {
                logger.LogMessage("Servizio in fase di avvio...", EnLogSeverity.Info);
                OnStartTask(args);
                logger.LogMessage("onStart eseguito con successo", EnLogSeverity.Debug);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nell'avvio del servizio: ", ex, EnLogSeverity.Fatal);

                ExitCode = 1064;
                throw;
            }
        }

        /// <summary>
        /// Codice da eseguire OnStart
        /// </summary>
        /// <param name="args"></param>
        public virtual void OnStartTask(string[] args)
        {
            throw new Exception("Metodo non implementato");
        }
        #endregion

        #region "STOP"
        protected override sealed void OnStop()
        {
            try
            {
                logger.LogMessage("Servizio in fase d'arresto...", EnLogSeverity.Debug);
                OnStopTask();
                logger.LogMessage("Servizio arrestato con successo.", EnLogSeverity.Info);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nello stop del servizio: ", ex, EnLogSeverity.Fatal);

                throw;
            }
        }

        /// <summary>
        /// Codice da eseguire OnStop
        /// </summary>
        public void OnStopTask() { }
        #endregion

        #region "CONTINUE"
        protected override sealed void OnContinue()
        {
            try
            {
                logger.LogMessage("Servizio in fase di ripristino...", EnLogSeverity.Debug);
                OnContinueTask();
                logger.LogMessage("Servizio rispristinato.", EnLogSeverity.Info);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nella ripresa del servizio: ", ex, EnLogSeverity.Fatal);

                throw;
            }
        }

        public void OnContinueTask() { }
        #endregion

        #region "PAUSE"
        protected override sealed void OnPause()
        {
            try
            {
                logger.LogMessage("Servizio in pausa...", EnLogSeverity.Debug);
                OnpauseTask();
                logger.LogMessage("Servizio messo in pausa.", EnLogSeverity.Info);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nella messa in pausa del servizio: ", ex, EnLogSeverity.Fatal);

                throw;
            }
        }

        public void OnpauseTask() { }
        #endregion
    }
}
