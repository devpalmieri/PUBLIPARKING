using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_ricorsi.Metadata))]
    public partial class tab_ricorsi : ISoftDeleted, IGestioneStato
    {
        public static string ConMediazione = "1";
        public static string SenzaMediazione = "0";

        //flag_tipo_ricorrente
        public const string ENTE = "E";
        public const string CONCESSIONARIO = "C";
        public const string SOGGETTO_DEBITORE = "D";

        //flag_individuazione_ricorrente
        public const string CONTRIBUENTE = "C";
        public const string REFERENTE = "R";
        public const string ALTRO = "A";

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

        public string importo_riferimento_ricorso_Euro
        {
            get
            {
                if (importo_riferimento_ricorso.HasValue)
                {
                    return importo_riferimento_ricorso.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string data_iscrizione_ruolo_String
        {
            get
            {
                if (data_iscrizione_ruolo.HasValue)
                {
                    return data_iscrizione_ruolo.Value.ToShortDateString();
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
                    data_iscrizione_ruolo = DateTime.Parse(value);
                }
                else
                {
                    data_iscrizione_ruolo = null;
                }
            }
        }

        public string data_scadenza_controdeduzioni_ricorso_String
        {
            get
            {
                if (data_scadenza_controdeduzioni_ricorso.HasValue)
                {
                    return data_scadenza_controdeduzioni_ricorso.Value.ToShortDateString();
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
                    data_scadenza_controdeduzioni_ricorso = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_controdeduzioni_ricorso = null;
                }
            }
        }

        public string data_presentazione_memorie_ricorso_String
        {
            get
            {
                if (data_presentazione_memorie_ricorso.HasValue)
                {
                    return data_presentazione_memorie_ricorso.Value.ToShortDateString();
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
                    data_presentazione_memorie_ricorso = DateTime.Parse(value);
                }
                else
                {
                    data_presentazione_memorie_ricorso = null;
                }
            }
        }

        public string data_scadenza_memorie_ricorso_String
        {
            get
            {
                if (data_scadenza_memorie_ricorso.HasValue)
                {
                    return data_scadenza_memorie_ricorso.Value.ToShortDateString();
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
                    data_scadenza_memorie_ricorso = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_memorie_ricorso = null;
                }
            }
        }

        public string data_scadenza_mediazione_String
        {
            get
            {
                if (data_scadenza_mediazione.HasValue)
                {
                    return data_scadenza_mediazione.Value.ToShortDateString();
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
                    data_scadenza_mediazione = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_mediazione = null;
                }
            }
        }

        public string data_scadenza_costituzione_giudizio_ricorrente_String
        {
            get
            {
                if (data_scadenza_costituzione_giudizio_ricorrente.HasValue)
                {
                    return data_scadenza_costituzione_giudizio_ricorrente.Value.ToShortDateString();
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
                    data_scadenza_costituzione_giudizio_ricorrente = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_costituzione_giudizio_ricorrente = null;
                }
            }
        }

        public string data_scadenza_costituzione_giudizio_resistente_String
        {
            get
            {
                if (data_scadenza_costituzione_giudizio_resistente.HasValue)
                {
                    return data_scadenza_costituzione_giudizio_resistente.Value.ToShortDateString();
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
                    data_scadenza_costituzione_giudizio_resistente = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_costituzione_giudizio_resistente = null;
                }
            }
        }

        public string data_prima_udienza_String
        {
            get
            {
                if (data_prima_udienza.HasValue)
                {
                    return data_prima_udienza.Value.ToShortDateString();
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
                    data_prima_udienza = DateTime.Parse(value);
                }
                else
                {
                    data_prima_udienza = null;
                }
            }
        }

        public string data_udienza_successiva_String
        {
            get
            {
                if (data_udienza_successiva.HasValue)
                {
                    return data_udienza_successiva.Value.ToShortDateString();
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
                    data_udienza_successiva = DateTime.Parse(value);
                }
                else
                {
                    data_udienza_successiva = null;
                }
            }
        }

        public string data_udienza_disposizione_chiamata_in_causa_String
        {
            get
            {
                if (data_udienza_disposizione_chiamata_in_causa.HasValue)
                {
                    return data_udienza_disposizione_chiamata_in_causa.Value.ToShortDateString();
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
                    data_udienza_disposizione_chiamata_in_causa = DateTime.Parse(value);
                }
                else
                {
                    data_udienza_disposizione_chiamata_in_causa = null;
                }
            }
        }

        public string data_richiesta_definizione_bonaria_String
        {
            get
            {
                if (data_richiesta_definizione_bonaria.HasValue)
                {
                    return data_richiesta_definizione_bonaria.Value.ToShortDateString();
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
                    data_richiesta_definizione_bonaria = DateTime.Parse(value);
                }
                else
                {
                    data_richiesta_definizione_bonaria = null;
                }
            }
        }

        public string TipoRicorrenteDescrizione
        {
            get
            {
                if (string.IsNullOrEmpty(flag_tipo_ricorrente) ||
                    flag_tipo_ricorrente == "D")
                {
                    return "Soggetto Debitore";
                }
                else if (flag_tipo_ricorrente == "C")
                {
                    return "Concessionario";
                }
                else if (flag_tipo_ricorrente == "E")
                {
                    return "Ente Creditore";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

        }
    }
}