using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabListaSpedNotificheLinq
    {
        public static IQueryable<tab_lista_sped_notifiche> WhereByIdEnte(this IQueryable<tab_lista_sped_notifiche> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByTipoAvvPag(this IQueryable<tab_lista_sped_notifiche> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(d => d.id_tipo_avv_pag == p_idTipoAvvPag);
        }
        /// <summary>
        /// Filtro per stato Stampa Creata
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> WhereStatoStampaCreata(this IQueryable<tab_lista_sped_notifiche> p_query)
        {
            return p_query.Where(l => l.cod_stato.Equals(tab_lista_sped_notifiche.DEF_STA));
        }
        public static IQueryable<tab_lista_sped_notifiche> WhereByIdDocEntrata(this IQueryable<tab_lista_sped_notifiche> p_query, int p_idTipoDocEntrata)
        {
            return p_query.Where(d => d.id_tipo_doc_entrata == p_idTipoDocEntrata);
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByNotIdDocEntrataList(this IQueryable<tab_lista_sped_notifiche> p_query, IList<int> p_idTipoDocEntrataList)
        {
            return p_query.Where(d => !d.id_tipo_doc_entrata.HasValue || !p_idTipoDocEntrataList.Contains(d.id_tipo_doc_entrata.Value));
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByTipoSpedNot(this IQueryable<tab_lista_sped_notifiche> p_query, int p_idTipoSpedNot)
        {
            return p_query.Where(d => d.id_tipo_spedizione_notifica == p_idTipoSpedNot);
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByNotSigleTipoSpedizione(this IQueryable<tab_lista_sped_notifiche> p_query, List<string> p_sigleTipoSpedizioneList)
        {
            return p_query.Where(d => !p_sigleTipoSpedizioneList.Contains(d.anagrafica_tipo_spedizione_notifica.sigla_tipo_spedizione_notifica));
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByTipoLista(this IQueryable<tab_lista_sped_notifiche> p_query, string p_codice)
        {
            return p_query.Where(d => d.tab_tipo_lista.cod_lista.Equals(p_codice));
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByAnno(this IQueryable<tab_lista_sped_notifiche> p_query, int p_anno)
        {
            return p_query.Where(d => d.anno == p_anno);
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereByCodStato(this IQueryable<tab_lista_sped_notifiche> p_query, string p_stato)
        {
            return p_query.Where(d => d.cod_stato == p_stato);
        }

        public static IQueryable<tab_lista_sped_notifiche> WhereStatoAperta(this IQueryable<tab_lista_sped_notifiche> p_query)
        {
            return p_query.Where(l => l.cod_stato.Equals(tab_lista_sped_notifiche.SPE_PRE));
        }
        /// <summary>
        /// Filtro per stato Stampa Approvata
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_lista_sped_notifiche> WhereStatoStampaApprovata(this IQueryable<tab_lista_sped_notifiche> p_query)
        {
            return p_query.Where(l => l.cod_stato.Equals(tab_liste.DEF_DEF));
        }
    }
}
