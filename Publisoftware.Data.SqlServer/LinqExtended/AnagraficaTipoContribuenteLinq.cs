using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoContribuenteLinq
    {

        /// <summary>
        /// Ordinamento per la descrizione della sigla
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_contribuente> OrderByDescrizioneSigla(this IQueryable<anagrafica_tipo_contribuente> p_query)
        {
            return p_query.OrderBy(e => e.descr_sigla);
        }

        /// <summary>
        /// Ordinamento per la descrizione del tipo contribuente
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_contribuente> OrderByDescrizioneTipoContribuente(this IQueryable<anagrafica_tipo_contribuente> p_query)
        {
            return p_query.OrderBy(e => e.desc_tipo_contribuente);
        }

        public static IQueryable<anagrafica_tipo_contribuente> OrderByOrdinamentoDescrizioneTipoContribuente(this IQueryable<anagrafica_tipo_contribuente> p_query)
        {
            return p_query.OrderBy(e => e.ordinamento).ThenBy(e => e.desc_tipo_contribuente);
        }

        /// <summary>
        /// Ordinamento di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_contribuente> OrderByDefault(this IQueryable<anagrafica_tipo_contribuente> p_query)
        {
            return p_query.OrderBy(e => e.ordinamento);
        }

        /// <summary>
        /// Filtro per la sigla
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_Sigla"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_contribuente> WhereBySigla(this IQueryable<anagrafica_tipo_contribuente> p_query, string p_Sigla)
        {
            return p_query.Where(d => d.sigla_tipo_contribuente == p_Sigla);
        }

        public static IQueryable<anagrafica_tipo_contribuente> WhereBySiglaNot(this IQueryable<anagrafica_tipo_contribuente> p_query, string p_Sigla)
        {
            return p_query.Where(d => d.sigla_tipo_contribuente != p_Sigla);
        }

        public static IQueryable<anagrafica_tipo_contribuente> WhereByFisiciGiuridici(this IQueryable<anagrafica_tipo_contribuente> p_query)
        {
            return p_query.Where(d => d.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA || d.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_GIURIDICA);
        }

        /// <summary>
        /// Raggruppamento per sigla, descrizione sigla ed ordinamento
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_tipo_contribuente> GroupBySigla(this IQueryable<anagrafica_tipo_contribuente> p_query)
        {
            return p_query.GroupBy(p => new { p.sigla_tipo_contribuente, p.descr_sigla, p.ordinamento }).Select(g => g.FirstOrDefault());
        }
    }
}
