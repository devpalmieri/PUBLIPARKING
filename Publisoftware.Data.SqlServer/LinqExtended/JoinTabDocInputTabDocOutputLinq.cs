using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTabDocInputTabDocOutputLinq
    {
        public static IQueryable<join_tab_doc_input_tab_doc_output> WhereByIdTabDocInput(this IQueryable<join_tab_doc_input_tab_doc_output> p_query, int p_idTabDocInput)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_idTabDocInput);
        }

        public static IQueryable<join_tab_doc_input_tab_doc_output> WhereByIdTabDocOutput(this IQueryable<join_tab_doc_input_tab_doc_output> p_query, int p_idTabDocOutput)
        {
            return p_query.Where(d => d.id_tab_doc_output == p_idTabDocOutput);
        }
    }
}
