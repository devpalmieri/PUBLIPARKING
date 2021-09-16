using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinContribuentiProcedureConcorsualiLinq
    {
        public static IQueryable<join_contribuenti_procedure_concorsuali> WhereByIdProceduraConcorsuale(this IQueryable<join_contribuenti_procedure_concorsuali> p_query, int p_idProceduraConcorsuale)
        {
            return p_query.Where(d => d.id_procedura_concorsuale == p_idProceduraConcorsuale);
        }

        public static IQueryable<join_contribuenti_procedure_concorsuali> WhereByIdDocInout(this IQueryable<join_contribuenti_procedure_concorsuali> p_query, int p_idDocInput)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_idDocInput);
        }

        public static IQueryable<join_contribuenti_procedure_concorsuali> WhereByIdContribuente(this IQueryable<join_contribuenti_procedure_concorsuali> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<join_contribuenti_procedure_concorsuali> WhereByIdAvviso(this IQueryable<join_contribuenti_procedure_concorsuali> p_query, int p_idAvviso)
        {
            return p_query.Where(d => d.id_avv_pag_domanda_ammissione == p_idAvviso);
        }

        public static IQueryable<join_contribuenti_procedure_concorsuali> WhereByCodStato(this IQueryable<join_contribuenti_procedure_concorsuali> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }
    }
}
