using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiContribuentiLinq
    {
        public static IQueryable<join_documenti_contribuenti> WhereByMacrocategorie(this IQueryable<join_documenti_contribuenti> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_contribuenti> WhereByIdContribuenteSto(this IQueryable<join_documenti_contribuenti> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente_sto == p_idContribuente);
        }

        public static IQueryable<join_documenti_contribuenti> WhereByIdDocumento(this IQueryable<join_documenti_contribuenti> p_query, decimal p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_contribuenti> WhereByIdContribuenteStoList(this IQueryable<join_documenti_contribuenti> p_query, List<decimal> p_idContribuenteStoList)
        {
            return p_query.Where(w => p_idContribuenteStoList.Contains(w.id_contribuente_sto.Value));
        }
    }
}
