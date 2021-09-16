using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_registro_assegnazione_pratiche.Metadata))]
    public partial class tab_registro_assegnazione_pratiche : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ANN = "ANN-";
        public const string ANN_ANN = "ANN-ANN";

        public const string FLAG_VERBALEQUIETANZA_VERBALE = "V";
        public const string FLAG_VERBALEQUIETANZA_QUIETANZA = "Q";

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

        public sealed class Metadata
        {
            private Metadata()
            {

            }
        }
    }
}
