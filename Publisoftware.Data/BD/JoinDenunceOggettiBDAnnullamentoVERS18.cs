using Publisoftware.Data.LinqExtended;
using Publisoftware.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public partial class JoinDenunceOggettiBD
    {
        public static async Task<join_denunce_oggetti> RetrieveJoinDenunceOggettiDaAnnullareVERS18(
           tab_oggetti_contribuzione oggContDenunciaSelezionato,
           tab_contribuente contrib,
           tab_denunce_contratti denunceContratti,
           dbEnte dbContext)
        {
            decimal salva_id_oggetto_annullato = oggContDenunciaSelezionato.id_oggetto;
            IList<join_denunce_oggetti> joinDenunceOggettiList = await JoinDenunceOggettiBD.GetList(dbContext)
                .Where(x =>
                    x.id_doc_input == null &&
                    x.id_denunce_contratti != null &&
                    x.tab_denunce_contratti.cod_stato == anagrafica_stato_denunce.ATT_ATT &&
                    x.id_denunce_contratti != denunceContratti.id_tab_denunce_contratti &&
                    // x.id_oggetti_contribuzione == oggContDenunciaDaVariare.id_oggetto_contribuzione &&
                    x.tab_oggetti_contribuzione.id_oggetto == salva_id_oggetto_annullato &&
                    (
                        !x.cod_stato.StartsWith(CodStato.ANN)
                        // Vecchi - non mettevano cod_stato:
                        || x.cod_stato == null
                    )
                    &&
                    !x.tab_oggetti_contribuzione.cod_stato_oggetto.StartsWith(CodStato.ANN))
                //.Include(x => x.tab_oggetti_contribuzione)
                //.Include(x => x.tab_oggetti_contribuzione.tab_oggetti)
                .OrderByDescending(x => x.id_denunce_contratti)
                .ToListAsync();

            // Oggetto da annulare  - ci deve essere sempre solo un unico ogg. di contrib. ATT-ATT
            join_denunce_oggetti firstJoinDenunceOggetti = joinDenunceOggettiList.FirstOrDefault();
            return firstJoinDenunceOggetti;
        }
        public static async Task<IList<join_denunce_oggetti>> RetrieveListaJoinDenunceOggettiPerAnnullamentoVERS18(
           tab_oggetti_contribuzione oggContDenunciaSelezionato,
           tab_contribuente contrib,
           tab_denunce_contratti denunceContratti,
           dbEnte dbContext)
        {
            decimal salva_id_oggetto_annullato = oggContDenunciaSelezionato.id_oggetto;
            IList<join_denunce_oggetti> joinDenunceOggettiList = await JoinDenunceOggettiBD.GetList(dbContext)
                .Where(x =>
                    !x.cod_stato.StartsWith(CodStato.ANN) &&
                    (
                        (
                            x.id_doc_input != null &&
                            !x.tab_doc_input.cod_stato.StartsWith(CodStato.ANN)
                        )
                        ||
                        (
                            x.id_denunce_contratti!=null &&
                            !x.tab_denunce_contratti.cod_stato.StartsWith(CodStato.ANN) &&
                            x.id_denunce_contratti != denunceContratti.id_tab_denunce_contratti
                        )
                    ) &&
                    x.id_oggetti_contribuzione != null &&

                    x.id_oggetti_contribuzione == oggContDenunciaSelezionato.id_oggetto_contribuzione &&

                    // NOTA: questo manca ma ci vuole:
                    x.tab_oggetti_contribuzione.id_oggetto == salva_id_oggetto_annullato &&
                    // NOTA: questo manca ma ci vuole:
                    !x.tab_oggetti_contribuzione.cod_stato_oggetto.StartsWith(CodStato.ANN)
                )
                //.Include(x => x.tab_oggetti_contribuzione)
                //.Include(x => x.tab_oggetti_contribuzione.tab_oggetti)
                .OrderByDescending(x => x.id_join_denunce_oggetti)
                .ToListAsync();
            return joinDenunceOggettiList;
        }


        // La funzione AnnullaOggettoContribuzione deve essere chiamata a nche da Giulivo, 
        // altrimenti può anche essere spostata fuori da quasta classe (e i metodi possono anche essere async).

        public static void AnnullaOggettoContribuzioneVERS18(
            dbEnte dbContext,
            join_denunce_oggetti joinDenunceOggetti,
            tab_oggetti_contribuzione oggettoDiContribuzione,
            tab_oggetti oggetto,
            // NOTA: se questa fnz deve essere chiamata da Giulivo id_denuncia_contratto
            //       dovrebbe essere nullable e la selezione delle join non dipendere da essa!
            int id_denuncia_contratto_da_annullare,
            int id_denuncia_contratto_corrente,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            decimal id_anag_contribuente, // == oggettoDiContribuzione.id_contribuente
            out string errorMessage)
        {
            if (joinDenunceOggetti.id_causale != null)
            {
                AnnullaOggettoContribuzioneVecchiaProceduraVERS18(
                    dbContext: dbContext,
                    joinDenunceOggetti: joinDenunceOggetti,
                    oggettoDiContribuzione: joinDenunceOggetti.tab_oggetti_contribuzione,
                    oggetto: joinDenunceOggetti.tab_oggetti_contribuzione.tab_oggetti,
                    id_denuncia_contratto_da_annullare: id_denuncia_contratto_da_annullare,
                    id_denuncia_contratto_corrente: id_denuncia_contratto_corrente,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    id_anag_contribuente: id_anag_contribuente,
                    errorMessage: out errorMessage);
                return;
            }

            errorMessage = null;
#if OLD
            IList<join_denunce_oggetti> joinDenunceOggettiList = JoinDenunceOggettiBD.GetList(dbContext)
                .Where(x => x.tab_oggetti_contribuzione.id_oggetto == oggettoDiContribuzione.id_oggetto &&
                            x.tab_oggetti_contribuzione.id_contribuente == id_anag_contribuente)
                .Include(x => x.tab_oggetti_contribuzione)
                .ToList();
#else
            IList<join_denunce_oggetti> joinDenunceOggettiList = JoinDenunceOggettiBD.GetList(dbContext)
                .Where(x => x.id_denunce_contratti == id_denuncia_contratto_da_annullare &&
                            x.id_oggetti_contribuzione != null &&
                            x.tab_oggetti_contribuzione.id_oggetto == oggettoDiContribuzione.id_oggetto &&
                            x.tab_oggetti_contribuzione.id_contribuente == id_anag_contribuente)
                .Include(x => x.tab_oggetti_contribuzione)
                .ToList();
#endif
            foreach (join_denunce_oggetti joinDenuncieOggetti in joinDenunceOggettiList)
            {
                if (joinDenuncieOggetti.cod_stato_oggetto_data_denuncia == null)
                {
                    join_denunce_oggetti joinDenunceOggettiNew = new join_denunce_oggetti();

                    // -------------------------------------------------------------------
                    // Metodo da testare (steppàllo)
                    joinDenunceOggettiNew.setEntityElementaryProperties(joinDenuncieOggetti, dbContext);
                    // -------------------------------------------------------------------

                    joinDenunceOggettiNew.id_denunce_contratti = id_denuncia_contratto_corrente;
                    joinDenunceOggettiNew.cod_stato_oggetto_data_denuncia = anagrafica_stato_oggetto.ANNULLATO;
                    joinDenunceOggettiNew.id_stato_oggetto_data_denuncia = anagrafica_stato_oggetto.ANNULLATO_ID;
                    joinDenunceOggettiNew.data_creazione = dtNow;
                    dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiNew);
                }

                // ---
                // Uso direttamente "joinDenuncieOggetti.tab_oggetti_contribuzione"
                // tab_oggetti_contribuzione oggContDellaJoin = joinDenuncieOggetti.tab_oggetti_contribuzione;
                // decimal salva_id_oggetto = oggContDellaJoin.id_oggetto;
                // ---
                decimal salva_id_oggetto = joinDenuncieOggetti.tab_oggetti_contribuzione.id_oggetto;
                // ---


                if (joinDenuncieOggetti.cod_stato_oggetto_data_denuncia == null)
                {
                    joinDenuncieOggetti.tab_oggetti_contribuzione.cod_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.id_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO_ID;
                }
                else if (0 == String.Compare(joinDenuncieOggetti.cod_stato_oggetto_data_denuncia?.Trim(), anagrafica_stato_oggetto.ATTIVO, StringComparison.InvariantCultureIgnoreCase))
                {

                    joinDenuncieOggetti.tab_oggetti_contribuzione.data_fine_contribuzione = null;
                    // Si mette
                    //  COD_STATO_OGGETTO = Cod_stato_oggetto_data_denuncia ???
                    joinDenuncieOggetti.tab_oggetti_contribuzione.cod_stato_oggetto = joinDenuncieOggetti.cod_stato_oggetto_data_denuncia;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.id_stato_oggetto = joinDenuncieOggetti.id_stato_oggetto_data_denuncia ?? 1;


                    //IList<tab_oggetti_contribuzione> odcSOgAttList = TabOggettiContribuzioneBD.GetList(dbContext)
                    //    .Where(x => x.id_oggetto == salva_id_oggetto &&
                    //            x.id_contribuente == id_anag_contribuente &&
                    //            x.cod_stato_oggetto == anagrafica_stato_oggetto.ATTIVO)
                    //    .ToList();
                    //if (odcSOgAttList.Count > 0)
                    //{
                    //    foreach (var odcSOgAtt in odcSOgAttList)
                    //    {
                    //        DateTime salva_data_inizio_contribuzione = odcSOgAtt.data_inizio_contribuzione;
                    //        odcSOgAtt.data_fine_contribuzione = salva_data_inizio_contribuzione.AddDays(-1);
                    //        odcSOgAtt.cod_stato_oggetto = anagrafica_stato_oggetto.ATT_VAR;
                    //        odcSOgAtt.id_stato_oggetto = anagrafica_stato_oggetto.ATT_VAR_ID;
                    //    }
                    //}
                }
                else // caso cod_stato_oggetto_data_denuncia not NULL && cod_stato_oggetto_data_denuncia != ATT-ATT
                {
                    // Si mette
                    joinDenuncieOggetti.tab_oggetti_contribuzione.cod_stato_oggetto = joinDenuncieOggetti.cod_stato_oggetto_data_denuncia;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.id_stato_oggetto = joinDenuncieOggetti.id_stato_oggetto_data_denuncia ?? 1;
                    // << SENZA CAMBIARE Data_fine_contribuzione >>
                }
            }
            return;
        }

        private static void AnnullaOggettoContribuzioneVecchiaProceduraVERS18(
            dbEnte dbContext,
            join_denunce_oggetti joinDenunceOggetti,
            tab_oggetti_contribuzione oggettoDiContribuzione,
            tab_oggetti oggetto,
            int? id_denuncia_contratto_da_annullare,
            int? id_denuncia_contratto_corrente,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            decimal id_anag_contribuente, // == oggettoDiContribuzione.id_contribuente
            out string errorMessage)
        {
            if (joinDenunceOggetti.id_causale == null)
            {
                throw new Exception("Se id_causale null si deve usare AnnullaOggettoContribuzione e non AnnullaOggettoContribuzioneVecchiaProcedura");
            }
            errorMessage = null;

            // var salvaID_OGGETTO_ANNULLATO = oggettoDiContribuzione.id_oggetto_contribuzione;
            var salvaCOD_STATO_OGGETTO_ANNULLATO = oggettoDiContribuzione.cod_stato_oggetto;
            var salvaID_STATO_OGGETTO_ANNULLATO = oggettoDiContribuzione.id_stato_oggetto;

            var salvaDATA_INIZIO_CONTRIBUZIONE_OGGETTO_ANNULLATO = oggettoDiContribuzione.data_inizio_contribuzione;
            var salvaDATA_FINE_CONTRIBUZIONE_OGGETTO_ANNULLATO = oggettoDiContribuzione.data_fine_contribuzione;
            var salvaID_OGGETTO_OLD = oggettoDiContribuzione.id_ogg_contribuzione_old;

            oggettoDiContribuzione.cod_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO;
            oggettoDiContribuzione.id_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO_ID;
            // data_stato, risorsa, struttura automatici

            join_denunce_oggetti joinDenunceOggettiNewPerVarStessaDataRet = new join_denunce_oggetti
            {
                id_doc_input = null,
                //id_denunce_contratti= id del nuovo tab_denunce_contratti che ancora non ho salvato:
                id_denunce_contratti = id_denuncia_contratto_corrente,

                id_oggetti_contribuzione = oggettoDiContribuzione.id_oggetto_contribuzione,

                // ---------------------
                id_causale = null,
                // ---------------------
                id_risorsa_acquisizione = id_risorsa_acquisizione,
                data_creazione = dtNow,

                num_ordine_den_ici = null,
                prog_num_ordine_den_ici = null,
                annotazioni = null,

                // ---------------------------------------------
                id_stato = anagrafica_stato_denunce.ATT_ATT_ID,
                cod_stato = anagrafica_stato_denunce.ATT_ATT,
                // ---------------------------------------------
                // Già fatto: id_doc_input = null,
                cod_stato_oggetto_data_denuncia = salvaCOD_STATO_OGGETTO_ANNULLATO,
                id_stato_oggetto_data_denuncia = salvaID_STATO_OGGETTO_ANNULLATO
                // SetUserStato:
                //      data_stato
                //      id_struttura_stato
                //      id_risorsa_stato
            };
            dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiNewPerVarStessaDataRet);

            // TRATTAMENTO_OGGETTI_VARIATI 

            tab_oggetti_contribuzione oggettoOld = null;
            if (salvaID_OGGETTO_OLD == null)
            {
                tab_oggetti_contribuzione recuperoVecchioAttivo = TabOggettiContribuzioneBD.GetList(dbContext)
                    .Where(x =>
                        x.id_oggetto_contribuzione == salvaID_OGGETTO_OLD &&
                        !x.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ANN) &&
                        x.data_inizio_contribuzione <= oggettoDiContribuzione.data_inizio_contribuzione &&
                        (
                            x.data_fine_contribuzione == null ||
                            (x.data_fine_contribuzione != null && x.data_fine_contribuzione > oggettoDiContribuzione.data_inizio_contribuzione)
                        )
                        // ---
                        && x.id_oggetto_contribuzione != oggettoDiContribuzione.id_oggetto_contribuzione
                        && x.id_oggetto_contribuzione > 0
                        && x.id_oggetto == oggettoDiContribuzione.id_oggetto
                        && x.id_contribuente == id_anag_contribuente)
                    .FirstOrDefault();
                if (recuperoVecchioAttivo == null) { return; }

                string codStatoOdgDaRecuperare = recuperoVecchioAttivo.cod_stato_oggetto?.Trim().ToUpperInvariant();
                if (codStatoOdgDaRecuperare.StartsWith(anagrafica_stato_oggetto.RET) ||
                    0 == string.Compare(codStatoOdgDaRecuperare, anagrafica_stato_oggetto.ATT_VAR, StringComparison.InvariantCultureIgnoreCase))
                {
                    recuperoVecchioAttivo.cod_stato_oggetto = anagrafica_stato_oggetto.ATTIVO;
                    recuperoVecchioAttivo.id_stato_oggetto = anagrafica_stato_oggetto.ATTIVO_ID;
                }
                
                return;
            }
            
            oggettoOld = TabOggettiContribuzioneBD.GetList(dbContext)
                    .Where(x =>
                        x.id_oggetto_contribuzione == salvaID_OGGETTO_OLD

                        // ---
                        && x.id_oggetto_contribuzione != oggettoDiContribuzione.id_oggetto_contribuzione
                        && x.id_oggetto_contribuzione > 0
                        && x.id_oggetto == oggettoDiContribuzione.id_oggetto
                        && x.id_contribuente == id_anag_contribuente

                    )
                    .SingleOrDefault();
            if (oggettoOld == null)
            {
                return;
            }

            var salva_id_stato_oggetto_old = oggettoOld.id_stato_oggetto;
            var salva_cod_stato_oggetto_old = oggettoOld.cod_stato_oggetto ?? "";


            int salva_id_stato_oggetto_variato = -1;
            string salva_cod_stato_oggetto_variato = "";


            if (salva_cod_stato_oggetto_old.ToUpperInvariant().StartsWith(anagrafica_stato_oggetto.RET))
            {
                TrattamentoOggettiRettificatiVERS18(
                    dbContext: dbContext,
                    joinDenunceOggetti: joinDenunceOggetti,
                    oggettoDiContribuzione: joinDenunceOggetti.tab_oggetti_contribuzione,
                    oggettoDiContribuzioneRettificato: oggettoOld,
                    oggetto: joinDenunceOggetti.tab_oggetti_contribuzione.tab_oggetti,
                    id_denuncia_contratto: id_denuncia_contratto_corrente,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    id_anag_contribuente: id_anag_contribuente,
                    errorMessage: out errorMessage);
                return;

            }
            else if (oggettoOld.data_cessazione_ogg_contribuzione != null)
            {
                oggettoOld.cod_stato_oggetto = anagrafica_stato_oggetto.CESSATO;
                oggettoOld.id_stato_oggetto = anagrafica_stato_oggetto.CESSATO_ID;
                oggettoOld.data_fine_contribuzione = oggettoDiContribuzione.data_cessazione_ogg_contribuzione;

                salva_id_stato_oggetto_variato = anagrafica_stato_oggetto.CESSATO_ID;
                salva_cod_stato_oggetto_variato = anagrafica_stato_oggetto.CESSATO;
            }
            else // data_cessazione_ogg_contribuzione == null
            {
                oggettoOld.cod_stato_oggetto = anagrafica_stato_oggetto.ATTIVO;
                oggettoOld.id_stato_oggetto = anagrafica_stato_oggetto.ATTIVO_ID;
                oggettoOld.data_fine_contribuzione = null;

                salva_id_stato_oggetto_variato = anagrafica_stato_oggetto.ATTIVO_ID;
                salva_cod_stato_oggetto_variato = anagrafica_stato_oggetto.ATTIVO;
            }

            if (salva_id_stato_oggetto_variato != -1)
            {
                join_denunce_oggetti joinDenunceOggettiNewVar = new join_denunce_oggetti
                {
                    id_doc_input = null,
                    //id_denunce_contratti= id del nuovo tab_denunce_contratti che ancora non ho salvato:
                    id_denunce_contratti = id_denuncia_contratto_corrente,

                    id_oggetti_contribuzione = oggettoDiContribuzione.id_oggetto_contribuzione,

                    // ---------------------
                    id_causale = null,
                    // ---------------------
                    id_risorsa_acquisizione = id_risorsa_acquisizione,
                    data_creazione = dtNow,

                    num_ordine_den_ici = null,
                    prog_num_ordine_den_ici = null,
                    annotazioni = null,

                    // ---------------------------------------------
                    id_stato = anagrafica_stato_denunce.ATT_ATT_ID,
                    cod_stato = anagrafica_stato_denunce.ATT_ATT,
                    // ---------------------------------------------
                    // Già fatto: id_doc_input = null,
                    cod_stato_oggetto_data_denuncia = salva_cod_stato_oggetto_old,
                    id_stato_oggetto_data_denuncia = salva_id_stato_oggetto_old
                    // SetUserStato:
                    //      data_stato
                    //      id_struttura_stato
                    //      id_risorsa_stato
                };
                dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiNewVar);
            }

        }

        private static void TrattamentoOggettiRettificatiVERS18(
            dbEnte dbContext,
            join_denunce_oggetti joinDenunceOggetti,
            tab_oggetti_contribuzione oggettoDiContribuzione,
            tab_oggetti_contribuzione oggettoDiContribuzioneRettificato,
            tab_oggetti oggetto,
            int? id_denuncia_contratto,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            decimal id_anag_contribuente, // == oggettoDiContribuzione.id_contribuente
            out string errorMessage)
        {
            errorMessage = null;

            // VERS18 pag 107
            // Nota bene : 
            // se Infedele denuncia si devono ripristinare i record di Tab_oggetti_contribuzione messi in COD-STATO_oggetto = RET-

            decimal salva_id_oggetto_ret = oggettoDiContribuzioneRettificato.id_oggetto_contribuzione;
            var salva_id_stato_oggetto_ret = oggettoDiContribuzioneRettificato.id_stato_oggetto;
            var salva_cod_stato_oggetto_ret = (oggettoDiContribuzioneRettificato.cod_stato_oggetto ?? "").Trim();

            //string nuovoCodStatoOggContRettificato = String.Concat("ATT-", StringHelpers.SafeRight(salva_cod_stato_oggetto_ret, 3));
            //oggettoDiContribuzioneRettificato.cod_stato_oggetto = nuovoCodStatoOggContRettificato;
            //oggettoDiContribuzioneRettificato.id_stato_oggetto = anagrafica_stato_oggetto.FindIdStatoOggetto(nuovoCodStatoOggContRettificato, false, 1);

            if (0 == string.Compare(salva_cod_stato_oggetto_ret, anagrafica_stato_oggetto.ATTIVO, StringComparison.OrdinalIgnoreCase))
            {
                oggettoDiContribuzioneRettificato.cod_stato_oggetto = anagrafica_stato_oggetto.ATTIVO;
                oggettoDiContribuzioneRettificato.id_stato_oggetto = anagrafica_stato_oggetto.ATTIVO_ID;

                join_denunce_oggetti joinDenunceOggettiRet = new join_denunce_oggetti
                {
                    id_doc_input = null,
                    //id_denunce_contratti= id del nuovo tab_denunce_contratti che ancora non ho salvato:
                    id_denunce_contratti = id_denuncia_contratto,

                    id_oggetti_contribuzione = salva_id_oggetto_ret,

                    // ---------------------
                    id_causale = null,
                    // ---------------------
                    id_risorsa_acquisizione = id_risorsa_acquisizione,
                    data_creazione = dtNow,

                    num_ordine_den_ici = null,
                    prog_num_ordine_den_ici = null,
                    annotazioni = null,

                    // ---------------------------------------------
                    id_stato = anagrafica_stato_denunce.ATT_ATT_ID,
                    cod_stato = anagrafica_stato_denunce.ATT_ATT,
                    // ---------------------------------------------
                    // Già fatto: id_doc_input = null,
                    cod_stato_oggetto_data_denuncia = salva_cod_stato_oggetto_ret,
                    id_stato_oggetto_data_denuncia = salva_id_stato_oggetto_ret
                    // SetUserStato:
                    //      data_stato
                    //      id_struttura_stato
                    //      id_risorsa_stato
                };
                dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiRet);
            }
            else if (0 == string.Compare(salva_cod_stato_oggetto_ret, anagrafica_stato_oggetto.RET_VAR, StringComparison.OrdinalIgnoreCase))
            {
                IList<tab_oggetti_contribuzione> oggContRetList = TabOggettiContribuzioneBD.GetList(dbContext)
                    .Where(x =>
                              // ---
                              x.id_oggetto == oggetto.id_oggetto
                              && x.cod_stato_oggetto.EndsWith("-ATT")
                              && x.cod_stato_oggetto != anagrafica_stato_oggetto.ATTIVO
                              && x.cod_stato_oggetto != "ANN-ATT"
                              // ---
                              && x.id_oggetto_contribuzione != oggettoDiContribuzione.id_oggetto_contribuzione
                              && x.id_oggetto_contribuzione > 0
                              && x.id_oggetto == oggettoDiContribuzione.id_oggetto
                              && x.id_contribuente == id_anag_contribuente
                          )
                    .OrderByDescending(x => x.id_oggetto_contribuzione)
                    .ToList();
                if (oggContRetList.Count == 0)
                {
                    errorMessage =
                            "Incongruenza dati - Oggetto contribuzione derivante " +
                            "denuncia senza oggetti contribuzione rettificati attivi";
                    return;
                }
                var odcOldAttivo = oggContRetList[0];
                odcOldAttivo.cod_stato_oggetto = anagrafica_stato_oggetto.ATTIVO;
                odcOldAttivo.id_stato_oggetto = anagrafica_stato_oggetto.ATTIVO_ID;

                join_denunce_oggetti joinDenunceOggettiRet = new join_denunce_oggetti
                {
                    id_doc_input = null,
                    //id_denunce_contratti= id del nuovo tab_denunce_contratti che ancora non ho salvato:
                    id_denunce_contratti = id_denuncia_contratto,

                    id_oggetti_contribuzione = odcOldAttivo.id_oggetto_contribuzione,

                    // ---------------------
                    id_causale = null,
                    // ---------------------
                    id_risorsa_acquisizione = id_risorsa_acquisizione,
                    data_creazione = dtNow,

                    num_ordine_den_ici = null,
                    prog_num_ordine_den_ici = null,
                    annotazioni = null,

                    // ---------------------------------------------
                    id_stato = anagrafica_stato_denunce.ATT_ATT_ID,
                    cod_stato = anagrafica_stato_denunce.ATT_ATT,
                    // ---------------------------------------------
                    // Già fatto: id_doc_input = null,
                    cod_stato_oggetto_data_denuncia = odcOldAttivo.cod_stato_oggetto,
                    id_stato_oggetto_data_denuncia = odcOldAttivo.id_stato_oggetto
                    // SetUserStato:
                    //      data_stato
                    //      id_struttura_stato
                    //      id_risorsa_stato
                };
                dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiRet);
            }
            else
            {
                errorMessage = "Impossibile annullare l'oggetto di contribuzione";
            }
        } // TrattamentoOggettiRettificatiVERS18

    } // class
}
