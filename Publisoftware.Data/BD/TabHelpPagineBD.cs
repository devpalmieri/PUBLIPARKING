using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.BD
{
    public class TabHelpPagineBD : EntityBD<tab_help_pagine>
    {
        #region Costructor
        public TabHelpPagineBD() { }
        #endregion Costructor

        #region Public Methods
        public new static tab_help_pagine GetHelpById(int p_id_help, dbEnte p_ctx, bool active = true)
        {
            return GetList(p_ctx).Where(d => d.IDHelp == p_id_help && d.Active==active).FirstOrDefault();
        }

        public static tab_help_pagine GetHelpByController(dbEnte p_ctx, string p_controller, bool active = true)
        {
            return GetList(p_ctx).Where(d => d.Controller == p_controller && d.Active == active).FirstOrDefault();
        }
        public static tab_help_pagine GetHelpByAction(dbEnte p_ctx, string p_action, bool active = true)
        {
            return GetList(p_ctx).Where(d => d.Action == p_action && d.Active == active).FirstOrDefault();
        }
        public static tab_help_pagine GetHelpByControllerAndAction(dbEnte p_ctx, string p_controller, string  p_action, bool active = true)
        {
            return GetList(p_ctx).Where(d => d.Action == p_action && d.Action==p_action && d.Active == active).FirstOrDefault();
        }
        public static List<tab_help_pagine> GetHelps(dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(d => d.Active == true).ToList();
        }
        #endregion Public Methods
    }
}
