using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaStatoAvvPagLinq
    {
        public static anagrafica_stato_avv_pag WhereByCodice(this IQueryable<anagrafica_stato_avv_pag> p_query, string p_codStato)
        {
            return p_query.SingleOrDefault(d => d.cod_stato.ToUpper().Equals(p_codStato.ToUpper()));
        }
    }
}
