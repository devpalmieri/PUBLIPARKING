using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinFattContFattConsumiLinq
    {
        public static IQueryable<join_fattcont_fattconsumi> WhereByIdJoinFattContFattCons(this IQueryable<join_fattcont_fattconsumi> p_query, int p_idJoinFattContFattCons)
        {
            return p_query.Where(d => d.id_join_fattcont_fattconsumi == p_idJoinFattContFattCons);
        }

        public static IQueryable<join_fattcont_fattconsumi> WhereByIdFattCont(this IQueryable<join_fattcont_fattconsumi> p_query, int p_idFattCont)
        {
            return p_query.Where(d => d.id_tab_fatt_cont == p_idFattCont);
        }

        public static IQueryable<join_fattcont_fattconsumi> WhereByIdFattConsumi(this IQueryable<join_fattcont_fattconsumi> p_query, int p_idFattConsumi)
        {
            return p_query.Where(d => d.id_tab_fatt_consumi == p_idFattConsumi);
        }

        public static IQueryable<join_fattcont_fattconsumi> WhereByIdFattConsumi(this IQueryable<join_fattcont_fattconsumi> p_query, List<int> p_idFattConsumiList)
        {
            return p_query.Where(d => p_idFattConsumiList.Contains(d.id_tab_fatt_consumi.Value));
        }

        public static IList<join_fattcont_fattconsumi_light> ToLight(this IQueryable<join_fattcont_fattconsumi> iniziale)
        {
            return iniziale.ToList().Select(d => new join_fattcont_fattconsumi_light
            {
                id_join_fattcont_fattconsumi = d.id_join_fattcont_fattconsumi,
                matricola_contatore = d.tab_fatt_cont.matricola_contatore,
                id_lettura_1 = d.tab_fatt_cont.id_lettura_1.HasValue ? d.tab_fatt_cont.id_lettura_1.Value : 0,
                data_lettura_1_String = d.tab_fatt_cont.data_lettura_1_String,
                qta_lettura_1 = d.tab_fatt_cont.qta_lettura_1.HasValue ? d.tab_fatt_cont.qta_lettura_1.Value : 0,
                id_lettura_2 = d.tab_fatt_cont.id_lettura_2.HasValue ? d.tab_fatt_cont.id_lettura_2.Value : 0,
                data_lettura_2_String = d.tab_fatt_cont.data_lettura_2_String,
                qta_lettura_2 = d.tab_fatt_cont.qta_lettura_2.HasValue ? d.tab_fatt_cont.qta_lettura_2.Value : 0,
                //letturaRiferimento1 = "Data: " + d.tab_fatt_cont.data_lettura_1_String + " Consumo: " + (d.tab_fatt_cont.qta_lettura_1.HasValue ? d.tab_fatt_cont.qta_lettura_1.Value : 0) + " mc",
                //letturaRiferimento2 = "Data: " + d.tab_fatt_cont.data_lettura_2_String + " Consumo: " + (d.tab_fatt_cont.qta_lettura_2.HasValue ? d.tab_fatt_cont.qta_lettura_2.Value : 0) + " mc",
                num_giorni_lettura = d.tab_fatt_cont.num_giorni_lettura.HasValue ? d.tab_fatt_cont.num_giorni_lettura.Value : 0,
                qta_cons_lettura = d.tab_fatt_cont.num_giorni_lettura.HasValue ? d.tab_fatt_cont.qta_cons_lettura.Value : 0,
                qta_prodie_lettura = d.tab_fatt_cont.num_giorni_lettura.HasValue ? d.tab_fatt_cont.qta_prodie_lettura.Value : 0
            }).ToList();
        }
    }
}
