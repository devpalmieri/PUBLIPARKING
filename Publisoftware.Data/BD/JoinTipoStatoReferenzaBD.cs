using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinTipoStatoReferenzaBD : EntityBD<join_tipo_stato_referenza>
    {
        public JoinTipoStatoReferenzaBD()
        {

        }

        /// <summary>
        /// Restituisce il rec x PF e DEC-
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static join_tipo_stato_referenza GetRowPFeDec(dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.anagrafica_stato_contribuente.cod_stato_contribuente.StartsWith(anagrafica_stato_contribuente.DEC) && d.anagrafica_tipo_contribuente.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA).FirstOrDefault();
        }
    }
}
