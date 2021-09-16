using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinAvvPagSoggettoDebitoreLinq
    {
        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdTerzo(this IQueryable<join_avv_pag_soggetto_debitore> p_query, int p_idTerzo)
        {
            return p_query.Where(d => d.tab_terzo.id_terzo == p_idTerzo);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdTabAvvPag(this IQueryable<join_avv_pag_soggetto_debitore> p_query, int p_idTabAvvPag)
        {
            return p_query.Where(d => d.id_tab_avv_pag == p_idTabAvvPag);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdTabReferenteNotNull(this IQueryable<join_avv_pag_soggetto_debitore> p_query)
        {
            return p_query.Where(d => d.id_join_referente_contribuente != null);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdTabTerzoDebitoreNotNull(this IQueryable<join_avv_pag_soggetto_debitore> p_query)
        {
            return p_query.Where(d => d.id_terzo_debitore != null);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByCodStato(this IQueryable<join_avv_pag_soggetto_debitore> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_stato.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByCodStato(this IQueryable<join_avv_pag_soggetto_debitore> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.tab_avv_pag.anagrafica_stato.cod_stato));
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdEntrateList(this IQueryable<join_avv_pag_soggetto_debitore> p_query, IList<int> p_idEntrateList)
        {
            return p_query.Where(d => p_idEntrateList.Contains(d.tab_avv_pag.id_entrata));
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdServizio(this IQueryable<join_avv_pag_soggetto_debitore> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == p_idServizio);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdentificativo(this IQueryable<join_avv_pag_soggetto_debitore> p_query, string p_identificativo)
        {
            return p_query.Where(d => d.tab_avv_pag.identificativo_avv_pag.ToUpper().Trim() == p_identificativo.ToUpper().Trim());
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdEntrata(this IQueryable<join_avv_pag_soggetto_debitore> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.tab_avv_pag.id_entrata == p_idEntrata);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdTipoAvvPag(this IQueryable<join_avv_pag_soggetto_debitore> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.tab_avv_pag.id_tipo_avvpag == p_idTipoAvvPag);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByNotRateizzato(this IQueryable<join_avv_pag_soggetto_debitore> p_query)
        {
            return p_query.Where(d => d.tab_avv_pag.flag_rateizzazione_bis == "0" ||
                                      d.tab_avv_pag.flag_rateizzazione_bis == "9" ||
                                      d.tab_avv_pag.flag_rateizzazione_bis == null);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByIdentificativoAvviso(this IQueryable<join_avv_pag_soggetto_debitore> p_query, string p_identificativo)
        {
            string p_identificativo2 = !string.IsNullOrEmpty(p_identificativo) ? p_identificativo.Replace("/", string.Empty).Replace("-", string.Empty).Trim() : string.Empty;
            string v_codice = string.Empty;
            string v_anno = string.Empty;
            string v_progressivo = string.Empty;

            if (!string.IsNullOrEmpty(p_identificativo2) &&
                p_identificativo2.Length > 8)
            {
                v_codice = p_identificativo2.Substring(0, 4);
                v_anno = p_identificativo2.Substring(4, 4);
                if (p_identificativo2.Substring(8).All(char.IsDigit))
                {
                    v_progressivo = Convert.ToInt32(p_identificativo2.Substring(8)).ToString();
                }
            }

            return p_query.Where(d => d.tab_avv_pag.identificativo_avv_pag.Trim() == p_identificativo || (d.tab_avv_pag.anagrafica_tipo_avv_pag.cod_tipo_avv_pag == v_codice &&
                                                                                                          d.tab_avv_pag.anno_riferimento == v_anno &&
                                                                                                          d.tab_avv_pag.numero_avv_pag == v_progressivo));
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> WhereByCodStatoRiferimento(this IQueryable<join_avv_pag_soggetto_debitore> p_query, List<string> p_codStatoList, string p_codStato)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento) ||
                                      d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.StartsWith(p_codStato));
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> OrderByDataEmissione(this IQueryable<join_avv_pag_soggetto_debitore> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<join_avv_pag_soggetto_debitore> OrderByDataEmissioneDesc(this IQueryable<join_avv_pag_soggetto_debitore> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_avv_pag.dt_emissione);
        }

        public static IList<join_avv_pag_soggetto_debitore_light> ToLight(this IQueryable<join_avv_pag_soggetto_debitore> iniziale)
        {
            return iniziale.ToList().Select(d => new join_avv_pag_soggetto_debitore_light
            {
                id_join_avv_pag_soggetto_debitore = d.id_join_avv_pag_soggetto_debitore,

                id_tab_avv_pag = d.tab_avv_pag.id_tab_avv_pag,
                NumeroAvviso = d.tab_avv_pag.identificativo_avv_pag,
                dt_emissione_String = d.tab_avv_pag.dt_emissione_String,
                dt_emissione = d.tab_avv_pag.dt_emissione,
                imp_tot_avvpag_rid = d.tab_avv_pag.imp_tot_avvpag_rid_decimal,
                imponibile = d.tab_avv_pag.imponibile,
                iva = d.tab_avv_pag.iva,
                Importo = d.tab_avv_pag.Importo,
                importoSpeseNotificaDecimal = d.tab_avv_pag.importoSpeseNotificaDecimal,
                importoSpeseCoattiveDecimal = d.tab_avv_pag.importoSpeseCoattiveDecimal,
                ImportoSpeseNotifica = d.tab_avv_pag.ImportoSpeseNotifica,
                ImportoSpeseCoattive = d.tab_avv_pag.ImportoSpeseCoattive,
                Rate = d.tab_avv_pag.Rate,
                Targa = d.tab_avv_pag.flag_iter_recapito_notifica,
                SpeditoNotificato = d.tab_avv_pag.SpeditoNotificato,
                imp_tot_pagato = d.tab_avv_pag.imp_tot_pagato_decimal,
                imp_tot_pagato_Euro = d.tab_avv_pag.imp_tot_pagato_Euro,
                importo_tot_da_pagare = d.tab_avv_pag.importo_tot_da_pagare_decimal,
                ImportoDaPagare = d.tab_avv_pag.ImportoDaPagare,
                stato = d.tab_avv_pag.DescrizioneStatoNew,
                cod_stato = d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento,
                codStatoReale = d.tab_avv_pag.cod_stato,
                Adesione = d.tab_avv_pag.Adesione,
                TipoBene = d.tab_avv_pag.TipoBene,
                id_tab_supervisione_finale = d.tab_avv_pag.id_tab_supervisione_finale.HasValue ? d.tab_avv_pag.id_tab_supervisione_finale.Value : -1,
                IntimazioneCorrelata = d.tab_avv_pag.IntimazioneCorrelata,
                impRidottoPerAdesione = d.tab_avv_pag.impRidottoPerAdesione,
                id_avvpag_preavviso_collegato = (d.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V21 != null && d.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue) ? d.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.Value : -1,
                IsIstanzaVisibile = d.tab_avv_pag.IsIstanzaVisibile,
                ExistsAtti = d.tab_avv_pag.ExistsAtti,
                ExistsAttiIntimSoll = d.tab_avv_pag.ExistsAttiIntimSoll,
                ExistsAttiCoattivi = d.tab_avv_pag.ExistsAttiCoattivi,
                IsFatturazioneAcqua = d.tab_avv_pag.IsFatturazioneAcqua,
                ExistsOrdineOrigine = d.tab_avv_pag.ExistsOrdineOrigine,
                IdAvvPagPreColl = d.tab_avv_pag.ExistsOrdineOrigine ? d.tab_avv_pag.TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.Value : -1,
                soggettoDebitore = d.tab_avv_pag.SoggettoDebitore,
                soggettoDebitoreTerzo = d.tab_avv_pag.SoggettoDebitoreTerzo,
                Contribuente = (d.tab_avv_pag != null && d.tab_avv_pag.tab_contribuente != null) ? d.tab_avv_pag.tab_contribuente.contribuenteDisplay : string.Empty,
                DescrizioneTipoAvviso = d.tab_avv_pag.DescrizioneTipoAvviso,
                SpeditoRicezione = d.tab_avv_pag.SpeditoRicezione,
                flag_spedizione_notifica = d.tab_avv_pag.flag_spedizione_notifica,
                ExistsAttiSuccessivi = d.tab_avv_pag.ExistsAttiSuccessivi,
                ExistsIspezioni = d.tab_avv_pag.ExistsIspezioni,
                IsVisibleAtti = d.tab_avv_pag.IsVisibleAtti,
                IsVisibleBene = d.tab_avv_pag.IsVisibleBene,
                IsVisibleAcqua = d.tab_avv_pag.IsVisibleAcqua,
                id_tipo_avvpag = d.tab_avv_pag.id_tipo_avvpag,
                ImportoAttiSuccessivi = d.tab_avv_pag.importo_atti_successivi_decimal,
                ImportoAttiSuccessivi_Euro = d.tab_avv_pag.importo_atti_successivi_Euro,
                importo_sanzioni_eliminate_eredi = d.tab_avv_pag.importo_sanzioni_eliminate_eredi_decimal,
                importo_sanzioni_eliminate_eredi_Euro = d.tab_avv_pag.importo_sanzioni_eliminate_eredi_Euro,
                interessi_eliminati_definizione_agevolata = d.tab_avv_pag.interessi_eliminati_definizione_agevolata_decimal,
                interessi_eliminati_definizione_agevolata_Euro = d.tab_avv_pag.interessi_eliminati_definizione_agevolata_Euro,
                sanzioni_eliminate_definizione_agevolata = d.tab_avv_pag.sanzioni_eliminate_definizione_agevolata_decimal,
                sanzioni_eliminate_definizione_agevolata_Euro = d.tab_avv_pag.sanzioni_eliminate_definizione_agevolata_Euro,
                importo_definizione_agevolata_eredi_decimal = d.tab_avv_pag.importo_definizione_agevolata_eredi_decimal,
                importo_definizione_agevolata_eredi_Euro = d.tab_avv_pag.importo_definizione_agevolata_eredi_Euro
            }).ToList();
        }
    }
}
