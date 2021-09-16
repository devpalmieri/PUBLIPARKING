using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_avv_pag : ISoftDeleted
    {
        public const int VAL_EME_ID = 7;
        public const string VAL_EME = "VAL-EME";

        public const int VAL_PCO_ID = 282;
        public const string VAL_PCO = "VAL-PCO";

        /// <summary>
        /// Preavviso di fermo
        /// </summary>
        public const int VAL_PFM_ID = 110;
        public const string VAL_PFM = "VAL-PFM";

        public const int VAL_ING_ID = 39;
        public const string VAL_ING = "VAL-ING";

        public const int VAL_RAT_ID = 25;
        public const string VAL_RAT = "VAL-RAT";

        /// <summary>
        /// (I)scrizione (F)ormalità (D)isposta per id = 11
        /// </summary>
        public const string VAL_IFD = "VAL-IFD"; // (I)scrizione (F)ormalità (D)isposta per id = 11
        public const int VAL_IFD_ID = 300;

        /// <summary>
        /// (C)itazione (T)erzo (D)isposta
        /// </summary>
        public const string VAL_CTD = "VAL-CTD";
        public const int VAL_CTD_ID = 301;

        public const string VAL_PRE = "VAL-PRE";
        public const int VAL_PRE_ID = 276;

        public const string VAL_CON = "VAL-CON";
        public const int VAL_CON_ID = 305;

        /// <summary>
        /// (D)ichiarazione (P)ositiva del (T)erzo
        /// </summary>
        public const string VAL_DPT = "VAL-DPT";
        public const int VAL_DPT_ID = 302;

        public const string VAL_DPV = "VAL-DPV";
        public const int VAL_DPV_ID = 2415;

        public static int ANNULLATO_DNT_ID = 2413;
        public static string ANNULLATO_DNT = "ANN-DNT";

        public static int ANNULLATO_DET_ID = 2416;
        public static string ANNULLATO_DET = "ANN-DET";

        public static int ANNULLATO_DEB_ID = 2417;
        public static string ANNULLATO_DEB = "ANN-DEB";

        public static int ANNULLATO_DIT_ID = 2411;
        public static string ANNULLATO_DIT = "ANN-DIT";

        public static int ANNULLATO_DGT_ID = 2414;
        public static string ANNULLATO_DGT = "ANN-DGT";

        public static int ANNULLATO_DNV_ID = 2418;
        public static string ANNULLATO_DNV = "ANN-DNV";

        public static int ANNULLATO_DIV_ID = 2419;
        public static string ANNULLATO_DIV = "ANN-DIV";

        /// <summary>
        /// (D)ichiarazione (P)arzialmente (P)ositiva del terzo
        /// </summary>
        public const string VAL_DPP = "VAL-DPP";
        public const int VAL_DPP_ID = 304;

        /// <summary>
        /// Fermo Iscritto
        /// </summary>
        public const int VAL_FIS_ID = 170;

        /// <summary>
        /// Fermo Iscritto
        /// </summary>
        public const string VAL_FIS = "VAL-FIS";

        public const string ANN_PAG = "ANN-PAG";
        public static string VALIDO = "VAL-";
        public static string SOSPESO = "SSP-";
        public static string SOSPESO_ISTANZA = "SSP-IST";
        public static string SOSPESO_EMESSO = "SSP-EME";
        public static int SOSPESO_EMESSO_ID = 47;
        public static string SOSPESO_COA = "SSP-COA";
        public static int SOSPESO_COA_ID = 84;
        public static string ANNULLATO = "ANN-";
        public static string RETTIFICATO = "RET-";
        public static string DAANNULLARE = "DAN-";
        public static string DARETTIFICARE = "DAR-";
        public static string SENTENZA = "-SEN";
        public static string LIBERATORIA = "-LIB";
        public static string REVISIONE = "-REV";
        public static string AUTOTUTELA = "-AUT";

        public static int DA_RETTIFICARE_ISTANZA_ID = 45;
        public static string DA_RETTIFICARE_ISTANZA = "DAR-IST";
        
        public static int SOSPESO_RATEIZZAZIONE_ID = 49;
        public static string SOSPESO_RATEIZZAZIONE = "SSP-RAT";

        public static int ANNULLATO_INSOLVENZA_ID = 279;
        public static string ANNULLATO_INSOLVENZA = "ANN-INS";

        public static int ANNULLATO_PRELIMINARE_ID = 280;
        public static string ANNULLATO_PRELIMINARE = "ANN-PRL";

        public static int ANNULLATO_ANNULLATO_ID = 283;
        public static string ANNULLATO_ANNULLATO = "ANN-ANN";

        public static int ANNULLATO_UFFICIO_ID = 30;
        public static string ANNULLATO_UFFICIO = "ANN-UFF";

        public static int CARICATO_CARICATO_ID = 284;
        public static string CARICATO_CARICATO = "CAR-CAR";

        public static int ANNULLATO_AUTOTUTELA_ID = 22;
        public static string ANNULLATO_AUTOTUTELA = "ANN-AUT";

        public static int ANNULLATO_ISTANZA_ACCOLTA_ID = 13;
        public static string ANNULLATO_ISTANZA_ACCOLTA = "ANN-IST";

        public static int ANNULLATO_INTIMAZIONE_SCADUTA_ID = 137;
        public static string ANNULLATO_INTIMAZIONE_SCADUTA = "ANN-ISC";

        public static int ANNULLATO_MANCATA_NOTIFICA_ID = 19;
        public static string ANNULLATO_MANCATA_NOTIFICA = "ANN-NOT";

        public static int ANNULLATO_DECESSO_ID = 67;
        public static string ANNULLATO_DECESSO = "ANN-DEC";

        public static int ANNULLATO_SQUADRATURA_ID = 36;
        public static string ANNULLATO_SQUADRATURA = "ANN-SQU";

        public static int ANNULLATO_RIEMISSIONE_POP_ID = 388;
        public static string ANNULLATO_RIEMISSIONE_POP = "ANN-ROP";

        public static int VALIDO_REVOCA_ID = 306;
        public static string VALIDO_REVOCA = "VAL-REV";

        public static int VALIDO_RET_ID = 76;
        public static string VALIDO_RET = "VAL-RET";

        public static int VALIDO_DICHIARAZIONE_TERZO_ID = 308;
        public static string VALIDO_DICHIARAZIONE_TERZO = "VAL-DTZ";

        public static int VALIDO_DTP_ID = 2398;
        public static string VALIDO_DTP = "VAL-DTP";

        public static int VALIDO_DTN_ID = 2399;
        public static string VALIDO_DTN = "VAL-DTN";

        public static int VALIDO_DTI_ID = 2401;
        public static string VALIDO_DTI = "VAL-DTI";

        public static int VALIDO_DTV_ID = 2403;
        public static string VALIDO_DTV = "VAL-DTV";

        public static int DA_RICALCOLARE_SENTENZA_ID = 2402;
        public static string DA_RICALCOLARE_SENTENZA = "DAR-SEN";

        public static int ANNULLATO_DICHIARAZIONE_TERZO_ID = 2397;
        public static string ANNULLATO_DICHIARAZIONE_TERZO = "ANN-DTZ";

        public static int VALIDO_DA_RINOTIFICARE_ID = 63;
        public static string VALIDO_DA_RINOTIFICARE = "VAL-DAN";

        public static int VALIDO_DA_SPEDIRE_NOTIFICARE_ID = 275;
        public static string VALIDO_DA_SPEDIRE_NOTIFICARE = "VAL-DEF";

        public static int ANNULLATO_PSE_ID = 322;
        public static string ANNULLATO_PSE = "ANN-PSE";

        public static int SOSPESO_ROTTAMATO_ID = 172;
        public static string SOSPESO_ROTTAMATO = "SSP-ROT";

        public static int VALIDO_ROTTAMATO_ID = 173;
        public static string VALIDO_ROTTAMATO = "VAL-ROT";

        public static int VALIDO_COATTIVO_ID = 44;
        public static string VALIDO_COATTIVO = "VAL-COA";

        public static int VALIDO_CON_ADESIONE_ID = 27;
        public static string VALIDO_CON_ADESIONE = "VAL-ADE";

        public static int ANNULLATO_SGRAVIO_ID = 132;
        public static string ANNULLATO_SGRAVIO = "ANN-SGR";

        public static int RETTIFICATO_SGRAVIO_ID = 380;
        public static string RETTIFICATO_SGRAVIO = "RET-SGR";

        public static int DARETTIFICARE_SGRAVIO_ID = 383;
        public static string DARETTIFICARE_SGRAVIO = "DAR-SGR";

        public static int DA_ANNULLARE_SGRAVIO_ID = 2394;
        public static string DA_ANNULLARE_SGRAVIO = "DAN-SGR";

        public static int SOSPESO_UFFICIO_ID = 54;
        public static string SOSPESO_UFFICIO = "SSP-UFF";

        public static int ANNULLATO_ISCRIZIONE_FERMO_FALLITA_ID = 1393;
        public static string ANNULLATO_ISCRIZIONE_FERMO_FALLITA = "ANN-IFF";

        public static int VALIDO_SENTENZA_ID = 2423;
        public static string VALIDO_SENTENZA = "VAL-SEN";

        public static int ANNULLATO_SENTENZA_ID = 2424;
        public static string ANNULLATO_SENTENZA = "ANN-SEN";

        public static int RETTIFICATO_SENTENZA_ID = 2395;
        public static string RETTIFICATO_SENTENZA = "RET-SEN";

        public static int ANNULLATO_RICORSO_ID = 2396;
        public static string ANNULLATO_RICORSO = "ANN-RIC";

        public static int VALIDO_LIB_ID = 2420;
        public static string VALIDO_LIB = "VAL-LIB";

        public static int ANNULLATO_LIB_ID = 2422;
        public static string ANNULLATO_LIB = "ANN-LIB";

        public static int VALIDO_RSG_ID = 2432;
        public static string VALIDO_RSG = "VAL-RSG";

        public static int ANNULLATO_SGP_ID = 2431;
        public static string ANNULLATO_SGP = "ANN-SGP";

        public static int DA_ANNULLARE_AUTOTUTELA_ID = 2435;
        public static string DA_ANNULLARE_AUTOTUTELA = "DAN-AUT";

        public const int VAL_AOP_ID = 65;
        public const string VAL_AOP = "VAL-AOP";

        public const int VAL_POP_ID = 145;
        public const string VAL_POP = "VAL-POP";

        public static int VALIDO_ORDINANZA_ASSEGNAZIONE_ID = 2436;
        public static string VALIDO_ORDINANZA_ASSEGNAZIONE = "VAL-OAS";

        public static int VALIDO_ORDINANZA_ESTINZIONE_ID = 2437;
        public static string VALIDO_ORDINANZA_ESTINZIONE = "VAL-OES";

        public static int VALIDO_ORDINANZA_ESTINZIONE_PAGAMENTO_ID = 2506;
        public static string VALIDO_ORDINANZA_ESTINZIONE_PAGAMENTO = "VAL-OEP";

        public static int SOSPESO_SENTENZA_ID = 2500;
        public static string SOSPESO_SENTENZA = "SSP-SEN";

        public static int SOSPESO_POP_ID = 318;
        public static string SOSPESO_POP = "SSP-POP";

        public static int SOSPESO_PCO_ID = 2472;
        public static string SOSPESO_PCO = "SSP-PCO";

        public static int SOSPESO_RIC_ID = 2448;
        public static string SOSPESO_RIC = "SSP-RIC";

        public static int VERIFICAUFFICIO = 0;
        public static int ISTANZA = 1;
        public static int RICORSO = 2;
        public static int RATEIZZAZIONE = 3;

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
