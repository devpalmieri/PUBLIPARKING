using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_dett_riscossione:ISoftDeleted, IGestioneStato
    {
        public const int ATT_ATT_ID = 1;
        public const string ATT = "ATT-";
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_CON = "ATT-CON";
        public const string ATT_REN = "ATT-REN";
        public const int ANN_ANN_ID = 0;
        public const string ANN_ANN = "ANN-ANN";
        public const string ANN = "ANN-";
        public const int ANN_REN_ID = 0;
        public const int ANN_CON_ID = 0;
        public const string ANN_REN = "ANN-REN";
        public const string ANN_CON = "ANN-CON";
        public const string RET_CON = "RET-CON";
        

        public const string FLAG_SEGNO_NEGATIVO = "0";
        public const string FLAG_SEGNO_POSITVO = "1";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }
    }
}
