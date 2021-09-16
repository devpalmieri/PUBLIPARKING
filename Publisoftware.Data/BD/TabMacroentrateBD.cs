using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class TabMacroentrateBD : EntityBD<tab_macroentrate>
    {
        public TabMacroentrateBD()
        {

        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa descrizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_descrizione">Descrizione</param>
        /// <returns></returns>
        public static bool CheckDescrizioneDuplicato(dbEnte p_dbContext, string p_descrizione)
        {
            return GetList(p_dbContext).Any(d => d.descrizione.Equals(p_descrizione));
        }

        /// <summary>
        /// Filtro per lista di macroentrate
        /// </summary>
        /// <param name="p_idListMacroentrata"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_macroentrate> getListByIdListMacroentrata(List<int> p_idListMacroentrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => p_idListMacroentrata.Contains(d.id_tab_macroentrate));
        }

        /// <summary>
        /// Filtro per id ente
        /// </summary>
        /// <param name="p_idEnte"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_macroentrate> getListByIdEnte(int p_idEnte, dbEnte p_dbContext)
        {
            //Il dottore vuole che le macroentrate siano soltanto quelle abilitate per l'ente in questione tramite la tab_voci_contrattuali_NEW
            List<int> v_listIdEntrateFromTabVociContra = TabVociContrattualiNewBD.GetList(p_dbContext).Where(d => d.id_ente == p_idEnte && d.tab_contratti_NEW.data_stipula.HasValue && d.tab_contratti_NEW.data_stipula.Value <= DateTime.Now && (!d.tab_contratti_NEW.data_scadenza.HasValue || d.tab_contratti_NEW.data_scadenza.Value >= DateTime.Now)).Select(d => d.id_entrata.HasValue ? d.id_entrata.Value : 0).Distinct().ToList();
            return JoinEntrateMacroentrateBD.GetList(p_dbContext).Where(d => v_listIdEntrateFromTabVociContra.Contains(d.id_entrata)).OrderBy(d => d.id_entrata).Select(j => j.tab_macroentrate).Distinct();
        }
    }
}