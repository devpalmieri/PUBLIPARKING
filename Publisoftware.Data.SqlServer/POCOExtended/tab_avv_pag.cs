using Publisoftware.Data.CustomValidationAttrs;
using Publisoftware.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Publisoftware.Utility.CAP;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_avv_pag.Metadata))]
    public partial class tab_avv_pag : Itab_avv_pag, ISoftDeleted, IGestioneStato
    {
        public static decimal MIN_IMPORTO_DA_PAGARE = 1;

        public const string FONTE_IMPORTATA = "IMP";
        public const string FONTE_EMESSA = "EME";

        public const string SOLLECITO_ID = "1";
        public const string SOLLECITO = "Sollecito di pagamento";

        public const string INTIMAZIONE_ID = "2";
        public const string INTIMAZIONE = "Intimazione di pagamento";

        public const string FLAG_TIPO_ATTO_SUCCESSIVO_SENTENZA = "S";

        public const string FLAG_ADESIONE_1 = "1";
        public const string FLAG_ADESIONE_0 = "0";

        public const string FLAG_ESITO_NOTIFICATO = "1";
        public const string FLAG_ESITO_NON_NOTIFICATO = "0";
        public const string FLAG_SENZA_ESITO_NOTIFICATA = "";

        public const string FLAG_RICALCOLATO_NO = "0";
        public const string FLAG_RICALCOLATO_60 = "1";
        public const string FLAG_RICALCOLATO_120 = "2";

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

        public IQueryable<tab_unita_contribuzione> tab_unita_contribuzione_valide()
        {
            return this.tab_unita_contribuzione.Where(u => u.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.VALIDO)).AsQueryable();
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

        public string data_avviso_bonario_String
        {
            get
            {
                if (data_avviso_bonario.HasValue)
                {
                    return data_avviso_bonario.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string data_notifica_avviso_bonario_String
        {
            get
            {
                if (data_notifica_avviso_bonario.HasValue)
                {
                    return data_notifica_avviso_bonario.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string data_intimazione
        {
            get
            {
                if (TAB_JOIN_AVVCOA_INGFIS_V21.Where(x => x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                                                          x.tab_avv_pag.flag_esito_sped_notifica == "1" &&
                                                          x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO)) != null &&
                    TAB_JOIN_AVVCOA_INGFIS_V21.Where(x => x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                                                          x.tab_avv_pag.flag_esito_sped_notifica == "1" &&
                                                          x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                                              .OrderByDescending(x => x.ID_JOIN_AVVCOA_INGFIS)
                                              .FirstOrDefault() != null)
                {
                    return TAB_JOIN_AVVCOA_INGFIS_V21.Where(x => x.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                                                                 x.tab_avv_pag.flag_esito_sped_notifica == "1" &&
                                                                 x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                                                     .OrderByDescending(x => x.ID_JOIN_AVVCOA_INGFIS)
                                                     .FirstOrDefault()
                                                     .tab_avv_pag
                                                     .data_ricezione_String;
                }
                else if (TAB_JOIN_AVVCOA_INGFIS_V21.Where(x => x.tab_avv_pag1.flag_tipo_atto_successivo == "2") != null &&
                         TAB_JOIN_AVVCOA_INGFIS_V21.Where(x => x.tab_avv_pag1.flag_tipo_atto_successivo == "2")
                                                   .OrderByDescending(x => x.ID_JOIN_AVVCOA_INGFIS)
                                                   .FirstOrDefault() != null)
                {
                    return TAB_JOIN_AVVCOA_INGFIS_V21.Where(x => x.tab_avv_pag1.flag_tipo_atto_successivo == "2")
                                                     .OrderByDescending(x => x.ID_JOIN_AVVCOA_INGFIS)
                                                     .FirstOrDefault()
                                                     .tab_avv_pag1
                                                     .data_avviso_bonario_String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string avvisoBonario
        {
            get
            {
                if (!string.IsNullOrEmpty(dati_avviso_bonario))
                {
                    return dati_avviso_bonario + " - " + data_avviso_bonario_String;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string dataMassimaPresentazioneIstanza
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault() != null &&
                    DateTime.Now > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.Value : 0) :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)))
                {
                    return (data_ricezione.HasValue ?
                            data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.Value : 0) :
                            dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)).ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string dataMassimaPagamentoAvviso
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault() != null &&
                    DateTime.Now > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_scadenza_pagamento.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_scadenza_pagamento.Value : 0) :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)))
                {
                    return (data_ricezione.HasValue ?
                            data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_scadenza_pagamento.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_scadenza_pagamento.Value : 0) :
                            dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)).ToShortDateString();
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

                if (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.GEST_ORDINARIA &&
                   (id_entrata == anagrafica_entrate.ICI ||
                    id_entrata == anagrafica_entrate.IMU ||
                    id_entrata == anagrafica_entrate.TASI) &&
                    tab_unita_contribuzione.Where(d => d.id_oggetto_contribuzione.HasValue).FirstOrDefault() != null)
                {
                    v_result = tab_unita_contribuzione.Where(d => d.id_oggetto_contribuzione.HasValue).FirstOrDefault().periodo_rif_a.Value.Month == 6 ? "ACCONTO" : "SALDO";
                    v_result = v_result + " " + anagrafica_entrate.descrizione_entrata + " ordinaria anno: " + tab_unita_contribuzione.Where(d => d.id_oggetto_contribuzione.HasValue).FirstOrDefault().anno_rif /*+ " (" + identificativo_avv_pag.Trim() + ")."*/;
                }

                return v_result;
            }
        }

        [DisplayName("Identificativo Avviso")]
        public string NumeroAvviso
        {
            get
            {
                string v_numeroAvviso = string.Empty;

                if (id_entrata == anagrafica_entrate.CDS &&
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO)
                {
                    v_numeroAvviso = string.IsNullOrEmpty(numero_avv_pag) ? identificativo_avv_pag : numero_avv_pag;
                }
                else if (identificativo_avv_pag != null &&
                         identificativo_avv_pag.Length > 0)
                {
                    v_numeroAvviso = identificativo_avv_pag;
                }
                else
                {
                    if (anagrafica_tipo_avv_pag != null)
                    {
                        v_numeroAvviso = anagrafica_tipo_avv_pag.cod_tipo_avv_pag + "/" + anno_riferimento + "/" + numero_avv_pag;
                    }
                    else
                    {
                        v_numeroAvviso = anno_riferimento + "/" + numero_avv_pag;
                    }
                }

                if (v_numeroAvviso == null)
                {
                    v_numeroAvviso = string.Empty;
                }

                return v_numeroAvviso.Trim();
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

        //Il dottore ha voluto cambiare il dettaglio bene, ora i beni collegati all'avviso possono essere più di uno quindi questo campo diviene obsoleto
        [DisplayName("Tipo Bene")]
        public string TipoBene
        {
            get
            {
                //if (TAB_SUPERVISIONE_FINALE_V2.Count > 0 &&
                //    TAB_SUPERVISIONE_FINALE_V2.Where(d => d.ID_AVVPAG_EMESSO == id_tab_avv_pag && !cod_stato.Contains("ATT-")).FirstOrDefault() != null &&
                //    TAB_SUPERVISIONE_FINALE_V2.Where(d => d.ID_AVVPAG_EMESSO == id_tab_avv_pag && !cod_stato.Contains("ATT-")).FirstOrDefault().tab_profilo_contribuente_new != null)
                //{
                //    tab_profilo_contribuente_new v_profiloContribuente = TAB_SUPERVISIONE_FINALE_V2.Where(d => d.ID_AVVPAG_EMESSO == id_tab_avv_pag && !cod_stato.Contains("ATT-")).FirstOrDefault().tab_profilo_contribuente_new;
                //    string v_descrizioneBene = string.Empty;

                //    if (v_profiloContribuente.tab_tipo_bene != null &&
                //        v_profiloContribuente.tab_tipo_bene.codice_bene == tab_tipo_bene.SIGLA_VEICOLI &&
                //        v_profiloContribuente.tab_veicoli != null)
                //    {
                //        v_descrizioneBene = v_profiloContribuente.tab_tipo_bene.descrizione.ToUpper();
                //        v_tipoBene = v_descrizioneBene + " - Targa N.: " + v_profiloContribuente.tab_veicoli.targa;
                //    }
                //    else
                //    {
                //        if (v_profiloContribuente.tab_terzo_debitore != null &&
                //            v_profiloContribuente.tab_terzo_debitore.tab_tipo_bene != null)
                //        {
                //            v_descrizioneBene = v_profiloContribuente.tab_terzo_debitore.tab_tipo_bene.descrizione.ToUpper();
                //            string v_nominativo = v_profiloContribuente.tab_terzo_debitore.Nominativo;

                //            if (v_profiloContribuente.tab_terzo_debitore.tab_tipo_bene.codice_bene == tab_tipo_bene.SIGLA_LOCAZIONE)
                //            {
                //                v_tipoBene = v_descrizioneBene + " da " + v_nominativo + " - N. Contr.: " + v_profiloContribuente.tab_terzo_debitore.nr_registrazione_contratto;
                //            }
                //            else if (v_profiloContribuente.tab_terzo_debitore.tab_tipo_bene.codice_bene == tab_tipo_bene.SIGLA_STIPENDI)
                //            {
                //                v_tipoBene = v_descrizioneBene + " da datore " + v_nominativo;
                //            }
                //            else if (v_profiloContribuente.tab_terzo_debitore.tab_tipo_bene.codice_bene == tab_tipo_bene.SIGLA_PENSIONI)
                //            {
                //                v_tipoBene = v_descrizioneBene + " da ente pensionistico " + v_nominativo;
                //            }
                //            else if (v_profiloContribuente.tab_terzo_debitore.tab_tipo_bene.codice_bene == tab_tipo_bene.SIGLA_BANCHE)
                //            {
                //                v_tipoBene = v_descrizioneBene + " di " + v_nominativo + " - ABI: " + v_profiloContribuente.tab_terzo_debitore.abi_cc + " CAB: " + v_profiloContribuente.tab_terzo_debitore.cab_cc;
                //            }
                //        }
                //    }
                //}

                return string.Empty;
            }
        }

        public string IsVisibleBene
        {
            get
            {
                string IsVisibleBene = "false";

                if (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI)
                {
                    IsVisibleBene = "true";
                }

                return IsVisibleBene;
            }
        }

        public string IsVisibleAtti
        {
            get
            {
                string IsVisibleAtti = "false";

                if (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO)
                {
                    IsVisibleAtti = "true";
                }

                return IsVisibleAtti;
            }
        }

        public string IsVisibleAcqua
        {
            get
            {
                string IsVisibleAcqua = "false";

                if (anagrafica_tipo_avv_pag.id_tipo_avvpag == anagrafica_tipo_avv_pag.PREAVVISO_FATTURAZIONE)
                {
                    IsVisibleAcqua = "true";
                }

                return IsVisibleAcqua;
            }
        }

        public bool ExistsAtti
        {
            get
            {
                return TAB_JOIN_AVVCOA_INGFIS_V21.Count > 0;
            }
        }

        public bool ExistsAttiIntimSoll
        {
            get
            {
                return TAB_JOIN_AVVCOA_INGFIS_V21.Any(d => d.tab_avv_pag != null && ((d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.SOLL_PRECOA) &&
                                                            (d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) ||
                                                             d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.SOSPESO)))) || !string.IsNullOrEmpty(avvisoBonario);
            }
        }

        public bool ExistsAttiCoattivi
        {
            get
            {
                return TAB_JOIN_AVVCOA_INGFIS_V21.Any(d => d.tab_avv_pag != null && ((d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI ||
                                                            d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI) &&
                                                            (d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) ||
                                                             d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.SOSPESO))));
            }
        }
        public bool FlagImportoAttiSuccessivi
        {
            get
            {
                if ((anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI ||
                    anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI &&
                    (imp_tot_avvpag_rid - imp_tot_pagato) > importoTotDaPagare)
                    ||
                    (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI && id_tipo_avvpag > 250 &&
                    (imp_tot_avvpag_rid - imp_tot_pagato) > importoTotDaPagare)
                    ||
                    (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.AVVISI_CAUTELARI && id_tipo_avvpag == anagrafica_tipo_avv_pag.IPOTECA &&
                    (imp_tot_avvpag_rid - imp_tot_pagato) > importoTotDaPagare))


                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool ExistsAttiSuccessivi
        {
            get
            {
                return !(new List<int> { anagrafica_tipo_servizi.ING_FISC,
                    anagrafica_tipo_servizi.SOLL_PRECOA,
                    anagrafica_tipo_servizi.INTIM,
                    anagrafica_tipo_servizi.AVVISI_CAUTELARI,
                    anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_ORDINE_TERZO,
                    anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_CITAZIONE_TERZO,
                    anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_IMMOBILIARI,
                    anagrafica_tipo_servizi.AVVISI_PIGNORAMENTO_MOBILIARI,}).Contains(anagrafica_tipo_avv_pag.id_servizio) &&
                    tab_unita_contribuzione1.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) && !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)).Count() > 0;
            }
        }

        public bool ExistsIspezioni
        {
            get
            {
                return anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ING_FISC &&
                       tab_ingiunzioni_ispezione.Any(d => d.cod_stato.StartsWith(Data.tab_ingiunzioni_ispezione.VAL) &&
                                                          d.join_ispezioni_ingiunzioni.Any(x => x.cod_stato.StartsWith(Data.join_ispezioni_ingiunzioni.VAL) &&
                                                                                                x.tab_ispezioni_coattivo_new != null &&
                                                                                                x.tab_ispezioni_coattivo_new.cod_stato.StartsWith(Data.tab_ispezioni_coattivo_new.VAL)));
            }
        }

        public bool ExistsOrdineOrigine
        {
            get
            {
                return TAB_SUPERVISIONE_FINALE_V21 != null && TAB_SUPERVISIONE_FINALE_V21.id_avvpag_preavviso_collegato.HasValue;
            }
        }

        public bool ExistsProceduraConcorsuale
        {
            get
            {
                return join_contribuenti_procedure_concorsuali.Any(d => d.tab_doc_input != null &&
                                                                        d.tab_doc_input.id_tipo_doc_entrate == tab_tipo_doc_entrate.ID_TIPO_DOC_ENTRATE_PROC_CONC &&
                                                                        d.cod_stato.StartsWith(Publisoftware.Data.join_contribuenti_procedure_concorsuali.ATT_ATT));
            }
        }

        public bool IsFatturazioneAcqua
        {
            get
            {
                if (id_tipo_avvpag == Publisoftware.Data.anagrafica_tipo_avv_pag.PREAVVISO_FATTURAZIONE)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsIstanzaPresentabile
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault() != null &&
                    DateTime.Now.Date > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.Value : 0).Date :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)).Date)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsAttoSuccessivo
        {
            get
            {
                if (tab_unita_contribuzione1.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                         d.tab_avv_pag != null &&
                                                        !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) &&
                                                        !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE))
                                            .Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsAttoSuccessivoIngFisc
        {
            get
            {
                bool v_result = true;

                List<tab_avv_pag> v_avvisiCollegati = tab_unita_contribuzione.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                                          d.id_avv_pag_collegato.HasValue)
                                                                             .Select(d => d.tab_avv_pag1)
                                                                             .ToList();

                if (v_avvisiCollegati.Count == 0)
                {
                    v_result = false;
                }
                else
                {
                    foreach (tab_avv_pag v_avvisoCollegato in v_avvisiCollegati)
                    {
                        if (v_avvisoCollegato.tab_unita_contribuzione1.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                                   d.tab_avv_pag.id_tab_avv_pag != id_tab_avv_pag &&
                                                                                   d.tab_avv_pag.dt_emissione > dt_emissione &&
                                                                                  !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                             .Count() == 0)
                        {
                            v_result = false;
                            break;
                        }
                    }
                }

                return v_result;
            }
        }

        public string AttoSuccessivo
        {
            get
            {
                if (IsAttoSuccessivo)
                {
                    List<tab_unita_contribuzione> v_unitaContribuzioneList = tab_unita_contribuzione1.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                                                                  d.tab_avv_pag != null &&
                                                                                                                 !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO))
                                                                                                     .ToList();
                    tab_avv_pag v_avviso = v_unitaContribuzioneList.OrderByDescending(d => d.tab_avv_pag.dt_emissione)
                                                                   .FirstOrDefault()
                                                                   .tab_avv_pag;

                    return v_avviso.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " " + v_avviso.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool IsSameIstanza
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault() != null &&
                    DateTime.Now.Date > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_definitivita_avviso.Value : 0).Date :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)).Date)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsAvvisoPagabile
        {
            get
            {
                if (anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault() != null &&
                    DateTime.Now > (data_ricezione.HasValue ?
                    data_ricezione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_scadenza_pagamento.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_scadenza_pagamento.Value : 0) :
                    dt_emissione.Value.AddDays(anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.HasValue ? anagrafica_tipo_avv_pag.tab_modalita_rate_avvpag_view.FirstOrDefault().GG_massimi_data_emissione.Value : 0)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsProvvedimentoPresentabile
        {
            get
            {
                if (TAB_JOIN_AVVCOA_INGFIS_V21.Where(d => d.tab_avv_pag != null &&
                                                         !d.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) &&
                                                          d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio > anagrafica_tipo_servizi.INTIM &&
                                                         (d.tab_avv_pag.flag_esito_sped_notifica == "1" || (d.tab_avv_pag.dt_emissione.HasValue && d.tab_avv_pag.dt_emissione.Value.AddDays(90) > DateTime.Now)))
                                             .Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool IsAvvisoSgravabile
        {
            get
            {
                List<string> p_codStatoList = new List<string> { anagrafica_stato_avv_pag.VAL_EME, anagrafica_stato_avv_pag.VALIDO_COATTIVO, anagrafica_stato_avv_pag.VAL_ING };

                if ((flag_rateizzazione_bis == "0" || flag_rateizzazione_bis == null) // Non rateizzato
                    && (p_codStatoList.Contains(anagrafica_stato.cod_stato_riferimento) || anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsAvvisoStatoAnnRetDanDar
        {
            get
            {
                if (anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE) ||
                    anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE) ||
                    anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) ||
                    anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.RETTIFICATO_SGRAVIO) ||
                    //anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO_SGRAVIO) ||
                    anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO_UFFICIO))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsAvvisoStatoAnnDan
        {
            get
            {
                if (anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE) ||
                    anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.ANNULLATO_SGRAVIO) ||
                    anagrafica_stato.cod_stato.Contains(anagrafica_stato_avv_pag.SOSPESO_UFFICIO))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsAvvisoStatoAnnDanDar
        {
            get
            {
                if (anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) ||
                    anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DAANNULLARE) ||
                    anagrafica_stato.cod_stato.StartsWith(anagrafica_stato_avv_pag.DARETTIFICARE))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string IntimazioneCorrelata
        {
            get
            {
                return string.Empty;
#if NON_CANCELLARE_POTREBBE_DOVER_ESSERE_RIMESSO
                string v_intimazioneCorrelata = string.Empty;
                //TAB_JOIN_AVVCOA_INGFIS_V2 è il legame verso l'atto coattivo (tab_avv_pag con id_servizio>5)
                //TAB_JOIN_AVVCOA_INGFIS_V21 è il legame verso l'ingiunzione (tab_avv_pag con id_servizio=3)
                if (TAB_JOIN_AVVCOA_INGFIS_V21.Any(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                                                          d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) &&
                                                          d.tab_avv_pag.flag_esito_sped_notifica == "1"))
                {
                    tab_avv_pag v_avvisoCoattivo = TAB_JOIN_AVVCOA_INGFIS_V21.Where(d => d.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.INTIM &&
                                                          d.tab_avv_pag.anagrafica_stato.cod_stato_riferimento.Contains(anagrafica_stato_avv_pag.VALIDO) &&
                                                          d.tab_avv_pag.flag_esito_sped_notifica == "1")
                                                          .OrderByDescending(d => d.ID_JOIN_AVVCOA_INGFIS).FirstOrDefault().tab_avv_pag;

                    //Il dottore ha voluto controllare se la data di avvenuta notifica rientra nei 180 giorni dalla data odierna
                    if (v_avvisoCoattivo.data_avvenuta_notifica.HasValue && DateTime.Now.Subtract(v_avvisoCoattivo.data_avvenuta_notifica.Value).Days <= 180)
                    {
                        v_intimazioneCorrelata = v_avvisoCoattivo.data_avvenuta_notifica_String;
                    }
                }
                return v_intimazioneCorrelata;
#endif
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
                //Il dottore ha voluto che le spese di notifica siano anche gli SPE, ONV e RSP (oltre che i NOT), inoltre le tab_contribuzione devono avere id_avv_pag e id_avv_pag_iniziale uguali
                if (anagrafica_tipo_avv_pag != null &&
                    anagrafica_tipo_avv_pag.IsAttoSuccessivoIngiunzione &&
                    tab_contribuzione.Any(d => (d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_NOT ||
                                                d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SPE ||
                                                d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_ONV ||
                                                d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_RSP) &&
                                               !d.cod_stato.StartsWith(Publisoftware.Data.tab_contribuzione.ANN) &&
                                                d.id_avv_pag == d.id_avv_pag_iniziale))
                {
                    return tab_contribuzione.Where(d => (d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_NOT ||
                                                         d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SPE ||
                                                         d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_ONV ||
                                                         d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_RSP) &&
                                                        !d.cod_stato.StartsWith(Publisoftware.Data.tab_contribuzione.ANN) &&
                                                         d.id_avv_pag == d.id_avv_pag_iniziale)
                                            .Sum(d => d.importo_residuo);
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal importoSpeseCoattiveDecimal
        {
            get
            {
                //Il dottore ha voluto che le tab_contribuzione debbano avere id_avv_pag e id_avv_pag_iniziale uguali
                if (anagrafica_tipo_avv_pag != null &&
                    anagrafica_tipo_avv_pag.IsAttoSuccessivoIngiunzione &&
                    tab_contribuzione.Any(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_COA &&
                                              !d.cod_stato.StartsWith(Publisoftware.Data.tab_contribuzione.ANN) &&
                                               d.id_avv_pag == d.id_avv_pag_iniziale))
                {
                    return tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_COA &&
                                                       !d.cod_stato.StartsWith(Publisoftware.Data.tab_contribuzione.ANN) &&
                                                        d.id_avv_pag == d.id_avv_pag_iniziale)
                                            .Sum(d => d.importo_residuo);
                }
                else
                {
                    return 0;
                }
            }
        }

        public string ImportoInteressi
        {
            get
            {
                return importoInteressiDecimal.ToString("C");
            }
        }

        public string ImportoSanzioniResiduo
        {
            get
            {
                return importoSanzioniResiduoDecimal.ToString("C");
            }
        }

        public decimal importoInteressiDecimal
        {
            get
            {
                if (tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_INTERESSI && d.importo_da_pagare > 1).Count() > 0)
                {
                    //Il dottore e Angelo hanno detto che l'importo degli interessi deve sempre essere a 0
                    return 0;
                    //return tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_INTERESSI && d.importo_da_pagare > 1).Sum(d => d.importo_da_pagare);
                }
                else
                {
                    return 0;
                }
            }
        }

        //Il dottore ha voluto per Lombardia usare l'importo_da_pagare (sottratto dell'importo_sanzioni_eliminate_eredi) mentre per gli altri comuni l'importo_residuo
        public decimal importoSanzioniResiduoDecimal
        {
            get
            {
                if (tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SANZIONI).Count() > 0)
                {
                    if (id_ente == anagrafica_ente.ID_ENTE_REGIONE_LOMBARDIA)
                    {
                        decimal v_residuo = tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SANZIONI).Sum(d => d.importo_da_pagare);

                        return v_residuo - (importo_sanzioni_eliminate_eredi.HasValue ? importo_sanzioni_eliminate_eredi.Value : 0);
                    }
                    else
                    {
                        return tab_contribuzione.Where(d => d.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_SANZIONI).Sum(d => d.importo_residuo);
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public string SoggettoDebitore
        {
            get
            {
                if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay +
                           " terzo debitore di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                           ", referente del contribuente " + v_joinAvvPaggSoggDeb.tab_avv_pag.tab_contribuente.contribuenteDisplay +
                           ", in qualità di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione;
                }
                else if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                           " in qualità di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione +
                           " del contribuente " + v_joinAvvPaggSoggDeb.tab_avv_pag.tab_contribuente.contribuenteDisplay;
                }
                else if (join_avv_pag_soggetto_debitore.Any(d => d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    return "Soggetto debitore: " + v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay +
                           " terzo debitore del contribuente " + v_joinAvvPaggSoggDeb.tab_avv_pag.tab_contribuente.contribuenteDisplay;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string SoggettoDebitoreTerzo
        {
            get
            {
                return SoggettoDebitore;
                //if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                //{
                //    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                //    return "Terzo debitore di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay +
                //        ", referente del contribuente " + v_joinAvvPaggSoggDeb.tab_avv_pag.tab_contribuente.contribuenteDisplay +
                //        ", in qualità di " + (v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione);
                //}
                //else
                //{
                //    return string.Empty;
                //}
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

        //30/03/2020 Il dottore ha chiesto di modificare la visualizzazione in Griglia del Campo
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
                        flag_esito_sped_notifica == "0")
                    {
                        return "Notificato con Esito Negativo";
                    }
                    else if (flag_spedizione_notifica == "1" &&
                             flag_esito_sped_notifica == null)
                    {
                        return "Esito notifica non pervenuto";
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        [DisplayName("Descrizione Stato")]
        public string DescrizioneStatoNew
        {
            get
            {
                if (anagrafica_stato != null)
                {
                    string desrizioneStato = anagrafica_stato.desc_stato_riferimento;
                    decimal minImpTotRid = 17m;

                    //List<int> idsTipoServizi = new List<int> {
                    //                                          anagrafica_tipo_servizi.GEST_ORDINARIA,
                    //                                          anagrafica_tipo_servizi.ACCERTAMENTO,
                    //                                          anagrafica_tipo_servizi.RISC_PRECOA,
                    //                                          anagrafica_tipo_servizi.ING_FISC,
                    //                                          anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM,
                    //                                          anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO,
                    //                                          anagrafica_tipo_servizi.AVVISI_ORDINARI_NON_SOGGETTO_AD_ACCERTAMENTO
                    //                                         };

                    //List<int> idsTipoServizi2 = new List<int> { anagrafica_tipo_servizi.SOLL_PRECOA, anagrafica_tipo_servizi.INTIM };

                    if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                      ((imp_tot_avvpag_rid_decimal - imp_tot_pagato_decimal) < 1))
                    {
                        desrizioneStato = "Pagato";
                    }
                    else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                            ((imp_tot_avvpag_rid_decimal - imp_tot_pagato_decimal) < minImpTotRid))
                    {
                        desrizioneStato = "Con residuo da pagare inf. a min.";
                    }
                    else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_RAT))
                    {
                        desrizioneStato = "Valido rateizzato";
                    }
                    else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO))
                    {
                        tab_unita_contribuzione v_unita_contr = tab_unita_contribuzione1.Where(x => !x.cod_stato.StartsWith(anagrafica_stato_unita_contribuzione.ANNULLATO) &&
                                                                                                   (!x.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)))
                                                                                        //.OrderBy(x => x.data_stato)
                                                                                        .FirstOrDefault();

                        if (v_unita_contr != null)
                        {
                            if (v_unita_contr.tab_avv_pag.flag_rateizzazione_bis == "1" ||
                                v_unita_contr.tab_avv_pag.cod_stato.Equals(anagrafica_stato_avv_pag.SOSPESO_RATEIZZAZIONE))
                            {
                                //desrizioneStato += " rateizzato";
                                desrizioneStato = "Valido rateizzato";
                            }
                        }
                    }
                    else
                    {
                        //if (anagrafica_stato_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                        //    idsTipoServizi2.Contains(anagrafica_tipo_avv_pag.id_servizio) &&
                        //    tab_unita_contribuzione.Where(x => x.id_avv_pag_collegato.HasValue && 
                        //                                       x.tab_avv_pag1.tab_unita_contribuzione1.Any(y => !y.tab_avv_pag.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) && 
                        //                                                                                         y.tab_avv_pag.dt_emissione > dt_emissione))
                        //                           .Count() > 0)
                        //{
                        //    desrizioneStato = "Avviso con atti presupposti inclusi in Atto successivo";
                        //}
                        //else
                        if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                            id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_FERMO_AMM &&
                            TAB_SUPERVISIONE_FINALE_V22.Where(x => (x.COD_STATO != null) &&
                                                                  (!x.COD_STATO.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)) &&
                                                                    x.id_avvpag_preavviso_collegato == id_tab_avv_pag &&
                                                                    x.ID_AVVPAG_EMESSO != null)
                                                       .Count() > 0)
                        {
                            desrizioneStato = "Preavviso con fermo iscritto";
                        }
                        else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                                 id_tipo_avvpag == anagrafica_tipo_avv_pag.PRE_IPOTECA &&
                                 TAB_SUPERVISIONE_FINALE_V22.Where(x => (x.COD_STATO != null) &&
                                                                       (!x.COD_STATO.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)) &&
                                                                         x.id_avvpag_preavviso_collegato == id_tab_avv_pag &&
                                                                         x.ID_AVVPAG_EMESSO != null)
                                                            .Count() > 0)
                        {
                            desrizioneStato = "Preavviso con ipoteca iscritta";
                        }
                        else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                                 id_tipo_avvpag == anagrafica_tipo_avv_pag.PIGN_TERZI_ORD &&
                                 TAB_SUPERVISIONE_FINALE_V22.Where(x => (x.COD_STATO != null) &&
                                                                       (!x.COD_STATO.StartsWith(anagrafica_stato_avv_pag.ANNULLATO)) &&
                                                                         x.id_avvpag_preavviso_collegato == id_tab_avv_pag &&
                                                                         x.ID_AVVPAG_EMESSO != null)
                                                            .Count() > 0)
                        {
                            desrizioneStato = "Ordine con citazione terzo";
                        }
                        else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO) &&
                                 join_tab_avv_pag_tab_doc_input.Any(x => x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_ANN_RET &&
                                                                        !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_TEMPORANEO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ESITATA)))
                        {
                            desrizioneStato = "Sospeso per istanza di ann.";
                        }
                        else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO) &&
                                 join_tab_avv_pag_tab_doc_input.Any(x => x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_RICORSI &&
                                                                        !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_TEMPORANEO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ESITATA)))
                        {
                            desrizioneStato = "Sospeso per ricorso/citazione";
                        }
                        else if (cod_stato.Equals(anagrafica_stato_avv_pag.SOSPESO_RATEIZZAZIONE) ||
                                (cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO) &&
                                 flag_rateizzazione_bis == "1" &&
                                 tab_unita_contribuzione1.Any(y => !y.cod_stato.StartsWith(anagrafica_stato_avv_pag.ANNULLATO) &&
                                                                    y.tab_avv_pag.anagrafica_tipo_avv_pag.id_servizio.Equals(anagrafica_tipo_servizi.SERVIZI_RATEIZZAZIONE_COA))))
                        {
                            desrizioneStato = "Valido rateizzato";
                        }
                        else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.SOSPESO) &&
                                 join_tab_avv_pag_tab_doc_input.Any(x => x.tab_doc_input.tab_tipo_doc_entrate.id_tipo_doc == tab_tipo_doc_entrate.TIPO_DOC_PROCEDURA_CONCORSUALE &&
                                                                        !x.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO) &&
                                                                        !x.tab_doc_input.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO)))
                        {
                            desrizioneStato = "Sospeso per proc. concorsuale";
                        }
                    }

                    return desrizioneStato;
                }
                else
                {
                    return string.Empty;
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

                if ((anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO ||
                     anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO) &&
                     cod_stato.StartsWith(anagrafica_stato_avv_pag.VAL_EME))
                {
                    return importo_accertamento_Euro;
                }
                else
                {
                    return imp_tot_avvpag_rid_Euro;
                }
            }
        }

        public decimal importoTotDaPagare
        {
            get
            {
                if (importo_tot_da_pagare.HasValue)
                {
                    return importo_tot_da_pagare.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string ImportoDaPagare
        {
            get
            {
                //if ((anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO ||
                //     anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO) &&
                //     importo_tot_da_pagare == imp_tot_avvpag)
                //{
                //    return importo_accertamento_Euro;
                //}
                //else
                //{
                return importo_tot_da_pagare_Euro;
                //}
            }
        }

        public string importo_tot_da_pagare_Euro
        {
            get
            {
                return importo_tot_da_pagare_decimal.ToString("C");
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
                //                                      riscosso_avvisi_coattivi_det_riscossione_decimal -
                //                                      riscosso_avvisi_not_coattivi_det_riscossione_decimal -
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
                if (importo_tot_da_pagare.HasValue)
                {
                    return importo_tot_da_pagare.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static string importoAccertamento_Euro(DateTime? v_data, tab_avv_pag v_avviso)
        {
            return importoAccertamento_decimal(v_data, v_avviso).ToString("C");
        }

        //Metodo statico (non proprietà) in cui v_data è la data di presentazione se BO altrimenti è oggi
        public static decimal importoAccertamento_decimal(DateTime? v_data, tab_avv_pag v_avviso)
        {
            if (!v_data.HasValue)
            {
                v_data = DateTime.Now;
            }

            decimal v_return = 0;

            if (v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO)
            {
                if (v_avviso.data_ricezione.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_ricezione.Value).TotalDays;

                    if (v_giorni > 60 &&
                        v_giorni <= 120)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_61_90.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else if (v_giorni > 120)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_121.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_121.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else
                    {
                        v_return = v_avviso.imp_tot_avvpag_rid.Value;
                    }
                }
                else if (v_avviso.data_avvenuta_notifica.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_avvenuta_notifica.Value).TotalDays;

                    if (v_giorni > 90 &&
                        v_giorni <= 150)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_61_90.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else if (v_giorni > 150)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_121.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_121.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else
                    {
                        v_return = v_avviso.imp_tot_avvpag_rid.Value;
                    }
                }
                else
                {
                    v_return = v_avviso.imp_tot_avvpag_rid.Value;
                }
            }
            else if (v_avviso.anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO)
            {
                if (v_avviso.data_ricezione.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_ricezione.Value).TotalDays;

                    if (v_giorni > 60 &&
                        v_giorni <= 120)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_61_90.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else if (v_giorni > 120)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_121.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_121.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else
                    {
                        v_return = v_avviso.importo_ridotto.Value;
                    }
                }
                else if (v_avviso.data_avvenuta_notifica.HasValue)
                {
                    int v_giorni = (int)(v_data.Value - v_avviso.data_avvenuta_notifica.Value).TotalDays;

                    if (v_giorni > 90 &&
                        v_giorni <= 150)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_61_90.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else if (v_giorni > 150)
                    {
                        v_return = v_avviso.imp_tot_avvpag.Value +
                                   (v_avviso.imp_maggiorazione_onere_riscossione_121.HasValue ? v_avviso.imp_maggiorazione_onere_riscossione_121.Value : 0)
                                   /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (v_avviso.imp_tot_interesse_mora_giornaliero.HasValue ? v_avviso.imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                    }
                    else
                    {
                        v_return = v_avviso.importo_ridotto.Value;
                    }
                }
                else
                {
                    v_return = v_avviso.importo_ridotto.Value;
                }
            }
            else
            {
                v_return = v_avviso.imp_tot_avvpag_rid.Value;
            }

            return v_return;
        }

        public string importo_accertamento_Euro
        {
            get
            {
                return importo_accertamento_decimal.ToString("C");
            }
        }

        public decimal importo_accertamento_decimal
        {
            get
            {
                decimal v_return = 0;

                if (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO)
                {
                    if (data_ricezione.HasValue)
                    {
                        int v_giorni = (int)(DateTime.Now - data_ricezione.Value).TotalDays;

                        if (v_giorni > 60 &&
                            v_giorni <= 120)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_61_90.HasValue ? imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else if (v_giorni > 120)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_121.HasValue ? imp_maggiorazione_onere_riscossione_121.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else
                        {
                            v_return = imp_tot_avvpag_rid.Value;
                        }
                    }
                    else if (data_avvenuta_notifica.HasValue)
                    {
                        int v_giorni = (int)(DateTime.Now - data_avvenuta_notifica.Value).TotalDays;

                        if (v_giorni > 90 &&
                            v_giorni <= 150)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_61_90.HasValue ? imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                       /*((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else if (v_giorni > 150)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_121.HasValue ? imp_maggiorazione_onere_riscossione_121.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else
                        {
                            v_return = imp_tot_avvpag_rid.Value;
                        }
                    }
                    else
                    {
                        v_return = imp_tot_avvpag_rid.Value;
                    }
                }
                else if (anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO)
                {
                    if (data_ricezione.HasValue)
                    {
                        int v_giorni = (int)(DateTime.Now - data_ricezione.Value).TotalDays;

                        if (v_giorni > 60 &&
                            v_giorni <= 120)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_61_90.HasValue ? imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else if (v_giorni > 120)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_121.HasValue ? imp_maggiorazione_onere_riscossione_121.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else
                        {
                            v_return = importo_ridotto.Value;
                        }
                    }
                    else if (data_avvenuta_notifica.HasValue)
                    {
                        int v_giorni = (int)(DateTime.Now - data_avvenuta_notifica.Value).TotalDays;

                        if (v_giorni > 90 &&
                            v_giorni <= 150)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_61_90.HasValue ? imp_maggiorazione_onere_riscossione_61_90.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else if (v_giorni > 150)
                        {
                            v_return = imp_tot_avvpag.Value +
                                       (imp_maggiorazione_onere_riscossione_121.HasValue ? imp_maggiorazione_onere_riscossione_121.Value : 0)
                                       /*+ ((v_giorni - 90) > 0 ? (v_giorni - 90) : 0) * (imp_tot_interesse_mora_giornaliero.HasValue ? imp_tot_interesse_mora_giornaliero.Value : 0)*/;
                        }
                        else
                        {
                            v_return = importo_ridotto.Value;
                        }
                    }
                    else
                    {
                        v_return = importo_ridotto.Value;
                    }
                }
                else
                {
                    v_return = imp_tot_avvpag_rid.Value;
                }

                return v_return;
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

        public string riscosso_avvisi_coattivi_det_riscossione_Euro
        {
            get
            {
                return riscosso_avvisi_coattivi_det_riscossione_decimal.ToString("C");
            }
        }

        public decimal riscosso_avvisi_coattivi_det_riscossione_decimal
        {
            get
            {
                if (riscosso_avvisi_coattivi_det_riscossione.HasValue)
                {
                    return riscosso_avvisi_coattivi_det_riscossione.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string riscosso_avvisi_not_coattivi_det_riscossione_Euro
        {
            get
            {
                return riscosso_avvisi_not_coattivi_det_riscossione_decimal.ToString("C");
            }
        }

        public decimal riscosso_avvisi_not_coattivi_det_riscossione_decimal
        {
            get
            {
                if (riscosso_avvisi_not_coattivi_det_riscossione.HasValue)
                {
                    return riscosso_avvisi_not_coattivi_det_riscossione.Value;
                }
                else
                {
                    return 0;
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

        //public Decimal? importo_tot_da_pagare_arrotondato
        //{
        //    get
        //    {
        //        if (imp_tot_avvpag_arrotondato.HasValue)
        //        {
        //            Decimal da_pagare = imp_tot_avvpag_arrotondato.Value;
        //            if (imp_tot_pagato.HasValue)
        //                da_pagare = Decimal.Subtract(da_pagare, imp_tot_pagato.Value);
        //            return da_pagare;
        //        }
        //        return null;
        //    }
        //}

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

        public string importo_atti_successivi_Euro
        {
            get
            {
                return importo_atti_successivi_decimal.ToString("C");
            }
        }

        public decimal importo_atti_successivi_decimal
        {
            get
            {
                return riscosso_avvisi_coattivi_det_riscossione_decimal + riscosso_avvisi_not_coattivi_det_riscossione_decimal;
            }
        }

        public string importo_definizione_agevolata_eredi_Euro
        {
            get
            {
                return importo_definizione_agevolata_eredi_decimal.ToString("C");
            }
        }

        public decimal importo_definizione_agevolata_eredi_decimal
        {
            get
            {
                return interessi_eliminati_definizione_agevolata_decimal + sanzioni_eliminate_definizione_agevolata_decimal + importo_sanzioni_eliminate_eredi_decimal;
            }
        }

        public decimal importo_ridotto_decimal
        {
            get
            {
                if (importo_ridotto.HasValue)
                {
                    return importo_ridotto.Value;
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
                return importo_ridotto_decimal.ToString("C");
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
                    return string.Empty;
                }
            }
        }

        public decimal importoVersamenti
        {
            get
            {
                return this.imp_tot_avvpag_rid_decimal - this.importoTotDaPagare;
            }
        }

        public decimal? InteressiRateizzazione
        {
            get
            {
                decimal? intRat = this.tab_unita_contribuzione_valide().Where(w => w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale != null && w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == anagrafica_voci_contribuzione.CODICE_TRIBUTO_IRA).Select(s => s.importo_unita_contribuzione).Sum();  //interessi Rateizzazione            ;
                return intRat;
            }
        }

        public decimal? Interessi
        {
            get
            {
                return this.tab_unita_contribuzione_valide().Where(w => w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale != null && w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_INTERESSI).Select(s => s.importo_unita_contribuzione).Sum();  //interessi Rateizzazione            ;
            }
        }

        public decimal? InteressiPregressi
        {
            get
            {
                return this.tab_unita_contribuzione_valide().Where(w => w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale != null && w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() != anagrafica_voci_contribuzione.CODICE_TRIBUTO_IRA).Select(s => s.importo_unita_contribuzione).Sum();
            }
        }

        public decimal? SpeseCoattive
        {
            get
            {
                return this.tab_unita_contribuzione_valide().Where(w => w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale != null && w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_COA).Select(s => s.importo_unita_contribuzione).Sum();
            }
        }

        public decimal? SpeseNotifica
        {
            get
            {
                return this.tab_unita_contribuzione_valide().Where(w => w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale != null && w.tab_tipo_voce_contribuzione.codice_tributo_ministeriale.Trim() == tab_tipo_voce_contribuzione.CODICE_NOT).Select(s => s.importo_unita_contribuzione).Sum();
            }
        }

        //public decimal? ImportoTotaleAvviso
        //{
        //    get
        //    {
        //        return importo_tot_da_pagare_arrotondato + InteressiPregressi + InteressiRateizzazione + SpeseCoattive + SpeseNotifica;
        //    }
        //}

        public string stato
        {
            get
            {
                if (anagrafica_stato != null)
                {
                    if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                        imp_tot_avvpag_rid_arrotondato_decimal - imp_tot_pagato_decimal <= 0)
                    {
                        return "Avviso pagato";
                    }
                    else if (cod_stato.StartsWith(anagrafica_stato_avv_pag.VALIDO) &&
                             imp_tot_avvpag_rid_arrotondato_decimal - imp_tot_pagato_decimal <= 17 &&
                             imp_tot_avvpag_rid_arrotondato_decimal - imp_tot_pagato_decimal > 0)
                    {
                        return "Avviso con residuo da pagare inf. a min.";
                    }
                    else
                    {
                        return anagrafica_stato.desc_stato_riferimento;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string statoOld
        {
            get
            {
                if (anagrafica_stato != null)
                {
                    return anagrafica_stato.desc_stato;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public IQueryable<tab_dett_riscossione> tab_dett_riscossione_totali
        {
            get
            {
                return this.tab_dett_riscossione
                        .Union(this.tab_dett_riscossione1)
                        .Union(this.tab_dett_riscossione2)
                        .Union(this.tab_dett_riscossione3)
                        .Union(this.tab_dett_riscossione4)
                        .Union(this.tab_dett_riscossione5)
                        .Union(this.tab_dett_riscossione11)
                        .Union(this.tab_dett_riscossione21)
                        .Union(this.tab_dett_riscossione31)
                        .Distinct().AsQueryable<tab_dett_riscossione>();
            }
        }

        public bool IsIstanzaVisibile
        {
            get
            {
                return join_tab_avv_pag_tab_doc_input.Where(d => !d.cod_stato.StartsWith(anagrafica_stato_doc.STATO_ANNULLATO)).Count() > 0;
            }
        }

        public bool isImportato
        {
            get
            {
                return (!string.IsNullOrEmpty(fonte_emissione) &&

                        //fonte_emissione.Contains(FONTE_IMPORTATA)) ||
                       (tab_liste != null &&
                        tab_liste.tab_tipo_lista.flag_tipo_lista != tab_tipo_lista.FLAG_TIPO_LISTA_C) ||
                       (tab_liste != null &&
                        tab_liste.anagrafica_strutture_aziendali1 != null &&
                        tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente != null &&
                        tab_liste.anagrafica_strutture_aziendali1.anagrafica_ente.id_ente != anagrafica_ente.ID_ENTE_PUBLISERVIZI) ||
                       (tab_liste == null &&
                        string.IsNullOrEmpty(barcode)));
            }
        }

        public bool isEmesso
        {
            get
            {
                return
                       // (string.IsNullOrEmpty(fonte_emissione) ||
                       //(!string.IsNullOrEmpty(fonte_emissione) &&
                       // !fonte_emissione.Contains(FONTE_IMPORTATA))) &&
                       ((tab_liste != null &&
                         tab_liste.tab_tipo_lista.flag_tipo_lista == tab_tipo_lista.FLAG_TIPO_LISTA_C) ||
                        (tab_liste == null &&
                        !string.IsNullOrEmpty(barcode)));
            }
        }

        public bool isAssoggettabileAdOnere
        {
            get
            {
                if (!tab_unita_contribuzione.Any(u => u.id_tipo_voce_contribuzione == tab_tipo_voce_contribuzione.ONERI_RISCOSSIONE)
                           || anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_ESECUTIVO
                           || anagrafica_tipo_avv_pag.id_servizio == anagrafica_tipo_servizi.ACCERT_OMESSO_PAGAM_ESECUTIVO)
                {
                    return true;
                }
                else
                {
                    return false;
                }



            }
        }


        public List<tab_rata_avv_pag> rate_utilizzabili
        {
            get
            {
                List<tab_rata_avv_pag> rateLst = new List<tab_rata_avv_pag>();
                tab_rata_avv_pag rataUnica = tab_rata_avv_pag.Where(r => r.isRataUnica).FirstOrDefault();
                List<tab_rata_avv_pag> rateizzate = tab_rata_avv_pag.Where(r => !r.isRataUnica).ToList();

                if (rataUnica != null /*&& rateizzate.Sum(r => r.imp_pagato) == 0*/)
                {
                    rateLst.Add(rataUnica);
                }

                if (rataUnica == null /*|| rataUnica.imp_pagato == 0*/)
                {
                    rateLst.AddRange(rateizzate);
                }

                return rateLst;
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
                string vcrlf = "\r\n";
                tab_contribuente tbContrAvvPag = this.tab_contribuente;

                if (tbContrAvvPag.cognome != null && tbContrAvvPag.nome != null)
                {
                    getContribuente = "Codice: " + tab_contribuente.id_anag_contribuente + " - " + txSogDeb.ToTitleCase(tbContrAvvPag.cognome.ToLower().Trim() + " " + tbContrAvvPag.nome.ToLower().Trim() + " - " + tbContrAvvPag.codFiscalePivaDisplay.Trim());
                }
                else if (tbContrAvvPag.rag_sociale != null)
                {
                    getContribuente = "Codice: " + tab_contribuente.id_anag_contribuente + " - " + tbContrAvvPag.rag_sociale.Trim() + " - " + tbContrAvvPag.codFiscalePivaDisplay.Trim();
                }
                else
                {
                    getContribuente = "";
                }

                if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    sSogg = v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay_Stampa.Trim();
                    //pos = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.LastIndexOf("-");
                    //sRefDis = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Substring(0, pos - 1);
                    //sRefCF = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Substring(pos + 2, 16);
                    //sRefDis = sRefDis + " - " + sRefCF;
                    sRefDis = v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay.Trim();
                    sRefDis = txSogDeb.ToTitleCase(sRefDis);
                    return sSogg + " terzo debitore di " + sRefDis + vcrlf +
                        ", coobbligato del contribuente: " + getContribuente + " in qualità di " + v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione;
                }
                else if (join_avv_pag_soggetto_debitore.Any(d => d.join_referente_contribuente != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.join_referente_contribuente != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();
                    sSogg = txSogDeb.ToTitleCase(v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay.Trim());
                    //pos = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.LastIndexOf("-");
                    //sRefDis = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Substring(0, pos - 1).ToLower();
                    //sRefCF = v_joinAvvPaggSoggDeb.tab_referente.referenteDisplay.Substring(pos + 2, 16);
                    sRefDis = v_joinAvvPaggSoggDeb.join_referente_contribuente.tab_referente.referenteDisplay.Trim();
                    sRefDis = txSogDeb.ToTitleCase(sRefDis);
                    //sRefDis = sRefDis + " - " + sRefCF;

                    return sRefDis + vcrlf +
                        v_joinAvvPaggSoggDeb.join_referente_contribuente.anagrafica_tipo_relazione.desc_tipo_relazione +
                        " del contribuente: " + getContribuente;
                }
                else if (join_avv_pag_soggetto_debitore.Any(d => d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)))
                {
                    join_avv_pag_soggetto_debitore v_joinAvvPaggSoggDeb = join_avv_pag_soggetto_debitore.Where(d => d.id_terzo_debitore != null && d.cod_stato.Equals(Data.join_avv_pag_soggetto_debitore.ATT_ATT)).FirstOrDefault();

                    return v_joinAvvPaggSoggDeb.tab_terzo.terzoNominativoDisplay_Stampa.Trim() + vcrlf +
                        " terzo debitore del contribuente" + getContribuente.Trim();
                }
                else
                {

                    return getContribuente.Trim();
                }
            }
        }

        public bool pari
        {
            get
            {
                return (this.id_tab_avv_pag % 2) == 0;
            }
        }

        public string DescrizioneAvvisoCollegato
        {
            get
            {
                return this.anagrafica_tipo_avv_pag.descr_tipo_avv_pag.Trim() + " n." + this.identificativo_avv_pag.Trim() + " emessa il " + this.dt_emissione_String + " notificata il " + this.data_ricezione_String;
            }
        }

        //Sandro
        public string DescrizioneAvvisoCollegato1Riga
        {
            get
            {
                return this.anagrafica_tipo_avv_pag.descr_tipo_avv_pag.Trim() + " n." + this.identificativo_avv_pag.Trim();
            }
        }
        //Sandro
        private string m_isFirenzeDataNotifica;
        public string isFirenzeDataNotifica
        {
            get
            {
                return m_isFirenzeDataNotifica;
            }
            set
            {
                m_isFirenzeDataNotifica = value;
            }
        }

        //Sandro
        public string DescrizioneAvvisoCollegato2Riga
        {
            get
            {
                string s_riga = string.Empty;
                if (string.IsNullOrEmpty(m_isFirenzeDataNotifica))
                {
                    if (!string.IsNullOrEmpty(dt_emissione_String))
                    {
                        s_riga = "data emissione: " + this.dt_emissione_String + " data notifica: " + this.data_ricezione_String;
                    }
                    else
                    {
                        s_riga = "data notifica: " + this.data_ricezione_String;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(dt_emissione_String))
                    {
                        s_riga = "data emissione: " + this.dt_emissione_String + " data notifica: " + isFirenzeDataNotifica;
                    }
                    else
                    {
                        s_riga = "data notifica: " + isFirenzeDataNotifica;
                    }
                }
                int gg_sosp = gg_sospensione_generati + gg_sospensione_trasmessi;
                if (this.gg_sospensione_trasmessi > 0 || this.gg_sospensione_generati > 0)
                {
                    s_riga = s_riga + ", con sospensione termini per istanze/ricorsi pari a: " + gg_sosp.ToString() + " giorni";
                }
                return s_riga;
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
