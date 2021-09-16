using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabContatoreLinq
    {
        public static IQueryable<tab_contatore> WhereAttivo(this IQueryable<tab_contatore> p_query)
        {
            return p_query.Where(d => d.cod_stato == tab_contatore.ATT_ATT);
        }

        public static IQueryable<tab_contatore> OrderByDefault(this IQueryable<tab_contatore> p_query)
        {
            return p_query.OrderBy(d => d.data_istallazione_presa_incarico);
        }

        public static IQueryable<tab_contatore_light> ToLight(this IQueryable<tab_contatore> iniziale)
        {
            return iniziale.Select(l => new tab_contatore_light
            {
                cod_stato = l.cod_stato,
                data_stato = l.data_stato,
                id_contatore = l.id_contatore,
                matricola = l.matricola
            });
        }
    }
}
