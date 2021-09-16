using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class SMSOutBD : EntityBD<SMSOut>
    {
        public SMSOutBD()
        {

        }

        public static IQueryable<SMSOut> GetListSMSNonElaborati(int p_minuti,  DbParkCtx p_context)
        {
            
            if (p_minuti > 0)
            {       
                DateTime v_datariferimento = DateTime.Now.AddMinutes(-p_minuti);
                return GetList(p_context).Where(s => !s.dataInvio.HasValue)
                                         .Where(s=> s.dataElaborazione.HasValue && s.dataElaborazione.Value > v_datariferimento);
            }
            else
            {
                return GetList(p_context).Where(s => !s.dataInvio.HasValue);
            }
            
        }

        public static IQueryable<SMSOut> GetList(DbParkCtx p_context)
        {
            Configurazione v_conf = ConfigurazioneBD.GetList(p_context).FirstOrDefault();                                  
            if (!string.IsNullOrEmpty(v_conf.numeroMittente))
            {
                return GetListInternal(p_context).Where(s => s.numeroMittente.Contains(v_conf.numeroMittente));
            }
            else
            {
                return GetListInternal(p_context);
            }

        }
    }
}
