using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_stampatori.Metadata))]
    public partial class anagrafica_stampatori : ISoftDeleted
    {
        public const int PUBLISERVIZI = 1;
        public const int POSTEL = 4;
        public const int NEXIVE = 3;
        public const int IMBALPLAST = 2;

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
