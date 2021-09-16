using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabContribuzioneBD : EntityBD<tab_contribuzione>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("TabContribuzioneBD");

        public TabContribuzioneBD()
        {

        }

        public static new IQueryable<tab_contribuzione> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        public static new tab_contribuzione GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_contribuzione == p_id);
        }

        public static IQueryable<tab_contribuzione> GetListCreditiPrivilegiatiOrChirografari(int p_idEnte, /*int p_idEnteGestito,*/ decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, string p_tipoCredito, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                                     .Where(c => c.id_contribuente == p_idContribuente)
                                                                     .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
                                                                     .Where(c => c.importo_residuo > p_importoResiduo)
                                                                     .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                                     .Where(c => c.tab_tipo_voce_contribuzione.tipo_credito_privilegiato_chirografario.Equals(p_tipoCredito))
                                                                     .OrderBy(o => o.priorita_pagamento)
                                                                     .ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListEntIntSanNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                    || (c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_INTERESSI) && !c.tab_tipo_voce_contribuzione.tipo_interesse.StartsWith(tab_tipo_voce_contribuzione.CODICE_IRA))
                                                                    || c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_SANZIONI))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListEntIntSanNonInteramentePagateAvvisoSuccessivo(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagSuccessivo, int p_idAvvPagIniziale, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagSuccessivo)
                                                        .Where(c => c.id_avv_pag_collegato == p_idAvvPagIniziale || c.id_avv_pag_iniziale == p_idAvvPagIniziale ||
                                                                    c.id_avv_pag_intermedio == p_idAvvPagIniziale || c.id_avv_pag_intermedio_1 == p_idAvvPagIniziale ||
                                                                    c.id_avv_pag_intermedio_2 == p_idAvvPagIniziale || c.id_avv_pag_intermedio_3 == p_idAvvPagIniziale ||
                                                                    c.id_avv_pag_intermedio_4 == p_idAvvPagIniziale || c.id_avv_pag_intermedio_5 == p_idAvvPagIniziale
                                                                    )
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                    || (c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_INTERESSI) && !c.tab_tipo_voce_contribuzione.tipo_interesse.StartsWith(tab_tipo_voce_contribuzione.CODICE_IRA))
                                                                    || c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_SANZIONI))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListEntIntNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                    || (c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_INTERESSI) && !c.tab_tipo_voce_contribuzione.tipo_interesse.StartsWith(tab_tipo_voce_contribuzione.CODICE_IRA)))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        //public static IQueryable<tab_contribuzione> GetListEntIntNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        //{
        //    m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

        //    IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
        //                                                .Where(c => c.id_contribuente == p_idContribuente)
        //                                                .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
        //                                                .Where(c => c.importo_residuo > p_importoResiduo)
        //                                                .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
        //                                                .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT)
        //                                                            || c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_INTERESSI))
        //                                                .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
        //    return v_list;
        //}

        public static IQueryable<tab_contribuzione> GetListEntNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPag, decimal p_importoResiduo, dbEnte p_context)
        {
            //m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPag)
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListEntNonInteramentePagateAvvisoSuccessivo(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagSuccessivo, int p_idAvvPagIniziale, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagSuccessivo)
                                                        .Where(c => c.id_avv_pag_collegato == p_idAvvPagIniziale || c.id_avv_pag_iniziale == p_idAvvPagIniziale ||
                                                                    c.id_avv_pag_intermedio == p_idAvvPagIniziale || c.id_avv_pag_intermedio_1 == p_idAvvPagIniziale ||
                                                                    c.id_avv_pag_intermedio_2 == p_idAvvPagIniziale || c.id_avv_pag_intermedio_3 == p_idAvvPagIniziale ||
                                                                    c.id_avv_pag_intermedio_4 == p_idAvvPagIniziale || c.id_avv_pag_intermedio_5 == p_idAvvPagIniziale
                                                                    )
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(CodStato.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        //public static IQueryable<tab_contribuzione> GetListEntrateNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        //{
        //    m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

        //    IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
        //                                                .Where(c => c.id_contribuente == p_idContribuente)
        //                                                .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
        //                                                .Where(c => c.importo_residuo > p_importoResiduo)
        //                                                .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
        //                                                .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
        //                                                .OrderBy(o => o.id_tab_contribuzione);
        //    //.OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
        //    return v_list;
        //}

        public static IQueryable<tab_contribuzione> GetListEntrateNonSanzionatorieResidue(int p_idEnte, int p_idEnteGestito, int p_idAvvPag, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte)
                                                        .Where(c => c.id_avv_pag == p_idAvvPag)
                                                        .Where(c => c.importo_residuo > 0)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.anagrafica_entrate.flag_natura_entrata == anagrafica_entrate.NaturaEntrateTributariaT
                                                                 || c.tab_tipo_voce_contribuzione.anagrafica_entrate.flag_natura_entrata == anagrafica_entrate.NaturaEntratePatrimonialeExtratributariaE)
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                 || c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_INTERESSI))
                                                        .OrderBy(o => o.id_tab_contribuzione);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        .OrderBy(o => o.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListSanzioniEVerbaliNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPag, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte /*&& c.id_ente_gestito == p_idEnteGestito*/)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPag)
                                                        .Where(c => c.importo_residuo >= p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().StartsWith(tab_tipo_voce_contribuzione.CODICE_SANZIONI)
                                                                || c.tab_tipo_voce_contribuzione.anagrafica_entrate.flag_natura_entrata.Trim().StartsWith(anagrafica_entrate.NaturaEntrateSanzioneS))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);

            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListSanzioniNonInteramentePagate(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCollegato)
                                                        .Where(c => c.importo_residuo >= p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_SANZIONI)
                                                                /*||  c.tab_tipo_voce_contribuzione.anagrafica_entrate.flag_natura_entrata.Equals(anagrafica_entrate.NaturaEntrateSanzioneS)*/)
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListInteressiNonInteramentePagati(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCollegato, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCollegato).Where(c => c.id_avv_pag_iniziale == p_idAvvPagCollegato)
                                                        .Where(c => c.importo_residuo >= p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_INTERESSI))
                                                        .OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }


        public static IQueryable<tab_contribuzione> GetVociContribuzioneSpeseNOTorCOA(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCoattivo, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCoattivo /*&& c.id_avv_pag_iniziale == p_idAvvPagCoattivo*/)
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        //.Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_NOT) || 
                                                        //            c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_COA) ||
                                                        //            c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_AGG)) 
                                                        .Where(c => !c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_SANZIONI)
                                                                    && !c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                    && !c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_INTERESSI)
                                                        ).OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetVociContribuzioneSpeseNOTorCOASuAttiSuccessivi(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagCoattivo, decimal p_importoResiduo, dbEnte p_context)
        {
            m_logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                        .Where(c => c.id_contribuente == p_idContribuente)
                                                        .Where(c => c.id_avv_pag == p_idAvvPagCoattivo && c.id_avv_pag_iniziale == c.id_avv_pag)
                                                        .Where(c => c.importo_residuo > p_importoResiduo)
                                                        .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                        .Where(c => !c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_SANZIONI)
                                                                    && !c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                    && !c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_INTERESSI)
                                                        ).OrderBy(o => o.priorita_pagamento).ThenBy(c => c.anno_rif);
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListForPriorita(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN));

            return v_list;
        }

        public static decimal GetSumImportoPagato(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, dbEnte p_context)
        {
            decimal v_importoPagato = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_pagato);

            return v_importoPagato;
        }

        public static decimal GetSumImportoDaPagare(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, dbEnte p_context)
        {
            decimal v_importoDaPagare = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_da_pagare);

            return v_importoDaPagare;
        }

        public static decimal GetSumImportoResiduo(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, dbEnte p_context)
        {
            decimal v_importoResiduo = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_residuo);

            return v_importoResiduo;
        }

        public static IQueryable<tab_contribuzione> GetListForPriorita(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int? p_salvaPriorita, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                         .Where(c => c.importo_residuo > 0)
                                                         .Where(c => c.priorita_pagamento_true == p_salvaPriorita)
                                                         .OrderBy(c => c.anno_rif);

            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListForPrioritaCoa(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagIniziale, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => c.id_avv_pag_iniziale == p_idAvvPagIniziale)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN));

            return v_list;
        }

        public static decimal GetSumImportoPagato(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagIniziale, dbEnte p_context)
        {
            decimal v_importoPagato = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => c.id_avv_pag_iniziale == p_idAvvPagIniziale)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_pagato);

            return v_importoPagato;
        }

        public static decimal GetSumImportoDaPagare(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagIniziale, dbEnte p_context)
        {
            decimal v_importoDaPagare = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => c.id_avv_pag_iniziale == p_idAvvPagIniziale)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_da_pagare);

            return v_importoDaPagare;
        }

        public static decimal GetSumImportoResiduo(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagIniziale, dbEnte p_context)
        {
            decimal v_importoResiduo = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => c.id_avv_pag_iniziale == p_idAvvPagIniziale)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_residuo);

            return v_importoResiduo;
        }

        public static IQueryable<tab_contribuzione> GetListForPrioritaContrCollegati(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagCollegato, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => (c.id_avv_pag_intermedio_5 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_4 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_3 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_2 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_1 == p_idAvvPagCollegato || c.id_avv_pag_intermedio == p_idAvvPagCollegato || c.id_avv_pag_collegato == p_idAvvPagCollegato || c.id_avv_pag_iniziale == p_idAvvPagCollegato))
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN));

            return v_list;
        }

        public static decimal GetSumImportoPagatoColl(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagCollegato, dbEnte p_context)
        {
            decimal v_importoPagato = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => (c.id_avv_pag_intermedio_5 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_4 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_3 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_2 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_1 == p_idAvvPagCollegato || c.id_avv_pag_intermedio == p_idAvvPagCollegato || c.id_avv_pag_collegato == p_idAvvPagCollegato || c.id_avv_pag_iniziale == p_idAvvPagCollegato))
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_pagato);

            return v_importoPagato;
        }

        public static decimal GetSumImportoDaPagareColl(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagCollegato, dbEnte p_context)
        {
            decimal v_importoDaPagare = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => (c.id_avv_pag_intermedio_5 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_4 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_3 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_2 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_1 == p_idAvvPagCollegato || c.id_avv_pag_intermedio == p_idAvvPagCollegato || c.id_avv_pag_collegato == p_idAvvPagCollegato || c.id_avv_pag_iniziale == p_idAvvPagCollegato))
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_da_pagare);

            return v_importoDaPagare;
        }

        public static decimal GetSumImportoResiduoColl(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagCollegato, dbEnte p_context)
        {
            decimal v_importoResiduo = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => (c.id_avv_pag_intermedio_5 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_4 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_3 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_2 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_1 == p_idAvvPagCollegato || c.id_avv_pag_intermedio == p_idAvvPagCollegato || c.id_avv_pag_collegato == p_idAvvPagCollegato || c.id_avv_pag_iniziale == p_idAvvPagCollegato))
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN)).Sum(j => j.importo_residuo);

            return v_importoResiduo;
        }

        public static IQueryable<tab_contribuzione> GetListForPrioritaCollegato(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagCollegato, int? p_salvaPriorita, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => (c.id_avv_pag_intermedio_5 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_4 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_3 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_2 == p_idAvvPagCollegato || c.id_avv_pag_intermedio_1 == p_idAvvPagCollegato || c.id_avv_pag_intermedio == p_idAvvPagCollegato || c.id_avv_pag_collegato == p_idAvvPagCollegato || c.id_avv_pag_iniziale == p_idAvvPagCollegato))
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN))
                                                         .Where(c => c.importo_residuo > 0)
                                                         .Where(c => c.priorita_pagamento_true == p_salvaPriorita)
                                                         .OrderBy(c => c.anno_rif);

            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListAltreVoci(int p_idEnte, int p_idEnteGestito, decimal p_idContribuente, int p_idAvvPagAccreditato, int p_idAvvPagInt5, int p_idAvvPagInt4, int p_idAvvPagInt3, int p_idAvvPagInt2, int p_idAvvPagInt1, int p_idAvvPagIntermedio, int p_idAvvPagCollegato, int p_idAvvPagIniziale, int p_idTipoVoce, string p_annoRif, int p_idEntrata, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_ente == p_idEnte && c.id_ente_gestito == p_idEnteGestito)
                                                         .Where(c => c.id_contribuente == p_idContribuente)
                                                         //con la versione del 29/06/2016 è stata eliminata  .Where(c => c.id_avv_pag == p_idAvvPagAccreditato)
                                                         .Where(c => (c.id_avv_pag_intermedio_5 == p_idAvvPagInt5 || c.id_avv_pag_intermedio_4 == p_idAvvPagInt4 || c.id_avv_pag_intermedio_3 == p_idAvvPagInt3 || c.id_avv_pag_intermedio_2 == p_idAvvPagInt2 || c.id_avv_pag_intermedio_1 == p_idAvvPagInt1 || c.id_avv_pag_intermedio == p_idAvvPagIntermedio || c.id_avv_pag_collegato == p_idAvvPagCollegato || c.id_avv_pag_iniziale == p_idAvvPagIniziale))
                                                         .Where(c => c.id_tipo_voce_contribuzione == p_idTipoVoce)
                                                         .Where(c => c.anno_rif == p_annoRif)
                                                         .Where(c => c.id_entrata == p_idEntrata)
                                                         .Where(c => !c.cod_stato.StartsWith(tab_contribuzione.ANN));

            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListSanzioni(int p_idAvvPag, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_avv_pag == p_idAvvPag)
                                                         //.Where(c => c.importo_residuo == 0)
                                                         .Where(c => c.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_SANZIONI));
            return v_list;
        }

        public static IQueryable<tab_contribuzione> GetListByDettRiscossione(int p_idAvvPagAccreditato, tab_dett_riscossione p_dettRiscossione, dbEnte p_context)
        {
            IQueryable<tab_contribuzione> v_list = GetList(p_context).Where(c => c.id_avv_pag != p_idAvvPagAccreditato && c.id_avv_pag_iniziale != p_idAvvPagAccreditato &&
                                                                                 c.id_tipo_voce_contribuzione == p_dettRiscossione.id_tipo_voce_contribuzione)
                                                                     .Where(c => c.tab_avv_pag.dt_emissione.Value < p_dettRiscossione.data_accredito_pagamento)
                                                                        .Where(c => c.id_avv_pag_iniziale == p_dettRiscossione.id_avv_pag_iniziale)
                                                                        .Where(c => c.id_avv_pag_collegato == p_dettRiscossione.id_avv_pag_collegato)
                                                                        .Where(c => c.id_avv_pag_intermedio == p_dettRiscossione.id_avv_pag_intermedio)
                                                                        .Where(c => c.id_avv_pag_intermedio_1 == p_dettRiscossione.id_avv_pag_intermedio_1)
                                                                        .Where(c => c.id_avv_pag_intermedio_2 == p_dettRiscossione.id_avv_pag_intermedio_2)
                                                                        .Where(c => c.id_avv_pag_intermedio_3 == p_dettRiscossione.id_avv_pag_intermedio_3)
                                                                        .Where(c => c.id_avv_pag_intermedio_4 == p_dettRiscossione.id_avv_pag_intermedio_4)
                                                                        .Where(c => c.id_avv_pag_intermedio_5 == p_dettRiscossione.id_avv_pag_intermedio_5);
            return v_list;
        }

        public static void AggiornaContribuzione(tab_avv_pag avviso, dbEnte specificDB, tab_liste listaEmissione = null)
        {
            // Selezioniamo le unità di contribuzione collegate per creare le "tab_contribuzione"
            //IQueryable<tab_unita_contribuzione> v_unitaContribuzioneAvvisiCollegatiCompostiList = avviso.tab_unita_contribuzione
            //                                    .Where(avv => !avv.cod_stato.StartsWith(CodStato.ANN) &&
            //                                        avv.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_AVV_COLLEGATO)
            //                                    .AsQueryable();

            IQueryable<tab_unita_contribuzione> v_unitaContribuzioneAvvisiCollegatiCompostiList = avviso.tab_unita_contribuzione
                                                                                                        .Where(u => !u.cod_stato.StartsWith(CodStato.ANN))
                                                                                                        .Where(u => u.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_AVVISO_COLLEGATO)
                                                                                                        .AsQueryable();

            //aggiorna tab_contribuzione
            if (v_unitaContribuzioneAvvisiCollegatiCompostiList.Any())
            {
                //pag.59 LOOP_UNITA_AVVISI_COLEGATI
                m_logger.LogMessage(String.Format("tab_contribuzione - collegati: {0} elementi", v_unitaContribuzioneAvvisiCollegatiCompostiList.Count()), EnLogSeverity.Debug);
                //seleziona unita contribuzione composte
                foreach (tab_unita_contribuzione v_unita in v_unitaContribuzioneAvvisiCollegatiCompostiList)
                {
                    m_logger.LogMessage(String.Format("Elaborazione unita contribuzione id: {0}", v_unita.id_unita_contribuzione), EnLogSeverity.Debug);
                    int? v_idAvvPagCollegato = v_unita.id_avv_pag_collegato;
                    int v_idTipoAvvpagCollegato = v_unita.tab_avv_pag1.id_tipo_avvpag;
                    int v_tipoServizio = v_unita.tab_avv_pag1.anagrafica_tipo_avv_pag.id_servizio;

                    // bool vai__seleziona_tab_contribuzione_avvpag_no_coa = v_tipoServizio != anagrafica_tipo_servizi.ING_FISC;

                    if (v_unita.tab_avv_pag1.cod_stato.StartsWith(anagrafica_stato_avv_pag.RETTIFICATO))
                    {
                        v_idAvvPagCollegato = v_unita.tab_avv_pag1.tab_avv_pag_riemesso.FirstOrDefault().id_tab_avv_pag;
                    }

                    //AVV_NO_COA
                    //   if (vai__seleziona_tab_contribuzione_avvpag_no_coa) //TODO: verifica in caso di pignoram. con cit. del terzo è no_coa???
                    //{
                    m_logger.LogMessage("Lavorazione tab_contribuzione: no coa", EnLogSeverity.Debug);

                    //pag.61 seleziona_tab_contribuzione_avvpag_no_coa
                    IQueryable<tab_contribuzione> v_contribuzioneAvvNoCoaList = TabContribuzioneBD.GetList(specificDB).Where(c => c.id_avv_pag == v_idAvvPagCollegato)
                                                                                                                      .Where(c => c.importo_residuo >= 0)
                                                                                                                      .Where(c => !c.cod_stato.StartsWith(CodStato.ANN));

                    if (!v_contribuzioneAvvNoCoaList.Any())
                    {
                        //Mancano i dati di tab_contribuzione per id_avvpag_collegato
                        m_logger.LogMessage(String.Format("Avv coll. id: {0} -> Mancano i dati di tab_contribuzione per id_avvpag_collegato", v_idAvvPagCollegato), EnLogSeverity.Warn);
                        throw new Exception("Mancano i dati di tab_contribuzione per id_avvpag_collegato");
                    }

                    m_logger.LogMessage(String.Format("tab_contribuzione - no coa: {0} elementi", v_contribuzioneAvvNoCoaList.Count()), EnLogSeverity.Debug);
                    //pag.62 LOOP_LLEGGI_TAB_CONTRIBUZIONE_NO_AVVCOA
                    foreach (tab_contribuzione v_contribuzioneAvvNoCoa in v_contribuzioneAvvNoCoaList)
                    {
                        m_logger.LogMessage(String.Format("--> Elaborazione tab contribuzione id: {0}", v_contribuzioneAvvNoCoa.id_tab_contribuzione), EnLogSeverity.Debug);
                        tab_contribuzione v_newContribuzione = new tab_contribuzione();
                        v_newContribuzione.setProperties(v_contribuzioneAvvNoCoa);
                        v_newContribuzione.tab_avv_pag = null;
                        v_newContribuzione.importo_residuo = v_newContribuzione.importo_da_pagare;
                        v_newContribuzione.importo_pagato = 0;

                        //pag.63 if su id_avv_pag_collegato
                        //if (v_contribuzioneAvvNoCoa.tab_avv_pag != v_contribuzioneAvvNoCoa.tab_avv_pag1)
                        //{
                        if (v_contribuzioneAvvNoCoa.tab_avv_pag2 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag1 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag2 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else if (v_contribuzioneAvvNoCoa.tab_avv_pag3 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag2 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag3 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else if (v_contribuzioneAvvNoCoa.tab_avv_pag4 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag3 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag4 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else if (v_contribuzioneAvvNoCoa.tab_avv_pag5 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag4 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag5 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else if (v_contribuzioneAvvNoCoa.tab_avv_pag6 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag5 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag6 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else if (v_contribuzioneAvvNoCoa.tab_avv_pag7 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag6 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag7 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else if (v_contribuzioneAvvNoCoa.tab_avv_pag8 == null)
                        {
                            if (v_contribuzioneAvvNoCoa.tab_avv_pag7 != v_contribuzioneAvvNoCoa.tab_avv_pag)
                            {
                                v_newContribuzione.tab_avv_pag8 = v_contribuzioneAvvNoCoa.tab_avv_pag;
                            }
                        }
                        else { }
                        //}

                        avviso.tab_contribuzione.Add(v_newContribuzione);
                        m_logger.LogMessage(String.Format("--> tab contribuzione creata per avviso"), EnLogSeverity.Debug);
                    }
                    //}
                }
            } // fine composte

            m_logger.LogMessage("Lavorazione tab_contribuzione: elementari", EnLogSeverity.Debug);
            //IQueryable<tab_unita_contribuzione> v_unitaContribuzioneAvvisiCollegatiElementariList = avviso.tab_unita_contribuzione
            //                                        .Where(avv => !avv.cod_stato.StartsWith(CodStato.ANN) && avv.tab_tipo_voce_contribuzione.id_tipo_voce_contribuzione != tab_tipo_voce_contribuzione.ID_AVVISO_COLLEGATO).AsQueryable();

            IQueryable<tab_unita_contribuzione> v_unitaContribuzioneAvvisiCollegatiElementariList = avviso.tab_unita_contribuzione
                                                    .Where(u => !u.cod_stato.StartsWith(CodStato.ANN) && !u.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE))
                                                    .Where(avv => avv.tab_tipo_voce_contribuzione.id_tipo_voce_contribuzione != tab_tipo_voce_contribuzione.ID_AVVISO_COLLEGATO).AsQueryable();

            foreach (var v_unita in v_unitaContribuzioneAvvisiCollegatiElementariList)
            {
                m_logger.LogMessage(String.Format("--> Elaborazione riepilogo con id tipo voce: {0}", v_unita.id_tipo_voce_contribuzione), EnLogSeverity.Debug);
                tab_tipo_voce_contribuzione currTipoVoce = TabTipoVoceContribuzioneBD.GetById(v_unita.id_tipo_voce_contribuzione, specificDB);

                int id_entrata_contribuzione = v_unita.id_entrata;//v_AvvisoDef.anagrafica_tipo_avv_pag.id_entrata;

                tab_contribuzione v_newContribuzione = new tab_contribuzione()
                {
                    id_ente = avviso.id_ente,
                    id_ente_gestito = avviso.id_ente_gestito,
                    id_entrata = id_entrata_contribuzione,
                    id_contribuente = avviso.id_anag_contribuente,
                    anno_rif = v_unita.anno_rif,
                    id_tipo_voce_contribuzione = v_unita.id_tipo_voce_contribuzione,
                    aliquota_iva = v_unita.aliquota_iva ?? 0,
                    importo_residuo = v_unita.importo_unita_contribuzione ?? 0,
                    importo_pagato = 0,
                    importo_da_pagare = v_unita.importo_unita_contribuzione ?? 0,
                    importo_ridotto = v_unita.importo_ridotto ?? 0,
                    flag_segno = "1",
                    id_stato = tab_contribuzione.ATT_ATT_ID,
                    cod_stato = tab_contribuzione.ATT_ATT,
                    priorita_pagamento = v_unita.tab_tipo_voce_contribuzione.id_priorita_pagamento_vera ?? 0, // SBAGLIATO v_unita.tab_tipo_voce_contribuzione.id_priorita_pagamento_vera ?? 0,
                    numero_accertamento_contabile = v_unita.numero_accertamento_contabile,
                    data_accertamento_contabile = v_unita.data_accertamento_contabile,
                    id_tipo_avvpag_origine = v_unita.id_tipo_avvpag_origine,
                    descr_tipo_avvpag_origine = v_unita.descr_tipo_avvpag_origine,
                    identificativo_avvpag_origine = v_unita.identificativo_avvpag_origine,
                    data_notifica_avvpag_origine = v_unita.data_notifica_avvpag_origine
                };

                // Valorizzo tab_contribuzione.id_avv_pag
                avviso.tab_contribuzione.Add(v_newContribuzione);

                if (listaEmissione == null)
                {
                    // tab_contribuzione.id_avv_pag_iniziale
                    if (v_unita.id_tipo_avvpag_origine != null || v_unita.descr_tipo_avvpag_origine != null)
                    {
                        v_newContribuzione.id_avv_pag_iniziale = null;
                    }
                    else
                    {
                        // tab_contribuzione.id_avv_pag_iniziale = v_newContribuzione.id_tab_contribuzione,
                        // cioè:
                        avviso.tab_contribuzione1.Add(v_newContribuzione);
                    }
                }
                else
                {
                    // 15/06/2017 - da appunti
                    if (listaEmissione.tab_tipo_lista.cod_lista != tab_tipo_lista.TIPOLISTA_TRASMISSIONE)
                    {
                        avviso.tab_contribuzione1.Add(v_newContribuzione);
                    }
                    else //Lista di trasmissione
                    {
                        // tab_contribuzione.id_avv_pag_iniziale
                        if (v_unita.id_tipo_avvpag_origine != null || v_unita.descr_tipo_avvpag_origine != null)
                        {
                            v_newContribuzione.id_avv_pag_iniziale = null;
                        }
                        else
                        {
                            // tab_contribuzione.id_avv_pag_iniziale = v_newContribuzione.id_tab_contribuzione,
                            // cioè:
                            avviso.tab_contribuzione1.Add(v_newContribuzione);
                        }
                    }
                }

                if (v_unita.id_avv_pag_voce_da_recuperare.HasValue)
                {
                    v_newContribuzione.id_avv_pag_iniziale = v_unita.id_avv_pag_voce_da_recuperare;
                }

                if (v_unita.id_unita_contribuzione_collegato.HasValue && v_unita.tab_unita_contribuzione2 != null)
                {
                    int? id_avv_pag_collegato = v_unita.tab_unita_contribuzione2.id_avv_pag_collegato;
                    v_newContribuzione.id_avv_pag_riferimento_voce = id_avv_pag_collegato;
                }

                m_logger.LogMessage(String.Format("--> tab contribuzione creata per avviso"), EnLogSeverity.Debug);
            }
        }

    }
}
