using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class join_doc_input_pag_avvpag : ISoftDeleted, IGestioneStato
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
                if (data_operazione_pagamento.HasValue)
                {
                    return data_operazione_pagamento.Value.ToShortDateString();
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
                    data_operazione_pagamento = DateTime.Parse(value);
                }
                else
                {
                    data_operazione_pagamento = null;
                }
            }
        }

        public string data_accredito_String
        {
            get
            {
                if (data_accredito_pagamento.HasValue)
                {
                    return data_accredito_pagamento.Value.ToShortDateString();
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
                    data_accredito_pagamento = DateTime.Parse(value);
                }
                else
                {
                    data_accredito_pagamento = null;
                }
            }
        }

        public string data_valuta_String
        {
            get
            {
                if (data_valuta_pagamento.HasValue)
                {
                    return data_valuta_pagamento.Value.ToShortDateString();
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
                    data_valuta_pagamento = DateTime.Parse(value);
                }
                else
                {
                    data_valuta_pagamento = null;
                }
            }
        }

        public string data_nascita_beneficiario_String
        {
            get
            {
                if (data_nascita_beneficiario.HasValue)
                {
                    return data_nascita_beneficiario.Value.ToShortDateString();
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
                    data_nascita_beneficiario = DateTime.Parse(value);
                }
                else
                {
                    data_nascita_beneficiario = null;
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

        public string imp_mov_da_imputare_avvpag_Euro
        {
            get
            {
                if (imp_mov_da_imputare_avvpag.HasValue)
                {
                    return imp_mov_da_imputare_avvpag.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string importo_rimborso_Euro
        {
            get
            {
                if (importo_rimborso.HasValue)
                {
                    return importo_rimborso.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }
        public string ComuneResidenzaBeneficiario
        {
            get
            {
                if (comune_residenza_benficiario == null)
                {
                    return string.Empty;
                }
                return comune_residenza_benficiario;
            }
        }
        public string SiglaProvResidenzaBeneficiario
        {
            get
            {
                if (sigla_provincia_residenza_beneficiario == null)
                {
                    return string.Empty;
                }
                return sigla_provincia_residenza_beneficiario;
            }
        }
        public string  IndirizzoresidenzaBeneficiario
        {
            get
            {
                if(indirizzo_residenza_beneficiario==null)
                {
                    return string.Empty;
                }
                return indirizzo_residenza_beneficiario;
            }
        }
        public string Pagante
        {
            get
            {
                if (codice_fiscale_pagante == null)
                {
                    return string.Empty;
                }
                else if (nome_pagante != null)
                {
                    return codice_fiscale_pagante + " - " + cognome_rag_soc_pagante + " " + nome_pagante;
                }
                else
                {
                    return codice_fiscale_pagante + " - " + cognome_rag_soc_pagante;
                }
            }
        }

        public string UbicazionePagante
        {
            get
            {
                if (indirizzo_pagante == null && nr_civico_pagante == null && cap_pagante == null && localita_pagante == null)
                {
                    return string.Empty;
                }
                else
                {
                    return indirizzo_pagante + " " + nr_civico_pagante + " " + cap_pagante + " " + localita_pagante;
                }
            }
        }
    }
}
