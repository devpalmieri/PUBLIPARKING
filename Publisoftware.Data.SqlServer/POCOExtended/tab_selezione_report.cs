using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_selezione_report:ISoftDeleted, IGestioneStato
    {
        public const string REPORT_INFORMATIVO = "INF";
        public const string REPORT_INGIUNZIONI = "ING";
        public const string REPORT_INPREMESSA = "PRE";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
        }
    }
}
