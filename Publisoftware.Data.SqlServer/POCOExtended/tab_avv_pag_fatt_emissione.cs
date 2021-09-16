using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_avv_pag_fatt_emissione.Metadata))]
    public partial class tab_avv_pag_fatt_emissione : PSBaseEntity<tab_avv_pag_fatt_emissione, tab_avv_pag_fatt_emissione.Metadata>, IValidator, Itab_avv_pag, ISoftDeleted, IGestioneStato
    {
        public const string VAL_EME = "VAL-EME";
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            dt_stato_avvpag = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
            id_stato_avv_pag = id_stato;
            cod_stato_avv_pag = cod_stato;
        }

        public tab_avv_pag_fatt_emissione(int p_idEnte, int p_idEnteGestito, int p_idEntrata, decimal p_idContribuente, int p_idTipoAvvpag,
                                          DateTime? p_dataEmissione, string p_annoRiferimento, int? p_idListaEmissione) : this()
        {
            id_ente = p_idEnte;
            id_ente_gestito = p_idEnteGestito;
            id_entrata = p_idEntrata;
            id_anag_contribuente = p_idContribuente;
            id_tipo_avvpag = p_idTipoAvvpag;
            dt_stato_avvpag = DateTime.Now;
            dt_emissione = p_dataEmissione;
            anno_riferimento = p_annoRiferimento;
            id_lista_emissione = p_idListaEmissione;
            id_stato = anagrafica_stato_avv_pag.VAL_PRE_ID;
            cod_stato = anagrafica_stato_avv_pag.VAL_PRE;
            dt_avv_pag = DateTime.Now;
        }

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

        [DisplayName("Data Emissione")]
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

        public string DescrizioneTipoAvviso
        {
            get
            {
                return TipoAvviso + " " + NumeroAvviso;
            }
        }

        public string TipoAvviso
        {
            get
            {
                string v_result = anagrafica_tipo_avv_pag.cod_tipo_avv_pag + " / " + anagrafica_tipo_avv_pag.descr_tipo_avv_pag;

                return v_result;
            }
        }

        [DisplayName("Numero Avviso")]
        public string NumeroAvviso
        {
            get
            {
                //if (numero_avv_pag != null)
                //{
                //    return numero_avv_pag + "/" + anno_riferimento;
                //}
                //else
                //{
                //    return string.Empty;
                //}
                if (identificativo_avv_pag != null && identificativo_avv_pag.Length > 0)
                {
                    return identificativo_avv_pag;
                }
                else
                {
                    if (anagrafica_tipo_avv_pag != null)
                    {
                        return anagrafica_tipo_avv_pag.cod_tipo_avv_pag + "/" + anno_riferimento + "/" + numero_avv_pag;
                    }
                    else
                    {
                        return anno_riferimento + "/" + numero_avv_pag;
                    }
                }
            }
        }

        [DisplayName("Numero Rate")]
        public string Rate
        {
            get
            {
                //if (flag_rateizzazione_bis == "1")
                //{
                //    return num_rate_bis + " su istanza";
                //}
                //else
                //{
                //    if (num_rate <= 1)
                //    {
                //        return "1 ordinaria";
                //    }
                //    else
                //    {
                //        return num_rate + " ordinarie";
                //    }
                //}
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

        public string TipoBene
        {
            get
            {
                //Leggere commento in tab_avv_pag.TipoBene

                return string.Empty;
            }
        }

        public string IntimazioneCorrelata
        {
            get
            {
                //Leggere commento in tab_avv_pag.IntimazioneCorrelata

                return string.Empty;
            }
        }

        public string ImportoSpeseNotifica
        {
            get
            {
                return importoSpeseNotificaDecimal.ToString("C");
            }
        }

        public string ImportoSpeseCoattive
        {
            get
            {
                return importoSpeseCoattiveDecimal.ToString("C");
            }
        }

        public decimal importoSpeseNotificaDecimal
        {
            get
            {
                return 0;
            }
        }

        public decimal importoSpeseCoattiveDecimal
        {
            get
            {
                return 0;
            }
        }

        //Il dottore ha voluto sostituire il campo SpeditoNotificato con il nuovo campo Modalità di recapito fatta da Francesco
        [DisplayName("Spedito/Notificato")]
        public string SpeditoNotificato
        {
            get
            {
                //if (flag_spedizione_notifica == "0" || flag_spedizione_notifica == null)
                //{
                //    return "Spedito";
                //}
                //else
                //{
                //    if (flag_esito_sped_notifica == "0" || flag_esito_sped_notifica == null)
                //    {
                //        return "Non notificato";
                //    }
                //    else
                //    {
                //        //Il dottore ha voluto mostrare la data di ricezione al posto di quella di avvenuta notifica
                //        //Poi ha cambiato idea ed ha voluto far vedere entrambi o l'una o l'altra o nessuna a seconda se sono null o no
                //        //if (data_avvenuta_notifica.HasValue)
                //        if (data_ricezione.HasValue)
                //        {
                //            //return "Notificato il " + data_avvenuta_notifica.Value.ToShortDateString();
                //            if (data_avvenuta_notifica.HasValue)
                //            {
                //                return "Ricezione:" + data_ricezione.Value.ToShortDateString() + "; Notifica: " + data_avvenuta_notifica.Value.ToShortDateString();
                //            }
                //            else
                //            {
                //                return "Ricezione:" + data_ricezione.Value.ToShortDateString();
                //            }
                //        }
                //        else if (data_avvenuta_notifica.HasValue)
                //        {
                //            if (data_ricezione.HasValue)
                //            {
                //                return "Ricezione:" + data_ricezione.Value.ToShortDateString() + "; Notifica: " + data_avvenuta_notifica.Value.ToShortDateString();
                //            }
                //            else
                //            {
                //                return "Notifica: " + data_avvenuta_notifica.Value.ToShortDateString();
                //            }
                //        }
                //        else
                //        {
                //            return "Notificato";
                //        }
                //    }
                //}
                return SpeditoRicezione;
            }
        }

        [DisplayName("Modalità di recapito")]
        public string SpeditoRicezione
        {
            get
            {
                if (flag_spedizione_notifica == "1" &&
                    flag_esito_sped_notifica == "1")
                {
                    if (data_ricezione.HasValue)
                    {
                        return "Notificato il " + data_ricezione.Value.ToShortDateString();
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
                else if (string.IsNullOrEmpty(flag_spedizione_notifica))
                {
                    return string.Empty;
                }
                else if (flag_spedizione_notifica == "0")
                {
                    return "Spedito con Posta Ordinaria";
                }
                else
                {
                    if (flag_spedizione_notifica == "1" &&
                       (flag_esito_sped_notifica == "0" ||
                        flag_esito_sped_notifica == null))
                    {
                        return "Notificato con Esito Negativo";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public string Importo
        {
            get
            {
                return imp_tot_avvpag_rid_Euro;
            }
        }

        public string ImportoDaPagare
        {
            get
            {
                return importo_tot_da_pagare_Euro;

                //if (importo_tot_da_pagare.HasValue)
                //{
                //    return importo_tot_da_pagare_Euro;
                //}
                //else
                //{
                //    return 0.ToString("C");
                //}
            }
        }

        public string importo_tot_da_pagare_Euro
        {
            get
            {
                return importo_tot_da_pagare_decimal.ToString("C");
                //if (importo_tot_da_pagare.HasValue)
                //{
                //    return importo_tot_da_pagare.Value.ToString("C");
                //}
                //else
                //{
                //    return 0.ToString("C");
                //}
            }
        }

        public decimal importo_tot_da_pagare_decimal
        {
            get
            {
                //if (imp_tot_avvpag_rid.HasValue)
                //{
                //    return imp_tot_avvpag_rid.Value - imp_tot_pagato_decimal - 
                //                                      importo_sanzioni_eliminate_eredi_decimal -
                //                                      importo_sgravio_decimal -
                //                                      sanzioni_eliminate_definizione_agevolata_decimal -
                //                                      interessi_eliminati_definizione_agevolata_decimal;

                //    decimal v_importo = imp_tot_avvpag_rid.Value - imp_tot_pagato_decimal;
                //    if (v_importo < 1)
                //    {
                //        return 0;
                //    }
                //    else
                //    {
                //        return v_importo;
                //    }
                //}
                //else
                //{
                //    if (importo_tot_da_pagare.HasValue)
                //    {
                //        return importo_tot_da_pagare.Value;
                //    }
                //    else
                //    {
                //        return 0;
                //    }
                //}

                if (importo_tot_da_pagare.HasValue/*&& anagrafica_stato_avv_pag1.cod_stato_riferimento.StartsWith(anagrafica_stato_avv_pag.VALIDO)*/)
                {
                    return importo_tot_da_pagare.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_sanzioni_eliminate_eredi_Euro
        {
            get
            {
                return importo_sanzioni_eliminate_eredi_decimal.ToString("C");
            }
        }

        public decimal importo_sanzioni_eliminate_eredi_decimal
        {
            get
            {
                if (importo_sanzioni_eliminate_eredi.HasValue)
                {
                    return importo_sanzioni_eliminate_eredi.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string sanzioni_eliminate_definizione_agevolata_Euro
        {
            get
            {
                return sanzioni_eliminate_definizione_agevolata_decimal.ToString("C");
            }
        }

        public decimal sanzioni_eliminate_definizione_agevolata_decimal
        {
            get
            {
                if (sanzioni_eliminate_definizione_agevolata.HasValue)
                {
                    return sanzioni_eliminate_definizione_agevolata.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string interessi_eliminati_definizione_agevolata_Euro
        {
            get
            {
                return interessi_eliminati_definizione_agevolata_decimal.ToString("C");
            }
        }

        public decimal interessi_eliminati_definizione_agevolata_decimal
        {
            get
            {
                if (interessi_eliminati_definizione_agevolata.HasValue)
                {
                    return interessi_eliminati_definizione_agevolata.Value;
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

        public string imp_tot_avvpag_rid_Euro
        {
            get
            {
                //if (imp_tot_avvpag_rid.HasValue)
                //{
                //    return imp_tot_avvpag_rid.Value.ToString("C");
                //}
                //else
                //{
                //    return 0.ToString("C");
                //}
                return imp_tot_avvpag_rid_arrotondato_Euro;
            }
        }

        public decimal imp_tot_avvpag_rid_decimal
        {
            get
            {
                //if (imp_tot_avvpag_rid.HasValue)
                //{
                //    return imp_tot_avvpag_rid.Value;
                //}
                //else
                //{
                //    return 0;
                //}
                return imp_tot_avvpag_rid_arrotondato_decimal;
            }
        }

        public string imp_tot_avvpag_rid_arrotondato_Euro
        {
            get
            {
                return imp_tot_avvpag_rid_arrotondato_decimal.ToString("C");
            }
        }

        public decimal imp_tot_avvpag_rid_arrotondato_decimal
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

                //Decimal? retImporto = null;
                //if (imp_tot_avvpag_rid.HasValue)
                //{
                //    Decimal tot = Math.Round(imp_tot_avvpag_rid.Value, 0);
                //    Decimal tot05 = Decimal.Add(tot, Convert.ToDecimal(0.5));

                //    retImporto = tot;
                //    if (tot05 == imp_tot_avvpag_rid)
                //    {
                //        retImporto = tot + 1;
                //    }
                //    return retImporto.Value;
                //}
                //return 0;
            }
        }

        public string importo_sgravio_Euro
        {
            get
            {
                return importo_sgravio_decimal.ToString("C");
            }
        }

        public decimal importo_sgravio_decimal
        {
            get
            {
                if (importo_sgravio.HasValue)
                {
                    return importo_sgravio.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string imp_tot_pagato_Euro
        {
            get
            {
                return imp_tot_pagato_decimal.ToString("C");
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

        public string imponibile
        {
            get
            {
                return ((imp_imp_entr_avvpag.HasValue ? imp_imp_entr_avvpag.Value : 0) +
                        (imp_esente_iva_avvpag.HasValue ? imp_esente_iva_avvpag.Value : 0)).ToString("C4");

            }
        }

        public string iva
        {
            get
            {
                if (imp_iva_avvpag.HasValue)
                {
                    return imp_iva_avvpag.Value.ToString("C4");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public string impRidottoPerAdesione
        {
            get
            {
                //Il dottore ha voluto inserire questo campo per gli avvisi di accertamento (id_servizio=1) che mostrano l'eventuale importo ridotto se i termini non sono scaduti ancora
                double v_giorni = (DateTime.Now - (data_avvenuta_notifica.HasValue ? data_avvenuta_notifica.Value : DateTime.Now)).TotalDays;

                if (v_giorni > 0 && v_giorni < 30 && importo_ridotto_Euro != string.Empty)
                {
                    return importo_ridotto_Euro + " (entro il " + data_avvenuta_notifica.Value.AddMonths(1).ToShortDateString() + ")";
                }
                else
                {
                    //return "Nessun imp. ridotto o termini scaduti";
                    return string.Empty;
                }
            }
        }
        //public string imp_tot_avvpag_rid_arrotondato_Euro
        //{
        //    get
        //    {
        //        Decimal? retImporto = null;
        //        if (imp_tot_avvpag.HasValue)
        //        {
        //            Decimal tot = Math.Round(imp_tot_avvpag_rid.Value, 0);
        //            Decimal tot05 = Decimal.Add(tot, Convert.ToDecimal(0.5));

        //            retImporto = tot;
        //            if (tot05 == imp_tot_avvpag)
        //            {
        //                retImporto = tot + 1;
        //            }
        //            return retImporto.Value.ToString("C");
        //        }
        //        return 0.ToString("C");
        //    }
        //}

        public string stato
        {
            get
            {
                if (anagrafica_stato_avv_pag1 != null)
                {
                    return anagrafica_stato_avv_pag1.desc_stato_riferimento;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string DescrizioneAvvisoCollegato
        {
            get
            {
                return this.anagrafica_tipo_avv_pag.descr_tipo_avv_pag.Trim() + " n. " + this.identificativo_avv_pag.Trim() + " emessa il " + this.dt_emissione_String + " notificata il " + this.data_ricezione.ToString();
            }
        }

        public bool ExistsOrdineOrigine
        {
            get
            {
                return TAB_SUPERVISIONE_FINALE_V2 != null && TAB_SUPERVISIONE_FINALE_V2.id_avvpag_preavviso_collegato.HasValue;
            }
        }

        public bool IsFatturazioneAcqua
        {
            get
            {
                if (id_tipo_avvpag == anagrafica_tipo_avv_pag.PREAVVISO_FATTURAZIONE)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string contribuenteAvvPag
        {
            get
            {
                string getContribuente = string.Empty;
                TextInfo txSogDeb = CultureInfo.CurrentCulture.TextInfo;
                string sSogg = string.Empty;
                string sRefDis = string.Empty;
                string sRefCF = string.Empty;
                //int pos = 0;
                // string vcrlf = @"\r\n"; // System.Environment.NewLine;
                tab_contribuente tbContrAvvPag = this.tab_contribuente;

                if (tbContrAvvPag.cognome != null && tbContrAvvPag.nome != null)
                {
                    getContribuente = txSogDeb.ToTitleCase(tbContrAvvPag.cognome.ToLower().Trim() + " " + tbContrAvvPag.nome.ToLower().Trim() + " - " + tbContrAvvPag.codFiscalePivaDisplay.Trim());
                }
                else if (tbContrAvvPag.rag_sociale != null)
                {
                    getContribuente = tbContrAvvPag.rag_sociale.Trim() + " - " + tbContrAvvPag.codFiscalePivaDisplay.Trim();
                }
                else
                {
                    getContribuente = "";
                }

                //if (tab_avv_pag.join_avv_pag_soggetto_debitore .Any(d => d.id_referente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(CodStato.ATT_ATT)))
                //{
                //    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = tab_avv_pag.join_avv_pag_soggetto_debitore.Where(d => d.id_referente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(CodStato.ATT_ATT)).FirstOrDefault();
                //    sSogg = v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay.Trim();
                //    sRefDis = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Trim();
                //    sRefDis = txSogDeb.ToTitleCase(sRefDis);
                //    return sSogg + " terzo debitore di " + sRefDis + vcrlf +
                //        ", coobbligato del contribuente: " + getContribuente + " in qualità di " + v_joinAvvPaggSoggDeb.tab_referente.join_referente_contribuente.Where(d => d.id_anag_contribuente == id_anag_contribuente && !d.cod_stato.StartsWith(CodStato.ANN)).SingleOrDefault().anagrafica_tipo_relazione.desc_tipo_relazione;
                //}
                //else if (tab_avv_pag.join_avv_pag_soggetto_debitore.Any(d => d.id_referente != null && d.cod_stato.Equals(CodStato.ATT_ATT)))
                //{
                //    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = tab_avv_pag.join_avv_pag_soggetto_debitore.Where(d => d.id_referente != null && d.cod_stato.Equals(CodStato.ATT_ATT)).FirstOrDefault();
                //    sSogg = txSogDeb.ToTitleCase(v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Trim());
                //    sRefDis = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Trim();
                //    sRefDis = txSogDeb.ToTitleCase(sRefDis);
                //    //sRefDis = sRefDis + " - " + sRefCF;

                //    return sRefDis + vcrlf +
                //        v_joinAvvPaggSoggDeb.tab_referente.join_referente_contribuente.Where(d => d.id_anag_contribuente == id_anag_contribuente && !d.cod_stato.StartsWith(CodStato.ANN)).Select(s => s.anagrafica_tipo_relazione.desc_tipo_relazione).FirstOrDefault() +
                //        " del contribuente: " + getContribuente;
                //}
                //else if (tab_avv_pag.join_avv_pag_soggetto_debitore.Any(d => d.id_terzo_debitore != null && d.cod_stato.Equals(CodStato.ATT_ATT)))
                //{
                //    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = tab_avv_pag.join_avv_pag_soggetto_debitore.Where(d => d.id_terzo_debitore != null && d.cod_stato.Equals(CodStato.ATT_ATT)).FirstOrDefault();

                //    return v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay.Trim() + vcrlf +
                //        " terzo debitore del contribuente" + getContribuente.Trim();
                //}
                //else
                //{

                return getContribuente.Trim();
                //}
            }
        }

        public sealed class Metadata
        {
            private Metadata()
            {
            }

            [Required(ErrorMessage = "Inserire l'entrata")]
            [DisplayName("Entrata")]
            public int id_entrata { get; set; }

            [Required(ErrorMessage = "Inserire il tipo avviso")]
            [DisplayName("Tipo Avviso")]
            public int id_tipo_avvpag { get; set; }
        }
    }
}
