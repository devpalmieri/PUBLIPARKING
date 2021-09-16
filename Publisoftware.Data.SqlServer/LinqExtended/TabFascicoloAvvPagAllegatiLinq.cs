using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFascicoloAvvPagAllegatiLinq
    {
        public static IQueryable<tab_fascicolo_avvpag_allegati> OrderByDefault(this IQueryable<tab_fascicolo_avvpag_allegati> p_query)
        {
            return p_query.OrderByDescending(d => d.tab_avv_pag.dt_emissione);
        }

        public static IQueryable<tab_fascicolo_avvpag_allegati> WhereByIdAvviso(this IQueryable<tab_fascicolo_avvpag_allegati> p_query, int p_idAvviso)
        {
            return p_query.Where(d => d.id_avvpag_rif_documenti == p_idAvviso);
        }

        public static IQueryable<tab_fascicolo_avvpag_allegati> WhereByIdSpedNot(this IQueryable<tab_fascicolo_avvpag_allegati> p_query, int p_idSpedNot)
        {
            return p_query.Where(d => d.id_tab_sped_not == p_idSpedNot);
        }

        public static IQueryable<tab_fascicolo_avvpag_allegati> WhereByIdFascicolo(this IQueryable<tab_fascicolo_avvpag_allegati> p_query, int p_idFascicolo)
        {
            return p_query.Where(d => d.id_fascicolo == p_idFascicolo);
        }

        public static IQueryable<tab_fascicolo_avvpag_allegati> WhereByCodStatoNot(this IQueryable<tab_fascicolo_avvpag_allegati> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_fascicolo_avvpag_allegati> WhereByEnteStrutturaTipoAvvPag(this IQueryable<tab_fascicolo_avvpag_allegati> p_query, int p_idEnteAppartenenza/*, int p_idEnte, int p_idStruttura*/)
        {
            if (p_idEnteAppartenenza != anagrafica_ente.ID_ENTE_PUBLISERVIZI)
            {
                return p_query.Where(x => x.tab_avv_pag.id_ente == x.tab_avv_pag.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza
                                          /*&& x.tab_avv_pag.tab_liste.id_struttura_approvazione == p_idStruttura*/);
            }
            else
            {
                return p_query/*.Where(x => x.tab_avv_pag.id_ente != x.tab_avv_pag.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza)*/;
            }
        }

        public static IList<tab_fascicolo_avvpag_allegati_light> ToLight(this IQueryable<tab_fascicolo_avvpag_allegati> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_fascicolo_avvpag_allegati_light
            {
                id_fascicolo_allegati_avvpag = d.id_fascicolo_allegati_avvpag,
                id_fascicolo = d.id_fascicolo,
                id_avvpag_rif_documenti = d.id_avvpag_rif_documenti,
                barcodeNotifica = d.tab_sped_not.barcode,
                destinatarioNotifica = d.tab_sped_not.Destinatario,
                dataNotifica = d.tab_sped_not.data_esito_notifica_String,
                numeroRaccomandata = d.tab_sped_not.numero_raccomandata,
                NumeroAttoGiudiziario = d.tab_sped_not.numero_atto_giudiziario,
                IsAvvisoImmagine = d.join_documenti_fascicolo_avvpag_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_RIF ||
                                                                                       x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_COLL ||
                                                                                       x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_AVVISO_ORDINE),
                IsNotificaImmagineNotificata = d.join_documenti_fascicolo_avvpag_allegati.Where(y => y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.NOT_OKK_ID ||
                                                                                                   ((y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.RIL_140_ID ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.RIL_140_ID) && y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                                                                                   ((y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.RIL_139_ID ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.RIL_139_ID) && y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_avvdep == anagrafica_stato_sped_not.NOT_OKK_ID) ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.NOT_143_ID ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.NOT_143_ID ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato_sped_not == anagrafica_stato_sped_not.NOT_SPO_ID ||
                                                                                                     y.tab_fascicolo_avvpag_allegati.tab_sped_not.id_stato == anagrafica_stato_sped_not.NOT_SPO_ID)
                                                                                         .Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA),
                IsNotificaImmagine = d.join_documenti_fascicolo_avvpag_allegati.Any(x => x.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_NOTIFICA_RELATA),
                IsConNotifica = d.tab_avv_pag.flag_spedizione_notifica == "1",
                identificativo_avv_pag = d.tab_avv_pag.identificativo_avv_pag != null ? d.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_avv_pag_rif = d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag != null ? d.tab_fascicolo.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_fascicolo.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_doc_input = d.tab_fascicolo.tab_doc_input.identificativo_doc_input != null ? d.tab_fascicolo.tab_doc_input.tab_tipo_doc_entrate.descr_doc + " " + d.tab_fascicolo.tab_doc_input.identificativo_doc_input : string.Empty,
                isButtonAcquisisciNotifica = d.id_avvpag_rif_documenti == d.tab_fascicolo.id_avv_pag && d.tab_sped_not.flag_soggetto_debitore != "U"
            }).ToList();
        }
    }
}
