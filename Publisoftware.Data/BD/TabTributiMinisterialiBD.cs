using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;
using Publisoftware.Data.LinqExtended;

namespace Publisoftware.Data.BD
{
    public class TabTributiMinisterialiBD : EntityBD<tab_tributi_ministeriali>
    {
        public TabTributiMinisterialiBD()
        {

        }
        
        /// <summary>
        /// Restituisce il tributo ministeriale per codice
        /// </summary>    
        /// <param name="p_codiceTributo"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_tributi_ministeriali GetByCodiceTributo(int p_idEnte, string p_codiceTributo, dbEnte p_dbContext)
        {
            tab_tributi_ministeriali risp;

            risp = GetList(p_dbContext).Where(a => a.codice_tributo == p_codiceTributo && a.id_ente == p_idEnte).SingleOrDefault();

            if (risp == null)
            {
                risp = GetList(p_dbContext).Where(a => a.codice_tributo == p_codiceTributo && a.id_ente == null).SingleOrDefault();
            }

            return risp;
        }
    }
}
