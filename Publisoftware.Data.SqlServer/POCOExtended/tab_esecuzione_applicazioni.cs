using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_esecuzione_applicazioni.Metadata))]
    public partial class tab_esecuzione_applicazioni : ISoftDeleted,IGestioneStato
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }
        /// <summary>
        /// Gestisce l'aggiornamento dei campi utente dello stato
        /// </summary>
        public void SetUserStato(int p_idStruttura, int p_idRisorsa)
        {
            data_stato = DateTime.Now;
            id_struttura_stato = p_idStruttura;
            id_risorsa_stato = p_idRisorsa;
        }

        public class EsecuzioneApplicazioniStatus
        {
            public enum ActionStatus_t
            {
                Scheduled,
                Schedulable,
                Uneditable,
                Unknown
            }

            public string CodStato { get; set; }
            public ActionStatus_t ActionStatus { get; set; }
            public string DescrizioneCodStato { get; set; }
        }

        public const string cod_stato_schedulata = "ATT-SCH";
        public const string cod_stato_pianificazione_annullata = "ATT-ANN";

        public static EsecuzioneApplicazioniStatus GetEsecuzioneApplicazioniStatus(string cod_status)
        {
            string retval = string.Empty;
            EsecuzioneApplicazioniStatus.ActionStatus_t status = EsecuzioneApplicazioniStatus.ActionStatus_t.Unknown;

            switch (cod_status)
            {
                // ------------------------------------------------
                // Pianificata
                case "ATT-PRE":
                    retval = "Applicazione in Pianificazione";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Scheduled;
                    break;
                case cod_stato_schedulata:
                    retval = "Applicazione Schedulata";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Scheduled;
                    break;
                case "ATT-PIA":
                    retval = "Applicazione Pianificata";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Scheduled;
                    break;

                // ------------------------------------------------
                // Pianificabile
                case "ATT-ESE":
                    retval = "Applicazione eseguita correttamente";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Schedulable;
                    break;
                case "ATT-SPS":
                    retval = "Applicazione sospesa";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Schedulable;
                    break;
                case "ATT-ERR":
                    retval = "Applicazione terminata con errore";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Schedulable;
                    break;
                case "ANN-":
                case "ANN-ANN":
                case cod_stato_pianificazione_annullata:
                    retval = "Applicazione annullata";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Schedulable;
                    break;

                // ------------------------------------------------
                // Non modificabile
                case "ATT-LAN":
                    retval = "Applicazione in esecuzione";
                    status = EsecuzioneApplicazioniStatus.ActionStatus_t.Uneditable;
                    break;

                default:
                    retval = "Applicazione in stato ignoto";
                    break;
            }

            return new EsecuzioneApplicazioniStatus
            {
                CodStato = cod_status,
                DescrizioneCodStato = retval,
                ActionStatus = status
            };
        }

        private string GetDescrizioneCodStato(string cod_stato)
        {
#if ORIG
            string retval = string.Empty;
            switch (cod_stato)
            {
                // ------------------------------------------------
                // Pianificata
                case "ATT-PRE":
                    retval = "Applicazione in Pianificazione";
                    break;
                case "ATT-SCH":
                    retval = "Applicazione Schedulata";
                    break;
                case "ATT-PIA":
                    retval = "Applicazione Pianificata";
                    break;

                // ------------------------------------------------
                // Pianificabile
                case "ATT-ESE":
                    retval = "Applicazione eseguita correttamente";
                    break;
                case "ATT-SPS":
                    retval = "Applicazione sospesa";
                    break;
                case "ATT-ERR":
                    retval = "Applicazione terminata con errore";
                    break;
                case "ANN-":
                case "ANN-ANN":
                case "ATT-ANN":
                    retval = "Applicazione annullata";
                    break;

                // ------------------------------------------------
                // Non modificabile
                case "ATT-LAN":
                    retval = "Applicazione in esecuzione";
                    break;

                default:
                    break;
            }
            return retval;
#else
            return GetEsecuzioneApplicazioniStatus(cod_stato).DescrizioneCodStato;
#endif
        }

        public string cod_stato_Descrizione
        {
            get
            {
                return GetDescrizioneCodStato(this.cod_stato);
            }
        }

        public string cod_stato_prec_Descrizione
        {
            get
            {
                return GetDescrizioneCodStato(this.cod_stato_prec);
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }
            [DisplayName("Codice Stato")]
            public string cod_stato { get; set; }
        }

    }
}
