using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_anagrafiche_da_rivestire.Metadata))]
    public partial class tab_anagrafiche_da_rivestire : ISoftDeleted
    {
        public const string FLAG_TIPO_SOGGETTO_DEBITORE_REFERENTE = "R";
        public const string FLAG_TIPO_SOGGETTO_DEBITORE_TERZO = "T";
        public const string FLAG_TIPO_SOGGETTO_DEBITORE_CONTRIBUENTE = "C";
        public static readonly string[] FlagTipoSoggettoDebitoreValues = new string[3]
        {
            FLAG_TIPO_SOGGETTO_DEBITORE_REFERENTE, FLAG_TIPO_SOGGETTO_DEBITORE_TERZO, FLAG_TIPO_SOGGETTO_DEBITORE_CONTRIBUENTE
        };
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
