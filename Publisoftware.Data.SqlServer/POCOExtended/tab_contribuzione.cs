using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_contribuzione:ISoftDeleted, IGestioneStato
    {
        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT = "ATT-";

        public const int ANN_ANN_ID = 183;
        public const string ANN_ANN = "ANN-ANN";
        public const string ANN = "ANN-";

        public const string ANN_SGR = "ANN-SGR";
        public const string SSP_UFF = "SSP-UFF";

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
