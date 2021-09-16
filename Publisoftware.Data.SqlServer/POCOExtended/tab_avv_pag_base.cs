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
    [MetadataTypeAttribute(typeof(tab_avv_pag_base.Metadata))]
    public class tab_avv_pag_base
    {
        public static decimal MIN_IMPORTO_DA_PAGARE = 1;
        public static string FONTE_IMPORTATA = "IMP";

        public int id_tab_avv_pag { get; set; }
        public int id_ente { get; set; }
        public int id_ente_gestito { get; set; }
        public Nullable<int> id_contratto { get; set; }
        public int id_entrata { get; set; }
        public Nullable<decimal> id_contribuente_old { get; set; }
        public decimal id_anag_contribuente { get; set; }
        public int id_tipo_avvpag { get; set; }
        public int id_stato_avv_pag { get; set; }
        public string cod_stato_avv_pag { get; set; }
        public System.DateTime dt_stato_avvpag { get; set; }
        public Nullable<System.DateTime> dt_emissione { get; set; }
        public string anno_riferimento { get; set; }
        public Nullable<System.DateTime> periodo_riferimento_da { get; set; }
        public Nullable<System.DateTime> periodo_riferimento_a { get; set; }
        public Nullable<int> id_tab_contr_doc { get; set; }
        public string numero_avv_pag { get; set; }
        
        [DisplayName("Numero Avviso")]
        public string NumeroAvviso { get; set; }
        public string barcode { get; set; }
        public string flag_spedizione_notifica { get; set; }
        public Nullable<int> id_tipo_spedizione_notifica { get; set; }
        public string tipo_spedizione_notifica { get; set; }
        public string numero_raccomandata { get; set; }
        public string flag_iter_recapito_notifica { get; set; }
        public string flag_esito_sped_notifica { get; set; }
        public Nullable<int> id_tipo_esito_notifica { get; set; }
        public string tipo_esito_notifica { get; set; }
        public Nullable<System.DateTime> data_avvenuta_notifica { get; set; }
        public Nullable<int> id_notificatore { get; set; }
        public Nullable<System.DateTime> dt_scadenza_not { get; set; }
        public Nullable<System.DateTime> data_ricezione { get; set; }
        public Nullable<System.DateTime> data_affissione_ap { get; set; }
        public Nullable<System.DateTime> data_notifica_avvdep { get; set; }
        public string esito_notifica_avvdep { get; set; }
        public Nullable<decimal> imp_tot_avvpag { get; set; }
        public Nullable<decimal> imp_imp_entr_avvpag { get; set; }
        public Nullable<decimal> imp_esente_iva_avvpag { get; set; }
        public Nullable<decimal> imp_iva_avvpag { get; set; }
        public Nullable<decimal> imp_tot_spese_avvpag { get; set; }
        public Nullable<decimal> imp_spese_notifica { get; set; }
        public Nullable<decimal> imp_tot_pagato { get; set; }
        public Nullable<decimal> importo_tot_da_pagare { get; set; }
        public Nullable<decimal> imp_tot_avvpag_rid { get; set; }
        public string flag_rateizzazione { get; set; }
        public Nullable<System.DateTime> data_rateizzazione { get; set; }
        public Nullable<decimal> imp_rateizzato { get; set; }
        public Nullable<int> periodicita_rate { get; set; }
        public Nullable<int> num_rate { get; set; }
        public Nullable<System.DateTime> data_scadenza_1_rata { get; set; }
        public string flag_rateizzazione_bis { get; set; }
        public Nullable<System.DateTime> data_rateizzazione_bis { get; set; }
        public Nullable<decimal> imp_rateizzato_bis { get; set; }
        public Nullable<int> periodicita_rate_bis { get; set; }
        public Nullable<int> num_rate_bis { get; set; }
        public Nullable<System.DateTime> data_scadenza_1_rata_bis { get; set; }
        public string flag_adesione { get; set; }
        public Nullable<System.DateTime> data_adesione { get; set; }
        public string flag_riemissione { get; set; }
        public Nullable<int> num_avv_riemesso { get; set; }
        public Nullable<int> id_risorsa { get; set; }
        public Nullable<System.DateTime> dt_avv_pag { get; set; }
        public string fonte_emissione { get; set; }
        public Nullable<int> id_lista_emissione { get; set; }
        public Nullable<int> id_lista_carico { get; set; }
        public Nullable<int> id_lista_scarico { get; set; }
        public string flag_carico { get; set; }
        public string flag_scarico { get; set; }
        public int id_stato { get; set; }
        public string cod_stato { get; set; }
        public System.DateTime data_stato { get; set; }
        public int id_struttura_stato { get; set; }
        public int id_risorsa_stato { get; set; }

        public string ImportoSpeseNotifica { get; set; }
        public string ImportoSpeseCoattive { get; set; }
        public string ImportoDaPagare { get; set; }
        public string stato { get; set; }
        [DisplayName("Tipo Bene")]
        public string TipoBene { get; set; }
        public string Atti { get; set; }
        public bool ExistsAtti { get; set; }
        public string IntimazioneCorrelata { get; set; }
        public string impRidottoPerAdesione { get; set; }
        public string SoggettoDebitore { get; set; }

        public string nome { get; set; }
        public string cognome { get; set; }
        public string codice_fiscale { get; set; }
        public string p_iva { get; set; }
        public string ragione_sociale { get; set; }

        public string data_adesione_String
        {
            get
            {
                if (data_adesione.HasValue)
                {
                    return data_adesione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string dt_emissione_String
        {
            get
            {
                if (dt_emissione.HasValue)
                {
                    return dt_emissione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_avvenuta_notifica_String
        {
            get
            {
                if (data_avvenuta_notifica.HasValue)
                {
                    return data_avvenuta_notifica.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_ricezione_String
        {
            get
            {
                if (data_ricezione.HasValue)
                {
                    return data_ricezione.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string dt_scadenza_not_String
        {
            get
            {
                if (dt_scadenza_not.HasValue)
                {
                    return dt_scadenza_not.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        [DisplayName("Numero Rate")]
        public string Rate
        {
            get
            {
                if (flag_rateizzazione_bis == "2")
                {
                    return "Rateizzato";
                }
                else if (flag_rateizzazione_bis == "1" && (flag_adesione == "0" || flag_adesione == null))
                {
                    if (num_rate_bis.HasValue)
                    {
                        return num_rate_bis.Value + " su istanza";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else if (flag_rateizzazione_bis == "1" && flag_adesione == "1")
                {
                    if (num_rate_bis.HasValue)
                    {
                        return num_rate_bis.Value + " su adesione";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    if (num_rate.HasValue && num_rate <= 1)
                    {
                        return "1";
                    }
                    else if (num_rate.HasValue)
                    {
                        return num_rate.Value.ToString();
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        [DisplayName("Adesione")]
        public string Adesione
        {
            get
            {
                if (flag_adesione == "1")
                {
                    if (data_adesione.HasValue)
                    {
                        return data_adesione_String;
                    }
                    else
                    {
                        return "Si";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string Tipo
        {
            get
            {
                if (flag_spedizione_notifica == "0" || flag_spedizione_notifica == null)
                {
                    return "Spedizione";
                }
                else
                {
                    return "Notifica";
                }
            }
        }

        public string EsitoNotifica
        {
            get
            {
                if (flag_esito_sped_notifica == "0" || flag_esito_sped_notifica == null)
                {
                    return "Non notificato";
                }
                else
                {
                    return "Notificato";
                }
            }
        }

        [DisplayName("Spedito/Notificato")]
        public string SpeditoNotificato
        {
            get
            {
                if (flag_spedizione_notifica == "0" || flag_spedizione_notifica == null)
                {
                    return "Spedito";
                }
                else
                {
                    if (flag_esito_sped_notifica == "0" || flag_esito_sped_notifica == null)
                    {
                        return "Non notificato";
                    }
                    else
                    {
                        if (data_avvenuta_notifica.HasValue)
                        {
                            return "Notificato il " + data_avvenuta_notifica.Value.ToShortDateString();
                        }
                        else
                        {
                            return "Notificato";
                        }
                    }
                }
            }
        }

        //Il dottore ha voluto sostituire l'imp_tot_avvpag/importo_ridotto con l'imp_tot_avvpag_rid
        public string Importo
        {
            get
            {
                //if (flag_adesione == "1")
                //{
                //    return importo_ridotto_Euro;
                //}
                //else
                //{
                //    return imp_tot_avvpag_Euro;
                //}
                return imp_tot_avvpag_rid_Euro;
            }
        }


        public decimal imp_tot_pagato_decimal
        {
            get
            {
                if (imp_tot_pagato.HasValue)
                {
                    return imp_tot_pagato.Value;
                }
                else
                {
                    return 0;
                }
            }
        }
/*
        public decimal importo_tot_da_pagare_decimal
        {
            get
            {
                if (importo_tot_da_pagare.HasValue && anagrafica_stato_avv_pag.cod_stato_riferimento.StartsWith(CodStato.VAL))
                {
                    return importo_tot_da_pagare.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_ridotto_Euro
        {
            get
            {
                if (importo_ridotto.HasValue)
                {
                    return importo_ridotto.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }
*/
        public string imp_tot_avvpag_Euro
        {
            get
            {
                if (imp_tot_avvpag.HasValue)
                {
                    return imp_tot_avvpag.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public Decimal? imp_tot_avvpag_arrotondato
        {
            get
            {
                Decimal? retImporto = null;
                if (imp_tot_avvpag.HasValue)
                {
                    Decimal tot = Math.Round(imp_tot_avvpag.Value, 0);
                    Decimal tot05 = Decimal.Add(tot, Convert.ToDecimal(0.5));

                    retImporto = tot;
                    if (tot05 == imp_tot_avvpag)
                    {
                        retImporto = tot + 1;
                    }
                }

                return retImporto;
            }
        }

        //TODO: Davide importo da pagare calcolato => tiene conto del pagato, perchè il da pagare nell'avviso trascura l'arrotondamento applicato (METODO CORRETTO COSI?)
        public Decimal? importo_tot_da_pagare_arrotondato
        {
            get
            {
                if (imp_tot_avvpag_arrotondato.HasValue)
                {
                    Decimal da_pagare = imp_tot_avvpag_arrotondato.Value;
                    if (imp_tot_pagato.HasValue)
                        da_pagare = Decimal.Subtract(da_pagare, imp_tot_pagato.Value);
                    return da_pagare;
                }
                return null;
            }
        }

        public decimal imp_tot_avvpag_rid_decimal
        {
            get
            {
                if (imp_tot_avvpag_rid.HasValue)
                {
                    return imp_tot_avvpag_rid.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string imp_tot_avvpag_rid_Euro
        {
            get
            {
                if (imp_tot_avvpag_rid.HasValue)
                {
                    return imp_tot_avvpag_rid.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string imp_tot_pagato_Euro
        {
            get
            {
                if (imp_tot_pagato.HasValue)
                {
                    return imp_tot_pagato.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }
/*
        public string impRidottoPerAdesione
        {
            get
            {
                //Il dottore ha voluto inserire questo campo per gli avvisi di accertamento (id_servizio=1) che mostrano l'eventuale importo ridotto se i termini non sono scaduti ancora
                double v_giorni = (System.DateTime.Now - (data_avvenuta_notifica.HasValue ? data_avvenuta_notifica.Value : System.DateTime.Now)).TotalDays;

                if (v_giorni > 0 && v_giorni < 30 && importo_ridotto_Euro != string.Empty)
                {
                    return importo_ridotto_Euro + " (entro il " + data_avvenuta_notifica.Value.AddMonths(1).ToShortDateString() + ")";
                }
                else
                {
                    return "Nessun imp. ridotto o termini scaduti";
                }
            }
        }
*/
        public string importo_tot_da_pagare_Euro
        {
            get
            {
                if (importo_tot_da_pagare.HasValue)
                {
                    return importo_tot_da_pagare.Value.ToString("C");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

/*
        public bool IsIstanzaVisibile
        {
            get
            {
                return join_tab_avv_pag_tab_doc_input.Count > 0;
            }
        }

        public List<tab_rata_avv_pag> rate_utilizzabili
        {
            get
            {
                List<tab_rata_avv_pag> rateLst = new List<tab_rata_avv_pag>();
                tab_rata_avv_pag rataUnica = this.tab_rata_avv_pag.Where(r => r.isRataUnica).FirstOrDefault();
                List<tab_rata_avv_pag> rateizzate = this.tab_rata_avv_pag.Where(r => !r.isRataUnica).ToList();
                if (rataUnica != null && rateizzate.Sum(r => r.imp_pagato) == 0)
                {
                    rateLst.Add(rataUnica);
                }
                if (rataUnica == null || rataUnica.imp_pagato == null || rataUnica.imp_pagato == 0)
                {
                    rateLst.AddRange(rateizzate);
                }

                return rateLst;
            }
        }
*/
        public bool pari
        {
            get
            {
                return (this.id_tab_avv_pag % 2) == 0;
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
