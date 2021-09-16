using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinCausaleDocNewLinq
    {
        public static IQueryable<join_causale_doc_new> GroupByIdFunzioneIdTipoDocEntrate(this IQueryable<join_causale_doc_new> p_query)
        {
            return p_query.GroupBy(p => new { p.id_funzione, p.id_tipo_doc_entrate }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<join_causale_doc_new> GroupByIdTipoDocEntrate(this IQueryable<join_causale_doc_new> p_query)
        {
            return p_query.GroupBy(p => new { p.id_tipo_doc_entrate }).Select(g => g.FirstOrDefault());
        }

        public static IQueryable<join_causale_doc_new> WhereByIdFunzione(this IQueryable<join_causale_doc_new> p_query, int p_idFunzione)
        {
            return p_query.Where(d => d.id_funzione == p_idFunzione);
        }

        public static IQueryable<join_causale_doc_new> WhereByIdEntrata(this IQueryable<join_causale_doc_new> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.tab_tipo_doc_entrate.id_entrata == p_idEntrata);
        }

        public static IQueryable<join_causale_doc_new> WhereByIdTipoDocEntrata(this IQueryable<join_causale_doc_new> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrata);
        }

        public static IQueryable<join_causale_doc_new> WhereIdTipoDocEntrateIsNotnull(this IQueryable<join_causale_doc_new> query)
        {
            return query.Where(d => d.id_tipo_doc_entrate != null);
        }

        public static IQueryable<join_causale_doc_new> WhereByFlagTipoOperazione(this IQueryable<join_causale_doc_new> p_query, string p_TipoOperazione)
        {
            return p_query.Where(d => d.flag_tipo_operazione == p_TipoOperazione);
        }

        public static IQueryable<join_causale_doc_new> WhereByIdEntrateList(this IQueryable<join_causale_doc_new> p_query, IList<int> p_idEntrateList)
        {
            return p_query.Where(d => p_idEntrateList.Contains(d.tab_tipo_doc_entrate.id_entrata)
                                      /*|| d.tab_tipo_doc_entrate.id_entrata == anagrafica_entrate.NESSUNA_ENTRATA*/);
        }

        public static IQueryable<join_causale_doc_new> OrderByDefault(this IQueryable<join_causale_doc_new> p_query)
        {
            return p_query.OrderBy(e => e.tab_tipo_doc_entrate.descr_doc);
        }
    }
}
