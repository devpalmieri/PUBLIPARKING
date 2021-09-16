using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRicercaInfocamereLinq
    {
        public static IQueryable<tab_ricerca_infocamere> WhereByIdContribuente(this IQueryable<tab_ricerca_infocamere> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }
    }
}