using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabVincoliBeniPignoratiLinq
    {
        public static IQueryable<tab_vincoli_beni_pignorati> WhereByIdJoinAvvPagDocInput(this IQueryable<tab_vincoli_beni_pignorati> p_query, int p_idJoinAvvPagDocInput)
        {
            return p_query.Where(d => d.id_join_avv_pag_doc_input == p_idJoinAvvPagDocInput);
        }

        public static IList<tab_vincoli_beni_pignorati_light> ToLight(this IQueryable<tab_vincoli_beni_pignorati> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_vincoli_beni_pignorati_light
            {
                id_tab_vincoli_beni = d.id_tab_vincoli_beni,
                descrizione = d.descr_vincolo,
                DataAccensione = d.data_accensione_vincolo_String,
                nominativoRagSoc = d.nominativo_rag_sociale_beneficiario,
                codicefiscPIva = d.cod_fiscale_piva_beneficiario,
                DataScadenza = d.data_scadenza_vincolo_String,
                importo_vincolato_Euro = d.importo_vincolato.HasValue ? d.importo_vincolato.Value.ToString("C") : 0.ToString("C"),
                note = d.note
            }).ToList();
        }
    }
}
