using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_fatt_cont.Metadata))]
    public partial class tab_fatt_cont : ISoftDeleted, IGestioneStato
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

        public string data_lettura_1_String
        {
            get
            {
                if (data_lettura_1.HasValue)
                {
                    return data_lettura_1.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_lettura_2_String
        {
            get
            {
                if (data_lettura_2.HasValue)
                {
                    return data_lettura_2.Value.ToShortDateString();
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
