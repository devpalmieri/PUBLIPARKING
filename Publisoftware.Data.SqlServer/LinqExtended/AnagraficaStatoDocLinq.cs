using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaStatoDocLinq
    {
        public static anagrafica_stato_doc WhereByCodice(this IQueryable<anagrafica_stato_doc> p_query, string p_codStato)
        {
            return p_query.SingleOrDefault(d => d.cod_stato.ToUpper().Equals(p_codStato.ToUpper()));
        }

        public static IQueryable<anagrafica_stato_doc> WhereByCodiceList(this IQueryable<anagrafica_stato_doc> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }
    }
}
