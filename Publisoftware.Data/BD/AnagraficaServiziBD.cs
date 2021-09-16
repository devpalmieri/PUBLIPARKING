using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaServiziBD : EntityBD<anagrafica_servizi>
    {
        public AnagraficaServiziBD()
        {

        }

        /// <summary>
        /// Restituisce il servizio corrispondente al codice indicato
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_servizi GetByCodice(string p_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(s => s.cod_servizio.ToUpper() == p_codice.ToUpper());
        }
    }
}
