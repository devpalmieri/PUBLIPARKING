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
    [MetadataTypeAttribute(typeof(tab_sped_not.Metadata))]
    public partial class tab_sped_not : ISoftDeleted, IGestioneStato
    {
        public const string FLAG_DESTINATARIO_CUR_FALLIMENTARE = "F";
        public const string FLAG_DESTINATARIO_EREDI = "E";
        public const string FLAG_DESTINATARIO_TRZ_DEB = "T";
        public const string FLAG_DESTINATARIO_SOGG_DEB = "D";
        public const string FLAG_DESTINATARIO_CONTRIBUENTE = "C";
        public const string FLAG_DESTINATARIO_REFERENTE = "R";
        public const string FLAG_DESTINATARIO_REFERENTE_DOPPIA_NOTIFICA = "K";
        public const string FLAG_DESTINATARIO_X = "X";
        public const string FLAG_DESTINATARIO_Y = "Y";
        public const string FLAG_DESTINATARIO_M = "M";

        public const string FLAG_SOGGETTO_DEBITORE_CONTRIBUENTE = "C";
        public const string FLAG_SOGGETTO_DEBITORE_REFERENTE = "R";
        public const string FLAG_SOGGETTO_DEBITORE_UFFICIALE_RISCOSSIONE = "U";// Copie solo per stampa ma da non spedire
        public const string FLAG_SOGGETTO_DEBITORE_EREDE = "E";

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

        [DisplayName("Data Spedizione")]
        public string dt_spedizione_notifica_String
        {
            get
            {
                if (dt_spedizione_notifica.HasValue)
                {
                    return dt_spedizione_notifica.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Data Scadenza")]
        public string dt_scadenza_notifica_String
        {
            get
            {
                if (dt_scadenza_notifica.HasValue)
                {
                    return dt_scadenza_notifica.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Data Notifica")]
        public string data_esito_notifica_String
        {
            get
            {
                if (data_esito_notifica.HasValue)
                {
                    return data_esito_notifica.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Data Affissione")]
        public string data_affissione_ap_String
        {
            get
            {
                if (data_affissione_ap.HasValue)
                {
                    return data_affissione_ap.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Data Spedizione Avv. Dep.")]
        public string data_spedizione_avvdep_String
        {
            get
            {
                if (data_spedizione_avvdep.HasValue)
                {
                    return data_spedizione_avvdep.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Data Notifica Avv. Dep.")]
        public string data_notifica_avvdep_String
        {
            get
            {
                if (data_notifica_avvdep.HasValue)
                {
                    return data_notifica_avvdep.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Stato")]
        public string Stato
        {
            get
            {
                return anagrafica_stato_sped_not1 != null ? anagrafica_stato_sped_not1.descr_stato_sped_not : string.Empty;
            }
        }

        [DisplayName("Esito Notifica")]
        public string EsitoNotifica
        {
            get
            {
                //return anagrafica_stato_sped_not1 != null ? anagrafica_stato_sped_not1.descr_stato_sped_not : string.Empty + (anagrafica_tipo_esito_notifica != null ? " - " + anagrafica_tipo_esito_notifica.descr_tipo_esito_notifica : string.Empty);
                return anagrafica_tipo_esito_notifica != null ? anagrafica_tipo_esito_notifica.descr_tipo_esito_notifica : string.Empty;
            }
        }

        [DisplayName("Esito Notifica Avv. Dep.")]
        public string EsitoNotificaAvvDep
        {
            get
            {
                return anagrafica_stato_sped_not2 != null ? anagrafica_stato_sped_not2.descr_stato_sped_not : string.Empty + (anagrafica_tipo_esito_notifica1 != null ? " - " + anagrafica_tipo_esito_notifica1.descr_tipo_esito_notifica : string.Empty);
            }
        }

        public string flagPersonaFisicaGiuridica
        {
            get
            {
                if (tab_notificatore != null)
                {
                    if (tab_notificatore.flag_persona_fisica_giuridica != null)
                    {
                        return tab_notificatore.flag_persona_fisica_giuridica;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Notificatore")]
        public string Notificatore
        {
            get
            {
                if (tab_notificatore != null)
                {
                    if (tab_notificatore.rag_sociale != null)
                    {
                        return tab_notificatore.rag_sociale;
                    }
                    else
                    {
                        return tab_notificatore.cognome + " - " + tab_notificatore.nome;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        [DisplayName("Ricevente")]
        public string Ricevente
        {
            get
            {
                return cognome_ricevente + " " + nome_ricevente;
            }
        }
        public virtual string DestinatarioSpedNot
        {
            get
            {
                switch (flag_soggetto_debitore)
                {
                    //case "C":
                    //    return cognome_ragsoc_contribuente;
                    //case "R":
                    //    return cognome_ragsoc_referente;
                    //case "E":
                    //    return "Eredi di " + cognome_ragsoc_contribuente;
                    //case "F":
                    //    return "Curatore Fallimentare " + cognome_ragsoc_referente;
                    ////case "R":
                    ////    return cognome_ragsoc_referente;
                    //case "T":
                    //    return cognome_ragsoc_terzo;
                    ////case "M":
                    ////    return cognome_ragsoc_terzo;
                    ////case "X":
                    ////    return cognome_ragsoc_terzo;
                    ////case "Y":
                    ////    return cognome_ragsoc_terzo;
                    //default:
                    //    return cognome_ragsoc_contribuente != null ? cognome_ragsoc_contribuente : nominativo_recapito;
                    case "C":
                        return !string.IsNullOrEmpty(cognome_ragsoc_contribuente) ? cognome_ragsoc_contribuente : tab_contribuente.nominativoDisplay;
                    case "E":
                        return "Eredi di " + (!string.IsNullOrEmpty(cognome_ragsoc_contribuente) ? cognome_ragsoc_contribuente : tab_contribuente.nominativoDisplay);
                    case "R":
                        return !string.IsNullOrEmpty(cognome_ragsoc_referente) ? cognome_ragsoc_referente : tab_referente.nominativoDisplay;
                    case "F":
                        return !string.IsNullOrEmpty(cognome_ragsoc_referente) ? cognome_ragsoc_referente : tab_referente.nominativoDisplay;
                    case "T":
                        return !string.IsNullOrEmpty(cognome_ragsoc_terzo) ? cognome_ragsoc_terzo : tab_terzo_sto.tab_terzo.terzoNominativoDisplay_Stampa;
                    case "U":
                        return "Copia per Ufficiale della Riscossione";
                    default:
                        return !string.IsNullOrEmpty(cognome_ragsoc_contribuente) ? cognome_ragsoc_contribuente : nominativo_recapito;
                }
            }
        }

        [DisplayName("Destinatario")]
        public string Destinatario
        {
            get
            {
                //if (id_terzo_debitore.HasValue && id_referente.HasValue)
                //{
                //    if (tab_contribuente != null)
                //    {
                //        return nominativo_recapito + " " + indicativo_rappresentanza + " " + nominativo_rappresentanza + " coobbligato del contribuente " + tab_contribuente.nominativoDiplay;
                //    }
                //    else
                //    {
                //        return nominativo_recapito + " " + indicativo_rappresentanza + " " + nominativo_rappresentanza;
                //    }
                //}
                //else if (id_terzo_debitore.HasValue || id_referente.HasValue)
                //{
                //    return nominativo_recapito + " " + indicativo_rappresentanza + " " + nominativo_rappresentanza;
                //}
                //else
                //{
                //    return nominativo_recapito;
                //}
                return DestinatarioSpedNot;
            }
        }

        [DisplayName("Indirizzo")]
        public string Indirizzo
        {
            get
            {
                string numcivicodaAsString = num_civico.HasValue ? Convert.ToString(num_civico.Value) : string.Empty;

                return indirizzo_recapito + " " + numcivicodaAsString + ", " + cap + " " + des_comune + (!string.IsNullOrEmpty(prov) ? " (" + prov.Trim() + ")" : string.Empty);
            }
        }

        public string IndirizzoDestinatario
        {
            get
            {
                string numcivicodaAsString = (num_civico.HasValue && num_civico.Value > 0) ? Convert.ToString(num_civico.Value) : "";
                string sigladaAsString =  "";
                if(sigla!=null && sigla.ToLower() != "snc" && sigla.Length==1)
                {
                    sigladaAsString = "/" +sigla ;
                }
                if(numcivicodaAsString.Trim().Length>0)
                {
                    numcivicodaAsString = " n. " + numcivicodaAsString;
                }
                return indirizzo_recapito +  numcivicodaAsString + sigladaAsString ;
            }
        }

        public string CapComuneProvDestinatario
        {
            get
            {
                return (cap != null ? cap.Trim() : string.Empty) + " " + (des_comune != null ? des_comune.Trim() : string.Empty) + " " + (prov != null ? prov.Trim() : string.Empty) + " ";
            }
        }
        public string CapComuneProvDestinatarioPerStampa
        {
            get
            {
                return (cap != null ? cap.Trim() : string.Empty) + " " + (des_comune != null ? des_comune.Trim() : string.Empty) + " " + (prov != null ? prov.Trim() : string.Empty);
            }
        }

        public bool isAvvisoImmagineVisibile
        {
            get
            {
                //Romolo dice di rimuovere gli stati ASS_ASS e ASS_NRR
                if ((cod_stato.Equals(anagrafica_stato_sped_not.SPE_PRE) && tab_avv_pag.anagrafica_tipo_avv_pag.id_entrata == anagrafica_entrate.RISCOSSIONE_COATTIVA) ||
                     //cod_stato.Equals(anagrafica_stato_sped_not.ASS_ASS) ||
                     //cod_stato.Equals(anagrafica_stato_sped_not.ASS_NRR) ||
                     cod_stato.StartsWith(anagrafica_stato_sped_not.ANN))
                {
                    return false;
                }
                else
                {
                    return true;
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
