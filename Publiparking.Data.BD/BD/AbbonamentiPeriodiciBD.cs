using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class AbbonamentiPeriodiciBD : EntityBD<AbbonamentiPeriodici>
    {
        public AbbonamentiPeriodiciBD()
        {

        }

        public static AbbonamentiPeriodici getAbbonamentoByCodice(string p_codice, DbParkCtx p_context)
        {
            return GetList(p_context).Where(a => a.codice.Equals(p_codice)).SingleOrDefault();

        }
    }
}
