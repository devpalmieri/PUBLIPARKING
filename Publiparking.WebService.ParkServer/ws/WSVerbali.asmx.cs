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

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSVerbali
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSVerbali : System.Web.Services.WebService
    {
        protected ILogger logger;

        [WebMethod]
        public List<TipoVerbaleLightDTO> loadList()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadlist", EnLogSeverity.Debug);
            List<TipoVerbaleLightDTO> risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                IQueryable<TipiVerbale> tvList = TipiVerbaleBD.GetList(v_context);
                if (tvList.Any())
                {
                    risp = tvList.ToLigthDTO().ToList();
                }             
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadlist", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public TipoVerbaleDTO loadByID(Int32 pIDTipoVerbale)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadByID", EnLogSeverity.Debug);
            TipoVerbaleDTO risp = null;
            
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                TipiVerbale v_tv = TipiVerbaleBD.GetById(pIDTipoVerbale, v_context);
                if (v_tv != null)
                {
                    risp = v_tv.toTipoVerbaleDTO();
                }
                
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadByID", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public VerbaleDTO loadVerbaleByCodBollettino(Int32 pCodBollettino, string pSerie)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadVerbaleByCodBollettino", EnLogSeverity.Debug);
            VerbaleDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_serieDefault = ConfigurationManager.AppSettings["primaSerie"].ToString();

                Verbali v_verbale = VerbaliBD.loadByCodiceBollettino(pCodBollettino.ToString(), v_serieDefault, pSerie, v_context);
                if (v_verbale != null)
                {
                    risp = v_verbale.toVerbaleDTO();
                }                 
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadVerbaleByCodBollettino", ex, EnLogSeverity.Error);
            }

            return risp;
        }



        [WebMethod]
        public VerbaleDTO loadLast(Int32 pIDOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadLast", EnLogSeverity.Debug);          
            VerbaleDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_serieDefault = ConfigurationManager.AppSettings["primaSerie"].ToString();

                Verbali v_verbale = VerbaliBD.getLastVerbaleByIdOperatoreAndSerie(pIDOperatore, v_serieDefault, v_context);
                if (v_verbale != null)
                {
                    risp = v_verbale.toVerbaleDTO();
                }
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadLast", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public List<CausaleDTO> loadListCausali()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadListCausali", EnLogSeverity.Debug);
            List<CausaleDTO> risp = new List<CausaleDTO>();
            
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                IQueryable<Causali> v_lista = CausaliBD.getListAttive(v_context);
                if (v_lista.Any())
                {
                    risp = v_lista.ToDTO().ToList();
                }
                
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadListCausali", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        [WebMethod]
        public List<Int32> loadListCausaliByDescTipoVerbale(string pDescTipoVerbale)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadListCausali", EnLogSeverity.Debug);            
            List<Int32> risp = new List<int>();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                IQueryable<Causali> v_lista = CausaliBD.getListAttiveByDescrTipoVerbale(pDescTipoVerbale, v_context);                                                 
                if (v_lista.Any())
                    risp = v_lista.Select(l=> l.idCausale).ToList();
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadListCausaliByDescTipoVerbale", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public CausaleDTO loadCausaleByID(Int32 pIDCausale)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadCausaleByID", EnLogSeverity.Debug);
            CausaleDTO risp = null;
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Causali v_causale = CausaliBD.GetById(pIDCausale,v_context);
                if (v_causale != null)
                {
                    risp = v_causale.ToCausaleDTO();
                }
                
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadCausaleByID", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        [WebMethod]
        public bool isValid(string pCodiceBollettino, Int32 pIDStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali isValid", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                if (pCodiceBollettino.Length == 12)
                {
                    risp = PenaliBD.isValid(pCodiceBollettino, pIDStallo, v_context);
                }
                else
                {
                    string v_serie = ConfigurationManager.AppSettings["primaSerie"].ToString();
                    risp = VerbaliBD.isValid(pCodiceBollettino, pIDStallo, v_serie, v_serie, v_context);
                }               
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:isValid", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isValidCodiceBollettino(Int32 pCodiceBollettino, Int32 p_idOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali isValidCodiceBollettino --> " + pCodiceBollettino, EnLogSeverity.Debug);
            bool risp = false;
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_serie = ConfigurationManager.AppSettings["primaSerie"].ToString();
                risp = BollettiniBD.isValidCodiceBollettino(pCodiceBollettino, p_idOperatore, v_serie,v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:isValidCodiceBollettino", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isVerbalizzazioneAttiva(string pIdTerminale)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali isVerbalizzazioneAttiva", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = TerminaliBD.verbalizzazioneAttiva(int.Parse(pIdTerminale), v_context);                
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:isVerbalizzazioneAttiva", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public string getModalitaPagamento()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali getModalitaPagamento", EnLogSeverity.Debug);
            string risp = string.Empty;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();
                
                risp = v_configurazione.modalitaPagamento;
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:getModalitaPagamento", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public VerbaleTemplateDTO loadTemplateByNome(string pNome)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali loadTemplateByNome nome: " + pNome, EnLogSeverity.Debug);
            VerbaleTemplateDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                VerbaliTemplate v_template = VerbaliTemplateBD.GetVerbaleTemplateByNome(pNome, v_context);
                if (v_template != null)
                {
                    risp = v_template.toVerbaleTemplateDTO();
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:loadTemplateByNome", ex, EnLogSeverity.Error);
            }

            logger.LogMessage("WSVerbali loadTemplateByNome nome: " + risp.nome, EnLogSeverity.Debug);

            return risp;
        }

        [WebMethod]
        public TipoVerbaleDTO getTipoDefault()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali getTipoDefault", EnLogSeverity.Debug);
            TipoVerbaleDTO risp = null;
            
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                TipiVerbale v_tipoverbale = TipiVerbaleBD.getDefault(v_context);
                if (v_tipoverbale != null)
                {
                    risp = v_tipoverbale.toTipoVerbaleDTO();
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:getTipoDefault", ex, EnLogSeverity.Error);
            }

            return risp;
        }


        [WebMethod]
        public TipoVerbaleDTO getTipoScaduto()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali getTipoScaduto", EnLogSeverity.Debug);
            TipoVerbaleDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();
                TipiVerbale v_tipoverbale = TipiVerbaleBD.GetById(v_configurazione.idTipoVerbaleSostaScaduta, v_context);
                if (v_tipoverbale != null)
                {
                    risp = v_tipoverbale.toTipoVerbaleDTO();
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:getTipoScaduto", ex, EnLogSeverity.Error);
            }

            return risp;
        }


        [WebMethod]
        public decimal getPercentualeRiduzioneVerbale()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSVerbali));
            logger.LogMessage("WSVerbali getPercentualeRiduzioneVerbale", EnLogSeverity.Debug);
            decimal risp = 0;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Configurazione v_configurazione = ConfigurazioneBD.GetList(v_context).FirstOrDefault();

                risp = v_configurazione.percentualeRiduzioneVerbale;
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSVerbali:getPercentualeRiduzioneVerbale", ex, EnLogSeverity.Error);
            }

            return risp;
        }
    }
}
