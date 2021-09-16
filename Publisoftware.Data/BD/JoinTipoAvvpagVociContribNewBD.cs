using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using Publisoftware.Utility.Log;

namespace Publisoftware.Data.BD
{
    public class JoinTipoAvvpagVociContribNewBD : EntityBD<join_tipo_avvpag_voci_contrib_new>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("JoinTipoAvvpagVociContribNewBD");

        public JoinTipoAvvpagVociContribNewBD()
        {

        }
        /// <summary>
        /// Filtro per l'id del tipo avviso, l'id dell'ente e l'id della voce di contribuzione
        /// </summary>
        /// <param name="p_idAnagVocecontrib"></param>
        /// <param name="p_idTipoAvvPag"></param>
        /// <param name="p_idEnte"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static join_tipo_avvpag_voci_contrib_new GetByJoinValues(int p_idAnagVocecontrib, int? p_idTipoAvvPag, int p_idEnte, dbEnte dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(dbContext).SingleOrDefault(j => j.id_anagrafica_voce_contribuzione == p_idAnagVocecontrib && j.id_tipo_avv_pag == p_idTipoAvvPag && j.id_ente == p_idEnte);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_idTipoAvvPag">ID Tipo Avviso Pagamento</param>
        /// /// <param name="p_idTipoVoce">ID Responsabile ricercato</param>
        /// /// <param name="p_codiceTributoMinisteriale">Codice tributo ministeriale</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static join_tipo_avvpag_voci_contrib_new GetJoinByIdTipoAvvPagAndIdTipoVoce(int p_idTipoAvvPag, int p_idTipoVoceContrib, int p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, dbEnte p_dbContext)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            var v_joinList = GetList(p_dbContext).WhereByIdTipoAvvPag(p_idTipoAvvPag)
            /*.Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))*/
            .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
            .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
            .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT);

            if (v_joinList.Count() == 0 || v_joinList.Count() > 1) { return null; } // errore

            join_tipo_avvpag_voci_contrib_new v_join = v_joinList.FirstOrDefault();

            return v_join;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_idTipoAvvPag">ID Tipo Avviso Pagamento</param>
        /// /// <param name="p_idTipoVoce">ID Responsabile ricercato</param>
        /// /// <param name="p_codiceTributoMinisteriale">Codice tributo ministeriale</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloInteressi(int p_idEnte,int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, int? p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            join_tipo_avvpag_voci_contrib_new v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiInternal(p_idEnte,p_idEntrata, p_idTipoServizio, p_idTipoAvvPag,
                                                                                            p_idTipoVocePrimaria, p_codiceTributoMinisteriale, p_dbContext);
            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiInternal(p_idEnte,p_idEntrata, p_idTipoServizio, p_idTipoAvvPag, null, p_codiceTributoMinisteriale, p_dbContext);
            }

            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiInternal(p_idEnte,p_idEntrata, p_idTipoServizio, null, null, p_codiceTributoMinisteriale, p_dbContext);
            }

            return v_join;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_idTipoAvvPag">ID Tipo Avviso Pagamento</param>
        /// /// <param name="p_idTipoVoce">ID Responsabile ricercato</param>
        /// /// <param name="p_codiceTributoMinisteriale">Codice tributo ministeriale</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        protected static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloInteressiInternal(int p_idEnte,int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, int? p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            var v_joinList = GetList(p_dbContext)
                           .Where(j => j.id_ente == p_idEnte)
                           .Where(j => j.id_entrata == p_idEntrata)
                           .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                           .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                           .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
                           .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                           .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                           .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale));                          

            if(v_joinList.Count() == 0)
            {
                v_joinList = GetList(p_dbContext)
                           .Where(j => j.id_ente == anagrafica_ente.ID_ENTE_GENERICO)
                           .Where(j => j.id_entrata == p_idEntrata)
                           .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                           .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                           .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
                           .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                           .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                           .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale));
            }

            if (v_joinList.Count() == 0 || v_joinList.Count() > 1) { return null; } // errore

            join_tipo_avvpag_voci_contrib_new v_join = v_joinList.FirstOrDefault();

            return v_join;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_idTipoAvvPag">ID Tipo Avviso Pagamento</param>
        /// /// <param name="p_idTipoVoce">ID Responsabile ricercato</param>
        /// /// <param name="p_codiceTributoMinisteriale">Codice tributo ministeriale</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        protected static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloInteressiRateizzazioneIntenal(int p_idEnte ,int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, int? p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            var v_joinList = GetList(p_dbContext)
                           .Where(j => j.id_ente == p_idEnte)
                           .Where(j => j.id_entrata == p_idEntrata)
                           .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                           .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                           .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
                           .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                           .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                           .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
                           .Where(j => j.tab_tipo_voce_contribuzione.tipo_interesse.Trim() == tab_tipo_voce_contribuzione.CODICE_IRA);//"IRA"                           

            if (v_joinList.Count() == 0)
            {
                v_joinList = GetList(p_dbContext)
                           .Where(j => j.id_ente == anagrafica_ente.ID_ENTE_GENERICO)
                           .Where(j => j.id_entrata == p_idEntrata)
                           .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                           .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                           .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
                           .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                           .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                           .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
                           .Where(j => j.tab_tipo_voce_contribuzione.tipo_interesse.Trim() == tab_tipo_voce_contribuzione.CODICE_IRA);//"IRA"                           
            }


            if (v_joinList.Count() == 0 || v_joinList.Count() > 1) { return null; } // errore

            join_tipo_avvpag_voci_contrib_new v_join = v_joinList.FirstOrDefault();

            return v_join;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_idTipoAvvPag">ID Tipo Avviso Pagamento</param>
        /// /// <param name="p_idTipoVoce">ID Responsabile ricercato</param>
        /// /// <param name="p_codiceTributoMinisteriale">Codice tributo ministeriale</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloInteressiRateizzazione(int p_idEnte, int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, int? p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

             join_tipo_avvpag_voci_contrib_new v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiRateizzazioneIntenal(p_idEnte,p_idEntrata, p_idTipoServizio, p_idTipoAvvPag,
                                                                                            p_idTipoVocePrimaria, p_codiceTributoMinisteriale, p_dbContext);
            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiRateizzazioneIntenal(p_idEnte,p_idEntrata, p_idTipoServizio, p_idTipoAvvPag, null, p_codiceTributoMinisteriale, p_dbContext);
            }

            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiRateizzazioneIntenal(p_idEnte,p_idEntrata, p_idTipoServizio, null, null, p_codiceTributoMinisteriale, p_dbContext);
            }
            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiRateizzazioneIntenal(p_idEnte, p_idEntrata, p_idTipoServizio, null, null, null, p_dbContext);
            }
            return v_join;
        }

        public static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloSanzioni(int p_idEnte, int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, 
                                                                                  string p_codiceTributoMinisteriale, int? p_sequenza_anag_voce, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            join_tipo_avvpag_voci_contrib_new v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloSanzioniInternal(p_idEnte, p_idEntrata, p_idTipoServizio, p_idTipoAvvPag,
                                                                                                                        p_codiceTributoMinisteriale, p_sequenza_anag_voce, p_dbContext);
            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloSanzioniInternal(p_idEnte, p_idEntrata, p_idTipoServizio, p_idTipoAvvPag, p_codiceTributoMinisteriale, p_sequenza_anag_voce, p_dbContext);
            }

            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloSanzioniInternal(p_idEnte, p_idEntrata, p_idTipoServizio, null, p_codiceTributoMinisteriale, p_sequenza_anag_voce, p_dbContext);
            }

            return v_join;
        }

        protected static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloSanzioniInternal(int p_idEnte, int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag,  
                                                                                             string p_codiceTributoMinisteriale, int? p_sequenza_anag_voce, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            var v_joinList = GetList(p_dbContext)
                            .Where(j => j.id_ente == p_idEnte)
                            .Where(j => j.id_entrata == p_idEntrata)
                            .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                            .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                            .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                            .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                            .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
                            .Where(j => j.sequenza_calcolo_anagrafica_voce == p_sequenza_anag_voce);

            if (v_joinList.Count() == 0)
            {
                v_joinList = GetList(p_dbContext)
                            .Where(j => j.id_ente == anagrafica_ente.ID_ENTE_GENERICO)
                            .Where(j => j.id_entrata == p_idEntrata)
                            .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                            .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                            .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                            .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                            .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
                            .Where(j => j.sequenza_calcolo_anagrafica_voce == p_sequenza_anag_voce);
            }

            if (v_joinList.Count() == 0 || v_joinList.Count() > 1) { return null; } // errore

            join_tipo_avvpag_voci_contrib_new v_join = v_joinList.FirstOrDefault();

            return v_join;
        }

        public static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloInteressi(int p_idEnte, int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, int? p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, int? p_sequenza_anag_voce, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            join_tipo_avvpag_voci_contrib_new v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiInternal(p_idEnte, p_idEntrata, p_idTipoServizio, p_idTipoAvvPag,
                                                                                            p_idTipoVocePrimaria, p_codiceTributoMinisteriale, p_sequenza_anag_voce,  p_dbContext);
            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiInternal(p_idEnte, p_idEntrata, p_idTipoServizio, p_idTipoAvvPag, null, p_codiceTributoMinisteriale, p_sequenza_anag_voce, p_dbContext);
            }

            if (v_join == null)
            {
                v_join = JoinTipoAvvpagVociContribNewBD.GetJoinForCalcoloInteressiInternal(p_idEnte, p_idEntrata, p_idTipoServizio, null, null, p_codiceTributoMinisteriale, p_sequenza_anag_voce, p_dbContext);
            }

            return v_join;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_idTipoAvvPag">ID Tipo Avviso Pagamento</param>
        /// /// <param name="p_idTipoVoce">ID Responsabile ricercato</param>
        /// /// <param name="p_codiceTributoMinisteriale">Codice tributo ministeriale</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        protected static join_tipo_avvpag_voci_contrib_new GetJoinForCalcoloInteressiInternal(int p_idEnte, int p_idEntrata, int p_idTipoServizio, int? p_idTipoAvvPag, int? p_idTipoVocePrimaria, string p_codiceTributoMinisteriale, int? p_sequenza_anag_voce, dbEnte p_dbContext)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            var v_joinList = GetList(p_dbContext)
                           .Where(j => j.id_ente == p_idEnte)
                           .Where(j => j.id_entrata == p_idEntrata)
                           .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                           .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                           .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
                           .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                           .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                           .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
                           .Where(j => j.sequenza_calcolo_anagrafica_voce == p_sequenza_anag_voce);

            if (v_joinList.Count() == 0)
            {
                v_joinList = GetList(p_dbContext)
                           .Where(j => j.id_ente == anagrafica_ente.ID_ENTE_GENERICO)
                           .Where(j => j.id_entrata == p_idEntrata)
                           .Where(j => j.id_tipo_servizio == p_idTipoServizio)
                           .Where(j => j.id_tipo_avv_pag == p_idTipoAvvPag)
                           .Where(j => j.id_rif_tipo_voce_contribuzione == p_idTipoVocePrimaria)
                           .Where(j => j.flag_calcolo_oggetti_contribuzione.Equals("1"))
                           .Where(j => j.cod_stato == join_tipo_avvpag_voci_contrib_new.ATT_ATT)
                           .Where(j => j.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codiceTributoMinisteriale))
                           .Where(j => j.sequenza_calcolo_anagrafica_voce == p_sequenza_anag_voce);
            }

            if (v_joinList.Count() == 0 || v_joinList.Count() > 1) { return null; } // errore

            join_tipo_avvpag_voci_contrib_new v_join = v_joinList.FirstOrDefault();

            return v_join;
        }
    }
}