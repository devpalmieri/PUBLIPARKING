using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabDettRiscossioneLinq
    {
        public static IQueryable<tab_dett_riscossione> WhereByIdAvvPag(this IQueryable<tab_dett_riscossione> p_query, Int32 p_id)
        {
            return p_query.Where(c => c.id_avv_pag.HasValue && c.id_avv_pag.Value == p_id);
        }

        public static IQueryable<tab_dett_riscossione> WhereByIdVoceContribuzione(this IQueryable<tab_dett_riscossione> p_query, Int32 p_id)
        {
            return p_query.Where(c => c.id_tab_contribuzione.HasValue && c.id_tab_contribuzione.Value == p_id);
        }

        public static IQueryable<tab_dett_riscossione> WhereByIdMovPag(this IQueryable<tab_dett_riscossione> p_query, int p_id)
        {
            return p_query.Where(d => d.id_mov_pag == p_id);
        }

        public static IQueryable<tab_dett_riscossione> WhereByIdAvvPagNull(this IQueryable<tab_dett_riscossione> p_query)
        {
            return p_query.Where(d => !d.id_avv_pag.HasValue || (d.id_avv_pag.HasValue && d.id_avv_pag < 1));
        }

        public static IQueryable<tab_dett_riscossione> WhereByCodStato(this IQueryable<tab_dett_riscossione> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_dett_riscossione> WhereByCodStatoNot(this IQueryable<tab_dett_riscossione> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.Contains(p_codStato));
        }

        public static IQueryable<tab_dett_riscossione> WhereByImportoRiscossoMaggioreUguale(this IQueryable<tab_dett_riscossione> p_query, decimal p_importo)
        {
            return p_query.Where(d => d.importo_totale_riscosso >= p_importo);
        }

        public static IQueryable<tab_dett_riscossione> WhereByImportoRiscossoUguale(this IQueryable<tab_dett_riscossione> p_query, decimal p_importo)
        {
            return p_query.Where(d => d.importo_totale_riscosso == p_importo);
        }
    }
}
