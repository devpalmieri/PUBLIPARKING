using Publisoftware.Utility.StringHandlling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD.Helper
{
    [Obsolete("Usare Publisoftware.Utilities.CodiceFiscaleToDatiNascita", false)]
    public class CFToDatiNascita : Publisoftware.Utilities.CodiceFiscaleToDatiNascita
    {
        public CFToDatiNascita(string cf, Func<string, string> belfioreNascitaTransformer, bool allowWrongWithDashes) : base(cf, belfioreNascitaTransformer, allowWrongWithDashes)
        {

        }
    }
}

