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
    [MetadataTypeAttribute(typeof(tab_log_utenti.Metadata))]
    public partial class tab_log_utenti
    {
        [DisplayName("Data Inizio Attività")]
        public string inizio_attivita_String
        {
            get
            {
                return inizio_attivita.ToString();
            }
        }

        [DisplayName("Data Fine Attività")]
        public string fine_attivita_String
        {
            get
            {
                if (fine_attivita.HasValue)
                {
                    return fine_attivita.Value.ToString();
                }
                else
                {
                    return string.Empty;
                }
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
