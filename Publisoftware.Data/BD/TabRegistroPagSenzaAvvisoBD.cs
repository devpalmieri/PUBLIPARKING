using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRegistroPagSenzaAvvisoBD : EntityBD<tab_registro_pag_senza_avviso>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabRegistroPagSenzaAvvisoBD");
        #endregion Private Members
        public TabRegistroPagSenzaAvvisoBD()
        {

        }


    }
}
