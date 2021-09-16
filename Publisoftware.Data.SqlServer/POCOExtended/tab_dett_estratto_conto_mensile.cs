using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_dett_estratto_conto_mensile.Metadata))]
    public partial class tab_dett_estratto_conto_mensile : Itab_dett_estratto_conto_mensile, ISoftDeleted, IGestioneStato
    {
        public static string NV_STATO_VERIFICA_PAGOPA = "NV";
        public static string FV_STATO_VERIFICA_PAGOPA = "FV";
        public static string VV_STATO_VERIFICA_PAGOPA = "VV";



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

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            
        }
    }
}
