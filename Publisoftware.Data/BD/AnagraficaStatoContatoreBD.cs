using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoContatoreBD : EntityBD<anagrafica_stato_contatore>
    {
        public AnagraficaStatoContatoreBD()
        {

        }

        /// <summary>
        /// Restituisce lo stato corrispondente al codice
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_stato_contatore GetByCodice(string p_codice, dbEnte p_dbContext)
        { 
            return GetList(p_dbContext).SingleOrDefault(sc => sc.cod_stato.ToUpper() == p_codice.ToUpper());
        }
    }
}
