using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabCcRiscossioneBD : EntityBD<tab_cc_riscossione>
    {
        public TabCcRiscossioneBD()
        {

        }

        /// <summary>
        /// Restituisce la lista dei conti correnti di un ente non chiusi o chiusi dopo la data indicata
        /// </summary>
        /// <param name="p_idEnte">ID Ente</param>
        /// <param name="p_dataChiusura">Data chiusura</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<tab_cc_riscossione> getCCvalidiAl(int p_idEnte, DateTime p_dataChiusura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(cc => cc.id_ente == p_idEnte && (!cc.data_chiusura_cc.HasValue || cc.data_chiusura_cc >= p_dataChiusura));
        }

        /// <summary>
        /// Restituisce il conto corrente del''Ente per l'entrata indicata
        /// </summary>
        /// <param name="p_idEnte">ID Ente</param>
        /// <param name="p_dataChiusura">Data chiusura</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_cc_riscossione getValidoNonGestitoByEntrata(int p_idEnte, DateTime p_dataChiusura, int p_idEntrata, dbEnte p_dbContext)
        {
            tab_cc_riscossione risp = null;

            risp = getCCvalidiAl(p_idEnte, p_dataChiusura, p_dbContext)
                   .Where(c => c.flag_tipo_cc == tab_cc_riscossione.FLAG_TIPO_CC_ENTE && c.id_entrata.HasValue && c.id_entrata.Value == p_idEntrata).SingleOrDefault();

            if (risp == null)
            {
                risp = getCCvalidiAl(p_idEnte, p_dataChiusura, p_dbContext)
                       .Where(c => c.flag_tipo_cc == tab_cc_riscossione.FLAG_TIPO_CC_ENTE && c.id_entrata == null).SingleOrDefault();
            }

            return risp;
        }
    }
}
