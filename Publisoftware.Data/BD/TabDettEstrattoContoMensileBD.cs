using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDettEstrattoContoMensileBD : EntityBD<tab_dett_estratto_conto_mensile>
    {
        #region Costructor
        public TabDettEstrattoContoMensileBD() { }
        #endregion Costructor

        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabCarrelloBD");
        #endregion Private Members

        #region Public Methods
        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_dett_estratto_conto_mensile> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            /// Ridefinisce la GetList per implementare la sicurezza di accesso sul contribuente
            return GetListInternal(p_dbContext, p_includeEntities);
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_dett_estratto_conto_mensile GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_dett_estratto_conto_mensile == p_id);
        }
        public static tab_dett_estratto_conto_mensile GetCarrelloByNomeFlusso(string p_nomeFlusso, dbEnte p_dbContext)
        {
            logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => d.nome_flusso_pagopa == p_nomeFlusso).FirstOrDefault();
        }
        public static tab_dett_estratto_conto_mensile GetCarrelloByNomeFlussoAndStatoVerifica(string p_nomeFlusso,string p_stato, dbEnte p_dbContext)
        {
            logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);
            //FORM-2021-01-12GovPAYPsp1-000000051
            //FORM-2021-01-12GovPAYPsp1-000000051
            tab_dett_estratto_conto_mensile A =GetList(p_dbContext).Where(d => d.nome_flusso_pagopa == p_nomeFlusso && d.flag_pagopa=="1").SingleOrDefault();
            return GetList(p_dbContext).Where(d => d.nome_flusso_pagopa == p_nomeFlusso && d.stato_verifica_pagopa==p_stato && d.flag_pagopa=="1").SingleOrDefault();
        }
        #endregion Public Methods
    }
}
