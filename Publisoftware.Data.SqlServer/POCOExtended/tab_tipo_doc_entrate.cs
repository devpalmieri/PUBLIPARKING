using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_tipo_doc_entrate : ISoftDeleted
    {
        //Tipo_Doc
        public const int TIPO_DOC_PROVVEDIMENTO_CANCELLAZIONE_ISCRIZIONE_FERMO = 0;

        public const int TIPO_DOC_ANN_RET = 1;
        public const int TIPO_DOC_RIMBORSO = 3;
        public const int TIPO_DOC_RATEIZZAZIONE = 4;
        public const int TIPO_DOC_REVOCA_FERMO = 5;
        public const int TIPO_DOC_SOSPENSIONE_FERMO = 6;
        public const int TIPO_DOC_LIBERATORIA_TERZO = 7;

        public const int TIPO_DOC_PROVVEDIMENTO_ANNULLATO_AUTOTUTELA = 16;
        public const int TIPO_DOC_PROVVEDIMENTO_REVOCA_FERMO = 17;

        public const int TIPO_DOC_ENTRATE_DEFINIZIONE_AGEVOLATA = 40;
        public const int TIPO_DOC_RICORSI = 49;
        public const int TIPO_DOC_RICORSI_VECCHI = 13;
        public const int TIPO_DOC_DICHIARAZIONE_TERZO = 50;
        public const int TIPO_DOC_CITAZIONI = 51;
        public const int TIPO_DOC_PIGNORAMENTI_MOBILIARI = 52;
        public const int TIPO_DOC_PIGNORAMENTI_IMMOBILIARI = 53;
        public const int TIPO_DOC_IPOTECHE = 54;
        public const int TIPO_DOC_PROCEDURA_CONCORSUALE = 60;
        public const int TIPO_DOC_PROVVEDIMENTO_ANNULLAMENTO_REVOCA_FERMO = 19;

        [Obsolete("Usare TIPO_DOC_DENUNCIA_TARI")]
        public const int TIPO_DOC_TARI = 61;
        public const int TIPO_DOC_DENUNCIA_TARI = 61;
        public const int TIPO_DOC_DENUNCIA_IMU = 62;
        public const int TIPO_DOC_DENUNCIA_GESTIONE_CANONE_PATRIMONIALE_OCCUPAZIONE_SUOLO = 63;


        //Id_Tipo_Doc_Entrate input
        public const int ID_TIPO_DOC_ENTRATE_ISTANZE_ANNULLAMENTO_RETTIFICA = 24;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_REVOCA_FERMO = 114;
        public const int ID_TIPO_DOC_ENTRATE_RIMBORSO = 426;
        public const int ID_TIPO_DOC_ENTRATE_RATEIZZAZIONE_SINGOLO_AVVISO = 411;
        public const int ID_TIPO_DOC_ENTRATE_RATEIZZAZIONE_ACCERTAMENTI_COATTIVI = 1499;
        public const int ID_TIPO_DOC_ENTRATE_PROC_CONC = 413;
        public const int ID_TIPO_DOC_ENTRATE_CITAZIONE_DEL_TERZO = 414;

        public const int ID_TIPO_DOC_ENTRATE_CTPro = 427;
        public const int ID_TIPO_DOC_ENTRATE_GDP = 428;
        public const int ID_TIPO_DOC_ENTRATE_TRIBORD = 429;
        public const int ID_TIPO_DOC_ENTRATE_TAR = 1457;
        public const int ID_TIPO_DOC_ENTRATE_CITAZIONE_GDP = 1469;
        public const int ID_TIPO_DOC_ENTRATE_Appello_CTReg = 415;
        public const int ID_TIPO_DOC_ENTRATE_Appello_COS = 1458;
        public const int ID_TIPO_DOC_ENTRATE_Appello_CAS_TRIBORD = 1459;
        public const int ID_TIPO_DOC_ENTRATE_Appello_CAS_CTR = 1460;
        public const int ID_TIPO_DOC_ENTRATE_Appello_TRIBORD_ricorso = 1461;
        public const int ID_TIPO_DOC_ENTRATE_Appello_TRIBORD_citazione = 1470;
        public const int ID_TIPO_DOC_ENTRATE_Appello_Corte = 1504;

        public const int ID_TIPO_DOC_ENTRATE_REVOCA_FERMO = 425;
        public const int ID_TIPO_DOC_ENTRATE_SOSPENSIONE_FERMO = 430;
        public const int ID_TIPO_DOC_ENTRATE_LIBERATORIA_TERZO = 431;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_RINUNCIA_PIGNORAMENTO = 1467;
        public const int ID_TIPO_DOC_ENTRATE_DEFINIZIONE_AGEVOLATA = 437;
        public const int ID_TIPO_DOC_ENTRATE_DEFINIZIONE_AGEVOLATA2 = 1462;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ANNULLAMENTO_REVOCA_FERMO = 1453;
        public const int ID_TIPO_DOC_ENTRATE_IPOTECA = 1455;
        public const int ID_TIPO_DOC_ENTRATE_ORDINE_TERZO = 412;
        public const int ID_TIPO_DOC_ENTRATE_CITAZIONE_TERZO = 1463;
        public const int ID_TIPO_DOC_ENTRATE_STRAGIUDIZIALE_TERZO = 1464;
        public const int ID_TIPO_DOC_ENTRATE_ASSEGNAZIONE_DICHIARAZIONE_TERZO = 1465;
        public const int ID_TIPO_DOC_ENTRATE_ORDINANZA_ESTINZIONE_DICHIARAZIONE_TERZO = 1490;
        public const int ID_TIPO_DOC_ENTRATE_ORDINANZA_MANCATA_DICHIARAZIONE_TERZO = 1491;
        public const int ID_TIPO_DOC_ENTRATE_ORDINANZA_RINVIO_UDIENZA = 1493;
        public const int ID_TIPO_DOC_ENTRATE_ORDINANZA_RINOTIFICA_CITAZIONE = 1492;
        public const int ID_TIPO_DOC_ENTRATE_ORDINANZA_FISSAZIONE_UDIENZA = 1494;

        //Id_Tipo_Doc_Entrate output
        public const int ID_TIPO_DOC_ENTRATE_COMUNICAZIONE_ESITO_ISTANZA_ANNRET_OUTPUT = 434;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ANNULLAMENTO_AUTOTUTELA_OUTPUT = 435;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_RIMBORSO_OUTPUT = 436;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_REVOCA_FERMO_OUTPUT = 1447;

        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_SOSPENSIONE_FERMO_OUTPUT = 1449;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ANNULLAMENTO_RATEIZZAZIONE_OUTPUT = 1452;
        public const int ID_TIPO_DOC_ENTRATE_SOLLECITO_PAGAMENTO_RATE_INSOLUTE = 1489;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_AUTORIZZAZIONE_CANCELLAZIONE_FERMO_OUTPUT = 1454;
        public const int ID_TIPO_DOC_ENTRATE_LIBERATORIA_TERZO_OUTPUT = 433;
        public const int ID_TIPO_DOC_ENTRATE_COMUNICAZIONE_RINUNCIA_ESECUZIONE_OUTPUT = 1468;
        public const int ID_TIPO_DOC_ENTRATE_DICHIARAZIONE_RINUNCIA_ESECUZIONE_OUTPUT = 1466;

        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ACCOGLIMENTO_MEDIAZIONE = 1448;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ACCOGLIMENTO_PARZIALE_MEDIAZIONE = 1472;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_ANNULLAMENTO_RETTIFICA_AUTOTUTELA_SU_RICORSO = 1478;


        public const string COD_TIPO_DOC__CITAZIONE_DEL_TERZO = "55Y01";
        public const string COD_TIPO_DOC__IPOTECA = "55Z01";

        #region Doc Entrate Per Denunce Tari - da questi risali all'essere denuncia abitativo o non abitativo

        // DenunceModel.ApplicazioneChiamante_t.PresentazioneDenunceTari
        public const int ID_TIPO_DOC_ENTRATE_DENUNCIA_TARI_PER_USO_ABITATIVO = 1473;
        public const int ID_TIPO_DOC_ENTRATE_DENUNCIA_TARI_PER_USO_NON_ABITATIVO = 1474;

        // DenunceModel.ApplicazioneChiamante_t.RavvedimentoTARIOmessaDichiarazione
        public const int ID_TIPO_DOC_ENTRATE_RAVVEDIMENTO_TARI_PER_OMESSA_DENUNCIA_USO_ABITATIVO = 1475;
        public const int ID_TIPO_DOC_ENTRATE_RAVVEDIMENTO_TARI_PER_OMESSA_DENUNCIA_USO_NON_ABITATIVO = 1476;

        // DenunceModel.ApplicazioneChiamante_t.RavvedimentoTARIInfedeleDichiarazione
        public const int ID_TIPO_DOC_ENTRATE_RAVVEDIMENTO_TARI_PER_INFEDELE_DENUNCIA_USO_ABITATIVO = 1480;
        public const int ID_TIPO_DOC_ENTRATE_RAVVEDIMENTO_TARI_PER_INFEDELE_DENUNCIA_USO_NON_ABITATIVO = 1481;

        // ProvvedimentiAccertamentoTariInfedeleDenuncia
        public const int ID_TIPO_DOC_ENTRATE_ACCERTAMENTO_TARI_PER_INFEDELE_DENUNCIA_USO_ABITATIVO = 1495;
        public const int ID_TIPO_DOC_ENTRATE_ACCERTAMENTO_TARI_PER_INFEDELE_DENUNCIA_USO_NON_ABITATIVO = 1496;

        // ProvvedimentiAccertamentoTariOmessaDenuncia
        public const int ID_TIPO_DOC_ENTRATE_ACCERTAMENTO_TARI_PER_OMESSA_DENUNCIA_USO_ABITATIVO = 1497;
        public const int ID_TIPO_DOC_ENTRATE_ACCERTAMENTO_TARI_PER_OMESSA_DENUNCIA_USO_NON_ABITATIVO = 1498;

        // ProvvedimentiVariazioneTariInAutotutela
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_IN_AUTOTUTELA_DI_VARIAZIONE_TARI_PER_USO_ABITATIVO = 1500;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_IN_AUTOTUTELA_DI_VARIAZIONE_TARI_PER_USO_NON_ABITATIVO = 1501;

        //DichiarazioneRequisitiPerAgevolazioniTari
        public const int ID_TIPO_DOC_ENTRATE_DICHIARAZIONE_REQUISITI_PER_AGEVOLAZIONI_TARI_USO_ABITATIVO = 1502;
        public const int ID_TIPO_DOC_ENTRATE_DICHIARAZIONE_REQUISITI_PER_AGEVOLAZIONI_TARI_USO_NON_ABITATIVO = 1503;

        // IMU
        public const int ID_TIPO_DOC_ENTRATE_PRESENTAZIONE_DICHIARAZIONI_IMU = 1505;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTI_DI_RETTIFICA_IMU = 1506;
        public const int ID_TIPO_DOC_ENTRATE_INTERROGAZIONE_DICHIARAZIONI_IMU = 1507;

        // CanonePatrimonialeOccupazioneSuoloPubblico
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_DI_CONCESSIONE_PER_OCCUPAZIONE_PERMANENTE_DI_SUOLO_PUBBLICO              = 1509;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_DI_AUTORIZZAZIONE_PER_OCCUPAZIONE_TEMPORANEA_DI_SUOLO_PUBBLICO           = 1510;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_DI_RETTIFICA_IN_AUTOTUTELA_DI_OCCUPAZIONE_PERMANENTE_DI_SUOLO_PUBBLICO   = 1511;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_DI_RETTIFICA_IN_AUTOTUTELA_DI_OCCUPAZIONE_TEMPORANEA_DI_SUOLO_PUBBLICO   = 1512;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_DI_RILEVAZIONE_OCCUPAZIONE_ABUSIVA_PERMANENTE_DI_SUOLO_PUBBLICO          = 1513;
        public const int ID_TIPO_DOC_ENTRATE_PROVVEDIMENTO_DI_RILEVAZIONE_OCCUPAZIONE_ABUSIVA_TEMPORANEA_DI_SUOLO_PUBBLICO          = 1514;

        #endregion

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public static bool isAppello(int IdTipoDocEntrate)
        {
            if (IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_CTReg ||
                IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_TRIBORD_ricorso ||
                IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_TRIBORD_citazione ||
                IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_COS ||
                IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_CAS_TRIBORD ||
                IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_CAS_CTR ||
                IdTipoDocEntrate == ID_TIPO_DOC_ENTRATE_Appello_Corte)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
