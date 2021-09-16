using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoOggettoLinq
    {
        public static IQueryable<tab_tipo_oggetto> WhereByIdEntrata(this IQueryable<tab_tipo_oggetto> p_query, int p_idEntrata)
        {
            return p_query.Where(e => e.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_tipo_oggetto> WhereByIdTipoOggetto(this IQueryable<tab_tipo_oggetto> p_query, int p_idTipo)
        {
            return p_query.Where(e => e.id_tipo_oggetto == p_idTipo);
        }
    }
}