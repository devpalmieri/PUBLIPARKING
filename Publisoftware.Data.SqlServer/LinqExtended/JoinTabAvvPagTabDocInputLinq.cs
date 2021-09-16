using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTabAvvPagTabDocInputLinq
    {
        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTabDocInput(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTabDocInput)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_idTabDocInput);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdAddettoLavorazione(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_addetto_lavorazione == p_idRisorsa);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdentificativoNotNull(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.Where(d => !string.IsNullOrEmpty(d.tab_doc_input.identificativo_doc_input));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdContribuente(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, Decimal p_idContribuente)
        {
            return p_query.Where(d => d.tab_doc_input.id_contribuente == p_idContribuente);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTerzo(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTerzo)
        {
            return p_query.Where(d => d.tab_doc_input.id_terzo == p_idTerzo);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdEntrata(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idEntratra)
        {
            return p_query.Where(d => d.tab_avv_pag.id_entrata == p_idEntratra);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTipoAvvPag(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.tab_avv_pag.id_tipo_avvpag == p_idTipoAvvPag);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdentificativo(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, string p_identificativo)
        {
            return p_query.Where(d => d.tab_doc_input.identificativo_doc_input.Trim().Equals(p_identificativo.Trim()));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByFlagIstanzaRicorsoIdTipoDoc(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_flagIstanzaRicorso, int p_idTipoDoc)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_stato.flag_istanza_ricorso == p_flagIstanzaRicorso
                                    && d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == p_idTipoDoc);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTipoDoc(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTipoDoc)
        {
            return p_query.Where(d => d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == p_idTipoDoc);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTipoDocList(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<int> p_idTipoDocList)
        {
            return p_query.Where(d => p_idTipoDocList.Contains(d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc));
        }
        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTabAvvPagList(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<int> p_idAvvPagList)
        {
            return p_query.Where(d => p_idAvvPagList.Contains(d.tab_avv_pag.id_tab_avv_pag));
        }
        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTipoDocEntrate(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.tab_doc_input.id_tipo_doc_entrate == p_idTipoDocEntrate);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByPropriONonAssegnati(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => (d.tab_doc_input.id_addetto_lavorazione == p_idRisorsa && d.tab_doc_input.id_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE_ID) ||
                                      (d.tab_doc_input.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoDocInput(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idStato)
        {
            return p_query.Where(d => d.tab_doc_input.id_stato == p_idStato);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoDocInputList(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<int> p_idStatoList)
        {
            return p_query.Where(d => p_idStatoList.Contains(d.tab_doc_input.id_stato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoDocInputNot(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idStato)
        {
            return p_query.Where(d => d.tab_doc_input.id_stato != p_idStato);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoDocInputListNot(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<int> p_idStatoList)
        {
            return p_query.Where(d => !p_idStatoList.Contains(d.tab_doc_input.id_stato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByTipoRateizzazione(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.tab_avv_pag_fatt_emissione.id_tipo_avvpag == p_idTipoAvvPag);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByTipoRateizzazioneList(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<int> p_idTipoAvvPagList)
        {
            return p_query.Where(d => p_idTipoAvvPagList.Contains(d.tab_avv_pag_fatt_emissione.id_tipo_avvpag));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByEnteStrutturaTipoAvvPag(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idEnteAppartenenza, int p_idEnte, int p_idStruttura, int p_idRisorsa)
        {
            if (p_idEnteAppartenenza != anagrafica_ente.ID_ENTE_PUBLISERVIZI)
            {
                return p_query.Where(x => x.tab_avv_pag1.id_ente == x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza &&
                                          x.tab_avv_pag1.tab_liste.id_struttura_approvazione == p_idStruttura);
            }
            else
            {
                return p_query.Where(x => x.tab_avv_pag1.id_ente != x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza);
            }
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIstanzeAcquisite(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idEnteAppartenenza, int p_idEnte, int p_idStruttura, int p_idRisorsa)
        {
            if (p_idEnteAppartenenza != anagrafica_ente.ID_ENTE_PUBLISERVIZI)
            {
                return p_query.Where(x => x.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID &&
                                          x.tab_avv_pag1.id_ente == x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza &&
                                          x.tab_avv_pag1.tab_liste.id_struttura_approvazione == p_idStruttura);
            }
            else
            {
                return p_query.Where(x => x.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID &&
                                          x.tab_avv_pag1.id_ente != x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza);
            }
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIstanzeLavorate(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idEnteAppartenenza, int p_idEnte, int p_idStruttura, int p_idRisorsa)
        {
            if (p_idEnteAppartenenza != anagrafica_ente.ID_ENTE_PUBLISERVIZI)
            {
                return p_query.Where(x => (x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO) || x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_VERIFICARE)) &&
                                           x.tab_avv_pag1.id_ente == x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza &&
                                           x.tab_avv_pag1.tab_liste.id_struttura_approvazione == p_idStruttura);
            }
            else
            {
                return p_query.Where(x => (x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO) || x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_VERIFICARE)) &&
                                           x.tab_avv_pag1.id_ente != x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza);
            }
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIstanzeAssegnate(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa, int p_idEnte, int p_idStruttura)
        {
            return p_query.Where(x => x.id_addetto_lavorazione == p_idRisorsa &&
                                      x.id_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE_ID);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIstanzeAcquisiteAssegnate(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idEnteAppartenenza, int p_idEnte, int p_idStruttura, int p_idRisorsa)
        {
            if (p_idEnteAppartenenza != anagrafica_ente.ID_ENTE_PUBLISERVIZI)
            {
                return p_query.Where(x => (x.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID &&
                                           x.tab_avv_pag1.id_ente == x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza &&
                                           x.tab_avv_pag1.tab_liste.id_struttura_approvazione == p_idStruttura) ||
                                          (x.id_addetto_lavorazione == p_idRisorsa && x.id_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE_ID));
            }
            else
            {
                return p_query.Where(x => (x.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID &&
                                           x.tab_avv_pag1.id_ente != x.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza) ||
                                          (x.id_addetto_lavorazione == p_idRisorsa && x.id_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE_ID));
            }
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByRicorsiAssegnati(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa, int p_idEnte)
        {
            if (p_idEnte == anagrafica_ente.ID_ENTE_COMUNE_DI_FIRENZE)
            {
                return p_query.Where(x => x.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID ||
                                         (x.id_addetto_lavorazione == p_idRisorsa && (x.id_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE_ID ||
                                                                                      x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO) ||
                                                                                      x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO))));
            }
            else
            {
                return p_query.Where(x =>
                                          //(x.tab_doc_input.id_addetto_acquisizione == p_idRisorsa &&
                                          // x.id_stato == anagrafica_stato_doc.STATO_ACQUISITO_ID) ||
                                          (x.id_addetto_lavorazione == p_idRisorsa && (x.id_stato == anagrafica_stato_doc.STATO_ASSEGNATA_LAVORAZIONE_ID ||
                                                                                       x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO) ||
                                                                                       x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_DEFINITIVO))));
            }
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIstanzeLavorateDaVerificare(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa, int p_idEnte, int p_idStruttura)
        {
            return p_query.Where(x => x.id_addetto_lavorazione == p_idRisorsa && x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_VERIFICARE));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIstanzeLavorateRettifica(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa, int p_idEnte, int p_idStruttura)
        {
            return p_query.Where(x => x.id_addetto_lavorazione == p_idRisorsa && x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_LAVORATO));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByStatoRicorso(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, string p_Stato)
        {
            return p_query.Where(x => x.tab_doc_input.tab_ricorsi.Count > 0 &&
                                      x.tab_doc_input.tab_ricorsi.FirstOrDefault().cod_stato.StartsWith(p_Stato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoAvvPag(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.StartsWith(p_codStato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdDocInput(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idDocInput)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_idDocInput);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdAvvPagNotNull(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.Where(d => d.id_avv_pag != null);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByImportoDaPagare(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, decimal p_importoMinimo)
        {
            return p_query.Where(d => d.tab_avv_pag.importo_tot_da_pagare.HasValue && d.tab_avv_pag.importo_tot_da_pagare.Value >= p_importoMinimo);
        }

        /// Filtro per gli avvisi spediti o notificati (vengono scartati quelli che non sono stati notificati ma dovevano esserlo)        
        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereSpedNot(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.Where(d => d.tab_avv_pag.flag_spedizione_notifica == "0"
                                    || d.tab_avv_pag.flag_spedizione_notifica == null
                                    || (d.tab_avv_pag.flag_spedizione_notifica == "1" && d.tab_avv_pag.flag_esito_sped_notifica == "1"));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereCausaleEnte(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, string p_Trattamento)
        {
            return p_query.Where(d => d.id_causale.HasValue && d.anagrafica_causale.flag_trattamento.Equals(p_Trattamento));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdStato(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<int> p_idStato)
        {
            return p_query.Where(d => p_idStato.Contains(d.id_stato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdStato(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idStato)
        {
            return p_query.Where(d => d.id_stato == p_idStato);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByAnno(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_anno)
        {
            return p_query.Where(d => d.tab_doc_input.anno >= p_anno);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdEntrateList(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, IList<int> p_idEntrateList)
        {
            return p_query.Where(d => p_idEntrateList.Contains(d.tab_avv_pag.id_entrata));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdTerzoDebitore(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, IList<int> p_idAvvisiDeiTerziList)
        {
            return p_query.Where(d => p_idAvvisiDeiTerziList.Contains(d.id_avv_pag.HasValue ? d.id_avv_pag.Value : 0));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdAvvPag(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag == p_idAvvPag);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdAvvPagCollegato(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_collegato == p_idAvvPag);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoListNot(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => !p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoList(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStatoNot(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByCodStato(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_codStato));
        }

        //Il dottore ha voluto che nel caso di acquisizione/esito sentenze ogni risorsa può vedere anche le motivazioni assegnate agli altri
        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByAddettoLavorazione(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa, bool p_isFromSentenze)
        {
            if (p_isFromSentenze)
            {
                return p_query;
            }
            else
            {
                return p_query.Where(d => d.id_addetto_lavorazione == p_idRisorsa);
            }
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByIdListaScarico(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idLista)
        {
            return p_query.Where(d => d.tab_avv_pag1.id_lista_scarico == p_idLista);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> GroupByIdAvvPag(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.GroupBy(p => new { p.id_avv_pag, p.id_avv_pag_collegato }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByAvvisiImportati(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.Where(d => (!string.IsNullOrEmpty(d.tab_avv_pag1.fonte_emissione) &&
                                       d.tab_avv_pag1.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA)) ||
                                       !string.IsNullOrEmpty(d.tab_avv_pag1.fonte_emissione) ||
                                      (d.tab_avv_pag1.tab_liste != null &&
                                       d.tab_avv_pag1.tab_liste.tab_tipo_lista.flag_tipo_lista != tab_tipo_lista.FLAG_TIPO_LISTA_C) ||
                                      (d.tab_avv_pag1.tab_liste != null &&
                                       d.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1 != null &&
                                       d.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente != null &&
                                       d.tab_avv_pag1.tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.id_ente != anagrafica_ente.ID_ENTE_PUBLISERVIZI) ||
                                      (d.tab_avv_pag1.tab_liste == null &&
                                       string.IsNullOrEmpty(d.tab_avv_pag1.barcode)));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByAvvisiEmessi(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.Where(d =>
                                     //(string.IsNullOrEmpty(d.tab_avv_pag1.fonte_emissione) ||
                                     //(!string.IsNullOrEmpty(d.tab_avv_pag1.fonte_emissione) &&
                                     // !d.tab_avv_pag1.fonte_emissione.Contains(tab_avv_pag.FONTE_IMPORTATA))) 
                                     // &&
                                     ((d.tab_avv_pag1.tab_liste != null &&
                                       d.tab_avv_pag1.tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C) ||
                                      (d.tab_avv_pag1.tab_liste == null &&
                                       !string.IsNullOrEmpty(d.tab_avv_pag1.barcode))));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByMotivazioniDaAltrePraticheCollegato(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTabDocInput, int p_idAvvPagCollegato)
        {
            return p_query.Where(d => (d.cod_stato.Equals(anagrafica_stato_doc.STATO_DEF_ACCOLTA) || (string.IsNullOrEmpty(d.flag_esito) && d.flag_esito.Equals("25"))) && //Mastrangelo ha detto che i 25 sono i vecchi
                                       d.id_tab_doc_input != p_idTabDocInput &&
                                      (d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_ANN_RET /*|| d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI*/) &&
                                       d.id_avv_pag_collegato == p_idAvvPagCollegato);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> WhereByMotivazioniDaAltrePraticheRiferimento(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idTabDocInput, int p_idAvvPag, int p_idAvvPagCollegato)
        {
            return p_query.Where(d => (d.cod_stato.Equals(anagrafica_stato_doc.STATO_DEF_ACCOLTA) || (string.IsNullOrEmpty(d.flag_esito) && d.flag_esito.Equals("25"))) && //Mastrangelo ha detto che i 25 sono i vecchi
                                       d.id_tab_doc_input != p_idTabDocInput &&
                                      (d.id_avv_pag_collegato == p_idAvvPag ||
                                       d.tab_avv_pag1.tab_unita_contribuzione.Any(x => x.id_avv_pag_collegato == p_idAvvPagCollegato)));
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByDataEmissioneRiferimento(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByDataEmissioneCollegato(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag1.dt_emissione);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByDataEsito(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.data_esito);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByDefault(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.id_join_avv_pag_doc_input);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByContribuenteAvvRifAvvColl(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.tab_contribuente.id_anag_contribuente)
                          .ThenBy(d => d.tab_avv_pag.identificativo_avv_pag)
                          .ThenBy(d => d.tab_avv_pag1.identificativo_avv_pag);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByAddettoLavorazioneDataPresentazione(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query, int p_idRisorsa)
        {
            return p_query.OrderByDescending(d => d.tab_doc_input.id_addetto_lavorazione == p_idRisorsa)
                          .ThenByDescending(d => d.tab_doc_input.data_presentazione);
        }

        public static IQueryable<join_tab_avv_pag_tab_doc_input> OrderByDataPresentazione(this IQueryable<join_tab_avv_pag_tab_doc_input> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_doc_input.data_presentazione);
        }
    }
}
