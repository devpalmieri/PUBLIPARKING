using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabMovPagBD : EntityBD<tab_mov_pag>
    {
        public TabMovPagBD()
        {

        }

        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_mov_pag> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetListInternal(p_dbContext).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || (d.id_contribuente.HasValue && p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente.Value)));
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_mov_pag GetById(int p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_mov_pag == p_id);
        }
        /// <summary>
        /// Filtro per premarcati
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_mov_pag> GetListPremarcati(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c =>  (c.tipo_boll=="896" || c.tipo_boll == "674" ||  c.id_tipo_pagamento== tab_tipo_pagamento.AccreditoTelematico || c.id_tipo_pagamento == tab_tipo_pagamento.BollettiniDematerializzati || c.id_tipo_pagamento == tab_tipo_pagamento.AccrPostinoTelematico));
        }
        public static async Task<tab_mov_pag> GetMovPagByIuvPagoPAAsync(dbEnte p_dbContext, string p_iuv)
        {
            return await GetList(p_dbContext).Where(c => c.iuv_pagopa.Equals(p_iuv)).FirstOrDefaultAsync();
        }
    }
}
