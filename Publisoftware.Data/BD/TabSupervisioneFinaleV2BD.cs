using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabSupervisioneFinaleV2BD : EntityBD<TAB_SUPERVISIONE_FINALE_V2>
    {
        public const decimal ISCRIZIONE_FERMI_IMPORTO_MINIMO_DA_PAGARE = 15.0M;
        
        public TabSupervisioneFinaleV2BD()
        {
        }

        /// <summary>
        /// Filtro per avviso di pagamento emesso
        /// </summary>
        /// <param name="p_idAvvPag"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetByIdAvvPag(int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.ID_AVVPAG_EMESSO == p_idAvvPag);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetByIdTipoAvvPagDaEmettere(int p_idAvvPag,
            dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.ID_TIPO_AVVPAG_DA_EMETTERE == p_idAvvPag);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetByIdIspezione(int p_idIspezione, int p_idEnte,
            dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                .Where(sf => sf.ID_ENTE == p_idEnte && sf.ID_TAB_ISPEZIONE_COATTIVO == p_idIspezione);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetConAvvDaEmettere(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(sf => sf.ID_ENTE == p_idEnte
                                                    && !sf.ID_TIPO_AVVPAG_EMESSO.HasValue
                                                    && sf.ID_TIPO_AVVPAG_DA_EMETTERE.HasValue
                                                    && sf.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL);
        }

        /// <summary>
        /// Filtro per codice fiscale/partita iva del terzo pignorato
        /// </summary>
        /// <param name="p_codiceFiscalePIVA"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetByCodiceFiscalePIVANonAnnullato(
            string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetList(p_dbContext).Where(d =>
                    d.tab_profilo_contribuente_new.tab_terzo_debitore.tab_terzo.cod_fiscale
                        .Equals(p_codiceFiscalePIVA) &&
                    d.ID_AVVPAG_EMESSO.HasValue &&
                    !d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag
                        .ANNULLATO) &&
                    !d.tab_profilo_contribuente_new.tab_terzo_debitore.cod_stato.StartsWith(CodStato.ANN) &&
                    !d.tab_profilo_contribuente_new.cod_stato.StartsWith(CodStato.ANN) &&
                    !d.COD_STATO.StartsWith(TAB_SUPERVISIONE_FINALE_V2.ANN));
            }
            else
            {
                return GetList(p_dbContext).Where(d =>
                    d.tab_profilo_contribuente_new.tab_terzo_debitore.tab_terzo.p_iva.Equals(p_codiceFiscalePIVA) &&
                    d.ID_AVVPAG_EMESSO.HasValue &&
                    !d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag
                        .ANNULLATO) &&
                    !d.tab_profilo_contribuente_new.tab_terzo_debitore.cod_stato.StartsWith(CodStato.ANN) &&
                    !d.tab_profilo_contribuente_new.cod_stato.StartsWith(CodStato.ANN) &&
                    !d.COD_STATO.StartsWith(TAB_SUPERVISIONE_FINALE_V2.ANN));
            }
        }

        /// <summary>
        /// Filtro per tipo ispezione
        /// Seleziona le supervisioni in seguito per la presentazione della pratica di iscrizione di fermo amministrativo
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerPresentazionePraticaIscrizioneFermo(
            int p_idEnte, dbEnte p_dbContext)
        {
            //return GetList(p_dbContext).Where(c => c.ID_ENTE == p_idEnte && (c.ID_TIPO_AVVPAG_EMESSO == null || c.ID_TIPO_AVVPAG_EMESSO == 0)
            //        && (c.ID_AVVPAG_EMESSO == null || c.ID_AVVPAG_EMESSO == 0) && (c.id_avvpag_preavviso_collegato == null || c.id_avvpag_preavviso_collegato == 0)
            //        && (c.COD_STATO.Equals(CodStato.SSP_DIS) || c.COD_STATO.Equals("SSP-TN1") || c.COD_STATO.Equals("SSP-TN1")) && c.join_tab_supervisione_profili.Any(i => i.riferimento_registro_protocollo_atto_coattivo == null && i.cod_stato.StartsWith(CodStato.SSP)));

            //vers.del 01/02/2017
            //return GetList(p_dbContext).Where(c => c.ID_ENTE == p_idEnte && (c.ID_TIPO_AVVPAG_EMESSO == null || c.ID_TIPO_AVVPAG_EMESSO == 0)&& c.ID_TIPO_AVVPAG_DA_EMETTERE==anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
            //        && (c.ID_AVVPAG_EMESSO == null || c.ID_AVVPAG_EMESSO == 0) && (c.id_avvpag_preavviso_collegato != null)
            //        && (c.COD_STATO.Equals(TAB_SUPERVISIONE_FINALE_V2.VAL_DIS) || c.COD_STATO.Equals(TAB_SUPERVISIONE_FINALE_V2.VAL_IF1) || c.COD_STATO.Equals(TAB_SUPERVISIONE_FINALE_V2.VAL_IF2)) && c.join_tab_supervisione_profili.Any(i => i.num_protocollo_registro_iscrizione_bene == null));


            //vers.del 12/04/2018
            return GetList(p_dbContext).Where(c =>
                c.ID_ENTE == p_idEnte && (c.ID_TIPO_AVVPAG_EMESSO == null || c.ID_TIPO_AVVPAG_EMESSO == 0) &&
                c.ID_TIPO_AVVPAG_DA_EMETTERE == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                && (c.ID_AVVPAG_EMESSO == null || c.ID_AVVPAG_EMESSO == 0) && (c.id_avvpag_preavviso_collegato != null)
                && c.join_tab_supervisione_profili.Any(i =>
                    i.num_protocollo_registro_iscrizione_bene == null &&
                    (i.cod_stato.Equals(join_tab_supervisione_profili.VAL_DIS) ||
                     i.cod_stato.Equals(join_tab_supervisione_profili.VAL_IF1) ||
                     i.cod_stato.Equals(join_tab_supervisione_profili.VAL_IF2)))); //.Take(20);

            //x test del13/06/2018
            //return GetList(p_dbContext).Where(c => c.ID_ENTE == p_idEnte && c.ID_TIPO_AVVPAG_DA_EMETTERE == 250 && c.ID_TAB_SUPERVISIONE_FINALE== 236480).Take(10);
        }

        /// <summary>
        /// Filtro per tipo ispezione. 
        /// Seleziona le supervisioni in seguito alla presentazione della pratica di iscrizione di fermo amministrativo
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerVerificareAcquisizioneIscrizioneFermo(
            int p_idEnte, dbEnte p_dbContext)
        {
            //vers.del 14/04/2018
            return GetList(p_dbContext).Where(c =>
                c.ID_ENTE == p_idEnte && /*(c.ID_TIPO_AVVPAG_EMESSO == null || c.ID_TIPO_AVVPAG_EMESSO == 0) &&*/
                c.ID_TIPO_AVVPAG_DA_EMETTERE == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                && /*(c.ID_AVVPAG_EMESSO == null || c.ID_AVVPAG_EMESSO == 0) &&*/
                (c.id_avvpag_preavviso_collegato != null)
                && c.join_tab_supervisione_profili.Any(i =>
                    i.num_protocollo_registro_iscrizione_bene != null &&
                    (i.cod_stato.Equals(join_tab_supervisione_profili.VAL_PRE))));
        }

        /// <summary>
        /// Filtro per tipo ispezione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerCancellazioneFermo(int p_idEnte,
            dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c =>
                c.ID_ENTE == p_idEnte && (c.ID_AVVPAG_EMESSO != null) && (c.id_avvpag_preavviso_collegato != null) &&
                c.ID_TIPO_AVVPAG_EMESSO == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                && (c.COD_STATO.Equals(TAB_SUPERVISIONE_FINALE_V2.VAL_DIS) ||
                    c.COD_STATO.Equals(TAB_SUPERVISIONE_FINALE_V2.VAL_IF1) ||
                    c.COD_STATO.Equals(TAB_SUPERVISIONE_FINALE_V2.VAL_IF2)) && c.join_tab_supervisione_profili.Any(i =>
                    i.num_protocollo_registro_iscrizione_bene == null &&
                    i.cod_stato.StartsWith(TAB_SUPERVISIONE_FINALE_V2.ANN_PEC)));
        }

        public static TAB_SUPERVISIONE_FINALE_V2 GetByPreavvisoCollegatoValido(int p_idAvvPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c =>
                c.id_avvpag_preavviso_collegato == p_idAvvPag && c.COD_STATO.StartsWith(CodStato.VAL));
        }

        /// <summary>
        /// Filtro per codice fiscale/partita iva del terzo pignorato
        /// </summary>
        /// <param name="p_codiceFiscalePIVA"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetByCFPIVASoggIspIdServizioNonAnnullato(
            string p_codiceFiscalePIVA, decimal p_idContribuente, int p_idTipoServizio, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                .Where(d => d.ID_CONTRIBUENTE == d.tab_ispezioni_coattivo_new.id_contribuente &&
                            d.tab_ispezioni_coattivo_new.cfiscale_piva_soggetto_ispezione.Equals(p_codiceFiscalePIVA) &&
                            !d.COD_STATO.StartsWith(CodStato.ANN) &&
                            !d.tab_avv_pag.cod_stato.StartsWith(CodStato.ANN) &&
                            !d.tab_avv_pag.cod_stato.StartsWith(CodStato.SSP) &&
                            !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE) &&
                            d.anagrafica_tipo_avv_pag1.id_servizio == p_idTipoServizio &&
                            d.tab_ispezioni_coattivo_new.id_contribuente == p_idContribuente &&
                            d.tab_avv_pag.flag_esito_sped_notifica != null &&
                            d.tab_avv_pag.flag_esito_sped_notifica == "1");
        }

        /// <summary>
        /// Filtro per codice fiscale/partita iva del terzo pignorato
        /// </summary>
        /// <param name="p_codiceFiscalePIVA"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetByCFPIVASoggIspPreavvisiOFermiNonPagati(
            string p_codiceFiscalePIVA, decimal p_idContribuente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext)
                .Where(d =>
                    d.ID_CONTRIBUENTE == d.tab_ispezioni_coattivo_new.id_contribuente && d.tab_ispezioni_coattivo_new
                                                                                          .cfiscale_piva_soggetto_ispezione
                                                                                          .Equals(p_codiceFiscalePIVA)
                                                                                      && d.tab_ispezioni_coattivo_new
                                                                                          .id_tab_ispezione_coattivo ==
                                                                                      d.ID_TAB_ISPEZIONE_COATTIVO &&
                                                                                      !d.COD_STATO.StartsWith(
                                                                                          CodStato.ANN)
                                                                                      && d.tab_ispezioni_coattivo_new
                                                                                          .id_contribuente ==
                                                                                      p_idContribuente)
                .Where(s => s.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.VAL_FIS)
                            || (s.tab_avv_pag.anagrafica_tipo_avv_pag.id_tipo_avvpag ==
                                anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO &&
                                s.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.VAL_EME)
                            || (s.tab_avv_pag.anagrafica_tipo_avv_pag.id_tipo_avvpag ==
                                anagrafica_tipo_avv_pag.PRE_FERMO_AMM && s.tab_avv_pag.flag_esito_sped_notifica != null
                                                                      && s.tab_avv_pag.flag_esito_sped_notifica ==
                                                                      "1" && s.tab_avv_pag.cod_stato.StartsWith(
                                                                          anagrafica_stato_avv_pag.VALIDO)
                                                                      && s.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V22
                                                                          .Where(f => f.COD_STATO.StartsWith(
                                                                              TAB_SUPERVISIONE_FINALE_V2.VAL))
                                                                          .Count() == 0))
                .Where(s => (s.tab_avv_pag.imp_tot_avvpag - s.tab_avv_pag.imp_tot_pagato) > 15);
        }

        public static void AnnullaSupervisione(TAB_SUPERVISIONE_FINALE_V2 p_supervisione)
        {
            p_supervisione.COD_STATO = TAB_SUPERVISIONE_FINALE_V2.ANN_ANN;

            //foreach (tab_ispezioni_coattivo_new v_ispezione in p_supervisione.tab_ispezioni_coattivo_new1)
            //{
            //    v_ispezione.flag_supervisione_finale = "0";
            //    v_ispezione.id_tab_ispezione_coattivo_soggetto_supervisione = null;
            //}

            p_supervisione.tab_ispezioni_coattivo_new.flag_supervisione_finale = "0";
            p_supervisione.tab_ispezioni_coattivo_new.id_tab_ispezione_coattivo_soggetto_supervisione = null;
        }

        #region Copernico3 WS ACI 

        public static IQueryable<join_tab_supervisione_profili> QueryWhereTabSupervisioneFinaleV2IsFermoAmministrativo(
            int p_idEnte,
            dbEnte p_dbContext,
            bool isIscrizione) // PATCH!
        {
            return WhereTabSupervisioneFinaleV2IsFermoAmministrativo(p_idEnte, JoinTabSupervisioneProfiliBD.GetList(p_dbContext), isIscrizione);
        }

        public static IQueryable<join_tab_supervisione_profili> WhereTabSupervisioneFinaleV2IsFermoAmministrativo(
            int p_idEnte,
            IQueryable<join_tab_supervisione_profili> qry,
            bool isIscrizione) // PATCH!
        {
            var query = qry.Where(c =>
                //--------
                //c.ID_TIPO_AVVPAG_EMESSO!=null && c.ID_TIPO_AVVPAG_EMESSO == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                c.TAB_SUPERVISIONE_FINALE_V2.COD_STATO == "VAL-VAL"
                //--------
                && c.TAB_SUPERVISIONE_FINALE_V2.ID_TIPO_AVVPAG_DA_EMETTERE == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                //&& (c.TAB_SUPERVISIONE_FINALE_V2.ID_TIPO_AVVPAG_EMESSO == null)
                && (c.TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato != null)
                && c.TAB_SUPERVISIONE_FINALE_V2.ID_ENTE == p_idEnte);


            query = isIscrizione ? query.Where(x => x.TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO == null) : query.Where(x => x.TAB_SUPERVISIONE_FINALE_V2.ID_AVVPAG_EMESSO != null);
            return query;
        }

        private static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecordsFermiAmministrativiC3(
            int p_idEnte,
            dbEnte p_dbContext,
            bool isIscrizione,
            bool bPrendereAncheANNConAvvisoANN,
            decimal importo_minimo_da_pagare) // PATCH!
        {
            IQueryable<TAB_SUPERVISIONE_FINALE_V2> query = GetList(p_dbContext).Where(c =>
                //--------
                //c.ID_TIPO_AVVPAG_EMESSO!=null && c.ID_TIPO_AVVPAG_EMESSO == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                //--------
                // Spostato dopo che a volte si devono prendere gli annullati per sgravio
                // (tipicamente dovrebbero essere cancellazioni per indebita iscrizione)
                //      c.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL &&
                //--------
                c.ID_TIPO_AVVPAG_DA_EMETTERE == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                //&& (c.ID_TIPO_AVVPAG_EMESSO == null)
                && (c.id_avvpag_preavviso_collegato != null)
                // ------------------------------------------------------------------------------------------------
                // I programmi successivi tagliano per importo minimo usando:
                // --- da modalità rate c'è importo_minimo_emissione_avvpag: prendi da là per id_tipo_avvpag 250
                // se non tagliamo qui può succedere che iscriviamo un fermo che poi non abbiamo
                && (c.tab_avv_pag2.importo_tot_da_pagare!=null) && (c.tab_avv_pag2.importo_tot_da_pagare >= importo_minimo_da_pagare)
                // Questa la metto perché non mi fido di questi nomi "tab_avv_pag1", "tab_avv_pag2", ...
                // che se qualcuno fa qualcosa al modello e cambiano i numeri addios,
                // aggiungendo questa condizione se succedesse non selezionerebbe mai nulla (meglio del disastro totale):
                && (c.id_avvpag_preavviso_collegato!=null && c.tab_avv_pag2.id_tab_avv_pag == c.id_avvpag_preavviso_collegato)
                // ------------------------------------------------------------------------------------------------
                && c.ID_ENTE == p_idEnte);
            if (bPrendereAncheANNConAvvisoANN)
            {
                query = query.Where(x =>
                    // validi
                    x.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL
                    ||
                    (
                        // annullati con ID_AVVPAG_EMESSO con avviso sgravato 
                        x.COD_STATO.StartsWith(TAB_SUPERVISIONE_FINALE_V2.ANN)
                        && x.tab_avv_pag.id_tab_avv_pag == x.ID_AVVPAG_EMESSO
                        &&
                        (
                            // Volevo prendere solo i sgravati, ma il dot. ha detto che i tipi di 
                            // annullamento possibilie sono tanti e conviene prendere tutti gli avvisi 'ANN-%'
                            //      x.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.ANNULLATO_SGRAVIO
                            //      || x.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.ANNULLATO_UFFICIO
                            x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)
                        )
                    )
                );
            }
            else
            {
                query = query.Where(x => x.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL);
            }

            return isIscrizione ? query.Where(x => x.ID_AVVPAG_EMESSO == null) 
                : query.Where(x => x.ID_AVVPAG_EMESSO != null);
        }

       
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecordsFermiAmministrativiByCodStato(
            int idEnte,
            dbEnte dbContext,
            IList<string> statiAmmessi,
            bool isIscrizione,
            bool bPrendereAncheANNConAvvisoANN,
            decimal importo_minimo_da_pagare)
        {
            return GetListRecordsFermiAmministrativiC3(
                    p_idEnte: idEnte,
                    p_dbContext: dbContext,
                    isIscrizione: isIscrizione,
                    bPrendereAncheANNConAvvisoANN: bPrendereAncheANNConAvvisoANN,
                    importo_minimo_da_pagare: importo_minimo_da_pagare)
                .Where(x =>
                    x.join_tab_supervisione_profili.Any(y => statiAmmessi.Any(z => z == y.cod_stato))
                );
        }

        // public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecordsFermiAmministrativiBySingleCodStato(
        //     int idEnte, dbEnte dbContext, string statoAmmesso, bool isIscrizione)
        // {
        //     return GetListRecordsFermiAmministrativiC3(idEnte, dbContext, isIscrizione)
        //         .Where(x =>
        //             x.join_tab_supervisione_profili.Any(y => y.cod_stato==statoAmmesso)
        //         );
        // }

        /// <summary>
        /// Filtro per tipo ispezione
        /// Seleziona le supervisioni per la creazione della pratica di iscrizione FA copernico 3
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        [Obsolete("Usare GetListRecordsFermiAmministrativiByCodStato")]
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerCreazionePraticaIscrizioneFermoC3(
            int p_idEnte, dbEnte p_dbContext, decimal importo_minimo_da_pagare)
        {
            /*
            return GetList(p_dbContext).Where(c =>
                //--------
                 //c.ID_TIPO_AVVPAG_EMESSO!=null && c.ID_TIPO_AVVPAG_EMESSO == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                 c.COD_STATO== "VAL-VAL"
                //--------
        
                && c.ID_TIPO_AVVPAG_DA_EMETTERE == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                && (c.ID_AVVPAG_EMESSO == null)
                //&& (c.ID_TIPO_AVVPAG_EMESSO == null)
                && (c.id_avvpag_preavviso_collegato != null)
        
                && c.ID_ENTE == p_idEnte
                
                    && c.join_tab_supervisione_profili.Any(i =>
                            i.num_protocollo_registro_iscrizione_bene == null 
                            && (
                                i.cod_stato.Equals(join_tab_supervisione_profili.VAL_DIS)
                                || i.cod_stato.Equals(join_tab_supervisione_profili.VAL_IF1)
                                || i.cod_stato.Equals(join_tab_supervisione_profili.VAL_IF2)))
                   ); //.Take(20);
            */
            return GetListRecordsFermiAmministrativiC3(p_idEnte, p_dbContext,
                                isIscrizione: true,  bPrendereAncheANNConAvvisoANN: false, importo_minimo_da_pagare: importo_minimo_da_pagare)
                .Where(x => x.join_tab_supervisione_profili.Any(y =>
                    y.cod_stato.Equals(join_tab_supervisione_profili.VAL_DIS)
                    || y.cod_stato.Equals(join_tab_supervisione_profili.VAL_IF1)
                    || y.cod_stato.Equals(join_tab_supervisione_profili.VAL_IF2))
                );
        }
        
#if false
        [Obsolete("Usare GetListRecordsFermiAmministrativiByCodStato")]
        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerCreazionePraticaRevocaFermoC3(int p_idEnte,
            dbEnte p_dbContext)
        {
            return GetListRecordsFermiAmministrativiC3(p_idEnte, p_dbContext,
                                isIscrizione: false)
                .Where(x => x.join_tab_supervisione_profili.Any(y =>
                    y.cod_stato.Equals(join_tab_supervisione_profili.REV_DIS)
                    || y.cod_stato.Equals(join_tab_supervisione_profili.REV_DI1)
                    || y.cod_stato.Equals(join_tab_supervisione_profili.REV_DI2))
                );
        }
#endif
        
        // [Obsolete("Usare GetListRecordsFermiAmministrativiByCodStato")]
        // public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerCreazionePraticaCancellazioneFermoC3(
        //     int p_idEnte, dbEnte p_dbContext)
        // {
        //     return GetListRecordsFermiAmministrativiC3(p_idEnte, p_dbContext,
        //                         isIscrizione: false)
        //         .Where(x => x.join_tab_supervisione_profili.Any(y =>
        //             y.cod_stato.Equals(join_tab_supervisione_profili.CAN_DIS)
        //             || y.cod_stato.Equals(join_tab_supervisione_profili.CAN_CR1)
        //             || y.cod_stato.Equals(join_tab_supervisione_profili.CAN_CR2))
        //         );
        // }

        // [Obsolete("Usare GetListRecordsFermiAmministrativiByCodStato")]
        // public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsFermoByJoinTabSupervisioneProfiliCodStatoC3(
        //     int p_idEnte, dbEnte p_dbContext, string codStatoJoinSupervisioneProfili, bool isIscrizione)
        // {
        //     return GetListRecordsFermiAmministrativiC3(p_idEnte, p_dbContext, isIscrizione: isIscrizione)
        //         .Where(x => x.join_tab_supervisione_profili
        //             .Any(y =>
        //                 y.cod_stato.Equals(codStatoJoinSupervisioneProfili)
        //             )
        //         );
        // }


        //        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerLetturaPraticaIscrizioneFermoC3(int p_idEnte, dbEnte p_dbContext)
        //        {
        //            /*
        //            //BISOGNA CHIEDERE AL DOTTORE GLI STATI
        //            return GetList(p_dbContext).Where(c =>
        //                    c.ID_ENTE == p_idEnte
        //                    && (c.ID_TIPO_AVVPAG_EMESSO == null || c.ID_TIPO_AVVPAG_EMESSO == 0)
        //                    && c.ID_TIPO_AVVPAG_DA_EMETTERE == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
        //                    && (c.ID_AVVPAG_EMESSO == null || c.ID_AVVPAG_EMESSO == 0) && (c.id_avvpag_preavviso_collegato != null)
        //                    && c.join_tab_supervisione_profili.Any(i =>
        //                        i.num_protocollo_registro_iscrizione_bene == null 
        //                        && (i.cod_stato.Equals(join_tab_supervisione_profili.VAL_PRE) ))
        //                  );//.Take(20);
        //             */
        //            return GetListRecsPerLetturaPraticaFermoC3(p_idEnte, p_dbContext, join_tab_supervisione_profili.VAL_PRE);
        //        }

        //        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerLetturaPraticaCancellazioneFermoC3(int p_idEnte, dbEnte p_dbContext)
        //        {
        //            return GetListRecsPerLetturaPraticaFermoC3(p_idEnte, p_dbContext, join_tab_supervisione_profili.CAN_PRE);
        //        }
        //        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerLetturaPraticaRevocaFermoC3(int p_idEnte, dbEnte p_dbContext)
        //        {
        //            return GetListRecsPerLetturaPraticaFermoC3(p_idEnte, p_dbContext, join_tab_supervisione_profili.REV_PRE);
        //        }

        // public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerPresentaPraticaFermoC3(
        //     int p_idEnte,
        //     dbEnte p_dbContext,
        //     string codStatoJoinSupervisioneProfili)
        // {
        //     return GetListRecordsFermiAmministrativiC3(p_idEnte, p_dbContext)
        //         .Where(x => x.join_tab_supervisione_profili.Any(y =>
        //             y.num_protocollo_registro_iscrizione_bene == null
        //             && (y.cod_stato.Equals(codStatoJoinSupervisioneProfili)))
        //         );
        // }

        // public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> GetListRecsPerLetturaPraticaFermoC3(
        //     int p_idEnte,
        //     dbEnte p_dbContext,
        //     string codStatoJoinSupervisioneprofili)
        // {
        //     return GetListRecordsFermiAmministrativiC3(p_idEnte, p_dbContext)
        //         .Where(x => x.join_tab_supervisione_profili.Any(y =>
        //             y.num_protocollo_registro_iscrizione_bene != null
        //             && (y.cod_stato.Equals(codStatoJoinSupervisioneprofili)))
        //         );
        // }

        #endregion
    }
}