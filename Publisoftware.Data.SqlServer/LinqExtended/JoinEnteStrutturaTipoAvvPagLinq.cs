using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinEnteStrutturaTipoAvvPagLinq
    {
        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByListIdServizio(this IQueryable<join_ente_strutture_tipoavvpag> p_query, List<int> v_listIdServizio)
        {
            return p_query.Where(e => v_listIdServizio.Contains(e.anagrafica_tipo_avv_pag.id_servizio));
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByIdTipoAvviso(this IQueryable<join_ente_strutture_tipoavvpag> p_query, int p_idTipoAvviso)
        {
            return p_query.Where(e => e.id_tipo_avvpag == p_idTipoAvviso);
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByIdEnte(this IQueryable<join_ente_strutture_tipoavvpag> p_query, int p_idEnte)
        {
            return p_query.Where(e => e.id_ente == p_idEnte);
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByIdStruttura(this IQueryable<join_ente_strutture_tipoavvpag> p_query, int p_idStruttura)
        {
            return p_query.Where(e => e.id_struttura == p_idStruttura);
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByIdEntrata(this IQueryable<join_ente_strutture_tipoavvpag> p_query, int p_idEntrata)
        {
            return p_query.Where(e => e.anagrafica_tipo_avv_pag.id_entrata == p_idEntrata);
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByTipoGestione(this IQueryable<join_ente_strutture_tipoavvpag> p_query, string p_flag)
        {
            return p_query.Where(e => e.flag_tipo_gestione_avvpag == p_flag);
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> WhereByEntrataAvvPagCollegatiNotNull(this IQueryable<join_ente_strutture_tipoavvpag> p_query)
        {
            return p_query.Where(e => e.anagrafica_tipo_avv_pag != null && e.anagrafica_tipo_avv_pag.anagrafica_entrate1 != null);
        }

        public static IQueryable<join_ente_strutture_tipoavvpag> OrderByDefault(this IQueryable<join_ente_strutture_tipoavvpag> p_query)
        {
            return p_query.OrderBy(e => e.anagrafica_tipo_avv_pag.descr_tipo_avv_pag);
        }
    }
}
