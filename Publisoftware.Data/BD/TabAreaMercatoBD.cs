using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAreaMercatoBD : EntityBD<tab_area_mercato>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabAreaMercatoBD");
        #endregion Private Members
        public TabAreaMercatoBD()
        {

        }

        public static IQueryable<tab_area_mercato> GetAreeMercatoAttive(dbEnte p_dbContext)
        {
            logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);
            return GetList(p_dbContext).Where(d => d.cod_stato == tab_area_mercato.ATT_ATT);
        }
    }
}
