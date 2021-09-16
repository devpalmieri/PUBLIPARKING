using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaEnteCodiceBD : EntityBD<anagrafica_ente_codici>
    {
        public AnagraficaEnteCodiceBD()
        {

        }

        /// <summary>
        /// Ricerca un record corrispondente a Codice o ID Ente
        /// </summary>
        /// <param name="p_codice"></param>
        /// <param name="p_idEnte"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_ente_codici> GetByCodiceOrEnte(string p_codice, int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(ce => ce.codice_ente.ToUpper() == p_codice.ToUpper() || ce.id_ente == p_idEnte);
        }
    }
}
