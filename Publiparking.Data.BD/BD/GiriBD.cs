using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class GiriBD : EntityBD<Giri>
    {
        public GiriBD()
        {

        }

        public static Giri getGiroByDescrisione(string v_descrizione, DbParkCtx p_context)
        {
            return GetList(p_context).Where(g => g.descrizione.Equals(v_descrizione)).SingleOrDefault();

        }

        public static DateTime getDataUltimaModifica(int v_idGiro, DbParkCtx p_context)
        {
            Giri v_giro = GiriBD.GetById(v_idGiro, p_context);
            return v_giro.dataUltimaModifica;

        }
    }
}
