using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.BD.Exceptions;
using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility.Log;

namespace Publisoftware.Data.BD
{
    public class TabTassonomiaPagoPaBD : EntityBD<tab_tassonomia_pagopa>
    {
        private const string FLAG_CUR_FALLIMENTARE = "F";
        private const string FLAG_EREDI = "E";
        private const string FLAG_TRZ_DEB = "T";
        private const string FLAG_SOGG_DEB = "D";

        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("TabTassonomiaPagoPaBD");

        public TabTassonomiaPagoPaBD()
        {

        }
            
        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_tassonomia_pagopa> GetList(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            /// Ridefinisce la GetList per implementare la sicurezza di accesso sul contribuente
            return GetListInternal(p_dbContext, p_includeEntities);
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_tassonomia_pagopa GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.Id_tassonomia == p_id);
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static tab_tassonomia_pagopa GetByIdTipoEnteAndIdEntrata(Int32 p_id_tipo_ente,Int32 p_id_entrata, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.Id_entrata == p_id_entrata && c.Id_tipo_ente==p_id_tipo_ente);
        }

    }
}
