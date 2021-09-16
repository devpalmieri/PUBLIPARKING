using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_occupazione_piazzole.Metadata))]
    public partial class tab_occupazione_piazzole : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string giorno_String
        {
            get
            {
                if (giorno.HasValue)
                {
                    return giorno.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    giorno = DateTime.Parse(value);
                }
                else
                {
                    giorno = null;
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
