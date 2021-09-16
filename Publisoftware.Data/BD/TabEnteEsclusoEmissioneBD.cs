using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabEnteEsclusoEmissioneBD : EntityBD<tab_ente_escluso_emissione>
    {
        public TabEnteEsclusoEmissioneBD()
        {

        }

        public static IQueryable<tab_ente_escluso_emissione> GetListEntiEsclusi(dbEnte p_dbContext, DateTime v_dataRiferimento)
        {
            return GetList(p_dbContext)
                       .Where(c => c.periodo_validita_da <= v_dataRiferimento)
                       .Where(c => c.periodo_validita_a > v_dataRiferimento);            
        }
    }
}
