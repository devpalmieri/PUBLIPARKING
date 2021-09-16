using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaCodStatoBaseBD : EntityBD<anagrafica_cod_stato_base>
    {
        public AnagraficaCodStatoBaseBD()
        {

        }

        public static string GetStatoNewPrefisso(string p_prefisso, string p_statoOld)
        {
            return p_prefisso + p_statoOld.Substring(4);
        }

        public static string GetStatoNewSuffisso(string p_statoOld, string p_suffisso)
        {
            return p_statoOld.Substring(0, 3) + p_suffisso;
        }
    }
}
