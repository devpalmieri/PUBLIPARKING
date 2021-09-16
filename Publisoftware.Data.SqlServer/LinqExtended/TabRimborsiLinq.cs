using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabRimborsiLinq
    {
        public static IQueryable<tab_rimborsi> WhereByIdTabDocInput(this IQueryable<tab_rimborsi> p_query, int p_idTabDocInput)
        {
            return p_query.Where(d => d.id_tab_doc_input == p_idTabDocInput);
        }

        public static IQueryable<tab_rimborsi> WhereByCodStato(this IQueryable<tab_rimborsi> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato == p_codStato);
        }

        public static IQueryable<tab_rimborsi> WhereByCodStato(this IQueryable<tab_rimborsi> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_rimborsi> WhereByTipoBonifico(this IQueryable<tab_rimborsi> p_query, string p_tipoBonifico)
        {
            return p_query.Where(d => d.tipo_bonifico == p_tipoBonifico);
        }

        public static IQueryable<tab_rimborsi> WhereByTabDocInputCodStato(this IQueryable<tab_rimborsi> p_query, string p_codStato)
        {
            return p_query.Where(d => d.tab_doc_input.cod_stato == p_codStato);
        }

        public static IQueryable<tab_rimborsi> WhereByIstanzaEsitataAccolta(this IQueryable<tab_rimborsi> p_query)
        {
            return p_query.Where(d => d.tab_doc_input.cod_stato == anagrafica_stato_doc.STATO_ESITATA_ACCOLTA);
        }

        public static IQueryable<tab_rimborsi> WhereByChiaveRaccordo(this IQueryable<tab_rimborsi> p_query, string p_ChiaveRaccordo)
        {
            return p_query.Where(d => d.chiave_raccordo_rendicontazione_bonifico == p_ChiaveRaccordo);
        }

        public static IQueryable<tab_rimborsi> WhereByParolaChiave(this IQueryable<tab_rimborsi> p_query, string p_ParolaChiave)
        {
            return p_query.Where(d => d.parola_chiave_bonifico_domiciliato == p_ParolaChiave);
        }

        public static IQueryable<tab_rimborsi> OrderByDataPresentazione(this IQueryable<tab_rimborsi> p_query)
        {
            return p_query.OrderBy(d => d.tab_doc_input.data_presentazione);
        }

        public static IList<tab_rimborsi_light> ToLight(this IQueryable<tab_rimborsi> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_rimborsi_light
            {
                id_tab_rimborsi = d.id_tab_rimborsi,
                id_tab_doc_input = d.id_tab_doc_input,
                data_creazione_disposizione_rimborso = d.data_creazione_disposizione_rimborso_String,
                data_presentazione = d.tab_doc_input.data_presentazione_String,
                nominativo_rag_soc_beneficiario = d.tab_doc_input.join_tab_avv_pag_tab_doc_input
                                                                 .Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO))
                                                                 .FirstOrDefault()
                                                                 .join_doc_input_pag_avvpag
                                                                 .Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO))
                                                                 .FirstOrDefault()
                                                                 .nominativo_rag_soc_beneficiario,
                iban_beneficiario_bonifico = d.tab_doc_input.join_tab_avv_pag_tab_doc_input
                                                            .Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO))
                                                            .FirstOrDefault()
                                                            .join_doc_input_pag_avvpag
                                                            .Where(x => !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO))
                                                            .FirstOrDefault()
                                                            .iban_beneficiario_bonifico,
                imp = d.importo,
                importo = d.importo_Euro,
                tipo_rimborso = d.tipoRimborso
            }).ToList();
        }
    }
}