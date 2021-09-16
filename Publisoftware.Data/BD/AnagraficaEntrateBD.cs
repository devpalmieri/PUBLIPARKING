using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaEntrateBD : EntityBD<anagrafica_entrate>
    {
        public AnagraficaEntrateBD()
        {

        }

        /// <summary>
        /// Ricerca un'entrata per Codice
        /// </summary>
        /// <param name="p_codEntrata">Codice Ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_entrate GetByCodice(string p_codEntrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(e => e.codice_entrata.ToUpper() == p_codEntrata.ToUpper());
        }

        /// <summary>
        /// Elenco di tutte le Entrate prese dall'anagrafica del tipo avviso appartenenti all'ID Macroentrata indicata
        /// </summary>
        /// <param name="p_idMacroentrata">ID Macroentrata</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_entrate> getListByIdMacroentrata(int p_idMacroentrata, int p_idEnte, dbEnte p_dbContext)
        {
            //Il dottore vuole che le entrate siano soltanto quelle presenti in anagrafica_tipo_avv_pag ed abilitate per l'ente in questione tramite la tab_voci_contrattuali_NEW
            List<int> v_listIdEntrateFromAnagTipoAvvPag = AnagraficaTipoAvvPagBD.GetList(p_dbContext).Select(d => d.id_entrata).Distinct().ToList();
            List<int> v_listIdEntrateFromTabVociContra = TabVociContrattualiNewBD.GetList(p_dbContext).Where(d => d.id_ente == p_idEnte).Select(d => d.id_entrata.HasValue ? d.id_entrata.Value : 0).Distinct().ToList();
            return JoinEntrateMacroentrateBD.GetList(p_dbContext).Where(d => d.id_tab_macroentrate == p_idMacroentrata && v_listIdEntrateFromAnagTipoAvvPag.Contains(d.id_entrata) && v_listIdEntrateFromTabVociContra.Contains(d.id_entrata)).OrderBy(d => d.id_entrata).Select(j => j.anagrafica_entrate).Distinct();
        }
    }
}
