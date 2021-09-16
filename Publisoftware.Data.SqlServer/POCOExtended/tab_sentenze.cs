using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_sentenze.Metadata))]
    public partial class tab_sentenze : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ESI_ACC = "ESI-ACC";
        public const string ESI_RES = "ESI-RES";

        public const string SPESE_COMPENSAZIONE = "C";
        public const string SPESE_CONTRIBUENTE = "D";
        public const string SPESE_ENTE = "E";

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

        public string data_deposito_sentenza_String
        {
            get
            {
                if (data_deposito_sentenza.HasValue)
                {
                    return data_deposito_sentenza.Value.ToShortDateString();
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
                    data_deposito_sentenza = DateTime.Parse(value);
                }
                else
                {
                    data_deposito_sentenza = null;
                }
            }
        }

        public string data_notifica_sentenza_String
        {
            get
            {
                if (data_notifica_sentenza.HasValue)
                {
                    return data_notifica_sentenza.Value.ToShortDateString();
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
                    data_notifica_sentenza = DateTime.Parse(value);
                }
                else
                {
                    data_notifica_sentenza = null;
                }
            }
        }

        public string data_scadenza_appello_String
        {
            get
            {
                if (data_scadenza_appello.HasValue)
                {
                    return data_scadenza_appello.Value.ToShortDateString();
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
                    data_scadenza_appello = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_appello = null;
                }
            }
        }

        public string data_sentenza_String
        {
            get
            {
                return data_sentenza.ToShortDateString();
            }
            set
            {
                data_sentenza = DateTime.Parse(value);
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