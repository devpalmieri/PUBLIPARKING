using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_tipo_avvpag_voci_contrib_trasmesse.Metadata))]
    public partial class join_tipo_avvpag_voci_contrib_trasmesse : ISoftDeleted, IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            // Gestire il salvataggio automatico dello stato
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_join_tipo_avvpag_voci_contrib_trasmesse { get; set; }

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
          
        }
    }
}
