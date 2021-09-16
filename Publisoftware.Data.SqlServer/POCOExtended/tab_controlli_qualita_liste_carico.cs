using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_controlli_qualita_liste_carico.Metadata))]
    public partial class tab_controlli_qualita_liste_carico : ISoftDeleted, IGestioneStato
    {
        public const String ATT_ATT = "ATT-ATT";

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
      
        public string imp_avvpag_controllati_Euro
        {
            get
            {
                if (imp_avvpag_controllati.HasValue)
                    return imp_avvpag_controllati.Value.ToString("C");
                else
                    return string.Empty;
            }
        }
     
        public string imp_avvpag_annullati_Euro
        {
            get
            {
                if (imp_avvpag_annullati.HasValue)
                    return imp_avvpag_annullati.Value.ToString("C");
                else
                    return string.Empty;
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
