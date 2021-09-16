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
using Publiparking.Data.dto;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSMarcheModelli
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSMarcheModelli : System.Web.Services.WebService
    {
        protected ILogger logger;
        [WebMethod]
        public List<MarcaLightDTO> loadListMarche()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSMarcheModelli));
            List<MarcaLightDTO> v_marcheList = new List<MarcaLightDTO>();
            logger.LogMessage("WSMarcheModelli loadListMarche", EnLogSeverity.Debug);
            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                v_marcheList = MarcheBD.GetList(v_context).ToLightDTO().OrderBy(o => o.descrizione).ToList();
                Global.DbInfo.DisposeParkctx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSMarcheModelli loadListMarche", ex, EnLogSeverity.Error);
            }

            return v_marcheList;
        }
        [WebMethod]
        public List<string> loadListModelliByIDMarca(Int32 pIDMarca)
        {            
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSMarcheModelli));
            List<string> risp = new List<string>();
            logger.LogMessage("WSMarcheModelli loadListModelliByIDMarca", EnLogSeverity.Debug);

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = ModelliBD.GetList(v_context).Where(m => m.idMarca == pIDMarca).OrderBy(o => o.descrizione)
                       .Select(m => m.descrizione).ToList();

                Global.DbInfo.DisposeParkctx(v_context);

            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSMarcheModelli loadListModelliByIDMarcab=> ", ex, EnLogSeverity.Error);
            }
            return risp;
        }

        [WebMethod]
        public List<string> loadListModelliByMarcaDesc(string pDescrizioneMarca)
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSMarcheModelli));
            List<string> risp = new List<string>();
            logger.LogMessage("WSMarcheModelli loadListModelliByMarcaDesc", EnLogSeverity.Debug);

            try
            {
                DbParkCtx v_context = Global.DbInfo.GetParkCtx();
                risp = ModelliBD.GetList(v_context)
                       .Where(m=> m.Marche.descrizione.Equals(pDescrizioneMarca))
                       .OrderBy(m=> m.descrizione)
                       .Select(m => m.descrizione).ToList();

                Global.DbInfo.DisposeParkctx(v_context);

            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSMarcheModelli loadListModelliByMarcaDesc", ex, EnLogSeverity.Error);
            }
            return risp;
        }


    }
}
