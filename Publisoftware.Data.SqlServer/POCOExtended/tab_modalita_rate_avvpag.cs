using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    //// Abilitare per la gestione dei controlli nelle View
    [MetadataTypeAttribute(typeof(tab_modalita_rate_avvpag.Metadata))]
    public partial class tab_modalita_rate_avvpag : ISoftDeleted, IGestioneStato
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
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        /// <summary>
        /// Ritorna i gg_massimi_data_notifica (0 se nullo)
        /// </summary>
        public int custom_prop_gg_max_data_notifica {
            get {
                return this.GG_massimi_data_notifica.HasValue ? this.GG_massimi_data_notifica.Value : 0;
            }
        }

        [DisplayName("Periodicità rate")]
        public PeriodicitaRateEnum periodicitaRate_enum
        {
            get
            {
                return (PeriodicitaRateEnum)periodicita_rate;
            }

            set
            {
                periodicita_rate = (int)value;
            }
        }

        [Required]
        [DisplayName("Validita da")]
        public string periodo_validita_da_String
        {
            get
            {
                return this.periodo_validita_da.ToShortDateString();
            }
            set
            {
                this.periodo_validita_da = DateTime.Parse(value);
            }
        }

        [Required]
        [DisplayName("Validita a")]
        public string periodo_validita_a_String
        {
            get
            {
                return this.periodo_validita_a.ToShortDateString();
            }
            set
            {
                this.periodo_validita_a = DateTime.Parse(value);
            }
        }

        [DisplayName("Doppia Notifica Amministratori P.G.")]
        public bool flag_notifica_amministratori_PG_NOT_NULL
        {
            get {
                return this.flag_notifica_amministratori_PG.HasValue ? this.flag_notifica_amministratori_PG.Value : false;
            }

            set {
                this.flag_notifica_amministratori_PG = value;
            }
        }

        [DisplayName("Decorrenza interessi")]
        public bool flag_decorrenza_interessi_NOT_NULL
        {
            get
            {
                return this.flag_decorrenza_interessi.HasValue ? this.flag_decorrenza_interessi.Value : false;
            }

            set
            {
                this.flag_decorrenza_interessi = value;
            }
        }


        [DisplayName("Rinotifica Amministratori P.G.")]
        public bool flag_rinotifica_amministratori_PG_NOT_NULL
        {
            get
            {
                return this.flag_rinotifica_amministratori_PG.HasValue ? this.flag_rinotifica_amministratori_PG.Value : false;
            }

            set
            {
                this.flag_rinotifica_amministratori_PG = value;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tab_modalita_rate_avvpag { get; set; }

            [DisplayName("Ente")]
            [Required]
            public int id_ente { get; set; }
            
            [DisplayName("Ente gestito")]
            [Required]
            public int id_ente_gestito { get; set; }

            [Required]
            [DisplayName("Tipo Avviso")]
            public int id_tipo_avv_pag { get; set; }
            
            [Range(0, 12, ErrorMessage = "deve essere compreso tra 1 e 12")]
            [DisplayName("Numero rate")]
            public int numero_rate { get; set; }

            [Required]
            [DisplayName("Periodicita rate")]
            public int periodicita_rate { get; set; }

            [DisplayName("Rate di importo uguale")]
            public bool flag_rate_importo_uguale { get; set; }
           

            [DisplayName("Spese di notifica su prima rata")]
            public bool flag_spese_sped_not_prima_rata { get; set; }

            [DisplayName("Spese coattive su prima rata")]
            public bool flag_spese_coattive_prima_rata { get; set; }

            [DisplayName("Validita da")]
            [Required]
            public System.DateTime periodo_validita_da { get; set; }

            [IsDateAfter("periodo_validita_da", false, ErrorMessage = "La data di fine deve essere successiva a quella iniziale")]
            [DisplayName("Validita a")]
            [Required]
            public System.DateTime periodo_validita_a { get; set; }

            [DisplayName("Responsabile emissione")]
            public Nullable<int> id_risorsa_resp_emissione_tipo_avvpag { get; set; }

            [DisplayName("Importo min. emissione avviso")]
            public decimal importo_minimo_emissione_avvpag { get; set; }

            [DisplayName("Importo min. da pagare")]
            public Nullable<decimal> importo_minimo_da_pagare { get; set; }

            [DisplayName("Importo min. sollecito bonario")]
            public Nullable<decimal> importo_limite_sollecito_bonario { get; set; }

            [DisplayName("Num. anni prescrizione")]
            public Nullable<int> AA_prescrizione_avviso { get; set; }

            [DisplayName("Num. gg per avviso definitivo")]
            public Nullable<int> GG_definitivita_avviso { get; set; }

            [DisplayName("Num. gg max per avviso non notificato")]
            public Nullable<int> GG_massimi_data_emissione { get; set; }

            [DisplayName("Num. gg min per emissione atto successivo")]
            public Nullable<int> GG_minimi_data_notifica { get; set; }

            [DisplayName("Num. gg max per avviso non valido")]
            public Nullable<int> GG_massimi_data_notifica { get; set; }

            [DisplayName("Responsabile Notifica")]
            public Nullable<int> id_risorsa_resp_notifica_tipo_avvpag { get; set; }

            [DisplayName("Modalità sped/notifica P.F. fuori Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_fuori_comune_PF { get; set; }
            [DisplayName("Modalità sped/notifica P.G. fuori Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_fuori_comune_PG { get; set; }
            [DisplayName("Modalità sped/notifica P.F. nel Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_nel_comune_PF { get; set; }
            [DisplayName("Modalità sped/notifica P.G. nel Comune")]
            [Required]
            public Nullable<int> modalita_sped_not_nel_comune_PG { get; set; }
            [DisplayName("Modalità sped/notifica per Estero")]
            [Required]
            public Nullable<int> modalita_sped_not_estero { get; set; }
            [DisplayName("CC riscossione")]
            [Required]
            public Nullable<int> id_cc_riscossione { get; set; }
            [DisplayName("Importo Min. da pagare avv. precedenti")]
            public decimal importo_minimo_avvpag_collegati { get; set; }
            [DisplayName("Num. gg min. da notifica avv. precedenti")]
            public int GG_minimi_notifica_avvpag_collegati { get; set; }
            [DisplayName("Num. Max rate insolute per annullamento")]
            public int num_rate_insolute { get; set; }
            [DisplayName("GG Max per rata insoluta")]
            public int num_gg_max_scadenza_rata_insoluta { get; set; }
            [DisplayName("Num. GG scadenza pagamento")]
            public Nullable<int> GG_scadenza_pagamento { get; set; }
            [DisplayName("Stampa F24 e non bollettini")]
            public string flag_stampa_F24 { get; set; }
        }
    }
}
