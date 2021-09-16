using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaVeicoliMarcheBD : EntityBD<anagrafica_veicoli_marche>
    {
        public AnagraficaVeicoliMarcheBD()
        {

        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa descrizione
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_descrizione">Descrizione</param>
        /// <returns></returns>
        public static bool CheckDescrizioneDuplicato(dbEnte p_dbContext, string p_descrizione)
        {
            return GetList(p_dbContext).Any(d => d.descrizione.Equals(p_descrizione));
        }
    }
}
