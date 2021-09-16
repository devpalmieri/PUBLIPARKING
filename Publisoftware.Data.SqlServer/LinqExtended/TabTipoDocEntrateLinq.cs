using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoDocEntrateLinq
    {
        public static IQueryable<tab_tipo_doc_entrate> WhereByIdTipoDocEntrate(this IQueryable<tab_tipo_doc_entrate> p_query, int p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrate);
        }

        public static IQueryable<tab_tipo_doc_entrate> WhereByPcnCitRic(this IQueryable<tab_tipo_doc_entrate> p_query)
        {
            return p_query.Where(d => d.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_CITAZIONI ||
                                      d.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_PROCEDURA_CONCORSUALE ||
                                      d.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI);
        }

        public static IQueryable<tab_tipo_doc_entrate> WhereByFlagPraticheCollegate(this IQueryable<tab_tipo_doc_entrate> p_query)
        {
            return p_query.Where(d => d.flag_pratiche_collegate == "1");
        }

        public static IQueryable<tab_tipo_doc_entrate> WhereByIdTipoDoc(this IQueryable<tab_tipo_doc_entrate> p_query, int p_idTipoDoc)
        {
            return p_query.Where(d => d.id_tipo_doc == p_idTipoDoc);
        }

        public static IQueryable<tab_tipo_doc_entrate> WhereByIdTipoDocList(this IQueryable<tab_tipo_doc_entrate> p_query, List<int> p_idTipoDocList)
        {
            return p_query.Where(d => p_idTipoDocList.Contains(d.id_tipo_doc));
        }

        public static IQueryable<tab_tipo_doc_entrate> OrderByIdTipoDoc(this IQueryable<tab_tipo_doc_entrate> p_query)
        {
            return p_query.OrderBy(d => d.id_tipo_doc);
        }

        public static IQueryable<tab_tipo_doc_entrate> OrderByDefault(this IQueryable<tab_tipo_doc_entrate> p_query)
        {
            return p_query.OrderBy(d => d.descr_doc);
        }

        public static IQueryable<tab_tipo_doc_entrate> OrderByIdTipoDocDescrizione(this IQueryable<tab_tipo_doc_entrate> p_query)
        {
            return p_query.OrderBy(d => d.id_tipo_doc).ThenBy(d => d.descr_doc);
        }
    }
}
