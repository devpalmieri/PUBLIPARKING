using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinDenunceDocInputBD : EntityBD<join_denunce_doc_input>
    {
        public JoinDenunceDocInputBD()
        {

        }

        public static new IQueryable<join_denunce_doc_input> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            /// Ridefinisce la GetList per implementare la sicurezza di accesso sul contribuente
            return GetListInternal(p_dbContext, p_includeEntities).Where(d => p_dbContext.idContribuenteDefaultList.Count == 0 || p_dbContext.idContribuenteDefaultList.Contains(d.id_contribuente));
        }

        public static new join_denunce_doc_input GetById(Int32 p_id, dbEnte p_dbContext)
        {
            /// Ridefinisce la GetById per implementare la sicurezza di accesso sul contribuente
            return GetList(p_dbContext).SingleOrDefault(c => c.id_join_denunce_doc_input == p_id);
        }
    }
}
