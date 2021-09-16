using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabTerzoDebitoreBD : EntityBD<tab_terzo_debitore>
    {
        public TabTerzoDebitoreBD()
        {

        }

        public static void AggiornaStatoTerzoDebitoreByAvviso(tab_avv_pag p_avvPag, dbEnte p_dbContext)
        {
            List<tab_profilo_contribuente_new> v_list = new List<tab_profilo_contribuente_new>();

            if (p_avvPag.TAB_SUPERVISIONE_FINALE_V2 != null &&
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.Count > 0 &&
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili != null &&
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.Count > 0 &&
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new != null &&
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new.tab_terzo_debitore != null &&
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new.tab_terzo_debitore.tab_profilo_contribuente_new != null)
            {
                v_list = p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new.tab_terzo_debitore.tab_profilo_contribuente_new.Where(d => d.cod_stato.StartsWith(tab_profilo_contribuente_new.ATT)).ToList();
            }

            if (v_list.Count == 1)
            {
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new.tab_terzo_debitore.cod_stato = tab_terzo_debitore.ANN_ATT;
            }
            else if (v_list.Count > 1)
            {
                p_avvPag.TAB_SUPERVISIONE_FINALE_V2.FirstOrDefault().join_tab_supervisione_profili.FirstOrDefault().tab_profilo_contribuente_new.tab_terzo_debitore.cod_stato = tab_terzo_debitore.ATT_CES;
            }

            p_dbContext.SaveChanges();
        }

        //public static void AggiornaAdAnnullatoStatoTerzoDebitoreByAvviso(tab_avv_pag p_avvPag, dbEnte p_dbContext)
        //{
        //    foreach (join_tab_supervisione_profili v_join in TabSupervisioneFinaleV2BD.GetList(p_dbContext)
        //                                                                              .WhereByAvvisoEmesso(p_avvPag.id_tab_avv_pag)
        //                                                                              .join_tab_supervisione_profili)
        //    {
        //        v_join.tab_profilo_contribuente_new.tab_terzo_debitore.cod_stato = tab_terzo_debitore.ANN_ANN;
        //    }

        //    p_dbContext.SaveChanges();
        //}

        /// <summary>
        /// Filtro per codice_fiscale soggetto, numero di registrazione contratto locazione e tipo credito 3(locazione)
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_terzo_debitore> GetListTzLocazioni(string p_nrContratto, string p_cfPivaSoggetto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.nr_registrazione_contratto == p_nrContratto.ToString() && c.cf_piva_soggetto_ispezione == p_cfPivaSoggetto.ToString() && c.id_tipo_debitore == 3);
        }

        /// <summary>
        /// Filtro per numero contratto, codice fiscale locatore, codice fiscale locatario
        /// </summary>
        /// <param name="p_nrContratto"></param>
        /// <param name="p_cfPivaLocatore"></param>
        /// <param name="p_cfLocatario"></param>
        /// <param name="p_dtAggiornamento"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_terzo_debitore GetTzLocazioneCf(String p_nrContratto, String p_cfPivaLocatore, String p_cfLocatario, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.nr_registrazione_contratto == p_nrContratto && c.cf_piva_soggetto_ispezione == p_cfPivaLocatore && c.tab_terzo.cod_fiscale == p_cfLocatario && c.id_tipo_debitore == 3).FirstOrDefault();
        }

        /// <summary>
        /// Filtro per numero contratto, p. iva locatore, p. iva locatario
        /// </summary>
        /// <param name="p_nrContratto"></param>
        /// <param name="p_cfPivaLocatore"></param>
        /// <param name="p_pivaLocatario"></param>
        /// <param name="p_dtAggiornamento"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_terzo_debitore GetTzLocazionePiva(String p_nrContratto, String p_cfPivaLocatore, String p_pivaLocatario, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.nr_registrazione_contratto == p_nrContratto && c.cf_piva_soggetto_ispezione == p_cfPivaLocatore && c.tab_terzo.p_iva == p_pivaLocatario && c.id_tipo_bene == 3).FirstOrDefault();
        }

        public static tab_terzo_debitore GetTzModelliFiscaliPiva(string p_siglaTipo_bene, String p_cfPivaDichiarante, String p_pivaSostituto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cf_piva_soggetto_ispezione == p_cfPivaDichiarante && c.tab_terzo.p_iva == p_pivaSostituto && c.tab_tipo_bene.codice_bene == p_siglaTipo_bene).FirstOrDefault();
        }
        public static tab_terzo_debitore GetTzModelliFiscaliCF(string p_siglaTipo_bene, String p_cfPivaDichiarante, String p_pivaSostituto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cf_piva_soggetto_ispezione == p_cfPivaDichiarante && c.tab_terzo.cod_fiscale == p_pivaSostituto && c.tab_tipo_bene.codice_bene == p_siglaTipo_bene).FirstOrDefault();
        }

        /// <summary>
        /// aggiorna lo stato
        /// </summary>
        /// <param name="p_cfPivaSoggettoIsp"></param>
        /// <param name="p_nrContratto"></param>
        /// <param name="p_codStato"></param>
        /// <param name="p_dateFile"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static int UpdateStato(String p_cfPivaSoggettoIsp, String p_nrContratto, String p_codStato, DateTime? p_dateFile, dbEnte p_dbContext)
        {
            try
            {
                IQueryable<tab_terzo_debitore> v_terziList = TabTerzoDebitoreBD.GetList(p_dbContext).WhereAttivi().WhereByCfPivaSoggettoIspezione(p_cfPivaSoggettoIsp)
                                                                    .Where(t => t.nr_registrazione_contratto == p_nrContratto && t.data_aggiornamento < p_dateFile);

                v_terziList.ToList().ForEach(x => x.cod_stato = p_codStato);

                return v_terziList.ToList().Count();
            }
            catch (Exception e) { return -1; }
        }

        public static int UpdateStatoModelliFiscali(String p_cfPivaSoggettoIsp, String p_codStato, int p_id_tipo_bene, DateTime? p_dateFile, dbEnte p_dbContext)
        {
            try
            {
                IQueryable<tab_terzo_debitore> v_terziList = TabTerzoDebitoreBD.GetList(p_dbContext).WhereAttivi().WhereByCfPivaSoggettoIspezione(p_cfPivaSoggettoIsp)
                                                                    .Where(t => t.id_tipo_bene == p_id_tipo_bene && t.data_aggiornamento < p_dateFile);

                v_terziList.ToList().ForEach(x => x.cod_stato = p_codStato);

                return v_terziList.ToList().Count();
            }
            catch (Exception ex) { return -1; }
        }
        /// <summary>
        /// Lista pf Attive per ente
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_terzo_debitore> GetListPfAtt(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.id_ente == p_idEnte && c.tab_terzo.cod_fiscale != null && c.cod_stato.StartsWith(tab_terzo_debitore.ATT));
        }
    }
}
