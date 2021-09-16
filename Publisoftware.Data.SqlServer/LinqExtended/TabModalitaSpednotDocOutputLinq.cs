using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabModalitaSpednotDocOutputLinq
    {
        public static IQueryable<tab_modalita_spednot_doc_output> WhereByIdEnte(this IQueryable<tab_modalita_spednot_doc_output> p_query, int? p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_modalita_spednot_doc_output> WhereByIdEnteNull(this IQueryable<tab_modalita_spednot_doc_output> p_query)
        {
            return p_query.Where(d => !d.id_ente.HasValue);
        }

        public static IQueryable<tab_modalita_spednot_doc_output> WhereByIdTipoDocEntrata(this IQueryable<tab_modalita_spednot_doc_output> p_query, int? p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrate);
        }

        public static IQueryable<tab_modalita_spednot_doc_output> WhereByFlagEstero(this IQueryable<tab_modalita_spednot_doc_output> p_query, string p_flagEstero)
        {
            return p_query.Where(d => d.flag_estero == p_flagEstero);
        }

        public static IQueryable<tab_modalita_spednot_doc_output> WhereByRangeValidita(this IQueryable<tab_modalita_spednot_doc_output> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_inizio_validita <= p_data &&
                                    ((d.data_fine_validita.HasValue && d.data_fine_validita.Value >= p_data) || !d.data_fine_validita.HasValue));
        }
    }
}
