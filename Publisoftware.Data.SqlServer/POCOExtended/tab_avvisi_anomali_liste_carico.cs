using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_avvisi_anomali_liste_carico.Metadata))]
    public partial class tab_avvisi_anomali_liste_carico : ISoftDeleted, IGestioneStato
    {
        public const string VAL_EME = "VAL-EME";
        public const string VAL = "VAL-";
        public const string ANN_ANN = "ANN-ANN";
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT = "ATT";

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
