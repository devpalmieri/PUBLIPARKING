using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiFascicoloLinq
    {
        public static IQueryable<join_documenti_fascicolo> WhereByMacrocategorie(this IQueryable<join_documenti_fascicolo> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_fascicolo> WhereBySigle(this IQueryable<join_documenti_fascicolo> p_query, List<string> sigle)
        {
            return p_query.Where(d => sigle.Contains(d.tab_documenti.anagrafica_documenti.sigla_doc));
        }

        public static IQueryable<join_documenti_fascicolo> WhereBySiglaDoc(this IQueryable<join_documenti_fascicolo> p_query, string p_siglaDoc)
        {
            return p_query.Where(d => d.tab_documenti.anagrafica_documenti.sigla_doc == p_siglaDoc);
        }

        public static IQueryable<join_documenti_fascicolo> WhereByIdDocumento(this IQueryable<join_documenti_fascicolo> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_fascicolo> WhereByIdFascicolo(this IQueryable<join_documenti_fascicolo> p_query, int p_idFascicolo)
        {
            return p_query.Where(d => d.id_fascicolo == p_idFascicolo);
        }

        public static IList<join_documenti_fascicolo_light> ToLight(this IQueryable<join_documenti_fascicolo> iniziale)
        {
            return iniziale.ToList().Select(d => new join_documenti_fascicolo_light
            {
                id_join_documenti_fascicolo = d.id_join_documenti_fascicolo,
                id_tab_documenti = d.tab_documenti.id_tab_documenti,
                tipo_documento = d.tab_documenti.anagrafica_documenti.descrizione_doc,
                identificativo_avv_pag = d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag != null ? d.tab_fascicolo.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_avv_pag_rif = d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag != null ? d.tab_fascicolo.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_doc_input = d.tab_fascicolo.tab_doc_input.identificativo_doc_input != null ? d.tab_fascicolo.tab_doc_input.tab_tipo_doc_entrate.descr_doc + " " + d.tab_fascicolo.tab_doc_input.identificativo_doc_input : string.Empty
            }).ToList();
        }
    }
}
