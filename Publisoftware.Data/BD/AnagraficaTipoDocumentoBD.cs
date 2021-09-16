using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoDocumentoBD : EntityBD<anagrafica_tipo_documento>
    {
        public AnagraficaTipoDocumentoBD()
        {

        }

        /// <summary>
        /// Controllo se è presente un altro elemento con lo stesso codice
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_codice">Codice</param>
        /// <returns></returns>
        public static bool CheckCodiceDuplicato(dbEnte p_dbContext, string p_codice)
        {
            return GetList(p_dbContext).Any(d => d.codice.Equals(p_codice));
        }

        /// <summary>
        /// Controllo se è presente un altro elemento con la stessa sigla
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <param name="p_sigla">Sigla</param>
        /// <returns></returns>
        public static bool CheckSiglaDuplicato(dbEnte p_dbContext, string p_sigla)
        {
            return GetList(p_dbContext).Any(d => d.sigla.Equals(p_sigla));
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
