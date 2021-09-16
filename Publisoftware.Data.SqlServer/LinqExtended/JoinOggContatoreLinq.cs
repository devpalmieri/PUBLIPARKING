using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinOggContatoreLinq
    {
        public static IQueryable<join_ogg_contatore> WhereByValidState(this IQueryable<join_ogg_contatore> p_query)
        {
            return p_query.Where(d => d.tab_contatore.anagrafica_stato_contatore.cod_stato == anagrafica_stato_contatore.ATT_ATT || 
                                      d.tab_contatore.anagrafica_stato_contatore.cod_stato == anagrafica_stato_contatore.SOS_SOS);
        }

        public static IQueryable<join_ogg_contatore> WhereInOverlapPeriodoContribuzione(this IQueryable<join_ogg_contatore> p_query, tab_oggetti_contribuzione p_oggettoContribuzione)
        {

            return p_query.Where(d => p_oggettoContribuzione.data_inizio_contribuzione <= (d.tab_contatore.data_cessazione.HasValue ? d.tab_contatore.data_cessazione.Value : DateTime.MaxValue) && 
                                      d.tab_contatore.data_istallazione_presa_incarico <= (p_oggettoContribuzione.data_fine_contribuzione.HasValue ? p_oggettoContribuzione.data_fine_contribuzione.Value : DateTime.MaxValue));
        }

        public static IQueryable<join_ogg_contatore> WhereByIdOggetto(this IQueryable<join_ogg_contatore> p_query, decimal p_idOggetto)
        {
            return p_query.Where(d => d.id_oggetto == p_idOggetto);
        }

        public static IQueryable<join_ogg_contatore> WhereByIdContatore(this IQueryable<join_ogg_contatore> p_query, decimal p_idContatore)
        {
            return p_query.Where(d => d.id_contatore == p_idContatore);
        }

        public static IQueryable<join_ogg_contatore> OrderByDefault(this IQueryable<join_ogg_contatore> p_query)
        {
            return p_query.OrderBy(d => d.id_oggetto);
        }

        public static IQueryable<join_ogg_contatore> OrderByDataInstallazione(this IQueryable<join_ogg_contatore> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_contatore.data_istallazione_presa_incarico);
        }
    }
}
