using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRegistroGiornalieroRiscossioniBD : EntityBD<tab_registro_giornaliero_riscossioni>
    {
        public TabRegistroGiornalieroRiscossioniBD()
        {     
                  
        }

        /// <summary>
        /// Restituisce l'entità a partire da id_mov_pag
        /// </summary>
        /// <param name="p_idTabMovPag">
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_registro_giornaliero_riscossioni GetByIdMovPag(Int32 p_idMovPag, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_mov_pag == p_idMovPag);
        }
    }
}