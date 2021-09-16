using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_cc_tesoreria.Metadata))]
    public partial class tab_cc_tesoreria : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
