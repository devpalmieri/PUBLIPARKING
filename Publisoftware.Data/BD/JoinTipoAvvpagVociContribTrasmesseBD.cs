using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;
using Publisoftware.Utility.Log;

namespace Publisoftware.Data.BD
{
    public class JoinTipoAvvpagVociContribTrasmesseBD : EntityBD<join_tipo_avvpag_voci_contrib_trasmesse>
    {
        private static ILogger m_logger = LoggerFactory.getInstance().getLogger<NLogger>("JoinTipoAvvpagVociContribTrasmesseBD");

        public JoinTipoAvvpagVociContribTrasmesseBD()
        {

        }
    }
}
