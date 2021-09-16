using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class anagrafica_stato_oggetto : ISoftDeleted
    {
        public const int ATTIVO_ID = 1;
        public const string ATTIVO = "ATT-ATT";

        public const int CESSATO_ID = 13;
        public const string CESSATO = "ATT-CES";

        public const int VARIATO_ID = 5;
        public const string VARIATO = "ATT-VAR";

        public const int ANNULLATO_ID = 3;
        public const string ANNULLATO = "ANN-ANN";

        public const int RET_CES_ID = 41;
        public const string RET_CES = "RET-CES";

        public const int RET_VAR_ID = 40;
        public const string RET_VAR = "RET-VAR";

        public const string ANN = "ANN-";
        public const string ATT = "ATT-";
        public const string RET = "RET-";
        public const string SSP = "SSP-";
        public const string SUB = "SUB-";

        public const string ANN_SSW = "ANN-SSW";

        public const int ATT_VAR_ID = 5;
        public const string ATT_VAR = "ATT-VAR";

        public const int RET_RET_ID = 4;
        public const string RET_RET = "RET-RET";
        public const int RET_ATT_ID = 43;
        public const string RET_ATT = "RET-ATT";


        // Vecchio, non usare
        public const int RET_INF_ID = 38;
        public const string RET_INF = "RET-INF";

        // Vecchio, non usare
        public const int RET_IRR_ID = 68;
        public const string RET_IRR = "RET-IRR";

        // Vecchio, non usare
        public const int RET_CON_ID = 69;
        public const string RET_CON = "RET-CON";

        /// <summary>
        /// Sospeso in attesa di consolidamento da una lista di trasmissione
        /// </summary>
        public const string SSP_TRA = "SSP-TRA";
        public const int SSP_TRA_ID = 100;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public static IDictionary<string, int> CodStatoToIdStatoOggettoDic = new Dictionary<string, int>
        {
            {ATTIVO, ATTIVO_ID   },
            // Sostituisci le stringhe e i numeri con le definizioni, se vuoi
            {ANNULLATO, ANNULLATO_ID   }, // "ANN-ANN"
            {"RET-RET", 4   },
            {"ATT-VAR", 5   },
            {"ATT-SUB", 8   },
            {"ATT-VOL", 9   },
            {"ATT-PRE", 10  },
            {"SAL-PRE", 11  },
            {"ATT-IRR", 12  },
            {CESSATO, CESSATO_ID  }, // ATT-CES
            {"INC-PRE", 14  },
            {"ATT-DCC", 32  },
            {"PCC-PRE", 33  },
            {"PAL-PRE", 35  },
            {"EFF-PRE", 36  },
            {"SUB-CES", 37  },
            {"RET-INF", 38  },
            {"ATT-OME", 39  },
            {"RET-VAR", 40  },
            {"RET-CES", 41  },
            {"ATT-CON", 42  },
            {"RET-ATT", 43  },
            {"ANN-ATT", 44  },
            {"ANN-CES", 45  },
            {"ANN-VAR", 46  },
            {"ANN-OME", 47  },
            {"SSP-ATT", 48  },
            {"SSP-CES", 49  },
            {"SSP-VAR", 50  },
            {"SSP-OME", 51  },
            {"SSP-SUB", 52  },
            {"SSP-VOL", 53  },
            {"SSP-PRE", 54  },
            {"SSP-IRR", 55  },
            {"SSP-DCC", 56  },
            {"SSP-CON", 57  },
            {"SUB-ATT", 58  },
            {"SUB-CON", 59  },
            {"SUB-VAR", 60  },
            {"SUB-OME", 61  },
            {"SUB-ANN", 62  },
            {"SUB-RET", 63  },
            {"SUB-VOL", 64  },
            {"SUB-PRE", 65  },
            {"SUB-IRR", 66  },
            {"SUB-DCC", 67  },
            {"RET-IRR", 68  },
            {"RET-CON", 69  },
            {"ANN-CON", 70  },
            {"ANN-RET", 71  },
            {"SAC-ATT", 72  },
            {"SAC-VAR", 73  },
            {"SAC-CES", 74  },
            {"SAC-IRR", 75  },
            {"ATT-VER", 76  },
            {"SSP-RIC", 77  },
            {"ANN-RIC", 78  },
            {"ANN-RRT", 79  },
            {"SSP-VDI", 80  },
            {"SSP-IDI", 81  },
            {"SSP-IDC", 82  },
            {"ATT-VDI", 83  },
            {"ATT-IDI", 84  },
            {"ANN-VDI", 85  },
            {"ANN-IDI", 86  },
            {"SSP-TRA", 100 }
        };

        public static int FindIdStatoOggetto(string codStatoOggetto, bool throwOnNotFound, int defaultCodStatoOnNotFound)
        {
            codStatoOggetto = (codStatoOggetto ?? "").Trim();

            if (CodStatoToIdStatoOggettoDic.TryGetValue(codStatoOggetto, out int idStatoOggetto))
            {
                return idStatoOggetto;
            }
            if (throwOnNotFound)
            {
                throw new Exception($"Cod stato oggetto di contribuzione non esesitente: {codStatoOggetto}");
            }
            return defaultCodStatoOnNotFound;
        }

        public static int TrovaIdStatoRET(string nuovoCodStatoOggettoImo, Publisoftware.Utility.Log.ILogger logger = null)
        {
            switch (nuovoCodStatoOggettoImo)
            {
                case anagrafica_stato_oggetto.RET_RET:
                    return anagrafica_stato_oggetto.RET_RET_ID;

                case anagrafica_stato_oggetto.RET_INF:
                    return anagrafica_stato_oggetto.RET_INF_ID;

                case anagrafica_stato_oggetto.RET_VAR:
                    return anagrafica_stato_oggetto.RET_VAR_ID;

                case anagrafica_stato_oggetto.RET_CES:
                    return anagrafica_stato_oggetto.RET_CES_ID;

                case anagrafica_stato_oggetto.RET_ATT:
                    return anagrafica_stato_oggetto.RET_ATT_ID;

                case anagrafica_stato_oggetto.RET_IRR:
                    return anagrafica_stato_oggetto.RET_IRR_ID;

                case anagrafica_stato_oggetto.RET_CON:
                    return anagrafica_stato_oggetto.RET_CON_ID;
            }

            // Il dot. ha detto che sono possibili solo RET-VAR, RET-ATT, RET-CES
            // infatti leggiamo solo questi stati nella griglia WEB,
            // quindi qui non dovrebbe mai arrivare:
            if (logger != null)
            {
                logger.LogMessage($"TARI: stato RET sconosciuto: {nuovoCodStatoOggettoImo} - uso id di {anagrafica_stato_oggetto.RET_RET}", Utility.Log.EnLogSeverity.Error);
            }
            return anagrafica_stato_oggetto.RET_RET_ID;
        }

    }
}
