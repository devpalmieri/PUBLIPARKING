using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TabTipoEnteBD : EntityBD<tab_tipo_ente>
    {
        public TabTipoEnteBD()
        {

        }
        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static IQueryable<tab_tipo_ente> GetList(DbParkContext p_dbContext)
        {
            return p_dbContext.tab_tipo_ente.OrderBy(o => o.desc_tipo_ente);
        }
    }
}
