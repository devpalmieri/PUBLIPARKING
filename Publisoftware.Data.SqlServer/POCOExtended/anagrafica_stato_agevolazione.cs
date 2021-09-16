using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_agevolazione : ISoftDeleted
    {
        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";

        public const int ATT_CES_ID = 2;
        public const string ATT_CES = "ATT-CES";

        public const int ANN_ANN_ID = 3;
        public const string ANN_ANN = "ANN-ANN";

        public const string ANN = "ANN-";

        public const int SSP_ATT_ID = 11;
        public const string SSP_ATT = "SSP-ATT";

        public const int SSP_CES_ID = 19;
        public const string SSP_CES = "SSP-CES";

        [Obsolete("Usare SSP_IST_ID")]
        public const int SSP_ISt_ID = 6;
        public const int SSP_IST_ID = 6;
        public const string SSP_IST = "SSP-IST";

        public const int    ANN_CES_ID = 20;
        public const string ANN_CES = "ANN-CES";


        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
