using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoLetturaBD : EntityBD<anagrafica_tipo_lettura>
    {
        public AnagraficaTipoLetturaBD()
        {

        }

        /// <summary>
        /// Restituisce il tipo lettura corrispondente al codice
        /// </summary>
        /// <param name="p_codice">Codice ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static anagrafica_tipo_lettura GetByCodice(string p_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(sl => sl.cod_tipo_lettura.ToUpper() == p_codice.ToUpper());
        }
    }
}
