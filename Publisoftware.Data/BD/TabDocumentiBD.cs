using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabDocumentiBD : EntityBD<tab_documenti>
    {
        public TabDocumentiBD()
        {

        }

        /// <summary>
        /// Ottiene tab_documenti dalla lista a partire dall'id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static new tab_documenti GetById(Int32 id, dbEnte dbContext)
        {
            return GetList(dbContext).SingleOrDefault(c => c.id_tab_documenti == id);
        }
    }
}
