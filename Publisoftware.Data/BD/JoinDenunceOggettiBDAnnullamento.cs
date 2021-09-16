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
        public static async Task<IList<join_denunce_oggetti>> RetrieveListaJoinDenunceOggettiPerAnnullamento(
           tab_oggetti_contribuzione oggContDenunciaSelezionato,
           tab_contribuente contrib,
           tab_denunce_contratti denunceContratti,
           dbEnte dbContext)
        {
            decimal salva_id_oggetto_annullato = oggContDenunciaSelezionato.id_oggetto;
            IList<join_denunce_oggetti> joinDenunceOggettiList = await JoinDenunceOggettiBD.GetList(dbContext)
                .Where(x =>
                    (x.cod_stato==null || !x.cod_stato.StartsWith(CodStato.ANN)) &&
                    (
                        (
                            x.id_doc_input != null &&
                            !x.tab_doc_input.cod_stato.StartsWith(CodStato.ANN)
                        )
                        ||
                        (
                            x.id_denunce_contratti != null &&
                            !x.tab_denunce_contratti.cod_stato.StartsWith(CodStato.ANN) &&
                            x.id_denunce_contratti != denunceContratti.id_tab_denunce_contratti
                        )
                    ) &&
                    x.id_oggetti_contribuzione != null &&
                    x.id_oggetti_contribuzione == oggContDenunciaSelezionato.id_oggetto_contribuzione &&
                    // enforcing:
                    x.tab_oggetti_contribuzione.id_contribuente == contrib.id_anag_contribuente
                )
                //.Include(x => x.tab_oggetti_contribuzione)
                //.Include(x => x.tab_oggetti_contribuzione.tab_oggetti)
                .OrderByDescending(x => x.id_join_denunce_oggetti)
                .ToListAsync();
            return joinDenunceOggettiList;
        }


        // La funzione AnnullaOggettoContribuzione deve essere chiamata a nche da Giulivo, 
        // altrimenti può anche essere spostata fuori da quasta classe (e i metodi possono anche essere async).

        public static void AnnullaOggettoContribuzione(
            dbEnte dbContext,
            join_denunce_oggetti joinDenunceOggetti,
            tab_oggetti_contribuzione oggettoDiContribuzioneDaAnnullareSelezionato,
            int? id_denuncia_contratto_corrente,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            decimal id_anag_contribuente, // == oggettoDiContribuzione.id_contribuente
            bool bConsideraOggettiRettificati,
            out string errorMessage)
        {
            if (joinDenunceOggetti == null)
            {
                AnnullaOggettoContribuzioneVecchiaProcedura(
                    dbContext: dbContext,
                    oggettoDiContribuzioneDaAnnullareSelezionato: oggettoDiContribuzioneDaAnnullareSelezionato,
                    id_denuncia_contratto_corrente: id_denuncia_contratto_corrente,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    bConsideraOggettiRettificati: bConsideraOggettiRettificati,
                    errorMessage: out errorMessage);
                return;
            }

            int? salva_id_denuncia = joinDenunceOggetti.id_denunce_contratti; //id denuncia del record selezionato (id_denuncia passata in input)
            int? salva_id_doc_input = joinDenunceOggetti.id_doc_input; //id_doc_input del record selezionato (id_doc_input passato in input)

            tab_oggetti oggettoSelezionato = oggettoDiContribuzioneDaAnnullareSelezionato.tab_oggetti;

            decimal salva_id_oggetto_contribuzione_annullato = oggettoDiContribuzioneDaAnnullareSelezionato.id_oggetto_contribuzione;

            var salva_id_stato_oggetto_annullato = oggettoDiContribuzioneDaAnnullareSelezionato.id_stato_oggetto;
            var salva_cod_stato_oggetto_annullato = oggettoDiContribuzioneDaAnnullareSelezionato.cod_stato_oggetto;
            var salva_data_inizio_contribuzione_oggetto_annullato = oggettoDiContribuzioneDaAnnullareSelezionato.data_inizio_contribuzione;
            var salva_data_fine_contribuzione_oggetto_annullato = oggettoDiContribuzioneDaAnnullareSelezionato.data_fine_contribuzione;
            var salva_id_oggetto_old = oggettoDiContribuzioneDaAnnullareSelezionato.id_ogg_contribuzione_old;

            oggettoDiContribuzioneDaAnnullareSelezionato.cod_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO;
            oggettoDiContribuzioneDaAnnullareSelezionato.id_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO_ID;
            oggettoDiContribuzioneDaAnnullareSelezionato.data_stato = dtNow;
            // data_stato, id_risorsa_stato, id_struttura_stato automatici

            join_denunce_oggetti joinDenunceOggettiAnnulalto = new join_denunce_oggetti
            {
                id_doc_input = null,
                id_denunce_contratti = id_denuncia_contratto_corrente,

                id_oggetti_contribuzione = salva_id_oggetto_contribuzione_annullato,

                // ---------------------
                id_causale = null,
                // ---------------------

                id_risorsa_acquisizione = id_risorsa_acquisizione,
                data_creazione = dtNow,

                num_ordine_den_ici = null,
                prog_num_ordine_den_ici = null,
                annotazioni = null,

                // ---------------------------------------------
                // N.B.: in VERS 18 usavo "anagrafica_stato_oggetto"
                id_stato = anagrafica_stato_denunce.ATT_ATT_ID,
                cod_stato = anagrafica_stato_denunce.ATT_ATT,
                // ---------------------------------------------
                // Già fatto: id_doc_input = null,
                cod_stato_oggetto_data_denuncia = salva_cod_stato_oggetto_annullato,
                id_stato_oggetto_data_denuncia = salva_id_stato_oggetto_annullato
                // automatici (SetUserStato):
                //      data_stato
                //      id_struttura_stato
                //      id_risorsa_stato
            };
            dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiAnnulalto);


            if (joinDenunceOggetti.id_causale != null)
            {
                AnnullaOggettoContribuzioneVecchiaProcedura(
                    dbContext: dbContext,
                    oggettoDiContribuzioneDaAnnullareSelezionato: oggettoDiContribuzioneDaAnnullareSelezionato,
                    id_denuncia_contratto_corrente: id_denuncia_contratto_corrente,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    bConsideraOggettiRettificati: bConsideraOggettiRettificati,
                    errorMessage: out errorMessage);
                return;
            }

            errorMessage = null;

            // ANNULLAMENTO_OGGETTO_CONTRIBUZIONE_NUOVA_PROCEDURA

            IList<join_denunce_oggetti> joinDenunceOggettiList = JoinDenunceOggettiBD.GetList(dbContext)
                .Where(x => x.id_denunce_contratti == salva_id_denuncia &&
                            x.id_doc_input == salva_id_doc_input &&
                            x.id_oggetti_contribuzione != null &&
                            x.tab_oggetti_contribuzione.id_oggetto == oggettoDiContribuzioneDaAnnullareSelezionato.id_oggetto &&
                            x.tab_oggetti_contribuzione.id_contribuente == id_anag_contribuente)
                .Include(x => x.tab_oggetti_contribuzione)
                .ToList();

            // LOOP_RECORD_DENUNCE_OGGETTI
            foreach (join_denunce_oggetti joinDenuncieOggetti in joinDenunceOggettiList)
            {
                // tab_oggetti_contribuzione odgDellaJoin = joinDenuncieOggetti.tab_oggetti_contribuzione;
                decimal salva_id_oggetto = joinDenuncieOggetti.tab_oggetti_contribuzione.id_oggetto;

                if (joinDenuncieOggetti.cod_stato_oggetto_data_denuncia == null)
                {
                    joinDenuncieOggetti.tab_oggetti_contribuzione.cod_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.id_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO_ID;
                }
                else if (0 == String.Compare(joinDenuncieOggetti.cod_stato_oggetto_data_denuncia?.Trim(), anagrafica_stato_oggetto.ATTIVO, StringComparison.InvariantCultureIgnoreCase))
                {

                    joinDenuncieOggetti.tab_oggetti_contribuzione.data_fine_contribuzione = null;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.cod_stato_oggetto = joinDenuncieOggetti.cod_stato_oggetto_data_denuncia;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.id_stato_oggetto = joinDenuncieOggetti.id_stato_oggetto_data_denuncia ?? 1;
                }
                else // caso cod_stato_oggetto_data_denuncia not NULL && cod_stato_oggetto_data_denuncia != ATT-ATT
                {
                    joinDenuncieOggetti.tab_oggetti_contribuzione.cod_stato_oggetto = joinDenuncieOggetti.cod_stato_oggetto_data_denuncia;
                    joinDenuncieOggetti.tab_oggetti_contribuzione.id_stato_oggetto = joinDenuncieOggetti.id_stato_oggetto_data_denuncia ?? 1;
                    // << SENZA CAMBIARE Data_fine_contribuzione >> (cambiata invece nel caso precedente)
                }
                // var currState = dbContext.Entry(joinDenunceOggetti).State;
                // var currStateOdg = dbContext.Entry(joinDenunceOggetti.tab_oggetti_contribuzione).State;
                // dbContext.Entry(joinDenunceOggetti).State = EntityState.Modified;
            }
            return;
        }

        private static void AnnullaOggettoContribuzioneVecchiaProcedura(
            dbEnte dbContext,
            // join_denunce_oggetti joinDenunceOggetti, // ATTENZIONE! joinDenunceOggetti può essere null
            tab_oggetti_contribuzione oggettoDiContribuzioneDaAnnullareSelezionato,
            int? id_denuncia_contratto_corrente,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            bool bConsideraOggettiRettificati,
            out string errorMessage)
        {
            decimal id_anag_contribuente = oggettoDiContribuzioneDaAnnullareSelezionato.id_contribuente;

            //if (joinDenunceOggetti!=null && joinDenunceOggetti.id_causale == null)
            //{
            //    throw new Exception("Se id_causale null si deve usare AnnullaOggettoContribuzione e non AnnullaOggettoContribuzioneVecchiaProcedura");
            //}

            // ----------------------------------------------------------------------
            // N.B.: bConsideraOggettiRettificati è true per TARI, false per IMU
            // ---
            // caso Oggetto di ontribuzione proveniente da Infedele denuncia TARI
            //      cioè
            //         - flag_accertamento=="2" con nuova procedura,
            //         - flag_allaciamento_fognatura =="2" con vecchia procedura
            // (qui stiamo considerando vecchia procedura quindi potresti trascurare "flag_accertamento",
            //  ma lo consideriamo lo stesso che non inficia la procedura)
            // ---
            // N.B.: bConsideraOggettiRettificati è true per TARI, false per IMU
            if (bConsideraOggettiRettificati &&
                0 == string.Compare(oggettoDiContribuzioneDaAnnullareSelezionato.flag_allaciamento_fognatura?.Trim(), "2", StringComparison.InvariantCultureIgnoreCase) ||
                // In realtà "flag-accertamento lo facciamo con la nuova procedura, la vecchia usave solo flag_allaciamento_fognatura"
                0 == string.Compare(oggettoDiContribuzioneDaAnnullareSelezionato.flag_accertamento?.Trim(), "2", StringComparison.InvariantCultureIgnoreCase))
            {
                TrattamentoOggettiRettificati(
                    dbContext: dbContext,
                    oggettoDiContribuzioneDaAnnullareSelezionato: oggettoDiContribuzioneDaAnnullareSelezionato,
                    id_denuncia_contratto: id_denuncia_contratto_corrente,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    id_anag_contribuente: id_anag_contribuente,
                    errorMessage: out errorMessage);
                return;
            }

            errorMessage = null;

            DateTime dtFineContribuzione = oggettoDiContribuzioneDaAnnullareSelezionato.data_inizio_contribuzione.AddDays(-1);
            tab_oggetti_contribuzione odgVar = TabOggettiContribuzioneBD.GetList(dbContext)
                .Where(x =>
                            x.id_oggetto == oggettoDiContribuzioneDaAnnullareSelezionato.id_oggetto &&
                            x.cod_stato_oggetto == anagrafica_stato_oggetto.ATT_VAR &&
                            x.data_inizio_contribuzione <= oggettoDiContribuzioneDaAnnullareSelezionato.data_inizio_contribuzione &&
                            (
                                x.data_fine_contribuzione != null ||
                                x.data_fine_contribuzione == dtFineContribuzione
                            ) &&
                            // Enforcing:
                            x.id_contribuente == oggettoDiContribuzioneDaAnnullareSelezionato.id_contribuente
                        ).FirstOrDefault();
            if (odgVar == null)
            {
                TrattamentoOggettiRettificati(
                    dbContext: dbContext,
                    oggettoDiContribuzioneDaAnnullareSelezionato: oggettoDiContribuzioneDaAnnullareSelezionato,
                    id_denuncia_contratto: id_denuncia_contratto_corrente,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    id_anag_contribuente: id_anag_contribuente,
                    errorMessage: out errorMessage);
                return;
            }


            int salva_id_stato_oggetto_variato = odgVar.id_stato_oggetto;
            string salva_cod_stato_oggetto_variato = odgVar.cod_stato_oggetto;
            decimal salva_id_oggetto_contribuzione_variato = odgVar.id_oggetto_contribuzione;

            odgVar.cod_stato_oggetto = anagrafica_stato_oggetto.ATTIVO;
            odgVar.id_stato_oggetto = anagrafica_stato_oggetto.ATTIVO_ID;
            odgVar.data_fine_contribuzione = null;


            join_denunce_oggetti joinDenunceOggettiExVar = new join_denunce_oggetti
            {
                id_doc_input = null,
                //id_denunce_contratti= id del nuovo tab_denunce_contratti che ancora non ho salvato:
                id_denunce_contratti = id_denuncia_contratto_corrente,

                // "SALVA_ID_OGGETTO_CONTRIBUZIONE" è dell'oggetto in joinDenunceOggetti!!! Secondo me dovrebbe essere odgVar.id_oggetto_contribuzione
                id_oggetti_contribuzione = salva_id_oggetto_contribuzione_variato, //odgVar.id_oggetto_contribuzione,

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
                cod_stato_oggetto_data_denuncia = salva_cod_stato_oggetto_variato,
                id_stato_oggetto_data_denuncia = salva_id_stato_oggetto_variato
                // SetUserStato:
                //      data_stato
                //      id_struttura_stato
                //      id_risorsa_stato
            };
            dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiExVar);
        }

        private static void TrattamentoOggettiRettificati(
            dbEnte dbContext,
            tab_oggetti_contribuzione oggettoDiContribuzioneDaAnnullareSelezionato,
            int? id_denuncia_contratto,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            decimal id_anag_contribuente, // == oggettoDiContribuzione.id_contribuente
            out string errorMessage)
        {
            errorMessage = null;


            // Nota bene : 
            // se Infedele denuncia si devono ripristinare i record di Tab_oggetti_contribuzione messi in COD-STATO_oggetto = RET- 


            // NOTAsSULLA QUERY SEGUENTE:
            //       in documentazione è scritto "ordine crescente",
            //       ma abbiamo corretto in ordine decrescente che vogliamo finire con il più vecchio
            //
            // Spiegazione:
            //
            //
            //   Se abbiamo dei RET:
            //          RET                 RET           RET
            //   *----------------------*---------*--------------------------------*
            //
            //   Poi è stato modificato un ret da un certo punto in poi, ciò che c'era prima
            //   è stato annullato e dobbiamo riportarlo in vita:
            //  
            //    ANN       ATT
            //   *----*------------------------------------------------------------*
            //
            //
            //
            IList<tab_oggetti_contribuzione> odgRetList = TabOggettiContribuzioneBD.GetList(dbContext)
                   .Where(x =>
                             // ---
                             x.cod_stato_oggetto.StartsWith(CodStato.RET) &&
                             x.id_oggetto == oggettoDiContribuzioneDaAnnullareSelezionato.id_oggetto
                             // ---
                             && (
                                x.data_fine_contribuzione == null || x.data_fine_contribuzione > oggettoDiContribuzioneDaAnnullareSelezionato.data_inizio_contribuzione
                             )
                             && x.id_contribuente == id_anag_contribuente
                         )
                   .OrderByDescending(x => x.data_inizio_contribuzione)
                   .ToList();

            if (odgRetList.Count > 0)
            {
                // Qui ci sarà l'ultimo del loop (il più vecchio);
                decimal salva_id_oggetto_ret = 0;
                // Qui ci sarà l'ultimo del loop  (il più vecchio):
                DateTime salva_data_inizio_oggetto_ret = dtNow;

                foreach (var odg in odgRetList)
                {
                    salva_id_oggetto_ret = odg.id_oggetto_contribuzione; // id_oggetto_contribuzione letto
                    salva_data_inizio_oggetto_ret = odg.data_inizio_contribuzione; // data_inizio_contribuzione del record letto

                    var salva_id_stato_oggetto_ret = odg.id_stato_oggetto;   // id_stato del record di tab_oggetti_contribuzione
                    var salva_cod_stato_oggetto_ret = odg.cod_stato_oggetto?.Trim().ToUpperInvariant(); // cod_stato del record di tab_oggetti_contribuzione

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

                    if (0 == string.Compare(salva_cod_stato_oggetto_ret, anagrafica_stato_oggetto.RET_VAR))
                    {
                        odg.cod_stato_oggetto = anagrafica_stato_oggetto.ATT_VAR;
                        odg.id_stato_oggetto = anagrafica_stato_oggetto.ATT_VAR_ID;
                    }
                    else if (0 == string.Compare(salva_cod_stato_oggetto_ret, anagrafica_stato_oggetto.RET_CES))
                    {
                        odg.cod_stato_oggetto = anagrafica_stato_oggetto.CESSATO;
                        odg.id_stato_oggetto = anagrafica_stato_oggetto.CESSATO_ID;
                    }
                    else if (0 == string.Compare(salva_cod_stato_oggetto_ret, anagrafica_stato_oggetto.RET_ATT))
                    {
                        odg.cod_stato_oggetto = anagrafica_stato_oggetto.ATTIVO;
                        odg.id_stato_oggetto = anagrafica_stato_oggetto.ATTIVO_ID;
                        odg.data_fine_contribuzione = null;
                    }
                }
                TrattamentoOggettiVariati(
                    dbContext: dbContext,
                    oggettoDiContribuzioneDaAnnullareSelezionato: oggettoDiContribuzioneDaAnnullareSelezionato,
                     id_denuncia_contratto: id_denuncia_contratto,
                    id_risorsa_acquisizione: id_risorsa_acquisizione,
                    dtNow: dtNow,
                    id_anag_contribuente: id_anag_contribuente, // == oggettoDiContribuzione.id_contribuente
                    salva_id_oggetto_ret: salva_id_oggetto_ret,
                    salva_data_inizio_oggetto_ret: salva_data_inizio_oggetto_ret,
                    errorMessage: out errorMessage);
            }
        } // TrattamentoOggettiRettificati

        private static void TrattamentoOggettiVariati(
            dbEnte dbContext,
            tab_oggetti_contribuzione oggettoDiContribuzioneDaAnnullareSelezionato,
            int? id_denuncia_contratto,
            int id_risorsa_acquisizione,
            DateTime dtNow,
            decimal id_anag_contribuente,
            decimal salva_id_oggetto_ret,
            DateTime salva_data_inizio_oggetto_ret,
            out string errorMessage)
        {
            errorMessage = null;

            var odgAttVar = TabOggettiContribuzioneBD.GetList(dbContext)
               .Where(x =>
                         // ---
                         x.id_oggetto == oggettoDiContribuzioneDaAnnullareSelezionato.id_oggetto
                         && x.cod_stato_oggetto == anagrafica_stato_oggetto.ATT_VAR
                          // ---
                          && (
                             x.data_inizio_contribuzione == salva_data_inizio_oggetto_ret
                          )
                         && x.id_contribuente == id_anag_contribuente
                     )
               // ----- da dicumentazione ce n'è solo uno, comunque
               //       se così non fosse prendo il più "recente" 
               .OrderByDescending(x => x.data_inizio_contribuzione)
               .FirstOrDefault();
            if (odgAttVar == null)
            {
                return;
            }
            var salva_id_oggetto_variato = odgAttVar.id_oggetto; // id_oggetto_contribuzione letto
            var salva_id_stato_oggetto_variato = odgAttVar.id_stato_oggetto; // id_stato del record di tab_oggetti_contribuzione
            var salva_cod_stato_oggetto_variato = odgAttVar.cod_stato_oggetto; // cod_stato del record di tab_oggetti_contribuzione

            odgAttVar.cod_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO;
            odgAttVar.id_stato_oggetto = anagrafica_stato_oggetto.ANNULLATO_ID;
            odgAttVar.data_fine_contribuzione = null;

            join_denunce_oggetti joinDenunceOggettiAttVar = new join_denunce_oggetti
            {
                id_doc_input = null,
                //id_denunce_contratti= id del nuovo tab_denunce_contratti che ancora non ho salvato:
                id_denunce_contratti = id_denuncia_contratto,

                id_oggetti_contribuzione = salva_id_oggetto_variato,

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
                cod_stato_oggetto_data_denuncia = salva_cod_stato_oggetto_variato,
                id_stato_oggetto_data_denuncia = salva_id_stato_oggetto_variato
                // SetUserStato:
                //      data_stato
                //      id_struttura_stato
                //      id_risorsa_stato
            };
            dbContext.Set<join_denunce_oggetti>().Add(joinDenunceOggettiAttVar);
        }
    } // class
}
