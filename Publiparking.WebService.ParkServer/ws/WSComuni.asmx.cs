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
using Publiparking.Data;
using Publiparking.Data.dto;

namespace Publiparking.WebService.ParkServer.ws
{
    /// <summary>
    /// Descrizione di riepilogo per WSComuni
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WSComuni : System.Web.Services.WebService
    {
        protected ILogger logger;
        [WebMethod]
        public List<ComuneDTO>  loadList()
        {
            logger = Publisoftware.Utility.Log.NLogger.CreateCurrentClassLogger(typeof(WSComuni));           
            List<ComuneDTO> v_comuniListDTO = new List<ComuneDTO>();
            logger.LogMessage("WSComuni loadlist", EnLogSeverity.Debug);
            try
            {
                dbEnte v_context = Global.DbInfo.GetEnteCtx();
                v_comuniListDTO = SerComuniBD.GetList(v_context).ToList()
                    .Select(d => new ComuneDTO
                    {
                        id = d.id,
                        descrizione = d.descrizione
                    }).ToList();
                Global.DbInfo.DisposeEntectx(v_context);
            }
            catch (Exception ex)
            {
                logger.LogException("Errore nel webMethod WSComuni:loadlist", ex, EnLogSeverity.Error);                
            }

            return v_comuniListDTO;
        }
    }
}
