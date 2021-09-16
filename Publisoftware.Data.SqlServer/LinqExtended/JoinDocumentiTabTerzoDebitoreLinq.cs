using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiTabTerzoDebitoreLinq
    {
        public static IQueryable<join_documenti_tab_terzo_debitore> WhereByMacrocategorie(this IQueryable<join_documenti_tab_terzo_debitore> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_tab_terzo_debitore> WhereByIdDocumento(this IQueryable<join_documenti_tab_terzo_debitore> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_tab_terzo_debitore> WhereByIdTabTerzoDebitore(this IQueryable<join_documenti_tab_terzo_debitore> p_query, int p_idTabTerzoDebitore)
        {
            return p_query.Where(d => d.id_trz_debitore == p_idTabTerzoDebitore);
        }
    }
}
