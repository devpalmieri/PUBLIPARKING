using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_rateizzazioni : ISoftDeleted
    {
        public const int PERIODICITA_CONSENTITA_MENSILE = 1;
        public const string DESC_PERIODICITA_CONSENTITA_MENSILE = "Mensile";

        public const string COD_TIPO_RATEIZZAZIONE_NORMAL = "NORMAL";
        public const string DESC_TIPO_RATEIZZAZIONE_NORMAL = "Rateizzazione mensile a rata costante";

        [DisplayName("Data inizio validita rateizzazione")]
        public string data_inizio_validita_rateizzazione_String
        {
            get
            {
                return data_inizio_validita_rateizzazione!=null ? data_inizio_validita_rateizzazione.Value.ToShortDateString() : string.Empty;
            }
            set
            {
                DateTime dt;
                string strDt = data_inizio_validita_rateizzazione != null ? data_inizio_validita_rateizzazione.Value.ToShortDateString() : string.Empty;
                if (DateTime.TryParse(strDt, out dt))
                {
                    data_inizio_validita_rateizzazione = dt;
                }
                else
                {
                    data_inizio_validita_rateizzazione = null;
                }
            }
        }

        [DisplayName("Data fine validita rateizzazione")]
        public string data_fine_validita_rateizzazione_String
        {
            get
            {
                return data_fine_validita_rateizzazione != null ? data_fine_validita_rateizzazione.Value.ToShortDateString() : string.Empty;
            }
            set
            {
                DateTime dt;
                string strDt = data_fine_validita_rateizzazione != null ? data_fine_validita_rateizzazione.Value.ToShortDateString() : string.Empty;
                if (DateTime.TryParse(strDt, out dt))
                {
                    data_fine_validita_rateizzazione = dt;
                }
                else
                {
                    data_fine_validita_rateizzazione = null;
                }
            }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
