using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{

    public partial class tab_unita_contribuzione_fatt_emissione : Itab_unita_contribuzione, ISoftDeleted, IGestioneStato
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

        public const string FLAG_TIPO_ADDEBITO_ACCONTO = "0";
        public const string FLAG_TIPO_ADDEBITO_NORMALE = "1";
        public const string FLAG_TIPO_ADDEBITO_CONGUAGLIO = "3";

        public const string FLAG_SEGNO_NEGATIVO = "0";
        public const string FLAG_SEGNO_POSITVO = "1";

        public const string FLAG_UNITA_COLLEGATA_NO = "0";
        public const string FLAG_UNITA_COLLEGATA_SI = "1";

        public string periodo_contribuzione_da_String
        {
            get
            {
                if (periodo_contribuzione_da.HasValue)
                {
                    return periodo_contribuzione_da.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string _periodoContribuzione = null;
        public string periodo_contribuzione_String
        {
            get
            {
                if (_periodoContribuzione == null)
                {
                    string v_temp = string.Empty;

                    if (periodo_contribuzione_da_String != string.Empty)
                    {
                        v_temp = periodo_contribuzione_da_String + " - " + (periodo_contribuzione_a_String == string.Empty ? "Ad oggi" : periodo_contribuzione_a_String);
                    }

                    _periodoContribuzione = v_temp;
                    return _periodoContribuzione;
                }
                else
                {
                    return _periodoContribuzione;
                }
            }
            set
            {
                _periodoContribuzione = value;
            }
        }

        public string periodo_contribuzione_a_String
        {
            get
            {
                if (periodo_contribuzione_a.HasValue)
                {
                    return periodo_contribuzione_a.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string periodo_rif_da_String
        {
            get
            {
                if (periodo_rif_da.HasValue)
                {
                    return periodo_rif_da.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string periodo_rif_a_String
        {
            get
            {
                if (periodo_rif_a.HasValue)
                {
                    return periodo_rif_a.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string _periodoRiferimento = null;
        public string periodo_rif_String
        {
            get
            {
                if (_periodoRiferimento == null)
                {
                    string v_temp = string.Empty;

                    if (periodo_rif_da_String != string.Empty)
                    {
                        v_temp = periodo_rif_da_String + " - " + (periodo_rif_a_String == string.Empty ? "Ad oggi" : periodo_rif_a_String);
                    }

                    _periodoRiferimento = v_temp;
                    return _periodoRiferimento;
                }
                else
                {
                    return _periodoRiferimento;
                }
            }
            set
            {
                _periodoRiferimento = value;
            }
        }

        public string QuantitaUnitaContribuzione
        {
            get
            {
                if (!string.IsNullOrEmpty(um_unita) &&
                    quantita_unita_contribuzione > 0)
                {
                    return um_unita + " " + string.Format("{0:0.0000}", quantita_unita_contribuzione);
                }
                else if (string.IsNullOrEmpty(um_unita) &&
                         quantita_unita_contribuzione > 0)
                {
                    return string.Format("{0:0}", quantita_unita_contribuzione);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string importo_unita_contribuzione_Euro
        {
            get
            {
                if (flag_segno == "0")
                {
                    return "- " + importo_unita_contribuzione.ToString("C4");
                }
                else
                {
                    return importo_unita_contribuzione.ToString("C4");
                }
            }
        }

        public string importo_unitario_contribuzione_Euro
        {
            get
            {
                return importo_unitario_contribuzione.ToString("C4");
            }
        }

        public string importo_sgravio_Euro
        {
            get
            {
                if (importo_sgravio.HasValue)
                {
                    return importo_sgravio.Value.ToString("C4");
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public decimal importo_sgravato
        {
            get
            {
                return importo_unita_contribuzione - (importo_sgravio.HasValue ? importo_sgravio.Value : 0);
            }
        }

        public string importo_sgravato_Euro
        {
            get
            {
                return importo_sgravato.ToString("C4");
            }
        }

        public string imponibile_unita_contribuzione_Euro
        {
            get
            {
                return imponibile_unita_contribuzione.ToString("C4");
            }
        }

        public string iva
        {
            get
            {
                if (iva_unita_contribuzione.HasValue)
                {
                    if (aliquota_iva.HasValue)
                    {
                        return iva_unita_contribuzione.Value.ToString("C4") + " (" + Math.Truncate(aliquota_iva.Value) + "%) ";
                    }
                    else
                    {
                        return iva_unita_contribuzione.Value.ToString("C4");
                    }
                }
                else
                {
                    return 0.ToString("C");
                }
            }
        }

        public int id_tab_macroentrate
        {
            get
            {
                if (anagrafica_entrate != null && anagrafica_entrate.join_entrate_macroentrate != null && anagrafica_entrate.join_entrate_macroentrate.Count > 0)
                {
                    return anagrafica_entrate.join_entrate_macroentrate.FirstOrDefault().id_tab_macroentrate;
                }
                else
                {
                    return -1;
                }
            }
        }

        public string SommatoriaAgevolazioni
        {
            get
            {
                decimal age1 = imp_agevolazione1.HasValue ? imp_agevolazione1.Value : 0;
                decimal age2 = imp_agevolazione2.HasValue ? imp_agevolazione2.Value : 0;
                decimal age3 = imp_agevolazione3.HasValue ? imp_agevolazione3.Value : 0;
                decimal age4 = imp_agevolazione4.HasValue ? imp_agevolazione4.Value : 0;
                decimal age = age1 + age2 + age3 + age4;
                return age.ToString();
            }
        }

        public string SommatoriaAgevolazioni_Euro
        {
            get
            {
                decimal age1 = imp_agevolazione1.HasValue ? imp_agevolazione1.Value : 0;
                decimal age2 = imp_agevolazione2.HasValue ? imp_agevolazione2.Value : 0;
                decimal age3 = imp_agevolazione3.HasValue ? imp_agevolazione3.Value : 0;
                decimal age4 = imp_agevolazione4.HasValue ? imp_agevolazione4.Value : 0;
                decimal age = age1 + age2 + age3 + age4;
                return age.ToString("C");
            }
        }

        private string m_barcodeAvvisoCollegato = string.Empty;
        public string barcodeAvvisoCollegato
        {
            get
            {
                return m_barcodeAvvisoCollegato;
            }
            set
            {
                m_barcodeAvvisoCollegato = value;
            }
        }

        private string _descrizioneVoceContribuzione = null;
        public string DescrizioneVoceContribuzione
        {
            get
            {
                if (id_avv_pag_collegato == null)
                {
                    if (id_unita_contribuzione_collegato != null && tab_unita_contribuzione_fatt_emissione2 != null && tab_unita_contribuzione_fatt_emissione2.tab_avv_pag != null)
                    {
                        string bCode = barcodeAvvisoCollegato == "" ? tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag.Trim() : barcodeAvvisoCollegato;
                        string v_desc = DescrizioneTipoVoceContribuzione;
                        if (importo_tributo.HasValue && importo_tributo.Value > tab_unita_contribuzione_fatt_emissione2.importo_unita_contribuzione)
                        {
                            string testo = "";

                            if (string.IsNullOrEmpty(descr_tipo_avvpag_origine))
                            {
                                if (tab_avv_pag1 != null)
                                {
                                    tab_avv_pag v_avvIniziale = tab_avv_pag1;

                                    if (v_avvIniziale != null && v_avvIniziale.tab_unita_contribuzione.Any(u => u.id_anagrafica_voce_contribuzione == 93 || u.id_anagrafica_voce_contribuzione == 199))
                                    {
                                        string v_anno = v_avvIniziale.tab_unita_contribuzione.Where(u => u.id_anagrafica_voce_contribuzione == 93 || u.id_anagrafica_voce_contribuzione == 199).First().anno_rif;
                                        testo = " per mancato/parziale versamento acconto " + v_anno;
                                    }
                                    else if (v_avvIniziale != null && v_avvIniziale.tab_unita_contribuzione.Any(u => u.id_anagrafica_voce_contribuzione == 99 || u.id_anagrafica_voce_contribuzione == 200))
                                    {
                                        string v_anno = v_avvIniziale.tab_unita_contribuzione.Where(u => u.id_anagrafica_voce_contribuzione == 99 || u.id_anagrafica_voce_contribuzione == 200).First().anno_rif;
                                        testo = " per mancato/parziale versamento saldo " + v_anno;
                                    }
                                }
                            }

                            v_desc = DescrizioneTipoVoceContribuzione + " da pagare su avviso " + tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag.Trim() + testo;
                        }
                        else
                        {
                            string testo = "";

                            if (string.IsNullOrEmpty(descr_tipo_avvpag_origine))
                            {
                                if (tab_avv_pag1 != null)
                                {
                                    tab_avv_pag v_avvIniziale = tab_avv_pag1;

                                    if (v_avvIniziale != null && v_avvIniziale.tab_unita_contribuzione.Any(u => u.id_anagrafica_voce_contribuzione == 93 || u.id_anagrafica_voce_contribuzione == 199))
                                    {
                                        string v_anno = v_avvIniziale.tab_unita_contribuzione.Where(u => u.id_anagrafica_voce_contribuzione == 93 || u.id_anagrafica_voce_contribuzione == 199).First().anno_rif;
                                        testo = " per mancato/parziale versamento acconto " + v_anno;
                                    }
                                    else if (v_avvIniziale != null && v_avvIniziale.tab_unita_contribuzione.Any(u => u.id_anagrafica_voce_contribuzione == 99 || u.id_anagrafica_voce_contribuzione == 200))
                                    {
                                        string v_anno = v_avvIniziale.tab_unita_contribuzione.Where(u => u.id_anagrafica_voce_contribuzione == 99 || u.id_anagrafica_voce_contribuzione == 200).First().anno_rif;
                                        testo = " per mancato/parziale versamento saldo " + v_anno;
                                    }
                                }
                            }

                            v_desc = DescrizioneTipoVoceContribuzione + " pari a " + (importo_tributo.HasValue ? importo_tributo.Value.ToString("C") : 0.ToString("C")) + " da pagare su avviso " + bCode + testo;
                        }

                        if (!string.IsNullOrEmpty(descr_tipo_avvpag_origine))
                        {
                            v_desc = v_desc + " derivante da atto propedeutico " + descr_tipo_avvpag_origine;

                            if (identificativo_avvpag_origine != null && identificativo_avvpag_origine != "")
                            {
                                v_desc = v_desc + " n. " + identificativo_avvpag_origine;
                            }

                            if (data_notifica_avvpag_origine.HasValue)
                            {
                                v_desc = v_desc + " notificato il " + data_notifica_avvpag_origine.Value.ToShortDateString();
                            }

                            return v_desc;
                        }
                        else
                        {
                            return v_desc;
                        }
                    }
                    else if (tab_unita_contribuzione_fatt_emissione2 != null && tab_unita_contribuzione_fatt_emissione2.tab_avv_pag != null)
                    {
                        if (id_avv_pag_origine != null && !string.IsNullOrEmpty(identificativo_avvpag_origine))
                        {
                            string v_risp = "";
                            if (importo_tributo.HasValue && importo_tributo.Value > tab_unita_contribuzione_fatt_emissione2.importo_unita_contribuzione)
                            {
                                v_risp = DescrizioneTipoVoceContribuzione + " da pagare su avviso " + (!string.IsNullOrEmpty(tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag) ? tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag.Trim() : string.Empty);
                            }
                            else
                            {
                                string testo = "";

                                if (string.IsNullOrEmpty(descr_tipo_avvpag_origine))
                                {
                                    tab_avv_pag v_avvIniziale = tab_avv_pag1;
                                    if (v_avvIniziale != null && v_avvIniziale.tab_unita_contribuzione.Any(u => u.id_anagrafica_voce_contribuzione == 93 || u.id_anagrafica_voce_contribuzione == 199))
                                    {
                                        string v_anno = v_avvIniziale.tab_unita_contribuzione.Where(u => u.id_anagrafica_voce_contribuzione == 93 || u.id_anagrafica_voce_contribuzione == 199).First().anno_rif;
                                        testo = " per mancato/parziale versamento acconto " + v_anno;
                                    }
                                    else if (v_avvIniziale != null && v_avvIniziale.tab_unita_contribuzione.Any(u => u.id_anagrafica_voce_contribuzione == 99 || u.id_anagrafica_voce_contribuzione == 200))
                                    {
                                        string v_anno = v_avvIniziale.tab_unita_contribuzione.Where(u => u.id_anagrafica_voce_contribuzione == 99 || u.id_anagrafica_voce_contribuzione == 200).First().anno_rif;
                                        testo = " per mancato/parziale versamento saldo " + v_anno;
                                    }
                                }

                                v_risp = DescrizioneTipoVoceContribuzione + " pari a " + (importo_tributo.HasValue ? importo_tributo.Value.ToString("C") : 0.ToString("C")) + " da pagare su avviso " + (!string.IsNullOrEmpty(tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag) ? tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag.Trim() : string.Empty) + testo;
                            }

                            if (id_avv_pag_origine != tab_unita_contribuzione_fatt_emissione2.id_avv_pag_collegato)
                            {
                                v_risp = v_risp + " derivante da " + descr_tipo_avvpag_origine + " n. " + tab_avv_pag1.identificativo_avv_pag;

                                if (data_notifica_avvpag_origine.HasValue)
                                {
                                    v_risp = v_risp + " notificato il " + data_notifica_avvpag_origine.Value.ToShortDateString();
                                }
                            }

                            return v_risp;
                        }
                        else if (id_avv_pag_origine == null && !string.IsNullOrEmpty(identificativo_avvpag_origine))
                        {
                            string v_risp = DescrizioneTipoVoceContribuzione + " pari a " + (importo_tributo.HasValue ? importo_tributo.Value.ToString("C") : 0.ToString("C")) + " da pagare su avviso " + (!string.IsNullOrEmpty(tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag) ? tab_unita_contribuzione_fatt_emissione2.tab_avv_pag.identificativo_avv_pag.Trim() : string.Empty);
                            v_risp = v_risp + " derivante da " + descr_tipo_avvpag_origine + " n. " + identificativo_avvpag_origine;

                            if (data_notifica_avvpag_origine.HasValue)
                            {
                                v_risp = v_risp + " notificato il " + data_notifica_avvpag_origine.Value.ToShortDateString();
                            }

                            return v_risp;
                        }
                        else
                        {
                            return DescrizioneTipoVoceContribuzione;
                        }
                    }
                    else
                    {
                        return DescrizioneTipoVoceContribuzione;
                    }
                }
                else
                {
                    string v_desc = tab_avv_pag.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " N. " + (string.IsNullOrEmpty(tab_avv_pag.identificativo_avv_pag) ? string.Empty : tab_avv_pag.identificativo_avv_pag.Trim());

                    //Il dottore ha voluto che per Firenze per gli avvisi importati si indicassero anche i barcode delle spednot nell'identificativo dell'avviso
                    if (id_ente == anagrafica_ente.ID_ENTE_COMUNE_DI_FIRENZE /*&& tab_avv_pag.tab_liste.anagrafica_strutture_aziendali2.id_ente_appartenenza == tab_avv_pag.id_ente*/)
                    {
                        foreach (tab_sped_not v_tabSpedNot in tab_avv_pag.tab_sped_not)
                        {
                            v_desc = v_desc + " - " + v_tabSpedNot.barcode;
                        }
                    }

                    if (tab_avv_pag.data_ricezione.HasValue)
                    {
                        v_desc = v_desc + " Data Notifica: " + tab_avv_pag.data_ricezione_String;
                    }

                    if (testo1 != null && testo1.Length > 0)
                    {
                        v_desc = v_desc + ". " + testo1;
                    }

                    if (tab_avv_pag.importo_sanzioni_eliminate_eredi != null && tab_avv_pag.importo_sanzioni_eliminate_eredi > 0)
                    {
                        v_desc = v_desc + ". Importo da pagare decurtato delle sanzioni pari ad " + tab_avv_pag.importo_sanzioni_eliminate_eredi.Value.ToString("C") + " non trasmissibili agli eredi";
                    }
                    return v_desc;
                }
            }
            set
            {
                _descrizioneVoceContribuzione = value;
            }
        }

        public string DescrizioneTipoVoceContribuzione
        {
            get
            {
                string v_descrizione = string.Empty;

                if (flag_tipo_addebito == FLAG_TIPO_ADDEBITO_ACCONTO)
                {
                    v_descrizione = "Acconto";
                }
                else if (flag_tipo_addebito == FLAG_TIPO_ADDEBITO_CONGUAGLIO &&
                        (id_entrata == anagrafica_entrate.ACQUEDOTTO ||
                         id_entrata == anagrafica_entrate.ACQUEDOTTO1))
                {
                    v_descrizione = "Conguaglio";
                }

                if (anagrafica_voci_contribuzione != null)
                {
                    if (id_avv_pag_origine.HasValue &&
                       !id_unita_contribuzione_collegato.HasValue)
                    {
                        v_descrizione = v_descrizione + " " + anagrafica_voci_contribuzione.descr_anagrafica_voce_contribuzione + " su " + tab_avv_pag1.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + tab_avv_pag1.identificativo_avv_pag;
                    }
                    else
                    {
                        v_descrizione = v_descrizione + " " + anagrafica_voci_contribuzione.descr_anagrafica_voce_contribuzione;
                    }
                }
                else
                {
                    if (id_avv_pag_origine.HasValue &&
                       !id_unita_contribuzione_collegato.HasValue)
                    {
                        v_descrizione = v_descrizione + " " + tab_tipo_voce_contribuzione1.descrizione_tipo_voce_contribuzione + " su " + tab_avv_pag1.anagrafica_tipo_avv_pag.descr_tipo_avv_pag + " n. " + tab_avv_pag1.identificativo_avv_pag;
                    }
                    else
                    {
                        v_descrizione = v_descrizione + " " + tab_tipo_voce_contribuzione1.descrizione_tipo_voce_contribuzione;
                    }
                }

                return v_descrizione;
            }
        }
    }
}
