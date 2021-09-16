using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_doc : ISoftDeleted
    {
        public static int STATO_ATTIVA_ID = 23;
        public static string STATO_ATTIVA = "ATT-ATT";

        public static int STATO_DEFINITIVA_ID = 32;
        public static string STATO_DEFINITIVA = "ATT-DEF";

        public static int STATO_TEMPORANEO_ID = 25;
        public static string STATO_TEMPORANEO = "SSP-ACQ";

        public static int STATO_ATTIVO_ACQUISITO_ID = 33;
        public static string STATO_ATTIVO_ACQUISITO = "ATT-ACQ";

        public static int STATO_ANNULLATO_ID = 11;
        public static string STATO_ANNULLATO = "ANN-ANN";

        public static int STATO_ACQUISITO_ID = 1;
        public static string STATO_ACQUISITO = "ACQ-ACQ";

        public static int STATO_ASSEGNATA_LAVORAZIONE_ID = 19;
        public static string STATO_ASSEGNATA_LAVORAZIONE = "ASS-ASS";

        public static int STATO_DAR_ASS_ID = 26;
        public static string STATO_DAR_ASS = "DAR-ASS";

        public static int STATO_ACCOLTA_ID = 6;
        public static string STATO_ACCOLTA = "ACC-ACC";

        public static int STATO_RESPINTA_ID = 7;
        public static string STATO_RESPINTA = "RES-RES";

        public static int STATO_SOSPESA_VERIFICA_ID = 5;
        public static string STATO_SOSPESA_VERIFICA = "SOS-VER";

        public static int STATO_LAV_ACC_ID = 27;
        public static string STATO_LAV_ACC = "LAV-ACC";

        public static int STATO_LAV_RES_ID = 30;
        public static string STATO_LAV_RES = "LAV-RES";

        //public static int STATO_LAV_PAG_ID = 1043;
        //public static string STATO_LAV_PAG = "LAV-PAG";

        //public static int STATO_LAV_SGR_ID = 42;
        //public static string STATO_LAV_SGR = "LAV-SGR";

        public static int STATO_LAV_SSP_ID = 43;
        public static string STATO_LAV_SSP = "LAV-SSP";

        public static int STATO_VER_ACC_ID = 34;
        public static string STATO_VER_ACC = "VER-ACC";

        public static int STATO_VER_RES_ID = 35;
        public static string STATO_VER_RES = "VER-RES";

        public static int STATO_DEF_ACCOLTA_ID = 38;
        public static string STATO_DEF_ACCOLTA = "DEF-ACC";

        public static int STATO_DEF_RESPINTA_ID = 37;
        public static string STATO_DEF_RESPINTA = "DEF-RES";

        public static int STATO_DEF_PAGAMENTO_ID = 1044;
        public static string STATO_DEF_PAGAMENTO = "DEF-PAG";

        public static int STATO_DEF_SGRAVIO_ID = 36;
        public static string STATO_DEF_SGRAVIO = "DEF-SGR";

        public static int STATO_DEF_SGRAVIO_PARZIALE_ID = 1061;
        public static string STATO_DEF_SGRAVIO_PARZIALE = "DEF-SGP";

        public static int STATO_DEF_PAN_ID = 1045;
        public static string STATO_DEF_PAN = "DEF-PAN";

        public static int STATO_DEF_ENT_ID = 1057;
        public static string STATO_DEF_ENT = "DEF-ENT";

        public static int STATO_DEF_ACQ_ID = 1058;
        public static string STATO_DEF_ACQ = "DEF-ACQ";

        public static int STATO_DEF_MEM_ID = 1063;
        public static string STATO_DEF_MEM = "DEF-MEM";

        public static int STATO_DEF_COG_ID = 1064;
        public static string STATO_DEF_COG = "DEF-COG";

        public static int STATO_DEF_CGS_ID = 1065;
        public static string STATO_DEF_CGS = "DEF-CGS";

        public static int STATO_DEF_DEF_ID = 1066;
        public static string STATO_DEF_DEF = "DEF-DEF";

        //public static int STATO_DEF_ACP_ID = 1059;
        //public static string STATO_DEF_ACP = "DEF-ACP";

        public static int STATO_ESITATA_ACCOLTA_ID = 39;
        public static string STATO_ESITATA_ACCOLTA = "ESI-ACC";

        public static int STATO_ESITATA_RESPINTA_ID = 40;
        public static string STATO_ESITATA_RESPINTA = "ESI-RES";

        public static int STATO_ESITATA_MEDIAZIONE_ID = 1056;
        public static string STATO_ESITATA_MEDIAZIONE = "ESI-MED";

        public static int STATO_ESITATA_NIR_ID = 1060;
        public static string STATO_ESITATA_NIR = "ESI-NIR";

        //public static int STATO_ACQUISITO_MEDIAZIONE_ID = 41;
        //public static string STATO_ACQUISITO_MEDIAZIONE = "ACQ-MED";

        public static int STATO_VAL_DPT_ID = 1046;
        public static string STATO_VAL_DPT = "VAL-DPT";

        public static int STATO_VAL_DPV_ID = 1052;
        public static string STATO_VAL_DPV = "VAL-DPV";

        public static int STATO_ANNULLATO_DNT_ID = 1048;
        public static string STATO_ANNULLATO_DNT = "ANN-DNT";

        public static int STATO_ANNULLATO_DET_ID = 1047;
        public static string STATO_ANNULLATO_DET = "ANN-DET";

        public static int STATO_ANNULLATO_DEB_ID = 1049;
        public static string STATO_ANNULLATO_DEB = "ANN-DEB";

        public static int STATO_ANNULLATO_DIT_ID = 1050;
        public static string STATO_ANNULLATO_DIT = "ANN-DIT";

        public static int STATO_ANNULLATO_DGT_ID = 1053;
        public static string STATO_ANNULLATO_DGT = "ANN-DGT";

        public static int STATO_ANNULLATO_DNV_ID = 1054;
        public static string STATO_ANNULLATO_DNV = "ANN-DNV";

        public static int STATO_ANNULLATO_DIV_ID = 1055;
        public static string STATO_ANNULLATO_DIV = "ANN-DIV";

        public static int STATO_RICORSO_NON_ISCRITTO_ID = 15;
        public static string STATO_RICORSO_NON_ISCRITTO = "RIC-NIR";

        public static int STATO_RICORSO_ESTINTO_ID = 1062;
        public static string STATO_RICORSO_ESTINTO = "EST-EST";

        //public static int STATO_RET_DPT_ID = 1051;
        //public static string STATO_RET_DPT = "RET-DPT";

        public static int STATO_RICORSO_SENTENZA_ACQUISITA_ID = 1067;
        public static string STATO_RICORSO_SENTENZA_ACQUISITA = "DEF-SEN";

        public static int STATO_RICORSO_SENTENZA_ACCOLTA_ID = 1068;
        public static string STATO_RICORSO_SENTENZA_ACCOLTA = "SEN-ACC";

        public static int STATO_RICORSO_SENTENZA_RESPINTA_ID = 1069;
        public static string STATO_RICORSO_SENTENZA_RESPINTA = "SEN-RES";

        public static int STATO_RICORSO_MEDIAZIONE_ACCOLTA_ID = 1070;
        public static string STATO_RICORSO_MEDIAZIONE_ACCOLTA = "MED-ACC";

        public static int STATO_RICORSO_MEDIAZIONE_RESPINTA_ID = 1071;
        public static string STATO_RICORSO_MEDIAZIONE_RESPINTA = "MED-RES";

        public static string STATO_LAVORATO = "LAV-";

        public static string STATO_VERIFICARE = "VER-";

        public static string STATO_DEFINITIVO = "DEF-";

        public static string STATO_ESITATA = "ESI-";

        public static string SENTENZA = "SEN-";

        public static string STATO_RICORSO_SENTENZA = "SEN-";

        public static string STATO_RICORSO_MEDIAZIONE = "MED-";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string statoDoc
        {
            get { return cod_stato + " - " + desc_stato; }
        }
    }
}
