using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabJoinAvvcoaIngfisV2Linq
    {
        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByIdIngiunzione(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, int p_idIngiunzione)
        {
            return p_query.Where(d => d.ID_INGIUNZIONE == p_idIngiunzione);
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereBySollecitiIntimazioni(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM || 
                                      d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA);
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByCautelariPignoramenti(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query)
        {
            return p_query.Where(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI 
                                    || d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByCoattivoCodStato(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, List<string> p_statoList)
        {
            return p_query.Where(d => p_statoList.Any(x => d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(x)));
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByIngiunzioneCodStato(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_avv_pag1.anagrafica_stato.cod_stato_riferimento.Contains(p_codStato));
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByCoattivoNotCodStato(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(p_codStato));
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByIngiunzioneNotCodStato(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_avv_pag1.anagrafica_stato.cod_stato_riferimento.Contains(p_codStato));
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereBySpedNot(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query)
        {
            return p_query.Where(d => d.tab_avv_pag.flag_esito_sped_notifica == "1");
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByCodStato(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> WhereByCodStatoNot(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> OrderByDataEmissione(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> OrderByDefault(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query)
        {
            return p_query.OrderBy(o => o.ID_JOIN_AVVCOA_INGFIS);
        }

        public static IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> OrderByDefaultDesc(this IQueryable<TAB_JOIN_AVVCOA_INGFIS_V2> p_query)
        {
            return p_query.OrderByDescending(o => o.ID_JOIN_AVVCOA_INGFIS);
        }
    }
}
