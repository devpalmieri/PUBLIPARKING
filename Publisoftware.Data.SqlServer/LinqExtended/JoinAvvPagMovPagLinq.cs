using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinAvvPagMovPagLinq
    {
        public static IQueryable<join_avv_pag_mov_pag> WhereByPagatoUgualeAccreditato(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_avv_pag_accreditato.HasValue && d.id_avv_pag_pagato == d.id_avv_pag_accreditato);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByPagatoDiversoAccreditato(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_avv_pag_accreditato.HasValue && d.id_avv_pag_pagato != d.id_avv_pag_accreditato);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdAvvPagPagato(this IQueryable<join_avv_pag_mov_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_pagato == p_idAvvPag);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByNotIdAvvPagPagato(this IQueryable<join_avv_pag_mov_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_pagato != p_idAvvPag);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdAvvPagAccreditato(this IQueryable<join_avv_pag_mov_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_accreditato == p_idAvvPag);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByNotIdAvvPagAccreditato(this IQueryable<join_avv_pag_mov_pag> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag_accreditato != p_idAvvPag);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdAvvPagAccreditatoNull(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.Where(d => !d.id_avv_pag_accreditato.HasValue);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdAvvPagAccreditatoNotNull(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.Where(d => d.id_avv_pag_accreditato.HasValue);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdMovPag(this IQueryable<join_avv_pag_mov_pag> p_query, int p_idMovPag)
        {
            return p_query.Where(d => d.id_mov_pag == p_idMovPag);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByCodStato(this IQueryable<join_avv_pag_mov_pag> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByNotCodStato(this IQueryable<join_avv_pag_mov_pag> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByImportoPagato(this IQueryable<join_avv_pag_mov_pag> p_query, decimal? p_importo)
        {
            if (p_importo.HasValue)
            {
                return p_query.Where(d => System.Math.Abs((System.Math.Round(d.importo_pagato - p_importo.Value))) <= 1);
            }
            else
            {
                return p_query;
            }
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdContribuente(this IQueryable<join_avv_pag_mov_pag> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.tab_mov_pag.id_contribuente == p_idContribuente);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereNotByIdContribuente(this IQueryable<join_avv_pag_mov_pag> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.tab_mov_pag.id_contribuente != p_idContribuente || d.tab_mov_pag.id_contribuente == null);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdTabCCRiscossione(this IQueryable<join_avv_pag_mov_pag> p_query, int p_idTabCCRiscossione)
        {
            return p_query.Where(d => d.tab_mov_pag.id_tab_cc_riscossione == p_idTabCCRiscossione);
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByStatoNonAnnullato(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.Where(d => !d.tab_mov_pag.cod_stato.StartsWith(anagrafica_stato_mov_pag.ANNULLATO));
        }

        public static IQueryable<join_avv_pag_mov_pag> WhereByIdContribuenteInAvviso(this IQueryable<join_avv_pag_mov_pag> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => !d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) && d.tab_avv_pag.id_anag_contribuente == p_idContribuente && (d.tab_mov_pag.id_contribuente != p_idContribuente || d.tab_mov_pag.id_contribuente == null));
        }

        public static IQueryable<join_avv_pag_mov_pag> OrderByDefault(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.OrderBy(o => o.tab_mov_pag.data_operazione);
        }

        public static IQueryable<join_avv_pag_mov_pag> OrderByContribuente(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.id_anag_contribuente);
        }

        public static IQueryable<join_avv_pag_mov_pag> OrderByDataEmissione(this IQueryable<join_avv_pag_mov_pag> p_query)
        {
            return p_query.OrderBy(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<join_avv_pag_mov_pag_light> ToLight(this IQueryable<join_avv_pag_mov_pag> iniziale)
        {
            return iniziale.ToList().Select(d => new join_avv_pag_mov_pag_light
            {
                id_avv_pag_mov_pag = d.id_avv_pag_mov_pag,
                importo_pagato_Euro = d.importo_pagato_Euro,
                importo_mov_pagato_Euro = d.tab_mov_pag != null ? d.tab_mov_pag.importo_mov_pagato_Euro : string.Empty,
                data_oper_acc_String = d.data_oper_acc_String,
                AvvisoPagato = d.tab_avv_pag != null ? d.AvvisoPagato : string.Empty,
                AvvisoAccreditato = d.AvvisoAccreditato,
                NumeroCC = d.NumeroCC,
                id_mov_pag = d.id_mov_pag,
                nome_file = d.tab_mov_pag?.nome_file_immagine ?? String.Empty,
                soggettoDebitore = d.tab_avv_pag != null ? d.SoggettoDebitore : string.Empty,
                ContribuenteAvvPag = d.tab_avv_pag?.tab_contribuente?.contribuenteDisplay ?? string.Empty,
                Contabilizzato = d.cod_stato.EndsWith(join_avv_pag_mov_pag._CON) ? "Si" : "No"
            }).AsQueryable();
        }
    }
}
