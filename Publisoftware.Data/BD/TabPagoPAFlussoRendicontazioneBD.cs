using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabPagoPAFlussoRendicontazioneBD : EntityBD<tab_pagopa_flusso_rendicontazione>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabCarrelloBD");
        #endregion Private Members

        #region Costructor
        public TabPagoPAFlussoRendicontazioneBD()
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
        public static new tab_pagopa_flusso_rendicontazione GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.ID == p_id);
        }
       
        // <summary>
        /// Restituisce l'entità a partire dalla dall'IUV
        /// della rata associata
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static bool UpdateStatoFlussoRendicontazione(string p_identificativoFlusso,int p_idstato, string p_stato,bool p_esito,string p_note, dbEnte p_dbContext)
        {
            //return GetList(p_dbContext).Where(d => d.tab_rata_avv_pag.cod_stato==anagrafica_stato_carrello.ATT_PGT && d.data_stato == p_date);
            tab_pagopa_flusso_rendicontazione flussoRendicontazione = GetList(p_dbContext).FirstOrDefault(x => x.IdentificativoFlusso == p_identificativoFlusso && x.Cod_Stato==anagrafica_stato_carrello.ATT_INS);
            if (flussoRendicontazione == null)
                return false;

            if (p_esito)
            {
                flussoRendicontazione.Id_Stato = p_idstato;
                flussoRendicontazione.Cod_Stato = p_stato;
            }
            flussoRendicontazione.DataVerifica = DateTime.Now;
            flussoRendicontazione.EsitoVerifica = p_esito;
            flussoRendicontazione.Note = p_note;

            p_dbContext.SaveChanges();

            return true;

        }
        #endregion Public Methods
    }
}
