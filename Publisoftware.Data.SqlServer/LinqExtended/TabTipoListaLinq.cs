using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoListaLinq
    {
        public static IQueryable<tab_tipo_lista> OrderByDefault(this IQueryable<tab_tipo_lista> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_entrate.descrizione_entrata).ThenBy(d => d.desc_lista);
        }

        public static IQueryable<tab_tipo_lista> WhereByIdEntrata(this IQueryable<tab_tipo_lista> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata.Value == p_idEntrata);
        }

        public static IQueryable<tab_tipo_lista> WhereByCodice(this IQueryable<tab_tipo_lista> p_query, string p_codice)
        {
            return p_query.Where(d => d.cod_lista.Equals(p_codice));
        }
    }
}
