using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiPraticheLinq
    {
        public static IQueryable<join_documenti_pratiche> WhereByMacrocategorie(this IQueryable<join_documenti_pratiche> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_pratiche> WhereBySigle(this IQueryable<join_documenti_pratiche> p_query, List<string> sigle)
        {
            return p_query.Where(d => sigle.Contains(d.tab_documenti.anagrafica_documenti.sigla_doc));
        }

        public static IQueryable<join_documenti_pratiche> WhereByIdJoinAvvPagDocInput(this IQueryable<join_documenti_pratiche> p_query, int p_idJoinAvvPagDocInput)
        {
            return p_query.Where(d => d.id_join_avv_pag_doc_input == p_idJoinAvvPagDocInput);
        }

        public static IQueryable<join_documenti_pratiche> WhereByIdDocInput(this IQueryable<join_documenti_pratiche> p_query, int p_idDocInput)
        {
            return p_query.Where(d => d.id_doc_input == p_idDocInput);
        }

        public static IQueryable<join_documenti_pratiche> WhereByIdJoinDenunceOggetti(this IQueryable<join_documenti_pratiche> p_query, int p_idjoinDenunceOggetti)
        {
            return p_query.Where(d => d.id_join_denunce_oggetti == p_idjoinDenunceOggetti);
        }

        public static IQueryable<join_documenti_pratiche> WhereByIdDenunceContratti(this IQueryable<join_documenti_pratiche> p_query, int p_idDenunceContratti)
        {
            return p_query.Where(d => d.id_denunce_contratti == p_idDenunceContratti);
        }

        public static IQueryable<join_documenti_pratiche> WhereByIdDocumento(this IQueryable<join_documenti_pratiche> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }
    }
}
