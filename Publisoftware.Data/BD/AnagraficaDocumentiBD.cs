using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class AnagraficaDocumentiBD : EntityBD<anagrafica_documenti>
    {
        public AnagraficaDocumentiBD()
        {

        }

        /// <summary>
        /// Restituisce la lista dei Documenti di uno specifico tipo
        /// </summary>
        /// <param name="p_idTipoDocumento">ID Tipo documento ricercato</param>
        /// <param name="p_dbContext">Context di ricerca</param>
        /// <returns></returns>
        public static IQueryable<anagrafica_documenti> GetList(dbEnte p_dbContext)
        {
            return GetList(p_dbContext, new string[] { "anagrafica_tipo_documento" });
        }
    }
}
