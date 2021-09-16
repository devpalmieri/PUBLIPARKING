using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_causale : ISoftDeleted
    {
        public static string TRATTAMENTO_AVVISO_RIFERIMENTO = "A";
        public static string TRATTAMENTO_AVVISO_COLLEGATO = "C";
        public static string TRATTAMENTO_AVVISO_ENTE = "E";
        public static string TRATTAMENTO_RIGA = "R";
        public static string TRATTAMENTO_X = "X";//A regime va eliminato
        public static string TRATTAMENTO_M = "M";

        public static string OPERAZIONE_ACQUISIZIONE = "A";
        public static string OPERAZIONE_LAVORAZIONE = "L";
        public static string OPERAZIONE_CESSAZIONE = "Z";
        public static string OPERAZIONE_I = "I"; // Non ho idea di cosa sia, mettiamo uguale a sigla e poi una volta chiarito modifichiamo con descrizione

        // 18/03/2020
        public static string OPERAZIONE_VARIAZIONE = "V";

        public static string SIGLA_TIPO_FISICO = "F";
        public static string SIGLA_TIPO_GIURIDICO = "G";

        public static string SIGLA_PAP = "PAP";
        public static string SIGLA_PAG = "PAG";
        public static string SIGLA_NAP = "NAP";
        public static string SIGLA_NOT = "NOT";
        public static string SIGLA_DCO = "DCO";
        public static string SIGLA_DRE = "DRE";
        public static string SIGLA_DTZ = "DTZ";
        public static string SIGLA_BEN = "BEN";
        public static string SIGLA_POS = "POS";
        public static string SIGLA_POI = "POI";
        public static string SIGLA_VIN = "VIN";
        public static string SIGLA_DEB = "DEB";
        public static string SIGLA_DNT = "DNT";
        public static string SIGLA_DET = "DET";
        public static string SIGLA_ELU = "ELU";
        public static string SIGLA_OGG = "OGG";
        public static string SIGLA_AGE = "AGE";
        public static string SIGLA_CNT = "CNT";
        public static string SIGLA_LET = "LET";
        public static string SIGLA_ACC = "ACC";
        public static string SIGLA_ACQ = "ACQ";
        public static string SIGLA_ASS = "ASS";
        public static string SIGLA_RES = "RES";
        public static string SIGLA_SSP = "SSP";
        public static string SIGLA_GEN = "GEN";
        public static string SIGLA_SAN = "SAN";
        public static string SIGLA_ESI = "ESI";

        public static int ID_GENERICO_ANNRETT = 5102;
        public static int ID_GENERICO_RICORSO = 5103;
        public static int ID_NOT_INGFISCOLL = 5030;
        public static int ID_SANZIONI_AVVISO = 5104;
        public static int ID_SANZIONI_RIGA = 5105;
        public static int ID_POSSESSO_REQUISITI_RATEIZZAZIONE = 5026;
        public static int ID_INDEBITA_ISCRIZIONE = 6207;
        public static int ID_VENDITA_ANTERIORE = 6216;
        public static int ID_RINUNCIA_RATEIZZAZIONE = 6251;
        public static int ID_ATTO_RIFERIMENTO_ANNULLATO_SGRAVIO = 6252;
        public static int ID_ATTO_COLLEGATO_ANNULLATO_SGRAVIO = 6253;
        public static List<int> IDs_PRECEDENTE_ISTANZA = new List<int>() { 6307, 6308, 6309, 6319, 6320, 6321 };

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
