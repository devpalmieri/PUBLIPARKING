using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaStatoContatoreLinq
    {
        public static IQueryable<anagrafica_stato_contatore_light> ToLight(this IQueryable<anagrafica_stato_contatore> iniziale)
        {
            return iniziale.Select(l => new anagrafica_stato_contatore_light { id_anagrafica_stato = l.id_anagrafica_stato, cod_stato = l.cod_stato, desc_stato = l.desc_stato });
        }
    }
}
