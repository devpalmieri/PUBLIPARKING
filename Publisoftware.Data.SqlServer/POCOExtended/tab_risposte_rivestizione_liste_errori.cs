using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_risposte_rivestizione_liste_errori.Metadata))]
    public partial class tab_risposte_rivestizione_liste_errori : ISoftDeleted
    {
        public bool IsSoftDeletable => true;

        public const string LIVELLO_ERRORE = "ERR";
        public const string LIVELLO_WARNING = "WARN";
        public const string LIVELLO_DEBUG = "DEBUG";
        public const string LIVELLO_INFORMATION = "INFO";

        public const string TIPO_RECORD_LAC = "LAC";
        public const string TIPO_RECORD_CO1_142 = "CO1.142";
        public const string TIPO_RECORD_CO1_151 = "CO1.151";
        public const string TIPO_RECORD_FM1_12 = "FM1.12";
        [Obsolete("Vedi Publisoftware.Batch.RivestizioniAnagrafiche.RivestiAnagraficheDaTabAnagrafe")]
        public const string TIPO_RECORD_TAB_ANAGRAFE = nameof(tab_anagrafe);

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
