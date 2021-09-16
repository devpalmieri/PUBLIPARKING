using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    //// Abilitare per la gestione dei controlli nelle View
    [MetadataTypeAttribute(typeof(join_tipo_avvpag_voci_contrib_new.Metadata))]
    public partial class join_tipo_avvpag_voci_contrib_new  : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            // Gestire il salvataggio automatico dello stato
            data_stato = DateTime.Now;
            id_struttura_stato= p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        [DisplayName("Sequenza di calcolo")]
        public int sequenza_calcolo_anagrafica_voce_Ext
        {
            get
            {
                return this.sequenza_calcolo_anagrafica_voce.HasValue ? this.sequenza_calcolo_anagrafica_voce.Value : 0;
            }
            set
            {               
               this.sequenza_calcolo_anagrafica_voce = value;               
            }
        }


        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_join_tipo_avvpag_voci_contrib { get; set; }

            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [DisplayName("Ente gestito")]
            public int id_ente_gestito { get; set; }

            [Required]
            [DisplayName("Tipo Avviso")]
            public int id_tipo_avv_pag { get; set; }

            [Required]
            [DisplayName("Voce contribuzione")]
            public int id_anagrafica_voce_contribuzione { get; set; }

            
            [DisplayName("Sequenza di calcolo")]
            [Range(0, int.MaxValue, ErrorMessage = "Selezionare un numero di sequenza valido")]
            public Nullable<int> sequenza_calcolo_anagrafica_voce { get; set; }

            [Required]
            [DisplayName("Flag di calcolo")]
            [Range(0, 1, ErrorMessage = "Selezionare un numero compreso tra 0 e 1")]
            public string flag_calcolo_oggetti_contribuzione { get; set; }
            
        }
    }
}
