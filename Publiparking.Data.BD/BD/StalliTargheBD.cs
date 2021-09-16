using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class StalliTargheBD : EntityBD<StalliTarghe>
    {
        public StalliTargheBD()
        {

        }

        public static bool creaStalloTarga(Int32 pIDOperatore, Int32 pIDStallo, string pTarga,DbParkCtx v_contextPark)
        {
            bool retval = false;
            StalliTarghe stalloTarga = new StalliTarghe();
            stalloTarga.idStallo = pIDStallo;
            stalloTarga.idOperatore = pIDOperatore;
            stalloTarga.targa = pTarga;
            stalloTarga.data = DateTime.Now;
            v_contextPark.StalliTarghe.Add(stalloTarga);
            int res = v_contextPark.SaveChanges();
            if (res > 0)
            {
                retval = true;
            }

            return retval;
        }

        public static StalliTarghe getLastByIdStallo(Int32 p_idStallo, DbParkCtx p_context)
        {
            return GetList(p_context).Where(p => p.idStallo == p_idStallo)
                                     .OrderByDescending(p => p.data)
                                     .FirstOrDefault();
        }

    }
}
