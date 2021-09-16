using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class GiriOperatoreBD : EntityBD<GiriOperatore>
    {
        public GiriOperatoreBD()
        {

        }

        public static IQueryable<GiriOperatore> getListByIdOperatore(int v_idOperatore, DbParkCtx p_context)
        {
            return GetList(p_context).Where(g => g.idOperatore.Equals(v_idOperatore));

        }
    }
}
