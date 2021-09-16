using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDenunceOggettiLinq
    {
        public static IQueryable<join_denunce_oggetti> WhereByIdOggettoContribuzione(this IQueryable<join_denunce_oggetti> p_query, int p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.id_oggetti_contribuzione == p_idOggettoContribuzione);
        }

        public static IQueryable<join_denunce_oggetti> WhereByIdOggettoContribuzioneDec(this IQueryable<join_denunce_oggetti> p_query, decimal p_idOggettoContribuzione)
        {
            return p_query.Where(d => d.id_oggetti_contribuzione == p_idOggettoContribuzione);
        }

        public static IQueryable<join_denunce_oggetti> WhereByIdOggettoContribuzioneDecListAndNotNull(this IQueryable<join_denunce_oggetti> p_query, IList<decimal> p_idOggettoContribuzioneList)
        {
            return p_query.Where(d => d.id_oggetti_contribuzione!=null && p_idOggettoContribuzioneList.Contains( d.id_oggetti_contribuzione.Value ));
        }

        public static IQueryable<join_denunce_oggetti> WhereByIdDenuncia(this IQueryable<join_denunce_oggetti> p_query, int p_idDenuncia)
        {
            return p_query.Where(d => d.id_denunce_contratti == p_idDenuncia);
        }

        public static IQueryable<join_denunce_oggetti> WhereByCodStato(this IQueryable<join_denunce_oggetti> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_denunce_oggetti> WhereByCodStatoNot(this IQueryable<join_denunce_oggetti> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_denunce_oggetti> WhereByDenunciaCodStato(this IQueryable<join_denunce_oggetti> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_denunce_contratti.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_denunce_oggetti> WhereByDenunciaCodStatoNot(this IQueryable<join_denunce_oggetti> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.tab_denunce_contratti.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_denunce_oggetti> WhereByIdContribuente(this IQueryable<join_denunce_oggetti> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.tab_denunce_contratti.id_contribuente == p_idContribuente);
        }

        public static IQueryable<join_denunce_oggetti> OrderByDataPresentazione(this IQueryable<join_denunce_oggetti> p_query)
        {
            return p_query.OrderBy(d => d.tab_denunce_contratti.data_presentazione);
        }

        public static IQueryable<join_denunce_oggetti> WhereCodStatoNullOrNotAnn(this IQueryable<join_denunce_oggetti> p_query)
        {
            return p_query.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_denunce.ANN) || x.cod_stato == null);
        }

        public static IQueryable<join_denunce_oggetti> WhereDenunceContrattiNotNull(this IQueryable<join_denunce_oggetti> p_query)
        {
            return p_query.Where(x => x.id_denunce_contratti != null);
        }
    }
}