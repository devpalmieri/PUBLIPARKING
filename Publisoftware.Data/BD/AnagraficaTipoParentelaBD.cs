using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoParentelaBD : EntityBD<anagrafica_tipo_parentela>
    {
        public AnagraficaTipoParentelaBD()
        {

        }

        /// <summary>
        /// Restituisce il tipo di parentela corrispondente al codice
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_tipo_parentela GetByCodice(string p_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(tp => tp.cod_tipo_parentela.ToUpper() == p_codice.ToUpper());
        }
    }
}
