using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaStatoContribuenteLinq
    {
        //public static IQueryable<anagrafica_stato_contribuente> WhereByIdTipoContribuente(this IQueryable<anagrafica_stato_contribuente> p_query, String p_idTipoContribuente)
        //{
        //    return p_query.Where(d => d.flag_fisica_giuridica == p_idTipoContribuente || d.flag_fisica_giuridica == "0");
        //}

        public static IQueryable<anagrafica_stato_contribuente> OrderByDefault(this IQueryable<anagrafica_stato_contribuente> p_query)
        {
            return p_query.OrderBy(o => o.statoContribuenteTotale);
        }
    }
}
