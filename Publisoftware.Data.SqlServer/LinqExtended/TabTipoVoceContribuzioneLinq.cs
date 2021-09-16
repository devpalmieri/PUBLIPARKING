using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTipoVoceContribuzioneLinq
    {
        public static IQueryable<tab_tipo_voce_contribuzione> OrderByDefault(this IQueryable<tab_tipo_voce_contribuzione> p_query)
        {
            return p_query.OrderBy(d => d.descrizione_tipo_voce_contribuzione);
        }

        public static IQueryable<tab_tipo_voce_contribuzione> WhereByIdEntrata(this IQueryable<tab_tipo_voce_contribuzione> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_tipo_voce_contribuzione> WhereByFlagAccertamento(this IQueryable<tab_tipo_voce_contribuzione> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_accertamento_contabile == p_flag);
        }
        
        public static IQueryable<tab_tipo_voce_contribuzione> WhereByCodiceTributoMinisteriale(this IQueryable<tab_tipo_voce_contribuzione> p_query, string p_codiceTributoMinisteriale)
        {
            return p_query.Where(d => d.codice_tributo_ministeriale.Trim() == p_codiceTributoMinisteriale.Trim());
        }
    }
}
