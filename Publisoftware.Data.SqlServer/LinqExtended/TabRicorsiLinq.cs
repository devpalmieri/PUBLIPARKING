using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRicorsiLinq
    {
        public static IQueryable<tab_ricorsi> WhereByConSentenzaAcquisita(this IQueryable<tab_ricorsi> p_query)
        {
            return p_query.Where(d => d.tab_sentenze.Count() > 0 && 
                                      d.tab_sentenze.FirstOrDefault().cod_stato.Equals(tab_sentenze.ATT_ATT));
        }

        public static IQueryable<tab_ricorsi> WhereByIdTabDocInput(this IQueryable<tab_ricorsi> p_query, int p_idTabDocInput)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_idTabDocInput);
        }

        public static IQueryable<tab_ricorsi> WhereByIdTabDocEntrate(this IQueryable<tab_ricorsi> p_query, int p_idTabDocEntrate)
        {
            return p_query.Where(d => d.tab_doc_input.id_tipo_doc_entrate == p_idTabDocEntrate);
        }

        public static IQueryable<tab_ricorsi> WhereByCodStato(this IQueryable<tab_ricorsi> p_query, string codStato)
        {
            return p_query.Where(d => d.cod_stato == codStato);
        }

        public static IQueryable<tab_ricorsi> WhereByCodStatoDocInput(this IQueryable<tab_ricorsi> p_query, string codStato)
        {
            return p_query.Where(d => d.tab_doc_input.cod_stato == codStato);
        }

        public static IQueryable<tab_ricorsi> WhereByCodiceIstanza(this IQueryable<tab_ricorsi> p_query, string codiceIstanzaRicerca)
        {
            return p_query.Where(d => d.tab_doc_input.identificativo_doc_input.Equals(codiceIstanzaRicerca));
        }

        public static IQueryable<tab_ricorsi> WhereByRangeDataPresentazione(this IQueryable<tab_ricorsi> p_query, DateTime? daRicorsoPresentazione, DateTime? aRicorsoPresentazione)
        {
            if (daRicorsoPresentazione.HasValue)
            {
                p_query = p_query.Where(d => d.tab_doc_input.data_presentazione >= daRicorsoPresentazione.Value || !d.tab_doc_input.data_presentazione.HasValue);
            }

            if (aRicorsoPresentazione.HasValue)
            {
                aRicorsoPresentazione = aRicorsoPresentazione.Value.AddHours(23).AddMinutes(59);
                p_query = p_query.Where(d => d.tab_doc_input.data_presentazione <= aRicorsoPresentazione.Value || !d.tab_doc_input.data_presentazione.HasValue);
            }

            return p_query;
        }

        public static IQueryable<tab_ricorsi> OrderByDataPresentazione(this IQueryable<tab_ricorsi> p_query)
        {
            return p_query.OrderBy(d => d.tab_doc_input.data_presentazione);
        }

        public static IQueryable<tab_ricorsi> OrderByDataScadenzaControdeduzioni(this IQueryable<tab_ricorsi> p_query)
        {
            return p_query.OrderByDescending(d => d.data_scadenza_controdeduzioni_ricorso);
        }
        public static IQueryable<tab_ricorsi> GroupByIdAutoritaGiudiziariaIdTipoDocEntrate(this IQueryable<tab_ricorsi> p_query)
        {
            return p_query.GroupBy(p => new { p.tab_doc_input.id_tipo_doc_entrate, p.tab_doc_input.id_autorita_giudiziaria }).Select(g => g.FirstOrDefault());
        }
        public static IQueryable<tab_ricorsi> OrderByIdAutoritaGiudiziariaIdTipoDocEntrate(this IQueryable<tab_ricorsi> p_query)
        {
            return p_query.OrderBy(d => d.tab_doc_input.id_tipo_doc_entrate).ThenBy(d => d.tab_doc_input.id_autorita_giudiziaria);
        }
    }
}