using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(TAB_JOIN_AVVCOA_INGFIS_V2.Metadata))]
    public partial class TAB_JOIN_AVVCOA_INGFIS_V2 : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_OLD = "ATT-OLD";
        public const string ANN_ANN = "ANN-ANN";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;

            if (string.IsNullOrEmpty(cod_stato))
            {
                cod_stato = ATT_ATT;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
