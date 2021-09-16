using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabSupervisioneFinaleV2Linq
    {
        public static TAB_SUPERVISIONE_FINALE_V2 WhereByAvvisoEmessoFirstOrDefault(this IQueryable<TAB_SUPERVISIONE_FINALE_V2> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.ID_AVVPAG_EMESSO == p_idAvvPag).FirstOrDefault();
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> WhereByAvvisoEmessoNull(this IQueryable<TAB_SUPERVISIONE_FINALE_V2> p_query)
        {
            return p_query.Where(d => !d.ID_AVVPAG_EMESSO.HasValue);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> WhereByAvvisoEmesso(this IQueryable<TAB_SUPERVISIONE_FINALE_V2> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.ID_AVVPAG_EMESSO == p_idAvvPag);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> WhereByPreavvisoNull(this IQueryable<TAB_SUPERVISIONE_FINALE_V2> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => !d.id_avvpag_preavviso_collegato.HasValue);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> WhereByPreavviso(this IQueryable<TAB_SUPERVISIONE_FINALE_V2> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avvpag_preavviso_collegato == p_idAvvPag);
        }

        public static IQueryable<TAB_SUPERVISIONE_FINALE_V2> WhereByCodStato(this IQueryable<TAB_SUPERVISIONE_FINALE_V2> p_query, string p_codStato)
        {
            return p_query.Where(d => d.COD_STATO.StartsWith(p_codStato));
        }
    }
}
