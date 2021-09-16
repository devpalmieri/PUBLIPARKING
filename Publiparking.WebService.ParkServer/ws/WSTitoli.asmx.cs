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
    /// Descrizione di riepilogo per WSTitoli
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSTitoli : System.Web.Services.WebService
    {
        protected ILogger logger;

        [WebMethod]
        public TariffaLightDTO getTariffaById(object p_idTariffa)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli getTariffaById", EnLogSeverity.Debug);
            TariffaLightDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Tariffe v_tariffa  = TariffeBD.GetById((Int32)p_idTariffa, v_context); 

                if (v_tariffa != null)
                    risp = v_tariffa.toTariffaLightDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:getTariffaById", ex, EnLogSeverity.Error);
            }

            return risp;
        }
        [WebMethod]
        public List<TariffaLightDTO> getListTariffe()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli getListTariffe", EnLogSeverity.Debug);
            List<TariffaLightDTO> risp = new List<TariffaLightDTO>();

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                IQueryable<Tariffe> v_tariffeLst = TariffeBD.GetList(v_context);
                if (v_tariffeLst.Any())
                {
                    risp = v_tariffeLst.ToDTO().ToList();                 
                }
                Global.DbInfo.DisposeParkctx(v_context);

            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:getListTariffe", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isValid(string pCodice)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli isValid", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_minutiTolleranzaVerbale = ConfigurationManager.AppSettings["minutiTolleranzaVerbale"].ToString();
                
                if (TitoloBD.existByCodice(pCodice, v_context))
                    risp = !TitoloBD.isExpired(pCodice, Int32.Parse(v_minutiTolleranzaVerbale), v_context);

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:isValid", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool titoliSovrapposti(Int32 p_IdStallo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli titoliSovrapposti", EnLogSeverity.Debug);
            bool risp = false;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                TitoloDTO v_titolo = TitoloBD.getUltimoPagatoByIdStallo(p_IdStallo, v_context);
                if (v_titolo != null)
                    risp = TitoloBD.titoliSovrapposti(v_titolo, v_context);

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:titoliSovrapposti", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public TitoloOperatoreDTO getLastByOperatore(Int32 p_IdOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli getLastByOperatore", EnLogSeverity.Debug);
            TitoloOperatoreDTO risp = null;                        
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                TitoliOperatori v_titoloOperatori = TitoliOperatoriBD.getLastTitoloOperatoreByIdOperatore(p_IdOperatore, v_context);
                if (v_titoloOperatori != null)
                    risp = v_titoloOperatori.toTitoloOperatoreDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:getLastByOperatore", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public List<TitoloOperatoreDTO> getListByOperatore(Int32 p_IdOperatore, Int32 p_numGiorni)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli getListByOperatore", EnLogSeverity.Debug);
            List<TitoloOperatoreDTO> risp = new List<TitoloOperatoreDTO>();
           
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                IQueryable<TitoliOperatori> v_titoliList = TitoliOperatoriBD.getListByIdOperatore(p_IdOperatore, p_numGiorni, v_context);

                if (v_titoliList.Any())
                {
                    risp = v_titoliList.ToDTO().ToList();
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:getListByOperatore", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public TitoloOperatoreDTO getPreventivoByTariffaMinuti(Int32 p_IdOperatore, Int32 p_idTariffa, Int32 p_minuti)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli getPreventivoByTariffaMinuti", EnLogSeverity.Debug);
            TitoloOperatoreDTO risp = null;
            
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Tariffe v_tariffa = TariffeBD.GetById(p_idTariffa, v_context);
                TitoliOperatori v_titolo = TitoliOperatoriBD.calcolaByTariffa(v_tariffa, p_minuti, DateTime.Now, v_context);

                if (v_titolo != null)
                {
                    v_titolo.idOperatore = p_IdOperatore;                   
                    risp = v_titolo.toTitoloOperatoreDTO();
                }

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:getPreventivoByTariffaMinuti", ex, EnLogSeverity.Error);
            }
            return risp;
        }
        [WebMethod]
        public TitoloOperatoreDTO getPreventivoByStalloMinuti(Int32 p_IdOperatore, Int32 p_idStallo, Int32 p_minuti)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli getPreventivoByStalloMinuti", EnLogSeverity.Debug);
            TitoloOperatoreDTO risp = null;       
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                Stalli v_stalli = StalliBD.GetById(p_idStallo, v_context);
                Int32 idTariffa = v_stalli.idTariffa.HasValue ? v_stalli.idTariffa.Value : 0;
                Tariffe v_tariffa = TariffeBD.GetById(idTariffa, v_context);
                TitoliOperatori v_titolo = TitoliOperatoriBD.calcolaByTariffa(v_tariffa, p_minuti, DateTime.Now, v_context);

                if (v_titolo != null)
                {
                    v_titolo.idStallo = p_idStallo;
                    v_titolo.idOperatore = p_IdOperatore;
                    risp = v_titolo.toTitoloOperatoreDTO();
                }

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:getPreventivoByStalloMinuti", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public TitoloOperatoreDTO createTitolo(TitoloOperatoreDTO p_titolo)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSTitoli));
            logger.LogMessage("WSTitoli createTitolo", EnLogSeverity.Debug);
            TitoloOperatoreDTO risp = null;

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                string v_idParcometroOperatori = ConfigurationManager.AppSettings["idParcometroOperatori"].ToString();
                TitoliOperatori v_titolo = TitoliOperatoriBD.salvaTitoloPreventivo(p_titolo, v_idParcometroOperatori,v_context);                
                if (v_titolo != null)
                    risp = v_titolo.toTitoloOperatoreDTO();

                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSTitoli:createTitolo", ex, EnLogSeverity.Error);
            }

            return risp;
        }

    }
}
