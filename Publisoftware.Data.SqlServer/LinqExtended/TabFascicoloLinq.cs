using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabFascicoloLinq
    {
        public static IQueryable<tab_fascicolo> WhereByIdTabDocInput(this IQueryable<tab_fascicolo> p_query, int p_idTabDocInput)
        {
            return p_query.Where(d => d.id_doc_input == p_idTabDocInput);
        }

        public static IQueryable<tab_fascicolo> WhereByIdAvvPag(this IQueryable<tab_fascicolo> p_query, int p_idAvvPag)
        {
            return p_query.Where(d => d.id_avv_pag == p_idAvvPag);
        }

        public static IQueryable<tab_fascicolo> WhereByEnte(this IQueryable<tab_fascicolo> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_fascicolo> WhereByContribuente(this IQueryable<tab_fascicolo> p_query, decimal p_idContribuente)
        {
            return p_query.Where(d => d.id_contribuente == p_idContribuente);
        }

        public static IQueryable<tab_fascicolo> WhereByTabDocEntrate(this IQueryable<tab_fascicolo> p_query, int p_idTabDocEntrate)
        {
            return p_query.Where(d => d.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc_entrate == p_idTabDocEntrate);
        }

        public static IQueryable<tab_fascicolo> WhereByIdFascicolo(this IQueryable<tab_fascicolo> p_query, int p_idFascicolo)
        {
            return p_query.Where(d => d.id_fascicolo == p_idFascicolo);
        }

        public static IQueryable<tab_fascicolo> WhereByIdStatoNot(this IQueryable<tab_fascicolo> p_query, int p_idStato)
        {
            return p_query.Where(d => d.id_stato != p_idStato);
        }

        public static IQueryable<tab_fascicolo> OrderByDefault(this IQueryable<tab_fascicolo> p_query)
        {
            return p_query.OrderByDescending(d => d.data_scadenza).ThenBy(d => d.tab_avv_pag.identificativo_avv_pag);
        }

        public static IQueryable<tab_fascicolo> WhereByEnteStrutturaTipoAvvPag(this IQueryable<tab_fascicolo> p_query, int p_idEnteAppartenenza/*, int p_idEnte, int p_idStruttura*/)
        {
            if (p_idEnteAppartenenza != anagrafica_ente.ID_ENTE_PUBLISERVIZI)
            {
                return p_query.Where(d => d.tab_fascicolo_avvpag_allegati.Count == 0 || d.tab_fascicolo_avvpag_allegati.Any(x => x.tab_avv_pag.id_ente == x.tab_avv_pag.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza
                                                                                                                                 /*&& x.tab_avv_pag.tab_liste.id_struttura_approvazione == p_idStruttura*/));
            }
            else
            {
                return p_query/*.Where(d => d.tab_fascicolo_avvpag_allegati.Count >= 0 || d.tab_fascicolo_avvpag_allegati.Any(x => x.tab_avv_pag.id_ente != x.tab_avv_pag.tab_liste.anagrafica_strutture_aziendali1.id_ente_appartenenza))*/;
            }
        }
    }
}
