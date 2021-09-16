using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoInterventoBD : EntityBD<anagrafica_stato_intervento>
    {
        public AnagraficaStatoInterventoBD()
        {

        }

        /// <summary>
        /// Restituisce lo stato corrispondente al codice
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_stato_intervento GetByCodice(string p_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(si => si.cod_stato.ToUpper() == p_codice.ToUpper());
        }
    }
}
