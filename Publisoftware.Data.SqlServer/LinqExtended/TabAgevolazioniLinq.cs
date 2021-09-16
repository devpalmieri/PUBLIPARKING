using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAgevolazioniLinq
    {
        public static IQueryable<tab_agevolazioni> WhereByCodStato(this IQueryable<tab_agevolazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_agevolazioni> WhereByCodStatoNot(this IQueryable<tab_agevolazioni> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_agevolazioni> WhereByRangeUnitaContribuzioneAnno(this IQueryable<tab_agevolazioni> p_query, int p_anno)
        {
            return p_query.Where(d => p_anno >= d.data_inizio_validita.Value.Year && (!d.data_fine_validita.HasValue || (d.data_fine_validita.HasValue && 
                                                                                                                         d.data_fine_validita.Value.Year >= p_anno)));
        }

        public static IQueryable<tab_agevolazioni> WhereByRangeUnitaContribuzione(this IQueryable<tab_agevolazioni> p_query, tab_unita_contribuzione v_unitaContribuzione)
        {
            return p_query.Where(d => d.data_inizio_validita <= v_unitaContribuzione.periodo_contribuzione_da &&
                                    (!d.data_fine_validita.HasValue || 
                                      d.data_fine_validita.Value >= v_unitaContribuzione.periodo_contribuzione_a));
        }

        public static IQueryable<tab_agevolazioni> WhereByAgevolazioniUnitaContribuzione(this IQueryable<tab_agevolazioni> p_query, tab_unita_contribuzione v_unitaContribuzione)
        {
            return p_query.Where(d => d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione1 &&
                                      d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione2 &&
                                      d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione3 &&
                                      d.id_tab_agevolazioni != v_unitaContribuzione.id_agevolazione4);
        }

        public static IQueryable<tab_agevolazioni> WhereByAnagraficaAgevolazione(this IQueryable<tab_agevolazioni> p_query, int p_idEntrata, int p_idEnte, string p_macrocategoria)
        {
            return p_query.Where(d => d.anagrafica_agevolazione.id_ente == p_idEnte &&
                                      d.anagrafica_agevolazione.id_entrata == p_idEntrata &&
                                      d.anagrafica_agevolazione.macrocategoria.Trim() == p_macrocategoria);
        }

        public static IQueryable<tab_agevolazioni_light> ToLight(this IQueryable<tab_agevolazioni> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_agevolazioni_light
            {
                id_tab_agevolazioni = d.id_tab_agevolazioni,
                codice = d.anagrafica_agevolazione.cod_agevolazione,
                descrizione = d.anagrafica_agevolazione.des_agevolazione,
                tipo = d.anagrafica_agevolazione.anagrafica_tipo_agevolazione.des_tipo_agevolazione,
                macrocategoria = d.anagrafica_agevolazione.macrocategoria,
                percentuale = d.anagrafica_agevolazione.Percentuale,
                periodo_validita_String = d.periodo_validita_String,
                stato = d.anagrafica_stato_agevolazione.desc_stato,
                riduzione = d.Riduzione,
                rinnovo = d.Rinnovo,
                occupante = d.Occupante
            }).AsQueryable();
        }
    }
}
