using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabProgDocLinq
    {
        [Obsolete("Il metodo non fa quello che dice - cambiare il nome in'WhereByIdTipoDocEntrateAndAnnoCorrente' o usare WhereByIdTipoDocEntrateSenzAltro", true)]
        public static IQueryable<tab_prog_doc> WhereByIdTipoDocEntrate(this IQueryable<tab_prog_doc> p_query, int p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrate && d.anno == DateTime.Now.Year);
        }

        public static IQueryable<tab_prog_doc> WhereByIdTipoDocEntrateSenzAltro(this IQueryable<tab_prog_doc> p_query, int p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrate);
        }

        public static IQueryable<tab_prog_doc> WhereByAnno(this IQueryable<tab_prog_doc> p_query, int p_anno)
        {
            return p_query.Where(d => d.anno == p_anno);
        }

        public static IQueryable<tab_prog_doc> OrderByProgressivo(this IQueryable<tab_prog_doc> p_query)
        {
            return p_query.OrderByDescending(e => e.prog_tipo_doc_entrata);
        }
    }
}
