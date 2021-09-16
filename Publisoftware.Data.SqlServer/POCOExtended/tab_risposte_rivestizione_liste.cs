using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_risposte_rivestizione_liste.Metadata))]
    public partial class tab_risposte_rivestizione_liste : ISoftDeleted
    {
        public const string COD_STATO_ANNULLATO = "ANN-ANN";
        public const string COD_STATO_ATTIVO = "ATT-ATT";
        public const string COD_STATO_ERRORE = "ATT-ERR";

        public bool IsSoftDeletable => true;

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
