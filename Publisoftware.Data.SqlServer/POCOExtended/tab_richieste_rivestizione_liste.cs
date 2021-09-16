using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_richieste_rivestizione_liste.Metadata))]
    public partial class tab_richieste_rivestizione_liste : ISoftDeleted
    {
        public const string COD_STATO_ERRORE = "ATT-ERR";
        public const string COD_STATO_ANNULLATO = "ANN-ANN";
        public const string COD_STATO_ATTIVO = "ATT-ATT";


        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
