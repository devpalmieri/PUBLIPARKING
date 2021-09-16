using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinDocumentiFascicoloAvvpagAllegatiLinq
    {
        public static IQueryable<join_documenti_fascicolo_avvpag_allegati> WhereByMacrocategorie(this IQueryable<join_documenti_fascicolo_avvpag_allegati> p_query, List<string> macrocategorie)
        {
            return p_query.Where(d => macrocategorie.Contains(d.tab_documenti.anagrafica_documenti.macrocategoria));
        }

        public static IQueryable<join_documenti_fascicolo_avvpag_allegati> WhereBySigle(this IQueryable<join_documenti_fascicolo_avvpag_allegati> p_query, List<string> sigle)
        {
            return p_query.Where(d => sigle.Contains(d.tab_documenti.anagrafica_documenti.sigla_doc));
        }

        public static IQueryable<join_documenti_fascicolo_avvpag_allegati> WhereByIdDocumento(this IQueryable<join_documenti_fascicolo_avvpag_allegati> p_query, int p_idDocumento)
        {
            return p_query.Where(d => d.id_tab_documenti == p_idDocumento);
        }

        public static IQueryable<join_documenti_fascicolo_avvpag_allegati> WhereByIdFascicoloAvvpagAllegati(this IQueryable<join_documenti_fascicolo_avvpag_allegati> p_query, int p_idFascicoloAvvpagAllegati)
        {
            return p_query.Where(d => d.id_fascicolo_allegati_avvpag == p_idFascicoloAvvpagAllegati);
        }

        public static IList<join_documenti_fascicolo_avvpag_allegati_light> ToLight(this IQueryable<join_documenti_fascicolo_avvpag_allegati> iniziale)
        {
            return iniziale.ToList().Select(d => new join_documenti_fascicolo_avvpag_allegati_light
            {
                id_join_documenti_fascicolo_avvpag_allegati = d.id_join_documenti_fascicolo_avvpag_allegati,
                id_tab_documenti = d.tab_documenti.id_tab_documenti,
                tipo_documento = d.tab_documenti.anagrafica_documenti.descrizione_doc,
                barcodeNotifica = d.tab_fascicolo_avvpag_allegati.tab_sped_not.barcode,
                destinatarioNotifica = d.tab_fascicolo_avvpag_allegati.tab_sped_not.Destinatario,
                dataNotifica = d.tab_fascicolo_avvpag_allegati.tab_sped_not.data_esito_notifica_String,
                identificativo_avv_pag = d.tab_fascicolo_avvpag_allegati.tab_avv_pag.identificativo_avv_pag != null ? d.tab_fascicolo_avvpag_allegati.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_fascicolo_avvpag_allegati.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_avv_pag_rif = d.tab_fascicolo_avvpag_allegati.tab_fascicolo.tab_avv_pag.identificativo_avv_pag != null ? d.tab_fascicolo_avvpag_allegati.tab_fascicolo.tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " num. " + d.tab_fascicolo_avvpag_allegati.tab_fascicolo.tab_avv_pag.identificativo_avv_pag : string.Empty,
                identificativo_doc_input = d.tab_fascicolo_avvpag_allegati.tab_fascicolo.tab_doc_input.identificativo_doc_input != null ? d.tab_fascicolo_avvpag_allegati.tab_fascicolo.tab_doc_input.tab_tipo_doc_entrate.descr_doc + " " + d.tab_fascicolo_avvpag_allegati.tab_fascicolo.tab_doc_input.identificativo_doc_input : string.Empty
            }).ToList();
        }
    }
}
