using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using Publisoftware.Utility.Log;
using System.Transactions;
using System.Data.SqlClient;

namespace Publisoftware.Data.BD
{
    public class TabAvvPagFattEmissioneBD : EntityBD<tab_avv_pag_fatt_emissione>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("TabAvvPagFattEmissioneBD");

        public TabAvvPagFattEmissioneBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_avv_pag_fatt_emissione> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente));
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> GetListByListaEmissione(int p_idLista, dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(afe => afe.id_lista_emissione == p_idLista);
        }
        public static IQueryable<tab_avv_pag_fatt_emissione> GetListByListaEmissioneAndCodStato(int p_idLista,string p_stato, dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(afe => afe.id_lista_emissione == p_idLista && afe.cod_stato==p_stato);
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_avv_pag_fatt_emissione GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_avv_pag == p_id);
        }

        /// <summary>
        /// Funzione di aggiornamento dei totali della tab_avv_pag_fatt_emissione
        /// </summary>
        /// <param name="p_Avviso"></param>
        public static void aggiornaAvvPagFattEmissioneOLD(tab_avv_pag_fatt_emissione p_Avviso)
        {
            decimal v_importoUnitaContribuzione = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.importo_unita_contribuzione).Sum();

            p_Avviso.imp_tot_avvpag = v_importoUnitaContribuzione;
            p_Avviso.importo_tot_da_pagare = v_importoUnitaContribuzione;
            p_Avviso.imp_tot_pagato = 0;
            p_Avviso.imp_tot_avvpag_rid = v_importoUnitaContribuzione;
            p_Avviso.importo_ridotto = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.importo_ridotto).Sum();
            p_Avviso.importo_sanzioni_eliminate_eredi = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.importo_sanzioni_eliminate_eredi).Sum();
            p_Avviso.imp_imp_entr_avvpag = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.imponibile_unita_contribuzione).Sum();
            p_Avviso.imp_iva_avvpag = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.iva_unita_contribuzione).Sum();
        }

        public static void aggiornaAvvPagFattEmissione(tab_avv_pag_fatt_emissione p_Avviso, dbEnte p_context)
        {
            decimal v_importoUnitaContribuzione = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.importo_unita_contribuzione).Sum();

            p_Avviso.imp_tot_avvpag = v_importoUnitaContribuzione;
            p_Avviso.importo_tot_da_pagare = v_importoUnitaContribuzione;
            p_Avviso.imp_tot_pagato = 0;
            p_Avviso.imp_tot_avvpag_rid = v_importoUnitaContribuzione;
            p_Avviso.importo_ridotto = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.importo_ridotto).Sum();
            p_Avviso.importo_sanzioni_eliminate_eredi = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.importo_sanzioni_eliminate_eredi).Sum();
            p_Avviso.imp_imp_entr_avvpag = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.imponibile_unita_contribuzione).Sum();
            p_Avviso.imp_iva_avvpag = p_Avviso.tab_unita_contribuzione_fatt_emissione.Select(u => u.iva_unita_contribuzione).Sum();

            //tutti i tipi_servizio cautelari o esecutivi
            if (p_Avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                p_Avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||                
                p_Avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                p_Avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                p_Avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI)
            {
                //modificato 30/05/19 dopo chiacchierata con Gennaro
                //decimal? v_importo_iscrizione_ruolo = p_Avviso.tab_unita_contribuzione_fatt_emissione
                //                                              .Where(u => u.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ID_AVVISO_COLLEGATO)
                //                                              .Select(u => u.importo_unitario_contribuzione).Sum();
                decimal? v_importo_iscrizione_ruolo = p_Avviso.imp_tot_avvpag;

                decimal ? v_percentuale_incremento_iscrizione_ruolo = TabModalitaRateAvvPagViewBD.GetByIdTipoAvvPagAndIdEnte(p_Avviso.id_tipo_avvpag, p_Avviso.id_ente, p_context).percentuale_inc_iscrizione_ruolo;                
                decimal v_perc = v_percentuale_incremento_iscrizione_ruolo.HasValue ? v_percentuale_incremento_iscrizione_ruolo.Value : 0;
                if(v_perc > 0)
                {
                    v_importo_iscrizione_ruolo = v_importo_iscrizione_ruolo + (v_importo_iscrizione_ruolo * (v_perc / 100));
                }
                else
                {
                    v_importo_iscrizione_ruolo = 0;
                }
                p_Avviso.importo_iscrizione_ruolo = v_importo_iscrizione_ruolo;                
            }

        }


        /// <summary>
        /// Funzione di arrotondamento della tab_avv_pag_fatt_emissione
        /// </summary>
        /// <param name="p_Avviso"></param>
        /// <param name="p_numRiga"></param>
        public static void arrotondaAvvPagFattEmissione(tab_avv_pag_fatt_emissione p_Avviso, int p_numRiga)
        {
            //restituisco l'importo con due cifre decimali
            decimal v_importoAvviso = p_Avviso.imp_tot_avvpag.HasValue ? p_Avviso.imp_tot_avvpag.Value : 0;
            decimal v_importoAvvisoArrotondato = p_Avviso.imp_tot_avvpag.HasValue ? p_Avviso.imp_tot_avvpag.Value.Arrotonda() : 0;
            decimal v_importoArrotondamento = v_importoAvviso - v_importoAvvisoArrotondato;

            p_Avviso.imp_tot_avvpag = v_importoAvvisoArrotondato;
            p_Avviso.imp_tot_avvpag_rid = v_importoAvvisoArrotondato;

            string v_flag_segno = tab_unita_contribuzione_fatt_emissione.FLAG_SEGNO_NEGATIVO;
            if (v_importoArrotondamento < 0)
            {
                v_flag_segno = tab_unita_contribuzione_fatt_emissione.FLAG_SEGNO_POSITVO;
            }

            tab_unita_contribuzione_fatt_emissione v_unitaContrFattEmissioneArrotondamento = new tab_unita_contribuzione_fatt_emissione()
            {
                id_ente = p_Avviso.id_ente,
                id_ente_gestito = p_Avviso.id_ente_gestito,
                id_entrata = p_Avviso.id_entrata,
                id_tipo_avv_pag_generato = p_Avviso.id_tipo_avvpag,
                num_riga_avv_pag_generato = p_numRiga,
                id_anagrafica_voce_contribuzione = anagrafica_voci_contribuzione.ID_ARROTONDAMENTO,
                id_tipo_voce_contribuzione = tab_tipo_voce_contribuzione.ID_ARROTONDAMENTO,
                flag_tipo_addebito = tab_unita_contribuzione_fatt_emissione.FLAG_TIPO_ADDEBITO_NORMALE,
                anno_rif = p_Avviso.anno_riferimento,
                id_contribuente = p_Avviso.id_anag_contribuente,
                id_avv_pag_collegato = p_Avviso.id_tab_avv_pag,
                flag_collegamento_unita_contribuzione = tab_unita_contribuzione_fatt_emissione.FLAG_UNITA_COLLEGATA_NO,
                um_unita = "€",
                flag_segno = v_flag_segno,
                quantita_unita_contribuzione = 1,
                importo_unitario_contribuzione = 1,
                importo_unita_contribuzione = Math.Abs(v_importoArrotondamento),
                imponibile_unita_contribuzione = 0,
                flag_val = "1",
                id_stato = anagrafica_stato_avv_pag.VAL_EME_ID,
                cod_stato = anagrafica_stato_avv_pag.VAL_EME
            };
            p_Avviso.tab_unita_contribuzione_fatt_emissione.Add(v_unitaContrFattEmissioneArrotondamento);
        }

        public static bool AnnullaAvvisoPerRicalcolo(int p_idAvvPag, dbEnte p_enteContext, int p_idStruttura = 0, int p_idRisora = 0)
        {
            //Pulizia tab_unita_contribuzione_fatt_emissione
            string v_sqlUnitaContribuzioneFattEmissione = string.Concat("UPDATE ", nameof(tab_unita_contribuzione_fatt_emissione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlUnitaContribuzioneFattEmissione = string.Concat(v_sqlUnitaContribuzioneFattEmissione, ", ", nameof(tab_unita_contribuzione_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_unita_contribuzione_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_unita_contribuzione_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlUnitaContribuzioneFattEmissione = string.Concat(v_sqlUnitaContribuzioneFattEmissione, " WHERE ", nameof(tab_unita_contribuzione_fatt_emissione.id_avv_pag_generato), " = ", p_idAvvPag.ToString());



            //Pulizia join_avv_pag_fatt_emissione_soggetto_debitore
            string v_sqlAvvPagSoggettoDebitoreFattEmissione = string.Concat("UPDATE ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagSoggettoDebitoreFattEmissione = string.Concat(v_sqlAvvPagSoggettoDebitoreFattEmissione, ", ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore.id_risorsa_stato), " = ", p_idRisora.ToString());
            }

            v_sqlAvvPagSoggettoDebitoreFattEmissione = string.Concat(v_sqlAvvPagSoggettoDebitoreFattEmissione, " WHERE ", nameof(join_avv_pag_fatt_emissione_soggetto_debitore.id_tab_avv_pag), " = ", p_idAvvPag.ToString());



            //Pulizia tab_avv_pag_fatt_emissione
            string v_sqlAvvPagFattEmissione = string.Concat("UPDATE ", nameof(tab_avv_pag_fatt_emissione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, ", ", nameof(tab_avv_pag_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(tab_avv_pag_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlAvvPagFattEmissione = string.Concat(v_sqlAvvPagFattEmissione, " WHERE ", nameof(tab_avv_pag_fatt_emissione.id_tab_avv_pag), " = ", p_idAvvPag.ToString());



            string v_sqlJoincontrolli = string.Concat("UPDATE ", nameof(join_controlli_avvpag_fatt_emissione), " SET FLAG_ON_OFF = '0'");

            if (p_idRisora != 0)//Segna i record eliminati se viene indicata una risorsa
            {
                v_sqlJoincontrolli = string.Concat(v_sqlJoincontrolli, ", ", nameof(join_controlli_avvpag_fatt_emissione.id_struttura_stato), " = ", p_idStruttura.ToString(),
                                                         ", ", nameof(join_controlli_avvpag_fatt_emissione.id_risorsa_stato), " = ", p_idRisora.ToString(),
                                                         ", ", nameof(join_controlli_avvpag_fatt_emissione.data_stato), " = @data_stato");
            }

            v_sqlJoincontrolli = string.Concat(v_sqlJoincontrolli, " WHERE ", nameof(join_controlli_avvpag_fatt_emissione.id_avvpag_fatt_emissione), " = ", p_idAvvPag.ToString());

            TimeSpan v_timeOut = new TimeSpan(1, 0, 0);
            using (TransactionScope v_trans = new TransactionScope(TransactionScopeOption.Required, v_timeOut))
            {
                try
                {
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlUnitaContribuzioneFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagSoggettoDebitoreFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    p_enteContext.Database.ExecuteSqlCommand(v_sqlJoincontrolli, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    int v_numAvvisi = p_enteContext.Database.ExecuteSqlCommand(v_sqlAvvPagFattEmissione, p_idRisora != 0 ? new SqlParameter("@data_stato", DateTime.Now) : null);
                    v_trans.Complete();

                }
                catch (Exception ex)
                {
                    //loggare eventualmente
                    m_logger.LogException("Errore nell'annullamento del consolidamento: ", ex, Utility.Log.EnLogSeverity.Fatal);
                    return false;
                }
            }
            return true;
        }

    }
}
