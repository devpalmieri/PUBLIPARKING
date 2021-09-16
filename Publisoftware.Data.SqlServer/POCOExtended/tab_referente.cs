using Publisoftware.Data.CustomValidationAttrs;
using Publisoftware.Data.Interface;
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
    [MetadataTypeAttribute(typeof(tab_referente.Metadata))]
    public partial class tab_referente : IValidator, ISoftDeleted, IGestioneStato, IValidatableObject, IContribuenteReferenteCampiComuni
    {
        public const string NON_COOBBLIGATO = "0";
        public const string COOBBLIGATO = "1";
        public const string COOBBLIGATO_PARZIALE = "2";
        public const string GARANTE = "3";

        public const string FONTE_IMP = tab_contribuente.FONTE_IMP;
        public const int SESSO_MASCHIO = tab_contribuente.SESSO_MASCHIO;
        public const int SESSO_FEMMINA = tab_contribuente.SESSO_FEMMINA;

        public const string STATO_IT = tab_contribuente.STATO_IT;
        public const string STATO_ITALIA = tab_contribuente.STATO_ITALIA;

        public Nullable<System.DateTime> DataInizioStatoRO { get { return data_inizio_stato; } }
        public Nullable<System.DateTime> DataFineStatoRO { get { return data_fine_stato; } }
        public Nullable<System.DateTime> DataStatoRO { get { return data_stato; } }

        public int? idTipo { get { return id_tipo_referente; } }

        public decimal PkIdAsDecimal { get { return id_tab_referente; } }

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_referente), typeof(tab_referente.Metadata)), typeof(tab_referente));

            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, true);

            return results;
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = System.DateTime.Now;
            id_risorsa_stato = p_idRisorsa;
            id_struttura_stato = p_idStruttura;
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

        public IEnumerable<join_referente_contribuente> joinReferentiFisici()
        {
            return join_referente_contribuente.Where(d => d.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente == anagrafica_tipo_contribuente.PERS_FISICA && 
                                                          d.data_inizio_validita <= DateTime.Now && 
                                                         (d.data_fine_validita.HasValue ? d.data_fine_validita.Value : DateTime.MaxValue) >= DateTime.Now)
                                              .OrderBy(d => d.anagrafica_tipo_relazione.cod_tipo_relazione)
                                              .ThenBy(d => d.anagrafica_tipo_relazione.desc_tipo_relazione)
                                              .ThenBy(d => d.tab_contribuente.cognome)
                                              .ThenBy(d => d.tab_contribuente.nome)
                                              .ThenBy(d => d.tab_contribuente.data_nas);
        }

        public IEnumerable<join_referente_contribuente> joinReferentiGiuridici()
        {
            return join_referente_contribuente.Where(d => (d.tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente != anagrafica_tipo_contribuente.PERS_FISICA) && 
                                                           d.data_inizio_validita <= DateTime.Now &&
                                                          (d.data_fine_validita.HasValue ? d.data_fine_validita.Value : DateTime.MaxValue) >= DateTime.Now)
                                              .OrderBy(d => d.anagrafica_tipo_relazione.cod_tipo_relazione)
                                              .ThenBy(d => d.anagrafica_tipo_relazione.desc_tipo_relazione)
                                              .ThenBy(d => d.tab_contribuente.rag_sociale)
                                              .ThenBy(d => d.tab_contribuente.denominazione_commerciale);
        }

        public ICollection<join_referente_contribuente> join_referente_contribuente_delegante
        {
            get { return this.join_referente_contribuente1; }
            set { this.join_referente_contribuente1 = value; }
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

        public DateTime? dataNas
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.data_nas;
                }
                else
                {
                    return data_nas;
                }
            }
        }

        public DateTime? dataMorte
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.data_morte;
                }
                else
                {
                    return data_morte;
                }
            }
        }

        public string data_nas_StringDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.data_nas_String;
                }
                else
                {
                    return data_nas_String;
                }
            }
        }

        public string data_morte_StringDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.data_morte_String;
                }
                else
                {
                    return data_morte_String;
                }
            }
        }

        public string cognomeDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cognome;
                }
                else
                {
                    return cognome;
                }
            }
        }

        public string nomeDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.nome;
                }
                else
                {
                    return nome;
                }
            }
        }

        public string cod_fiscaleDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cod_fiscale;
                }
                else
                {
                    return cod_fiscale;
                }
            }
        }

        public string p_ivaDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.p_iva;
                }
                else
                {
                    return p_iva;
                }
            }
        }

        public string rag_socialeDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.rag_sociale;
                }
                else
                {
                    return rag_sociale;
                }
            }
        }

        public string denominazione_commercialeDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.denominazione_commerciale;
                }
                else
                {
                    return denominazione_commerciale;
                }
            }
        }

        public int? idTipoReferente
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.anagrafica_tipo_contribuente.id_tipo_contribuente;
                }
                else
                {
                    return id_tipo_referente;
                }
            }
        }

        public string descTipoReferente
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.anagrafica_tipo_contribuente.desc_tipo_contribuente;
                }
                else
                {
                    return anagrafica_tipo_contribuente != null ? anagrafica_tipo_contribuente.desc_tipo_contribuente : string.Empty;
                }
            }
        }

        public string siglaTipoReferente
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.anagrafica_tipo_contribuente.sigla_tipo_contribuente;
                }
                else
                {
                    return anagrafica_tipo_contribuente != null ? anagrafica_tipo_contribuente.sigla_tipo_contribuente : string.Empty;
                }
            }
        }

        public string EmailDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.e_mail;
                }
                else
                {
                    return e_mail;
                }
            }
        }

        public string PecDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.pec;
                }
                else
                {
                    return pec;
                }
            }
        }

        public string TelDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.tel;
                }
                else
                {
                    return tel;
                }
            }
        }

        public string CellDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cell;
                }
                else
                {
                    return cell;
                }
            }
        }

        public string FaxDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.fax;
                }
                else
                {
                    return fax;
                }
            }
        }

        public string NoteDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.note;
                }
                else
                {
                    return note;
                }
            }
        }

        public string StatoDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.stato;
                }
                else
                {
                    return stato;
                }
            }
        }

        public int? idToponimo
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.id_toponimo;
                }
                else
                {
                    return id_toponimo;
                }
            }
        }

        public int? idStradeDbPoste
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.id_strada_db_poste;
                }
                else
                {
                    return id_strada_db_poste;
                }
            }
        }

        public int? NrCivico
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.nr_civico;
                }
                else
                {
                    return nr_civico;
                }
            }
        }

        public string SiglaCivico
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.sigla_civico;
                }
                else
                {
                    return sigla_civico;
                }
            }
        }

        public string CapDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cap;
                }
                else
                {
                    return cap;
                }
            }
        }

        public string FrazioneDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.frazione;
                }
                else
                {
                    return frazione;
                }
            }
        }

        public string EdificioDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.edificio;
                }
                else
                {
                    return edificio;
                }
            }
        }

        public string ColoreDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.colore;
                }
                else
                {
                    return colore;
                }
            }
        }

        public decimal? KmDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.km;
                }
                else
                {
                    return km;
                }
            }
        }

        public string CondominioDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.condominio;
                }
                else
                {
                    return condominio;
                }
            }
        }

        public string ScalaDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.scala;
                }
                else
                {
                    return scala;
                }
            }
        }

        public string PianoDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.piano;
                }
                else
                {
                    return piano;
                }
            }
        }

        public string InternoDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.interno;
                }
                else
                {
                    return interno;
                }
            }
        }

        public int? idSesso
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.id_sesso;
                }
                else
                {
                    return id_sesso;
                }
            }
        }

        public string StatoNas
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.stato_nas;
                }
                else
                {
                    return stato_nas;
                }
            }
        }

        public string ComuneNas
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.comune_nas;
                }
                else
                {
                    return comune_nas;
                }
            }
        }

        public int? CodComuneNas
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cod_comune_nas;
                }
                else
                {
                    return cod_comune_nas;
                }
            }
        }

        public string codStato
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cod_stato_contribuente;
                }
                else
                {
                    return cod_stato_referente;
                }
            }
        }

        public string sesso
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.sesso;
                }
                else
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
        }

        public string civico
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.civico;
                }
                else
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
        }

        public bool isPersonaFisica
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.isPersonaFisica;
                }
                else
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
        }

        public string nominativoDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.nominativoDisplay;
                }

                if (isPersonaFisica)
                {
                    return cognome + " " + nome;
                }
                else if (rag_sociale != null && rag_sociale != string.Empty)
                {
                    return rag_sociale;
                }
                else if (denominazione_commerciale != null && denominazione_commerciale != string.Empty)
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
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cognomeRagSoc;
                }

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
                if (tab_contribuente != null)
                {
                    return tab_contribuente.codFiscalePivaDisplay;
                }
                else
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
        }

        public string referenteDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.contribuenteDisplay;
                }
                else
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
        }

        public string referenteNominativoDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.contribuenteNominativoDisplay;
                }
                else
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
        }

        public string referenteNominativoDisplayPA
        {
            get
            {
                if (tab_contribuente != null)
                {
                    if (tab_contribuente.isPersonaFisica)
                    {
                        return tab_contribuente.nome + " " + tab_contribuente.cognome;
                    }
                    else if (!string.IsNullOrEmpty(tab_contribuente.rag_sociale))
                    {
                        return tab_contribuente.rag_sociale;
                    }
                    else if (!string.IsNullOrEmpty(tab_contribuente.denominazione_commerciale))
                    {
                        return tab_contribuente.denominazione_commerciale;
                    }
                    else
                    {
                        return tab_contribuente.nome + " " + tab_contribuente.cognome;
                    }
                }
                else
                {
                    if (isPersonaFisica)
                    {
                        return nome + " " + cognome;
                    }
                    else if (!string.IsNullOrEmpty(tab_contribuente.rag_sociale))
                    {
                        return rag_sociale;
                    }
                    else if (!string.IsNullOrEmpty(tab_contribuente.denominazione_commerciale))
                    {
                        return denominazione_commerciale;
                    }
                    else
                    {
                        return nome + " " + cognome;
                    }
                }
            }
        }

        public string indirizzoDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.indirizzoDisplay;
                }
                else
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
        }

        public string cittaDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.cittaDisplay;
                }
                else
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
        }

        public string provinciaDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.provinciaDisplay;
                }
                else
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
        }

        public string provinciaEstesaDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.provinciaEstesaDisplay;
                }
                else
                {
                    if (tab_toponimi != null && tab_toponimi.id_tipo_toponimo != 0)
                    {
                        if (tab_toponimi.ser_comuni != null)
                        {
                            if (tab_toponimi.ser_comuni.ser_province != null)
                            {
                                return tab_toponimi.ser_comuni.ser_province.des_provincia;
                            }
                            else
                            {
                                return ser_comuni.ser_province.des_provincia;
                            }
                        }
                        else
                        {
                            return ser_comuni.ser_province.des_provincia;
                        }
                    }
                    else
                    {
                        return ser_comuni.ser_province.des_provincia;
                    }
                }
            }
        }

        public int? CodCitta
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.CodCitta;
                }
                else
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
        }

        public string indirizzoTotaleDisplay
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.indirizzoTotaleDisplay;
                }
                else
                {
                    return ((indirizzoDisplay != null && indirizzoDisplay != string.Empty) ? indirizzoDisplay : string.Empty) +
                           ((civico != null && civico != string.Empty && civico.Trim() != "0") ? " " + civico : string.Empty) +
                           ((sigla_civico != null && sigla_civico != string.Empty) ? " " + sigla_civico.Trim() : string.Empty) +
                           ((colore != null && colore != string.Empty) ? " " + colore.Trim() : string.Empty) +
                           ((km.HasValue) ? ", km: " + km : string.Empty) +
                           ((cap != null && cap != string.Empty) ? " - " + cap : string.Empty) +
                           ((cittaDisplay != null && cittaDisplay != string.Empty) ? "  " + cittaDisplay : string.Empty) +
                           ((prov != null && prov != string.Empty) ? " (" + prov + ")" : string.Empty);
                }
            }
        }

        public string indirizzoRaccomandata
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.indirizzoRaccomandata;
                }
                else
                {
                    return ((indirizzoDisplay != null && indirizzoDisplay != string.Empty) ? indirizzoDisplay : string.Empty) +
                           ((civico != null && civico != string.Empty && civico.Trim() != "0") ? " " + civico : string.Empty) +
                           ((sigla_civico != null && sigla_civico != string.Empty && sigla_civico != "SNC") ? " " + sigla_civico.Trim() : string.Empty) +
                           ((condominio != null && condominio != string.Empty) ? " " + condominio.Trim() : string.Empty) +
                           ((colore != null && colore != string.Empty) ? " " + colore.Trim() : string.Empty) +
                           ((km.HasValue) ? ", km: " + km : string.Empty);
                }
            }
        }

        public string CapComuneProvinciaPerStampa
        {
            get
            {
                if (tab_contribuente != null)
                {
                    return tab_contribuente.CapComuneProvinciaPerStampa;
                }
                else
                {
                    return ((cap != null && cap != string.Empty) ? cap : string.Empty) + " " +
                           ((cittaDisplay != null && cittaDisplay != string.Empty) ? cittaDisplay : string.Empty) +
                           ((prov != null && prov != string.Empty) ? " " + prov : string.Empty);
                }
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tab_referente { get; set; }

            [DisplayName("Id Contribuente")]
            public int id_anag_contribuente { get; set; }

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
            // 10/12/2019: vedi tab_contribuente! 
            // [RegularExpression("[0-9]{0,}", ErrorMessage = "Formato non valido")]
            public string cell { get; set; }

            [DisplayName("Fax")]
            // 10/12/2019: vedi tab_contribuente! 
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (true)
            {
                //Il dottore ha voluto eliminare questa validazione perchè (come nel caso delle procedure concorsuali) c'è la necessità di inserire referenti senza CF
            }
            else if (id_anag_contribuente == null)
            {
                if (anagrafica_tipo_contribuente != null)
                {
                    if (anagrafica_tipo_contribuente.sigla_tipo_contribuente != anagrafica_tipo_contribuente.PERS_FISICA)
                    {
                        // 23/02/2018 piva/cf possono essere vuoti se il flag_verifica_cf_piva == "1"
                        if (flag_verifica_cf_piva != "1" && (p_iva == null || p_iva == string.Empty))
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
                        // 23/02/2018 piva/cf possono essere vuoti se il flag_verifica_cf_piva == "1"
                        if (flag_verifica_cf_piva != "1" && (cod_fiscale == null || cod_fiscale == string.Empty))
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
}
