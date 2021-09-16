using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabUnitaContribuzioneFattEmissioneLinq
    {
        public static IQueryable<tab_unita_contribuzione_fatt_emissione> WhereHasIdOggetto(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query)
        {
            return p_query.Where(w => w.id_oggetto != null);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> WhereByIdAvvPagGenerato(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_generato == p_idAvvPag);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> WhereByImportoPositivo(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query)
        {
            return p_query.Where(d => d.importo_unita_contribuzione > 0 ||
                                      d.imp_maggiorazione_onere_riscossione_61_90 > 0 ||
                                      d.imp_maggiorazione_onere_riscossione_121 > 0);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> OrderByNumRiga(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query)
        {
            return p_query.OrderBy(o => o.num_riga_avv_pag_generato);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> WhereByTabAvvPagIdListaEmissione(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query, int p_idListaEmissione)
        {
            return p_query.Where(w => w.tab_avv_pag_fatt_emissione.id_lista_emissione == p_idListaEmissione);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> WhereByTabAgevolazioniIdAnagAgevolazioni(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query, int p_idAnagraficaAgevolazione)
        {
            return p_query.Where(u =>
                (u.tab_agevolazioni != null && u.tab_agevolazioni.anagrafica_agevolazione != null && u.tab_agevolazioni.anagrafica_agevolazione.id_anagrafica_agevolazione == p_idAnagraficaAgevolazione) ||
                (u.tab_agevolazioni1 != null && u.tab_agevolazioni1.anagrafica_agevolazione != null && u.tab_agevolazioni1.anagrafica_agevolazione.id_anagrafica_agevolazione == p_idAnagraficaAgevolazione) ||
                (u.tab_agevolazioni2 != null && u.tab_agevolazioni2.anagrafica_agevolazione != null && u.tab_agevolazioni2.anagrafica_agevolazione.id_anagrafica_agevolazione == p_idAnagraficaAgevolazione) ||
                (u.tab_agevolazioni3 != null && u.tab_agevolazioni3.anagrafica_agevolazione != null && u.tab_agevolazioni3.anagrafica_agevolazione.id_anagrafica_agevolazione == p_idAnagraficaAgevolazione)
                );
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> WhereByIdOggetto(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query, int p_idOggetto)
        {
            return p_query.Where(w => w.id_oggetto == p_idOggetto);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione> OrderByDefault(this IQueryable<tab_unita_contribuzione_fatt_emissione> p_query)
        {
            return p_query.OrderBy(tuc => tuc.id_entrata);
        }

        public static IQueryable<tab_unita_contribuzione_fatt_emissione_light> ToLight(this IQueryable<tab_unita_contribuzione_fatt_emissione> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_unita_contribuzione_fatt_emissione_light
            {
                id_unita_contribuzione = d.id_unita_contribuzione,
                anno_rif = d.anno_rif,
                periodo_contribuzione_da_String = d.periodo_contribuzione_da_String,
                periodo_contribuzione_a_String = d.periodo_contribuzione_a_String,
                periodo_contribuzione_String = d.periodo_contribuzione_String,
                periodo_rif_da_String = d.periodo_rif_da_String,
                periodo_rif_a_String = d.periodo_rif_a_String,
                periodo_rif_String = d.periodo_rif_String,
                um_unita = d.um_unita,
                QuantitaUnitaContribuzione = d.QuantitaUnitaContribuzione,
                quantita_unita_contribuzione = d.quantita_unita_contribuzione,
                importo_unitario_contribuzione_Euro = d.importo_unitario_contribuzione_Euro,
                importo_unita_contribuzione = d.importo_unita_contribuzione,
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
                isUnitaSemplice = !d.id_avv_pag_collegato.HasValue && d.tab_unita_contribuzione_fatt_emissione1.Count == 0 ? true : false,
                num_riga_avv_pag_generato = d.num_riga_avv_pag_generato,
                periodo_contribuzione_da = d.periodo_contribuzione_da,
                id_oggetto_contribuzione = d.id_oggetto_contribuzione,
                id_oggetto = d.id_oggetto,
                id_entrata = d.id_entrata,
                id_anagrafica_voce_contribuzione = d.id_anagrafica_voce_contribuzione,
                codice_tributo_ministeriale = (d.tab_tipo_voce_contribuzione1 != null && d.tab_tipo_voce_contribuzione1.codice_tributo_ministeriale != null) ? d.tab_tipo_voce_contribuzione1.codice_tributo_ministeriale.Trim() : string.Empty,
                id_tab_macroentrate = d.id_tab_macroentrate,
                flag_segno = d.flag_segno,
                flag_tipo_composto = d.tab_avv_pag_fatt_emissione.anagrafica_tipo_avv_pag.flag_tipo_composto,
                color = "",
                oggettoGroupBy = (d.tab_oggetti == null || (d.tab_oggetti != null && (!d.tab_oggetti.tab_tipo_oggetto.isUbicazione))) ? string.Empty : d.tab_oggetti.Ubicazione + ";" + d.id_oggetto.Value,
                importo_sgravio = d.importo_sgravio.HasValue ? d.importo_sgravio.Value : 0,
                importo_sgravio_Euro = d.importo_sgravio_Euro,
                importo_sgravato = d.importo_sgravato,
                importo_sgravato_Euro = d.importo_sgravato_Euro,
                cod_stato = d.cod_stato,
                imponibile_unita_contribuzione_Euro = d.imponibile_unita_contribuzione_Euro,
                iva = d.iva,
                affissione = d.num_giorni_contribuzione.HasValue ? "Affissione per num.giorni: " + d.num_giorni_contribuzione.Value + ". Manifesti: " + d.testo1 + "." : string.Empty
            }).AsQueryable();
        }
    }
}
