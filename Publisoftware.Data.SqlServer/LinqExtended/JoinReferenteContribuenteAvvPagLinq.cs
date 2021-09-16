using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinReferenteContribuenteAvvPagLinq
    {
        public static IQueryable<join_referente_contribuente_avv_pag> WhereByIdJoinReferenteContribuente(this IQueryable<join_referente_contribuente_avv_pag> p_query, int p_idJoinReferenteContribuente)
        {
            return p_query.Where(d => d.id_join_referente_contribuente == p_idJoinReferenteContribuente);
        }

        public static IQueryable<join_referente_contribuente_avv_pag> WhereByIdAvvPag(this IQueryable<join_referente_contribuente_avv_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag == p_idAvvPag);
        }
    }
}
