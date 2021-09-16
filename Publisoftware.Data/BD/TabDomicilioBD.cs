using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDomicilioBD : EntityBD<tab_domicilio>
    {
        public TabDomicilioBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_domicilio> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente.Value));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_domicilio GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_domicilio == p_id);
        }

        /// <summary>
        /// Restituisce l'Ultimo domicilio di un contribuente
        /// </summary>
        /// <param name="p_idContribuente">ID Contribuente ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_domicilio GetLastDomicilioByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).Any(d => d.id_anag_contribuente == p_idContribuente))
            {
                return GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente).OrderByDescending(d => d.data_fine ?? DateTime.MaxValue).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Restituisce l'ultimo domicilio attico di un contribuente
        /// </summary>
        /// <param name="p_idContribuente">ID Contribuente ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_domicilio GetLastOpenDomicilioByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).Any(d => d.id_anag_contribuente == p_idContribuente && d.cod_stato.Equals(tab_domicilio.ATT_ATT) && d.data_fine == null))
            {
                return GetList(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente && d.cod_stato.Equals(tab_domicilio.ATT_ATT) && d.data_fine == null).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
