using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaCicliBD : EntityBD<anagrafica_cicli>
    {
        public AnagraficaCicliBD()
        {

        }

        /// <summary>
        /// Ricerca un ciclo per descrizione ed ente
        /// </summary>
        /// <param name="p_idEnte">ID Ente in cui cercare il ciclo</param>
        /// <param name="p_descrizione">Descrizione ricercata</param>
        /// <param name="p_dbContext">Context da utilizzare</param>
        /// <returns>Record trovato o null</returns>
        public static anagrafica_cicli GetByIdEnteDescrizione(Int32 p_idEnte, string p_descrizione, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(e => e.id_ente == p_idEnte && e.descrizione.ToUpper() == p_descrizione.ToUpper());
        }
    }
}
