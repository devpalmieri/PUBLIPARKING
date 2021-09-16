using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_unita_contribuzione : ISoftDeleted
    {
        public const int VAL_EME_ID = 7;
        public const string VAL_EME = "VAL-EME";

        public static int ANNULLATO_NOTIFICA_ID = 34;
        public static string ANNULLATO_NOTIFICA = "ANN-NOT";

        public static int ANNULLATO_ANNULLATO_ID = 40;
        public static string ANNULLATO_ANNULLATO = "ANN-ANN";

        public static int ANNULLATO_AUTOTUTELA_ID = 22;
        public static string ANNULLATO_AUTOTUTELA = "ANN-AUT";

        public static int ANNULLATO_SGRAVIO_ID = 47;
        public static string ANNULLATO_SGRAVIO = "ANN-SGR";


        public static int VAL_CON_ID = 36;
        public static string VAL_CON = "VAL-CON";


        public static string ANNULLATO = "ANN-";
        public static string VALIDO = "VAL-";
        public static string DAANNULLARE = "DAN-";
        public static string SOSPESO = "SSP-";

        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
