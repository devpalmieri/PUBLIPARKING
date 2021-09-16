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

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_oggetti.Metadata))]
    public partial class tab_oggetti : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const int ATT_ATT_ID = 1;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string NumCivico
        {
            get
            {
                string numcivicodaAsString = num_civico_da.HasValue ? Convert.ToString(num_civico_da.Value) : string.Empty;
                string numcivicoaAsString = num_civico_a.HasValue ? " - " + Convert.ToString(num_civico_a.Value) : string.Empty;

                if (numcivicodaAsString == string.Empty && numcivicoaAsString == string.Empty)
                {
                    if (string.IsNullOrEmpty(indirizzo))
                    {
                        numcivicodaAsString = string.Empty;
                    }
                    else
                    {
                        numcivicodaAsString = "SNC";
                    }
                }

                return numcivicodaAsString + " " + numcivicoaAsString;
            }
        }

        public string SiglaCivico
        {
            get
            {
                string siglacivicodaAsString = (sigla_civico_da != null && sigla_civico_da != string.Empty) ? Convert.ToString(sigla_civico_da) : string.Empty;
                string siglacivicoaAsString = (sigla_civico_a != null && sigla_civico_a != string.Empty) ? " - " + Convert.ToString(sigla_civico_a) : string.Empty;

                if (siglacivicodaAsString == string.Empty && siglacivicoaAsString == string.Empty && NumCivico == "SNC")
                {
                    siglacivicodaAsString = "SNC";
                }

                return siglacivicodaAsString + " " + siglacivicoaAsString;
            }
        }

        public string indirizzoDisplay
        {
            get
            {
                if (tab_toponimi != null)
                {
                    return tab_toponimi.descrizione_toponimo;
                }
                else
                {
                    return indirizzo;
                }
            }
        }

        public string provinciaDisplay
        {
            get
            {
                if (tab_toponimi != null)
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

        public string frazioneDisplay
        {
            get
            {
                if (tab_toponimi != null)
                {
                    return tab_toponimi.frazione_toponimo;
                }
                else
                {
                    return frazione;
                }
            }
        }

        public string cittaDisplay
        {
            get
            {
                if (tab_toponimi != null)
                {
                    return tab_toponimi.ser_comuni.des_comune;
                }
                else
                {
                    return citta;
                }
            }
        }

        private string _ubicazione = null;
        [DisplayName("Ubicazione")]
        public string Ubicazione
        {
            get
            {
                if (_ubicazione == null)
                {
                    string v_temp = string.Empty;

                    string numcivicodaAsString = num_civico_da.HasValue ? Convert.ToString(num_civico_da.Value) : string.Empty;
                    string numcivicoaAsString = num_civico_a.HasValue ? " - " + Convert.ToString(num_civico_a.Value) : string.Empty;

                    string sigla_civico = sigla_civico_da;

                    if (numcivicodaAsString == string.Empty && numcivicoaAsString == string.Empty)
                    {
                        // numcivicodaAsString = "SNC";
                        if (!string.IsNullOrWhiteSpace(sigla_civico_da))
                        {
                            sigla_civico = "SNC";
                        }
                    }

                    if (tab_toponimi != null)
                    {
                        if (tab_toponimi.ser_comuni != null)
                        {
                            if (id_ente == anagrafica_ente.ID_ENTE_CITL)
                            {
                                if (tab_toponimi.ser_comuni.ser_province != null)
                                {
                                    v_temp = tab_toponimi.descrizione_toponimo + " " + numcivicodaAsString + " " + numcivicoaAsString + " " + sigla_civico + " " + tab_toponimi.ser_comuni.des_comune + " " + tab_toponimi.ser_comuni.cap_comune + " " + tab_toponimi.ser_comuni.ser_province.sig_provincia;
                                }
                                else
                                {
                                    v_temp = tab_toponimi.descrizione_toponimo + " " + numcivicodaAsString + " " + numcivicoaAsString + " " + " " + sigla_civico + tab_toponimi.ser_comuni.des_comune + " " + tab_toponimi.ser_comuni.cap_comune;
                                }
                            }
                            else
                            {
                                if (tab_toponimi.ser_comuni.ser_province != null)
                                {
                                    v_temp = tab_toponimi.descrizione_toponimo + " " + numcivicodaAsString + " " + numcivicoaAsString + " " + sigla_civico;
                                }
                                else
                                {
                                    v_temp = tab_toponimi.descrizione_toponimo + " " + numcivicodaAsString + " " + numcivicoaAsString + " " + sigla_civico;
                                }
                            }
                        }
                        else
                        {
                            v_temp = tab_toponimi.descrizione_toponimo + " " + numcivicodaAsString + " " + numcivicoaAsString + " " + sigla_civico;
                        }
                    }
                    else
                    {
                        v_temp = indirizzo + " " + numcivicodaAsString + " " + numcivicoaAsString + " " + sigla_civico;
                    }

                    //if (!string.IsNullOrWhiteSpace(sigla_civico_da))
                    //{
                    //    v_temp += " " + sigla_civico_da;
                    //}

                    _ubicazione = v_temp;
                    return _ubicazione;
                }
                else
                {
                    return _ubicazione;
                }
            }
            set
            {
                _ubicazione = value;
            }
        }

        public string OggettoDes
        {
            get
            {
                return string.IsNullOrEmpty(tab_tipo_oggetto.flag_descrizione_oggetto) ?
                      (string.IsNullOrEmpty(tab_tipo_oggetto.flag_ubicazione_oggetto) ? string.Empty : tab_tipo_oggetto.des_tipo_oggetto + " - " + tab_tipo_oggetto.dicitura_descrizione_oggetto) :
                       tab_tipo_oggetto.des_tipo_oggetto + " - " + tab_tipo_oggetto.dicitura_descrizione_oggetto + ": " + descrizione_oggetto;
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}
