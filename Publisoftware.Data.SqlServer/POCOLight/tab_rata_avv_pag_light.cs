using Publisoftware.Utility.SecureString;
using Publisoftware.Utility.Stringa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_rata_avv_pag_light : BaseEntity<tab_rata_avv_pag_light>
    {
        public int id_rata_avv_pag { get; set; }

                string sec = string.Empty;

        public string id_rata_avv_pag_sec
        {
            get
            {
                if (id_rata_avv_pag > 0)
                {
                    return EncryptionHelper.Encrypt(id_rata_avv_pag.ToString());
                }
                else
                {
                    return "";
                }
            }
        }
        public string id_rata_avv_pag_sec_simple
        {
            
            get
            {
                if (!string.IsNullOrEmpty(id_rata_avv_pag_sec))
                {
                    sec = string.Empty;
                    sec = StringHelper.RemoveSymbolFromString(id_rata_avv_pag_sec);
                    return sec;
                }
                else
                {
                    return "";
                }
            }
        }
        public int num_rata { get; set; }
        public bool IsRataPagata { get; set; }
        
        public string identificativo_rt { get; set; }
        public string dt_scadenza_rata_String { get; set; }
        public string imp_tot_rata_Euro { get; set; }
        public string imp_pagato_Euro { get; set; }
        public string imp_da_pagare_Euro { get; set; }
        public string bar_code { get; set; }
        public bool scaduto { get; set; }
        public string intestazione { get; set; }
        public string numerocc { get; set; }
        public string ABI { get; set; }
        public string CAB { get; set; }
        public string IBAN { get; set; }
        public tab_contribuente contribuente { get; set; }
        public bool IsAvvisoPagabile { get; set; }
        public string dataMassimaPagamentoAvviso { get; set; }
        public int IdTipoAvvPag { get; set; }

        //CARRELLO PAGOPA
        public bool pagabile { get; set; }
        public bool inPagamento { get; set; }
        public string IUV { get; set; }
        public int IdTabAvvPag { get; set; }
        public bool Selected { get; set; }

        public string Cod_Stato { get; set; }
        public string Descrizione_Stato { get; set; }
        public int Id_Stato { get; set; }
        public decimal Id_Contribuente { get; set; }

        public string Flag_Validita_Riga { get; set; }

        public decimal Importo_Pagato { get; set; }

        public string Importo_Pagato_Euro
        {
            get
            {
                return Importo_Pagato.ToString("C");
            }
        }
        public decimal Importo_Residuo { get; set; }
        public string Importo_Residuo_Euro
        {
            get
            {
                return Importo_Residuo.ToString("C");
            }
        }
        public int Id_CC_Riscossione { get; set; }
        public string Bic_Accredito { get; set; }
        public string Iban_Accredito { get; set; }

        public decimal? Imp_Spese_Coattive { get; set; }
        public decimal? Imp_Spese_Notifica { get; set; }
        public string Descr_Tipo_AvvPag { get; set; }
        public string Identificativo_Avviso { get; set; }

        public DateTime? Data_Ricezione_AvvPag { get; set; }

        public string codice_cbill { get; set; }
        public string codice_tassonomia_pagopa { get; set; }
        public string codice_pagamento_pagopa { get; set; }

        public int id_carrello { get; set; }
        public string nazione_debitore { get; set; }

    }
}
