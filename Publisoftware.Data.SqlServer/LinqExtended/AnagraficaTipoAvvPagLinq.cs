using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaTipoAvvPagLinq
    {
        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdTipoServizio(this IQueryable<anagrafica_tipo_avv_pag> p_query, int id_servizio)
        {
            return p_query.Where(d => d.id_servizio == id_servizio);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdTipiServizoList(this IQueryable<anagrafica_tipo_avv_pag> p_query, IList<int> p_idTipiServizioList)
        {
            return p_query.Where(d => p_idTipiServizioList.Contains(d.id_servizio));
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdEntrata(this IQueryable<anagrafica_tipo_avv_pag> p_query, int id_entrata)
        {
            return p_query.Where(d => d.id_entrata == id_entrata
                                      /*|| d.id_entrata == anagrafica_entrate.NESSUNA_ENTRATA*/);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdEntrataCollegata(this IQueryable<anagrafica_tipo_avv_pag> p_query, int id_entrata)
        {
            return p_query.Where(d => d.id_entrata_avvpag_collegati == id_entrata);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdEntrateList(this IQueryable<anagrafica_tipo_avv_pag> p_query, IList<int> p_idEntrateList)
        {
            return p_query.Where(d => p_idEntrateList.Contains(d.id_entrata)
                                      /*|| d.id_entrata == anagrafica_entrate.NESSUNA_ENTRATA*/);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereBySollecitiIntimazioniEsclusi(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio != anagrafica_tipo_servizi.SOLL_PRECOA &&
                                      d.id_servizio != anagrafica_tipo_servizi.INTIM);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereBySollecitiIntimazioni(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
                                      d.id_servizio == anagrafica_tipo_servizi.INTIM);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByCautelariPignoramenti(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdTipoAvvPagList(this IQueryable<anagrafica_tipo_avv_pag> p_query, IList<int> p_idTipoAvvPagList)
        {
            return p_query.Where(d => p_idTipoAvvPagList.Contains(d.id_tipo_avvpag));
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByCodice(this IQueryable<anagrafica_tipo_avv_pag> p_query, string p_codice)
        {
            return p_query.Where(d => d.cod_tipo_avv_pag == p_codice);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> OrderByDefault(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.descr_tipo_avv_pag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> OrderByTipoAvviso(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.cod_tipo_avv_pag).ThenBy(d => d.descr_tipo_avv_pag); ;
        }

        public static IQueryable<anagrafica_tipo_avv_pag> OrderByEntrataAndTipo(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.id_entrata).ThenBy(d => d.id_tipo_avvpag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> OrderByIdTipoAvvPag(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.id_tipo_avvpag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> OrderByIdTipoAvvPagDesc(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.OrderByDescending(d => d.id_tipo_avvpag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> OrderByIdServizioIdEntrataCodiceAvviso(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.anagrafica_entrate.join_entrate_macroentrate.FirstOrDefault().id_tab_macroentrate)
                          .ThenBy(d => d.id_entrata)
                          .ThenBy(d => d.id_servizio)
                          .ThenBy(d => d.cod_tipo_avv_pag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdTipoAvvPag(this IQueryable<anagrafica_tipo_avv_pag> p_query, int p_id_tipo_avvpag)
        {
            return p_query.Where(w => w.id_tipo_avvpag == p_id_tipo_avvpag);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIciImuTasiEsclusi(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(w => !((w.id_entrata == anagrafica_entrate.ICI ||
                                         w.id_entrata == anagrafica_entrate.IMU ||
                                         w.id_entrata == anagrafica_entrate.TASI) &&
                                         w.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA));
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIdServizio(this IQueryable<anagrafica_tipo_avv_pag> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.id_servizio == p_idServizio);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByServiziIstanzeRicorsi(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => (d.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA) ||
                                      (d.id_servizio >= anagrafica_tipo_servizi.ACCERTAMENTO && d.id_servizio <= anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.SERVIZI_DEFINIZIONE_AGEVOLATA_COA));
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByServiziRimborsi(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => (d.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA && d.id_entrata != anagrafica_entrate.ICI && d.id_entrata != anagrafica_entrate.IMU) ||
                                      (d.id_servizio >= anagrafica_tipo_servizi.ACCERTAMENTO && d.id_servizio <= anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO) ||
                                      (d.id_servizio == anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA));
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByServiziRateizzazione(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio != anagrafica_tipo_servizi.GEST_ORDINARIA ||
                                     (d.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA &&
                                      d.id_entrata != anagrafica_entrate.ICI &&
                                      d.id_entrata != anagrafica_entrate.IMU &&
                                      d.id_entrata != anagrafica_entrate.TASI));
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByRateizzazioneCoattivaEAccertamenti(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_entrata == anagrafica_entrate.RISCOSSIONE_COATTIVA ||
                                      d.id_servizio == anagrafica_tipo_servizi.ACCERTAMENTO ||
                                      d.id_servizio == anagrafica_tipo_servizi.RISC_PRECOA ||
                                      d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM ||
                                      d.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO ||
                                      d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByServiziIngiunzione(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio == anagrafica_tipo_servizi.ING_FISC ||
                                      d.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA ||
                                      d.id_servizio == anagrafica_tipo_servizi.INTIM ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI ||
                                      d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByServiziCommTrib(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => (d.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.id_servizio == anagrafica_tipo_servizi.ACCERTAMENTO && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.id_servizio == anagrafica_tipo_servizi.ING_FISC && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.id_servizio == anagrafica_tipo_servizi.INTIM && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByServiziGDPOrTribOrd(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => (d.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.id_servizio == anagrafica_tipo_servizi.RISC_PRECOA && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E)
                                    || (d.id_servizio == anagrafica_tipo_servizi.ING_FISC && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || (d.id_servizio == anagrafica_tipo_servizi.INTIM && d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T)
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI
                                    || d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByNaturaToG(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_T ||
                                      d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_G);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByNaturaEoG(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_E ||
                                      d.flag_natura_avv_collegati == anagrafica_tipo_avv_pag.FLAG_NATURA_G);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByNaturaNotT(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.flag_natura_avv_collegati != anagrafica_tipo_avv_pag.FLAG_NATURA_T);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByIngiunzioniCautelari(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio == anagrafica_tipo_servizi.ING_FISC ||
                                      d.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO ||
                                      d.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI);
        }

        public static IQueryable<anagrafica_tipo_avv_pag> WhereByPignoramentiVersoTerzi(this IQueryable<anagrafica_tipo_avv_pag> p_query)
        {
            return p_query.Where(d => d.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO);
        }
    }
}
