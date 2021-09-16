using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_avv_pag_mov_pag_fatt_emissione.Metadata))]
    public partial class join_avv_pag_mov_pag_fatt_emissione : IGestioneStato
    {        

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }
        internal sealed class Metadata
        {
            private Metadata()
            {
            }
        }

    }
}
