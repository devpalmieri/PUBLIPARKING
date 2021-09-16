using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Publisoftware.Data.Interface;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_terzo.Metadata))]
    public partial class tab_terzo : ISoftDeleted, IGestioneStato, IValidator, /*IValidatableObject,*/ IContribuenteReferenteCampiComuni
    {
        public const string ATT_ATT = "ATT-ATT";
        public const int ATT_ATT_ID = 1;

        public const string ATT_CES = "ATT-CES";
        public const string ANN = "ANN-";

        public static int TIPO_FISICO = 1;
        public static int TIPO_GIURIDICO = 2;
       
        public Nullable<System.DateTime> DataInizioStatoRO => data_inizio_stato;
        public Nullable<System.DateTime> DataFineStatoRO => data_fine_stato;
        public Nullable<System.DateTime> DataStatoRO => data_stato;

        public int? idTipo { get { return id_tipo_terzo; } }

        public decimal PkIdAsDecimal { get { return id_terzo; } }

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_terzo), typeof(tab_terzo.Metadata)), typeof(tab_terzo));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        public void SetUserStato(int p_struttura, int p_risorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_struttura;
            id_risorsa_stato = p_risorsa;
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

        [DisplayName("Data Inizio Stato")]
        public string data_inizio_stato_String
        {
            get
            {
                if (data_inizio_stato.HasValue)
                {
                    return data_inizio_stato.Value.ToShortDateString();
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
                    data_inizio_stato = DateTime.Parse(value);
                }
                else
                {
                    data_inizio_stato = null;
                }
            }
        }

        [DisplayName("Data Fine Stato")]
        public string data_fine_stato_String
        {
            get
            {
                if (data_fine_stato.HasValue)
                {
                    return data_fine_stato.Value.ToShortDateString();
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
                    data_fine_stato = DateTime.Parse(value);
                }
                else
                {
                    data_fine_stato = null;
                }
            }
        }

        [DisplayName("Data Inizio Validità Indirizzo")]
        public string data_inizio_validita_indirizzo_String
        {
            get
            {
                if (data_inizio_validita_indirizzo.HasValue)
                {
                    return data_inizio_validita_indirizzo.Value.ToShortDateString();
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
                    data_inizio_validita_indirizzo = DateTime.Parse(value);
                }
                else
                {
                    data_inizio_validita_indirizzo = null;
                }
            }
        }

        [DisplayName("Data Fine Validità Indirizzo")]
        public string data_fine_validita_indirizzo_String
        {
            get
            {
                if (data_fine_validita_indirizzo.HasValue)
                {
                    return data_fine_validita_indirizzo.Value.ToShortDateString();
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
                    data_fine_validita_indirizzo = DateTime.Parse(value);
                }
                else
                {
                    data_fine_validita_indirizzo = null;
                }
            }
        }

        [DisplayName("Data Inizio Validità Altri Dati")]
        public string data_inizio_validita_altri_dati_String
        {
            get
            {
                if (data_inizio_validita_altri_dati.HasValue)
                {
                    return data_inizio_validita_altri_dati.Value.ToShortDateString();
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
                    data_inizio_validita_altri_dati = DateTime.Parse(value);
                }
                else
                {
                    data_inizio_validita_altri_dati = null;
                }
            }
        }

        [DisplayName("Data Fine Validità Altri Dati")]
        public string data_fine_validita_altri_dati_String
        {
            get
            {
                if (data_fine_validita_altri_dati.HasValue)
                {
                    return data_fine_validita_altri_dati.Value.ToShortDateString();
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
                    data_fine_validita_altri_dati = DateTime.Parse(value);
                }
                else
                {
                    data_fine_validita_altri_dati = null;
                }
            }
        }

        [DisplayName("Data Nascita")]
        public string data_nas_String
        {
            get
            {
                if (data_nas.HasValue)
                {
                    return data_nas.Value.ToShortDateString();
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
                    data_nas = DateTime.Parse(value);
                }
                else
                {
                    data_nas = null;
                }
            }
        }

        [DisplayName("Data Decesso")]
        public string data_morte_String
        {
            get
            {
                if (data_morte.HasValue)
                {
                    return data_morte.Value.ToShortDateString();
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
                    data_morte = DateTime.Parse(value);
                }
                else
                {
                    data_morte = null;
                }
            }
        }

        public string sesso
        {
            get
            {
                if (id_sesso == 1)
                {
                    return "M";
                }
                else if (id_sesso == 2)
                {
                    return "F";
                }
                else
                {
                    return null;
                }
            }
        }

        public string civico
        {
            get
            {
                if (sigla_civico != null && sigla_civico != string.Empty)
                {
                    if (nr_civico.HasValue && nr_civico != 0)
                    {
                        return nr_civico.Value + "/" + sigla_civico;
                    }
                    else
                    {
                        return sigla_civico;
                    }
                }
                else
                {
                    if (nr_civico.HasValue)
                    {
                        return nr_civico.Value.ToString();
                    }
                    {
                        return string.Empty;
                    }
                }
            }
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

        public string nominativoDisplay
        {
            get
            {
                if (isPersonaFisica)
                {
                    return cognome + " " + nome;
                }
                else if (!string.IsNullOrEmpty(rag_sociale))
                {
                    return rag_sociale;
                }
                else if (!string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return denominazione_commerciale;
                }
                else
                {
                    return cognome + " " + nome;
                }
            }
        }

        public string cognomeRagSoc
        {
            get
            {
                if (isPersonaFisica)
                {
                    return cognome;
                }
                else
                {
                    return rag_sociale;
                }
            }
        }

        public string codFiscalePivaDisplay
        {
            get
            {
                if (isPersonaFisica)
                {
                    return cod_fiscale ?? string.Empty;
                }
                else
                {
                    return p_iva ?? string.Empty;
                }
            }
        }

        public string terzoDisplay
        {
            get
            {
                if (isPersonaFisica)
                {
                    return nome + " " + cognome + " - " + cod_fiscale;
                }
                else if (!string.IsNullOrEmpty(rag_sociale) && !string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return rag_sociale + " - " + denominazione_commerciale + " - " + p_iva;
                }
                else if (!string.IsNullOrEmpty(rag_sociale))
                {
                    return rag_sociale + " - " + p_iva;
                }
                else if (!string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return denominazione_commerciale + " - " + p_iva;
                }
                else
                {
                    return nome + " " + cognome + " - " + cod_fiscale + "/" + p_iva;
                }
            }
        }

        public string terzoNominativoDisplay
        {
            get
            {
                if (isPersonaFisica)
                {
                    return cognome + " " + nome;
                }
                else if (!string.IsNullOrEmpty(rag_sociale) && !string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return rag_sociale + " - " + denominazione_commerciale;
                }
                else if (!string.IsNullOrEmpty(rag_sociale))
                {
                    return rag_sociale;
                }
                else if (!string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return denominazione_commerciale;
                }
                else
                {
                    return cognome + " " + nome;
                }
            }
        }

        public string terzoNominativoDisplayPA
        {
            get
            {
                if (isPersonaFisica)
                {
                    return nome + " " + cognome;
                }
                else if (!string.IsNullOrEmpty(rag_sociale) && !string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return rag_sociale + " - " + denominazione_commerciale;
                }
                else if (!string.IsNullOrEmpty(rag_sociale))
                {
                    return rag_sociale;
                }
                else if (!string.IsNullOrEmpty(denominazione_commerciale))
                {
                    return denominazione_commerciale;
                }
                else
                {
                    return nome + " " + cognome;
                }
            }
        }

        public string indirizzoDisplay
        {
            get
            {
                if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                {
                    return tab_toponimi.descrizione_toponimo;
                }
                else
                {
                    return indirizzo;
                }
            }
        }

        public string cittaDisplay
        {
            get
            {
                if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                {
                    if (tab_toponimi.ser_comuni != null)
                    {
                        return tab_toponimi.ser_comuni.des_comune;
                    }
                    else
                    {
                        return citta;
                    }
                }
                else
                {
                    return citta;
                }
            }
        }

        public string provinciaDisplay
        {
            get
            {
                if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                {
                    if (tab_toponimi.ser_comuni != null)
                    {
                        if (tab_toponimi.ser_comuni.ser_province != null)
                        {
                            return tab_toponimi.ser_comuni.ser_province.sig_provincia;
                        }
                        else
                        {
                            return prov;
                        }
                    }
                    else
                    {
                        return prov;
                    }
                }
                else
                {
                    return prov;
                }
            }
        }

        public int? CodCitta
        {
            get
            {
                if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                {
                    if (tab_toponimi.ser_comuni != null)
                    {
                        return tab_toponimi.ser_comuni.cod_comune;
                    }
                    else
                    {
                        return cod_citta;
                    }
                }
                else
                {
                    return cod_citta;
                }
            }
        }

        public string indirizzoTotaleDisplay
        {
            get
            {
                return ((indirizzoDisplay != null && indirizzoDisplay != string.Empty) ? indirizzoDisplay : string.Empty) +
                       ((civico != null && civico != string.Empty) ? " " + civico : string.Empty) +
                       ((sigla_civico != null && sigla_civico != string.Empty) ? " " + sigla_civico.Trim() : string.Empty) +
                       ((colore != null && colore != string.Empty) ? " " + colore.Trim() : string.Empty) +
                       ((km.HasValue) ? ", km: " + km : string.Empty) +
                       ((cap != null && cap != string.Empty) ? " - " + cap : string.Empty) +
                       ((cittaDisplay != null && cittaDisplay != string.Empty) ? "  " + cittaDisplay : string.Empty) +
                       ((prov != null && prov != string.Empty) ? " (" + prov + ")" : string.Empty);
            }
        }

        public string indirizzoRaccomandata
        {
            get
            {
                return ((indirizzoDisplay != null && indirizzoDisplay != string.Empty) ? indirizzoDisplay : string.Empty) +
                       ((civico != null && civico != string.Empty && civico.Trim() != "0") ? " " + civico : string.Empty) +
                       ((sigla_civico != null && sigla_civico != string.Empty && sigla_civico != "SNC") ? " " + sigla_civico.Trim() : string.Empty) +
                       ((colore != null && colore != string.Empty) ? " " + colore.Trim() : string.Empty) +
                       ((km.HasValue) ? ", km: " + km : string.Empty);
            }
        }

        public string CapComuneProvinciaPerStampa
        {
            get
            {
                return ((cap != null && cap != string.Empty) ? cap : string.Empty) + " " +
                       ((cittaDisplay != null && cittaDisplay != string.Empty) ? cittaDisplay : string.Empty) +
                       ((prov != null && prov != string.Empty) ? " " + prov : string.Empty);
            }
        }

        public string terzoNominativoDisplay_Stampa
        {
            get
            {
                TextInfo rt = new CultureInfo("it-IT", false).TextInfo;
                string nominativo = string.Empty;
                if (cod_fiscale != null && cod_fiscale != string.Empty)
                {
                    nominativo = nome + " " + cognome;
                    nominativo = rt.ToLower(nominativo);
                    return rt.ToTitleCase(nominativo);
                }
                else
                {
                    nominativo = rag_sociale;
                    nominativo = rt.ToLower(nominativo);
                    return rt.ToTitleCase(nominativo);
                }
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_terzo { get; set; }

            [DisplayName("Cognome")]
            public string cognome { get; set; }

            [DisplayName("Nome")]
            public string nome { get; set; }

            [DisplayName("Codice Fiscale")]
            public string cod_fiscale { get; set; }

            [DisplayName("Ragione Sociale")]
            public string rag_sociale { get; set; }

            [DisplayName("Denominazione Commerciale")]
            public string denominazione_commerciale { get; set; }

            [DisplayName("P.IVA")]
            public string p_iva { get; set; }

            [DisplayName("Data Nascita")]
            public DateTime? data_nas { get; set; }

            [IsDateAfter("data_nas", true, ErrorMessage = "La data di decesso deve essere maggiore della data di nascita")]
            [DisplayName("Data Decesso")]
            public DateTime? data_morte { get; set; }

            [DisplayName("Codice Comune Nascita")]
            public int cod_comune_nas { get; set; }

            [DisplayName("Comune Nascita")]
            public string comune_nas { get; set; }

            [DisplayName("Nazione Nascita")]
            public string stato_nas { get; set; }

            [DisplayName("Codice Comune")]
            public int? cod_citta { get; set; }

            [DisplayName("Comune")]
            public string citta { get; set; }

            [RegularExpression(tab_contribuente.capRegex, ErrorMessage = "Formato non valido")]
            [DisplayName("CAP")]
            public string cap { get; set; }

            [DisplayName("Provincia")]
            public string prov { get; set; }

            [DisplayName("Nazione")]
            public string stato { get; set; }

            [DisplayName("Indirizzo")]
            public string indirizzo { get; set; }

            [DisplayName("Nr Civico")]
            [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public int? nr_civico { get; set; }

            [DisplayName("Sigla Civico")]
            [RegularExpression(tab_contribuente.codiceCivicoRegex, ErrorMessage = tab_contribuente.codiceCivicoRegexErrMsg)]
            public string sigla_civico { get; set; }

            [DisplayName("Frazione")]
            public string frazione { get; set; }

            [DisplayName("Colore")]
            [RegularExpression(tab_contribuente.coloreRegexp, ErrorMessage = "Formato non valido: inserire una lettera (Ad es. R per Rosso, N per Nero)")]
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

            [DisplayName("Note")]
            public string note { get; set; }

            [DisplayName("Email")]
            //[EmailAddress]
            //[RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Formato email non valido")]
            [RegularExpression(tab_contribuente.emailPecRegularExpression, ErrorMessage = "Formato email non valido")]
            public string e_mail { get; set; }

            [DisplayName("Email pec")]
            //[EmailAddress]
            //[RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Formato email non valido")]
            [RegularExpression(tab_contribuente.emailPecRegularExpression, ErrorMessage = "Formato email non valido")]
            public string pec { get; set; }

            [DisplayName("Telefono")]
            // [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public string tel { get; set; }

            [DisplayName("Cellulare")]
            // [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public string cell { get; set; }

            [DisplayName("Fax")]
            // [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public string fax { get; set; }

            [DisplayName("Data Inizio Stato")]
            public DateTime data_inizio_stato { get; set; }

            //[IsDateAfter("data_inizio_stato", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            [DisplayName("Data Fine Stato")]
            public DateTime? data_fine_stato { get; set; }

            [DisplayName("Data Inizio Validità")]
            public DateTime data_inizio_validita_indirizzo { get; set; }

            //[IsDateAfter("data_inizio_validita_indirizzo", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            [DisplayName("Data Fine Validità")]
            public DateTime? data_fine_validita_indirizzo { get; set; }

            [DisplayName("Data Inizio Validità")]
            public DateTime data_inizio_validita_altri_dati { get; set; }

            //[IsDateAfter("data_inizio_validita_altri_dati", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            [DisplayName("Data Fine Validità")]
            public DateTime? data_fine_validita_altri_dati { get; set; }
        }

        //Il dottore e Luigi hanno voluto togliere la validazione per i terzi
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (anagrafica_tipo_contribuente != null)
        //    {
        //        if (anagrafica_tipo_contribuente.sigla_tipo_contribuente != anagrafica_tipo_contribuente.PERS_FISICA)
        //        {
        //            if (p_iva == null || p_iva == string.Empty)
        //            {
        //                yield return new ValidationResult
        //                 ("Inserire la P. IVA", new[] { "p_iva" });
        //            }
        //            else
        //            {
        //                Regex regex = new Regex("^[0-9]{11}$");
        //                Match match = regex.Match(p_iva);
        //                if (!match.Success)
        //                {
        //                    yield return new ValidationResult
        //                     ("Formato Non Valido", new[] { "p_iva" });
        //                }
        //            }
        //        }
        //        else if (anagrafica_tipo_contribuente.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA)
        //        {
        //            if (cod_fiscale == null || cod_fiscale == string.Empty)
        //            {
        //                yield return new ValidationResult
        //                 ("Inserire il Codice Fiscale", new[] { "cod_fiscale" });
        //            }
        //            else
        //            {
        //                Regex regex = new Regex("^[a-zA-Z]{6}[a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{3}[a-zA-Z]$");
        //                Match match = regex.Match(cod_fiscale);
        //                if (!match.Success)
        //                {
        //                    yield return new ValidationResult
        //                     ("Formato non valido", new[] { "cod_fiscale" });
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
