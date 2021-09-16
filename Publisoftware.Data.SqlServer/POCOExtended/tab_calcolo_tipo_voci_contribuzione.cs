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
    [MetadataTypeAttribute(typeof(tab_calcolo_tipo_voci_contribuzione.Metadata))]
    public partial class tab_calcolo_tipo_voci_contribuzione : ISoftDeleted, IGestioneStato
    {

        public const string MODALITA_CALCOLO_FISSA = "F";
        public const string MODALITA_CALCOLO_ALIQUOTA_ANNUALE = "A";
        public const string MODALITA_CALCOLO_ALIQUOTA_SEMESTRALE = "S";

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

        [DisplayName("SpeseSpedizione")]
        public string SpeseSpedizioneDisplay
        {
            get
            {
                return anagrafica_tipo_spedizione_notifica.descr_tipo_spedizione_notifica + " - " + (importo_fisso.HasValue ? importo_fisso.Value.ToString("C") : 0.ToString("C"));
            }
        }

        internal sealed class Metadata
        {
            [DisplayName("Tipo Spedizione")]
            public Nullable<int> id_tipo_spedizione_notifica { get; set; }

            private Metadata()
            {

            }

        }
    }
}
