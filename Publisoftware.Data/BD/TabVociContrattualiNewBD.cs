using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabVociContrattualiNewBD : EntityBD<tab_voci_contrattuali_NEW>
    {
        public TabVociContrattualiNewBD()
        {

        }

        /// <summary>
        /// Restituisce la liste degli ID Entrata contrattualizzati per l'Ente
        /// </summary>
        /// <param name="p_idEnte">ID Ente di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IList<int> GetListIdEntrateByIdEnte(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(v => v.id_entrata.HasValue && v.id_ente == p_idEnte).Select(v => v.id_entrata.Value).Distinct().OrderBy(o => o).ToList();
        }

        /// <summary>
        /// Restituisce l'elenco di anni gestiti per la coppia Ente-Entrata cercando da p_annoInizio fino all'anno corrente
        /// </summary>
        /// <param name="p_idEnte">Ente di ricerca</param>
        /// <param name="p_idEntrata">Entrata di ricerca</param>
        /// <param name="p_annoInizio">Anno da cui l'elenco deve partire</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IList<int> GetListAnniByIdEnteIdEntrata(int p_idEnte, int p_idEntrata, int p_annoInizio, dbEnte p_dbContext)
        {
            int v_annoCorrente = DateTime.Now.Year;
            IList<int> risp = new List<int>();

            //Range di anni coperti
            var v_listAnni = GetList(p_dbContext).Where(v => v.id_entrata.HasValue && v.id_ente == p_idEnte && v.id_entrata == p_idEntrata)
                                     .Where(w => w.Anno_rif_da <= v_annoCorrente && w.Anno_rif_a >= p_annoInizio)
                                     .Select(v => new { v.Anno_rif_da, v.Anno_rif_a })
                                     .OrderBy(o => o.Anno_rif_da).ThenBy(o => o.Anno_rif_a);

            //Cicla sugli anni da inizio ad oggi
            for (int v_anno = p_annoInizio; v_anno <= v_annoCorrente; v_anno++)
            {
                //Se l'anno rientra in un range di attività contrattuale lo inserisce in lista
                bool v_attivo = v_listAnni.Any(w => w.Anno_rif_da <= v_anno && w.Anno_rif_a >= v_anno);

                if (v_attivo)
                {
                    risp.Add(v_anno);
                }
            }

            return risp;
        }
    }
}
