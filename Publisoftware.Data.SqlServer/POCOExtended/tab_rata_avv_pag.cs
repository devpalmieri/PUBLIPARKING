using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    public partial class tab_rata_avv_pag : ISoftDeleted, IGestioneStato
    {
        public const string ANN = "ANN-";
        public const string ANN_ANN = "ANN-ANN";

        public const string ATT_ATT = "ATT-ATT";
        public const string ATT = "ATT-";
        public const int ATT_ATT_ID = 1;
        public const string DESCR_DAPAGARE = "DA PAGARE";
        public const string DESCR_PAGATA = "PAGATA";

        public const string ATT_INP = "ATT-INP";
        public const int ATT_INP_ID = 2;
        public const string ATT_INP_DESCR = "IN PAGAMENTO";

        public const string ATT_RPT = "ATT-RPT";

        public const string VAL = "VAL-";
        public const string VAL_PRE = "VAL-PRE";
        public const string VAL_VAL = "VAL-VAL";

        public const string PAG_TOT = "PAG-TOT";
        public const string ATT_PAG = "ATT-PAG";
        public const string ATT_REN = "ATT-REN";
        public const string SSP_REN = "SSP-REN";

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

        public string dt_scadenza_rata_String
        {
            get
            {
                if (dt_scadenza_rata.HasValue)
                {
                    return dt_scadenza_rata.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Descrizione_Stato
        {
            get
            {
                if (pagabile)
                {
                    return DESCR_DAPAGARE;
                }
                else if (IsRataPagata)
                {
                    return DESCR_PAGATA;
                }
                else if (inPagamento)
                {
                    return ATT_INP_DESCR;
                }
                else
                {
                    return string.Empty;
                };
            }
        }

        public string imp_tot_rata_Euro
        {
            get
            {
                return imp_tot_rata.ToString("C");
            }
        }

        public string imp_pagato_Euro
        {
            get
            {
                return imp_pagato.ToString("C");
            }
        }

        public decimal imp_da_pagare
        {
            get
            {
                return decimal.Subtract(imp_tot_rata, imp_pagato);
            }
        }

        public bool pagabile
        {
            get
            {
                if (imp_da_pagare > 0 && (cod_stato == ATT_ATT))
                    return true;
                else
                    return false;
            }
        }

        public bool inPagamento
        {
            get
            {
                if (imp_da_pagare > 0 && (cod_stato == ATT_INP))
                    return true;
                else
                    return false;
            }
        }

        public bool IsRataPagata
        {
            get
            {
                return decimal.Subtract(imp_tot_rata, imp_pagato) <= 0;
            }
        }

        public string imp_da_pagare_Euro
        {
            get
            {
                decimal imp_da_pagare = this.imp_da_pagare;

                if (imp_da_pagare >= 0)
                {
                    return imp_da_pagare.ToString("C");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool scaduto
        {
            get
            {
                if (dt_scadenza_rata.HasValue)
                {
                    if (dt_scadenza_rata.Value.Date < DateTime.Now.Date)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public bool isRataUnica
        {
            get
            {
                return this.num_rata == 0;
            }
        }

        public string barCodeBollettino128
        {
            get
            {
                string iuv_Temp = string.Empty;
                string sCodImp = string.Empty;
                //TODO Sandro: per l'avviso di rateizzazione di Giulivo
                sCodImp = this.imp_tot_rata.ToString("N2");
                //sCodImp =sCodImp.Substring(0, sCodImp.Length - 5) + this.imp_tot_rata.ToString("N2").Substring(imp_tot_rata.ToString("N2").Length - 5, 2);
                string sIdentificativo = string.Empty;
                //sIdentificativo = sIdentificativo.Substring(sIdentificativo.Length - 2,2);
                if (this.tab_avv_pag.anagrafica_ente.flag_tipo_gestione_pagopa != null)
                {
                    sIdentificativo = this.codice_pagamento_pagopa;
                }
                else
                {
                    sIdentificativo = this.Iuv_identificativo_pagamento;
                }

                sCodImp = sCodImp.Replace(",", "");
                sCodImp = sCodImp.Replace(".", "");
                //sCodImp = sCodImp.Substring(0, (sCodImp.Length - 6));
                while (sCodImp.Length < 10)
                {
                    sCodImp = "0" + sCodImp;
                }
                sCodImp = sCodImp + "3896";
                string sCodCC = this.tab_cc_riscossione.num_cc_new;
                while (sCodCC.Length < 12)
                {
                    sCodCC = "0" + sCodCC;
                }

                string sBCode = "18" + sIdentificativo + "12" + sCodCC + "10" + sCodImp;

                return sBCode;
            }
        }

        public static string GetRataPagabile(DateTime? v_data, tab_avv_pag v_avviso)
        {
            if (!v_data.HasValue)
            {
                v_data = DateTime.Now;
            }

            string v_return = "0";

            if (v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO ||
                v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO)
            {
                if (v_avviso.data_ricezione.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_ricezione.Value).TotalDays;

                    if (v_giorni > 60 &&
                        v_giorni <= 120)
                    {
                        v_return = "1";
                    }
                    else if (v_giorni > 120)
                    {
                        v_return = "2";
                    }
                    else
                    {
                        v_return = "0";
                    }
                }
                else if (v_avviso.data_avvenuta_notifica.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_avvenuta_notifica.Value).TotalDays;

                    if (v_giorni > 90 &&
                        v_giorni <= 150)
                    {
                        v_return = "1";
                    }
                    else if (v_giorni > 150)
                    {
                        v_return = "2";
                    }
                    else
                    {
                        v_return = "0";
                    }
                }
                else
                {
                    v_return = "0";
                }
            }
            else if (v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO &&
                     v_avviso.id_entrata == anagrafica_entrate.CDS)
            {
                if (v_avviso.data_ricezione.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_ricezione.Value).TotalDays;

                    if (v_giorni > 5 &&
                        v_giorni <= 60)
                    {
                        v_return = "1";
                    }
                    else if (v_giorni > 60)
                    {
                        v_return = "2";
                    }
                    else
                    {
                        v_return = "0";
                    }
                }
                else if (v_avviso.data_avvenuta_notifica.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_avvenuta_notifica.Value).TotalDays;

                    if (v_giorni > 35 &&
                        v_giorni <= 120)
                    {
                        v_return = "1";
                    }
                    else if (v_giorni > 120)
                    {
                        v_return = "2";
                    }
                    else
                    {
                        v_return = "0";
                    }
                }
                else
                {
                    v_return = "0";
                }
            }

            return v_return;
        }
    }
}
