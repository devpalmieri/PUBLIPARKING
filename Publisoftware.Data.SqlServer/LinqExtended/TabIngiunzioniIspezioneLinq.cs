using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabIngiunzioniIspezioneLinq
    {
        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_ingiunzioni_ispezione> OrderByDefault(this IQueryable<tab_ingiunzioni_ispezione> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_ingiunzioni_ispezione);
        }

        /// <summary>
        /// Filtro per Codice Stato
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_codStato">Codice Stato</param>
        /// <returns></returns>
        public static IQueryable<tab_ingiunzioni_ispezione> WhereByCodStato(this IQueryable<tab_ingiunzioni_ispezione> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        /// <summary>
        /// Filtro per Id Contribuente
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_codStato">Id Contribuente</param>
        /// <returns></returns>
        public static IQueryable<tab_ingiunzioni_ispezione> WhereByIdContribuente(this IQueryable<tab_ingiunzioni_ispezione> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_anag_contribuente == p_idContribuente);
        }

        /// <summary>
        /// Filtra le ingiunzioni valide ai fini supervisione
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_ingiunzioni_ispezione> ValidePerSupervisione(this IQueryable<tab_ingiunzioni_ispezione> p_query)
        {
            p_query =
                p_query.Where(i =>
                    //Avviso in sato Valido...
                    i.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL)
                    &&
                    //...con un da pagare > 10 euro...
                    i.tab_avv_pag.importo_tot_da_pagare.HasValue && i.tab_avv_pag.importo_tot_da_pagare.Value > 10
                    &&                    
                    //non rateizzato...
                    (i.tab_avv_pag.flag_rateizzazione_bis != null && i.tab_avv_pag.flag_rateizzazione_bis.CompareTo("1") != 0
                    ||
                    i.tab_avv_pag.flag_rateizzazione_bis == null)
                );

            return p_query;
        }
    }
}
