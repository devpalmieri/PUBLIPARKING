using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabProcedureConcorsualiLinq
    {
        public static IQueryable<tab_procedure_concorsuali> WhereByCFPIVA(this IQueryable<tab_procedure_concorsuali> p_query, string p_cfPiva)
        {
            return p_query.Where(d => d.cf_piva.Equals(p_cfPiva));
        }

        public static IQueryable<tab_procedure_concorsuali> WhereByCodStato(this IQueryable<tab_procedure_concorsuali> p_query, string p_codstato)
        {
            return p_query.Where(d => d.cod_stato == p_codstato);
        }

        public static IQueryable<tab_procedure_concorsuali> WhereByCodStato(this IQueryable<tab_procedure_concorsuali> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_procedure_concorsuali> WhereByRangeDataAperturaProcedura(this IQueryable<tab_procedure_concorsuali> p_query, DateTime? p_da, DateTime? p_a)
        {
            return p_query.Where(d => !d.data_avviso_apertura_procedura.HasValue ||
                                      (d.data_avviso_apertura_procedura <= p_a &&
                                       d.data_avviso_apertura_procedura >= p_da));
        }
    }
}