using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_sped_not : ISoftDeleted
    {
        public const string NOT_OKK = "NOT-OKK";
        public const int NOT_OKK_ID = 1;

        public const string NOT_NOK = "NOT-NOK";
        public const int NOT_NOK_ID = 2;
        //valido per le pec
        //pec accettata dal gestore ma non ancora consegnata
        //se  consegnata lo stato sara NOT-OKK viceversa NOT-NOK
        public const string NOT_ACC = "NOT-ACC";
        public const int NOT_ACC_ID = 5;

        public const string SPE_OKK = "SPE-OKK";
        public const int SPE_OKK_ID = 3;

        public const string SPE_NOK = "SPE-NOK";
        public const int SPE_NOK_ID = 4;

        public const string NOT_143 = "NOT-143";
        public const int NOT_143_ID = 14;

        public const string RIL_139 = "RIL-139";
        public const int RIL_139_ID = 28;

        public const string RIL_140 = "RIL-140";
        public const int RIL_140_ID = 16;

        public const string RIC_SIA = "RIC-SIA";
        public const int RIC_SIA_ID = 13;

        public const string SPE_PRE = "SPE-PRE";
        public const int SPE_PRE_ID = 17;

        public const string ASS_DEF = "ASS-DEF";
        public const int ASS_DEF_ID = 22;

        public const string ASS_ASS = "ASS-ASS";
        public const int ASS_ASS_ID = 6;

        public const int NOT_SPO_ID = 55;

        public const string VAL_DEF = "VAL-DEF";

        public const string ASS_NRR = "ASS-NRR";

        public const string ANN = "ANN-";

        public const string RIA_NOT = "RIC-NOT";
        public const int RIA_NOT_ID = 8;

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
