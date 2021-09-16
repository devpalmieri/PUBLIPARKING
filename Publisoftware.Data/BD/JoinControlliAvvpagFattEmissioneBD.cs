using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinControlliAvvpagFattEmissioneBD : EntityBD<join_controlli_avvpag_fatt_emissione>
    {
        public JoinControlliAvvpagFattEmissioneBD()
        {

        }

        public static IQueryable<join_controlli_avvpag_fatt_emissione> GetListByListaEmissione(int p_idLista, dbEnte p_dbContext)
        {
            return p_dbContext.join_controlli_avvpag_fatt_emissione.Where(j => j.id_lista == p_idLista);
        }
    }
}
