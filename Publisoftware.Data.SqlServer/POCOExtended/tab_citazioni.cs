using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_citazioni.Metadata))]
    public partial class tab_citazioni : ISoftDeleted, IGestioneStato
    {
        public const string ATT_ATT = "ATT-ATT";
        public const string ANN = "ANN-";

        public const string ORDINANZA_ASSEGNAZIONE = "ASS";
        public const string ORDINANZA_ESTINZIONE = "EST";
        public const string ORDINANZA_MANCATA_DICHIARAZIONE = "MAD";
        public const string ORDINANZA_RINVIO_UDIENZA = "RIU";
        public const string ORDINANZA_RINOTIFICA = "RIN";
        public const string ORDINANZA_FISSAZIONE_PRIMA_UDIENZA = "FPU";

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

        public string StatoOrdinanza(string p_tipoOrdinanza)
        {
            switch (p_tipoOrdinanza)
            {
                case ORDINANZA_ASSEGNAZIONE:
                    return (!string.IsNullOrEmpty(flag_assegnazione_somme) && flag_assegnazione_somme == "1") ? "Da notificare" :
                           ((!string.IsNullOrEmpty(flag_assegnazione_somme) && flag_assegnazione_somme == "2") ? "Notificata" : "Non notificata");
                case ORDINANZA_ESTINZIONE:
                    return (!string.IsNullOrEmpty(flag_estinzione_citazione) && flag_estinzione_citazione == "1") ? "Da notificare" :
                           ((!string.IsNullOrEmpty(flag_estinzione_citazione) && flag_estinzione_citazione == "2") ? "Notificata" : "Non notificata");
                case ORDINANZA_MANCATA_DICHIARAZIONE:
                    return (!string.IsNullOrEmpty(flag_ordinanza_mancata_dichiarazione) && flag_ordinanza_mancata_dichiarazione == "1") ? "Da notificare" :
                           ((!string.IsNullOrEmpty(flag_ordinanza_mancata_dichiarazione) && flag_ordinanza_mancata_dichiarazione == "2") ? "Notificata" : "Non notificata");
                case ORDINANZA_RINVIO_UDIENZA:
                    return (!string.IsNullOrEmpty(flag_ordinanza_rinvio_udienza) && flag_ordinanza_rinvio_udienza == "1") ? "Da notificare" :
                           ((!string.IsNullOrEmpty(flag_ordinanza_rinvio_udienza) && flag_ordinanza_rinvio_udienza == "2") ? "Notificata" : "Non notificata");
                case ORDINANZA_RINOTIFICA:
                    return (!string.IsNullOrEmpty(flag_ordinanza_rinotifica_citazione) && flag_ordinanza_rinotifica_citazione == "1") ? "Da notificare" :
                           ((!string.IsNullOrEmpty(flag_ordinanza_rinotifica_citazione) && flag_ordinanza_rinotifica_citazione == "2") ? "Notificata" : "Non notificata");
                case ORDINANZA_FISSAZIONE_PRIMA_UDIENZA:
                    return (!string.IsNullOrEmpty(flag_ordinanza_fissazione_prima_udienza) && flag_ordinanza_fissazione_prima_udienza == "1") ? "Da notificare" :
                           ((!string.IsNullOrEmpty(flag_ordinanza_fissazione_prima_udienza) && flag_ordinanza_fissazione_prima_udienza == "2") ? "Notificata" : "Non notificata");
                default:
                    return string.Empty;
            }
        }

        public bool IsRelataControfirmata
        {
            get
            {
                return id_avv_pag_citazione.HasValue &&
                       tab_avv_pag.tab_fascicolo_avvpag_allegati
                                  .Where(x => x.tab_sped_not != null &&
                                              x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                  .FirstOrDefault() != null &&
                       tab_avv_pag.tab_fascicolo_avvpag_allegati
                                  .Where(x => x.tab_sped_not != null &&
                                              x.tab_sped_not.flag_soggetto_debitore == tab_sped_not.FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE)
                                  .FirstOrDefault()
                                  .tab_fascicolo
                                  .join_documenti_fascicolo
                                  .Where(y => y.tab_documenti.anagrafica_documenti.sigla_doc == anagrafica_documenti.SIGLA_ATTO_PIGNORAMENTO).Count() > 0;
            }
        }

        public string Contribuente
        {
            get
            {
                if (id_contribuente.HasValue)
                {
                    return tab_contribuente.contribuenteDisplay;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string TerzoDebitore
        {
            get
            {
                if (id_terzo.HasValue)
                {
                    return tab_terzo.terzoDisplay;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string SoggettoDebitore
        {
            get
            {
                if (id_referente.HasValue)
                {
                    return tab_referente.referenteDisplay + " ref. del contr. " + tab_contribuente.contribuenteDisplay;
                }
                else
                {
                    if (id_contribuente.HasValue)
                    {
                        return tab_contribuente.contribuenteDisplay;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        public string importoPignorato_Euro
        {
            get
            {
                return importoPignorato_decimal.ToString("C");
            }
        }

        public decimal importoPignorato_decimal
        {
            get
            {
                if (id_avv_pag_citazione.HasValue &&
                    tab_avv_pag.imp_tot_avvpag_rid.HasValue)
                {
                    return tab_avv_pag.imp_tot_avvpag_rid.Value + decimal.Divide(decimal.Multiply(50, tab_avv_pag.imp_tot_avvpag_rid.Value), 100);
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_titoli_Euro
        {
            get
            {
                return importo_assegnato_titoli_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_titoli_decimal
        {
            get
            {
                if (importo_assegnato_titoli.HasValue)
                {
                    return importo_assegnato_titoli.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_mensile_Euro
        {
            get
            {
                return importo_assegnato_mensile_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_mensile_decimal
        {
            get
            {
                if (importo_assegnato_mensile.HasValue)
                {
                    return importo_assegnato_mensile.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_cc_depositi_Euro
        {
            get
            {
                return importo_assegnato_cc_depositi_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_cc_depositi_decimal
        {
            get
            {
                if (importo_assegnato_cc_depositi.HasValue)
                {
                    return importo_assegnato_cc_depositi.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_tfr_Euro
        {
            get
            {
                return importo_assegnato_tfr_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_tfr_decimal
        {
            get
            {
                if (importo_assegnato_tfr.HasValue)
                {
                    return importo_assegnato_tfr.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_altri_redditi_una_tantum_Euro
        {
            get
            {
                return importo_assegnato_altri_redditi_una_tantum_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_altri_redditi_una_tantum_decimal
        {
            get
            {
                if (importo_assegnato_altri_redditi_una_tantum.HasValue)
                {
                    return importo_assegnato_altri_redditi_una_tantum.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_totale_assegnato_Euro
        {
            get
            {
                return importo_totale_assegnato_decimal.ToString("C");
            }
        }

        public decimal importo_totale_assegnato_decimal
        {
            get
            {
                if (importo_totale_assegnato.HasValue)
                {
                    return importo_totale_assegnato.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_recupero_spese_esecutive_Euro
        {
            get
            {
                return importo_assegnato_recupero_spese_esecutive_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_recupero_spese_esecutive_decimal
        {
            get
            {
                if (importo_assegnato_recupero_spese_esecutive.HasValue)
                {
                    return importo_assegnato_recupero_spese_esecutive.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string importo_assegnato_recupero_spese_legali_Euro
        {
            get
            {
                return importo_assegnato_recupero_spese_legali_decimal.ToString("C");
            }
        }

        public decimal importo_assegnato_recupero_spese_legali_decimal
        {
            get
            {
                if (importo_assegnato_recupero_spese_legali.HasValue)
                {
                    return importo_assegnato_recupero_spese_legali.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string IdentificativoAvvisoCitazione
        {
            get
            {
                if (id_avv_pag_citazione.HasValue)
                {
                    return tab_avv_pag.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string IdentificativoAvvisoTerzo
        {
            get
            {
                if (id_avv_pag_pignoramento_ordine_terzo.HasValue)
                {
                    return tab_avv_pag1.identificativo_avv_pag;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string AvvocatoAssegnatario
        {
            get
            {
                if (id_risorsa_procuratore_1.HasValue)
                {
                    return anagrafica_risorse.CognomeNome;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string UfficialeRiscossione
        {
            get
            {
                if (id_ufficiale_riscossione.HasValue)
                {
                    return anagrafica_risorse3.CognomeNome;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string StatoIscrizioneRuolo
        {
            get
            {
                if (flag_iscrizione_ruolo != null &&
                    flag_iscrizione_ruolo == "1")
                {
                    return "Iscritto";
                }
                else
                {
                    return "Non rilevata";
                }
            }
        }

        public string StatoGiudizio
        {
            get
            {
                if ((flag_richiesta_estinzione_procedura_esecutiva == null ||
                     flag_richiesta_estinzione_procedura_esecutiva == "0") &&
                    (flag_assegnazione_somme == null ||
                     flag_assegnazione_somme == "0"))
                {
                    return "In attesa di giudizio";
                }
                else if (flag_richiesta_estinzione_procedura_esecutiva != null &&
                         flag_richiesta_estinzione_procedura_esecutiva == "1" &&
                        (flag_estinzione_citazione == null ||
                         flag_estinzione_citazione == "0"))
                {
                    return "Con richiesta di estinzione del pignoramento";
                }
                else if (flag_richiesta_estinzione_procedura_esecutiva != null &&
                         flag_richiesta_estinzione_procedura_esecutiva == "1" &&
                         flag_estinzione_citazione != null &&
                         flag_estinzione_citazione == "1")
                {
                    return "Estinzione del pignoramento da notificare";
                }
                else if (flag_richiesta_estinzione_procedura_esecutiva != null &&
                         flag_richiesta_estinzione_procedura_esecutiva == "1" &&
                         flag_estinzione_citazione != null &&
                         flag_estinzione_citazione == "2")
                {
                    return "Estinzione del pignoramento notificata";
                }
                else if (flag_assegnazione_somme != null &&
                         flag_assegnazione_somme == "1")
                {
                    return "Con assegnazione da notificare";
                }
                else if (flag_assegnazione_somme != null &&
                         flag_assegnazione_somme == "2")
                {
                    return "Con assegnazione notificata";
                }
                else
                {
                    return string.Empty;
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

        public string data_udienza_atto_citazione_String
        {
            get
            {
                if (data_udienza_atto_citazione.HasValue)
                {
                    return data_udienza_atto_citazione.Value.ToShortDateString();
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
                    data_udienza_atto_citazione = DateTime.Parse(value);
                }
                else
                {
                    data_udienza_atto_citazione = null;
                }
            }
        }

        public string data__restituzione_ufficiale_giudiziario_String
        {
            get
            {
                if (data__restituzione_ufficiale_giudiziario.HasValue)
                {
                    return data__restituzione_ufficiale_giudiziario.Value.ToShortDateString();
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
                    data__restituzione_ufficiale_giudiziario = DateTime.Parse(value);
                }
                else
                {
                    data__restituzione_ufficiale_giudiziario = null;
                }
            }
        }

        public string data_assegnazione_ufficiale_riscossione_String
        {
            get
            {
                if (data_assegnazione_ufficiale_riscossione.HasValue)
                {
                    return data_assegnazione_ufficiale_riscossione.Value.ToShortDateString();
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
                    data_assegnazione_ufficiale_riscossione = DateTime.Parse(value);
                }
                else
                {
                    data_assegnazione_ufficiale_riscossione = null;
                }
            }
        }

        public string data_ordinanza_assegnazione_String
        {
            get
            {
                if (data_ordinanza_assegnazione.HasValue)
                {
                    return data_ordinanza_assegnazione.Value.ToShortDateString();
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
                    data_ordinanza_assegnazione = DateTime.Parse(value);
                }
                else
                {
                    data_ordinanza_assegnazione = null;
                }
            }
        }

        public string data_caricamento_ruolo_String
        {
            get
            {
                if (data_caricamento_ruolo.HasValue)
                {
                    return data_caricamento_ruolo.Value.ToShortDateString();
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
                    data_caricamento_ruolo = DateTime.Parse(value);
                }
                else
                {
                    data_caricamento_ruolo = null;
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

        public string data_decorrenza_assegnato_mensile_String
        {
            get
            {
                if (data_decorrenza_assegnato_mensile.HasValue)
                {
                    return data_decorrenza_assegnato_mensile.Value.ToShortDateString();
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
                    data_decorrenza_assegnato_mensile = DateTime.Parse(value);
                }
                else
                {
                    data_decorrenza_assegnato_mensile = null;
                }
            }
        }

        public string data_scadenza_assegnato_mensile_String
        {
            get
            {
                if (data_scadenza_assegnato_mensile.HasValue)
                {
                    return data_scadenza_assegnato_mensile.Value.ToShortDateString();
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
                    data_scadenza_assegnato_mensile = DateTime.Parse(value);
                }
                else
                {
                    data_scadenza_assegnato_mensile = null;
                }
            }
        }

        public string data_decorrenza_assegnato_cc_depositi_String
        {
            get
            {
                if (data_decorrenza_assegnato_cc_depositi.HasValue)
                {
                    return data_decorrenza_assegnato_cc_depositi.Value.ToShortDateString();
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
                    data_decorrenza_assegnato_cc_depositi = DateTime.Parse(value);
                }
                else
                {
                    data_decorrenza_assegnato_cc_depositi = null;
                }
            }
        }

        public string data_decorrenza_assegnato_titoli_String
        {
            get
            {
                if (data_decorrenza_assegnato_titoli.HasValue)
                {
                    return data_decorrenza_assegnato_titoli.Value.ToShortDateString();
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
                    data_decorrenza_assegnato_titoli = DateTime.Parse(value);
                }
                else
                {
                    data_decorrenza_assegnato_titoli = null;
                }
            }
        }

        public string data_decorrenza_assegnato_tfr_String
        {
            get
            {
                if (data_decorrenza_assegnato_tfr.HasValue)
                {
                    return data_decorrenza_assegnato_tfr.Value.ToShortDateString();
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
                    data_decorrenza_assegnato_tfr = DateTime.Parse(value);
                }
                else
                {
                    data_decorrenza_assegnato_tfr = null;
                }
            }
        }

        public string data_decorrenza_assegnato_altri_redditi_una_tantum_String
        {
            get
            {
                if (data_decorrenza_assegnato_altri_redditi_una_tantum.HasValue)
                {
                    return data_decorrenza_assegnato_altri_redditi_una_tantum.Value.ToShortDateString();
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
                    data_decorrenza_assegnato_altri_redditi_una_tantum = DateTime.Parse(value);
                }
                else
                {
                    data_decorrenza_assegnato_altri_redditi_una_tantum = null;
                }
            }
        }

        public string RichiestaEstinzione
        {
            get
            {
                if (!string.IsNullOrEmpty(flag_richiesta_estinzione_procedura_esecutiva) &&
                    flag_richiesta_estinzione_procedura_esecutiva != "0" &&
                   (string.IsNullOrEmpty(flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva) ||
                    flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva == "0"))
                {
                    return "Estinzione da richiedere";
                }
                else if (!string.IsNullOrEmpty(flag_richiesta_estinzione_procedura_esecutiva) &&
                         flag_richiesta_estinzione_procedura_esecutiva != "0" &&
                         flag_caricamento_ruolo_richiesta_estinzione_procedura_esecutiva == "1")
                {
                    return "Richiesta estinzione effettuata";
                }
                else
                {
                    return string.Empty;
                }
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
