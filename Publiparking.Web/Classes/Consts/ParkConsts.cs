using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Classes.Consts
{
    public class ParkConsts
    {
        public const int DELAY_TIME = 1000;
        /// <summary>
        /// Messaggio di tipo Success per la modale test
        /// </summary>
        public const string MODAL_TYPE_SUCCESS = "SUCCESS";

        /// <summary>
        /// Messaggio di tipo Info per la modale
        /// </summary>
        public const string MODAL_TYPE_INFO = "INFO";
        /// <summary>
        /// Messaggio di tipo warning per la modale
        /// </summary>
        public const string MODAL_TYPE_WARNING = "WARNING";
        /// <summary>
        /// Messaggio di tipo question per la modale
        /// </summary>
        public const string MODAL_TYPE_QUESTION = "QUESTION";
        /// <summary>
        /// Messaggio di tipo error per la modale
        /// </summary>
        public const string MODAL_TYPE_ERROR = "ERROR";

        /// <summary>
        /// Start Mode Utente Web
        /// </summary>
        public const string DEFAULT_HOME_UTENTE = "HOMEUTENTE";
        /// <summary>
        /// Start Mode Operatore Backoffice
        /// </summary>
        public const string DEFAULT_HOME_OPERATORE = "HOMEOPERATORE";


        public const string ERR_PDF_PATH_EMPTY = "Il percorso del file PDF è vuoto.";

        public const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";

        public const string ERR_LOGIN_FAILLURE = "Autenticazione non riuscita. Credenziali errate.";

        public const string COOKIE_ACCEPET = "OK";
        public const string COOKIE_NOT_ACCEPET = "KO";

        public const string CONSENSO_ALL = "TOTALE";
        public const string CONSENSO_PARTIAL = "PARZIALE";
        public const string CONSENSO_DENIED = "NEGATO";
    }
}
