using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinSpuntistiPrenotazionePiazzoleLinq
    {
        public static IQueryable<join_spuntisti_prenotazione_piazzole> WhereByIdSpuntista(this IQueryable<join_spuntisti_prenotazione_piazzole> p_query, int p_idSpuntista)
        {
            return p_query.Where(d => d.id_spuntista == p_idSpuntista);
        }

        public static IQueryable<join_spuntisti_prenotazione_piazzole> WhereByIdAreaMercato(this IQueryable<join_spuntisti_prenotazione_piazzole> p_query, int p_idAreaMercato)
        {
            return p_query.Where(d => d.id_area_mercato == p_idAreaMercato);
        }

        public static IQueryable<join_spuntisti_prenotazione_piazzole> WhereByRangeData(this IQueryable<join_spuntisti_prenotazione_piazzole> p_query, DateTime p_dataDa, DateTime p_dataA)
        {
            return p_query.Where(d => d.giorno >= p_dataDa &&
                                      d.giorno <= p_dataA);

        }

        public static IQueryable<join_spuntisti_prenotazione_piazzole> WhereByAnno(this IQueryable<join_spuntisti_prenotazione_piazzole> p_query, int p_anno)
        {
            return p_query.Where(d => d.giorno.Value.Year == p_anno);
        }
    }
}