using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAvvPagFattRettificaBD : EntityBD<tab_avv_pag_fatt_rettifica>
    {
        public TabAvvPagFattRettificaBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_avv_pag_fatt_rettifica> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext, p_includeEntities).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_avv_pag_fatt_rettifica GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_anag_contribuente == p_id);
        }
    }
}
