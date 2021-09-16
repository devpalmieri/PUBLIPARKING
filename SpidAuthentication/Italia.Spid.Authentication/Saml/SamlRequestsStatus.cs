using System;
using System.Collections.Generic;
using System.Text;

namespace Italia.Spid.Authentication.Saml
{
    /// <summary>
    /// Saml Request Status
    /// </summary>
    public enum SamlRequestStatus
    {
        /// <summary>
        /// Richiesta eseguita con successo.
        /// </summary>
        Success,

        /// <summary>
        /// Errore generico
        /// </summary>
        GenericError,

        /// <summary>
        /// la richiesta non può essere eseguita a causa di un errore da parte del richiedente.
        /// </summary>
        RequesterError,

        /// <summary>
        /// Impossibile eseguire la richiesta a causa di un errore da parte del risponditore SAML o dell'autorità SAML.
        /// </summary>
        ResponderError,

        /// <summary>
        /// Il risponditore SAML non è stato in grado di elaborare la richiesta perché la versione del messaggio di richiesta era errata.
        /// </summary>
        VersionMismatchError,


        /// <summary>
        /// Il provider non è stato in grado di autenticare correttamente l'entità.
        /// </summary>
        AuthnFailed,

        /// <summary>
        ///  È stato rilevato contenuto imprevisto o non valido all'interno di un elemento <saml: Attribute> o <saml: AttributeValue>.
        /// </summary>
        InvalidAttrNameOrValue,

        /// <summary>
        /// Il provider non può o non supporterà la politica di identificazione del nome richiesta.
        /// </summary>
        InvalidNameIDPolicy,

        /// <summary>
        /// Il rispondente non può soddisfare i requisiti del contesto di autenticazione specificati.
        /// </summary>
        NoAuthnContext,

        /// <summary>
        /// Nessuno degli elementi <Loc> del provider di identità supportati in un <IDPList> può essere risolto o che nessuno dei provider di identità supportati è disponibile.
        /// </summary>
        NoAvailableIDP,

        /// <summary>
        ///Il provider non può autenticare passivamente l'entità, come è stato richiesto.
        /// </summary>
        NoPassive,

        /// <summary>
        /// Nessuno dei provider di identità dell'<IDPList> è supportato dall'intermediario.
        /// </summary>
        NoSupportedIDP,

        /// <summary>
        /// Non è possibile eseguire la richiesta di disconnessione a tutti gli altri partecipanti alla sessione.
        /// </summary>
        PartialLogout,

        /// <summary>
        /// Il provider non può autenticare direttamente l'entità e non è autorizzato a delegare ulteriormente la richiesta.
        /// </summary>
        ProxyCountExceeded,

        /// <summary>
        /// Il risponditore SAML o l'autorità SAML è in grado di elaborare la richiesta ma ha scelto di non rispondere. Questo codice di stato PUO 'essere utilizzato in caso di dubbi sul contesto di sicurezza del messaggio di richiesta o sulla sequenza dei messaggi di richiesta ricevuti da un particolare richiedente.
        /// </summary>
        RequestDenied,

        /// <summary>
        /// Il risponditore SAML o l'autorità SAML non supporta la richiesta.
        /// </summary>
        RequestUnsupported,

        /// <summary>
        /// Il risponditore SAML non può elaborare alcuna richiesta con la versione del protocollo specificata nella richiesta.
        /// </summary>
        RequestVersionDeprecated,

        /// <summary>
        /// Il risponditore SAML non può elaborare la richiesta perché la versione del protocollo specificata nel messaggio di richiesta è un aggiornamento importante rispetto alla versione del protocollo più alta supportata dal risponditore.
        /// </summary>
        RequestVersionTooHigh,

        /// <summary>
        /// Il risponditore SAML non può elaborare la richiesta perché la versione del protocollo specificata nel messaggio di richiesta è troppo bassa.
        /// </summary>
        RequestVersionTooLow,

        /// <summary>
        /// Il valore della risorsa fornito nel messaggio di richiesta non è valido o non riconosciuto.
        /// </summary>
        ResourceNotRecognized,

        /// <summary>
        /// Il messaggio di risposta conterrebbe più elementi di quanti il ​​risponditore SAML sia in grado di restituire.
        /// </summary>
        TooManyResponses,

        /// <summary>
        /// Un'entità che non ha conoscenza di un particolare profilo di attributo è stata presentata con un attributo tratto da quel profilo.
        /// </summary>
        UnknownAttrProfile,

        /// <summary>
        /// Il fornitore non riconosce l'utente (Principal) specificato o implicito dalla richiesta.
        /// </summary>
        UnknownPrincipal,

        /// <summary>
        /// Il risponditore SAML non può soddisfare correttamente la richiesta utilizzando l'associazione di protocollo specificata nella richiesta stessa.
        /// </summary>
        UnsupportedBinding
    }
}
