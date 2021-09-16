using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaProcedureConcorsualiLinq
    {
        public static IQueryable<anagrafica_procedure_concorsuali> WhereByFlagPFPGAssoggettabili(this IQueryable<anagrafica_procedure_concorsuali> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_pf_pg_assoggettabili == p_flag ||
                                      d.flag_pf_pg_assoggettabili == "T");

        }
    }
}