using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_piano_conti_economici.Metadata))]
    public partial class tab_piano_conti_economici : ISoftDeleted
    {
        public const string VAL_VAL = "VAL-VAL";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public string CodiceDescrizione
        {
            get
            {
                return codice_completo + "/" + descr_conto_bilancio;
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
