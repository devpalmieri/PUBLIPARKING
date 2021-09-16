using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTerzoReferenteLinq
    {
        public static IQueryable<join_terzo_referente> WhereByIdTerzo(this IQueryable<join_terzo_referente> p_query, decimal p_idTerzo)
        {
            return p_query.Where(d => d.id_terzo == p_idTerzo);
        }

        public static IQueryable<join_terzo_referente> WhereByIdReferente(this IQueryable<join_terzo_referente> p_query, int p_idReferente)
        {
            return p_query.Where(d => d.id_referente == p_idReferente);
        }

        public static IQueryable<join_terzo_referente> WhereByCodStato(this IQueryable<join_terzo_referente> p_query, string p_cod_stato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_cod_stato));
        }
    }
}
