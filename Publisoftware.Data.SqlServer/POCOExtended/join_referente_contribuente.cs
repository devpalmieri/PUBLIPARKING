using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(join_referente_contribuente.Metadata))]
    public partial class join_referente_contribuente : IValidator, ISoftDeleted, IGestioneStato, IValidatableObject
    {
        public const string ATT = "ATT-";
        public const string ANN = "ANN-";
        public const string ANN_ANN = "ANN-ANN";
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_CES = "ATT-CES";
        public const string ANN_CES = "ANN-CES";
        public const string ANN_ATT = "ANN-ATT";

        public tab_referente tab_referente_delegante {
            get { return this.tab_referente1; }
            set { this.tab_referente = value; }
        }

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_domicilio), typeof(tab_domicilio.Metadata)), typeof(tab_domicilio));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public bool IsValid
        {
            get { return checkValidity(); }
        }

        protected bool checkValidity()
        {
            return this.validationErrors().Count == 0;
        }

        static Dictionary<string, string> _statiJoinReferenteContribuenteDictionaryEnum = new Dictionary<string, string> { { ATT_ATT, "ATTIVO" }, { ATT_CES, "CESSATO" }, { ANN_CES, "ANNULLATO DA CESSATO" }, { ANN_ATT, "ANNULLATO DA ATTIVO" } };
        public static Dictionary<string, string> StatiJoinReferenteContribuenteDomicilioDictionaryEnum { get { return _statiJoinReferenteContribuenteDictionaryEnum; } }

        public string statoJoinReferenteContribuenteTotale
        {
            get
            {
                if (cod_stato != null)
                {
                    return cod_stato + " - " + StatiJoinReferenteContribuenteDomicilioDictionaryEnum[cod_stato];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        [DisplayName("Data Inizio")]
        public string data_inizio_validita_String
        {
            get
            {
                return data_inizio_validita.ToShortDateString();
            }
            set
            {
                data_inizio_validita = DateTime.Parse(value);
            }
        }

        [DisplayName("Data Fine")]
        public string data_fine_validita_String
        {
            get
            {
                if (data_fine_validita.HasValue)
                {
                    return data_fine_validita.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    data_fine_validita = DateTime.Parse(value);
                }
                else
                {
                    data_fine_validita = null;
                }
            }
        }

        public string messaggioCoobbligato
        {
            get
            {
                string v_messaggioCoobbligato = string.Empty;

                if (tab_contribuente.isPersonaFisica && tab_contribuente.cod_stato_contribuente.StartsWith(anagrafica_stato_contribuente.DEC) && flag_coobbligato == tab_referente.COOBBLIGATO && coobbligazione_percentuale == 100)
                {
                    v_messaggioCoobbligato = "Contribuente deceduto - Utente coobbligato in solido con il contribuente per eventuali situazioni di morosità";
                }
                else if (tab_contribuente.isPersonaFisica && tab_contribuente.cod_stato_contribuente.StartsWith(anagrafica_stato_contribuente.DEC) && flag_coobbligato == tab_referente.COOBBLIGATO_PARZIALE && coobbligazione_percentuale < 100)
                {
                    v_messaggioCoobbligato = "Contribuente deceduto - Utente coobbligato al " + coobbligazione_percentuale + " % con il contribuente per eventuali situazioni di morosità";
                }
                else if (!tab_contribuente.isPersonaFisica && flag_coobbligato == tab_referente.COOBBLIGATO && coobbligazione_percentuale == 100)
                {
                    v_messaggioCoobbligato = "Utente coobbligato in solido con il contribuente per eventuali situazioni di morosità";
                }
                else if (!tab_contribuente.isPersonaFisica && flag_coobbligato == tab_referente.COOBBLIGATO_PARZIALE && coobbligazione_percentuale < 100)
                {
                    v_messaggioCoobbligato = "Utente coobbligato al " + coobbligazione_percentuale + " % con il contribuente per eventuali situazioni di morosità";
                }

                return v_messaggioCoobbligato;
            }
        }

        public string coobbligato
        {
            get
            {
                return flag_coobbligato == tab_referente.COOBBLIGATO ? "Coobbligato in solido" : (flag_coobbligato == tab_referente.COOBBLIGATO_PARZIALE ? "Coobbligato parziale" : (flag_coobbligato == tab_referente.GARANTE ? "Garante" : "Nessuna coobbligazione"));
            }
        }

        public string parentela
        {
            get
            {
                return anagrafica_parentela != null ? anagrafica_parentela.descrizione_parentela : "Nessuna";
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_join_referente_contribuente { get; set; }

            [DisplayName("Id Referente")]
            public int id_tab_referente { get; set; }

            [DisplayName("Id Contribuente")]
            public int id_anag_contribuente { get; set; }

            [DisplayName("Id Relazione")]
            public int id_tipo_relazione { get; set; }

            [DisplayName("Id Parentela")]
            public int id_anagrafica_parentela { get; set; }

            [DisplayName("Livello Autorizzativo")]
            [RegularExpression("[0-3]{1,1}", ErrorMessage = "Il livello deve essere un numero compreso tra 0 e 3")]
            public int livello_autorizzazione_interno { get; set; }

            [DisplayName("Flag Coobbligato")]
            public string flag_coobbligato { get; set; }

            [DisplayName("Coobbligazione Percentuale")]
            [Range(0,100,ErrorMessage = "La percentuale deve essere un numero compreso tra 0 e 100")]
            public decimal coobbligazione_percentuale { get; set; }

            [DisplayName("Importo massimo Obbligazione")]
            [Range(0.0, Double.MaxValue, ErrorMessage = "Inserire solo valori positivi")]
            public decimal importo_max_obbligazione { get; set; }

            [DisplayName("Data Inizio")]
            public DateTime data_inizio_validita { get; set; }

            [IsDateAfter("data_inizio_validita", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            [DisplayName("Data Fine")]
            public DateTime? data_fine_validita { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (id_join_referente_contribuente < 0)
            {
                yield return new ValidationResult
                 ("id_join_referente_contribuente", new[] { "id_join_referente_contribuente" });
            }
        }
    }
}
