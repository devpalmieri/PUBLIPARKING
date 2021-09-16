using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabTipoToponimoBD : EntityBD<tab_tipo_toponimo>
    {
        public TabTipoToponimoBD()
        {

        }

        public static tab_tipo_toponimo GetByIdEnte(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(a => a.id_tipo_toponimo_ente == p_idEnte).SingleOrDefault();
        }
    }
}
