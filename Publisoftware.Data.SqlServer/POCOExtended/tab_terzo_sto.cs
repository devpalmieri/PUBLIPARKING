using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_terzo_sto : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = System.DateTime.Now;
            id_risorsa_stato = p_idRisorsa;
            id_struttura_stato = p_idStruttura;
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }
    }
}
