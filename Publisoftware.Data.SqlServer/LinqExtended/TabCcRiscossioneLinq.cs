using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabCcRiscossioneLinq
    {
        public static IQueryable<tab_cc_riscossione> WhereByIdEnte(this IQueryable<tab_cc_riscossione> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_cc_riscossione> WhereByIdAbiCabNumeroNotNull(this IQueryable<tab_cc_riscossione> p_query)
        {
            return p_query.Where(d => d.id_anagrafica_abi_cab.HasValue);
        }

        public static IQueryable<tab_cc_riscossione> WhereByCCNonGestiti(this IQueryable<tab_cc_riscossione> p_query)
        {
            return p_query.Where(d => d.flag_tipo_cc == tab_cc_riscossione.FLAG_TIPO_CC_ENTE ||
                                      d.flag_tipo_cc == tab_cc_riscossione.FLAG_TIPO_CC_TESORERIA);
        }

        public static IQueryable<tab_cc_riscossione> WhereByCCGestiti(this IQueryable<tab_cc_riscossione> p_query)
        {
            return p_query.Where(d => d.flag_tipo_cc != tab_cc_riscossione.FLAG_TIPO_CC_ENTE &&
                                      d.flag_tipo_cc != tab_cc_riscossione.FLAG_TIPO_CC_TESORERIA);
        }

        public static IQueryable<tab_cc_riscossione> WhereByNumeroCC(this IQueryable<tab_cc_riscossione> p_query, int p_idEnte, string p_numeroCC)
        {
            if (p_numeroCC == string.Empty)
            {
                return p_query.Where(d => d.id_ente == p_idEnte &&
                                          d.id_tipo_servizio != anagrafica_tipo_servizi.SERVIZI_RIMBORSO);
            }
            else
            {
                return p_query.Where(d => d.id_ente == p_idEnte &&
                                          d.id_tipo_servizio != anagrafica_tipo_servizi.SERVIZI_RIMBORSO &&
                                          d.num_cc.Contains(p_numeroCC));
            }
        }

        public static IQueryable<tab_cc_riscossione> WhereByIdTipoServizio(this IQueryable<tab_cc_riscossione> p_query, int p_idTipoServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio == p_idTipoServizio);
        }

        public static IQueryable<tab_cc_riscossione> WhereByIdTipoServizioNot(this IQueryable<tab_cc_riscossione> p_query, int p_idTipoServizio)
        {
            return p_query.Where(d => d.id_tipo_servizio != p_idTipoServizio);
        }

        //public static IQueryable<tab_cc_riscossione> WhereByAbiCabNumeroCC(this IQueryable<tab_cc_riscossione> p_query, int p_idEnte, int p_idAbiCab, string p_numeroCC)
        //{
        //    if (p_numeroCC == string.Empty)
        //    {
        //        return p_query.Where(d => d.id_ente == p_idEnte && 
        //                                 (d.id_anagrafica_abi_cab == p_idAbiCab || d.id_anagrafica_abi_cab == null));
        //    }
        //    else
        //    {
        //        return p_query.Where(d => d.id_ente == p_idEnte &&
        //                                 (d.id_anagrafica_abi_cab == p_idAbiCab || d.id_anagrafica_abi_cab == null) && 
        //                                  d.num_cc.Equals(p_numeroCC));
        //    }
        //}

        //public static IQueryable<tab_cc_riscossione> WhereByAbiCabNumeroCCIdEntrataAvvPag(this IQueryable<tab_cc_riscossione> p_query, int p_idEnte, int p_idAbiCab, string p_numeroCC, int IdEntrataAvvPag)
        //{
        //    if (p_numeroCC == string.Empty)
        //    {
        //        if (p_query.Where(d => d.id_ente == p_idEnte && d.ANAGRAFICA_ABI_CAB.ID_ABI_CAB == p_idAbiCab && d.id_entrata == IdEntrataAvvPag).Count() > 0)
        //        {
        //            return p_query.Where(d => d.id_ente == p_idEnte &&
        //                                     (d.id_anagrafica_abi_cab == p_idAbiCab || d.id_anagrafica_abi_cab == null) &&
        //                                      d.id_entrata == IdEntrataAvvPag);
        //        }
        //        else
        //        {
        //            return p_query.Where(d => d.id_ente == p_idEnte &&
        //                                     (d.id_anagrafica_abi_cab == p_idAbiCab || d.id_anagrafica_abi_cab == null) &&
        //                                      d.id_entrata == null);
        //        }
        //    }
        //    else
        //    {
        //        if (p_query.Where(d => d.id_ente == p_idEnte && d.ANAGRAFICA_ABI_CAB.ID_ABI_CAB == p_idAbiCab && d.id_entrata == IdEntrataAvvPag).Count() > 0)
        //        {
        //            return p_query.Where(d => d.id_ente == p_idEnte &&
        //                                     (d.id_anagrafica_abi_cab == p_idAbiCab || d.id_anagrafica_abi_cab == null) &&
        //                                      d.id_entrata == IdEntrataAvvPag && 
        //                                      d.num_cc.Equals(p_numeroCC));
        //        }
        //        else
        //        {
        //            return p_query.Where(d => d.id_ente == p_idEnte && 
        //                                     (d.id_anagrafica_abi_cab == p_idAbiCab || d.id_anagrafica_abi_cab == null) &&
        //                                      d.id_entrata == null && 
        //                                      d.num_cc.Equals(p_numeroCC));
        //        }
        //    }
        //}

        public static IQueryable<tab_cc_riscossione> OrderByDefault(this IQueryable<tab_cc_riscossione> p_query)
        {
            return p_query.OrderBy(o => o.id_entrata);
        }

        public static IQueryable<tab_cc_riscossione> OrderByPagamenti(this IQueryable<tab_cc_riscossione> p_query)
        {
            return p_query.OrderBy(d => d.ANAGRAFICA_ABI_CAB.ABI).ThenBy(d => d.ANAGRAFICA_ABI_CAB.CAB).ThenBy(d => d.id_entrata).ThenBy(d => d.data_apertura_cc);
        }

        public static IQueryable<tab_cc_riscossione_light> ToLight(this IQueryable<tab_cc_riscossione> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_cc_riscossione_light
            {
                id_tab_cc_riscossione = d.id_tab_cc_riscossione,
                Banca = d.ANAGRAFICA_ABI_CAB != null ? d.ANAGRAFICA_ABI_CAB.BANCA : string.Empty,
                NumCC = d.num_cc,
                IntestazioneCC = d.intestazione_cc
            }).AsQueryable();
        }
    }
}
