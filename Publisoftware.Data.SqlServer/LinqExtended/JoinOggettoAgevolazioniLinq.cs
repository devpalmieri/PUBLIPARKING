using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinOggettoAgevolazioniLinq
    {
        public static IQueryable<join_oggetto_agevolazioni> WhereByCodStato(this IQueryable<join_oggetto_agevolazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByCodStatoNot(this IQueryable<join_oggetto_agevolazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByAgevolazioneCodStato(this IQueryable<join_oggetto_agevolazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_agevolazioni.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByAgevolazioneCodStatoNot(this IQueryable<join_oggetto_agevolazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_agevolazioni.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByIdOggetto(this IQueryable<join_oggetto_agevolazioni> p_query, Decimal p_idOggetto)
        {
            return p_query.Where(d => d.id_oggetto == p_idOggetto);
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByIdContribuente(this IQueryable<join_oggetto_agevolazioni> p_query, Decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByRangeUnitaContribuzione(this IQueryable<join_oggetto_agevolazioni> p_query, tab_unita_contribuzione v_unitaContribuzione)
        {
            return p_query.Where(d => d.tab_agevolazioni.data_inizio_validita <= v_unitaContribuzione.periodo_contribuzione_da &&
                                    (!d.tab_agevolazioni.data_fine_validita.HasValue ||
                                      d.tab_agevolazioni.data_fine_validita.Value >= v_unitaContribuzione.periodo_contribuzione_a));
        }

        public static IQueryable<join_oggetto_agevolazioni> WhereByAgevolazioniUnitaContribuzione(this IQueryable<join_oggetto_agevolazioni> p_query, tab_unita_contribuzione v_unitaContribuzione)
        {
            return p_query.Where(d => d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione1 &&
                                      d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione2 &&
                                      d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione3 &&
                                      d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione4);
        }

        public static IQueryable<join_oggetto_agevolazioni_light> ToLight(this IQueryable<join_oggetto_agevolazioni> iniziale)
        {
            return iniziale.ToList().Select(d => new join_oggetto_agevolazioni_light
            {
                id_join_oggetto_agevolazioni = d.id_join_oggetto_agevolazioni,
                id_tab_agevolazioni = d.id_tab_agevolazioni,
                codice = d.tab_agevolazioni.anagrafica_agevolazione.cod_agevolazione,
                descrizione = d.tab_agevolazioni.anagrafica_agevolazione.des_agevolazione,
                tipo = d.tab_agevolazioni.anagrafica_agevolazione.anagrafica_tipo_agevolazione.des_tipo_agevolazione,
                macrocategoria = d.tab_agevolazioni.anagrafica_agevolazione.macrocategoria,
                percentuale = d.tab_agevolazioni.anagrafica_agevolazione.Percentuale,
                periodo_validita_String = d.tab_agevolazioni.periodo_validita_String,
                stato = d.tab_agevolazioni.anagrafica_stato_agevolazione.desc_stato,
                riduzione = d.tab_agevolazioni.Riduzione,
                rinnovo = d.tab_agevolazioni.Rinnovo,
                occupante = d.tab_agevolazioni.Occupante
            }).AsQueryable();
        }
    }
}
