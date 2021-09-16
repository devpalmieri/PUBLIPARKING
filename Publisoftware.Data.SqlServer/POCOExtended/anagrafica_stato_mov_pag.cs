using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_mov_pag : ISoftDeleted
    {
        public const int ANN_ANN_ID = 52;
        public const string ANN_ANN = "ANN-ANN";

        public const int PPA_ACC_ID = 78;
        public const string PPA_ACC = "PPA-ACC";

        /// <summary>
        /// Annullato
        /// </summary>
        public static string ANNULLATO = "ANN-";

        /// <summary>
        /// Caricato
        /// </summary>
        public static string CARICATO = "CAR-";

        public static string CON_ = "CON-";
        public static string _CON = "-CON";

        /// <summary>
        /// Ricerca Avviso: contribuente trivato ma non l'avviso (quindi non accoppiato)
        /// </summary>
        public static string RAV = "RAV-";

        /// <summary>
        /// Errore Code Line: il bollettino premarcato non accoppiato automaticamente
        /// Stato temporaneo, verrà risolto nell'accoppiamento guidato.
        /// </summary>
        public static string ECL = "ECL-";

        /// <summary>
        /// VErifica Pagante: tab_mov_pag non accoppiato per impossibilità di individuazione del contribuente
        /// </summary>
        public static string VEP = "VEP-";

        public static string MNC = "MNC-";

        public static string ACCOPPIATO = "ACC-";

        public static string ACCOPPIATOGUIDATO = "AGT-";
        public static string AGT_QUA = "AGT-QUA";

        public static string SMARRITO = "SMA-";

        public const int ACC_ACC_ID = 20;
        public static string ACC_ACC = "ACC-ACC";

        public const int STO_QUA_ID = 39;
        public const string STO_QUA = "STO-QUA";

        public const int RAV_CAR_ID = 9;
        public const string RAV_CAR = "RAV-CAR";

        public const int RAV_QUA_ID = 33;
        public const string RAV_QUA = "RAV-QUA";

        public const int RTI_QUA_ID = 66;
        public const string RTI_QUA = "RTI-QUA";

        public const int VEP_CAR_ID = 7;
        public const string VEP_CAR = "VEP-CAR";

        public const int VEP_QUA_ID = 32;
        public const string VEP_QUA = "VEP-QUA";
        //
        public const int ACC_CAR_ID = 3;
        public static string ACC_CAR = "ACC-CAR";

        public const int MNC_QUA_ID = 76;
        public static string MNC_QUA = "MNC-QUA";

        public static string MNC_CAR = "MNC-CAR";

        public const int CAR_QUA_ID = 26;
        public const string CAR_QUA = "CAR-QUA";

        public static int IMU_NON_ACCERTAMENTO_ID = 73;
        public static string IMU_NON_ACCERTAMENTO = "PRE-ICM";

        public const int ANN_CON_ID = 53;
        public static string ANN_CON = "ANN-CON";

        public const int ACC_CON_ID = 48;
        public static string ACC_CON = "ACC-CON";

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
