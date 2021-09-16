using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.PagoPA.Consts
{
    public class PagoPAConsts_Old
    {
        public const string ERR_MESSAGE_MAX_AVVISI_CARRELLO = "L'avviso selezionato non può essere pagato con il Carrello già costruito  in quanto sono stati precedentemente selezionati 5 differenti atti di pagamento che è il numero massimo di Atti pagabili con lo stesso Carrello di pagamento – Seleziona Conferma Carrello per pagare il carrello già completo. ";
        public const string PGP_FONTE_CARRELLO_WEB = "WEB";
        public const string PGP_FONTE_CARRELLO_PSP = "WEB";
        public const string PGP_TIPO_CARRELLO_S = "S";
        public const string PGP_TIPO_CARRELLO_M = "M";
        public const string PGP_TIPO_CARRELLO_C = "C";

        public const string ERR_RESULT_CARRELLO = "KO_CARRELLO";
        public const string SUCCESS_RESULT_CARRELLO = "OK_CARRELLO";
        //<i class="fas fa-angle-double-right"></i
        //<span style='color:rgba(255,217,104,.6)'>&#9658;</span>
        public const string ERR_ENTE_NOT_PAGOPA = "Operazione non consentita in quanto l'Ente creditore a favore del quale si vuole effettuare il pagamento non gestisce ancora il sistema PAGOPA. ";
        public const string ERR_NOT_CONTRIBUENTE = "Non è stato selezionato alcun contribuente. ";
        public const string ERR_NOT_RATE_NOT_MATCH = "Per l'avviso selezionato non sono state trovate rate pagabili. ";
        public const string OK_SAVE_CARRELLO = "Salvataggio del carrello dei pagamenti eseguito con successo!";
        public const string ERR_SAVE_CARRELLO = "Errore in fase di salvataggio del carrello dei pagamenti.";
        public const string ERR_PREPARE_CARRELLO = "Errore in fase di preparazione del carrello dei pagamenti.";
        public const string ERR_PREPARE_RATE_CARRELLO = "Errore in fase di preparazione delle rate del carrello dei pagamenti.";
        public const string COUNT_RATE_CARRELLO = "<span style='font-weight:600'>&#8226;</span> Totale rate dell'avviso {0}:  Pagate:  {1} <span style='font-weight:600'>&#187;</span> Da pagare: {2} <span style='font-weight:600'>&#187;</span> In Pagamento: {3}";
        public const string RPT_VERSIONE = "6.2.0";

        public const string LEVEL_MSG_PAGOPA_INFORMATION = "INFORMATION";
        public const string LEVEL_MSG_PAGOPA_WARNING = "WARNING";
        public const string LEVEL_MSG_PAGOPA_ERROR = "ERROR";

        public const int CRONOLOGIA_AVVISI_PAGABILI = 15537;
        public const string MSG_START_FIND_AVVISI_PAGABILI = "Avvio ricerca avvisi pagabili in ordine decrescente.";
        public const string MSG_AVVISI_PAGABILI_NO_MATCH = "Non sono stati trovati avvisi pagabili per le entrate selezionate.";
        public const string MSG_AVVISI_PAGABILI_NO_MATCH_CONTR = "Non sono stati trovati avvisi pagabili per il  contribuente.";
        public const string MSG_REMOVE_CARRELLO = "Rimozione del carrello in memoria.";
        public const string ERR_PAGOPA_FIND_AVVISI = "Errore in fase di ricerca degli avvisi. {0}";
        public const string MSG_FIND_AVVISI = "Sono stati trovati {0} avvisi.";
        public const string ERR_LOADING_AVVISI = "Errore in fase di caricamento degli avvisi pagabili. {0}";
        public const int CARRELLO_CRONOLOGIA_AVVISI_PAGABILI = 15560;
        public const string MSG_START_CARRELLO_AVVISI_PAGABILI = "Avvio carrello avvisi pagabili in ordine decrescente.";
        public const string MSG_CARRELLO_TOT_PAGAMENTI = "Totale pagamenti carrello {0}.";
        public const string ERR_PAGOPA_CARRELLO_AVVISI_LOADING = "Errore in fase di caricamento del carrello degli avvisi pagabili. {0}";
        public const int CARRELLO_RATE = 15588;
        public const string MSG_START_RATE = "Verifica rate dell'avviso. Id Avviso: {0}";
        public const string MSG_NUM_AVVISI_CARRELLO = "Numero avvisi in pagamento presenti nel carrello {0}.";
        public const string MSG_CARRELLO_TOT_RATE_PAGAMENTI_AVVISO = "Totale pagamento rate per l'avviso in carrello {0}.";
        public const string ERR_CARRELLO_RATE = "Errore in fase di caricamento delle rate. {0}";
        public const string ERR_CARRELLO_RATE_LOADING_AJAX = "[GridUpdateRateCarrello] Errore in fase di caricamento delle rate dell'avviso. {0}";
        //Per le applicazioni non presenti nel database
        public const int NOT_APP_ID = 0;
        public const string MSG_SELECT_RATA = "[SelezionaRata] Avvio flusso per la selezione della rata con Id {0}. Importo: {1}";
        public const string ERR_SELEZIONA_RATA= "[SelezionaRata] Errore in fase di selezione della rata. {0}";

        public const string MSG_UNDO_CARRELLO_AVVISO = "[AnnullaPagamentoRate] Annullamento pagamento rate per l'avviso {0}.";
        public const string ERR_UNDO_CARRELLO_AVVISO = "[AnnullaPagamentoRate] Errore in fase di annullamento pagamento rate per l'avviso. {0}";

        public const string MSG_CONFERMA_PAGAMENTO_RATE = "[ConfermaPagamentoRate] Conferma del pagamento delle rate.";
        public const string ERR_CONFERMA_PAGAMENTO_RATE = "[ConfermaPagamentoRate] Errore in fase di conferma del pagamento delle rate per l'avviso. {0}";

        public const string MSG_UNDO_PAGAMENTI = "[AnnullaPagamenti] Annullamento di tutti i pagamenti in carrello.";

        public const int DETTAGLIO_CARRELLO = 15562;
        public const string MSG_DETTAGLIO_CARRELLO = "Caricamento del dettaglio del carrello.";
        public const string MSG_TOT_DETTAGLIO_CARRELLO = "Totale del dettaglio del carrello {0}.";
        public const string ERR_DETTAGLIO_CARRELLO = "Errore in fase di caricamento del dettaglio del carrello. {0}";

        public const string MSG_DELETE_PAGAMENTO = "[EliminaPagamento] Eliminazione del pagamento della rata con Id {0} dal carrello.";
        public const string ERR_DELETE_PAGAMENTO = "[EliminaPagamento] Errore in fase di eliminazione del pagamento della rata dal carrello. {0}";

        public const string MSG_CONFERMA_PAGAMENTO_CARRELLO = "[ConfermaCarrello] Conferma del pagamento del carrello.";

        public const string MSG_PREPARE_CARRELLO_SUCCESS = "[ConfermaCarrello] Generazione del carrello eseguita con successo.";
        public const string MSG_PREPARE_RATE_CARRELLO_SUCCESS = "[ConfermaCarrello] Generazione delle rate del carrello eseguita con successo.";
        public const string MSG_SAVE_CARRELLO_SUCCESS = "[ConfermaCarrello] Salvataggio del carrello eseguito con successo. Id Carrello {0}";
        public const string MSG_CALL_NODO_PAGOPA = "[ConfermaCarrello] Avvio chiamata al nodo PagoPA {0}.";
        public const string ERR_LOG_SAVE_CARRELLO = "[ConfermaCarrello] Errore in fase di salvataggio del carrello.";
        public const string ERR_LOG_CALL_NODO_PAGOPA = "[ConfermaCarrello] Errore nell'invio del carrello RPT al nodo {0} PagoPA. [{1}].";
        public const string ERR_CALL_NODO_PAGOPA = "Errore nell'invio del carrello dei pagamenti al servizio PagoPA.";

        public const string ERR_RESPONSE_IS_NULL = "La risposta di GovPay è NULL.";
        public const string EXP_GOVPAY_IS_NULL = "GovPayException è NULL.";

        public const string GOVPAY_DB_ERROR = "Errore nel database. {0}";
    }
}
