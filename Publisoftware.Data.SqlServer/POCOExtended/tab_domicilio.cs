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
    [MetadataTypeAttribute(typeof(tab_domicilio.Metadata))]
    public partial class tab_domicilio : IValidator, ISoftDeleted, IGestioneStato, IValidatableObject
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ATT_CES = "ATT-CES";
        public const string ATT_VAR = "ATT-VAR";

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_domicilio), typeof(tab_domicilio.Metadata)), typeof(tab_domicilio));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        static Dictionary<string, string> _statiDomicilioDictionaryEnum = new Dictionary<string, string> {
            { ATT_ATT, "ATTIVO" },
            { ATT_CES, "CESSATO" },
            { ATT_VAR, "VARIATO" }
        };
        public static Dictionary<string, string> StatiDomicilioDictionaryEnum { get { return _statiDomicilioDictionaryEnum; } }

        public string statoDomicilioTotale
        {
            get
            {
                if (cod_stato != null && StatiDomicilioDictionaryEnum.ContainsKey(cod_stato))
                {
                    return cod_stato + " - " + StatiDomicilioDictionaryEnum[cod_stato];
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
            id_risorsa_stato = p_idRisorsa;
            id_struttura_stato = p_idStruttura;
        }

        public bool isPersonaFisica
        {
            get
            {
                if (anagrafica_tipo_contribuente != null)
                {
                    return anagrafica_tipo_contribuente.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool isEqual(tab_domicilio v_domicilio)
        {
            if (v_domicilio.indirizzo == indirizzo &&
                v_domicilio.nr_civico == nr_civico &&
                v_domicilio.sigla_civico == sigla_civico &&
                v_domicilio.cap == cap &&
                v_domicilio.prov == prov &&
                v_domicilio.cod_citta == cod_citta &&
                v_domicilio.citta == citta &&
                v_domicilio.frazione == frazione)
            {
                return true;
            }
            else if (v_domicilio.id_toponimo.HasValue &&
                    v_domicilio.tab_toponimi.ser_comuni != null &&
                    !id_toponimo.HasValue &&
                    v_domicilio.tab_toponimi.descrizione_toponimo == indirizzo &&
                    v_domicilio.nr_civico == nr_civico &&
                    v_domicilio.sigla_civico == sigla_civico &&
                    v_domicilio.cap == cap &&
                    v_domicilio.tab_toponimi.ser_comuni.ser_province.sig_provincia == prov &&
                    v_domicilio.tab_toponimi.cod_comune_toponimo == cod_citta &&
                    v_domicilio.tab_toponimi.ser_comuni.des_comune == citta &&
                    v_domicilio.tab_toponimi.frazione_toponimo == frazione)
            {
                return true;
            }
            else if (!v_domicilio.id_toponimo.HasValue &&
                    id_toponimo.HasValue &&
                    tab_toponimi.ser_comuni != null &&
                    v_domicilio.indirizzo == tab_toponimi.descrizione_toponimo &&
                    v_domicilio.nr_civico == nr_civico &&
                    v_domicilio.sigla_civico == sigla_civico &&
                    v_domicilio.cap == cap &&
                    v_domicilio.prov == tab_toponimi.ser_comuni.ser_province.sig_provincia &&
                    v_domicilio.cod_citta == tab_toponimi.cod_comune_toponimo &&
                    v_domicilio.citta == tab_toponimi.ser_comuni.des_comune &&
                    v_domicilio.frazione == tab_toponimi.frazione_toponimo)
            {
                return true;
            }
            else if (v_domicilio.id_toponimo.HasValue &&
                    id_toponimo.HasValue &&
                    v_domicilio.id_toponimo == id_toponimo &&
                    v_domicilio.nr_civico == nr_civico &&
                    v_domicilio.sigla_civico == sigla_civico &&
                    v_domicilio.cap == cap)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        [DisplayName("Data Inizio")]
        public string data_inizio_String
        {
            get
            {
                return data_inizio.ToShortDateString();
            }
            set
            {
                data_inizio = DateTime.Parse(value);
            }
        }

        [DisplayName("Data Fine")]
        public string data_fine_String
        {
            get
            {
                if (data_fine.HasValue)
                {
                    return data_fine.Value.ToShortDateString();
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
                    data_fine = DateTime.Parse(value);
                }
                else
                {
                    data_fine = null;
                }
            }
        }

        public string civico
        {
            get
            {
                if (this.sigla_civico != null && this.sigla_civico != string.Empty)
                {
                    return this.nr_civico + "/" + this.sigla_civico;
                }
                else
                {
                    return this.nr_civico.ToString();
                }
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_domicilio { get; set; }

            [DisplayName("Id Contribuente")]
            public int id_anag_contribuente { get; set; }

            [DisplayName("Cognome")]
            public string cognome { get; set; }

            [DisplayName("Nome")]
            public string nome { get; set; }

            [DisplayName("Ragione Sociale")]
            public string rag_sociale { get; set; }

            [DisplayName("Codice Fiscale")]
            public string cod_fiscale { get; set; }

            [DisplayName("P.IVA")]
            public string p_iva { get; set; }

            [DisplayName("Casella Postale")]
            public string casella_postale { get; set; }
           
            //[Required(ErrorMessage = "Inserire un comune valido")]
            [DisplayName("Codice Comune")]
            public int? cod_citta { get; set; }

            [DisplayName("Comune")]
            public string citta { get; set; }

            [RegularExpression(tab_contribuente.capRegex, ErrorMessage = "Formato CAP non valido")]
            //[Required(ErrorMessage = "CAP obbligatorio: inserire comune/indirizzo/civico/km")]
            [DisplayName("CAP")]
            public string cap { get; set; }

            //[Required(ErrorMessage = "Inserire la provincia")]
            [DisplayName("Provincia")]
            public string prov { get; set; }

            //[Required(ErrorMessage = "Inserire la nazione")]
            [DisplayName("Nazione")]
            public string stato { get; set; }

            //[Required(ErrorMessage = "Inserire l'indirizzo")]
            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [DisplayName("Nr Civico")]
            [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public int? nr_civico { get; set; }

            [DisplayName("Sigla Civico")]
            // [RegularExpression("^[a-zA-Z]{0,1}$|^[0-9]{0,4}$|^[a-zA-Z]{0,1}[0-9]{0,4}$|^\\bBIS\\b$|^\\bTER\\b$|^\\bQUATER\\b$|^\\bSNC\\b$", ErrorMessage = "Formato non valido: inserire SNC/BIS/TER/QUATER, una lettera, un numero (max 4 cifre) o una lettera e un numero (max 4 cifre)")]
            [RegularExpression(tab_contribuente.codiceCivicoRegex, ErrorMessage = tab_contribuente.codiceCivicoRegexErrMsg)]
            public string sigla_civico { get; set; }

            [DisplayName("Frazione")]
            public string frazione { get; set; }

            [DisplayName("Colore")]
            [RegularExpression("^[a-zA-Z]{0,1}$", ErrorMessage = "Formato non valido: inserire una lettera (Ad es. R per Rosso, N per Nero)")]
            public string colore { get; set; }

            [DisplayName("Condominio")]
            public string condominio { get; set; }

            [DisplayName("Interno")]
            public string interno { get; set; }

            [DisplayName("Scala")]
            public string scala { get; set; }

            [DisplayName("Piano")]
            public string piano { get; set; }

            [DisplayName("KM")]
            [RegularExpression(@"[\d]{1,5}([,][\d]{1,3})?", ErrorMessage = "Formato non valido")]
            public string km { get; set; }

            [DisplayName("Data Inizio")]
            public DateTime data_inizio { get; set; }

            [IsDateAfter("data_inizio", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            [DisplayName("Data Fine")]
            public DateTime? data_fine { get; set; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (anagrafica_tipo_contribuente != null)
            {
                if (anagrafica_tipo_contribuente.sigla_tipo_contribuente != anagrafica_tipo_contribuente.PERS_FISICA)
                {
                    if (p_iva == null || p_iva == string.Empty)
                    {
                        yield return new ValidationResult
                         ("Inserire la P. IVA", new[] { "p_iva" });
                    }
                    else
                    {
                        Regex regex = new Regex("^[0-9]{11}$");
                        Match match = regex.Match(p_iva);
                        if (!match.Success)
                        {
                            yield return new ValidationResult
                             ("Formato Non Valido", new[] { "p_iva" });
                        }
                    }
                }
                else if (anagrafica_tipo_contribuente.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA)
                {
                    if (cod_fiscale == null || cod_fiscale == string.Empty)
                    {
                        yield return new ValidationResult
                         ("Inserire il Codice Fiscale", new[] { "cod_fiscale" });
                    }
                    else
                    {
                        Regex regex = new Regex("^[a-zA-Z]{6}[a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{3}[a-zA-Z]$");
                        Match match = regex.Match(cod_fiscale);
                        if (!match.Success)
                        {
                            yield return new ValidationResult
                             ("Formato non valido", new[] { "cod_fiscale" });
                        }
                    }
                }
            }
        }
    }
}
