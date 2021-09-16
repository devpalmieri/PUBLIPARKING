using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiAutorizzazioneLinq
    {
        public static IQueryable<join_documenti_autorizzazione> WhereByMacrocategorie(this IQueryable<join_documenti_autorizzazione> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_autorizzazione> WhereBySigle(this IQueryable<join_documenti_autorizzazione> p_query, List<string> sigle)
        {
            return p_query.Where(d => sigle.Contains(d.tab_documenti.anagrafica_documenti.sigla_doc));
        }

        public static IQueryable<join_documenti_autorizzazione> WhereByIdDocumento(this IQueryable<join_documenti_autorizzazione> p_query, decimal p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_autorizzazione> WhereByIdAutorizzazione(this IQueryable<join_documenti_autorizzazione> p_query, decimal p_idAutorizzazione)
        {
            return p_query.Where(d => d.id_autorizzazione == p_idAutorizzazione);
        }
    }
}
