using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_spuntista.Metadata))]
    public partial class tab_spuntista : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_CES = "ATT-CES";
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
        }

        public string data_inserimento_String
        {
            get
            {
                if (data_inserimento.HasValue)
                {
                    return data_inserimento.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_cessazione_String
        {
            get
            {
                if (data_cessazione.HasValue)
                {
                    return data_cessazione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
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
