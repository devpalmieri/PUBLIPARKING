using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFattConsumiLinq
    {
        public static IQueryable<tab_fatt_consumi> WhereByIdFattConsumi(this IQueryable<tab_fatt_consumi> p_query, int p_idFattConsumi)
        {
            return p_query.Where(d => d.id_tab_fatt_consumi == p_idFattConsumi);
        }

        public static IQueryable<tab_fatt_consumi> WhereByIdFattConsumi(this IQueryable<tab_fatt_consumi> p_query, List<int> p_idFattConsumiList)
        {
            return p_query.Where(d => p_idFattConsumiList.Contains(d.id_tab_fatt_consumi));
        }

        public static IQueryable<tab_fatt_consumi> WhereByIdEntrata(this IQueryable<tab_fatt_consumi> p_query, int p_idEntrata)
        {
            return p_query.Where(d => d.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_fatt_consumi> OrderByPeriodoContribuzioneDa(this IQueryable<tab_fatt_consumi> p_query)
        {
            return p_query.OrderBy(d => d.periodo_contribuzione_da);
        }

        public static IList<tab_fatt_consumi_light> ToLight(this IQueryable<tab_fatt_consumi> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_fatt_consumi_light
            {
                id_tab_fatt_consumi = d.id_tab_fatt_consumi,
                periodo_contribuzione_da_String = d.periodo_contribuzione_da_String,
                periodo_contribuzione_a_String = d.periodo_contribuzione_a_String,
                CategoriaTariffaria = d.tab_oggetti_contribuzione.CategoriaTariffaria,
                num_giorni_contribuzione = d.num_giorni_contribuzione.HasValue ? d.num_giorni_contribuzione.Value : 0,
                qta_contribuzione = d.qta_contribuzione.HasValue ? d.qta_contribuzione.Value : 0,
                tipoAddebito = d.tipoAddebito
            }).ToList();
        }
    }
}
