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
    [MetadataTypeAttribute(typeof(tab_documenti.Metadata))]
    public partial class tab_documenti : ISoftDeleted, IGestioneStato, IValidatableObject, IValidator
    {
        public const string CONTRIBUENTE = "Contribuente";
        public const string REFERENTE = "Referente";
        public const string RELAZIONEREFERENTE = "RelazioneReferente";
        public const string TERZO = "Terzo";
        public const string PRATICHE = "Pratiche";
        public const string DOMICILIAZIONI = "Domiciliazioni";
        public const string FASCICOLI = "Fascicoli";
        public const string FASCICOLIAVVPAG = "FascicoliAvvisi";
        public const string FASCICOLIDOCOUTPUT = "FascicoliDocOutput";

        public const int ATT_ATT_ID = 1;
        public const string ATT_ATT = "ATT-ATT";

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_documenti), typeof(tab_documenti.Metadata)), typeof(tab_documenti));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        public bool IsValid
        {
            get { return checkValidity(); }
        }

        protected bool checkValidity()
        {
            return this.validationErrors().Count == 0;
        }

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_risorsa_stato = p_idRisorsa;
            id_struttura_stato = p_idStruttura;
        }

        [DisplayName("Data Rilascio Documento")]
        public string data_rilascio_documento_String
        {
            get
            {
                if (data_rilascio_documento.HasValue)
                {
                    return data_rilascio_documento.Value.ToShortDateString();
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
                    data_rilascio_documento = DateTime.Parse(value);
                }
                else
                {
                    data_rilascio_documento = null;
                }
            }
        }

        [DisplayName("Data Inizio Validità")]
        public string data_validita_documento_da_String
        {
            get
            {
                if (data_validita_documento_da.HasValue)
                {
                    return data_validita_documento_da.Value.ToShortDateString();
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
                    data_validita_documento_da = DateTime.Parse(value);
                }
                else
                {
                    data_validita_documento_da = null;
                }
            }
        }

        [DisplayName("Data Fine Validità")]
        public string data_validita_documento_a_String
        {
            get
            {
                if (data_validita_documento_a.HasValue)
                {
                    return data_validita_documento_a.Value.ToShortDateString();
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
                    data_validita_documento_a = DateTime.Parse(value);
                }
                else
                {
                    data_validita_documento_a = null;
                }
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_documenti { get; set; }

            [DisplayName("Numero Documento")]
            public string num_documento { get; set; }

            [DisplayName("Ente Rilascio Documento")]
            public string descr_ente_rilascio_documento { get; set; }

            [DisplayName("Data Rilascio Documento")]
            public DateTime data_rilascio_documento { get; set; }

            [DisplayName("Data Inizio Validità")]
            public DateTime data_validita_documento_da { get; set; }

            [IsDateAfter("data_validita_documento_da", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            [DisplayName("Data Fine Validità")]
            public DateTime? data_validita_documento_a { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (id_tab_documenti < 0)
            {
                yield return new ValidationResult
                 ("Errore", new[] { "id_tab_documenti" });
            }
        }
    }
}