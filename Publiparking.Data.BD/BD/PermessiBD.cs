using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Publiparking.Data.BD
{
    public class PermessiBD : EntityBD<Permessi>
    {
        public PermessiBD()
        {

        }
        public static Permessi getPermessoByCodiceAndCodComune(string pCodicePermesso,Int32 pcodcomune, DbParkCtx pcontext)
        {
            return GetList(pcontext).Where(p => p.codice.Equals(pCodicePermesso))
                                    .Where(p => p.idComune == pcodcomune)
                                    .OrderByDescending(p => p.idPermesso)
                                    .FirstOrDefault();
                          
        }
    }
}
