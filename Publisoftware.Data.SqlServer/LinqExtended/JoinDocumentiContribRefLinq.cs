using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiContribRefLinq
    {
        public static IQueryable<join_documenti_contrib_ref> WhereByMacrocategorie(this IQueryable<join_documenti_contrib_ref> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_contrib_ref> WhereByIdDocumento(this IQueryable<join_documenti_contrib_ref> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_contrib_ref> WhereByIdContribRef(this IQueryable<join_documenti_contrib_ref> p_query, int p_idJoinContribRef)
        {
            return p_query.Where(d => d.id_join_contribuente_referente == p_idJoinContribRef);
        }
    }
}
