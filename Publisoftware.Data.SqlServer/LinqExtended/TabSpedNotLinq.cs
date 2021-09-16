using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabSpedNotLinq
    {
        public static IQueryable<tab_sped_not> WhereByCodStato(this IQueryable<tab_sped_not> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_sped_not> WhereByCodStatoNot(this IQueryable<tab_sped_not> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_sped_not> WhereByIdSpedNot(this IQueryable<tab_sped_not> p_query, int p_idSpedNot)
        {
            return p_query.Where(d => d.id_sped_not == p_idSpedNot);
        }

        public static IQueryable<tab_sped_not> WhereByNotIdSpedNot(this IQueryable<tab_sped_not> p_query, int p_idSpedNot)
        {
            return p_query.Where(d => d.id_sped_not != p_idSpedNot);
        }

        public static IQueryable<tab_sped_not> WhereIdEnteOrGeneric(this IQueryable<tab_sped_not> p_query, int p_idEnte)
        {
            return p_query.Where(c => c.tab_avv_pag.id_ente == p_idEnte || c.tab_avv_pag.id_ente == anagrafica_ente.ID_ENTE_GENERICO);
        }

        public static IQueryable<tab_sped_not> WhereByIdDocOutput(this IQueryable<tab_sped_not> p_query, int p_idDoc)
        {
            return p_query.Where(d => d.id_doc_output == p_idDoc);
        }

        public static IQueryable<tab_sped_not> WhereByTipoLista(this IQueryable<tab_sped_not> p_query, string p_codLista)
        {
            return p_query.Where(ac => ac.tab_lista_sped_notifiche.tab_tipo_lista.cod_lista.Equals(p_codLista));
        }

        public static IQueryable<tab_sped_not> WhereByNumeroRaccomandata(this IQueryable<tab_sped_not> p_query, string p_numeroRaccomandata)
        {
            return p_query.Where(d => d.numero_raccomandata.Trim().Equals(p_numeroRaccomandata.Trim()));
        }

        public static IQueryable<tab_sped_not> WhereByNumeroAttoGiudiziario(this IQueryable<tab_sped_not> p_query, string p_numeroGiudiziario)
        {
            return p_query.Where(d => d.numero_atto_giudiziario.Trim().Equals(p_numeroGiudiziario.Trim()));
        }

        public static IQueryable<tab_sped_not> WhereByNomeLotto(this IQueryable<tab_sped_not> p_query, string p_nomeLotto)
        {
            return p_query.Where(ac => ac.nome_lotto_spedizione != null && ac.nome_lotto_spedizione.Equals(p_nomeLotto));
        }

        public static IQueryable<tab_sped_not> WhereByIdStato(this IQueryable<tab_sped_not> p_query, int p_idStato)
        {
            return p_query.Where(d => d.id_stato_sped_not == p_idStato);
        }

        public static IQueryable<tab_sped_not> WhereByNotificati(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.id_stato_sped_not == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                    ((d.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.RIL_140_ID) && d.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                    ((d.id_stato_sped_not == anagrafica_stato_sped_not.RIL_139_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.RIL_139_ID) && d.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                      d.id_stato_sped_not == anagrafica_stato_sped_not.NOT_143_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.NOT_143_ID ||
                                      d.id_stato_sped_not == anagrafica_stato_sped_not.NOT_SPO_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.NOT_SPO_ID);
        }

        public static IQueryable<tab_sped_not> WhereByNotificatiENonOggettoDiNotifica(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.tab_avv_pag.flag_spedizione_notifica) ||
                                      d.tab_avv_pag.flag_spedizione_notifica == "0" ||
                                      d.id_stato_sped_not == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                    ((d.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.RIL_140_ID) && d.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                    ((d.id_stato_sped_not == anagrafica_stato_sped_not.RIL_139_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.RIL_139_ID) && d.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                      d.id_stato_sped_not == anagrafica_stato_sped_not.NOT_143_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.NOT_143_ID ||
                                      d.id_stato_sped_not == anagrafica_stato_sped_not.NOT_SPO_ID ||
                                      d.id_stato == anagrafica_stato_sped_not.NOT_SPO_ID);
        }

        public static IQueryable<tab_sped_not> WhereByIdAvviso(this IQueryable<tab_sped_not> p_query, int p_idTabAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag == p_idTabAvvPag);
        }

        public static IQueryable<tab_sped_not> WhereByIdAvvisoNotNull(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.id_avv_pag.HasValue);
        }

        public static IQueryable<tab_sped_not> WhereBySenzaDocOutput(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.tab_doc_output == null);
        }

        public static IQueryable<tab_sped_not> WhereByIdTipoListaNot(this IQueryable<tab_sped_not> p_query, int p_idTipoLista)
        {
            return p_query.Where(d => d.id_tipo_lista != p_idTipoLista);
        }

        public static IQueryable<tab_sped_not> WhereByIdTipoLista(this IQueryable<tab_sped_not> p_query, int p_idTipoLista)
        {
            return p_query.Where(d => d.id_tipo_lista == p_idTipoLista);
        }

        public static IQueryable<tab_sped_not> WhereBySpediteAlContribuente(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.id_terzo_debitore == null && d.id_referente == null);
        }

        public static IQueryable<tab_sped_not> WhereByIdTipoSpedizione(this IQueryable<tab_sped_not> p_query, int p_idTipoSpedizione)
        {
            return p_query.Where(d => d.id_tipo_spedizione_notifica == p_idTipoSpedizione);
        }

        public static IQueryable<tab_sped_not> WhereByNotIdTipoSpedizione(this IQueryable<tab_sped_not> p_query, int p_idTipoSpedizione)
        {
            return p_query.Where(d => d.id_tipo_spedizione_notifica != p_idTipoSpedizione);
        }

        public static IQueryable<tab_sped_not> WhereByNotSigleTipoSpedizione(this IQueryable<tab_sped_not> p_query, List<string> p_sigleTipoSpedizioneList)
        {
            return p_query.Where(d => !p_sigleTipoSpedizioneList.Contains(d.anagrafica_tipo_spedizione_notifica.sigla_tipo_spedizione_notifica));
        }

        public static IQueryable<tab_sped_not> WhereByNotIdDocEntrataList(this IQueryable<tab_sped_not> p_query, IList<int> p_idTipoDocEntrataList)
        {
            return p_query.Where(d => !d.id_doc_output.HasValue || !p_idTipoDocEntrataList.Contains(d.tab_doc_output.id_tipo_doc_entrate));
        }

        public static IQueryable<tab_sped_not> WhereBySpediteAiTerziAiReferenti(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.id_terzo_debitore != null ||
                                      d.id_referente != null);
        }

        public static IQueryable<tab_sped_not> WhereByTerzoDebitore(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.flag_soggetto_debitore == tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB);
        }

        public static IQueryable<tab_sped_not> WhereByNotTerzoDebitore(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.flag_soggetto_debitore != tab_sped_not.FLAG_DESTINATARIO_TRZ_DEB);
        }

        public static IQueryable<tab_sped_not> WhereByNotUfficialeRiscossione(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.flag_soggetto_debitore != tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE);
        }

        public static IQueryable<tab_sped_not> OrderByDefault(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.OrderBy(d => d.id_lista_sped_not).ThenBy(d => d.id_contribuente).ThenBy(d => d.id_destinatario);
        }

        public static IQueryable<tab_sped_not> WhereByIdListaEmissione(this IQueryable<tab_sped_not> p_query, int p_idListaEmissione)
        {
            return p_query.Where(d => d.tab_avv_pag.id_lista_emissione == p_idListaEmissione);
        }
        public static IQueryable<tab_sped_not> WhereByListaEmissioneStampaApprovata(this IQueryable<tab_sped_not> p_query, string p_CodStatoLista)
        {
            return p_query.Where(d => d.tab_avv_pag.tab_liste.cod_stato == p_CodStatoLista);
        }

        public static IQueryable<tab_sped_not> WhereByIdListaSpedizione(this IQueryable<tab_sped_not> p_query, int p_idListaSpedizione)
        {
            return p_query.Where(d => d.id_lista_sped_not == p_idListaSpedizione);
        }

        public static IQueryable<tab_sped_not> WhereByNonSpedita(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.Where(d => d.cod_stato == anagrafica_stato_sped_not.SPE_PRE || d.cod_stato == anagrafica_stato_sped_not.ASS_ASS);
        }

        public static IQueryable<tab_sped_not> WhereByStampatoreTipoSpedizione(this IQueryable<tab_sped_not> p_query, Int32 p_idStampatore, string p_codiceTipoSpedizione)
        {
            return p_query.Where(d => d.anagrafica_tipo_spedizione_notifica.id_stampatore == p_idStampatore &&
                                      d.anagrafica_tipo_spedizione_notifica.flag_sped_not == p_codiceTipoSpedizione);
        }

        public static IQueryable<tab_sped_not> WhereByBarcode(this IQueryable<tab_sped_not> p_query, string p_barcode)
        {
            return p_query.Where(d => d.barcode == p_barcode);
        }

        public static IQueryable<tab_sped_not> OrderByDataEsitoNotifica(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.OrderByDescending(d => d.data_esito_notifica);
        }

        //ordina per id: area, zona, toponimo, notificatore
        public static IQueryable<tab_sped_not> OrderByNotificatore(this IQueryable<tab_sped_not> p_query)
        {
            return p_query.OrderBy(d => d.id_notificatore)
                          .ThenBy(z => z.cod_zona)
                          .ThenBy(z => z.cod_area)
                          .ThenBy(t => t.id_toponimo)
                          .ThenBy(n => n.num_civico)
                          .ThenBy(n => n.sigla);
        }

        public static IQueryable<tab_sped_not_light> ToLight(this IQueryable<tab_sped_not> iniziale)
        {
            List<tab_sped_not> lst = iniziale.ToList();

            return iniziale.ToList().Select(d => new tab_sped_not_light
            {
                id_sped_not = d.id_sped_not,
                barcode = d.barcode,
                TipoLista = d.tab_lista_sped_notifiche == null ? string.Empty : d.tab_lista_sped_notifiche.tab_tipo_lista == null ? string.Empty : d.tab_lista_sped_notifiche.tab_tipo_lista.desc_lista,
                EsitoNotifica = d.EsitoNotifica,
                Stato = d.Stato,
                data_esito_notifica_String = d.data_esito_notifica_String,
                Indirizzo = d.Indirizzo,
                Destinatario = d.Destinatario,
                flag_spedizione_notifica = d.tab_avv_pag.flag_spedizione_notifica,
                id_stato_sped_not = d.id_stato_sped_not.HasValue ? d.id_stato_sped_not.Value : -1,
                isAvvisoImmaginePresente = false, //Il campo viene valorizzato a valle del GridUpdateNotifiche
                isAvvisoImmagineVisibile = d.isAvvisoImmagineVisibile,
                TipoSpedizione = d.anagrafica_tipo_spedizione_notifica != null ? d.anagrafica_tipo_spedizione_notifica.descr_tipo_spedizione_notifica : string.Empty
            }).AsQueryable();
        }
    }
}
