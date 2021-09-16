using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public partial class TabProgressiviFormalitaAciBD : EntityBD<tab_progressivi_formalita_aci>
    {
        public TabProgressiviFormalitaAciBD()
        {

        }
        /// <summary>
        /// Ultimo Progressivo utilizzato
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static int GetMaxProgressivoUtilizzato(dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).Where(c => c.cod_stato.Equals(CodStato.VAL_VAL)).Max(o => o.num_pratica_iscrizione_fermo).Value != null)
            {
                return GetList(p_dbContext).Where(c => c.cod_stato.Equals(CodStato.VAL_VAL)).Max(o => o.num_pratica_iscrizione_fermo).Value;
            }
            else
            {
                return 0;
            }            
        }
    }
}
