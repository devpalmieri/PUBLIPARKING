using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFascePignorabilitaBeniLinq
    {
        public static IQueryable<tab_fasce_pignorabilita_beni> WhereByIdTipoBene(this IQueryable<tab_fasce_pignorabilita_beni> p_query, int p_idTipoBene)
        {
            return p_query.Where(d => d.id_tipo_bene == p_idTipoBene);
        }

        public static IQueryable<tab_fasce_pignorabilita_beni> WhereByIdTipoDocEntrate(this IQueryable<tab_fasce_pignorabilita_beni> p_query, int p_idTipoDocEntrate)
        {
            return p_query.Where(d => d.id_tipo_doc_entrate == p_idTipoDocEntrate);
        }

        public static IQueryable<tab_fasce_pignorabilita_beni> WhereByRangeValidita(this IQueryable<tab_fasce_pignorabilita_beni> p_query, DateTime p_data)
        {
            return p_query.Where(d => d.data_inizio_validita <= p_data && 
                                    ((d.data_fine_validita.HasValue && d.data_fine_validita.Value >= p_data) || !d.data_fine_validita.HasValue));
        }

        public static IQueryable<tab_fasce_pignorabilita_beni> WhereByRangeImporto(this IQueryable<tab_fasce_pignorabilita_beni> p_query, decimal p_importo)
        {
            return p_query.Where(d => d.importo_da <= p_importo &&
                                    ((d.importo_a.HasValue && d.importo_a.Value >= p_importo) || !d.importo_a.HasValue));
        }
    }
}
