using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class CausaliBD : EntityBD<Causali>
    {
        public CausaliBD()
        {

        }

        public static IQueryable<Causali> getListAttive(DbParkCtx p_context)
        {
            return GetList(p_context).Where(c => c.attivo == true);
        }
        public static IQueryable<Causali> getListAttiveByDescrTipoVerbale(string pDescTipoVerbale,DbParkCtx p_context)
        {
            return getListAttive(p_context)
                .Where(c => c.TipiVerbale.Any(tv => tv.descrizione.Equals(pDescTipoVerbale)));
        }
    }
}
