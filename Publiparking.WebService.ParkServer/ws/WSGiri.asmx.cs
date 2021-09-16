using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Publiparking.Data.POCOLight;
using Publiparking.Data.LinqExtended;
using Publiparking.Data.BD;
using Publisoftware.Utility.Log;
using Publiparking.Data;
using Publiparking.Data.LinqExtended;
using Publiparking.Data.dto;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSGiri
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    
    [System.Web.Script.Services.ScriptService]
    public class WSGiri : System.Web.Services.WebService
    {
        protected ILogger logger;

        private List<GiroLightDTO> loadList(Int32 pIdOperatore, Int32 pNumDay)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSGiri));
            List<Giri_light> lista = new List<Giri_light>();
            IQueryable<int> vListIdGiri = null;
            IQueryable<GiriOperatore> vCadenza = null;
            
            logger.LogMessage("WSGiri loadlist", EnLogSeverity.Debug);
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                //CadenzaBD.loadByIdOperatore(pIdOperatore);
                vCadenza = GiriOperatoreBD.getListByIdOperatore(pIdOperatore, v_context);  

                switch (pNumDay)
                {
                    case 0:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.DOMENICA).Select(c=> c.idGiro);
                            break;
                        }

                    case 1:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.LUNEDI).Select(c => c.idGiro);
                            break;
                        }

                    case 2:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.MARTEDI).Select(c => c.idGiro); ;
                            break;
                        }

                    case 3:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.MERCOLEDI).Select(c => c.idGiro);
                            break;
                        }

                    case 4:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.GIOVEDI).Select(c => c.idGiro);
                            break;
                        }

                    case 5:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.VENERDI).Select(c => c.idGiro);
                            break;
                        }

                    case 6:
                        {
                            vListIdGiri = vCadenza.Where(c => c.giorno == GiriOperatore.SABATO).Select(c => c.idGiro);
                            break;
                        }
                }

                foreach (Int32 vId in vListIdGiri)
                {
                    Giri v_giro = GiriBD.GetById(vId, v_context);
                    
                    lista.Add(v_giro.ToGiroLight());
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSGiri:loadlist", ex, EnLogSeverity.Error);
            }

            return lista.Select(d => new GiroLightDTO
            {
                id = d.id,
                descrizione = d.descrizione,
                ultimaModifica = d.ultimaModifica
            }).ToList();
        }
        
        [WebMethod]
        public List<GiroLightDTO> loadList(Int32 pIdOperatore)
        {
            return loadList(pIdOperatore, (int)DateTime.Now.DayOfWeek).ToList();
        }

        [WebMethod]
        public GiroLightDTO loadByDescrizione(string pDescrizione)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSGiri));
            GiroLightDTO risp = null;
            Giri vGiro = null;
            logger.LogMessage("WSGiri loadByDescrizione", EnLogSeverity.Debug);
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
               
                vGiro = GiriBD.getGiroByDescrisione(pDescrizione,v_context);
                if (vGiro != null)
                {
                    risp = vGiro.ToGiroLightDTO();
                }
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSGiri:loadByDescrizione", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public DateTime getDataUltimaModifica(Int32 pIDGiro)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSGiri));
            DateTime risp = default(DateTime);
            
            logger.LogMessage("WSGiri getDataUltimaModifica", EnLogSeverity.Debug);
            
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = GiriBD.getDataUltimaModifica(pIDGiro,v_context);
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSGiri:getDataUltimaModifica", ex, EnLogSeverity.Error);
            }

            return risp;
        }

        [WebMethod]
        public bool isValid(Int32 pIDGiro, Int32 pIdOperatore)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSGiri));
            bool risp = false;

            logger.LogMessage("WSGiri isValid", EnLogSeverity.Debug);



            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                List<GiroLightDTO> lista = loadList(pIdOperatore, (int)DateTime.Now.DayOfWeek);
                risp = lista.Contains(GiriBD.GetById(pIDGiro,v_context).ToGiroLightDTO());
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSGiri:isValid", ex, EnLogSeverity.Error);
            }

            return risp;
        }
    }
}
