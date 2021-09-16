using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAvvPagLinq
    {
        private static DateTime InizioCovid = new DateTime(2020, 3, 8); //Data di inizio del covid
        private static DateTime FineCovid = new DateTime(2020, 12, 31, 23, 59, 59); //Data di inizio del covid
        private static int GiorniSospensioneCovid = 85;                 //Giorni di sospensione del covid

        public static void setPrescrizione(tab_avv_pag p_avviso, DateTime p_dataEmissione, Int32 p_maxMesiDallaPrescrizione)
        {
            try
            {

                DateTime? dataPrescrizione = null;

                bool v_isBollo = p_avviso.tab_contribuzione.Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
                                                         .OrderBy(o => o.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata)
                                                         .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.id_entrata).FirstOrDefault() == anagrafica_entrate.BOLLO_AUTO;

                int? v_aggiuntaAnno = p_avviso.tab_contribuzione
                                             .Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
                                             .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(v => (int?)v);

                if (v_aggiuntaAnno == null)
                {
                    v_aggiuntaAnno = p_avviso.tab_contribuzione
                                             .Where(u => u.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata > 0)
                                             .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min();
                    p_avviso.flag_adesione = "E";
                }

                DateTime? v_dataNotifica = p_avviso.TAB_JOIN_AVVCOA_INGFIS_V21
                                                    .Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                && j.tab_avv_pag.flag_esito_sped_notifica == "1")
                                                    .Max(x => x.tab_avv_pag.data_ricezione);

                if (v_dataNotifica == null)
                {
                    v_dataNotifica = p_avviso.data_ricezione.Value;
                }

                if (v_isBollo)
                {
                    dataPrescrizione = new DateTime(v_dataNotifica.Value.Year + v_aggiuntaAnno.Value, 12, 31, 23, 59, 59);
                }
                else
                {
                    dataPrescrizione = v_dataNotifica.Value.AddYears(v_aggiuntaAnno.Value);
                }

                dataPrescrizione = (dataPrescrizione >= InizioCovid && dataPrescrizione <= FineCovid
                                                 ?
                                                 dataPrescrizione.Value.AddDays(85)
                                                 :
                                                 dataPrescrizione);

                p_avviso.data_adesione = dataPrescrizione;
                //p_avviso.flag_atto_non_pagabile = "R";
                p_avviso.flag_atto_non_pagabile = "S";
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static IQueryable<tab_avv_pag> WhereByIdListaEmissione(this IQueryable<tab_avv_pag> p_query, int p_idListaEmissione)
        {
            return p_query.Where(w => w.id_lista_emissione == p_idListaEmissione);
        }

        public static IQueryable<tab_avv_pag> WhereByIdServizio(this IQueryable<tab_avv_pag> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == p_idServizio);
        }

        public static IQueryable<tab_avv_pag> WhereByNotIdServizio(this IQueryable<tab_avv_pag> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio != p_idServizio);
        }

        public static IQueryable<tab_avv_pag> WhereByIdentificativoAvviso(this IQueryable<tab_avv_pag> p_query, string p_identificativo)
        {
            string p_identificativo2 = !string.IsNullOrEmpty(p_identificativo) ? p_identificativo.Replace("/", string.Empty).Replace("-", string.Empty).Trim() : string.Empty;
            string v_codice = string.Empty;
            string v_anno = string.Empty;
            string v_progressivo = string.Empty;

            if (!string.IsNullOrEmpty(p_identificativo2) && p_identificativo2.Length > 8)
            {
                v_codice = p_identificativo2.Substring(0, 4);
                v_anno = p_identificativo2.Substring(4, 4);
                if (p_identificativo2.Substring(8).All(char.IsDigit))
                {
                    v_progressivo = Convert.ToInt32(p_identificativo2.Substring(8)).ToString();
                }
            }

            return p_query.Where(d => d.identificativo_avv_pag.Trim() == p_identificativo || (d.anagrafica_tipo_avv_pag.cod_tipo_avv_pag == v_codice &&
                                                                                              d.anno_riferimento == v_anno &&
                                                                                              d.numero_avv_pag == v_progressivo));
        }

        public static IQueryable<tab_avv_pag> WhereByDatiAvviso(this IQueryable<tab_avv_pag> p_query, string p_codTipoAvviso, string p_anno, string p_numero)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.cod_tipo_avv_pag == p_codTipoAvviso &&
                                      d.anno_riferimento == p_anno &&
                                      d.numero_avv_pag == p_numero);
        }

        public static IQueryable<tab_avv_pag> WhereByIciImuTasiEsclusi(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(w => !((w.id_entrata == anagrafica_entrate.ICI ||
                                         w.id_entrata == anagrafica_entrate.IMU ||
                                         w.id_entrata == anagrafica_entrate.TASI)
                                            && w.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA));
        }

        public static IQueryable<tab_avv_pag> WhereBySollecitiIntimazioniEsclusi(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.SOLL_PRECOA
                                        && d.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.INTIM);
        }

        public static IQueryable<tab_avv_pag> WhereByServiziIngiunzione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<tab_avv_pag> WhereByServiziAttiSuccessiviIngiunzioni(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<tab_avv_pag> WhereByServiziCommTrib(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERTAMENTO && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<tab_avv_pag> WhereByServiziGDPOrTribOrd(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.RISC_PRECOA && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM && d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<tab_avv_pag> WhereByIngiunzioniCautelari(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC
                                    || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI);
        }

        public static IQueryable<tab_avv_pag> WhereByIngNonPignorateECautelariAdIng(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC && !d.TAB_JOIN_AVVCOA_INGFIS_V21.Any(x => (x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                                                                                                                                            x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                                                                                                                                            x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                                                                                                                                            x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI) &&
                                                                                                                                                      x.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                                                                                                                                                      x.tab_avv_pag.flag_esito_sped_notifica == "1"))
                                    || ((d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM) && d.TAB_JOIN_AVVCOA_INGFIS_V2.Any(x => x.tab_avv_pag1.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC &&
                                                                                                                                                                                                                                              x.tab_avv_pag1.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) &&
                                                                                                                                                                                                                                              x.tab_avv_pag1.flag_esito_sped_notifica == "1")));
        }

        public static IQueryable<tab_avv_pag> WhereByIngECautelariPignoramentiSollecitiAdIng(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC)
                                    || ((d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                                    d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                    d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                    d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                    d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI ||
                                    d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM ||
                                    d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA) && d.TAB_JOIN_AVVCOA_INGFIS_V2.Any(x => x.tab_avv_pag1.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC &&
                                                                                                                                                          x.tab_avv_pag1.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) &&
                                                                                                                                                          x.tab_avv_pag1.flag_esito_sped_notifica == "1")));
        }

        public static IQueryable<tab_avv_pag> WhereByNotIdAvvPag(this IQueryable<tab_avv_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_tab_avv_pag != p_idAvvPag);
        }

        public static IQueryable<tab_avv_pag> WhereByIdAvvPag(this IQueryable<tab_avv_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_tab_avv_pag == p_idAvvPag);
        }

        public static IQueryable<tab_avv_pag> WhereByIdContribuente(this IQueryable<tab_avv_pag> p_query, Decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_anag_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_avv_pag> WhereByIdEntrata(this IQueryable<tab_avv_pag> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata || d.id_entrata == anagrafica_entrate.NESSUNA_ENTRATA);
        }

        public static IQueryable<tab_avv_pag> WhereByNotIdEntrata(this IQueryable<tab_avv_pag> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata != p_idEntrata);
        }

        public static IQueryable<tab_avv_pag> WhereByIdTipoAvvPag(this IQueryable<tab_avv_pag> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.id_tipo_avvpag == p_idTipoAvvPag);
        }

        public static IQueryable<tab_avv_pag> WhereByIdTipoAvvPagList(this IQueryable<tab_avv_pag> p_query, List<int> p_idTipoAvvPagList)
        {
            return p_query.Where(d => p_idTipoAvvPagList.Contains(d.id_tipo_avvpag));
        }

        public static IQueryable<tab_avv_pag> WhereByJoinTabDocInputCountNotZero(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.join_tab_avv_pag_tab_doc_input.Count > 0);
        }

        public static IQueryable<tab_avv_pag> WhereByCodStato(this IQueryable<tab_avv_pag> p_query, string p_codStato)
        {
            if (!string.IsNullOrEmpty(p_codStato))
            {
                return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
            }
            else
            {
                return p_query;
            }
        }
        public static IQueryable<tab_avv_pag> WhereByCodStatoEquals(this IQueryable<tab_avv_pag> p_query, string p_codStato)
        {
            if (!string.IsNullOrEmpty(p_codStato))
            {
                return p_query.Where(d => d.cod_stato.Equals(p_codStato));
            }
            else
            {
                return p_query;
            }
        }
        public static IQueryable<tab_avv_pag> WhereByCodStatoNotStart(this IQueryable<tab_avv_pag> p_query, string p_codStato)
        {
            if (!string.IsNullOrEmpty(p_codStato))
            {
                return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
            }
            else
            {
                return p_query;
            }
        }
        public static IQueryable<tab_avv_pag> WhereByCodStatoNotStartAndNotEquals(this IQueryable<tab_avv_pag> p_query, string p_codStatoStart, string p_codStato)
        {
            if (!string.IsNullOrEmpty(p_codStatoStart) && !string.IsNullOrEmpty(p_codStato))
            {
                return p_query.Where(d => !d.cod_stato.StartsWith(p_codStatoStart) || !d.cod_stato.Equals(p_codStato));
            }
            else
            {
                return p_query;
            }
        }
        public static IQueryable<tab_avv_pag> WheretNotAcceratamentoOrIngFisc(this IQueryable<tab_avv_pag> p_query)
        {
            decimal imp = 1.5m;
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERTAMENTO
            || d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC
            &&
            d.imp_tot_pagato < (d.imp_tot_avvpag - 1)
            && ((!d.importo_ridotto.HasValue) || d.importo_ridotto.Value == 0)
            && d.importo_tot_da_pagare > imp);
        }

        public static IQueryable<tab_avv_pag> WherebyCodiceAnnoNumero(this IQueryable<tab_avv_pag> p_query, string p_codTipoAvvPag, string p_anno, string p_numero)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.cod_tipo_avv_pag.Equals(p_codTipoAvvPag) &&
                                      d.anno_riferimento.Equals(p_anno) &&
                                      d.numero_avv_pag.Equals(p_numero));
        }

        public static IQueryable<tab_avv_pag> WhereByServiziList(this IQueryable<tab_avv_pag> p_query, List<int> p_serviziList)
        {
            return p_query.Where(d => p_serviziList.Contains(d.anagrafica_tipo_avv_pag.id_servizio));
        }

        public static IQueryable<tab_avv_pag> WhereByCodStato(this IQueryable<tab_avv_pag> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_avv_pag> WhereByAnyCodStato(this IQueryable<tab_avv_pag> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Any(e => d.cod_stato.StartsWith(e)));
        }

        public static IQueryable<tab_avv_pag> WhereByStartsWithDANorDAR(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE) || d.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE));
        }

        public static IQueryable<tab_avv_pag> WhereByCodStatoRiferimento(this IQueryable<tab_avv_pag> p_query, List<string> p_codStatoList, string p_codStato)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.anagrafica_stato.cod_stato_riferimento) ||
                                      d.anagrafica_stato.cod_stato_riferimento.StartsWith(p_codStato));
        }

        public static IQueryable<tab_avv_pag> WhereByNotCodStato(this IQueryable<tab_avv_pag> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_avv_pag> WhereByFlagIstanzaRicorso(this IQueryable<tab_avv_pag> p_query, int p_flagIstanzaRicorso)
        {
            return p_query.Where(d => d.anagrafica_stato.flag_istanza_ricorso == p_flagIstanzaRicorso);
        }

        public static IQueryable<tab_avv_pag> WhereByIdLista(this IQueryable<tab_avv_pag> p_query, int p_idLista)
        {
            return p_query.Where(d => d.id_lista_emissione == p_idLista);
        }

        public static IQueryable<tab_avv_pag> WhereByIdListaScarico(this IQueryable<tab_avv_pag> p_query, int p_idLista)
        {
            return p_query.Where(d => d.id_lista_scarico == p_idLista);
        }

        public static IQueryable<tab_avv_pag> WhereByValido(this IQueryable<tab_avv_pag> p_query, string p_codStato)
        {
            if (!string.IsNullOrEmpty(p_codStato))
            {
                if (p_codStato.Contains(anagrafica_stato_avv_pag.VALIDO))
                {
                    return p_query.Where(d => d.anagrafica_stato.cod_stato_riferimento.StartsWith(p_codStato) && d.anagrafica_stato.flag_validita == "1");
                }
                else
                {
                    return p_query.Where(d => d.anagrafica_stato.cod_stato_riferimento.Contains(p_codStato));
                }
            }
            else
            {
                return p_query.Where(d => (d.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO) && d.anagrafica_stato.flag_validita == "1") ||
                                          !d.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO));
            }
        }
        public static IQueryable<tab_avv_pag> WhereHasIUV(this IQueryable<tab_avv_pag> p_query)
        {

            return p_query.Where(a => a.tab_rata_avv_pag.Any(s => !string.IsNullOrEmpty(s.Iuv_identificativo_pagamento)));
        }
        public static IQueryable<tab_avv_pag> WhereByCodStatoSgravio(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO) && d.anagrafica_stato.flag_validita == "1") ||
                                       d.anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE) ||
                                       d.anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE) ||
                                       //d.anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.RETTIFICATO_SGRAVIO) ||
                                       d.anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO_SGRAVIO) ||
                                       d.anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO_UFFICIO));
        }

        public static IQueryable<tab_avv_pag> WhereByValido(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_stato.flag_validita == "1");
        }

        public static IQueryable<tab_avv_pag> WhereByIstanzaAnnullamento(this IQueryable<tab_avv_pag> p_query)
        {

            return p_query.Where(d => d.join_tab_avv_pag_tab_doc_input.Where(x => !(x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ANNULLATO) || x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ESITATA))
                                                                                  &&
                                                                                  (x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_ANN_RET))
                                                                      .Count() > 0);
        }

        public static IQueryable<tab_avv_pag> WhereByIstanzaAnnullamentoNotTipoDocAnnRetAndRat(this IQueryable<tab_avv_pag> p_query, List<int> idsTipoDocEntrate)
        {
            return p_query.Where(d => d.join_tab_avv_pag_tab_doc_input.Where(x => !(x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ANNULLATO) || x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ESITATA))
                                                                                 &&
                                                                                 (idsTipoDocEntrate.Contains(x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc)))
                                                                     .Count() > 0);
        }

        public static IQueryable<tab_avv_pag> WhereByIdJoinTabDocInput(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where((d => d.join_tab_avv_pag_tab_doc_input1.Where(x => x.cod_stato.Equals(anagrafica_stato_doc.STATO_DEF_ACCOLTA)).Count() > 0));

        }

        public static IQueryable<tab_avv_pag> WhereNotByIdJoinTabDocInput(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where((d => d.join_tab_avv_pag_tab_doc_input1.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO)).Count() <= 0));

        }

        public static IQueryable<tab_avv_pag> WhereByRicorso(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.join_tab_avv_pag_tab_doc_input.Where(x => !(x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ANNULLATO) || x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ESITATA))
                                                                                  &&
                                                                                  (x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI))
                                                                      .Count() > 0);
        }

        public static IQueryable<tab_avv_pag> WhereByRateizzazione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.cod_stato.Equals(anagrafica_stato_avv_pag.SOSPESO_RATEIZZAZIONE)
                                     ||
                                     (d.cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO) &&
                                      d.flag_rateizzazione_bis == "1"
                                      &&
                                      (d.tab_unita_contribuzione1.Where(y => y.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).Count() > 0)
                                      && d.anagrafica_tipo_avv_pag.id_servizio.Equals(anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA)));

        }

        public static IQueryable<tab_avv_pag> WhereByProceduraConcorsuale(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO))
                                      &&
                                      d.join_tab_avv_pag_tab_doc_input.Where(y => y.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_PROCEDURA_CONCORSUALE).Count() > 0);

        }

        public static IQueryable<tab_avv_pag> WhereByAvvisiImportati(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (!string.IsNullOrEmpty(d.fonte_emissione) &&
                                       d.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA)) ||
                                       !string.IsNullOrEmpty(d.fonte_emissione) ||
                                      (d.tab_liste != null &&
                                       d.tab_liste.tab_tipo_lista.flag_tipo_lista != tab_tipo_lista.FLAG_TIPO_LISTA_C) ||
                                      (d.tab_liste != null &&
                                       d.tab_liste.anagrafica_strutture_aziendali1 != null &&
                                       d.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente != null &&
                                       d.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.id_ente != anagrafica_ente.ID_ENTE_PUBLISERVIZI) ||
                                      (d.tab_liste == null &&
                                       string.IsNullOrEmpty(d.barcode)));
        }

        public static IQueryable<tab_avv_pag> WhereByAvvisiEmessi(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d =>
                                     //(string.IsNullOrEmpty(d.fonte_emissione) ||
                                     //(!string.IsNullOrEmpty(d.fonte_emissione) &&
                                     // !d.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA)))
                                     // &&
                                     ((d.tab_liste != null &&
                                       d.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C) ||
                                      (d.tab_liste == null &&
                                       !string.IsNullOrEmpty(d.barcode))));
        }

        public static IQueryable<tab_avv_pag> WhereByInNessunaIstanzaRicorso(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.join_tab_avv_pag_tab_doc_input.Where(x => !x.cod_stato.Equals(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                                  (x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ACQUISITO) ||
                                                                                   x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO)) &&
                                                                                  (x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_ANN_RET ||
                                                                                   x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI))
                                                                      .Count() == 0 &&
                                      d.join_tab_avv_pag_tab_doc_input1.Where(x => !x.cod_stato.Equals(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                                   (x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ACQUISITO) ||
                                                                                    x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO)) &&
                                                                                   (x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_ANN_RET ||
                                                                                    x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI))
                                                                      .Count() == 0);
        }

        public static IQueryable<tab_avv_pag> WhereByFermoAmministrativo(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO ||
                                     (d.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMM_OLD && (d.cod_stato.Equals(anagrafica_stato_avv_pag.VAL_FIS) || d.cod_stato.Equals(anagrafica_stato_avv_pag.SOSPESO_RATEIZZAZIONE))));
        }

        public static IQueryable<tab_avv_pag> WhereByNotAttoSuccessivo(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.tab_unita_contribuzione1.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                            !x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                .Count() == 0);
        }

        public static IQueryable<tab_avv_pag> WhereByRimborso(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.imp_tot_pagato > 0 && d.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)) ||
                                      (d.imp_tot_pagato > d.imp_tot_avvpag_rid && d.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)) ||
                                     ((Math.Abs((d.imp_tot_pagato ?? 0) - (d.imp_tot_avvpag_rid ?? 0)) < 1) &&
                                       d.tab_unita_contribuzione.Any(x => x.id_avv_pag_collegato.HasValue &&
                                                                          x.tab_avv_pag1.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))) ||
                                     ((Math.Abs((d.imp_tot_pagato ?? 0) - (d.imp_tot_avvpag_rid ?? 0)) < 1) &&
                                       d.tab_unita_contribuzione.Any(x => x.id_avv_pag_collegato.HasValue &&
                                                                          x.tab_avv_pag1.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC &&
                                                                        ((x.tab_avv_pag1.riscosso_avvisi_coattivi_det_riscossione ?? 0) >= ((x.tab_avv_pag1.imp_tot_avvpag_rid ?? 0) - 1) * 2))));
        }

        public static IQueryable<tab_avv_pag> WhereByNotAvvisiRateizzazione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_avvpag != anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA &&
                                      d.id_tipo_avvpag != anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA_OLTRETERMINI);
        }

        public static IQueryable<tab_avv_pag> WhereByStatoSSPANNDAR(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.SOSPESO_ISTANZA) ||
                                      d.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.ANNULLATO) ||
                                      d.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.DARETTIFICARE));
        }

        public static IQueryable<tab_avv_pag> WhereByImportoDaPagare(this IQueryable<tab_avv_pag> p_query, decimal p_importoMinimo)
        {
            return p_query.Where(d => d.importo_tot_da_pagare.HasValue && d.importo_tot_da_pagare.Value >= p_importoMinimo);
        }

        public static IQueryable<tab_avv_pag> WhereByRateNonCompletamentePagate(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.tab_rata_avv_pag.Any(r => (!r.cod_stato.StartsWith(CodStato.ANN) && !r.imp_pagato.Equals(r.imp_tot_rata))));
        }

        public static IQueryable<tab_avv_pag> WhereByNonInUnitaFattEmissione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => !d.tab_unita_contribuzione_fatt_emissione.Any(x => x.tab_avv_pag_fatt_emissione.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                                                                                        (x.tab_avv_pag_fatt_emissione.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_dataEmissione"></param>
        /// <param name="p_minGiorniNotifica"></param>
        /// <param name="p_maxMesiDallaPrescrizione">Se 0 prende tutte le non prescritte, altrimenti solo quelle a cui mancano meno di p_maxMesiDallaPrescrizione mesi dalla prescrizione</param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> WhereNonPrescrittaNew(this IQueryable<tab_avv_pag> p_query, DateTime? p_dataEmissione = null, Int32 p_minGiorniNotifica = 0, Int32 p_maxMesiDallaPrescrizione = 0)
        {
            DateTime v_dataEmissione = p_dataEmissione ?? DateTime.Now;

            var v_idAvvPagList = p_query.Select(s => new
            {
                id = s.id_tab_avv_pag,
                //id_contribuente = s.id_anag_contribuente,
                //tipoPrescrizione = (s.tab_contribuzione.Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale == tab_tipo_voce_contribuzione.CODICE_ENT)
                //                                                                .OrderBy(o => o.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata)
                //                                                                .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.id_entrata).FirstOrDefault() == anagrafica_entrate.BOLLO_AUTO ? "Fine Anno" : "Alla Data"),
                dataPrescrizione = (s.tab_contribuzione.Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                                        .OrderBy(o => o.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata)
                                                                                        .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.id_entrata).FirstOrDefault() == anagrafica_entrate.BOLLO_AUTO
                                                                                        ?
                                                            DbFunctions.CreateDateTime(
                                                                                        SqlFunctions.DatePart("year", SqlFunctions.DateAdd("year"
                                                                                                                                        , s.tab_contribuzione
                                                                                                                                            .Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
                                                                                                                                            .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min()
                                                                                                                                        , s.TAB_JOIN_AVVCOA_INGFIS_V21
                                                                                                                                            .Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                                                                                                        && j.tab_avv_pag.flag_esito_sped_notifica == "1")
                                                                                                                                            .Max(x => x.tab_avv_pag.data_ricezione) ?? s.data_ricezione.Value))
                                                                                        , 12, 31, 23, 59, 59)
                                                                                        :
                                                                                        SqlFunctions.DateAdd("year"
                                                                                                            , s.tab_contribuzione
                                                                                                            .Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.StartsWith(tab_tipo_voce_contribuzione.CODICE_ENT))
                                                                                                            .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min()
                                                                                                            , s.TAB_JOIN_AVVCOA_INGFIS_V21
                                                                                                            .Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                                                                        && j.tab_avv_pag.flag_esito_sped_notifica == "1")
                                                                                                            .Max(x => x.tab_avv_pag.data_ricezione) ?? s.data_ricezione.Value)
                                                        )
                //,
                //idEntrataMinore = s.tab_contribuzione.Where(u => u.tab_tipo_voce_contribuzione.codice_tributo_ministeriale == tab_tipo_voce_contribuzione.CODICE_ENT)
                //                                                                .OrderBy(o => o.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata)
                //                                                                .Select(v => v.tab_tipo_voce_contribuzione.anagrafica_entrate.id_entrata).FirstOrDefault()
            })
                    .Select(s => new
                    {
                        id = s.id,
                        //id_contribuente = s.id_contribuente,
                        //tipoPrescrizione = s.tipoPrescrizione,
                        dataPrescrizione = s.dataPrescrizione >= InizioCovid && s.dataPrescrizione <= FineCovid
                                            ?
                                            SqlFunctions.DateAdd("day", 85, s.dataPrescrizione.Value)
                                            :
                                            s.dataPrescrizione.Value
                        //,
                        //idEntrataMinore = s.idEntrataMinore
                    })
                    .Where(w => w.dataPrescrizione >= SqlFunctions.DateAdd("day", p_minGiorniNotifica, v_dataEmissione) && (p_maxMesiDallaPrescrizione == 0 || w.dataPrescrizione <= SqlFunctions.DateAdd("month", p_maxMesiDallaPrescrizione, v_dataEmissione)));

            var test = from q in p_query
                       from f in v_idAvvPagList
                       where q.id_tab_avv_pag == f.id
                       select q;


            return test; //p_query.Where(w => v_idAvvPagList.Contains(w.id_tab_avv_pag));
        }


        /// <summary>
        /// Verifica se un atto è prescritto
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_dataEmissione">Data di emissione dell'atto</param>
        /// <param name="p_giorniTolleranzaPrescrizione">Giorni di tolleranza sulla prescrizione. Es.: con 5 prenderà anche atti prescritti da 5 giorni</param>
        /// <returns></returns>
        public static IQueryable<tab_avv_pag> WhereNonPrescritta(this IQueryable<tab_avv_pag> p_query, DateTime? p_dataEmissione = null,
                                                                  int p_giorniTolleranzaPrescrizione = 0, DateTime? p_limitePrescrizione = null)
        {
            DateTime v_dtEmissione = p_dataEmissione ?? DateTime.Now;
            DateTime v_inizioAnno = new DateTime(v_dtEmissione.Year, 1, 1);
            DateTime v_dataTagliPrescrizione = p_limitePrescrizione ?? DateTime.Now.AddMonths(6);
            DateTime v_dataTaglio = v_dtEmissione.AddDays(-1 * p_giorniTolleranzaPrescrizione);//Data taglio della prescrizione per atti che non attraversano il covid
            DateTime v_dataTaglioCovid = v_dataTaglio.AddDays(-1 * GiorniSospensioneCovid); //Data taglio per atti che attraversano il covid
            List<int> v_entratePrescritteFineAnno = new List<int>() { anagrafica_entrate.BOLLO_AUTO };

            List<int> listaEntDaScartare = new List<int>() { anagrafica_entrate.NESSUNA_ENTRATA,
                                                             anagrafica_entrate.RISCOSSIONE_COATTIVA,
                                                             anagrafica_entrate.SPESE_NOTIFICA,
                                                             anagrafica_entrate.SPESE_RISCOSSIONE_COATTIVA,
                                                             anagrafica_entrate.ENTRATA_NON_ATTRIBUIBILE };

            return p_query.Where(d => (
                                        //Prescrizione Ordinaria
                                        (
                                            //Prende le non prescritte inclusa la tolleranza (scadute da X giorni)
                                            SqlFunctions.DateAdd("year", d.tab_contribuzione.Where(u => !listaEntDaScartare.Contains(u.tab_tipo_voce_contribuzione.id_entrata.Value)).Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(),
                                                            (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                            && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => (x.tab_avv_pag.data_ricezione ?? x.tab_avv_pag.data_avvenuta_notifica))
                                                            ) ?? (d.data_ricezione ?? d.data_avvenuta_notifica.Value)) > v_dataTaglio
                                        )
                                        &&
                                        (
                                            //La prescrizione non super la data indicata
                                            SqlFunctions.DateAdd("year", d.tab_contribuzione.Where(u => !listaEntDaScartare.Contains(u.tab_tipo_voce_contribuzione.id_entrata.Value)).Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(),
                                                            (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                            && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => (x.tab_avv_pag.data_ricezione ?? x.tab_avv_pag.data_avvenuta_notifica))
                                                            ) ?? (d.data_ricezione ?? d.data_avvenuta_notifica.Value)) <= v_dataTagliPrescrizione
                                        )
                                      )
                                    ||
                                     //Se l'ultimo atto successivo è stato emesso prima della fine del covid e viene prescritto dopo l'inizio del covid, alla sua prescrizione (con tolleranza, vengono aggiunti 85 giorni) 
                                     (
                                          //Ultimo atto emesso prima della fine del covid
                                          (((d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)) && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => x.tab_avv_pag.dt_emissione)) ?? d.dt_emissione.Value) <= FineCovid)
                                          &&
                                          //La prescrizione avviene dopo l'inizio del covid
                                          (SqlFunctions.DateAdd("year", d.tab_contribuzione.Where(u => !listaEntDaScartare.Contains(u.tab_tipo_voce_contribuzione.id_entrata.Value)).Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(),
                                                          (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                         && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => (x.tab_avv_pag.data_ricezione ?? x.tab_avv_pag.data_avvenuta_notifica))
                                                          ) ?? (d.data_ricezione ?? d.data_avvenuta_notifica.Value)) > InizioCovid)
                                          &&
                                          //Test prescrizione con Covid
                                          (

                                              //Ingiunzioni su entrate che non si prescrivono con la fine dell'anno
                                              (SqlFunctions.DateAdd("year", d.tab_contribuzione.Where(u => !listaEntDaScartare.Contains(u.tab_tipo_voce_contribuzione.id_entrata.Value)).Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(),
                                                              (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                             && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => (x.tab_avv_pag.data_ricezione ?? x.tab_avv_pag.data_avvenuta_notifica))
                                                              ) ?? (d.data_ricezione ?? d.data_avvenuta_notifica.Value)) > v_dataTaglioCovid)
                                              &&
                                              !v_entratePrescritteFineAnno.Contains(d.anagrafica_tipo_avv_pag.id_entrata_avvpag_collegati.Value)
                                              ||
                                              //Ingiunzioni su entrate che non si prescrivono con la fine dell'anno
                                              (SqlFunctions.DateAdd("year", d.tab_contribuzione.Where(u => !listaEntDaScartare.Contains(u.tab_tipo_voce_contribuzione.id_entrata.Value)).Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(),
                                                              (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                             && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => (x.tab_avv_pag.data_ricezione ?? x.tab_avv_pag.data_avvenuta_notifica))
                                                              ) ?? (d.data_ricezione ?? d.data_avvenuta_notifica.Value)) >= v_inizioAnno)
                                              &&
                                              v_entratePrescritteFineAnno.Contains(d.anagrafica_tipo_avv_pag.id_entrata_avvpag_collegati.Value)
                                          )
                                      )
                                );
        }

        public static IQueryable<tab_avv_pag> WherePrescrittaEntroAnno(this IQueryable<tab_avv_pag> p_query, DateTime? p_dataEmissione = null, int p_giorniTolleranza = 0)
        {

            List<int> listaEntDaScartare = new List<int>() { anagrafica_entrate.NESSUNA_ENTRATA,
                                                             anagrafica_entrate.RISCOSSIONE_COATTIVA,
                                                             anagrafica_entrate.SPESE_NOTIFICA,
                                                             anagrafica_entrate.SPESE_RISCOSSIONE_COATTIVA,
                                                             anagrafica_entrate.ENTRATA_NON_ATTRIBUIBILE };

            return p_query.WhereNonPrescritta(p_dataEmissione, p_giorniTolleranza)
                          //Prescritta entro un anno
                          .Where(d => (SqlFunctions.DateAdd("year", d.tab_contribuzione.Where(u => !listaEntDaScartare.Contains(u.tab_tipo_voce_contribuzione.id_entrata.Value)).Select(s => s.tab_tipo_voce_contribuzione.anagrafica_entrate.AA_prescrizione_entrata).Min(),
                                                           (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => (!j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                && j.tab_avv_pag.flag_esito_sped_notifica == "1").Max(x => x.tab_avv_pag.data_ricezione)
                                                           ) ?? d.data_ricezione.Value) < DateTime.Now) //prescritte entro un anno
                                 );
        }

        public static IQueryable<tab_avv_pag> WhereNonPrescrittaPerIngiunzione(this IQueryable<tab_avv_pag> p_query, DateTime p_dataRiferimento, DateTime p_dataEmissione)
        {
            //Precedente Versione sostituita
            //return p_query.Where(d => SqlFunctions.DateAdd("year", d.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.Count() > 0 ? d.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag.FirstOrDefault().AA_prescrizione_avviso.Value : 99, (d.TAB_JOIN_AVVCOA_INGFIS_V21.Where(j => !j.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).Max(x => x.tab_avv_pag.data_avvenuta_notifica)) ?? d.data_avvenuta_notifica.Value) > DateTime.Now);


            List<int> listaEntrateCDS = new List<int>() { anagrafica_entrate.CDS, anagrafica_entrate.CDS_ACCERTAMENTI, anagrafica_entrate.CDS_SANZIONI_SOSTA, anagrafica_entrate.CDS_ZTL, anagrafica_entrate.SANZIONE_AMMINISTRATIVA };

            return p_query.Where(d => (listaEntrateCDS.Contains(d.id_entrata) //Entrate da CDS
                                       && d.data_ricezione != null && SqlFunctions.DateAdd("year", 5, d.data_ricezione.Value) >= p_dataRiferimento)
                                   || (!listaEntrateCDS.Contains(d.id_entrata) //Entrate non CDS
                                       && d.data_ricezione != null && SqlFunctions.DateAdd("year", 3, (SqlFunctions.DateAdd("day", 60, d.data_ricezione.Value))).Value.Year >= p_dataEmissione.Year))
                        ;
        }

        public static IQueryable<tab_avv_pag> WhereByNotSpeditiConNotificaNonNotificati(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.flag_spedizione_notifica == "0" ||
                                      d.flag_spedizione_notifica == null ||
                                     (d.flag_spedizione_notifica == "1" && d.flag_esito_sped_notifica == "1"));
        }

        public static IQueryable<tab_avv_pag> WhereByRateizzato(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.flag_rateizzazione_bis == "1");
        }

        public static IQueryable<tab_avv_pag> WhereByNotRateizzato(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.flag_rateizzazione_bis == "0" ||
                                      d.flag_rateizzazione_bis == "9" ||
                                      d.flag_rateizzazione_bis == null);
        }

        public static IQueryable<tab_avv_pag> WhereByAvvisiRateizzati(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_avvpag == anagrafica_tipo_avv_pag.RATEIZZAZIONE_COATTIVO_ACCERTAMENTI_ATTI_COATTIVI ||
                                      d.id_tipo_avvpag == anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA);
        }

        public static IQueryable<tab_avv_pag> WhereByNotAvvisiRateizzati(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_tipo_avvpag != anagrafica_tipo_avv_pag.RATEIZZAZIONE_COATTIVO_ACCERTAMENTI_ATTI_COATTIVI &&
                                      d.id_tipo_avvpag != anagrafica_tipo_avv_pag.RATEIZZAZIONE_SINGOLA);
        }

        public static IQueryable<tab_avv_pag> WhereByAttiSuccessiviIngiunzione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<tab_avv_pag> WhereByServiziIstanzeRicorsi(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio >= anagrafica_tipo_servizi.ACCERTAMENTO && d.anagrafica_tipo_avv_pag.id_servizio <= anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_DEFINIZIONE_AGEVOLATA_COA));
        }

        public static IQueryable<tab_avv_pag> WhereByServiziRimborsi(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA && d.anagrafica_tipo_avv_pag.id_entrata != anagrafica_entrate.ICI && d.anagrafica_tipo_avv_pag.id_entrata != anagrafica_entrate.IMU) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio >= anagrafica_tipo_servizi.ACCERTAMENTO && d.anagrafica_tipo_avv_pag.id_servizio <= anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO) ||
                                      (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA));
        }

        public static IQueryable<tab_avv_pag> WhereByServiziRateizzazione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.GEST_ORDINARIA ||
                                     (d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA &&
                                      d.anagrafica_tipo_avv_pag.id_entrata != anagrafica_entrate.ICI &&
                                      d.anagrafica_tipo_avv_pag.id_entrata != anagrafica_entrate.IMU &&
                                      d.anagrafica_tipo_avv_pag.id_entrata != anagrafica_entrate.TASI));
        }

        public static IQueryable<tab_avv_pag> WhereByRateizzazioneCoattivaEAccertamenti(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_entrata == anagrafica_entrate.RISCOSSIONE_COATTIVA ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERTAMENTO ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.RISC_PRECOA ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO ||
                                      d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO);
        }

        public static IQueryable<tab_avv_pag> WhereByNaturaToG(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T ||
                                      d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_G);
        }

        public static IQueryable<tab_avv_pag> WhereByNaturaEoG(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E ||
                                      d.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_G);
        }

        public static IQueryable<tab_avv_pag> WhereByNotIdEntrateList(this IQueryable<tab_avv_pag> p_query, IList<int> p_idEntrateList)
        {

            return p_query.Where(d => !p_idEntrateList.Contains(d.id_entrata));
        }

        public static IQueryable<tab_avv_pag> WhereByIdEntrateList(this IQueryable<tab_avv_pag> p_query, IList<int> p_idEntrateList)
        {
            return p_query.Where(d => p_idEntrateList.Contains(d.id_entrata));
        }
        public async static Task<List<tab_avv_pag>> WhereByIdListEntrate(this IQueryable<tab_avv_pag> p_query, IList<int> p_idEntrateList)
        {
            return await p_query.Where(d => p_idEntrateList.Contains(d.id_entrata)).ToListAsync();
        }
        //Luigi ha voluto togliere la seconda condizione
        public static IQueryable<tab_avv_pag> WhereByDateRottamazione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => (d.data_ricezione.HasValue && d.data_ricezione <= new DateTime(2017, 09, 30) &&
                                      (d.id_ente != 26 || d.data_ricezione >= new DateTime(2017, 01, 01))) ||
                                       d.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.ING_FISC);
        }

        public static IQueryable<tab_avv_pag> WhereBySollecitiIntimazioniEsclusi(this IQueryable<tab_avv_pag> p_query, IList<int> p_idEntrateList)
        {
            return p_query.Where(d => p_idEntrateList.Contains(d.id_entrata));
        }

        public static IQueryable<tab_avv_pag> WhereByIdAvvPagList(this IQueryable<tab_avv_pag> p_query, IList<int> p_idAvvPagList)
        {
            return p_query.Where(d => p_idAvvPagList.Contains(d.id_tab_avv_pag));
        }

        public static IQueryable<tab_avv_pag> WhereByDataEmissione(this IQueryable<tab_avv_pag> p_query, DateTime p_data, int? p_offset)
        {
            if (p_offset.HasValue)
            {
                return p_query.Where(d => d.dt_emissione.HasValue && DbFunctions.AddDays(d.dt_emissione, p_offset) > p_data);
            }
            else
            {
                return p_query.Where(d => d.dt_emissione.HasValue && DbFunctions.AddDays(d.dt_emissione, d.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? d.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0) > p_data);
            }
        }

        public static IQueryable<tab_avv_pag> WhereByRangeDataEmissione(this IQueryable<tab_avv_pag> p_query, DateTime p_dataDa, DateTime p_dataA)
        {
            return p_query.Where(d => d.dt_emissione >= p_dataDa &&
                                      d.dt_emissione <= p_dataA);

        }

        public static IQueryable<tab_avv_pag> WhereIsEmesso(this IQueryable<tab_avv_pag> p_query)
        {
            return WhereByAvvisiEmessi(p_query);
            //return p_query.Where(d => string.IsNullOrEmpty(d.fonte_emissione) ||
            //(!string.IsNullOrEmpty(d.fonte_emissione) && !d.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA))
            //&& (d.tab_liste != null && d.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C)
            //|| (d.tab_liste == null && !string.IsNullOrEmpty(d.barcode)));

        }
        public static IQueryable<tab_avv_pag> WhereByServiziRiscossioneCoattiva(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC);
            //return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC ||
            //                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM ||
            //                                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
            //                                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
            //                                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
            //                                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
            //                                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
            //                                        d.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);

        }
        public static IQueryable<tab_avv_pag> WhereByNotServiziRiscossioneCoattiva(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio != anagrafica_tipo_servizi.ING_FISC);

        }
        public static IQueryable<tab_avv_pag> OrderByDefault(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag).ThenBy(d => d.dt_emissione);
        }

        public static IQueryable<tab_avv_pag> OrderByIdAvvDesc(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderByDescending(d => d.id_tab_avv_pag);
        }

        public static IQueryable<tab_avv_pag> OrderByDataEmissioneDesc(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderByDescending(d => d.dt_emissione);
        }
        public async static Task<List<tab_avv_pag>> OrderByDataEmissioneDescAsync(this IQueryable<tab_avv_pag> p_query)
        {
            return await p_query.OrderByDescending(d => d.dt_emissione).ToListAsync();
        }

        public static IQueryable<tab_avv_pag> OrderByDataEmissione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.dt_emissione);
        }

        public static IQueryable<tab_avv_pag> OrderByIdContribuenteDataEmissione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.id_anag_contribuente).ThenBy(d => d.dt_emissione);
        }

        public static IQueryable<tab_avv_pag> OrderByIdContribuente(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.id_anag_contribuente);
        }

        public static IQueryable<tab_avv_pag> OrderByPrioritaVisualizzazione(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_stato.priorita_visualizzazione)
                          .ThenBy(d => d.dt_emissione);
        }

        public static tab_avv_pag WhereByIdAvvPagRiemesso(this IQueryable<tab_avv_pag> p_query, int p_idAvvPagRiemesso)
        {
            return p_query.Where(d => d.num_avv_riemesso == p_idAvvPagRiemesso).FirstOrDefault();
        }

        public static IQueryable<tab_avv_pag> WhereByIdEnte(this IQueryable<tab_avv_pag> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_avv_pag> WhereByIdEnteGestito(this IQueryable<tab_avv_pag> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente_gestito == p_idEnte);
        }

        public static IQueryable<tab_avv_pag> WhereByIdTipoServizio(this IQueryable<tab_avv_pag> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == p_idServizio);
        }

        public static IQueryable<tab_avv_pag> WhereNonPresenteInRicorsiAccolti(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(d => d.join_tab_avv_pag_tab_doc_input.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                                   x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI &&
                                                                                  !x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ESITATA_RESPINTA)).Count() == 0 &&
                                      d.join_tab_avv_pag_tab_doc_input1.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                                    x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI &&
                                                                                   !x.tab_doc_input.cod_stato.Equals(anagrafica_stato_doc.STATO_ESITATA_RESPINTA)).Count() == 0);
        }

        public static IQueryable<tab_avv_pag> WhereNonRateizzataOrAnnullata(this IQueryable<tab_avv_pag> p_query)
        {
            // null oppure 0 ingiunzione non rateizzata
            // 9 ingiunzione con rateizzazione annullata
            return p_query.Where(ac => ac.flag_rateizzazione_bis == null || ac.flag_rateizzazione_bis.Equals("0") || ac.flag_rateizzazione_bis.Equals("9"));
        }

        public static IQueryable<tab_avv_pag> WhereNonRottamata(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(ac => !ac.cod_stato.Equals(anagrafica_stato_avv_pag.SOSPESO_ROTTAMATO) && !ac.cod_stato.Equals(anagrafica_stato_avv_pag.VALIDO_ROTTAMATO));
        }

        public static IList<tab_avv_pag_light> ToLight(this IQueryable<tab_avv_pag> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_avv_pag_light
            {
                NumeroIstanza = string.Empty,
                TipoIstanza = string.Empty,
                DataPresentazione = string.Empty,
                id_tab_avv_pag = d.id_tab_avv_pag,
                id_tipo_avvpag = d.id_tipo_avvpag,
                Identificativo = d.identificativo_avv_pag,
                NumeroAvviso = d.identificativo_avv_pag,
                dt_emissione_String = d.dt_emissione_String,
                dt_emissione = d.dt_emissione,
                imp_tot_avvpag_rid = d.imp_tot_avvpag_rid_decimal,
                imp_tot_avvpag_Euro = d.imp_tot_avvpag.HasValue ? d.imp_tot_avvpag.Value : 0,
                imponibile = d.imponibile,
                iva = d.iva,
                Importo = d.Importo,
                importoSpeseNotificaDecimal = d.importoSpeseNotificaDecimal,
                importoSpeseCoattiveDecimal = d.importoSpeseCoattiveDecimal,
                ImportoSpeseNotifica = d.ImportoSpeseNotifica,
                ImportoSpeseCoattive = d.ImportoSpeseCoattive,
                Rate = d.Rate,
                Targa = d.flag_iter_recapito_notifica,
                SpeditoNotificato = d.SpeditoNotificato,
                imp_tot_pagato = d.imp_tot_pagato_decimal,
                imp_tot_pagato_Euro = d.imp_tot_pagato_Euro,
                importo_tot_da_pagare = d.importo_tot_da_pagare_decimal,
                ImportoDaPagare = d.ImportoDaPagare,
                stato = d.DescrizioneStatoNew,//d.stato,
                cod_stato = d.anagrafica_stato.cod_stato_riferimento,
                codStatoReale = d.cod_stato,
                Adesione = d.Adesione,
                TipoBene = d.TipoBene,
                id_tab_supervisione_finale = d.id_tab_supervisione_finale.HasValue ? d.id_tab_supervisione_finale.Value : -1,
                IntimazioneCorrelata = d.IntimazioneCorrelata,
                impRidottoPerAdesione = d.impRidottoPerAdesione,
                id_avvpag_preavviso_collegato = (d.TAB_SUPERVISIONE_FINALE_V21 != null && d.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue) ? d.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.Value : -1,
                IsIstanzaVisibile = d.IsIstanzaVisibile,
                ExistsAtti = d.ExistsAtti,
                ExistsAttiIntimSoll = d.ExistsAttiIntimSoll,
                ExistsAttiCoattivi = d.ExistsAttiCoattivi,
                ExistsAttiSuccessivi = d.ExistsAttiSuccessivi,
                ExistsIspezioni = d.ExistsIspezioni,
                IsFatturazioneAcqua = d.IsFatturazioneAcqua,
                ExistsOrdineOrigine = d.ExistsOrdineOrigine,
                ExistsProceduraConcorsuale = d.ExistsProceduraConcorsuale,
                IdAvvPagPreColl = d.ExistsOrdineOrigine ? d.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.Value : -1,
                soggettoDebitore = d.SoggettoDebitore,
                soggettoDebitoreTerzo = d.SoggettoDebitoreTerzo,
                Contribuente = d.tab_contribuente != null ? d.tab_contribuente.contribuenteDisplay : string.Empty,
                IsIstanzaPresentabile = d.IsIstanzaPresentabile,
                dataMassimaPresentazioneIstanza = d.dataMassimaPresentazioneIstanza,
                IsAvvisoPagabile = d.IsAvvisoPagabile,
                dataMassimaPagamentoAvviso = d.dataMassimaPagamentoAvviso,
                data_ricezione_String = d.data_ricezione_String,
                data_ricezione = d.data_ricezione,
                avvisoBonario = d.avvisoBonario,
                IsProvvedimentoPresentabile = d.IsProvvedimentoPresentabile,
                importo_sgravio = d.importo_sgravio,
                importo_sgravio_Euro = d.importo_sgravio_Euro,
                color = "",
                IsAvvisoSgravabile = d.IsAvvisoSgravabile,
                IsAvvisoStatoAnnRetDanDar = d.IsAvvisoStatoAnnRetDanDar,
                IsAvvisoStatoAnnDan = d.IsAvvisoStatoAnnDan,
                DescrizioneTipoAvviso = d.DescrizioneTipoAvviso,
                SpeditoRicezione = d.SpeditoRicezione,
                ImportoAttiSuccessivi = d.importo_atti_successivi_decimal,
                ImportoAttiSuccessivi_Euro = d.importo_atti_successivi_Euro,
                importo_sanzioni_eliminate_eredi = d.importo_sanzioni_eliminate_eredi_decimal,
                importo_sanzioni_eliminate_eredi_Euro = d.importo_sanzioni_eliminate_eredi_Euro,
                interessi_eliminati_definizione_agevolata = d.interessi_eliminati_definizione_agevolata_decimal,
                interessi_eliminati_definizione_agevolata_Euro = d.interessi_eliminati_definizione_agevolata_Euro,
                sanzioni_eliminate_definizione_agevolata = d.sanzioni_eliminate_definizione_agevolata_decimal,
                sanzioni_eliminate_definizione_agevolata_Euro = d.sanzioni_eliminate_definizione_agevolata_Euro,
                importo_definizione_agevolata_eredi_decimal = d.importo_definizione_agevolata_eredi_decimal,
                importo_definizione_agevolata_eredi_Euro = d.importo_definizione_agevolata_eredi_Euro,
                flag_spedizione_notifica = d.flag_spedizione_notifica,
                IsVisibleAtti = d.IsVisibleAtti,
                IsVisibleBene = d.IsVisibleBene,
                IsVisibleAcqua = d.IsVisibleAcqua
            }).ToList();
        }

        public static IQueryable<tab_avv_pag> SelezionaIngiunzioniPerSollecito(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view v_modalitaRate, DateTime v_dataOdiernaRif, List<Int32> v_serviziscartoListCoattivo, Int32 v_idTipoAvvPagDaEmettere, bool v_taglioSituazioneDebitoria)
        {
            IQueryable<tab_avv_pag> v_avvisi = p_query
                                    .WhereByIdTipoServizio(anagrafica_tipo_servizi.ING_FISC)
                                    //.Where(a => (a.anagrafica_tipo_avv_pag.flag_natura_avv_collegati == v_naturaEntrata || a.tab_liste.flag_acconto == v_naturaEntrata)) //nel caso di ingiunzione generica si legge dal flag_acconto la natura dell'avviso
                                    .WhereByCodStato(CodStato.VAL)
                                    .WhereNonPrescritta()
                                    .WhereByImportoDaPagare(v_modalitaRate.importo_minimo_avvpag_collegati)
                                    .WhereByValido()
                                    .Where(a => a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO || (a.flag_esito_sped_notifica.Equals("1") && a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_SI && (v_dataOdiernaRif > a.data_ricezione.Value || v_dataOdiernaRif > a.data_avvenuta_notifica.Value)))
                                    .WhereNonRateizzataOrAnnullata() //flag_rateizzazione_bis
                                                                     //.Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V2.Any(c => c.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !(c.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO) && !c.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.ANNULLATO_INTIMAZIONE_SCADUTA))))
                                    .Where(a => (!a.TAB_JOIN_AVVCOA_INGFIS_V2.Any(c => c.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !(c.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO) && !c.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.ANNULLATO_INTIMAZIONE_SCADUTA))))
                                             || (a.num_avv_riemesso != null && !a.tab_avv_pag_riemesso.FirstOrDefault().TAB_JOIN_AVVCOA_INGFIS_V2.Any(c => c.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !(c.tab_avv_pag.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO) && !c.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.ANNULLATO_INTIMAZIONE_SCADUTA)))))
                                    .WhereNonRottamata()
                                    .Where(i => !i.tab_unita_contribuzione_fatt_emissione.Any(a => a.tab_avv_pag_fatt_emissione.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)
                                            && (a.tab_avv_pag_fatt_emissione.tab_liste.cod_stato.StartsWith(tab_liste.PRE) && !a.tab_avv_pag_fatt_emissione.tab_liste.cod_stato.Equals(tab_liste.PRE_PRE))))//Non prende le ingiunzioni presenti in un'altra lista in corso di emissione
                                    .Where(a => !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2")); //contribuente con stato valido ai fini dell'emissione                                                    

            if (!v_taglioSituazioneDebitoria)
            {
                //Taglio a livello di singolo avviso
                v_avvisi = v_avvisi.Where(a => a.importo_tot_da_pagare.HasValue && a.importo_tot_da_pagare.Value <= v_modalitaRate.importo_limite_sollecito_bonario)
                                   .Where(a =>
                                                (!a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => v_serviziscartoListCoattivo.Contains(coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio) && (!coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN) || coa.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.ANNULLATO_INTIMAZIONE_SCADUTA) && (coa.tab_avv_pag.data_ricezione != null || coa.tab_avv_pag.data_avvenuta_notifica != null)
                                             //|| (coa.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN) && v_dataOdiernaRif > coa.tab_avv_pag.data_ricezione.Value)                                             
                                             || (coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA /*&& coa.tab_avv_pag.id_tipo_avvpag == v_idTipoAvvPagDaEmettere*/))
                                          ));//Taglia le ingiunzioni finite in un avviso coattivo valido 
                                             //oppure finite in avviso coattivo rateizzato in corso di validità 
                                             //oppure finite in solleciti dello stesso tipo del sollecito di emissione                                                                                
            }

            return v_avvisi;
        }

        public static IQueryable<tab_avv_pag> SelezionaIngiunzioniPerComunicazioneSDCRottamabile(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view v_modalitaRate, DateTime v_dataRifRottamazione_inizio, DateTime v_dataRifRottamazione_fine, List<Int32> v_idTipiAvvPagDaConsiderareList)
        {
            IQueryable<tab_avv_pag> v_avvisi;

            //Il dottore,Dante e Nicoletta hanno voluto diversificare il filtro per la rottamazione in questo modo
            if (v_modalitaRate.id_ente == anagrafica_ente.ID_ENTE_REGIONE_LOMBARDIA)
            {
                v_avvisi = p_query.WhereByIdTipoServizio(anagrafica_tipo_servizi.ING_FISC)
                                                         .Where(a => !a.cod_stato.StartsWith(CodStato.ANN))
                                                         .WhereNonPrescritta()
                                                         //.WhereByImportoDaPagare(v_modalitaRate.importo_minimo_avvpag_collegati)
                                                         .WhereByValido()
                                                         .Where(a => a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO ||
                                                                       (a.flag_esito_sped_notifica.Equals("1") &&
                                                                        a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_SI &&
                                                                        ((a.data_ricezione.HasValue &&
                                                                          v_dataRifRottamazione_inizio < a.data_ricezione.Value &&
                                                                          v_dataRifRottamazione_fine > a.data_ricezione.Value) ||
                                                                         (a.data_avvenuta_notifica.HasValue &&
                                                                          v_dataRifRottamazione_inizio < a.data_avvenuta_notifica.Value &&
                                                                          v_dataRifRottamazione_fine > a.data_avvenuta_notifica.Value))))
                                                         .WhereNonRottamata();
            }
            else
            {
                v_avvisi = p_query.WhereByIdTipoServizio(anagrafica_tipo_servizi.ING_FISC)
                                         .Where(a => !a.cod_stato.StartsWith(CodStato.ANN))
                                         //.WhereNonPrescritta()
                                         //.WhereByImportoDaPagare(v_modalitaRate.importo_minimo_avvpag_collegati)
                                         .WhereByValido()
                                         //l'ingiunzione non deve stare in un altro avviso di rottamazione non annullato (viene filtrata in questo modo per gestire le vecchie rottamazioni)
                                         .Where(a => !a.tab_unita_contribuzione1.Any(s => s.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio.Equals(anagrafica_tipo_servizi.SERVIZI_DEFINIZIONE_AGEVOLATA_COA)
                                                                                      && !s.tab_avv_pag.cod_stato.StartsWith(CodStato.ANN)))
                                         .Where(a => a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO ||
                                                       (a.flag_esito_sped_notifica.Equals("1") &&
                                                        a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_SI &&
                                                        ((a.data_ricezione.HasValue &&
                                                          v_dataRifRottamazione_inizio < a.data_ricezione.Value &&
                                                          v_dataRifRottamazione_fine > a.data_ricezione.Value) ||
                                                         (a.data_avvenuta_notifica.HasValue &&
                                                          v_dataRifRottamazione_inizio < a.data_avvenuta_notifica.Value &&
                                                          v_dataRifRottamazione_fine > a.data_avvenuta_notifica.Value))))
                                         //.WhereNonRottamata()
                                         ;
            }

            if (v_idTipiAvvPagDaConsiderareList != null)
            {
                v_avvisi = v_avvisi.Where(a => v_idTipiAvvPagDaConsiderareList.Contains(a.id_tipo_avvpag));
            }

            return v_avvisi;
        }

        /// <summary>
        /// ELIMINARE. NON USATA
        /// </summary>
        public static IQueryable<tab_avv_pag> SelezionaIngiunzioniInPrescrizionePerIntimazione(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view p_modalitaRate, DateTime p_dataOdiernaRif, List<Int32> p_serviziscartoListCoattivo, DateTime p_dataOdiernaIntimazioniRif, DateTime p_dataEmissione)
        {
            //DateTime v_oggi = DateTime.Now;
            IQueryable<tab_avv_pag> v_avvisi = p_query
                    .WhereByIdTipoServizio(anagrafica_tipo_servizi.ING_FISC)
                    .WhereByCodStato(CodStato.VAL)
                    .WhereByValido()
                    .WhereByImportoDaPagare(p_modalitaRate.importo_minimo_avvpag_collegati)
                    //.Where(a => a.flag_esito_sped_notifica.Equals("1") && a.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione && (SqlFunctions.DateAdd("day", r.GG_massimi_data_notifica + a.gg_sospensione_trasmessi, a.data_ricezione.HasValue ? a.data_ricezione.Value : a.data_avvenuta_notifica.Value) < p_dataEmissione)))
                    .WhereNonRateizzataOrAnnullata()
                    .WhereNonRottamata()
                    .WhereNonInIspezioneValida()
                    //.WhereNonPrescrittaNew(p_dataEmissione, 0, 7)
                    .Where(a => !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2")) //contribuente con stato valido ai fini dell'emissione                                                                                                                                                               
                    .Where(a => a.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => coa.tab_avv_pag.flag_rateizzazione_bis.Equals("1") && !coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)))
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO)))

                    .Where(i => !i.tab_unita_contribuzione_fatt_emissione.Any(a => a.tab_avv_pag_fatt_emissione.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)
                                            && (a.tab_avv_pag_fatt_emissione.tab_liste.cod_stato.StartsWith(tab_liste.PRE) && !a.tab_avv_pag_fatt_emissione.tab_liste.cod_stato.Equals(tab_liste.PRE_PRE))))//Non prende le ingiunzioni presenti in un'altra lista in corso di emissione

                    //scarta le ingiunzioni finite in atti successivi (per i servizi elencati: Pignoramenti) non annullati e notificati
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => p_serviziscartoListCoattivo.Contains(coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio)
                                                                      && !coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)
                                                                      && coa.tab_avv_pag.flag_esito_sped_notifica.Equals("1")))

                    //Scarta le ingiunzioni che sono contenute in intimazioni non annullate (escluso annullato per scadenza (vecchia gestione forzatura)) 
                    // e intimazioni non notificate ma emesse da poco (notifiche ancora non scaricate)
                    // o intimazioni notificate da meno di x giorni (non prossime alla scadenza)
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM
                                                                    && (!coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) || coa.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.ANNULLATO_INTIMAZIONE_SCADUTA))
                                                                    && (
                                                                        (!coa.tab_avv_pag.flag_esito_sped_notifica.Equals("1") && coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione && (SqlFunctions.DateAdd("day", r.GG_minimi_data_notifica, coa.tab_avv_pag.dt_emissione.Value) > p_dataEmissione)))
                                                                        ||
                                                                        (coa.tab_avv_pag.flag_esito_sped_notifica.Equals("1") && coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione && (SqlFunctions.DateAdd("day", r.GG_massimi_data_notifica, coa.tab_avv_pag.data_ricezione.Value) > p_dataEmissione)))
                                                                       )
                                                                  )
                          )
                    //Scarta le ingiunzioni tenendo conto di una eventuale intimazione emesse dall'ente notificate da meno di x giorni (non prossime alla scadenza)
                    .Where(a => !(a.flag_tipo_atto_successivo == "2" && a.data_avviso_bonario != null && p_dataOdiernaIntimazioniRif > SqlFunctions.DateAdd("day", a.gg_sospensione_trasmessi, a.data_avviso_bonario.Value)))

                    //Scarta le ingiunzioni finite in solleciti emessi da meno di x giorni
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA
                                        && !coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)
                                        && (
                                            coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione && (SqlFunctions.DateAdd("day", r.GG_minimi_data_notifica, coa.tab_avv_pag.dt_emissione.Value) > p_dataEmissione))))
                                           )

                    //Verifica sui Fermi/Ipoteca da preavvisi di ipoteca tenendo conto della validità
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => (coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                                                          && coa.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V2.Where(s => s.COD_STATO == TAB_SUPERVISIONE_FINALE_V2.VAL_VAL).Any(p => p.id_avvpag_preavviso_collegato != null))
                                        && !coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)
                                        && (
                                            coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione && (SqlFunctions.DateAdd("day", r.GG_massimi_data_emissione, coa.tab_avv_pag.dt_emissione.Value) > p_dataEmissione))))
                                           )

                    //Scarta le ingiunzioni in avvisi emessi di recente e ancora nei termini di pagamento
                    .Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => !coa.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)
                                                                    && (coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT)
                                                                    && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_definitivita_avviso.Value) >= p_dataEmissione))))
                   ;

            return v_avvisi;
        }

        public static IQueryable<tab_avv_pag> SelezionaAttiPerIngiunzioni(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view p_modalitaRate, DateTime p_dataEmissione, DateTime p_dataOdiernaRif, List<Int32> v_idTipiAvvPagDaAccertareList)
        {
            string flag_natura_entrata = p_modalitaRate.anagrafica_tipo_avv_pag.flag_natura_avv_collegati;

            IQueryable<tab_avv_pag> v_avvisi = p_query
                        .WhereByCodStato(CodStato.VAL)
                        .WhereByImportoDaPagare(p_modalitaRate.importo_minimo_avvpag_collegati)
                        .WhereByValido()
                        .Where(a => !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))

                        //Elimina gli avvisi contenuti in una ingiunzione non annullata
                        .Where(a => !a.tab_unita_contribuzione1.Any(u => !u.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)
                                                                      && u.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC
                                                                      && (u.tab_avv_pag.flag_esito_sped_notifica == "1"
                                                                          ||
                                                                         (u.tab_avv_pag.flag_esito_sped_notifica == null &&
                                                                          u.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione
                                                                                                                                                  && (SqlFunctions.DateAdd("day", r.GG_massimi_data_emissione.Value, u.tab_avv_pag.dt_emissione.Value) > p_dataEmissione)))
                                                                       )))

                        //Scarta gli avvisi contenuti in un atto successivo di riscossione pre coattiva (servizio 2) valido entro i termini di pagamento o sospesi per istanza
                        .Where(a => !a.tab_unita_contribuzione1.Any(u => (u.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.RISC_PRECOA && u.tab_avv_pag.anagrafica_tipo_avv_pag.flag_notifica == "0")
                                                                        && !(u.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) ||
                                                                        (u.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)
                                                                            && u.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione
                                                                                                                            && (SqlFunctions.DateAdd("day", r.GG_massimi_data_emissione.Value, u.tab_avv_pag.dt_emissione.Value) < p_dataEmissione))
                                                                            )
                                                                            )
                                                                        )
                                                                    )

                        // Scarta gli accertamenti ancora nei termini di pagamento
                        .Where(a => a.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= p_dataEmissione && r.periodo_validita_a > p_dataEmissione
                                && a.flag_esito_sped_notifica.Equals("1") && a.data_ricezione.HasValue
                                && (SqlFunctions.DateAdd("day", r.GG_minimi_data_notifica.Value, a.data_ricezione.Value) < p_dataEmissione)))

                        //.WhereNonPrescrittaPerIngiunzione(p_dataOdiernaRif, p_dataEmissione)
                        .WhereNonRateizzataOrAnnullata()
                        .WhereNonRottamata()

                        /*
                        .Where(a => (!a.tab_rata_avv_pag.Any() || (a.tab_rata_avv_pag.Any(r => !r.cod_stato.StartsWith(CodStato.ANN) &&
                                               r.num_rata == a.tab_rata_avv_pag.Where(rr => !rr.cod_stato.StartsWith(CodStato.ANN)).Max(rr => rr.num_rata) &&
                                                                                    r.dt_scadenza_rata <= p_dataScadenza))));
                        */
                        ;

            if (v_idTipiAvvPagDaAccertareList != null)
            {
                v_avvisi = v_avvisi.Where(a => v_idTipiAvvPagDaAccertareList.Contains(a.id_tipo_avvpag))
                    ;
            }
            else
            {
                v_avvisi = v_avvisi
                       .Where(a => a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERTAMENTO || a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.RISC_PRECOA ||
                                   a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM ||
                                   a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO)
                       ;
            }

            return v_avvisi;
        }

        public static IQueryable<tab_avv_pag> SelezionaAttiPerSollecitoBonario(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view p_modalitaRate, DateTime p_dataOdiernaRif, DateTime p_dataScadenza, List<Int32> v_idTipiAvvPagDaAccertareList)
        {

            string flag_natura_entrata = p_modalitaRate.anagrafica_tipo_avv_pag.flag_natura_avv_collegati;

            IQueryable<tab_avv_pag> v_avvisi = p_query;
            if (v_idTipiAvvPagDaAccertareList != null)
            {
                v_avvisi = v_avvisi.Where(a => v_idTipiAvvPagDaAccertareList.Contains(a.id_tipo_avvpag));
            }
            else
            {
                v_avvisi = v_avvisi.Where(a => a.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO);
            }

            v_avvisi = v_avvisi.Where(a => v_idTipiAvvPagDaAccertareList.Contains(a.id_tipo_avvpag))
                .WhereByCodStato(CodStato.VAL)
                .WhereByImportoDaPagare(p_modalitaRate.importo_minimo_avvpag_collegati)
                .WhereByValido()
                .Where(a => a.anagrafica_stato.flag_validita.Equals("1"))
                .Where(a => a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO || (a.flag_esito_sped_notifica.Equals("1") && a.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_SI && p_dataOdiernaRif > a.data_ricezione.Value))
                .WhereNonRateizzataOrAnnullata()
                .WhereNonRottamata()
                .Where(a => !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !a.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                .Where(a => (!a.tab_rata_avv_pag.Any() || (a.tab_rata_avv_pag.Any(r => !r.cod_stato.StartsWith(CodStato.ANN) &&
                                        r.num_rata == a.tab_rata_avv_pag.Where(rr => !rr.cod_stato.StartsWith(CodStato.ANN)).Max(rr => rr.num_rata) &&
                                                                            r.dt_scadenza_rata <= p_dataScadenza))))
                .Where(a => !a.tab_unita_contribuzione1.Any(s => !s.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)));




            return v_avvisi;
        }

        public static IQueryable<tab_avv_pag> WhereIngiunzioneNonInPreavvisoFermoNonAndatoAFermo(this IQueryable<tab_avv_pag> p_query)
        {
            DateTime v_dataCorrente = DateTime.Now;

            return p_query.Where(a => a.TAB_JOIN_AVVCOA_INGFIS_V21.Where(coa => (
                                                                                   //Per i preavvisi di fermo considera validi solo gli atti non annullati,
                                                                                   //Notificati o se non notificati, emessi da poco tempo,
                                                                                   //Non emessi da più di 365 giorni
                                                                                   //::Se l'ingiunzione non è in un avviso valido viene presa
                                                                                   (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM  //Condizioni sul Preavviso
                                                                                     && (coa.tab_avv_pag.flag_esito_sped_notifica == "1" // Con queste 2 righe prende gli atti notificati o emessi da poco tempo sui quali non sono ancora state scaricate le notifiche
                                                                                         || ((coa.tab_avv_pag.flag_esito_sped_notifica == "0" || coa.tab_avv_pag.flag_esito_sped_notifica == null) && coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT) && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_massimi_data_emissione.Value) > v_dataCorrente)))
                                                                                     && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, 365) >= v_dataCorrente // Da un massimo di 365 giorni per convertire un fermo
                                                                                   )
                                                                              ||
                                                                                 //Per i Fermi considera validi solo quelli che sono ancora nei termini di pagamento
                                                                                 (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                                                                                  && (coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT) && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_scadenza_pagamento.Value) > v_dataCorrente))
                                                                                 )
                                                                              )
                                                                              &&
                                                                              //Scarta gli atti successivi annullati
                                                                              !coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN))
                                                                              .Count() == 0 //L'ingiunzione per essere presa da questa condizione non deve essere in atti successivi validi per i filtri precedenti
                                                                                            //Se l'ingiunzione è contenuta in un atto successivo valido, la prende solo se l'ultimo emesso è un fermo  
                                 || a.TAB_JOIN_AVVCOA_INGFIS_V21.Where(coa => (
                                                                                   //Per i preavvisi di fermo considera validi solo gli atti non annullati,
                                                                                   //Notificati o se non notificati, emessi da poco tempo,
                                                                                   //Non emessi da più di 365 giorni
                                                                                   //::Se l'ingiunzione non è in un avviso valido viene presa
                                                                                   (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM  //Condizioni sul Preavviso
                                                                                     && (coa.tab_avv_pag.flag_esito_sped_notifica == "1" // Con queste 2 righe prende gli atti notificati o emessi da poco tempo sui quali non sono ancora state scaricate le notifiche
                                                                                         || (coa.tab_avv_pag.flag_esito_sped_notifica != "1" && coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT) && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_massimi_data_emissione.Value) > v_dataCorrente)))
                                                                                     && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, 365) >= v_dataCorrente // Da un massimo di 365 giorni per convertire un fermo
                                                                                   )
                                                                              ||
                                                                                 //Per i Fermi considera validi solo quelli che sono ancora nei termini di pagamento
                                                                                 coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO
                                                                              )
                                                                              &&
                                                                              !coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN)
                                                                            ).OrderByDescending(o => o.tab_avv_pag.dt_emissione).FirstOrDefault().tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.FERMO_AMMINISTRATIVO);
        }

        public static IQueryable<tab_avv_pag> WhereIngiunzioneNonInPreavvisoIpotecaNonAndatiAIpoteca(this IQueryable<tab_avv_pag> p_query)
        {
            DateTime v_dataCorrente = DateTime.Now;

            return p_query.Where(a => a.TAB_JOIN_AVVCOA_INGFIS_V21.Where(coa => (
                                                                                   //Per i preavvisi di fermo considera validi solo gli atti non annullati,
                                                                                   //Notificati o se non notificati, emessi da poco tempo,
                                                                                   //Non emessi da più di 365 giorni
                                                                                   //::Se l'ingiunzione non è in un avviso valido viene presa
                                                                                   (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA  //Condizioni sul Preavviso
                                                                                     && (coa.tab_avv_pag.flag_esito_sped_notifica == "1" // Con queste 2 righe prende gli atti notificati o emessi da poco tempo sui quali non sono ancora state scaricate le notifiche
                                                                                         || (coa.tab_avv_pag.flag_esito_sped_notifica != "1" && coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT) && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_massimi_data_emissione.Value) > v_dataCorrente)))
                                                                                     && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, 365) >= v_dataCorrente // Da un massimo di 365 giorni per convertire un fermo
                                                                                   )
                                                                              ||
                                                                                 //Per i Fermi considera validi solo quelli che sono ancora nei termini di pagamento
                                                                                 (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.IPOTECA
                                                                                  && (coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT) && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_scadenza_pagamento.Value) > v_dataCorrente))
                                                                                 )
                                                                              )
                                                                              &&
                                                                              //Scarta gli atti successivi annullati
                                                                              !coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN))
                                                                              .Count() == 0 //L'ingiunzione per essere presa da questa condizione non deve essere in atti successivi validi per i filtri precedenti
                                                                                            //Se l'ingiunzione è contenuta in un atto successivo valido, la prende solo se l'ultimo emesso è un fermo  
                                 || a.TAB_JOIN_AVVCOA_INGFIS_V21.Where(coa => (
                                                                                   //Per i preavvisi di fermo considera validi solo gli atti non annullati,
                                                                                   //Notificati o se non notificati, emessi da poco tempo,
                                                                                   //Non emessi da più di 365 giorni
                                                                                   //::Se l'ingiunzione non è in un avviso valido viene presa
                                                                                   (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA  //Condizioni sul Preavviso
                                                                                     && (coa.tab_avv_pag.flag_esito_sped_notifica == "1" // Con queste 2 righe prende gli atti notificati o emessi da poco tempo sui quali non sono ancora state scaricate le notifiche
                                                                                         || (coa.tab_avv_pag.flag_esito_sped_notifica != "1" && coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT) && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_massimi_data_emissione.Value) > v_dataCorrente)))
                                                                                     && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, 365) >= v_dataCorrente // Da un massimo di 365 giorni per convertire un fermo
                                                                                   )
                                                                              ||
                                                                                 //Per i Fermi considera validi solo quelli che sono ancora nei termini di pagamento
                                                                                 coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.IPOTECA
                                                                              )
                                                                              &&
                                                                              !coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN)
                                                                            ).OrderByDescending(o => o.tab_avv_pag.dt_emissione).FirstOrDefault().tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.IPOTECA);
        }

        public static IQueryable<tab_avv_pag> WhereIngiunzioneInAttoSuccessivoAncoraPagabile(this IQueryable<tab_avv_pag> p_query)
        {
            DateTime v_dataCorrente = DateTime.Now;

            return p_query.Where(a => !a.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => !coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN)// Scarta le ingiunzioni in avvisi emessi di recente e ancora nei termini di pagamento
                                                                            && (coa.tab_avv_pag.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any(r => r.periodo_validita_da <= v_dataCorrente && r.periodo_validita_a > v_dataCorrente && r.cod_stato.Equals(tab_modalita_rate_avvpag_view.ATT_ATT)
                                                                            && System.Data.Entity.DbFunctions.AddDays(coa.tab_avv_pag.dt_emissione.Value, r.GG_definitivita_avviso.Value) >= v_dataCorrente))));
        }

        public static IQueryable<tab_avv_pag> WhereIngiunzioneSollecitata(this IQueryable<tab_avv_pag> p_query, bool p_emissioneSecondoSollecito, List<int> p_serviziEquivalentiSollecito)
        {
            return p_query.Where(i => i.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => (coa.tab_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.SOLLECITO_POST_INGIUNZIONE_G && !coa.tab_avv_pag.cod_stato.StartsWith(CodStato.ANN))
                                                                                                                            || !p_emissioneSecondoSollecito))
                          .Where(i => i.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => (coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA && !coa.tab_avv_pag.cod_stato.StartsWith(CodStato.ANN))
                                                                               || i.dati_avviso_bonario != null)
                                   || i.TAB_JOIN_AVVCOA_INGFIS_V21.Any(coa => p_serviziEquivalentiSollecito.Contains(coa.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio) && (!coa.tab_avv_pag.cod_stato.Contains(CodStato.ANN) || coa.tab_avv_pag.cod_stato == anagrafica_stato_avv_pag.ANNULLATO_INTIMAZIONE_SCADUTA)
                                                                          && (coa.tab_avv_pag.data_ricezione != null || coa.tab_avv_pag.data_avvenuta_notifica != null)));
        }

        public static IQueryable<tab_avv_pag> WhereNonInIspezioneValida(this IQueryable<tab_avv_pag> p_query)
        {
            return p_query.Where(i => !i.tab_ingiunzioni_ispezione.Any(t => t.join_ispezioni_ingiunzioni.Any(j => j.tab_ispezioni_coattivo_new.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL)));
        }

        public static IQueryable<tab_avv_pag> SelezionaAttiPerAccertamentoAOP(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view p_modalitaRate,
                                                                              DateTime p_dataEmissione, int p_annoRif, int p_idEntrata, List<int> v_idTipiAvvPagDaAccertareList)
        {
            IQueryable<tab_avv_pag> v_avvisi;

            if (p_idEntrata != anagrafica_entrate.IMU && p_idEntrata != anagrafica_entrate.TASI )
            { 
                List<int> v_lista_tipi_con_notifica = new List<int>() { anagrafica_entrate.TARI, anagrafica_entrate.TARSU };

                v_avvisi = p_query
                      .WhereByImportoDaPagare(p_modalitaRate.importo_minimo_avvpag_collegati)
                      .WhereByValido()
                      .Where(avv => !avv.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !avv.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                      .Where(avv => avv.id_entrata == p_idEntrata)
                      .Where(avv => avv.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                      .Where(avv => !avv.cod_stato.Contains(anagrafica_stato_avv_pag.VAL_AOP))
                      .Where(avv => avv.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
                      //.Where(avv => avv.anagrafica_tipo_avv_pag.flag_tipo_composto != anagrafica_tipo_avv_pag.FLAG_TIPO_COMPOSTO)
                      .Where(avv =>
                                 (
                                    avv.anagrafica_tipo_avv_pag.flag_notifica == anagrafica_tipo_avv_pag.FLAG_NOTIFICA_NO &&
                                    !v_lista_tipi_con_notifica.Contains(avv.id_entrata) ?
                                    String.IsNullOrEmpty(avv.flag_esito_sped_notifica) :
                                    avv.flag_esito_sped_notifica == tab_avv_pag.FLAG_ESITO_NOTIFICATO
                                 )
                            )
                      .Where(avv => avv.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any
                                 (
                                    rate => rate.periodo_validita_da <= p_dataEmissione &&
                                    rate.periodo_validita_a > p_dataEmissione &&
                                    (SqlFunctions.DateAdd("day", rate.GG_massimi_data_emissione.Value, avv.dt_emissione.Value) < p_dataEmissione)
                                 )
                            )
                      .Where(avv => avv.tab_unita_contribuzione.Any
                                                                   (
                                                                        unita =>
                                                                        unita.anno_rif == p_annoRif.ToString() &&
                                                                        unita.id_entrata == p_idEntrata &&
                                                                        (
                                                                            unita.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                            ||
                                                                            unita.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_AVV_COLLEGATO)
                                                                        )
                                                                   ));


            }
            else
            {
                v_avvisi = p_query
                      .WhereByImportoDaPagare(p_modalitaRate.importo_minimo_avvpag_collegati)
                      .WhereByValido()
                      .Where(avv => !avv.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !avv.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                      .Where(avv => avv.id_entrata == p_idEntrata)
                      .Where(avv => avv.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                      .Where(avv => !avv.cod_stato.Contains(anagrafica_stato_avv_pag.VAL_AOP))
                      .Where(avv => avv.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
                      //.Where(avv => avv.anagrafica_tipo_avv_pag.flag_tipo_composto != anagrafica_tipo_avv_pag.FLAG_TIPO_COMPOSTO)
                      .Where(avv => avv.tab_unita_contribuzione.Any
                                                                   (
                                                                        unita =>
                                                                        unita.anno_rif == p_annoRif.ToString() &&
                                                                        unita.id_entrata == p_idEntrata &&
                                                                        (
                                                                            unita.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_ENT)
                                                                            ||
                                                                            unita.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(tab_tipo_voce_contribuzione.CODICE_AVV_COLLEGATO)
                                                                        )
                                                                   ));


            }
            
            if (v_idTipiAvvPagDaAccertareList != null)
            {
                v_avvisi = v_avvisi.Where(avv => v_idTipiAvvPagDaAccertareList.Contains(avv.id_tipo_avvpag));
            }

            return v_avvisi;
        }

        public static IQueryable<tab_avv_pag> SelezionaAttiAccertAOP_POP(this IQueryable<tab_avv_pag> p_query, tab_modalita_rate_avvpag_view p_modalitaRate,
                                                                         DateTime p_dataEmissione, List<int> p_idListaPOP, int p_idEntrata, List<int> v_idTipiAvvPagDaAccertareList)
        {
            IQueryable<tab_avv_pag> v_avvisi = p_query
                      .WhereByImportoDaPagare(p_modalitaRate.importo_minimo_avvpag_collegati)
                      .WhereByValido()
                      .Where(avv => !avv.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("1") && !avv.tab_contribuente.anagrafica_stato_contribuente.flag_validita.Equals("2"))
                      .Where(avv => avv.id_entrata == p_idEntrata)
                      .Where(avv => p_idListaPOP.Contains(avv.id_lista_emissione ?? 0))
                      .Where(avv => avv.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                      .Where(avv => !avv.cod_stato.Contains(anagrafica_stato_avv_pag.VAL_AOP))
                      .Where(avv => avv.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA)
                      .Where(avv => avv.anagrafica_tipo_avv_pag.flag_tipo_composto == anagrafica_tipo_avv_pag.FLAG_TIPO_COMPOSTO)
                      .Where(avv => avv.flag_esito_sped_notifica == tab_avv_pag.FLAG_ESITO_NOTIFICATO)
                      .Where(avv => avv.anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.Any
                                 (
                                    rate => rate.periodo_validita_da <= p_dataEmissione &&
                                    rate.periodo_validita_a > p_dataEmissione &&
                                    (SqlFunctions.DateAdd("day", rate.GG_massimi_data_emissione.Value, avv.dt_emissione.Value) < p_dataEmissione)
                                 )
                            );

            if (v_idTipiAvvPagDaAccertareList != null)
            {
                v_avvisi = v_avvisi.Where(avv => v_idTipiAvvPagDaAccertareList.Contains(avv.id_tipo_avvpag));
            }

            return v_avvisi;
        }
    }
}
