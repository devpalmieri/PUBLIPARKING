using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_avv_pag_fatt_emissione_soggetto_debitore : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = join_avv_pag_soggetto_debitore.ATT_ATT;
        public const string ANN_ANN = join_avv_pag_soggetto_debitore.ANN_ANN;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }
    }
}
