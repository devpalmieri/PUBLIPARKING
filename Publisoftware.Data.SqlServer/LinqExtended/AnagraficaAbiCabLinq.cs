using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaAbiCabLinq
    {
        public static ANAGRAFICA_ABI_CAB SingleOrDefaultAbiCab(this IQueryable<ANAGRAFICA_ABI_CAB> p_query, string p_abi, string p_cab)
        {
            return p_query.SingleOrDefault(d => d.ABI == p_abi && d.CAB == p_cab);
        }

        public static IQueryable<ANAGRAFICA_ABI_CAB> WhereByABI(this IQueryable<ANAGRAFICA_ABI_CAB> p_query, string p_abi)
        {
            return p_query.Where(d => d.ABI == p_abi);
        }

        public static IQueryable<ANAGRAFICA_ABI_CAB> WhereByCAB(this IQueryable<ANAGRAFICA_ABI_CAB> p_query, string p_cab)
        {
            return p_query.Where(d => d.CAB == p_cab);
        }

        public static IQueryable<ANAGRAFICA_ABI_CAB> OrderByDefault(this IQueryable<ANAGRAFICA_ABI_CAB> p_query)
        {
            return p_query.OrderBy(o => o.BANCA);
        }

        public static IQueryable<ANAGRAFICA_ABI_CAB> OrderByBancaAgenzia(this IQueryable<ANAGRAFICA_ABI_CAB> p_query)
        {
            return p_query.OrderBy(o => o.BANCA).ThenBy(o => o.AGENZIA);
        }

        public static IQueryable<ANAGRAFICA_ABI_CAB_light> ToLight(this IQueryable<ANAGRAFICA_ABI_CAB> iniziale)
        {
            return iniziale.Select(d => new ANAGRAFICA_ABI_CAB_light
            {
                ID_ABI_CAB = d.ID_ABI_CAB,
                ABI = d.ABI,
                CAB = d.CAB,
                BANCA = d.BANCA,
                AGENZIA = d.AGENZIA
            });
        }
    }
}
