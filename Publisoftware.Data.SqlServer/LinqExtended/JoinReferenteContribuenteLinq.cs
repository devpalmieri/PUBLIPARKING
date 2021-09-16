using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinReferenteContribuenteLinq
    {
        public static IQueryable<join_referente_contribuente> WhereByIdJoinReferenteContribuenteList(this IQueryable<join_referente_contribuente> p_query, List<int> p_idJoinReferenteContribuenteList)
        {
            return p_query.Where(d => p_idJoinReferenteContribuenteList.Contains(d.id_join_referente_contribuente));
        }

        /// <summary>
        /// Controlla se c'è qualche join_referente_contribuente attivo
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static bool ExistAttivo(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.Any(d => d.data_fine_validita == null);
        }

        /// <summary>
        /// Controlla se c'è qualche join_referente_contribuente attivo
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static bool ExistAttivo(this IEnumerable<join_referente_contribuente> p_query)
        {
            return p_query.Any(d => d.data_fine_validita == null);
        }

        /// <summary>
        /// Restituisce il join_referente_contribuente attivo
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static join_referente_contribuente SingleOrDefaultAttivo(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.SingleOrDefault(s => s.data_fine_validita == null);
        }

        /// <summary>
        /// Restituisce il join_referente_contribuente attivo
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static join_referente_contribuente SingleOrDefaultAttivo(this IEnumerable<join_referente_contribuente> p_query)
        {
            return p_query.SingleOrDefault(s => s.data_fine_validita == null);
        }

        public static IQueryable<join_referente_contribuente> WhereByIdContribuente(this IQueryable<join_referente_contribuente> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_anag_contribuente == p_idContribuente);
        }

        public static IQueryable<join_referente_contribuente> WhereByIdReferente(this IQueryable<join_referente_contribuente> p_query, int p_idReferente)
        {
            return p_query.Where(d => d.id_tab_referente == p_idReferente);
        }

        public static IQueryable<join_referente_contribuente> WhereByIsCoobbligato(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.Where(d => d.flag_coobbligato == tab_referente.COOBBLIGATO ||
                                      d.flag_coobbligato == tab_referente.COOBBLIGATO_PARZIALE ||
                                      d.flag_coobbligato == tab_referente.GARANTE);
        }

        public static IQueryable<join_referente_contribuente> WhereByContainsStato(this IQueryable<join_referente_contribuente> p_query, string p_cod_stato)
        {
            return p_query.Where(c => c.cod_stato.Contains(p_cod_stato));
        }

        public static IQueryable<join_referente_contribuente> WhereByContainsStatoNot(this IQueryable<join_referente_contribuente> p_query, string p_cod_stato)
        {
            return p_query.Where(c => !c.cod_stato.Contains(p_cod_stato));
        }

        public static IQueryable<join_referente_contribuente> WhereByRangeValidita(this IQueryable<join_referente_contribuente> p_query, DateTime v_data)
        {
            return p_query.Where(d => d.data_inizio_validita <= v_data && (!d.data_fine_validita.HasValue || d.data_fine_validita >= v_data));
        }

        public static IQueryable<join_referente_contribuente> WhereByCoobbligatiDateValidita(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.Where(d => d.data_fine_validita == null || d.data_fine_validita != d.data_inizio_validita);
        }

        public static IQueryable<join_referente_contribuente> GroupByIdReferenteIdRelazioneFlagCoobbligato(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.GroupBy(p => new { p.id_tab_referente, p.id_tipo_relazione, p.flag_coobbligato }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<join_referente_contribuente> WhereByCFPIvaReferente(this IQueryable<join_referente_contribuente> p_query, string p_codFiscalePIva)
        {
            if (p_codFiscalePIva.Length == 16)
            {
                return p_query.Where(d => (d.tab_referente.tab_contribuente == null && d.tab_referente.cod_fiscale.Equals(p_codFiscalePIva)) ||
                                          (d.tab_referente.tab_contribuente != null && d.tab_referente.tab_contribuente.cod_fiscale.Equals(p_codFiscalePIva)));
            }
            else
            {
                return p_query.Where(d => (d.tab_referente.tab_contribuente == null && d.tab_referente.p_iva.Equals(p_codFiscalePIva)) ||
                                          (d.tab_referente.tab_contribuente != null && d.tab_referente.tab_contribuente.p_iva.Equals(p_codFiscalePIva)));
            }
        }

        public static IQueryable<join_referente_contribuente> OrderByDefault(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_tipo_relazione.cod_tipo_relazione).ThenBy(d => d.anagrafica_tipo_relazione.desc_tipo_relazione);
        }

        public static IQueryable<join_referente_contribuente> OrderByLastReferenza(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.OrderByDescending(d => d.data_fine_validita ?? DateTime.MaxValue);
        }

        public static IQueryable<join_referente_contribuente> OrderByInizioValidita(this IQueryable<join_referente_contribuente> p_query)
        {
            return p_query.OrderBy(d => d.data_inizio_validita);
        }

        public static IQueryable<join_referente_contribuente> Attivi(this IQueryable<join_referente_contribuente> p_query)
        {
            return WhereByContainsStatoNot(p_query, join_referente_contribuente.ANN);
        }

        public static IList<join_referente_contribuente_light> ToLight(this IQueryable<join_referente_contribuente> iniziale)
        {
            return iniziale.ToList().Select(d => new join_referente_contribuente_light
            {
                id_join_referente_contribuente = d.id_join_referente_contribuente,
                id_anag_contribuente = d.id_anag_contribuente,
                id_tab_referente = d.id_tab_referente,
                referenteDisplay = d.tab_referente.referenteDisplay,
                desc_tipo_relazione = d.anagrafica_tipo_relazione.desc_tipo_relazione,
                descrizione_parentela = d.parentela,
                coobbligato = d.coobbligato,
                coobbligazione_percentuale = d.coobbligazione_percentuale,
                importoMaxObbligazione = d.importo_max_obbligazione.ToString("C"),
                data_inizio_validita_String = d.data_inizio_validita_String,
                data_fine_validita_String = d.data_fine_validita_String
            }).ToList();
        }
    }
}
