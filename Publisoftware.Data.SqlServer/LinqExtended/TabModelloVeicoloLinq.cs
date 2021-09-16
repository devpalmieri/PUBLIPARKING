using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabModelloVeicoloLinq
    {
        public static IQueryable<tab_modello_veicolo> OrderByDefault(this IQueryable<tab_modello_veicolo> p_query)
        {
            return p_query.OrderBy(d => d.tab_marca_veicolo.descr_marca).ThenBy(d => d.anagrafica_tipo_veicolo.descrizione).ThenBy(d => d.descr_modello).ThenBy(d => d.descr_serie);
        }
    }
}
