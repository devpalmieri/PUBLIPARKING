using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public class CodStato
    {
        public const string ANN = "ANN-";
        public const string ATT = "ATT";

        /// <summary>
        /// Contribuente non attivo
        /// </summary>
        public const string NAT = "NAT-";

        //public const string PRE = "PRE";
        public const string RET = "RET-";
        public const string SSP = "SSP-";
        public const string VAL = "VAL-";
        public const string VEP = "VEP-";
        public const string CAR = "CAR";
        public const string CAR_ = "CAR-";
        public const string ECL = "ECL";
        public const string ACC = "ACC";
        public const string EAT = "EAT";
        public const string QUA = "QUA";
        public const string CON = "CON";
        public const string RAV = "RAV";

        public const string AEC = "AEC-";
        public const string ACD = "ACD-";
        public const string VAD = "VAD-";
        public const string AGT = "AGT-";
        public const string COU = "COU-";
        public const string AAV = "AAV-";


        // Pietro: commento ATT_ACQ: non dovrebbe essere "ATT-ACQ" invece di "ATT_ACQ" ?!
        //public const string ATT_ACQ        = "ATT_ACQ";

        public const string ATT_PCO        = "ATT-PCO";
        public const string ATT_LAN        = "ATT-LAN";

        //per stampe
        public const string STATO_ACC = "ACC-ACC";
        public const string STATO_RES = "RES-RES";

        ///// <summary>
        ///// (D)ichiarazione (N)egativa del (T)erzo
        ///// </summary>
        //public const string VAL_DNT = "VAL-DNT";
        //public const int VAL_DNT_ID = 203;

        //public const int VAL_AOP_ID = 65;
        //public const string VAL_AOP = "VAL-AOP";

        //public const int VAL_INP_ID = 117;
        //public const string VAL_INP = "VAL-INP";

        //public const int VAL_COA_ID = 44;
        //public const string VAL_COA = "VAL-COA";


        //public const int SSP_VER_ID        = 129;
        //public const string SSP_VER        = "SSP-VER";
        //public const int SSP_VRT_ID        = 130;
        //public const string SSP_VRT        = "SSP-VRT";
        public const string VAL_VAL        = "VAL-VAL";
        public const string VAL_EME        = "VAL-EME";

        public const string VAL_OLD        = "VAL-OLD";

        public const string VAL_NEG        = "VAL-NEG";

        //public const string PRE_PRE        = "PRE-PRE";
        //public const string PRE_DEF        = "PRE-DEF";
        //public const string DEF_DEF        = "DEF-DEF";
        //public const string DEF_SPE        = "DEF-SPE";
        //public const string DEF_CON        = "DEF-CON";

        public const string ACC_CON        = "ACC-CON";
        public const string ATT_ANN        = "ATT-ANN";
        public const string ATT_ESE        = "ATT-ESE";
        public const string ATT_ERR        = "ATT-ERR";
        public const string ATT_VAR        = "ATT-VAR";
        public const string ANN_CES        = "ANN-CES";
        public const string ANN_ATT        = "ANN-ATT";
        public const string ANN_DEC        = "ANN-DEC";
        //public const string DEF_CAR        = "DEF-CAR";
        public const string ANN_DIS        = "ANN-DIS";
        public const string ANN_DIC = "ANN-DIC";
        public const string ANN_ANN = "ANN-ANN";

        /// <summary>
        /// Annullato Riemesso
        /// </summary>
        public const string ANN_RIE = "ANN-RIE";

        /// <summary>
        /// Attivo PreSchedulato
        /// </summary>
        public const string ATT_PRE        = "ATT-PRE";

        /// <summary>
        /// Attivo Schedulato
        /// </summary>
        public const string ATT_SCH        = "ATT-SCH";

        /// <summary>
        /// Attivo Pianificato
        /// </summary>
        public const string ATT_PIA = "ATT-PIA";

        /// <summary>
        /// Attivo Sospeso
        /// </summary>
        public const string ATT_SPS = "ATT-SPS";

        /// <summary>
        /// Pre-acquisito
        /// </summary>
        public const string PRE_ACQ = "PRE-ACQ";

        /// <summary>
        /// Acquisito
        /// </summary>
        public const string ACQ_ACQ = "ACQ-ACQ";

        /// <summary>
        /// Errore durante l'acquisizione
        /// </summary>
        public const string PRE_ERR = "PRE-ERR";

        // 2017-02-13: aggiunta
        // 2017-02-14: aleminata
        //public const string ATT_ASS = "ATT-ASS";

        public const string ATT_CES = "ATT-CES";

        public const string ATT_ATT = "ATT-ATT";

        public const string PAG_TOT = "PAG-TOT";

        public const string ANN_CON = "ANN-CON";
    }
}
