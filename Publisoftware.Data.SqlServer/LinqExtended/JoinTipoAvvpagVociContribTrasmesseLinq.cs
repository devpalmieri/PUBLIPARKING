using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTipoAvvpagVociContribTrasmesseLinq
    {
        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> WhereByListIdServizio(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query, List<int> v_listIdServizio)
        {
            return p_query.Where(e => v_listIdServizio.Contains(e.anagrafica_tipo_avv_pag.id_servizio));
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> WhereByIdEnte(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> WhereByIdTipoAvvPag(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.id_tipo_avv_pag == p_idTipoAvvPag);
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> WhereByCodiceTributoMinisteriale(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query, string p_codice)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim().Equals(p_codice));
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> WhereByFlagAccertamento(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query, string p_flag)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione.flag_accertamento_contabile == p_flag);
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> WhereByEntrataAvvPagCollegatiNotNull(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query)
        {
            return p_query.Where(e => e.anagrafica_tipo_avv_pag != null && e.anagrafica_tipo_avv_pag.anagrafica_entrate1 != null);
        }

        public static IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> OrderByDefault(this IQueryable<join_tipo_avvpag_voci_contrib_trasmesse> p_query)
        {
            return p_query.OrderBy(e => e.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);
        }
    }
}
