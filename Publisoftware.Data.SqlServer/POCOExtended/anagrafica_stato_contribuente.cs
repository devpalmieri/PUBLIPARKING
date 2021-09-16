using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_stato_contribuente.Metadata))]
    public partial class anagrafica_stato_contribuente : ISoftDeleted
    {
        public const string ATT = "ATT-";
        public const string ANN = "ANN-";

        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";

        public const string ATT_CES = "ATT-CES";
        // 2018-03-28:
        public const int ATT_CES_ID = 105;

        public const string DEC = "DEC-";

        public const int ATT_LIQ_ID = 100;
        public const string ATT_LIQ = "ATT-LIQ";

        public const int ATT_PCO_ID = 106;
        public const string ATT_PCO = "ATT-PCO";

        public const int DEC_DEC_ID = 49;
        public const string DEC_DEC = "DEC-DEC";

        public const int DEC_RIL_ID = 23;
        public const string DEC_RIL = "DEC-RIL";

        public const string NAT = "NAT-";

        public const int NAT_CAN_ID = 5;
        public const string NAT_CAN = "NAT-CAN";

        public const string FAL_FAL = "FAL-FAL";

        public const int ATT_AMS_AMMINISTRAZIONE_STRAORDINARIA_ID = 76;
        public const string ATT_AMS_AMMINISTRAZIONE_STRAORDINARIA = "ATT-AMS";

        public const int ATT_CPR_IN_CONCORDATO_PREVENTIVO_ID = 79;
        public const string ATT_CPR_IN_CONCORDATO_PREVENTIVO = "ATT-CPR";

        public const int ATT_FAL_IN_PROCEDURA_FALLIMENTARE_ID = 103;
        public const string ATT_FAL_IN_PROCEDURA_FALLIMENTARE = "ATT-FAL";

        public const int ATT_LCA_IN_LIQUIDAZIONE_COATTA_AMMINISTRATIVA_ID = 111;
        public const string ATT_LCA_IN_LIQUIDAZIONE_COATTA_AMMINISTRATIVA = "ATT-LCA";

        public const int ATT_COF_IN_CONCORDATO_FALLIMENTARE_ID = 113;
        public const string ATT_COF_IN_CONCORDATO_FALLIMENTARE = "ATT-COF";

        public const int ATT_CSI_ID = 117;
        public const string ATT_CSI = "ATT-CSI";

        public const int ATT_GIU_ID = 119;
        public const string ATT_GIU = "ATT-GIU";

        public const int ACQ_ACQ_ID = 126;
        public const string ACQ_ACQ = "ACQ-ACQ";

        public const string FLAG_VALIDITA_ATTO = "0";
        public const string FLAG_VALIDITA_NO_ATTO = "1";
        public const string FLAG_VALIDITA_PROC_CONC = "2";

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }

        public bool IsValid
        {
            get { return true; }
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string statoContribuenteTotale
        {
            //Il dottore ha voluto usare il cod_stato_riferimento, poi ha voluto riusare cod_stato_contribuente
            get { return cod_stato_contribuente + " - " + des_stato_contribuente; }
            //get { return cod_stato_riferimento + " - " + desc_stato_riferimento; }
        }
    }
}
