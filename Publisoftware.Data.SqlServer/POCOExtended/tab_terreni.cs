using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_terreni.Metadata))]
    public partial class tab_terreni
    {
        public string RedditoDominicale
        {
            get
            {
                if (reddito_dominicale_euro.HasValue)
                {
                    return String.Format("{0:0}", reddito_dominicale_euro.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string RedditoAgrario
        {
            get
            {
                if (reddito_agrario_euro.HasValue)
                {
                    return String.Format("{0:0}", reddito_agrario_euro.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string data_inizio_String
        {
            get
            {
                if (tab_note1 != null)
                {
                    if (tab_note1.data_efficacia.HasValue)
                    {
                        return tab_note1.data_efficacia.Value.ToShortDateString();
                    }
                    else
                    {
                        return tab_note1.data_registrazione.ToShortDateString();
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_fine_String
        {
            get
            {
                if (tab_note != null)
                {
                    if (tab_note.data_efficacia.HasValue)
                    {
                        return tab_note.data_efficacia.Value.ToShortDateString();
                    }
                    else
                    {
                        return tab_note.data_registrazione.ToShortDateString();
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
