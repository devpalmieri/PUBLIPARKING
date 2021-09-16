using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAutoritaGiudiziariaBD : EntityBD<tab_autorita_giudiziaria>
    {
        public TabAutoritaGiudiziariaBD()
        {

        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa sigla
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_sigla">Sigla</param>
        /// <returns></returns>
        public static bool CheckSiglaDuplicato(dbEnte p_dbContext, string p_codice)
        {
            return GetList(p_dbContext).Any(d => d.sigla_autorita.Equals(p_codice));
        }
    }
}
