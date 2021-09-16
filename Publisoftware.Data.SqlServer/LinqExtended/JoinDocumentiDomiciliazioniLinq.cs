using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiDomiciliazioniLinq
    {
        public static IQueryable<join_documenti_domiciliazioni> WhereByMacrocategorie(this IQueryable<join_documenti_domiciliazioni> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_domiciliazioni> WhereByIdDocumento(this IQueryable<join_documenti_domiciliazioni> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_domiciliazioni> WhereByIdDomicilio(this IQueryable<join_documenti_domiciliazioni> p_query, int p_idDomicilio)
        {
            return p_query.Where(d => d.id_tab_domiciliazione == p_idDomicilio);
        }
    }
}
