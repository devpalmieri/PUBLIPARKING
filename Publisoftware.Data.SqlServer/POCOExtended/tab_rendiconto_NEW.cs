using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_rendiconto_NEW.Metadata))]
    public partial class tab_rendiconto_NEW
    {
        public const string ANN = "ANN-";
        public const string ANN_REN = "ANN-REN";
        public const string ANN_ANN = "ANN-ANN";
        public const string ATT = "ATT-";
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_REN = "ATT-REN";
        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }
    }
}
