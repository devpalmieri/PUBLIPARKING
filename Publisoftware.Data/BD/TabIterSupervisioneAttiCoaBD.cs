using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabIterSupervisioneAttiCoaBD : EntityBD<tab_iter_supervisione_atti_coa>
    {
        public TabIterSupervisioneAttiCoaBD()
        {

        }

        /// <summary>
        /// Filtro per totale morosità
        /// </summary>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_iter_supervisione_atti_coa> GetListByFasce(decimal? p_totaleMorosita, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(c => c.fascia_imp_minimo.Value <= p_totaleMorosita && c.fascia_imp_massimo.Value >= p_totaleMorosita && c.data_inizio_validita.Value <= DateTime.Now && c.data_fine_validita.Value >= DateTime.Now && c.cod_stato == tab_iter_supervisione_atti_coa.ATT_ATT);
        }

    }
}
