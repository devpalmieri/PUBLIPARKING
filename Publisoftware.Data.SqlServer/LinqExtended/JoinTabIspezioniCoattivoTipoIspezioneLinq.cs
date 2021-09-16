using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinTabIspezioniCoattivoTipoIspezioneLinq
    {
        //public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereSiatel(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query)
        //{
        //    return p_query.Where(w => w.tab_tipo_ispezione.sigla_tipo_ispezione.Trim() == tab_tipo_ispezione.SIATEL);
        //}

        //public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereDisponibilitaFinanziaria(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query)
        //{
        //    return p_query.Where(w => w.tab_tipo_ispezione.sigla_tipo_ispezione.Trim() == tab_tipo_ispezione.DISPONIBILITA_FINANZIARIA);
        //}

        //public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereImmobili(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query)
        //{
        //    return p_query.Where(w => w.tab_tipo_ispezione.sigla_tipo_ispezione.Trim() == tab_tipo_ispezione.IMMOBILI);
        //}

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereSiglaTipoIspezione(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, string p_sigla)
        {
            return p_query.Where(w => w.tab_tipo_ispezione.sigla_tipo_ispezione.Trim() == p_sigla);
        }

        /// <summary>
        /// Seleziona quelle collegate ad una Ispezione
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByIdIspezione(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, int p_idIspezione)
        {
            return p_query.Where(j => j.id_tab_ispezione_coattivo == p_idIspezione);
        }
        
        /// <summary>
        /// Seleziona quelle valide e non assegnate
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereAssegnabili(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query)
        {
            return p_query.Where(j => j.id_risorsa_ispezione == null && 
                                      j.cod_stato == join_tab_ispezioni_coattivo_tipo_ispezione.VAL_VAL && 
                                      j.flag_fine_ispezione == "0" && 
                                      j.tab_ispezioni_coattivo_new.cod_stato == CodStato.VAL_VAL && 
                                      j.tab_ispezioni_coattivo_new.flag_fine_ispezione_totale == "0");
        }

        /// <summary>
        /// Seleziona quelle valide e assegnate alla risorsa indicata
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereInLavorazioneByIdRisorsaAssegnata(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, int p_idRisorsa)
        {
            return p_query.Where(j => j.id_risorsa_ispezione == p_idRisorsa && 
                                      j.cod_stato == join_tab_ispezioni_coattivo_tipo_ispezione.VAL_VAL && 
                                      j.flag_fine_ispezione == "0");
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByIdRisorsaIspezione(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, int p_idRisorsa)
        {
            return p_query.Where(j => j.id_risorsa_ispezione == p_idRisorsa);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByLiberaOAssegnateASe(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => !d.id_risorsa_ispezione.HasValue || d.id_risorsa_ispezione == p_idRisorsa);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByStato(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, string p_stato)
        {
            return p_query.Where(d => d.cod_stato == p_stato);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByFlagFineIspezione(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, string p_flag)
        {
            return p_query.Where(d => d.flag_fine_ispezione == p_flag);
        }


        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByStatoIspezione(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, string p_stato)
        {
            return p_query.Where(d => d.tab_ispezioni_coattivo_new.cod_stato == p_stato);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> WhereByFlagFineIspezioneTotale(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, string p_flag)
        {
            return p_query.Where(d => d.tab_ispezioni_coattivo_new.flag_fine_ispezione_totale == p_flag);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> OrderByLiberaOAssegnateASe(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query, int p_idRisorsa)
        {
            return p_query.OrderBy(d => d.id_risorsa_ispezione.HasValue && 
                                        d.id_risorsa_ispezione.Value == p_idRisorsa)
                          .ThenBy(d => !d.id_risorsa_ispezione.HasValue);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> OrderByMorositaSoggettoIspezione(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query)
        {
            return p_query.OrderByDescending(o => o.tab_ispezioni_coattivo_new.totale_morosita_soggetto_ispezione.HasValue ? o.tab_ispezioni_coattivo_new.totale_morosita_soggetto_ispezione.Value : 0);
        }

        public static IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> OrderByDefault(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> p_query)
        {
            return p_query.OrderBy(o => o.id_join_tab_ispezioni_coattivo_tipo_ispezione);
        }

        public static IList<join_tab_ispezioni_coattivo_tipo_ispezione_light> ToLight(this IQueryable<join_tab_ispezioni_coattivo_tipo_ispezione> iniziale)
        {
            return iniziale.ToList().Select(d => new join_tab_ispezioni_coattivo_tipo_ispezione_light
            {
                id_join_tab_ispezioni_coattivo_tipo_ispezione = d.id_join_tab_ispezioni_coattivo_tipo_ispezione,
                descrizioneIspezione = d.tab_tipo_ispezione.descrizione,
                descrizioneTipoBene = d.tab_tipo_ispezione.tab_tipo_bene.FirstOrDefault().descrizione,
                dataIspezione = d.data_fine_ispezione_String,
                esitoIspezione = d.esitoIspezione
            }).ToList();
        }
    }
}
