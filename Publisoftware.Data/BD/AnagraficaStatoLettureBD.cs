using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoLettureBD : EntityBD<anagrafica_stato_letture>
    {
        public AnagraficaStatoLettureBD()
        {

        }

        /// <summary>
        /// Restituisce lo stato corrispondente al codice
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_stato_letture GetByCodice(string p_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(sl => sl.cod_stato.ToUpper() == p_codice.ToUpper());
        }
    }
}
