using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTipoStatoReferenzaLinq
    {
        public static IQueryable<join_tipo_stato_referenza> WhereByIdTipo(this IQueryable<join_tipo_stato_referenza> p_query, int p_idTipo)
        {
            return p_query.Where(d => d.id_tipo_contribuente == p_idTipo);
        }

        public static IQueryable<join_tipo_stato_referenza> WhereByIdStato(this IQueryable<join_tipo_stato_referenza> p_query, int p_idStato)
        {
            return p_query.Where(d => d.id_stato_contribuente == p_idStato);
        }

        public static IQueryable<join_tipo_stato_referenza> WhereByIdTipoIdStato(this IQueryable<join_tipo_stato_referenza> p_query, int p_idTipo, int p_idStato)
        {
            return p_query.Where(d => d.id_tipo_contribuente == p_idTipo && d.id_stato_contribuente == p_idStato);
        }

        public static IQueryable<join_tipo_stato_referenza> WhereByIdTipoIdRelazione(this IQueryable<join_tipo_stato_referenza> p_query, int p_idTipo, int p_idRelazione)
        {
            return p_query.Where(d => d.id_tipo_contribuente == p_idTipo && d.id_tipo_relazione == p_idRelazione);
        }

        public static IQueryable<join_tipo_stato_referenza> WhereByIdTipoIdStatoIdRelazione(this IQueryable<join_tipo_stato_referenza> p_query, int p_idTipo, int p_idStato, int p_idRelazione)
        {
            return p_query.Where(d => d.id_tipo_contribuente == p_idTipo && d.id_stato_contribuente == p_idStato && d.id_tipo_relazione == p_idRelazione);
        }

        public static IQueryable<join_tipo_stato_referenza> WhereByTipoRelazioneNotNull(this IQueryable<join_tipo_stato_referenza> p_query)
        {
            return p_query.Where(d => d.id_tipo_relazione.HasValue);
        }

        public static IQueryable<join_tipo_stato_referenza> WhereByTipoContribuenteNull(this IQueryable<join_tipo_stato_referenza> p_query)
        {
            return p_query.Where(d => !d.id_tipo_contribuente.HasValue);
        }

        public static IQueryable<join_tipo_stato_referenza> OrderByStato(this IQueryable<join_tipo_stato_referenza> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_stato_contribuente.des_stato_contribuente);
        }
    }
}
