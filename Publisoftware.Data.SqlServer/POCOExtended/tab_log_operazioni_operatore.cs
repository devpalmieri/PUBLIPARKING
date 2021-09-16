using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_log_operazioni_operatore.Metadata))]
    public partial class tab_log_operazioni_operatore
    {
        [DisplayName("Data")]
        public string data_String
        {
            get
            {
                return data.ToString();
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
