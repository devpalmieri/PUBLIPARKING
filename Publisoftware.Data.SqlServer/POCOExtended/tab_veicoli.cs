using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_veicoli : ISoftDeleted, IGestioneStato
    {
        public static string STATO_ANN_IST = "ANN-IST";
        public static string ANN_UFF = "ANN-UFF";

        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";

        public const int ATT_CES_ID = 13;
        public const string ATT_CES = "ATT-CES";

        public const int TIPO_AUTOVEICOLO = 1;
        public const int TIPO_MOTOVEICOLO = 2;
        public const int TIPO_RIMORCHIO = 3;
        public const int TIPO_CICLOMOTORE = 4;
        public const int TIPO_AUTOCARRO = 5;
        public const int TIPO_TARGA_PROVA = 6;
        public const int TIPO_AUTOBUS = 7;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_struttura, int p_risorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
        }
    }
}
