using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class TipiVerbaleBD : EntityBD<TipiVerbale>
    {
        public TipiVerbaleBD()
        {

        }

        public static TipiVerbale getDefault(DbParkCtx p_context)
        {           
                return GetList(p_context).Where(p => p.isDefault.Value)
                                        .OrderByDescending(p=> p.idTipoVerbale)
                                        .FirstOrDefault();  
        }

    }
}
