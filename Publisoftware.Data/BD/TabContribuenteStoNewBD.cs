using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabContribuenteStoNewBD : EntityBD<tab_contribuente_sto_new>
    {
        public TabContribuenteStoNewBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_contribuente_sto_new> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_anag_contribuente));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_contribuente_sto_new GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_anag_contribuente_sto == p_id);
        }

        public static tab_contribuente_sto_new GetLastContribuenteStoByIdContribuente(Decimal p_idContribuente, dbEnte p_dbContext)
        {
            return GetListInternal(p_dbContext).Where(d => d.id_anag_contribuente == p_idContribuente).OrderByDescending(d => d.id_anag_contribuente_sto).FirstOrDefault();
        }

        /// <summary>
        /// Lista dei records PF Attivi per ente. Quando ci sarà la relazione con anagrafica_tipo_contribuente
        /// "&& c.id_tipo_contribuente==1" dovrà essere sostituito con "&& c.anagrafica_tipo_contribuente.sigla_tipo_contribuente="PF""
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="id_ente"></param>
        /// <returns></returns>
        public static IQueryable<tab_contribuente_sto_new> GetListPfAtt(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.cod_stato.StartsWith(tab_contribuente_sto_new.ATT) && c.id_tipo_contribuente == 1);
        }
    }
}
