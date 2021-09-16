using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAvvPagFattEmissioneLinq
    {
        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByIdContribuenteAndIdTipo(this IQueryable<tab_avv_pag_fatt_emissione> p_query, Decimal p_idContribuente, int p_idTipoAvvPag)
        {
            return p_query.Where(w => w.id_anag_contribuente == p_idContribuente && w.id_tipo_avvpag == p_idTipoAvvPag);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByIdListaEmissione(this IQueryable<tab_avv_pag_fatt_emissione> p_query, int p_idListaEmissione)
        {
            return p_query.Where(w => w.id_lista_emissione == p_idListaEmissione);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByIdSupervisioneFinale(this IQueryable<tab_avv_pag_fatt_emissione> p_query, int p_idSupervisioneFinale)
        {
            return p_query.Where(d => d.id_tab_supervisione_finale == p_idSupervisioneFinale);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByContribuenteIdTipoContribuente(this IQueryable<tab_avv_pag_fatt_emissione> p_query, int p_idTipoContribuente)
        {
            return p_query.Where(w => w.tab_contribuente.id_tipo_contribuente == p_idTipoContribuente);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByCodStato(this IQueryable<tab_avv_pag_fatt_emissione> p_query, string p_codStato)
        {
            return p_query.Where(w => w.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByCodStatoStartsWith(this IQueryable<tab_avv_pag_fatt_emissione> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByCodStatoStartsWithNot(this IQueryable<tab_avv_pag_fatt_emissione> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> OrderByDefault(this IQueryable<tab_avv_pag_fatt_emissione> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_avv_pag);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByIdContribuente(this IQueryable<tab_avv_pag_fatt_emissione> p_query, Decimal p_idContribuente)
        {
            return p_query.Where(w => w.tab_contribuente.id_anag_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByIdAvvPag(this IQueryable<tab_avv_pag_fatt_emissione> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_tab_avv_pag == p_idAvvPag);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> NonNotificati(this IQueryable<tab_avv_pag_fatt_emissione> p_query)
        {
            return p_query.Where(afe => !afe.data_avvenuta_notifica.HasValue);
        }

        public static IQueryable<tab_avv_pag_fatt_emissione> WhereByValido(this IQueryable<tab_avv_pag_fatt_emissione> p_query, string p_codStato)
        {
            //Il dottore ha voluto cambiare il filtro da "VAL-EME" a "VAL-" con flag_validita=1, inoltre il controllo si fa su cod_stato_riferimento
            if (p_codStato.Equals(anagrafica_stato_avv_pag.VALIDO) || p_codStato.Equals(anagrafica_stato_avv_pag.VAL_EME))
            {
                return p_query.Where(d => d.anagrafica_stato_avv_pag1.cod_stato_riferimento.StartsWith(p_codStato) && d.anagrafica_stato_avv_pag1.flag_validita == "1");
            }
            else
            {
                return p_query.Where(d => d.anagrafica_stato_avv_pag1.cod_stato_riferimento.Contains(p_codStato));
            }
        }

        public static IQueryable<tab_avv_pag_fatt_emissione_light> ToLight(this IQueryable<tab_avv_pag_fatt_emissione> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_avv_pag_fatt_emissione_light
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
                stato = d.stato,
                cod_stato = d.anagrafica_stato_avv_pag1.cod_stato_riferimento,
                codStatoReale = d.cod_stato,
                Adesione = d.Adesione,
                TipoBene = d.TipoBene,
                id_tab_supervisione_finale = d.id_tab_supervisione_finale.HasValue ? d.id_tab_supervisione_finale.Value : -1,
                IntimazioneCorrelata = d.IntimazioneCorrelata,
                impRidottoPerAdesione = d.impRidottoPerAdesione,
                id_avvpag_preavviso_collegato = (d.TAB_SUPERVISIONE_FINALE_V2 != null && d.TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato.HasValue) ? d.TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato.Value : -1,
                IsIstanzaVisibile = false,
                ExistsAtti = false,
                ExistsAttiIntimSoll = false,
                ExistsAttiCoattivi = false,
                ExistsAttiSuccessivi = false,
                ExistsIspezioni = false,
                IsFatturazioneAcqua = d.IsFatturazioneAcqua,
                ExistsOrdineOrigine = d.ExistsOrdineOrigine,
                ExistsProceduraConcorsuale = false,
                IdAvvPagPreColl = d.ExistsOrdineOrigine ? d.TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato.Value : -1,
                soggettoDebitore = string.Empty,
                soggettoDebitoreTerzo = string.Empty,
                Contribuente = d.tab_contribuente != null ? d.tab_contribuente.contribuenteDisplay : string.Empty,
                IsIstanzaPresentabile = false,
                dataMassimaPresentazioneIstanza = string.Empty,
                IsAvvisoPagabile = false,
                dataMassimaPagamentoAvviso = string.Empty,
                data_ricezione_String = string.Empty,
                data_ricezione = d.data_ricezione,
                avvisoBonario = string.Empty,
                IsProvvedimentoPresentabile = false,
                importo_sgravio = d.importo_sgravio,
                importo_sgravio_Euro = d.importo_sgravio_Euro,
                color = "",
                IsAvvisoSgravabile = false,
                IsAvvisoStatoAnnRetDanDar = false,
                IsAvvisoStatoAnnDan = false,
                DescrizioneTipoAvviso = d.DescrizioneTipoAvviso,
                SpeditoRicezione = d.SpeditoRicezione,
                ImportoAttiSuccessivi = null,
                ImportoAttiSuccessivi_Euro = string.Empty,
                importo_sanzioni_eliminate_eredi = d.importo_sanzioni_eliminate_eredi_decimal,
                importo_sanzioni_eliminate_eredi_Euro = d.importo_sanzioni_eliminate_eredi_Euro,
                interessi_eliminati_definizione_agevolata = d.interessi_eliminati_definizione_agevolata_decimal,
                interessi_eliminati_definizione_agevolata_Euro = d.interessi_eliminati_definizione_agevolata_Euro,
                sanzioni_eliminate_definizione_agevolata = d.sanzioni_eliminate_definizione_agevolata_decimal,
                sanzioni_eliminate_definizione_agevolata_Euro = d.sanzioni_eliminate_definizione_agevolata_Euro,
                importo_definizione_agevolata_eredi_decimal = 0,
                importo_definizione_agevolata_eredi_Euro = string.Empty,
                flag_spedizione_notifica = d.flag_spedizione_notifica,
                IsVisibleAtti = "false",
                IsVisibleBene = "false",
                IsVisibleAcqua = "false",
                nome = d.tab_contribuente.nome,
                cognome = d.tab_contribuente.cognome,
                codice_fiscale = d.tab_contribuente.cod_fiscale,
                p_iva = d.tab_contribuente.p_iva,
                ragione_sociale = d.tab_contribuente.rag_sociale
            }).AsQueryable();
        }

        public static IQueryable<tab_avv_pag_fatt_emissione_light> ToLightPerControlli(this IQueryable<tab_avv_pag_fatt_emissione> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_avv_pag_fatt_emissione_light
            {
                id_tab_avv_pag = d.id_tab_avv_pag,
                identificativo_avv_pag = d.identificativo_avv_pag,
                NumeroAvviso = d.NumeroAvviso,
                dt_emissione_String = d.dt_emissione_String,
                dt_emissione = d.dt_emissione,
                stato = d.stato,
                cod_stato = d.anagrafica_stato_avv_pag1.cod_stato_riferimento,
                Contribuente = d.tab_contribuente.contribuenteDisplay,
                nome = d.tab_contribuente.nome,
                cognome = d.tab_contribuente.cognome,
                codice_fiscale = d.tab_contribuente.cod_fiscale,
                p_iva = d.tab_contribuente.p_iva,
                ragione_sociale = d.tab_contribuente.rag_sociale,
                color = d.cod_stato.Equals(anagrafica_stato_avv_pag.VAL_CON) ? "green" : (d.cod_stato.Equals(anagrafica_stato_avv_pag.ANNULLATO_ANNULLATO) ? "red" : "")
            }).AsQueryable();
        }
    }
}

