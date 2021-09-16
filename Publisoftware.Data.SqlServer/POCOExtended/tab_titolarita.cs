using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_titolarita
    {
        public string Quota
        {
            get
            {
                return quota_numeratore + "/" + quota_denominatore;
            }
        }

        public string DataInizio
        {
            get
            {
                return tab_note1.data_efficacia_String;
            }
        }

        public string DataFine
        {
            get
            {
                if(tab_note != null)
                {
                    return tab_note.data_efficacia_String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
