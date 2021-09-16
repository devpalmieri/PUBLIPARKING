using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_generale_udienze.Metadata))]
    public partial class tab_generale_udienze : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_OLD = "ATT-OLD";
        public const string ANN = "ANN-";

        public const string RINVIO_ASSEGNAZIONE_SOMME = "OAS";
        public const string RINVIO_A_SENTENZA = "SEN";
        public const string RINVIO_BONARIO = "BON";
        public const string RINVIO_UFFICIO = "RUF";
        public const string RINVIO_NOSTRA_RICHIESTA = "RRP";
        public const string RINVIO_RICHIESTA_CONTROPARTE = "RRC";
        public const string ANTICIPO_UDIENZA = "ANT";
        public const string ORDINANZA_ESTINZIONE = "OES";
        public const string ORDINANZA_MANCATA_DICHIARAZIONE = "OMD";
        public const string ORDINANZA_RINVIO_UDIENZA = "ORU";
        public const string ORDINANZA_RINOTIFICA_CITAZIONE = "ONC";

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

        public string data_udienza_String
        {
            get
            {
                if (data_udienza.HasValue)
                {
                    return data_udienza.Value.ToShortDateString();
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
                    data_udienza = DateTime.Parse(value);
                }
                else
                {
                    data_udienza = null;
                }
            }
        }

        public string data_rinvio_udienza_String
        {
            get
            {
                if (data_rinvio_udienza.HasValue)
                {
                    return data_rinvio_udienza.Value.ToShortDateString();
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
                    data_rinvio_udienza = DateTime.Parse(value);
                }
                else
                {
                    data_rinvio_udienza = null;
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
