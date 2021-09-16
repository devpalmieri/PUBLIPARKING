using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabConsuntivoListaCaricoLinq
    {
        public static IQueryable<tab_consuntivo_lista_carico> WhereByCodStatoAvv(this IQueryable<tab_consuntivo_lista_carico> p_query, string p_cod_stato)
        {
            return p_query.Where(c => c.cod_stato_avv_pag.ToUpper().Contains(p_cod_stato.ToUpper()));
        }

        public static IQueryable<tab_consuntivo_lista_carico> WhereNOTbyCodStatoAvv(this IQueryable<tab_consuntivo_lista_carico> p_query, string p_cod_stato)
        {
            return p_query.Where(c => !c.cod_stato_avv_pag.ToUpper().Contains(p_cod_stato.ToUpper()));
        }
    }
}
