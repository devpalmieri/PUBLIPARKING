using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.POCOLight;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinOggettoDatiMetriciLinq
    {
        public static IQueryable<join_oggetto_dati_metrici> WhereByIdOggetto(this IQueryable<join_oggetto_dati_metrici> p_query, int p_idOggetto)
        {
            return p_query.Where(d => d.id_oggetto == p_idOggetto);
        }

        public static IQueryable<join_oggetto_dati_metrici> WhereByIdOggettoM(this IQueryable<join_oggetto_dati_metrici> p_query, decimal p_idOggetto)
        {
            return p_query.Where(x => x.id_oggetto == p_idOggetto);
        }

        public static IQueryable<join_oggetto_dati_metrici> WhereByDatiMetriciCatastaliNotNull(this IQueryable<join_oggetto_dati_metrici> p_query, bool bAncheDatiMetriciDichiarati = true)
        {
            return bAncheDatiMetriciDichiarati
                ? p_query.Where(d => d.id_dati_metrici_catastali.HasValue || d.id_dati_metrici_dichiarati.HasValue)
                : p_query.Where(d => d.id_dati_metrici_catastali.HasValue);
        }

        public static IQueryable<join_oggetto_dati_metrici> OrderByDefault(this IQueryable<join_oggetto_dati_metrici> p_query)
        {
            return p_query.OrderBy(d => d.tab_dati_metrici_catastali.id_tab_dati_metrici_catastali);
        }

        public static IQueryable<join_oggetto_dati_metrici> WhereCodStatoLikeAttivo(this IQueryable<join_oggetto_dati_metrici> p_query)
        {
            return p_query.Where(x => x.cod_stato.StartsWith(CodStato.ATT));
        }

        public static IQueryable<join_oggetto_dati_metrici> WhereByMqOccupazioneDichiaratiNotNull(this IQueryable<join_oggetto_dati_metrici> p_query)
        {
            return p_query.Where(x => x.mq_occupazione_dichiarati != null);
        }

        public static IQueryable<join_oggetto_dati_metrici_light> ToLight(this IQueryable<join_oggetto_dati_metrici> iniziale)
        {
            return iniziale.ToList().Select(d => new join_oggetto_dati_metrici_light
            {
                id_join_oggetto_catasto = d.id_join_oggetto_catasto,
                id_dati_metrici_catastali = d.id_dati_metrici_catastali,
                id_dati_metrici_dichiarati = d.id_dati_metrici_dichiarati,
                Categoria = (d.tab_dati_metrici_catastali != null && d.tab_dati_metrici_catastali.tab_categorie_fabbricati != null) ? d.tab_dati_metrici_catastali.tab_categorie_fabbricati.descrizione : string.Empty,
                Superficie = d.tab_dati_metrici_catastali != null ? d.tab_dati_metrici_catastali.Superficie : (d.tab_dati_metrici_dichiarati != null ? d.tab_dati_metrici_dichiarati.Superficie : string.Empty),
                Foglio = d.tab_dati_metrici_catastali != null ? d.tab_dati_metrici_catastali.foglio : (d.tab_dati_metrici_dichiarati != null ? d.tab_dati_metrici_dichiarati.foglio : string.Empty),
                Numero = d.tab_dati_metrici_catastali != null ? d.tab_dati_metrici_catastali.numero : (d.tab_dati_metrici_dichiarati != null ? d.tab_dati_metrici_dichiarati.numero : string.Empty),
                Particella = d.tab_dati_metrici_catastali != null ? d.tab_dati_metrici_catastali.particella : (d.tab_dati_metrici_dichiarati != null ? d.tab_dati_metrici_dichiarati.particella : string.Empty),
                Subalterno = d.tab_dati_metrici_catastali != null ? d.tab_dati_metrici_catastali.subalterno : (d.tab_dati_metrici_dichiarati != null ? d.tab_dati_metrici_dichiarati.subalterno : string.Empty)
            }).AsQueryable();
        }
    }
}
