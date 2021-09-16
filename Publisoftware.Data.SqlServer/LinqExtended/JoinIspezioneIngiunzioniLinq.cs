using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinIspezioneIngiunzioniLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_ispezioni_ingiunzioni> OrderByDefault(this IQueryable<join_ispezioni_ingiunzioni> p_query)
        {
            return p_query.OrderBy(o => o.id_join_ispezioni_ingiunzioni);
        }

        public static IQueryable<join_ispezioni_ingiunzioni> OrderByDataRilevazioneMorosita(this IQueryable<join_ispezioni_ingiunzioni> p_query)
        {
            return p_query.OrderBy(o => o.tab_ispezioni_coattivo_new.data_rilevazione_morosita);
        }

        /// <summary>
        /// Filtro per codice stato
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="stato"></param>
        /// <returns></returns>
        public static IQueryable<join_ispezioni_ingiunzioni> WhereCodStato(this IQueryable<join_ispezioni_ingiunzioni> p_query, string stato)
        {
            return p_query.Where(ii => ii.cod_stato.CompareTo(stato) == 0);
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByCodStato(this IQueryable<join_ispezioni_ingiunzioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByCodStatoIngiunzione(this IQueryable<join_ispezioni_ingiunzioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_ingiunzioni_ispezione.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByCodStatoIspezione(this IQueryable<join_ispezioni_ingiunzioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_ispezioni_coattivo_new.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByCodStatoNot(this IQueryable<join_ispezioni_ingiunzioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByCodStatoIngiunzioneNot(this IQueryable<join_ispezioni_ingiunzioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_ingiunzioni_ispezione.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByCodStatoIspezioneNot(this IQueryable<join_ispezioni_ingiunzioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_ispezioni_coattivo_new.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByIdTabIspezioneCoattivo(this IQueryable<join_ispezioni_ingiunzioni> p_query, int p_id_tab_ispezione_coattivo)
        {
            return p_query.Where(j => j.id_tab_ispezione_coattivo == p_id_tab_ispezione_coattivo);
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByIdAvviso(this IQueryable<join_ispezioni_ingiunzioni> p_query, int p_idAvviso)
        {
            return p_query.Where(j => j.tab_ingiunzioni_ispezione.id_tab_avv_pag == p_idAvviso);
        }

        public static IQueryable<join_ispezioni_ingiunzioni> WhereByIdContribuente(this IQueryable<join_ispezioni_ingiunzioni> p_query, decimal p_idContribuente)
        {
            return p_query.Where(j => j.tab_ingiunzioni_ispezione.id_anag_contribuente == p_idContribuente);
        }

        public static IList<join_ispezioni_ingiunzioni_light> ToLight(this IQueryable<join_ispezioni_ingiunzioni> iniziale)
        {
            return iniziale.ToList().Select(d => new join_ispezioni_ingiunzioni_light
            {
                id_join_ispezioni_ingiunzioni = d.id_join_ispezioni_ingiunzioni,
                id_tab_ingiunzioni_ispezione = d.tab_ingiunzioni_ispezione.id_tab_ingiunzioni_ispezione,
                id_tab_ispezione_coattivo = d.tab_ispezioni_coattivo_new.id_tab_ispezione_coattivo,
                stato = d.tab_ispezioni_coattivo_new.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL ? "In corso" : (d.tab_ispezioni_coattivo_new.cod_stato == tab_ispezioni_coattivo_new.VAL_OLD ? "Chiusa" : string.Empty),
                nominativo = d.tab_ispezioni_coattivo_new.nominativoRagSoc,
                cfiscale_piva_soggetto_ispezione = d.tab_ispezioni_coattivo_new.cfiscale_piva_soggetto_ispezione,
                impMorosita = d.tab_ispezioni_coattivo_new.impMorosita,
                impMorositaFermo = d.tab_ispezioni_coattivo_new.impMorositaFermo,
                impMorositaIpoteca = d.tab_ispezioni_coattivo_new.impMorositaIpoteca,
                impMorositaAssoggettabileAttiEsecutivi = d.tab_ispezioni_coattivo_new.impMorositaAssoggettabileAttiEsecutivi,
                data_rilevazione_morosita_String = d.tab_ispezioni_coattivo_new.data_rilevazione_morosita_String,
                oggettoGroupBy = d.tab_ispezioni_coattivo_new.data_rilevazione_morosita_String,
                esitoIsp = d.tab_ispezioni_coattivo_new.esitoIsp,
                fineIsp = d.tab_ispezioni_coattivo_new.fineIsp,
                supervisione = d.tab_ispezioni_coattivo_new.supervisione,
                attoEmesso = d.tab_ispezioni_coattivo_new.attoEmesso,
                tipoRelazione = d.tab_ispezioni_coattivo_new.tipoRelazione,

                descrizioneIngiunzione = d.tab_ingiunzioni_ispezione.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " " + d.tab_ingiunzioni_ispezione.tab_avv_pag.identificativo_avv_pag,
                dataNotifica = d.tab_ingiunzioni_ispezione.tab_avv_pag.data_ricezione_String,
                ggSospensione = d.tab_ingiunzioni_ispezione.tab_avv_pag.gg_sospensione_trasmessi > 0 ? d.tab_ingiunzioni_ispezione.tab_avv_pag.gg_sospensione_trasmessi : d.tab_ingiunzioni_ispezione.tab_avv_pag.gg_sospensione_generati,
                impDaPagare = d.tab_ingiunzioni_ispezione.impDaPagare,
                dataNotificaIntimazione = d.tab_ingiunzioni_ispezione.tab_avv_pag.data_intimazione,
                tipoAtti = d.tab_ingiunzioni_ispezione.tipoAtti
            }).ToList();
        }
    }
}
