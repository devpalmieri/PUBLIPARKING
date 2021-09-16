using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_dett_mov_pag : ISoftDeleted, IGestioneStato
    {

        public const int ANN_CON_ID = 0;
        public const string ANN_CON = "ANN-CON";
        public const string CAR_CAR = "CAR-CAR";
        public const string CAR_CON = "CAR-CON";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }
    }
}
