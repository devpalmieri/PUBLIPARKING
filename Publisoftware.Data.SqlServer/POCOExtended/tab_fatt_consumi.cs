using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_fatt_consumi.Metadata))]
    public partial class tab_fatt_consumi: ISoftDeleted, IGestioneStato
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

        public string periodo_contribuzione_da_String
        {
            get
            {
                if (periodo_contribuzione_da.HasValue)
                {
                    return periodo_contribuzione_da.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string periodo_contribuzione_a_String
        {
            get
            {
                if (periodo_contribuzione_a.HasValue)
                {
                    return periodo_contribuzione_a.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string tipoAddebito
        {
            get
            {
                if (tipo_addebito == Publisoftware.Data.tab_unita_contribuzione.FLAG_TIPO_ADDEBITO_ACCONTO)
                {
                    return "Acconto";
                }
                else if (tipo_addebito == Publisoftware.Data.tab_unita_contribuzione.FLAG_TIPO_ADDEBITO_NORMALE)
                {
                    return "Reale";
                }
                else if (tipo_addebito == Publisoftware.Data.tab_unita_contribuzione.FLAG_TIPO_ADDEBITO_CONGUAGLIO)
                {
                    return "Conguaglio";
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
