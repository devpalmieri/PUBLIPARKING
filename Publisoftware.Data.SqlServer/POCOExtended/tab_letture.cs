using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_letture.Metadata))]
    public partial class tab_letture : ISoftDeleted, IGestioneStato
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

        public string data_lettura_String
        {
            get
            {
                return data_lettura.ToShortDateString();
            }
        }

        public string Lettura
        {
            get
            {
                return string.Format("{0:0}", qta_lettura) + " mc";
            }
        }

        public string TipoLettura
        {
            get
            {
                if(flag_iof == "I")
                {
                    return "Iniziale";
                }
                else if (flag_iof == "O")
                {
                    return "Ordinaria";
                }
                else if (flag_iof == "F")
                {
                    return "Finale";
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
