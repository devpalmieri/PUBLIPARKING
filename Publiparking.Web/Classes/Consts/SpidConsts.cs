using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Publiparking.Web.Classes.Consts
{
    public class SpidConsts
    {
        /// <summary>
        /// Id applicazione richiesta SPID
        /// </summary>
        public const int SPID_REQUEST = 15483;
        /// <summary>
        /// Id applicazione logout  SPID
        /// </summary>
        public const int SPID_LOGOUT = 15493;
        /// <summary>
        /// Id applicazione response  SPID
        /// </summary>
        public const int SPID_RESPONSE = 15490;
        public const string LEVEL_MSG_SPID_INFORMATION = "INFORMATION";
        public const string LEVEL_MSG_SPID_WARNING = "WARNING";
        public const string LEVEL_MSG_SPID_ERROR = "ERROR";
        //MESSAGGI
        public const string MSG_SPID_START_AUTHENTICATION = "Avvio autenticazione SPID.";
        public const string MSG_SPID_ENTE = "Ente (IDP) di autenticazione SPID: {0}";
        public const string MSG_SPID_CERTIFICATE = "Recupero e validazione certificato.";
        public const string MSG_SPID_SIGNED = "Inserita la firma sulla richiesta SAML.";
        public const string MSG_SPID_URL_SIGN = "URL di autenticazione: {0}";
        public const string MSG_SPID_SAML_REQUEST = "SAML Request: {0}";
        public const string MSG_SPID_SAML_RESPONSE = "SAML Response: {0}";
        public const string MSG_SPID_AUTHENTICATION_SUCCESS = "Autenticazione SPID eseguita con successo. Utente: {0} {1} - CF: {2}";
        public const string MSG_SPID_START_AUTHENTICATION_PUBLISERVIZI = "Avvio autenticazione utente SPID sul portale Publiservizi.";
        public const string MSG_SPID_AUTHENTICATION_PUBLISERVIZI_SUCCESS = "Autenticazione utente SPID sul portale Publiservizi eseguita con successo. ID: {0}; USERNAME: {1}";
        public const string MSG_SPID_ATTEMPT_NEW_AUTHENTICATION = "Avvio tentativo nuova autenticazione SPID.";
        public const string MSG_SPID_START_LOGOUT = "Avvio logout SPID.";
        //WARNING
        public const string WARN_SPID_AUTHENTICATION_SUCCESS_NOT_PUBLISERVIZI = "L'autenticazione SPID è stata eseguita correttamente. Ma è necessario registrarsi al portale Publiservizi!";
        public const string WARN_SPID_AUTHENTICATION_SUCCESS_NOT_CONTRIBUENTE = "L'autenticazione SPID è stata eseguita correttamente. Ma non sei registrato come contribuente in Fiscolocale!";


        //ERRORI
        public const string ERR_SPID_AUTHENTICATION_FAILLURE = "Autenticazione SPID fallita. ";

        public const string ERR_SPID_AUTHENTICATE_REQUEST = "Errore nella preparazione della richiesta di autenticazione da inviare al provider. {0}";
        public const string ERR_SPID_COOKIE_EXPIRED = "Impossibile recuperare i dati della sessione (cookie scaduto).";
        public const string ERR_SPID_AUTHENTICATION_REFUSED = "La richiesta di identificazione è stata rifiutata.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
        public const string ERR_SPID_REPLY_IDP_NOT_VALID = "La risposta dell'IdP non è valida perché non corrisponde alla richiesta.  RequestId: _{0}; RequestPath: {1}; InResponseTo: {2}; Recipient: {3}.";
        public const string ERR_SPID_READ_REPLY_PROVIDER = "Errore nella lettura della risposta ricevuta dal provider. {0}";
        public const string ERR_SPID_LOGOUT_REFUSED = "La richiesta di logout è stata rifiutata.  $StatusCode: {0} con StatusMessage: {1} e StatusDetail: {}.";
        public const string ERR_SPID_REPLY_IDP = "La risposta dell'IdP non è valida perché non corrisponde alla richiesta.  $RequestId: _{0}, RequestPath: {1}, InResponseTo: {2}.";
        public const string ERR_SPID_COOKIE_NOT_DATA = "Impossibile recuperare i dati della sessione (il cookie non contiene tutti i dati necessari).";
        public const string ERR_SPID_LOGOUT_REQUEST = "Errore nella preparazione della richiesta di logout da inviare al provider. {0}";
        public const string ERR_SPID_REQUEST_TIMEOUT = "Errore di timeout in fase di invio della richiesta. {0}";
        public const string ERR_SPID_RESPONSE_TIMEOUT = "Errore di timeout in fase di recupero della risposta. {0}";
        public const string ERR_SPID_RESP_LOGOUT_TIMEOUT = "Errore di timeout in fase di recupero della risposta di logout. {0}";
        public const string ERR_SPID_REQ_LOGOUT_TIMEOUT = "Errore di timeout in fase di invio della richiesta di logout. {0}";
        public const string ERR_SPID_DESTINATION = "L'URL di destinazione presente è errato!";
        public const string ERR_SPID_AUDIENCE = "L'URL dell'audience contenuto nella response è errato!";
        public const string ERR_SPID_ISSUER = "L'URL dell'AssertionIssuer è errato!";
        public const string ERR_INSERT_USER_SPID = "Errore in fase di inserimento dell'utente ({0}) SPID. {1}";


        //TIPOLOGIA LISTA IDP
        public const string SPID_IDP_PRODUZIONE = "P";
        public const string SPID_IDP_TEST = "T";
        public const string SPID_IDP_ALL = "A";
        public const string SPID_ERR_MSG_CUSTOM_18 = "Elemento StatusCode diverso da success (non valido).";
        public const string SPID_ERR_MSG_19 = "Autenticazione fallita per ripetuta sottomissione di credenziali errate.";
        public const string SPID_ERR_MSG_20 = "Utente privo di credenziali compatibili con il livello richiesto dal fornitore del servizio.";
        public const string SPID_ERR_MSG_21 = "Timeout durante l'autenticazione utente.";
        public const string SPID_ERR_MSG_22 = "L'Utente ha negato il consenso all'invio di dati al SP.";
        public const string SPID_ERR_MSG_23 = "Utente con identità sospesa/ revocata o con credenziali bloccate.";
        //string SPID_ERR_MSG_24 = "ErrorCode nr24";
        public const string SPID_ERR_MSG_25 = "Processo di autenticazione annullato dall'utente.";
        public const string SPID_ERR_CODE_CUSTOM_18 = "errorcode nr18";
        public const string SPID_ERR_CODE_19 = "errorcode nr19";
        public const string SPID_ERR_CODE_20 = "errorcode nr20";
        public const string SPID_ERR_CODE_21 = "errorcode nr21";
        public const string SPID_ERR_CODE_22 = "errorcode nr22";
        public const string SPID_ERR_CODE_23 = "errorcode nr23";
        //string SPID_ERR_CODE_24 = "ErrorCode nr24";
        public const string SPID_ERR_CODE_25 = "errorcode nr25";


        public const string OK_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";


    }
    public static class SpidHelper
    {
        public static string SerializeAnObject(object AnObject)
        {
            XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
            StringWriter Writer = new StringWriter();

            Xml_Serializer.Serialize(Writer, AnObject);
            return Writer.ToString();
        }
        public static Object DeSerializeAnObject(string XmlOfAnObject, Type ObjectType)
        {
            StringReader StrReader = new StringReader(XmlOfAnObject);
            XmlSerializer Xml_Serializer = new XmlSerializer(ObjectType);
            XmlTextReader XmlReader = new XmlTextReader(StrReader);
            try
            {
                Object AnObject = Xml_Serializer.Deserialize(XmlReader);
                return AnObject;
            }
            finally
            {
                XmlReader.Close();
                StrReader.Close();
            }
        }
        public static string FormatFiscalCodeSpid(string p_value)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(p_value))
            {
                string[] splitted = p_value.Split('-');
                if (splitted.Length == 1)
                    result = splitted[0];
                else if (splitted.Length > 1)
                    result = splitted[1];
                else
                    result = string.Empty;
            }
            return result;
        }
        public static int GetIdSpidUserLog()
        {
            int result = Convert.ToInt32(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
            return result;
        }

        public static string GetMessageStatusRequest(string statusCode)
        {
            string message = string.Empty;
            switch (statusCode.ToLower())
            {
                case "genericerror":
                    {
                        message = "Errore generico.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "requestererror":
                    {
                        message = "La richiesta non può essere eseguita a causa di un errore da parte del richiedente.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "respondererror":
                    {
                        message = "Impossibile eseguire la richiesta a causa di un errore da parte del risponditore SAML o dell'autorità SAML.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "versionmismatcherror":
                    {
                        message = "Il risponditore SAML non è stato in grado di elaborare la richiesta perché la versione del messaggio di richiesta era errata.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "authnfailed":
                    {
                        message = "Il provider non è stato in grado di autenticare correttamente l'entità.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "invalidattrnameorvalue":
                    {
                        message = "È stato rilevato contenuto imprevisto o non valido all'interno di un elemento <saml: Attribute> o <saml: AttributeValue>.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "invalidnameidpolicy":
                    {
                        message = "Il provider non può o non supporterà la politica di identificazione del nome richiesta.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "noauthncontext":
                    {
                        message = "Il rispondente non può soddisfare i requisiti del contesto di autenticazione specificati.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "noavailableidp":
                    {
                        message = "Nessuno degli elementi <Loc> del provider di identità supportati in un <IDPList> può essere risolto o che nessuno dei provider di identità supportati è disponibile.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "nopassive":
                    {
                        message = "Il provider non può autenticare passivamente l'entità, come è stato richiesto.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "nosupportedidp":
                    {
                        message = "Nessuno dei provider di identità dell'<IDPList> è supportato dall'intermediario.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "partiallogout":
                    {
                        message = "Non è possibile eseguire la richiesta di disconnessione a tutti gli altri partecipanti alla sessione.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "proxycountexceeded":
                    {
                        message = "Il provider non può autenticare direttamente l'entità e non è autorizzato a delegare ulteriormente la richiesta.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "requestdenied":
                    {
                        message = "Il risponditore SAML o l'autorità SAML è in grado di elaborare la richiesta ma ha scelto di non rispondere. Questo codice di stato PUO 'essere utilizzato in caso di dubbi sul contesto di sicurezza del messaggio di richiesta o sulla sequenza dei messaggi di richiesta ricevuti da un particolare richiedente.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "requestunsupported":
                    {
                        message = "Il risponditore SAML o l'autorità SAML non supporta la richiesta.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "requestversiondeprecated":
                    {
                        message = "Il risponditore SAML non può elaborare alcuna richiesta con la versione del protocollo specificata nella richiesta.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "requestversiontoohigh":
                    {
                        message = "Il risponditore SAML non può elaborare la richiesta perché la versione del protocollo specificata nel messaggio di richiesta è un aggiornamento importante rispetto alla versione del protocollo più alta supportata dal risponditore.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case " requestversiontoolow":
                    {
                        message = "Il risponditore SAML non può elaborare la richiesta perché la versione del protocollo specificata nel messaggio di richiesta è troppo bassa.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "resourcenotrecognized":
                    {
                        message = "Il valore della risorsa fornito nel messaggio di richiesta non è valido o non riconosciuto.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "toomanyresponses":
                    {
                        message = "Il messaggio di risposta conterrebbe più elementi di quanti il ​​risponditore SAML sia in grado di restituire.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "unknownattrprofile":
                    {
                        message = "Un'entità che non ha conoscenza di un particolare profilo di attributo è stata presentata con un attributo tratto da quel profilo.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "unknownprincipal":
                    {
                        message = "Il fornitore non riconosce l'utente (Principal) specificato o implicito dalla richiesta.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
                case "unsupportedbinding":
                    {
                        message = "Il risponditore SAML non può soddisfare correttamente la richiesta utilizzando l'associazione di protocollo specificata nella richiesta stessa.  $ StatusCode: {0} con StatusMessage: {1} e StatusDetail: {2}.";
                        break;
                    }
            }

            return message;
        }

    }
    public enum SpidUserLog : int
    {
        SPID_USER = 0
    }
}
