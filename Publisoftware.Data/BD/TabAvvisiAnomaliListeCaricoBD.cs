using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAvvisiAnomaliListeCaricoBD :EntityBD<tab_avvisi_anomali_liste_carico>
    {
        public TabAvvisiAnomaliListeCaricoBD()
        {

        }

        public static IQueryable<tab_avvisi_anomali_liste_carico> GetListByListaEmissione(int p_id_ente, int p_id_lista, dbEnte p_dbContext)
        {
            return p_dbContext.tab_avvisi_anomali_liste_carico.Where(t => t.id_lista == p_id_lista && t.id_ente == p_id_ente);
        }
    }
}
