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
    public partial class tab_agevolazioni_verifica : ISoftDeleted, IGestioneStato
    {
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

        public string Riduzione
        {
            get
            {
                return quantita_agevolazione != null ? quantita_agevolazione + "%" : string.Empty;
            }
        }

        public string Rinnovo
        {
            get
            {
                if (string.IsNullOrEmpty(anagrafica_agevolazione.flag_rinnovo_automatico) || anagrafica_agevolazione.flag_rinnovo_automatico == "0")
                {
                    return "Rinnovo automatico";
                }
                else if (anagrafica_agevolazione.flag_rinnovo_automatico == "1")
                {
                    return "Su istanza approvata";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Occupante
        {
            get
            {
                if (anagrafica_agevolazione.flag_unico_occupante == "1")
                {
                    return "Unico occupante";
                }
                else if (anagrafica_agevolazione.flag_unico_occupante == "2")
                {
                    return "Unico occupante over 65";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Periodo Validità Inizio")]
        public string periodo_validita_da_String
        {
            get
            {
                if (data_inizio_validita.HasValue)
                {
                    return data_inizio_validita.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Periodo Validità Fine")]
        public string periodo_validita_a_String
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
        }

        private string _periodoValidita = null;
        [DisplayName("Periodo Validità")]
        public string periodo_validita_String
        {
            get
            {
                if (_periodoValidita == null)
                {
                    string v_temp = string.Empty;

                    if (periodo_validita_da_String != string.Empty)
                    {
                        v_temp = periodo_validita_da_String + " - " + (periodo_validita_a_String == string.Empty ? "Ad oggi" : periodo_validita_a_String);
                    }

                    _periodoValidita = v_temp;
                    return _periodoValidita;
                }
                else
                {
                    return _periodoValidita;
                }
            }
            set
            {
                _periodoValidita = value;
            }
        }
    }
}
