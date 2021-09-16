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
    [MetadataTypeAttribute(typeof(tab_mov_pag.Metadata))]
    public partial class tab_mov_pag : ISoftDeleted, IGestioneStato, IValidator, IValidatableObject
    {

        public List<ValidationResult> validationErrors()
        {
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(tab_mov_pag), typeof(tab_mov_pag.Metadata)), typeof(tab_mov_pag));

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

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public string Codice
        {
            get
            {
                if (!string.IsNullOrEmpty(code_line))
                {
                    return code_line;
                }
                else if (!string.IsNullOrEmpty(iuv_pagopa))
                {
                    return iuv_pagopa;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string NumeroCC
        {
            get
            {
                if (tab_cc_riscossione != null && tab_cc_riscossione.num_cc != null)
                {
                    return tab_cc_riscossione.num_cc;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string IntestazioneCC
        {
            get
            {
                if (tab_cc_riscossione != null && tab_cc_riscossione.intestazione_cc != null)
                {
                    return tab_cc_riscossione.intestazione_cc;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string IBAN
        {
            get
            {
                if (tab_cc_riscossione != null && tab_cc_riscossione.IBAN != null)
                {
                    return tab_cc_riscossione.IBAN;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string IdCUAS
        {
            get
            {
                if (id_cuas != null)
                {
                    return id_cuas.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string UfficioPostale
        {
            get
            {
                if (uff_postale != null)
                {
                    return uff_postale;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_operazione_String
        {
            get
            {
                if (data_operazione.HasValue)
                {
                    return data_operazione.Value.ToShortDateString();
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
                    data_operazione = DateTime.Parse(value);
                }
                else
                {
                    data_operazione = null;
                }
            }
        }

        public string data_accredito_String
        {
            get
            {
                if (data_accredito.HasValue)
                {
                    return data_accredito.Value.ToShortDateString();
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
                    data_accredito = DateTime.Parse(value);
                }
                else
                {
                    data_accredito = null;
                }
            }
        }

        public string data_valuta_String
        {
            get
            {
                if (data_valuta.HasValue)
                {
                    return data_valuta.Value.ToShortDateString();
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
                    data_valuta = DateTime.Parse(value);
                }
                else
                {
                    data_valuta = null;
                }
            }
        }

        public string importo_mov_pagato_Euro
        {
            get
            {
                if (importo_mov_pagato.HasValue)
                {
                    return importo_mov_pagato.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string Stato
        {
            get
            {
                if (join_avv_pag_mov_pag.Count == 0 && !cod_stato.StartsWith(anagrafica_stato_mov_pag.RAV))
                {
                    return anagrafica_stato_mov_pag != null ? anagrafica_stato_mov_pag.descr_stato_mov_pag : string.Empty + ".";
                }
                else if (join_avv_pag_mov_pag.Count == 0 && cod_stato.StartsWith(anagrafica_stato_mov_pag.RAV))
                {
                    return anagrafica_stato_mov_pag != null ? anagrafica_stato_mov_pag.descr_stato_mov_pag : string.Empty + ".";
                }
                else
                {
                    if (anagrafica_stato_mov_pag == null)
                    {
                        if (join_avv_pag_mov_pag.Where(d => !d.cod_stato.StartsWith(Data.join_avv_pag_mov_pag.ANN)).Sum(d => d.importo_pagato) < (importo_mov_pagato.HasValue ? importo_mov_pagato.Value : 0))
                        {
                            return "Movimento parzialmente accoppiato";
                        }
                        else
                        {
                            return "Movimento interamente accoppiato.";
                        }
                        //return cod_stato ?? "-";
                    }
                    else
                    {
                        return anagrafica_stato_mov_pag.descr_stato_mov_pag ?? (cod_stato ?? "-");
                    }
                }
            }
        }

        public string TipoPulsante
        {
            get
            {
                if (join_avv_pag_mov_pag.Count == 0 && !cod_stato.StartsWith(anagrafica_stato_mov_pag.RAV))
                {
                    return "0";
                }
                else if (join_avv_pag_mov_pag.Count == 0 && cod_stato.StartsWith(anagrafica_stato_mov_pag.RAV))
                {
                    return "1";
                }
                else
                {
                    if (tab_tipo_pagamento.id_tipo_pag == tab_tipo_pagamento.BollInternetICI ||
                        tab_tipo_pagamento.id_tipo_pag == tab_tipo_pagamento.BollOrdinarioICI ||
                        tab_tipo_pagamento.id_tipo_pag == tab_tipo_pagamento.BonificoICI ||
                        tab_tipo_pagamento.id_tipo_pag == tab_tipo_pagamento.PagamentiImportatiICIIMU ||
                        tab_tipo_pagamento.id_tipo_pag == tab_tipo_pagamento.PagamentoF24ICI)
                    {
                        return "2";
                    }
                    else
                    {
                        return "3";
                    }
                }
            }
        }

        public string ContribuenteDisplay
        {
            get
            {
                //return tab_contribuente != null ? tab_contribuente.contribuenteDisplay : string.Empty;

                StringBuilder sb = new StringBuilder();

                if (cognome_rag_soc != null)
                {
                    sb.Append(cognome_rag_soc);
                    sb.Append(" ");
                }

                if (nome != null)
                {
                    sb.Append(nome);
                    sb.Append(" ");
                }

                if (codice_fiscale != null)
                {
                    sb.Append(codice_fiscale);
                    sb.Append(" ");
                }

                return sb.ToString();
            }
        }

        public string PaganteDisplay
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                //if (cognome_rag_soc != null)
                //{
                //    sb.Append(cognome_rag_soc);
                //    sb.Append(" ");
                //}

                //if (nome != null)
                //{
                //    sb.Append(nome);
                //    sb.Append(" ");
                //}

                if (cf_piva_pagante != null)
                {
                    sb.Append(cf_piva_pagante);
                    sb.Append(" ");
                }

                return sb.ToString();
            }
        }

        public class MessaggioStatoMovPag
        {
            public static readonly MessaggioStatoMovPag Empty = new MessaggioStatoMovPag { Stato = "" };
            public static readonly MessaggioStatoMovPag Unknown = new MessaggioStatoMovPag { Stato = "N.P." };
            public string Stato { get; set; }
            //public string TipoPulsante { get; set; }

            public static MessaggioStatoMovPag ByTabMovPag(tab_mov_pag movPag, decimal? current_contribuente_or_null)
            {
                string codStato = movPag.cod_stato ?? ""; // Oppure movPag.anagrafica_stato_mov_pag.cod_stato_mov_pag?

                // TODO: Pietro: aggiungere id_terzo a tab_mov_pag!
                int? id_terzo = null; //

                if (codStato.StartsWith(anagrafica_stato_mov_pag.ACCOPPIATO))
                {

                    if (current_contribuente_or_null != null && movPag.id_contribuente == current_contribuente_or_null.Value)
                    {
                        return new MessaggioStatoMovPag
                        {
                            Stato = "pagam. attrib. al contribuente"
                        };
                    }
                    else if (movPag.id_contribuente != current_contribuente_or_null && id_terzo == null)
                    {
                        return new MessaggioStatoMovPag
                        {
                            Stato = "pagamento accoppiato con avviso di altro contribuente",
                        };
                    }
                    //else if (movPag.id_contribuente != current_contribuente && id_terzo != null)
                    //{
                    //    return new StatoTipoPulsante
                    //    {
                    //        Stato = "pagamento di terzo debitore e altro contribuente",
                    //        TipoPulsante = ""
                    //    };
                    //}
                    else if (current_contribuente_or_null != null && movPag.id_contribuente != current_contribuente_or_null.Value)
                    {
                        return new MessaggioStatoMovPag
                        {
                            //Stato = "Pagamento effettuato da terzo debitore o da altro contribuente"
                            Stato = "Pagamento effettuato da altro contribuente"
                        };
                    }
                    else if (movPag.id_contribuente == null && id_terzo != null)
                    {
                        return new MessaggioStatoMovPag
                        {
                            Stato = "pagamento di terzo debitore"
                        };
                    }
                }
                else if (movPag.id_contribuente == null && codStato.StartsWith(anagrafica_stato_mov_pag.CARICATO))
                {
                    return new MessaggioStatoMovPag
                    {
                        Stato = "Accoppiamento avvisi non effettuato"
                    };
                }
                else if (movPag.id_contribuente == null &&
                        (codStato.StartsWith(anagrafica_stato_mov_pag.VEP) || codStato.StartsWith(anagrafica_stato_mov_pag.ECL)))
                {
                    return new MessaggioStatoMovPag
                    {
                        Stato = "Contribuente non riconosciuto"
                    };
                }

                return MessaggioStatoMovPag.Empty;
            }
        }

        public int id_tab_avv_pag_PagoPA { get; set; }
        public int id_rata_PagoPA { get; set; }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            public int id_tab_mov_pag { get; set; }

            public string codice_fiscale { get; set; }

            public DateTime data_operazione { get; set; }

            public DateTime data_accredito { get; set; }

            public DateTime data_valuta { get; set; }

            [RegularExpression("^[0-9]{1,}([,][0-9]{1,4})?$", ErrorMessage = "Formato non valido")]
            public decimal importo_mov_pagato { get; set; }

            public string uff_postale { get; set; }

            public string code_line { get; set; }

            public string causale_pagamento { get; set; }

            public int anno_riferimento { get; set; }

            public string comune_ubicazione_immobili { get; set; }

            public string cognome_rag_soc { get; set; }

            public string nome { get; set; }

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (id_tab_mov_pag < 0)
            {
                yield return new ValidationResult
                 ("Errore", new[] { "id_tab_mov_pag" });
            }
        }
    }
}
