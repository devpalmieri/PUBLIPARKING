﻿using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFascicoloDocOutputAllegatiLinq
    {
        public static IQueryable<tab_fascicolo_doc_output_allegati> OrderByDefault(this IQueryable<tab_fascicolo_doc_output_allegati> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_doc_output.data_emissione_doc);
        }

        public static IQueryable<tab_fascicolo_doc_output_allegati> WhereByIdDocOutput(this IQueryable<tab_fascicolo_doc_output_allegati> p_query, int p_idDocOutput)
        {
            return p_query.Where(d => d.id_doc_output_rif_documenti == p_idDocOutput);
        }

        public static IQueryable<tab_fascicolo_doc_output_allegati> WhereByIdSpedNot(this IQueryable<tab_fascicolo_doc_output_allegati> p_query, int p_idSpedNot)
        {
            return p_query.Where(d => d.id_tab_sped_not == p_idSpedNot);
        }

        public static IQueryable<tab_fascicolo_doc_output_allegati> WhereByIdFascicolo(this IQueryable<tab_fascicolo_doc_output_allegati> p_query, int p_idFascicolo)
        {
            return p_query.Where(d => d.id_fascicolo == p_idFascicolo);
        }

        public static IQueryable<tab_fascicolo_doc_output_allegati> WhereByCodStatoNot(this IQueryable<tab_fascicolo_doc_output_allegati> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IList<tab_fascicolo_doc_output_allegati_light> ToLight(this IQueryable<tab_fascicolo_doc_output_allegati> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_fascicolo_doc_output_allegati_light
            {
                id_fascicolo_doc_output_allegati = d.id_fascicolo_doc_output_allegati,
                id_fascicolo = d.id_fascicolo,
                id_doc_output_rif_documenti = d.id_doc_output_rif_documenti,
                barcodeNotifica = d.tab_sped_not.barcode,
                destinatarioNotifica = d.tab_sped_not.Destinatario,
                dataNotifica = d.tab_sped_not.data_esito_notifica_String,
                numeroRaccomandata = d.tab_sped_not.numero_raccomandata,
                NumeroAttoGiudiziario = d.tab_sped_not.numero_atto_giudiziario,
                IsDocOutputImmagine = d.join_documenti_fascicolo_doc_output_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_DOC_OUTPUT),
                IsNotificaImmagine = d.join_documenti_fascicolo_doc_output_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA),
                IsNotificaImmagineNotificata = d.join_documenti_fascicolo_doc_output_allegati.Where(y => y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                                                                                       ((y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.RIL_140_ID) && y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                                                                                       ((y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.RIL_139_ID ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.RIL_139_ID) && y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.NOT_143_ID ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.NOT_143_ID ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.NOT_SPO_ID ||
                                                                                                         y.tab_fascicolo_doc_output_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.NOT_SPO_ID)
                                                                                             .Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA),
                //identificativo_avv_pag = d.tab_avv_pag.identificativo_avv_pag != null ? d.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_avv_pag.identificativo_avv_pag : string.Empty,
                //identificativo_avv_pag_rif = d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag != null ? d.tab_fascicolo.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_doc_input = d.tab_fascicolo.tab_doc_input.identificativo_doc_input != null ? d.tab_fascicolo.tab_doc_input.tab_tipo_doc_entrate.descr_doc + " " + d.tab_fascicolo.tab_doc_input.identificativo_doc_input : string.Empty,
                barcodeDocOutput = !string.IsNullOrEmpty(d.tab_doc_output.barcode) ? d.tab_doc_output.barcode : string.Empty
                //isButtonAcquisisciNotifica = d.id_avvpag_rif_documenti == d.tab_fascicolo.id_avv_pag && d.tab_sped_not.flag_soggetto_debitore != "U"
            }).ToList();
        }
    }
}
