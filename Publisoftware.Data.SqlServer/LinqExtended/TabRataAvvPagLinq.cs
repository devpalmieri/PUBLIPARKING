using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRataAvvPagLinq
    {
        public static IQueryable<tab_rata_avv_pag> WhereByIdRateList(this IQueryable<tab_rata_avv_pag> p_query, List<int> p_idRataList)
        {
            return p_query.Where(d => p_idRataList.Contains(d.id_rata_avv_pag));
        }

        public static IQueryable<tab_rata_avv_pag> WhereByDataScadenza(this IQueryable<tab_rata_avv_pag> p_query, int p_numGGMaxScadenzaRataInsoluta)
        {
            return p_query.Where(d => !d.dt_scadenza_rata.HasValue || DateTime.Now > System.Data.Entity.DbFunctions.AddDays(d.dt_scadenza_rata.Value, p_numGGMaxScadenzaRataInsoluta));
        }

        public static IQueryable<tab_rata_avv_pag> WhereByIdAvvPag(this IQueryable<tab_rata_avv_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_tab_avv_pag == p_idAvvPag);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByImportoDaPagarePositivo(this IQueryable<tab_rata_avv_pag> p_query)
        {
            return p_query.Where(d => (d.imp_tot_rata - d.imp_pagato) > 1);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByStato(this IQueryable<tab_rata_avv_pag> p_query, string p_stato)
        {
            return p_query.Where(d => d.cod_stato.Contains(p_stato));
        }

        public static IQueryable<tab_rata_avv_pag> WhereByNotStato(this IQueryable<tab_rata_avv_pag> p_query, string p_stato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_stato));
        }

        public static IQueryable<tab_rata_avv_pag> WhereByIUV(this IQueryable<tab_rata_avv_pag> p_query, string p_identificativo)
        {
            return p_query.Where(d => d.Iuv_identificativo_pagamento == p_identificativo);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByCodicePagamentoPagoPA(this IQueryable<tab_rata_avv_pag> p_query, string p_codice_pagamento_pagopa)
        {
            return p_query.Where(d => d.codice_pagamento_pagopa == p_codice_pagamento_pagopa);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByIUVCodicePagamentoPagoPA(this IQueryable<tab_rata_avv_pag> p_query, string p_identificativo)
        {
            return p_query.Where(d => d.Iuv_identificativo_pagamento == p_identificativo ||
                                      d.codice_pagamento_pagopa == p_identificativo);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByIdAvvPagSenzaRataUnica(this IQueryable<tab_rata_avv_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(w => w.id_tab_avv_pag == p_idAvvPag && w.num_rata != 0);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByIdAvvPagSoloRataUnica(this IQueryable<tab_rata_avv_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(w => w.id_tab_avv_pag == p_idAvvPag && w.num_rata == 0);
        }

        public static IQueryable<tab_rata_avv_pag> WhereByImportoRata(this IQueryable<tab_rata_avv_pag> p_query, decimal? p_importo)
        {
            if (p_importo.HasValue)
            {
                //Il dottore ha voluto filtrare per l'importo esatto
                //return p_query.Where(d => System.Math.Abs((System.Math.Round((d.importo_mov_pagato.HasValue ? d.importo_mov_pagato.Value : 0) - p_importo.Value))) <= 1);
                return p_query.Where(d => d.imp_tot_rata == p_importo);
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<tab_rata_avv_pag> OrderByDefault(this IQueryable<tab_rata_avv_pag> p_query)
        {
            return p_query.OrderBy(o => o.num_rata);
        }
        public static IQueryable<tab_rata_avv_pag> OrderByMinImpTotAsc(this IQueryable<tab_rata_avv_pag> p_query)
        {
            return p_query.OrderBy(o => o.imp_tot_rata);
        }
        public static IQueryable<tab_rata_avv_pag> OrderByMaxImpTotDesc(this IQueryable<tab_rata_avv_pag> p_query)
        {
            return p_query.OrderByDescending(o => o.imp_tot_rata);
        }

        public static IQueryable<tab_rata_avv_pag> OrderByScadenzaRata(this IQueryable<tab_rata_avv_pag> p_query)
        {
            return p_query.OrderBy(d => d.dt_scadenza_rata);
        }

        public static IQueryable<tab_rata_avv_pag_light> ToLight(this IQueryable<tab_rata_avv_pag> iniziale)
        {
            DateTime? dtNullable = null;
            return iniziale.ToList().Select(d => new tab_rata_avv_pag_light
            {
                id_rata_avv_pag = d.id_rata_avv_pag,
                num_rata = d.num_rata,
                IsRataPagata = d.IsRataPagata,
                dt_scadenza_rata_String = d.dt_scadenza_rata_String,
                imp_tot_rata_Euro = d.imp_tot_rata_Euro,
                imp_pagato_Euro = d.imp_pagato_Euro,
                imp_da_pagare_Euro = d.imp_da_pagare_Euro,
                bar_code = d.bar_code,
                scaduto = d.scaduto,
                intestazione = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.intestazione_cc : string.Empty,
                numerocc = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.num_cc : string.Empty,
                ABI = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.ABI_CC : string.Empty,
                CAB = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.CAB_CC : string.Empty,
                IBAN = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.IBAN : string.Empty,
                IsAvvisoPagabile = d.tab_avv_pag.IsAvvisoPagabile,
                dataMassimaPagamentoAvviso = d.tab_avv_pag.dataMassimaPagamentoAvviso,
                IdTipoAvvPag = d.tab_avv_pag.id_tipo_avvpag,
                pagabile = d.pagabile,
                inPagamento = d.inPagamento,
                IUV = d.Iuv_identificativo_pagamento,
                IdTabAvvPag = d.id_tab_avv_pag,
                Cod_Stato = d.cod_stato,
                Descrizione_Stato = d.Descrizione_Stato,
                Id_Stato = d.pagabile == true ? tab_rata_avv_pag.ATT_ATT_ID : tab_rata_avv_pag.ATT_INP_ID,
                Importo_Residuo = d.imp_da_pagare,
                Id_CC_Riscossione = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.id_tab_cc_riscossione : 0,
                //Bic_Accredito = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.bic_swift : string.Empty,
                //Iban_Accredito = d.tab_cc_riscossione != null ? d.tab_cc_riscossione.IBAN : string.Empty,
                Imp_Spese_Coattive = d.imp_spese_coattive.HasValue ? d.imp_spese_coattive.Value : 0,
                Imp_Spese_Notifica = d.imp_spese_notifica.HasValue ? d.imp_spese_notifica.Value : 0,
                Descr_Tipo_AvvPag = d.tab_avv_pag.identificativo_avv_pag + " - " + d.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag,
                Identificativo_Avviso = d.tab_avv_pag.identificativo_avv_pag,
                Data_Ricezione_AvvPag = d.tab_avv_pag.data_ricezione.HasValue ? d.tab_avv_pag.data_ricezione : dtNullable,
                codice_cbill = d.codice_cbill,
                codice_tassonomia_pagopa = d.codice_tassonomia_pagopa,
                codice_pagamento_pagopa = d.codice_pagamento_pagopa

            }).AsQueryable();
        }
    }
}
