using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabProcedureBD : EntityBD<tab_procedure>
    {
        public TabProcedureBD()
        {

        }

        /// <summary>
        /// restituisce l'entità applicazione associata al FullCode
        /// </summary>
        /// <param name="p_fullCode">Codice Procedura ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_procedure GetByFullCode(string p_fullCode, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(pr => pr.codice.Equals(p_fullCode)).SingleOrDefault();
        }
    }
}
