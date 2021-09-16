using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{    
    public static class JoinDocumentiReferentiLinq
    {
        public static IQueryable<join_documenti_referenti> WhereByMacrocategorie(this IQueryable<join_documenti_referenti> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_referenti> WhereByIdDocumento(this IQueryable<join_documenti_referenti> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_referenti> WhereByIdReferenteSto(this IQueryable<join_documenti_referenti> p_query, int p_idReferente)
        {
            return p_query.Where(d => d.id_referente_sto == p_idReferente);
        }

        public static IQueryable<join_documenti_referenti> WhereByIdReferenteStoList(this IQueryable<join_documenti_referenti> p_query, List<int> p_idReferenteStoList)
        {
            return p_query.Where(w => p_idReferenteStoList.Contains(w.id_referente_sto.Value));
        }
    }
}
