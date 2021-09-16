using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_registro_giornaliero_riscossioni.Metadata))]
    public partial class tab_registro_giornaliero_riscossioni : ISoftDeleted, IGestioneStato
    {

        public const int ANN_CON_ID = 0;
        public const string ANN_CON = "ANN-CON";

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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
