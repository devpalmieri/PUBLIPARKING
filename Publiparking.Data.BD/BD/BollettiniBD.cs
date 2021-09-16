using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class BollettiniBD : EntityBD<Bollettini>
    {
        public BollettiniBD()
        {

        }

        public static bool isValidCodiceBollettino(Int32 pCodiceBollettino, Int32 p_IdOperatore, string p_serie,DbParkCtx p_context)
        {
            Verbali v_verbale = VerbaliBD.loadByCodiceBollettino(pCodiceBollettino.ToString(), p_serie, p_serie, p_context);

            if (v_verbale == null)
                return existByIdAndIdOperatore(pCodiceBollettino, p_IdOperatore, p_context);
            else
                return false;
        }
        public static bool existByIdAndIdOperatore(Int32 pidBollettino, Int32 p_IdOperatore, DbParkCtx p_context)
        {
            return GetList(p_context).Where(b => b.idBollettino.Equals(pidBollettino))
                                     .Where(b => b.idOperatore.HasValue && b.idOperatore.Value.Equals(p_IdOperatore))
                                     .Any() ? true : false;
        }

    }
}
