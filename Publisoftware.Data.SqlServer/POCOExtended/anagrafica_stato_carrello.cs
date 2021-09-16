using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_carrello : ISoftDeleted
    {
        public const int ATT_RPT_ID = 1;
        public const string ATT_RPT = "ATT-RPT";

        public const int ANN_ANN_ID = 2;
        public const string ANN_ANN = "ANN-ANN";

        public const int ANN_ERR_ID = 3;
        public const string ANN_ERR = "ANN-ERR";

        public const int ATT_PGT_ID = 4;
        public const string ATT_PGT = "ATT-PAG";

        public const int ATT_PRZ_ID = 5;
        public const string ATT_PRZ = "ATT-PRZ";

        public const int ANN_STO_ID = 6;
        public const string ANN_STO = "ANN-STO";

        public const int ATT_REN_ID = 7;
        public const string ATT_REN = "ATT-REN";

        //Flusso di rendicontazione inserito e scaricato
        public const int ATT_INS_ID = 8;
        public const string ATT_INS = "ATT-INS";

        //Flusso di rendicontazione verificato dal batch
        public const int ATT_VER_ID = 9;
        public const string ATT_VER = "ATT-VER";

        public const int SSP_REN_ID = 11;
        public const string SSP_REN = "SSP-REN";

        public const int ATT_RIC_ID = 12;
        public const string ATT_RIC = "ATT-RIC";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
