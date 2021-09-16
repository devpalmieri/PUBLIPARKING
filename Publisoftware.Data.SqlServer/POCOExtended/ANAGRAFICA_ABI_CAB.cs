using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(ANAGRAFICA_ABI_CAB.Metadata))]
    public partial class ANAGRAFICA_ABI_CAB : ISoftDeleted, IGestioneStato
    {

        public const string ATT_ATT = "ATT-ATT";
        public const int BANCOPOSTA_ID = 97111;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            DATA_AGGIORNAMENTO = DateTime.Now;
            ID_STRUTTURA_STATO = p_idStruttura;
            ID_RISORSA_STATO = p_idRisorsa;
        }        

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [Required]
            [DisplayName("ID")]
            public int ID_ABI_CAB { get; set; }

            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato ABI non valido (Es: 12345)")]
            public string ABI { get; set; }

            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato CAB non valido (Es: 12345)")]
            public string CAB { get; set; }

            [Required]
            [DisplayName("Banca")]
            public string BANCA { get; set; }

            [DisplayName("Agenzia")]
            public string AGENZIA { get; set; }

            [DisplayName("Indirizzo")]
            public string INDIRIZZO { get; set; }

            [RegularExpression("[0-9]{5}", ErrorMessage = "Formato CAP non valido (Es: 12345)")]
            public string CAP { get; set; }

            [DisplayName("Località")]
            public string LOCALITA { get; set; }

            [RegularExpression("[A-Za-z]{2}", ErrorMessage = "Formato PROVINCIA non valido (Es: AB)")]
            [DisplayName("Provincia")]
            public string PROVINCIA { get; set; }
        }
    }
}
