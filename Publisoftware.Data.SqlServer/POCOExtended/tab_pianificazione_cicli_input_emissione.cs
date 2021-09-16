using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_pianificazione_cicli_input_emissione.Metadata))]
    public partial class tab_pianificazione_cicli_input_emissione : IValidatableObject
    {
        public tab_pianificazione_cicli_input_emissione()
        {
            TypeDescriptor.AddProviderTransparent(
                new AssociatedMetadataTypeTypeDescriptionProvider(
                    typeof(tab_pianificazione_cicli_input_emissione), typeof(tab_pianificazione_cicli_input_emissione.Metadata)), typeof(tab_pianificazione_cicli_input_emissione));
        }

        public List<ValidationResult> validationErrors()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            return results;            
        }
            
        [DisplayName("Controllo Anagrafico")]
        public bool controllo_anagrafico_Ext
        {
            get
            {
                return this.controllo_anagrafico.HasValue ? this.controllo_anagrafico.Value : false;
            }
            set
            {
                this.controllo_anagrafico = value;                                
            }
        }

        [DisplayName("Creazione Bollettini")]
        public bool creazione_bollettini_Ext
        {
            get
            {
                return this.creazione_bollettini.HasValue ? this.creazione_bollettini.Value : false;
            }
            set
            {
                this.creazione_bollettini = value;
            }
        }

        [DisplayName("Arrotondamento")]
        public bool arrotondamento_Ext
        {
            get
            {
                return this.arrotondamento.HasValue ? this.arrotondamento.Value : false;
            }
            set
            {
                this.arrotondamento = value;
            }
        }

        [DisplayName("Tutte le entrate")]
        public bool ingiunzioni_entrate_tutte_Ext
        {
            get
            {
                return this.ingiunzioni_entrate_tutte.HasValue ? this.ingiunzioni_entrate_tutte.Value : false;
            }
            set
            {
                if (value)
                {
                    this.ingiunzioni_entrate_tutte = true;
                }
                else
                {
                    this.ingiunzioni_entrate_tutte = false;
                }
            }
        }


        internal sealed class Metadata
        {
            public Metadata()
            {
            }

            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_ordinario_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_ordinario_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_ordinario_a { get; set; }


            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_accertamento_1_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_accertamento_1_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_accertamento_1_a { get; set; }

            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_accertamento_2_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_accertamento_2_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_accertamento_2_a { get; set; }

            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_accertamento_3_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_accertamento_3_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_accertamento_3_a { get; set; }


            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_accertamento_4_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_accertamento_4_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_accertamento_4_a { get; set; }

            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_accertamento_5_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_accertamento_5_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_accertamento_5_a { get; set; }


            [DisplayName("Da")]
            public Nullable<System.DateTime> intimazioni_solleciti_rif_ing_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("intimazioni_solleciti_rif_ing_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> intimazioni_solleciti_rif_ing_a { get; set; }

            [DisplayName("Da")]
            public Nullable<System.DateTime> precoa_bonari_data_importo_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("precoa_bonari_data_importo_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> precoa_bonari_data_importo_a { get; set; }

            [DisplayName("Da")]
            public Nullable<System.DateTime> periodo_rif_fattura_idrica_da { get; set; }

            [DisplayName("a")]
            [IsDateAfter("periodo_rif_fattura_idrica_da", true, ErrorMessage = "La data di fine deve essere successiva di quella inizio")]
            public Nullable<System.DateTime> periodo_rif_fattura_idrica_a { get; set; }


            [Range(1900, int.MaxValue, ErrorMessage = "Selezionare un anno di riferimento")]
            [DisplayName("Anno rif ordinario")]
            public int anno_rif_ordinario { get; set; }

            [Range(1900, int.MaxValue, ErrorMessage = "Selezionare un anno di riferimento")]
            [DisplayName("Anno rif fattura idrica")]
            public int anno_rif_fattura_idrica { get; set; }

            [RegularExpression("[A|S]{1,1}", ErrorMessage = "Selezionare tipo fattura valido Es: A = Acconto or S = Saldo")]
            [DisplayName("Tipo calcolo fattura")]
            public string fattura_tipo_calcolo { get; set; }                

            [DisplayName("Anno accertamento 1")]
            public int anno_rif_accertamento_1 { get; set; }

            [DisplayName("Anno accertamento 2")]
            public int anno_rif_accertamento_2 { get; set; }

            [DisplayName("Anno accertamento 3")]
            public int anno_rif_accertamento_3 { get; set; }

            [DisplayName("Anno accertamento 4")]
            public int anno_rif_accertamento_4 { get; set; }

            [DisplayName("Anno accertamento 5")]
            public int anno_rif_accertamento_5 { get; set; }
                        

            [DisplayName("Tutte le entrate")]
            public Nullable<bool> ingiunzioni_entrate_tutte { get; set; }
            
            [DisplayName("Entrata 1")]
            public Nullable<int> ingiunzioni_entrata_1 { get; set; }
            [DisplayName("Entrata 2")]
            public Nullable<int> ingiunzioni_entrata_2 { get; set; }
            [DisplayName("Entrata 3")]
            public Nullable<int> ingiunzioni_entrata_3 { get; set; }
            [DisplayName("Entrata 4")]
            public Nullable<int> ingiunzioni_entrata_4 { get; set; }
            [DisplayName("Entrata")]
            public Nullable<int> ingiunzioni_entrata_5 { get; set; }
            [DisplayName("Entrata")]
            public Nullable<int> ingiunzioni_entrata_6 { get; set; }
            [DisplayName("Entrata")]
            public Nullable<int> ingiunzioni_entrata_7 { get; set; }
            [DisplayName("Entrata")]
            public Nullable<int> ingiunzioni_entrata_8 { get; set; }
            [DisplayName("Entrata")]
            public Nullable<int> ingiunzioni_entrata_9 { get; set; }
            [DisplayName("Entrata")]
            public Nullable<int> ingiunzioni_entrata_10 { get; set; }

            [DisplayName("Tutti i solleciti")]
            public Nullable<bool> intimazioni_solleciti_rif_ing_tutte { get; set; }

            [DisplayName("Importo bonari da ")]
            [Range(0, int.MaxValue, ErrorMessage = "Selezionare un importo valido")]
            public Nullable<int> precoa_bonari_importo_da { get; set; }
            [DisplayName("Importo bonari a")]
            [Range(0, int.MaxValue, ErrorMessage = "Selezionare un importo valido")]
            public Nullable<int> precoa_bonari_importo_a { get; set; }

            [DisplayName("Tutti i solleciti")]
            public Nullable<bool> avvintimazioni_solleciti_rif_ing_tutte { get; set; }

            [DisplayName("Lista di rif 1")]
            public Nullable<int> id_lista_rif_1 { get; set; }
            [DisplayName("Lista di rif 2")]
            public Nullable<int> id_lista_rif_2 { get; set; }
            [DisplayName("Lista di rif 3")]
            public Nullable<int> id_lista_rif_3 { get; set; }
            [DisplayName("Lista di rif 4")]
            public Nullable<int> id_lista_rif_4 { get; set; }
            [DisplayName("Lista di rif 5")]
            public Nullable<int> id_lista_rif_5 { get; set; }

            //[Required]
            //[Range(0, int.MaxValue, ErrorMessage = "Selezionare un numero di rate valido")]
            //[DisplayName("Numero rate")]
            //public int numero_rate { get; set; }

            //[DisplayName("Scadenza rata unica")]
            //public Nullable<DateTime> data_scadenza_rata_unica { get; set; }

            //[DisplayName("Scadenza prima rata")] 
            //public Nullable<DateTime> data_scadenza_rata_1 { get; set; }

            //[DisplayName("Scadenza seconda rata")]
            //[IsDateAfter("data_scadenza_rata_1", true, ErrorMessage = "La scadenza della seconda rata deve essere successiva a quella della prima")]
            //public Nullable<DateTime> data_scadenza_rata_2 { get; set; }
                                   
            //[DisplayName("Scadenza terza rata")]
            //[IsDateAfter("data_scadenza_rata_2", true, ErrorMessage = "La scadenza della terza rata deve essere successiva a quella della seconda")]
            //public Nullable<DateTime> data_scadenza_rata_3 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_3", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_4 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_4", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_5 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_5", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_6 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_6", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_7 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_7", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_8 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_8", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_9 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_9", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_10 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_10", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_11 { get; set; }

            //[DisplayName("Scadenza quarta rata")]
            //[IsDateAfter("data_scadenza_rata_11", true, ErrorMessage = "La scadenza della quarta rata deve essere successiva a quella della terza")]
            //public Nullable<DateTime> data_scadenza_rata_12 { get; set; }


            [DisplayName("Importo minimo in Euro")]
            [Range(0, int.MaxValue, ErrorMessage = "Selezionare un importo valido")]
            public Nullable<int> importo_minimo_avvpag { get; set; }
            
            [DisplayName("Importo massimo in Euro")]
            [Range(0, int.MaxValue, ErrorMessage = "Selezionare un importo valido")]
            public Nullable<int> importo_massimo_avvpag { get; set; }


      

            //[Required]
            //[Range(0, int.MaxValue, ErrorMessage = "Selezionare una periodicità valida")]
            //[DisplayName("Periodicità rate ")]
            //public int periodicita_rate { get; set; }

            //[Required]
            //[DisplayName("Numero mesi trascorsi")]
            //public Nullable<int> num_mesi_trascorsi { get; set; }
  

        }
    }
}