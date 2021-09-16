using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabContribuzioneLinq
    {
        public static IQueryable<tab_contribuzione> WhereByIdAvviso(this IQueryable<tab_contribuzione> p_query, int p_idAvviso)
        {
            return p_query.Where(d => d.id_avv_pag == p_idAvviso);
        }

        public static IQueryable<tab_contribuzione> WhereByIdAvvisoList(this IQueryable<tab_contribuzione> p_query, List<int> p_idAvvisoList)
        {
            return p_query.Where(d => p_idAvvisoList.Contains(d.id_avv_pag));
        }

        public static IQueryable<tab_contribuzione> WhereByIdAvvisoUgualeIdAvvisoIniziale(this IQueryable<tab_contribuzione> p_query)
        {
            return p_query.Where(d => d.id_avv_pag == d.id_avv_pag_iniziale);
        }

        public static IQueryable<tab_contribuzione> WhereByCodiceTributoMinisteriale(this IQueryable<tab_contribuzione> p_query, string p_codice)
        {
            return p_query.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == p_codice);
        }
    }
}
