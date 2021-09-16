using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabListeLinq
    {
        public static IQueryable<tab_liste> WhereByIdEntrata(this IQueryable<tab_liste> p_query, int p_idEntrata)
        {
            return p_query.Where(ac => ac.id_entrata == p_idEntrata);
        }

        public static IQueryable<tab_liste> WhereByIdEntrataCollegata(this IQueryable<tab_liste> p_query, int p_idEntrata)
        {
            return p_query.Where(ac => ac.anagrafica_tipo_avv_pag.id_entrata_avvpag_collegati == p_idEntrata);
        }

        public static IQueryable<tab_liste> WhereByIdStrutturaCreazione(this IQueryable<tab_liste> p_query, int p_idStruttura)
        {
            return p_query.Where(ac => ac.id_struttura_creazione == p_idStruttura);
        }

        public static IQueryable<tab_liste> WhereByIdStrutturaApprovazione(this IQueryable<tab_liste> p_query, int p_idStruttura)
        {
            return p_query.Where(ac => ac.id_struttura_approvazione == p_idStruttura);
        }

        public static IQueryable<tab_liste> WhereIdTipoLista(this IQueryable<tab_liste> p_query, string p_codLista)
        {
            return p_query.Where(ac => ac.tab_tipo_lista.cod_lista.Equals(p_codLista));
        }

        public static IQueryable<tab_liste> WhereByIdentificativoEnte(this IQueryable<tab_liste> p_query, string p_identificativoEnte)
        {
            return p_query.Where(ac => ac.identificativo_lista_ente.Equals(p_identificativoEnte));
        }

        public static IQueryable<tab_liste> WhereByIdEnte(this IQueryable<tab_liste> p_query, int p_idEnte)
        {
            return p_query.Where(ac => ac.id_ente == p_idEnte);
        }

        public static IQueryable<tab_liste> WhereIdEnteOrGeneric(this IQueryable<tab_liste> p_query, int p_idEnte)
        {
            return p_query.Where(c => c.id_ente == p_idEnte || c.id_ente == anagrafica_ente.ID_ENTE_GENERICO);
        }

        /// <summary>
        /// Filtro per stato PRE_PRE o ANN
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_liste> WhereStatoPREPREorANN(this IQueryable<tab_liste> p_query)
        {
            return p_query.Where(l => l.cod_stato.Equals(tab_liste.PRE_PRE) || l.cod_stato.StartsWith(tab_liste.ANN));
        }

        /// <summary>
        /// Filtro per stato Stampa Approvata
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_liste> WhereStatoStampaApprovata(this IQueryable<tab_liste> p_query)
        {
            return p_query.Where(l => l.cod_stato.Equals(tab_liste.DEF_DEF));
        }

        /// <summary>
        /// Filtro per stato Stampa Creata
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_liste> WhereStatoStampaCreata(this IQueryable<tab_liste> p_query)
        {
            return p_query.Where(l => l.cod_stato.Equals(tab_liste.DEF_STA));
        }

        public static IQueryable<tab_liste> WhereByIdTipoLista(this IQueryable<tab_liste> p_query, int p_idTipoLista)
        {
            return p_query.Where(d => d.id_tipo_lista == p_idTipoLista);
        }

        public static IQueryable<tab_liste> WhereByCodStato(this IQueryable<tab_liste> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_liste> WhereByCodStatoNot(this IQueryable<tab_liste> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_liste> WhereByCodStatoList(this IQueryable<tab_liste> p_query, List<string> p_codStatoList)
        {
            return p_query.Where(d => p_codStatoList.Contains(d.cod_stato));
        }

        public static IQueryable<tab_liste> WhereTipoAvvPagComposto(this IQueryable<tab_liste> p_query)
        {
            return p_query.Where(l => l.anagrafica_tipo_avv_pag.flag_tipo_composto == "1");
        }

        public static IQueryable<tab_liste> WhereByIdTipoAvvPag(this IQueryable<tab_liste> p_query, int p_idTipoAvvPag)
        {
            return p_query.Where(ac => ac.id_tipo_avv_pag == p_idTipoAvvPag);
        }

        public static IQueryable<tab_liste> WhereByIdServizio(this IQueryable<tab_liste> p_query, int p_idServizio)
        {
            return p_query.Where(d => d.anagrafica_tipo_avv_pag.id_servizio == p_idServizio);
        }

        public static IQueryable<tab_liste> WhereByAnnoRif(this IQueryable<tab_liste> p_query, int p_anno)
        {
            return p_query.Where(d => d.anno_rif == p_anno.ToString());
        }

        public static IQueryable<tab_liste> WhereByAnno(this IQueryable<tab_liste> p_query, int p_anno)
        {
            return p_query.Where(d => d.data_lista.Year == p_anno);
        }

        public static IQueryable<tab_liste> WhereByIdLista(this IQueryable<tab_liste> p_query, int p_id)
        {
            return p_query.Where(d => d.id_lista == p_id);
        }

        public static IQueryable<tab_liste> WhereTipoAvvPagNonComposto(this IQueryable<tab_liste> p_query)
        {
            return p_query.Where(l => string.IsNullOrEmpty(l.anagrafica_tipo_avv_pag.flag_tipo_composto) || l.anagrafica_tipo_avv_pag.flag_tipo_composto == "0");
        }

        public static IQueryable<tab_liste> diTipo(this IQueryable<tab_liste> iniziale, int tipo)
        {
            return iniziale.Where(l => l.id_tipo_lista == tipo);
        }

        public static IQueryable<tab_liste> WhereByListIdServizio(this IQueryable<tab_liste> p_query, List<int> v_listIdServizio)
        {
            return p_query.Where(e => v_listIdServizio.Contains(e.anagrafica_tipo_avv_pag.id_servizio));
        }

        public static IQueryable<tab_liste> WhereByFlagTipoLista(this IQueryable<tab_liste> p_query, string p_flag)
        {
            return p_query.Where(l => l.tab_tipo_lista.flag_tipo_lista == p_flag);
        }

        public static IQueryable<tab_liste> OrderByDefault(this IQueryable<tab_liste> p_query)
        {
            return p_query.OrderBy(d => d.id_lista);
        }

        public static IQueryable<tab_liste> OrderByDefaultDesc(this IQueryable<tab_liste> p_query)
        {
            return p_query.OrderByDescending(d => d.id_lista);
        }

        public static IList<tab_liste_light> ToLight(this IQueryable<tab_liste> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_liste_light
            {
                id_lista = d.id_lista,
                numero_lista = d.numero_lista,
                dt_approvazione_lista_String = d.dt_approvazione_lista_String,
                determina_approvazione = d.numero_determina_approvazione,
                identificativo_lista = d.identificativo_lista,
                stato = d.cod_stato_Descrizione,
                anno_riferimento_lista = d.anno_rif,
                descr_lista = d.descr_lista,
                num_avvpag = d.num_avvpag,
                data_lista_String = d.data_lista_String
            }).ToList();
        }
    }
}
