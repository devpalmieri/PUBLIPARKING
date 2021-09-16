using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_fabbricati.Metadata))]
    public partial class tab_fabbricati
    {
        public string Superficie
        {
            get
            {
                if (superficie.HasValue)
                {
                    return superficie.Value + " mq";
                }
                else
                {
                    return "0 mq";
                }
            }
        }

        public string SuperficieTarsu
        {
            get
            {
                if (superficietarsu.HasValue)
                {
                    return superficietarsu.Value + " mq";
                }
                else
                {
                    return "0 mq";
                }
            }
        }

        public string Rendita
        {
            get
            {
                if (rendita_euro.HasValue)
                {
                    return String.Format("{0:0.00000}", rendita_euro.Value) + " €";
                }
                else
                {
                    return "0 €";
                }
            }
        }

        public string Ubicazione
        {
            get
            {
                string v_tipoToponimo = tab_fabbricati_indirizzi.Count > 0 && tab_fabbricati_indirizzi.FirstOrDefault().tab_toponimi_catasto != null && tab_fabbricati_indirizzi.FirstOrDefault().tab_toponimi_catasto.tab_tipi_toponimo_catasto != null ? tab_fabbricati_indirizzi.FirstOrDefault().tab_toponimi_catasto.tab_tipi_toponimo_catasto.tipo_toponimo : string.Empty;
                string v_toponimo = tab_fabbricati_indirizzi.Count > 0 && tab_fabbricati_indirizzi.FirstOrDefault().tab_toponimi_catasto != null ? tab_fabbricati_indirizzi.FirstOrDefault().tab_toponimi_catasto.toponimo : string.Empty;
                string v_civico = tab_fabbricati_indirizzi.Count > 0 && tab_fabbricati_indirizzi.FirstOrDefault().civico1 != string.Empty ? tab_fabbricati_indirizzi.FirstOrDefault().civico1.TrimStart('0') : string.Empty;

                return v_tipoToponimo + " " + v_toponimo + " " + v_civico;
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
