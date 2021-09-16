using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabBollPagBD : EntityBD<tab_boll_pag>
    {
        public TabBollPagBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_boll_pag> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_boll_pag GetById(Int32 p_id, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(c => c.id_tab_boll_pag == p_id);
        }

        public static tab_boll_pag creaBollPagPark(string p_numeroConto, decimal p_idContribuente, tab_avv_pag p_tab_avv_pag, string p_codeLine, string p_checkDigit, tab_rata_avv_pag p_rata, decimal p_importo, DateTime p_emissioneVerbale, Int32 p_idStrutturaStato, Int32 p_idRisorsa, dbEnte p_context)
        {
            tab_boll_pag v_tabBollPag = p_context.tab_boll_pag.Create();

            v_tabBollPag.num_cc = p_numeroConto;
            v_tabBollPag.id_contribuente = p_idContribuente;            
            v_tabBollPag.tab_avv_pag = p_tab_avv_pag;
            v_tabBollPag.code_line = p_codeLine.PadLeft(16, '0');
            v_tabBollPag.ceck_digit = p_checkDigit;
            v_tabBollPag.tab_rata_avv_pag = p_rata;
            v_tabBollPag.importo_boll_pag = p_importo;
            v_tabBollPag.cod_stato = tab_boll_pag.ATT_ATT;
            v_tabBollPag.id_stato = tab_boll_pag.ATT_ATT_ID;
            v_tabBollPag.data_stato = p_emissioneVerbale;
            v_tabBollPag.id_struttura_stato = p_idStrutturaStato;
            v_tabBollPag.id_risorsa_stato = p_idRisorsa;
            p_context.tab_boll_pag.Add(v_tabBollPag);
          
            return v_tabBollPag;
        }

    }
}
