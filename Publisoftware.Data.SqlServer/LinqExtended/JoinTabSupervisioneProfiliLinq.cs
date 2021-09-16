using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTabSupervisioneProfiliLinq
    {
        public static IQueryable<join_tab_supervisione_profili> WhereByIdSupervisione(this IQueryable<join_tab_supervisione_profili> p_query, int p_idSupervisione)
        {
            return p_query.Where(d => d.id_tab_supervisione_finale == p_idSupervisione);
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByIdJoinSupervisioneProfilo(this IQueryable<join_tab_supervisione_profili> p_query, int p_idJoinSupervisioneProfilo)
        {
            return p_query.Where(d => d.id_join_tab_supervisione_profili == p_idJoinSupervisioneProfilo);
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByStato(this IQueryable<join_tab_supervisione_profili> p_query, List<string> p_statoList)
        {
            return p_query.Where(d => p_statoList.Any(x => d.cod_stato.Contains(x)));
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByStatoSupervisione(this IQueryable<join_tab_supervisione_profili> p_query, List<string> p_statoList)
        {
            return p_query.Where(d => p_statoList.Any(x => d.TAB_SUPERVISIONE_FINALE_V2.COD_STATO.Contains(x)));
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByStatoProfilo(this IQueryable<join_tab_supervisione_profili> p_query, List<string> p_statoList)
        {
            return p_query.Where(d => p_statoList.Any(x => d.tab_profilo_contribuente_new.cod_stato.Contains(x)));
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByStato(this IQueryable<join_tab_supervisione_profili> p_query, string p_stato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_stato));
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByStatoSupervisione(this IQueryable<join_tab_supervisione_profili> p_query, string p_stato)
        {
            return p_query.Where(d => d.TAB_SUPERVISIONE_FINALE_V2.COD_STATO.Contains(p_stato));
        }

        public static IQueryable<join_tab_supervisione_profili> WhereByStatoProfilo(this IQueryable<join_tab_supervisione_profili> p_query, string p_stato)
        {
            return p_query.Where(d => d.tab_profilo_contribuente_new.cod_stato.Contains(p_stato));
        }

        public static IQueryable<join_tab_supervisione_profili_light> ToLight(this IQueryable<join_tab_supervisione_profili> iniziale)
        {
            return iniziale.ToList().Select(d => new join_tab_supervisione_profili_light
            {
                id_join_tab_supervisione_profili = d.id_join_tab_supervisione_profili,
                descrizione = d.tab_profilo_contribuente_new.DescrizioneBeneEstesa,
                protocollo_registro = d.num_protocollo_registro_iscrizione_bene,
                data_registro_String = d.data_protocollo_registro_iscrizione_bene_String,
                id_tab_profilo_contribuente = d.tab_profilo_contribuente_new.id_tab_profilo_contribuente
            }).AsQueryable();
        }
    }
}
