using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinControlliAvvpagFattEmissioneLinq
    {
        public static IQueryable<join_controlli_avvpag_fatt_emissione> WhereByCodStato(this IQueryable<join_controlli_avvpag_fatt_emissione> p_query, string p_codStato)
        {
            return p_query.Where(j => j.cod_stato.StartsWith(p_codStato));
        }

    }
}
