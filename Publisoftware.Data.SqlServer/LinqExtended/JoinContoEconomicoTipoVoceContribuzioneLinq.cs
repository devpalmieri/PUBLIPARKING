using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinContoEconomicoTipoVoceContribuzioneLinq
    {
        public static IQueryable<join_conto_economico_tipo_voce_contribuzione> WhereByIdTipoVoceContribuzione(this IQueryable<join_conto_economico_tipo_voce_contribuzione> p_query, int p_idTipoVoceContribuzione)
        {
            return p_query.Where(d => d.id_tipo_voce_contribuzione == p_idTipoVoceContribuzione);
        }

        public static IQueryable<join_conto_economico_tipo_voce_contribuzione> WhereByCodStato(this IQueryable<join_conto_economico_tipo_voce_contribuzione> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_piano_conti_economici.cod_stato_conto_bilancio == p_codStato);
        }

        public static IQueryable<join_conto_economico_tipo_voce_contribuzione> WhereByRangeValiditaOdierno(this IQueryable<join_conto_economico_tipo_voce_contribuzione> p_query)
        {
            return p_query.Where(d => (d.tab_piano_conti_economici.data_inizio_validita.HasValue ? d.tab_piano_conti_economici.data_inizio_validita.Value : DateTime.MinValue) <= DateTime.Now
                                   && (d.tab_piano_conti_economici.data_fine_validita.HasValue ? d.tab_piano_conti_economici.data_fine_validita.Value : DateTime.MaxValue) >= DateTime.Now);
        }
    }
}