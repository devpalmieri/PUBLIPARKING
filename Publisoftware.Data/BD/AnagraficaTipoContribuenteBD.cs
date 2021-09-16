using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data;

namespace Publisoftware.Data.BD
{
    public class AnagraficaTipoContribuenteBD : EntityBD<anagrafica_tipo_contribuente>
    {
        public AnagraficaTipoContribuenteBD()
        {

        }

        /// <summary>
        /// Ottiene la sigla in funzione dell'id tipo contribuente
        /// </summary>
        /// <param name="p_idTipoContribuente"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static string GetSiglaByIdTipoContribuente(int p_idTipoContribuente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_tipo_contribuente == p_idTipoContribuente).SingleOrDefault().sigla_tipo_contribuente;
        }
    }
}
