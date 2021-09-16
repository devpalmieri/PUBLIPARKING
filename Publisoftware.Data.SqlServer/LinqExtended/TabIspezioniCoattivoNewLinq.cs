using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabIspezioniCoattivoNewLinq
    {
        public static IQueryable<tab_ispezioni_coattivo_new> OrderByDefault(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_ispezione_coattivo);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> OrderByMorosita(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.OrderBy(o => o.totale_morosita);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> OrderByMorositaSoggettoIspezione(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.OrderByDescending(o => o.totale_morosita_soggetto_ispezione.HasValue ? o.totale_morosita_soggetto_ispezione.Value : 0);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> OrderByMorositaEsecutiva(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.OrderBy(o => o.totale_morosita_assoggettabile_atti_esecutivi);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> OrderByDataRilevazioneMorosita(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.OrderBy(o => o.data_rilevazione_morosita);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> OrderByContribuente(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.OrderBy(o => o.id_contribuente);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> OrderByLiberaOAssegnateASe(this IQueryable<tab_ispezioni_coattivo_new> p_query, int p_idRisorsa)
        {
            return p_query.OrderBy(d => d.id_risorsa_supervisione.HasValue && d.id_risorsa_supervisione.Value == p_idRisorsa).ThenBy(d => !d.id_risorsa_supervisione.HasValue);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByCodStato(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByCodStatoNot(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByIdContribuente(this IQueryable<tab_ispezioni_coattivo_new> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByRangeImporti(this IQueryable<tab_ispezioni_coattivo_new> p_query, decimal? p_da, decimal? p_a)
        {
            return p_query.Where(d => (d.totale_morosita.HasValue ? d.totale_morosita.Value : 0) > (p_da.HasValue ? p_da.Value : decimal.MinValue)
                                   && (d.totale_morosita.HasValue ? d.totale_morosita.Value : 0) <= (p_a.HasValue ? p_a.Value : decimal.MaxValue));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByValidaNonSupervisionata(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.Where(d => d.flag_supervisione_finale == "0" && d.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByCodFiscaleContribuente(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_cod_fiscale)
        {
            return p_query.Where(d => d.tab_contribuente.cod_fiscale.Equals(p_cod_fiscale));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByIdEnte(this IQueryable<tab_ispezioni_coattivo_new> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByIdRisorsa(this IQueryable<tab_ispezioni_coattivo_new> p_query, int p_idRisorsa)
        {
            return p_query.Where(ac => ac.id_risorsa_supervisione == p_idRisorsa);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereBySenzaIdRisorsa(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.Where(ac => !ac.id_risorsa_supervisione.HasValue);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByLiberaOAssegnateASe(this IQueryable<tab_ispezioni_coattivo_new> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => !d.id_risorsa_supervisione.HasValue || d.id_risorsa_supervisione == p_idRisorsa);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereAssegnabile(this IQueryable<tab_ispezioni_coattivo_new> p_query)
        {
            return p_query.Where(w => !w.join_tab_ispezioni_coattivo_tipo_ispezione.Any(j => j.flag_fine_ispezione == "0" && j.id_risorsa_ispezione.HasValue) &&
                                       w.cod_stato == CodStato.VAL_VAL &&
                                       w.flag_fine_ispezione_totale == "0");
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagFineIspezioneTotaleNullOr(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => (String.IsNullOrEmpty(d.flag_fine_ispezione_totale) || d.flag_fine_ispezione_totale.Equals(p_flag)));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagEsitoIspezioneTotaleNullOr(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => (String.IsNullOrEmpty(d.flag_esito_ispezione_totale) || d.flag_esito_ispezione_totale.Equals(p_flag)));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagSupervisioneFinaleNullOr(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => (String.IsNullOrEmpty(d.flag_supervisione_finale) || d.flag_supervisione_finale.Equals(p_flag)));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagEmissioneCoattivoNullOr(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => (String.IsNullOrEmpty(d.flag_emissione_avviso_coattivo) || d.flag_emissione_avviso_coattivo.Equals(p_flag)));
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagFineIspezioneTotale(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_fine_ispezione_totale == p_flag);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagEsitoIspezioneTotale(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_esito_ispezione_totale == p_flag);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagSupervisioneFinale(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_supervisione_finale == p_flag);
        }

        public static IQueryable<tab_ispezioni_coattivo_new> WhereByFlagEmissioneCoattivo(this IQueryable<tab_ispezioni_coattivo_new> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_emissione_avviso_coattivo == p_flag);
        }

        public static IList<tab_ispezioni_coattivo_new_light> ToLight(this IQueryable<tab_ispezioni_coattivo_new> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_ispezioni_coattivo_new_light
            {
                id_tab_ispezione_coattivo = d.id_tab_ispezione_coattivo,
                stato = d.cod_stato == tab_ispezioni_coattivo_new.VAL_VAL ? "In corso" : (d.cod_stato == tab_ispezioni_coattivo_new.VAL_OLD ? "Chiusa" : string.Empty),
                nominativo = d.nominativoRagSoc,
                cfiscale_piva_soggetto_ispezione = d.cfiscale_piva_soggetto_ispezione,
                impMorosita = d.impMorosita,
                impMorositaFermo = d.impMorositaFermo,
                impMorositaIpoteca = d.impMorositaIpoteca,
                impMorositaAssoggettabileAttiEsecutivi = d.impMorositaAssoggettabileAttiEsecutivi,
                data_rilevazione_morosita_String = d.data_rilevazione_morosita_String,
                oggettoGroupBy = d.data_rilevazione_morosita_String,
                esitoIsp = d.esitoIsp,
                fineIsp = d.fineIsp,
                supervisione = d.supervisione,
                attoEmesso = d.attoEmesso,
                tipoRelazione = d.tipoRelazione,
                risorsaSupervisione = d.risorsaSupervisione,
                nominativoDiplayContribuente = d.tab_contribuente.nominativoDisplay,
                codFiscalePivaDisplayContribuente = d.tab_contribuente.codFiscalePivaDisplay,
                statoContribuenteTotaleContribuente = d.tab_contribuente.anagrafica_stato_contribuente.statoContribuenteTotale
            }).ToList();
        }
    }
}
