using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinTabCarrelloTabRateBD : EntityBD<join_tab_carrello_tab_rate>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabCarrelloBD");
        #endregion Private Members
        #region Costructor
        public JoinTabCarrelloTabRateBD()
        {

        }
        #endregion Costructor
        #region Public Methods
        // <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new join_tab_carrello_tab_rate GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_join_carrello_rate == p_id);
        }
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static join_tab_carrello_tab_rate GetByIUV(string p_iuv, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.Iuv_identificativo_pagamento == p_iuv && d.cod_stato == anagrafica_stato_carrello.ATT_PGT).FirstOrDefault();
        }
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static List<join_tab_carrello_tab_rate> GetByIUVAndStato(string p_iuv, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.Iuv_identificativo_pagamento == p_iuv && (d.cod_stato == anagrafica_stato_carrello.ATT_PGT || d.cod_stato == anagrafica_stato_carrello.ATT_RPT)).ToList();
        }
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static List<join_tab_carrello_tab_rate> GetByIUVAndStatoAndIdCarrello(string p_iuv, int p_idcarrello, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.Iuv_identificativo_pagamento == p_iuv && (d.cod_stato == anagrafica_stato_carrello.ATT_PGT || d.cod_stato == anagrafica_stato_carrello.ATT_RPT) && d.id_carrello == p_idcarrello).ToList();
        }
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static async Task<join_tab_carrello_tab_rate> GetByIdRataAsync(int p_idrata, dbEnte p_dbContext)
        {
            //return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.id_rata_avv_pag == p_idrata && (d.cod_stato == anagrafica_stato_carrello.ATT_PGT || d.cod_stato == anagrafica_stato_carrello.ATT_REN)).FirstOrDefault();
            return await GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.id_rata_avv_pag == p_idrata).FirstOrDefaultAsync();

        }
        public static join_tab_carrello_tab_rate GetByIdRata(int p_idrata, dbEnte p_dbContext)
        {
            //return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.id_rata_avv_pag == p_idrata && (d.cod_stato == anagrafica_stato_carrello.ATT_PGT || d.cod_stato == anagrafica_stato_carrello.ATT_REN)).FirstOrDefault();
            return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.id_rata_avv_pag == p_idrata).FirstOrDefault();

        }
        public static join_tab_carrello_tab_rate GetByIdRataAndStatoAndIdCarrello(int p_idrata, string p_cod_stato, int p_id_carrello, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.id_rata_avv_pag == p_idrata && d.cod_stato == p_cod_stato && d.id_carrello == p_id_carrello).FirstOrDefault();
        }
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static IQueryable<join_tab_carrello_tab_rate> GetRTInsolute(DateTime p_date, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.cod_stato == anagrafica_stato_carrello.ATT_PGT && d.data_stato <= p_date);
        }
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static IQueryable<join_tab_carrello_tab_rate> GetPagamentiByCarrello(int p_id_carrello, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_carrello == p_id_carrello);
        }
        #endregion Public Methods
    }
}
