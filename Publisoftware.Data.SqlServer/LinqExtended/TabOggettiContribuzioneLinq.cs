using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabOggettiContribuzioneLinq
    {
        public static IQueryable<tab_oggetti_contribuzione> WhereByIdContribuente(this IQueryable<tab_oggetti_contribuzione> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByIdOggetto(this IQueryable<tab_oggetti_contribuzione> p_query, decimal p_idOggetto)
        {
            return p_query.Where(d => d.id_oggetto == p_idOggetto);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByIdOggettoContribuzioneNot(this IQueryable<tab_oggetti_contribuzione> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.id_oggetto_contribuzione != p_idOggettoContribuzione);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByIdEntrata(this IQueryable<tab_oggetti_contribuzione> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.tab_oggetti.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByIdTipoOggetto(this IQueryable<tab_oggetti_contribuzione> p_query, int p_idTipoOggetto)
        {
            return p_query.Where(d => d.tab_oggetti.id_tipo_oggetto == p_idTipoOggetto);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByCodStato(this IQueryable<tab_oggetti_contribuzione> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato_oggetto.StartsWith(p_codStato));
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByDataInizioContribuzione(this IQueryable<tab_oggetti_contribuzione> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_inizio_contribuzione == p_data);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByDataFineContribuzione(this IQueryable<tab_oggetti_contribuzione> p_query, DateTime? p_data)
        {
            return p_query.Where(d => d.data_fine_contribuzione == p_data);
        }

        public static IQueryable<tab_oggetti_contribuzione> OrderByDefault(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return p_query.OrderBy(d => d.data_variazione).ThenBy(d => d.data_inizio_contribuzione).ThenBy(d => d.id_oggetto_contribuzione);
        }

        public static IQueryable<tab_oggetti_contribuzione> OrderByDataFineContribuzioneDesc(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return p_query.OrderByDescending(d => d.data_fine_contribuzione);
        }

        public static IQueryable<tab_oggetti_contribuzione> OrderByIdOggettoContribuzioneDesc(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return p_query.OrderByDescending(d => d.id_oggetto_contribuzione);
        }

        public static IQueryable<tab_oggetti_contribuzione_light> ListToLight(this IList<tab_oggetti_contribuzione> iniziale)
        {
            return iniziale.Select(d => new tab_oggetti_contribuzione_light
            {
                id_oggetto_contribuzione = d.id_oggetto_contribuzione,
                id_oggetto = d.id_oggetto,
                Ubicazione = d.tab_oggetti == null ? string.Empty : d.tab_oggetti.Ubicazione,
                DataVariazione = d.data_variazione_String,
                PeriodoContribuzione = d.PeriodoContribuzione,
                data_inizio_contribuzione_String = d.data_inizio_contribuzione_String,
                descrizioneCategoria = d.anagrafica_categoria == null ? string.Empty : d.anagrafica_categoria.des_cat_contr,
                rendita = d.Rendita,
                possesso = d.PercentualePossesso,
                numTotOccupanti = d.NumTotOccupanti,
                stato = d.anagrafica_stato_oggetto.desc_stato,
                matrContatore = string.Empty,
                numFacce = d.NumFacce,
                dim = d.Dimensioni,
                sup = d.SuperficieTassabile,
                tipo = d.anagrafica_categoria.TipoTosapCosap,
                denuncia = d.Denuncia
            }).AsQueryable();
        }

        [Obsolete("Non funziona, devi prima eseguire query: usare ListToLight che è più esplicito")]
        public static IQueryable<tab_oggetti_contribuzione_light> ToLight(this IQueryable<tab_oggetti_contribuzione> iniziale)
        {
            return iniziale.Select(d => new tab_oggetti_contribuzione_light
            {
                id_oggetto_contribuzione = d.id_oggetto_contribuzione,
                id_oggetto = d.id_oggetto,
                Ubicazione = d.tab_oggetti == null ? string.Empty : d.tab_oggetti.Ubicazione,
                DataVariazione = d.data_variazione_String,
                PeriodoContribuzione = d.PeriodoContribuzione,
                data_inizio_contribuzione_String = d.data_inizio_contribuzione_String,
                descrizioneCategoria = d.anagrafica_categoria == null ? string.Empty : d.anagrafica_categoria.des_cat_contr,
                rendita = d.Rendita,
                possesso = d.PercentualePossesso,
                numTotOccupanti = d.NumTotOccupanti,
                stato = d.anagrafica_stato_oggetto.desc_stato,
                matrContatore = string.Empty,
                numFacce = d.NumFacce,
                dim = d.Dimensioni,
                sup = d.SuperficieTassabile,
                tipo = d.anagrafica_categoria.TipoTosapCosap,
                denuncia = d.Denuncia
            }).AsQueryable();
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereAttivi(this IQueryable<tab_oggetti_contribuzione> p_query, DateTime p_dataInizio, DateTime p_dataFine)
        {
            return p_query.Where(oc => oc.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ATT) &&
                                       oc.quantita_base > 0 &&
                                       oc.data_inizio_contribuzione <= p_dataFine &&
                                       (oc.data_fine_contribuzione == null || oc.data_fine_contribuzione >= p_dataInizio) &&
                                       (oc.data_allaciamento_fognatura == null || oc.data_allaciamento_fognatura < p_dataInizio));
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereAttiviAccertamenti(this IQueryable<tab_oggetti_contribuzione> p_query, DateTime p_dataInizio, DateTime p_dataFine)
        {
            return p_query.Where(oc => oc.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.ATT) &&
                                       oc.quantita_base > 0 &&
                                       oc.data_inizio_contribuzione <= p_dataFine &&
                                       (oc.data_fine_contribuzione == null || oc.data_fine_contribuzione >= p_dataInizio) &&
                                       (oc.data_allaciamento_fognatura == null || oc.data_allaciamento_fognatura > p_dataInizio));
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereRett(this IQueryable<tab_oggetti_contribuzione> p_query, DateTime p_dataInizio, DateTime p_dataFine)
        {
            return p_query.Where(oc => oc.cod_stato_oggetto.StartsWith(anagrafica_stato_oggetto.RET) &&
                                       oc.quantita_base > 0 &&
                                       oc.data_inizio_contribuzione <= p_dataFine &&
                                       (oc.data_fine_contribuzione == null || oc.data_fine_contribuzione >= p_dataInizio) &&
                                       (oc.data_allaciamento_fognatura == null || oc.data_allaciamento_fognatura < p_dataInizio));
        }

        [Obsolete("Usare IncludeInfoChildDenunceTari")]
        public static IQueryable<tab_oggetti_contribuzione> IncludeInfoChild(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return IncludeInfoChildDenunceTari(p_query);
        }

        // Gli include son ui po' tanti: in fase di ottimizzazione creare una proiezione adeguata
        public static IQueryable<tab_oggetti_contribuzione> IncludeInfoChildDenunceTari(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return p_query
                .Include(x => x.anagrafica_stato_oggetto)
                .Include(x => x.anagrafica_categoria)
                .Include(x => x.tab_macroentrate)
                .Include(x => x.tab_oggetti)
                .Include(x => x.tab_oggetti.join_ogg_contatore)
                .Include(x => x.tab_oggetti.tab_toponimi);
        }

        // Gli include son ui po' tanti: in fase di ottimizzazione creare una proiezione adeguata
        public static IQueryable<tab_oggetti_contribuzione> IncludeInfoChildDenunceImu(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return p_query
                .Include(x => x.anagrafica_stato_oggetto)
                .Include(x => x.anagrafica_categoria)
                .Include(x => x.tab_macroentrate)
                .Include(x => x.tab_oggetti)
                .Include(x => x.tab_oggetti.join_ogg_contatore)
                .Include(x => x.tab_oggetti.tab_toponimi)
                
                .Include(x=>x.anagrafica_utilizzo)
                ;
        }

        // Gli include son ui po' tanti: in fase di ottimizzazione creare una proiezione adeguata
        public static IQueryable<tab_oggetti_contribuzione> IncludeInfoChildGestioneCanonePatrimonialeOccupazioneSuoloPubblico(this IQueryable<tab_oggetti_contribuzione> p_query)
        {
            return p_query
                .Include(x => x.anagrafica_stato_oggetto)
                .Include(x => x.anagrafica_categoria)
                .Include(x => x.tab_macroentrate)
                .Include(x => x.tab_oggetti)
                .Include(x => x.tab_oggetti.join_ogg_contatore)
                .Include(x => x.tab_oggetti.tab_toponimi)

                .Include(x => x.anagrafica_utilizzo)
                ;
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereByIdMacroentrata(this IQueryable<tab_oggetti_contribuzione> query, int idMacroentrata)
        {
            // N.B.: id_entrata si sarebbe dovuta chiamare id_macroentrata e...
            return query.Where(x => x.id_entrata == idMacroentrata); 
            // ...equivale a: return query.Where(x => x.tab_macroentrate.id_tab_macroentrate == idMacroentrata);
        }

        public static IQueryable<tab_oggetti_contribuzione> WhereAttiviMaNonVariatiAl(this IQueryable<tab_oggetti_contribuzione> p_query, DateTime alDate)
        {
            return p_query.Where(oc => (oc.cod_stato_oggetto == anagrafica_stato_oggetto.ATTIVO) && // N.B.: solo ATTIVO non ci interessano i "oc.cod_stato_oggetto == anagrafica_stato_oggetto.VARIATO)"
                                       oc.data_inizio_contribuzione <= alDate &&
                                       (oc.data_fine_contribuzione == null || oc.data_fine_contribuzione >= alDate));
        }

    }
}
