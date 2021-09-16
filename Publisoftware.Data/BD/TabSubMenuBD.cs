using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.BD
{
    public class TabSubMenuBD : EntityBD<tab_sub_menu>
    {
        #region Costructor
        public TabSubMenuBD() { }
        #endregion Costructor

        #region Public Methods
        public static List<tab_sub_menu> GetListSubMenu(dbEnte p_ctx)
        {

            List<tab_sub_menu> results = GetList(p_ctx).ToList();
            return results;
        }
        #endregion Public Methods
    }
}
