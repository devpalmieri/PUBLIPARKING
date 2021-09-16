using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabUnitaContribuzioneLinq
    {
        public static IQueryable<tab_unita_contribuzione> WhereByIdOggettoContribuzione(this IQueryable<tab_unita_contribuzione> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.id_oggetto_contribuzione == p_idOggettoContribuzione);
        }

        public static IQueryable<tab_unita_contribuzione> WhereByIdTabUnitaContribuzione(this IQueryable<tab_unita_contribuzione> p_query, int p_idTabUnitaContribuzione)
        {
            return p_query.Where(d => d.id_unita_contribuzione == p_idTabUnitaContribuzione);
        }

        public static IQueryable<tab_unita_contribuzione> WhereByIdAvvPagGenerato(this IQueryable<tab_unita_contribuzione> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_generato == p_idAvvPag);
        }

        public static IQueryable<tab_unita_contribuzione> WhereByIdAvvPagCollegato(this IQueryable<tab_unita_contribuzione> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_collegato == p_idAvvPag);
        }

        public static IQueryable<tab_unita_contribuzione> WhereIdAvvPagCollegatoNotNull(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.Where(d => d.id_avv_pag_collegato.HasValue);
        }

        public static IQueryable<tab_unita_contribuzione> WhereIdAvvPagCollegatoNull(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.Where(d => !d.id_avv_pag_collegato.HasValue);
        }

        public static IQueryable<tab_unita_contribuzione> WhereByCodStato(this IQueryable<tab_unita_contribuzione> p_query, string p_stato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_stato));
        }

        public static IQueryable<tab_unita_contribuzione> WhereByCodStatoNot(this IQueryable<tab_unita_contribuzione> p_query, string p_stato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_stato));
        }

        public static IQueryable<tab_unita_contribuzione> WhereByCodStatoAvvisoGenerato(this IQueryable<tab_unita_contribuzione> p_query, string p_stato)
        {
            return p_query.Where(d => d.tab_avv_pag.cod_stato.StartsWith(p_stato));
        }

        public static IQueryable<tab_unita_contribuzione> WhereByCodStatoNotAvvisoGenerato(this IQueryable<tab_unita_contribuzione> p_query, string p_stato)
        {
            return p_query.Where(d => !d.tab_avv_pag.cod_stato.StartsWith(p_stato));
        }

        public static IQueryable<tab_unita_contribuzione> WhereByStatoAvvisiSuccessivi(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                      !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO));
        }

        public static IQueryable<tab_unita_contribuzione> WhereByImportoPositivo(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.Where(d => d.importo_unita_contribuzione > 0 ||
                                      d.imp_maggiorazione_onere_riscossione_61_90 > 0 ||
                                      d.imp_maggiorazione_onere_riscossione_121 > 0);
        }

        public static IQueryable<tab_unita_contribuzione> WhereByIdContribuente(this IQueryable<tab_unita_contribuzione> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_unita_contribuzione> WhereByCodiceTributoMinisteriale(this IQueryable<tab_unita_contribuzione> p_query, string p_codice)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione != null &&
                                      d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == p_codice);
        }

        public static IQueryable<tab_unita_contribuzione> OrderByPeriodoOggetto(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.OrderBy(o => o.periodo_contribuzione_da).ThenByDescending(o => o.id_oggetto);
        }

        public static IQueryable<tab_unita_contribuzione> OrderByNumRiga(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.OrderBy(o => o.num_riga_avv_pag_generato);
        }

        public static IQueryable<tab_unita_contribuzione> OrderByIdEntrata(this IQueryable<tab_unita_contribuzione> p_query)
        {
            return p_query.OrderBy(o => o.id_entrata);
        }

        public static IList<tab_unita_contribuzione_light> ToLight(this IQueryable<tab_unita_contribuzione> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_unita_contribuzione_light
            {
                id_unita_contribuzione = d.id_unita_contribuzione,
                anno_rif = d.anno_rif,
                //periodo_contribuzione_da_String = d.periodo_contribuzione_da_String,
                //periodo_contribuzione_a_String = d.periodo_contribuzione_a_String,
                periodo_contribuzione_String = d.periodo_contribuzione_String,
                //periodo_rif_da_String = d.periodo_rif_da_String,
                //periodo_rif_a_String = d.periodo_rif_a_String,
                //periodo_rif_String = d.periodo_rif_String,
                um_unita = d.um_unita,
                QuantitaUnitaContribuzione = d.QuantitaUnitaContribuzione,
                quantita_unita_contribuzione = d.quantita_unita_contribuzione,
                importo_unitario_contribuzione_Euro = d.importo_unitario_contribuzione_Euro,
                importo_unita_contribuzione = d.importo_unita_contribuzione.HasValue ? d.importo_unita_contribuzione.Value : 0,
                importo_unita_contribuzione_Euro = d.importo_unita_contribuzione_Euro,
                SommatoriaAgevolazioni_Euro = d.SommatoriaAgevolazioni_Euro,
                SommatoriaAgevolazioni = d.SommatoriaAgevolazioni,
                Ubicazione = d.tab_oggetti == null ? string.Empty : d.tab_oggetti.Ubicazione,
                DescrizioneVoceContribuzione = d.DescrizioneVoceContribuzione,
                descrizioneCategoria = d.tab_oggetti_contribuzione == null ? string.Empty : (d.tab_oggetti_contribuzione.anagrafica_categoria == null ? string.Empty : /*d.tab_oggetti_contribuzione.anagrafica_categoria.sigla_cat_contr + "/" +*/ d.tab_oggetti_contribuzione.anagrafica_categoria.des_cat_contr),
                codiceCategoria = d.tab_oggetti_contribuzione == null ? string.Empty : (d.tab_oggetti_contribuzione.anagrafica_categoria == null ? string.Empty : d.tab_oggetti_contribuzione.anagrafica_categoria.sigla_cat_contr),
                PercentualePossesso = d.tab_oggetti_contribuzione == null ? string.Empty : d.tab_oggetti_contribuzione.PercentualePossesso,
                numTotOccupanti = d.tab_oggetti_contribuzione == null ? string.Empty : d.tab_oggetti_contribuzione.NumTotOccupanti,
                descrizioneUtilizzo = d.tab_oggetti_contribuzione == null ? string.Empty : (d.tab_oggetti_contribuzione.anagrafica_utilizzo == null ? string.Empty : d.tab_oggetti_contribuzione.anagrafica_utilizzo.des_utilizzo),
                descrizioneDiritto = d.tab_oggetti_contribuzione == null ? string.Empty : (d.tab_oggetti_contribuzione.anagrafica_tipo_diritto == null ? string.Empty : d.tab_oggetti_contribuzione.anagrafica_tipo_diritto.descrizione),
                Rendita = d.tab_oggetti_contribuzione == null ? string.Empty : d.tab_oggetti_contribuzione.Rendita,
                OggettoDes = d.tab_oggetti != null ? d.tab_oggetti.OggettoDes : string.Empty,
                OggettoUbicazione = d.tab_oggetti != null && d.tab_oggetti.tab_tipo_oggetto != null && d.tab_oggetti.tab_tipo_oggetto.isUbicazione ? true : false,
                id_avv_pag_collegato = d.id_avv_pag_collegato,
                id_avv_pag_generato = d.id_avv_pag_generato,
                id_unita_contribuzione_collegato = d.id_unita_contribuzione_collegato,
                isUnitaSemplice = !d.id_avv_pag_collegato.HasValue && d.tab_unita_contribuzione1.Count == 0 ? true : false,
                num_riga_avv_pag_generato = d.num_riga_avv_pag_generato,
                periodo_contribuzione_da = d.periodo_contribuzione_da,
                id_oggetto_contribuzione = d.id_oggetto_contribuzione,
                id_oggetto = d.id_oggetto,
                id_entrata = d.id_entrata,
                id_anagrafica_voce_contribuzione = d.id_anagrafica_voce_contribuzione,
                codice_tributo_ministeriale = (d.tab_tipo_voce_contribuzione != null && d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale != null) ? d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() : string.Empty,
                //id_tab_macroentrate = d.id_tab_macroentrate,
                flag_segno = d.flag_segno,
                flag_tipo_composto = d.tab_avv_pag.anagrafica_tipo_avv_pag.flag_tipo_composto,
                color = "",
                oggettoGroupBy = (d.tab_oggetti == null || (d.tab_oggetti != null && (!d.tab_oggetti.tab_tipo_oggetto.isUbicazione))) ? string.Empty : d.tab_oggetti.Ubicazione + ";" + d.id_oggetto.Value,
                importo_sgravio = d.importo_sgravio.HasValue ? d.importo_sgravio.Value : 0,
                importo_sgravio_Euro = d.importo_sgravio_Euro,
                importo_sgravato = d.importo_sgravato,
                importo_sgravato_Euro = d.importo_sgravato_Euro,
                IsAvvisoStatoAnnRetDanDar = d.IsAvvisoStatoAnnRetDanDar,
                IsAvvisoStatoAnnDan = d.IsAvvisoStatoAnnDan,
                IsAttoSuccessivo = d.IsAttoSuccessivo,
                AttoSuccessivo = d.AttoSuccessivo,
                statoAvvisoCollegato = d.statoAvvisoCollegato,
                flag_natura_avv_collegati = d.tab_avv_pag1 != null ? d.tab_avv_pag1.anagrafica_tipo_avv_pag.flag_natura_avv_collegati : string.Empty,
                cod_stato = d.cod_stato,
                AvvisoCollegato = d.tab_avv_pag1 != null ? d.tab_avv_pag1.NumeroAvviso : string.Empty,
                AvvisoCollegatoStato = d.tab_avv_pag1 != null ? d.tab_avv_pag1.stato : string.Empty,
                importo_collegato = d.tab_avv_pag1 != null ? d.tab_avv_pag1.imp_tot_avvpag_rid : 0,
                importo_collegato_Euro = d.tab_avv_pag1 != null ? (d.tab_avv_pag1.imp_tot_avvpag_rid.HasValue ? d.tab_avv_pag1.imp_tot_avvpag_rid.Value.ToString("C4") : 0.ToString("C4")) : string.Empty,
                IsAttoComposto = d.tab_avv_pag1 != null && d.tab_avv_pag1.tab_unita_contribuzione.Any(x => !x.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) && x.id_avv_pag_collegato.HasValue),
                imponibile_unita_contribuzione_Euro = d.imponibile_unita_contribuzione_Euro,
                iva = d.iva,
                IsBottoneAvvisoCollegatoVisibile = d.id_avv_pag_collegato.HasValue && d.tab_avv_pag1.dt_emissione.HasValue && d.tab_avv_pag1.dt_emissione.Value.Year >= (DateTime.Now.Year - 5),
                affissione = d.num_giorni_contribuzione.HasValue ? "Affissione per num.giorni: " + d.num_giorni_contribuzione.Value + ". Manifesti: " + d.testo1 + "." : string.Empty
            }).ToList();
        }
    }
}
