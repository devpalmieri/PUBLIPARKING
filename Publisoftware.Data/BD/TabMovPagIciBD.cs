using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabMovPagIciBD : EntityBD<tab_mov_pag_ici>
    {
        public TabMovPagIciBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_mov_pag_ici> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || (d.id_contribuente != null && p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente.Value)));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_mov_pag_ici GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_mov_pag == p_id);
        }
    }
}
