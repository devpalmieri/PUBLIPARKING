using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_dati_metrici_dichiarati : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = System.DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string Superficie
        {
            get
            {
                if (superficie_min_tarsu.HasValue)
                {
                    return superficie_min_tarsu.Value + " mq";
                }
                else
                {
                    return "0 mq";
                }
            }
        }
    }
}
