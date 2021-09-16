using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabTipoBeneBD : EntityBD<tab_tipo_bene>
    {
        public TabTipoBeneBD()
        {
        }

        public static tab_tipo_bene GetByCodice(string p_codice, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.codice_bene == p_codice);
        }
    }
}
