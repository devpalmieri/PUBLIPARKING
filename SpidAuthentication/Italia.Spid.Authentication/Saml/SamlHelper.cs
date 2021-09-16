using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Italia.Spid.Authentication.IdP;
using Italia.Spid.Authentication.Schema;

namespace Italia.Spid.Authentication.Saml
{
    public static class SamlHelper
    {
        //private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.GovPay.Service");
        public const string SPID_ERR_CODE_CUSTOM_18 = "errorcode nr18";
        public const string SPID_ERR_CODE_19 = "errorcode nr19";
        public const string SPID_ERR_CODE_20 = "errorcode nr20";
        public const string SPID_ERR_CODE_21 = "errorcode nr21";
        public const string SPID_ERR_CODE_22 = "errorcode nr22";
        public const string SPID_ERR_CODE_23 = "errorcode nr23";
        //string SPID_ERR_CODE_24 = "ErrorCode nr24";
        public const string SPID_ERR_CODE_25 = "errorcode nr25";
        public const string SPID_USER_INFO = "spidCode|fiscalNumber|email|mobilePhone";
        /// <summary>
        /// Build a signed SAML authentication request.
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="destination"></param>
        /// <param name="consumerServiceURL"></param>
        /// <param name="securityLevel"></param>
        /// <param name="certFile"></param>
        /// <param name="certPassword"></param>
        /// <param name="storeLocation"></param>
        /// <param name="storeName"></param>
        /// <param name="findType"></param>
        /// <param name="findValue"></param>
        /// <param name="identityProvider"></param>
        /// <param name="enviroment"></param>
        /// <param name="request"></param>
        /// <returns>Returns a Base64 Encoded String of the SAML request </returns>
        public static string BuildAuthnPostRequest(string uuid, string destination, string consumerServiceURL, int securityLevel,
                                                       X509Certificate2 certificate, IdentityProvider identityProvider, int enviroment, out AuthnRequestType request)
        {
            request = null;
            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ArgumentNullException("Il parametro uuid non può essere nullo o vuoto.");
            }

            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new ArgumentNullException("Il parametro della destinazione non può essere nullo o vuoto.");
            }

            if (string.IsNullOrWhiteSpace(consumerServiceURL))
            {
                throw new ArgumentNullException("Il parametro consumerServiceURL non può essere nullo o vuoto.");
            }

            if (certificate == null)
            {
                throw new ArgumentNullException("Il certificato non può essere nullo.");
            }

            if (identityProvider == null)
            {
                throw new ArgumentNullException("Il parametro identityProvider non può essere nullo.");
            }

            if (enviroment < 0)
            {
                throw new ArgumentNullException("Il parametro enviroment non può essere.");
            }

            DateTime now = DateTime.UtcNow;

            AuthnRequestType authnRequest = new AuthnRequestType
            {
                ID = "_" + uuid,
                Version = "2.0",
                IssueInstant = identityProvider.Now(now),
                Destination = destination,
                AssertionConsumerServiceIndex = (ushort)enviroment,
                AssertionConsumerServiceIndexSpecified = true,
                AttributeConsumingServiceIndex = 1,
                AttributeConsumingServiceIndexSpecified = true,
                ForceAuthn = (securityLevel >= 1),
                ForceAuthnSpecified = (securityLevel >= 1),
                Issuer = new NameIDType
                {
                    Value = consumerServiceURL.Trim(),
                    Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity",
                    NameQualifier = consumerServiceURL
                },
                NameIDPolicy = new NameIDPolicyType
                {
                    Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient"
                },
                Conditions = new ConditionsType
                {
                    NotBefore = identityProvider.NotBefore(now),
                    NotBeforeSpecified = true,
                    NotOnOrAfter = identityProvider.After(now.AddMinutes(10)),
                    NotOnOrAfterSpecified = true
                },
                RequestedAuthnContext = new RequestedAuthnContextType
                {
                    Comparison = AuthnContextComparisonType.minimum,
                    ComparisonSpecified = true,
                    ItemsElementName = new ItemsChoiceType7[] { ItemsChoiceType7.AuthnContextClassRef },
                    Items = new string[] { "https://www.spid.gov.it/SpidL" + securityLevel.ToString() }
                }
            };
            request = authnRequest;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");
            ns.Add("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");

            StringWriter stringWriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                Encoding = Encoding.UTF8
            };

            XmlWriter responseWriter = XmlTextWriter.Create(stringWriter, settings);
            XmlSerializer responseSerializer = new XmlSerializer(authnRequest.GetType());
            responseSerializer.Serialize(responseWriter, authnRequest, ns);
            responseWriter.Close();

            string samlString = stringWriter.ToString();
            stringWriter.Close();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(samlString);

            XmlElement signature = XmlSigningHelper.SignXMLDoc(doc, certificate, "_" + uuid);
            doc.DocumentElement.InsertBefore(signature, doc.DocumentElement.ChildNodes[1]);

            return Convert.ToBase64String(Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + doc.OuterXml));
        }

        /// <summary>
        /// Get the IdP Authn Response and extract metadata to the returned DTO class
        /// </summary>
        /// <param name="base64Response"></param>
        /// <returns>IdpSaml2Response</returns>
        public static IdpAuthnResponse GetAuthnResponse(string base64Response, DateTime dtReceipt, out string userError, string idpName = "", string idpCertificate = "",
            string requestIssueInstant = "", string requestID = "", string requestSecurity = "")
        {
            const string VALUE_NOT_AVAILABLE = "N/A";
            string idpResponse;
            string OK_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
            string OK_Sub_NameID_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";
            string OK_Sub_Method = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
            string OK_Ass_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
            userError = string.Empty;
            string ErrorMessage = "Impossibile leggere gli attributi AttributeStatement dal documento SAML2. {0}";
            //Verifica base64 Response
            if (String.IsNullOrEmpty(base64Response))
            {
                throw new ArgumentNullException("Il parametro base64Response non può essere nullo o vuoto.");
            }
            //Verifica ID Response
            try
            {
                idpResponse = Encoding.UTF8.GetString(Convert.FromBase64String(base64Response));

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Impossibile convertire la risposta base64 in stringa ASCII.", ex);
            }

            try
            {
                // Verifica signature
                XmlDocument xml = new XmlDocument { PreserveWhitespace = true };
                xml.LoadXml(idpResponse);
                if (!XmlSigningHelper.VerifySignature(xml, idpResponse, idpCertificate))
                {
                    throw new Exception("Errore nella verificare la firma della risposta dell'IdP.");
                }

                // Parse XML document
                XDocument xdoc = new XDocument();
                xdoc = XDocument.Parse(idpResponse);

                string destination = VALUE_NOT_AVAILABLE;
                string id = VALUE_NOT_AVAILABLE;
                string inResponseTo = VALUE_NOT_AVAILABLE;
                DateTimeOffset issueInstant = DateTimeOffset.MinValue;
                string version = VALUE_NOT_AVAILABLE;
                string statusCodeValue = VALUE_NOT_AVAILABLE;
                string statusCodeInnerValue = VALUE_NOT_AVAILABLE;
                string statusMessage = VALUE_NOT_AVAILABLE;
                string statusDetail = VALUE_NOT_AVAILABLE;
                string assertionId = VALUE_NOT_AVAILABLE;
                DateTimeOffset assertionIssueInstant = DateTimeOffset.MinValue;
                string assertionVersion = VALUE_NOT_AVAILABLE;
                string assertionIssuer = VALUE_NOT_AVAILABLE;
                string subjectNameId = VALUE_NOT_AVAILABLE;
                string subjectConfirmationMethod = VALUE_NOT_AVAILABLE;
                string subjectConfirmationDataInResponseTo = VALUE_NOT_AVAILABLE;
                DateTimeOffset subjectConfirmationDataNotOnOrAfter = DateTimeOffset.MinValue;
                string subjectConfirmationDataRecipient = VALUE_NOT_AVAILABLE;
                DateTimeOffset conditionsNotBefore = DateTimeOffset.MinValue;
                DateTimeOffset conditionsNotOnOrAfter = DateTimeOffset.MinValue;
                string audience = VALUE_NOT_AVAILABLE;
                string audienceRestriction = VALUE_NOT_AVAILABLE;
                DateTimeOffset authnStatementAuthnInstant = DateTimeOffset.MinValue;
                string authnStatementSessionIndex = VALUE_NOT_AVAILABLE;
                Dictionary<string, string> spidUserInfo = new Dictionary<string, string>();

                //Regex per la verifica di date UTC con e senza millisecondi
                Regex regex = new Regex(@"\d{4}-[01]{1}\d{1}-[0-3]{1}\d{1}T[0-2]{1}\d{1}:[0-6]{1}\d{1}:[0-6]{1}\d{1}(\.\d{3})?Z");
                // Recupero metadata Response
                XElement responseElement = xdoc.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}Response").Single();
                if (responseElement == null)
                {
                    throw new ArgumentException("La Response è null.");
                }

                XAttribute xDestination = responseElement.Attribute("Destination");
                if (xDestination == null)
                {
                    throw new ArgumentException("Attributo Destination mancante.");
                }

                destination = responseElement.Attribute("Destination").Value;
                if (string.IsNullOrEmpty(destination))
                {
                    throw new ArgumentException("Attributo Destination non specificato.");
                }
                //Test 09
                XAttribute xId = responseElement.Attribute("ID");
                if (xId == null)
                {
                    throw new ArgumentException("Attributo ID mancante.");
                }
                //Test 08
                id = responseElement.Attribute("ID").Value;
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("Attributo ID non specificato.");
                }
                XAttribute xResponseTo = responseElement.Attribute("InResponseTo");
                if (xResponseTo == null)
                {
                    throw new ArgumentException("Attributo InResponseTo mancante.");
                }
                inResponseTo = responseElement.Attribute("InResponseTo").Value;
                if (string.IsNullOrEmpty(inResponseTo))
                {
                    throw new ArgumentException("Attributo InResponseTo non specificato.");
                }
                //TODO: IssueInstant Response
                //Test 12
                XAttribute xIssueInstant = responseElement.Attribute("IssueInstant");
                if (xIssueInstant == null)
                {
                    throw new ArgumentException("Attributo IssueInstant mancante.");
                }
                //Verifica presenza IssueInstant. Test 11                 
                issueInstant = DateTimeOffset.Parse(responseElement.Attribute("IssueInstant").Value);

                //Recupero valore IssueInstant
                string issueInstantValue = responseElement.Attribute("IssueInstant").Value;
                if (string.IsNullOrEmpty(issueInstantValue))
                {
                    throw new ArgumentException("Attributo IssueInstant non specificato.");
                }
                //Verifica formato UTC IssueInstant Test 13
                //Rimuovo eventuali spazi
                issueInstantValue = issueInstantValue.Replace(" ", "");
                Match match = regex.Match(issueInstantValue);
                if (!match.Success)
                {
                    throw new ArgumentException("Formato IssueInstant della Response non corretto.");
                }
                //Verifica versione (2.0)-Test 10
                XAttribute xVersion = responseElement.Attribute("Version");
                if (xVersion == null)
                {
                    throw new ArgumentException("Attributo Version mancante.");
                }
                version = responseElement.Attribute("Version").Value;
                if ((string.IsNullOrEmpty(version)) || (version != "2.0"))
                {
                    throw new ArgumentException("Attributo Version non specificato o diverso da 2.0.");
                }

                //Recupero nodo Issuer
                XElement issuerElelemnt = null;
                try
                {
                    issuerElelemnt = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single();
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento Issuer mancante.");
                }
                if (issuerElelemnt == null)
                {
                    throw new ArgumentException("Elemento Issuer mancante.");
                }
                //Verifica presenza e valore dell'attributo Format (Issuer)
                //PRECEDENTE FORMA DI CONTROLLO
                //XAttribute xFormat = issuerElelemnt.Attribute("Format");
                //if (xFormat == null)
                //{
                //    throw new ArgumentException("Attributo Format di Issuer mancante.");
                //}
                //string format = issuerElelemnt.Attribute("Format").Value;

                //if (format.ToLower() != OK_Issuer_Format.ToLower())
                //{
                //    throw new ArgumentException("Attributo Format non corretto.");
                //}

                XAttribute xFormat = issuerElelemnt.Attribute("Format");
                if (xFormat != null)
                {
                    string format = issuerElelemnt.Attribute("Format").Value;

                    if (format.ToLower() != OK_Issuer_Format.ToLower())
                    {
                        throw new ArgumentException("Attributo Format non corretto.");
                    }
                }

                //Verifica valore Issuer
                string issuer = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();
                if (string.IsNullOrEmpty(issuer))
                {
                    throw new ArgumentException("Valore Issuer non specificato.");
                }
                //Verifica uguaglianza Issuer rispetto all'idp della Request
                if (issuer.ToLower() != idpName.ToLower())
                {
                    throw new ArgumentException("Valore Issuer non corrisponde all'Idp della Request.");

                }

                // Recupero metdata Status
                XElement StatusElement = null;
                if (responseElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}Status") == null)
                {
                    throw new ArgumentException("Elemento Status della Response mancante.");
                }
                try
                {
                    StatusElement = responseElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}Status").Single();
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento Status mancante.");
                }
                if (StatusElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}StatusCode") == null)
                {
                    throw new ArgumentException("Elemento StatusCode mancante.");
                }
                IEnumerable<XElement> statusCodeElements = StatusElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}StatusCode");

                try
                {
                    statusCodeValue = statusCodeElements.First().Attribute("Value").Value;
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento StatusCode mancante.");
                }

                try
                {
                    statusCodeValue = statusCodeElements.First().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "");
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento Status non specificato.");
                }
                statusCodeInnerValue = statusCodeElements.Count() > 1 ? statusCodeElements.Last().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "") : VALUE_NOT_AVAILABLE;
                statusMessage = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusMessage").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;
                statusDetail = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusDetail").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;

                //Verifica errori/anomalie utente
                //Se il volore è diverso da "Success" recupero lo status code
                if (string.IsNullOrEmpty(statusCodeValue))
                {
                    throw new ArgumentException("Elemento StatusCode non specificato.");
                }
                if (statusCodeValue.ToLower() != "success")
                {
                    if (statusMessage.ToLower() == SPID_ERR_CODE_22)
                        userError = SPID_ERR_CODE_22;
                    else if (statusMessage.ToLower() == SPID_ERR_CODE_20)
                        userError = SPID_ERR_CODE_20;
                    else if (statusMessage.ToLower() == SPID_ERR_CODE_21)
                        userError = SPID_ERR_CODE_21;
                    else if (statusMessage.ToLower() == SPID_ERR_CODE_23)
                        userError = SPID_ERR_CODE_23;
                    else if (statusMessage.ToLower() == SPID_ERR_CODE_25)
                        userError = SPID_ERR_CODE_25;
                    else
                    {
                        userError = SPID_ERR_CODE_CUSTOM_18;
                    }

                }

                //TODO: Verificare valore IssueInstant Test 14-15
                DateTime IssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);

                DateTime reqIssueInstant = Convert.ToDateTime(requestIssueInstant);
                if (IssueInstant < reqIssueInstant)
                {
                    throw new ArgumentException("Valore di IssueInstant della Response inferiore alla Request");
                }
                if (issueInstant > reqIssueInstant.AddMinutes(5))
                {
                    throw new ArgumentException("Valore di IssueInstant della Response maggiore alla data di ricezione.");
                }
                //TODO: Non da eseguire sulla data di ricezione
                //if (issueInstant < dtReceipt)
                //{
                //    throw new ArgumentException("Valore di IssueInstant della Response inferiore alla Request");
                //}
                //if (issueInstant > dtReceipt.AddMinutes(5))
                //{
                //    throw new ArgumentException("Valore di IssueInstant della Response maggiore alla data di ricezione.");
                //}
                //Il valore dello status è Success
                //la Response è corretta inizia la verifica dell'Assertion
                if (statusCodeValue == "Success")
                {
                    // Recupero metadata dell'Assertion 
                    XElement assertionElement = null;
                    try
                    {
                        assertionElement = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Assertion").Single();
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Elemento Assertion della Response mancante.");
                    }
                    if (responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Assertion") == null)
                    {
                        throw new Exception("Elemento Assertion della Response mancante.");
                    }
                    // Verifica della Signature dell'Assertion
                    //TODO: Modificato per verificare
                    XmlDocument xmlAss = new XmlDocument { PreserveWhitespace = true };
                    xmlAss.LoadXml(idpResponse);
                    if (!XmlSigningHelper.VerifySignature(xmlAss, idpResponse, idpCertificate))
                    {
                        throw new Exception("Errore nella verificare la firma della risposta dell'IdP.");
                    }
                    //xml.LoadXml(idpResponse);
                    //if (!XmlSigningHelper.VerifyAssertionSignature(xml))
                    //{
                    //    throw new ArgumentException("Impossibile verificare la firma dell'Assertion.");
                    //}

                    XAttribute xAssertionId = assertionElement.Attribute("ID");
                    if (xAssertionId == null)
                    {
                        throw new ArgumentException("Attributo ID dell'Assertion mancante.");
                    }
                    assertionId = assertionElement.Attribute("ID").Value;
                    //Verifica Id dell'Assertion
                    if (string.IsNullOrEmpty(assertionId))
                    {
                        throw new ArgumentException("Attributo ID dell'Assertion non specificato.");
                    }
                    //Verifica IssueInstant dell'Assertion
                    XAttribute xAssertionIssueInstant = assertionElement.Attribute("IssueInstant");
                    if (xAssertionIssueInstant == null)
                    {
                        throw new ArgumentException("Attributo IssueInstant dell'Assertion mancante.");
                    }
                    try
                    {
                        assertionIssueInstant = DateTimeOffset.Parse(assertionElement.Attribute("IssueInstant").Value);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Attributo IssueInstant dell'Assertion non specificato.");
                    }

                    string issueAss = assertionElement.Attribute("IssueInstant").Value;
                    //Verifica formato UTC dell'IssueInstant dell'Assertion
                    //Rimuovo eventuali spazi
                    issueAss = issueAss.Replace(" ", "");
                    match = regex.Match(issueAss);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato attributo IssueInstant dell'Assertion non corretto.");
                    }
                    #region CONTROLLI ASSERTION IssueInstant SPOSTATI ALLA FINE
                    //TODO: SPOSTATO ALLA FINE 13/11/2020
                    ////Test 39-40
                    ////39) In Assertion IssueInstant non può essere minore della Request
                    ////40) In Assertion IssueInstant non può essere maggiore oltre 5 min della Request
                    //if (request != null)
                    //{
                    //    DateTime reqIssueInstant = Convert.ToDateTime(request.IssueInstant);
                    //    DateTime IssueInstantAss = Convert.ToDateTime(assertionElement.Attribute("IssueInstant").Value);
                    //    //TODO: Verificare IssueIstant Assertion
                    //    if (IssueInstantAss < reqIssueInstant)
                    //    {
                    //        throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion minore della Request.");
                    //    }
                    //    if (IssueInstantAss > reqIssueInstant.AddMinutes(5))
                    //    {
                    //        throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion maggiore della Request oltre il massimo consentito.");
                    //    }
                    //}
                    #endregion CONTROLLI ASSERTION IssueInstant SPOSTATI ALLA FINE
                    //Verifica versione dell'Assertion (2.0)
                    XAttribute xAssertionVersion = assertionElement.Attribute("Version");
                    if (xAssertionVersion == null)
                    {
                        throw new ArgumentException("Attributo Version dell'Assertion manacante.");
                    }
                    assertionVersion = assertionElement.Attribute("Version").Value;
                    if ((string.IsNullOrEmpty(assertionVersion)) || (assertionVersion != "2.0"))
                    {
                        throw new ArgumentException("Attributo Version dell'Assertion non specificato o diverso da 2.0");
                    }
                    //Verifica presenza Issuer dell'Assertion                    
                    try
                    {
                        assertionIssuer = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento Issuer dell'Assertion mancante.");
                    }
                    assertionIssuer = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();

                    // Recupero metadata Subject
                    try
                    {
                        if (assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Subject").Single() == null)
                        {
                            throw new ArgumentException("Elemento Subject dell'Assertion mancante.");
                        }
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Elemento Subject dell'Assertion mancante.");
                    }
                    XElement subjectElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Subject").Single();
                    if (!subjectElement.HasElements)
                    {
                        throw new ArgumentException("Elemento Subject dell'Assertion non specificato.");
                    }
                    //Verifica NameId Subject                  
                    try
                    {
                        subjectNameId = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}NameID").Single().Value.Trim();
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Elemento NameID di Subject dell'Assertion mancante.");
                    }
                    //Verifica valore SubjectId
                    if (string.IsNullOrEmpty(subjectNameId) || subjectNameId.ToUpper() == "N/A")
                    {
                        throw new ArgumentException("Valore di NameID di Subject non specificato.");
                    }
                    //Recupero e verifica NameId
                    XElement AssNameId = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}NameID").Single();
                    if (AssNameId == null)
                    {
                        throw new ArgumentException("Impossibile recuperare l'elemento NameID di Subject.");
                    }
                    //Recupero e verifica Attributo Format di NameId
                    XAttribute xFormatNameId = AssNameId.Attribute("Format");
                    if (xFormatNameId == null)
                    {
                        throw new ArgumentException("Attributo Format di NameID mancante.");
                    }
                    string formatNameId = AssNameId.Attribute("Format").Value;
                    if (string.IsNullOrEmpty(formatNameId))
                    {
                        throw new ArgumentException("Attributo Format dell'elemento NameID dell'Assertion non specificato.");
                    }
                    if (formatNameId.ToLower() != OK_Sub_NameID_Format.ToLower())
                    {
                        throw new ArgumentException("Valore dell'Attributo Format di NameID non corretto.");
                    }
                    //Recupero e verifica Attributo Format di NameQualifier
                    XAttribute xNameQualifier = AssNameId.Attribute("NameQualifier");
                    if (xNameQualifier == null)
                    {
                        throw new ArgumentException("Attributo NameQualifier di NameID mancante.");
                    }
                    string nameQualifier = AssNameId.Attribute("NameQualifier").Value;
                    if (string.IsNullOrEmpty(nameQualifier))
                    {
                        throw new ArgumentException("Valore dell'attributo NameQualifier di NameID non specificato.");
                    }
                    //Recupero e verifica SubjectConfirmation
                    try
                    {
                        XElement xSubjectConfirmation = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmation").Single();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento SubjectConfirmation mancante.");
                    }
                    try
                    {
                        XElement checkConfirmationElement = subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData").Single();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento SubjectConfirmation non specificato.");
                    }


                    try
                    {
                        subjectConfirmationMethod = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmation").Single().Attribute("Method").Value;
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Attributo Method di SubjectConfirmation dell'Assertion mancante..");
                    }

                    //Verifica valore di SubjectConfirmation
                    if (subjectConfirmationMethod.ToLower() != OK_Sub_Method.ToLower())
                    {
                        throw new ArgumentException("Attributo Method di SubjectConfirmation dell'Assertion non corretto.");
                    }
                    //Recupero elemento SubjectConfirmationData
                    if (subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData") == null)
                    {
                        throw new ArgumentException("Elemento SubjectConfirmationData mancante.");
                    }
                    XElement confirmationDataElement = subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData").Single();

                    //Recupero Attributo Recipient di SubjectConfirmationData                    
                    try
                    {
                        string recipientValue = confirmationDataElement.Attribute("Recipient").Value;
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Attributo Recipient di SubjectConfirmationData mancante.");
                    }

                    XAttribute xRecipient = confirmationDataElement.Attribute("Recipient");
                    string recipient = confirmationDataElement.Attribute("Recipient").Value;
                    if (string.IsNullOrEmpty(recipient))
                    {
                        throw new ArgumentException("Attributo Recipient di SubjectConfirmationData non specificato.");
                    }

                    //Recupero elemento Issuer dell'Assertion
                    if (assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer") == null)
                    {
                        throw new ArgumentException("Elemento Issuer dell'Assertion mancante.");
                    }
                    XElement issuerAss = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single();
                    if (issuerAss == null)
                    {
                        throw new ArgumentException("Elemento Issuer dell'Assertion mancante.");
                    }
                    //Verifica valore dell'attributo Format di Issuer
                    XAttribute xFormatAssertionIssuer = issuerAss.Attribute("Format");
                    if (xFormatAssertionIssuer == null)
                    {
                        throw new ArgumentException("Attributo Format di Issuer dell'Assertion mancante.");
                    }
                    string formatIssuer = issuerAss.Attribute("Format").Value;
                    if (string.IsNullOrEmpty(formatIssuer))
                    {
                        throw new ArgumentException("Attributo Format di Issuer dell'Assertion non specificato.");
                    }
                    if (formatIssuer.ToLower() != OK_Ass_Issuer_Format.ToLower())
                    {
                        throw new ArgumentException("Valore dell'attributo Format di Issuer dell'Assertion non corretto.");
                    }
                    //Recupero e verifica dell'attributo InResponseTo di SubjectConfirmationData
                    XAttribute xInResponseToConfirmationData = confirmationDataElement.Attribute("InResponseTo");
                    if (xInResponseToConfirmationData == null)
                    {
                        throw new ArgumentException("Attributo InResponseTo di ConfirmationData mancante.");
                    }

                    subjectConfirmationDataInResponseTo = confirmationDataElement.Attribute("InResponseTo").Value;
                    if (string.IsNullOrEmpty(subjectConfirmationDataInResponseTo))
                    {
                        throw new ArgumentException("Valore dell'attributo InResponseTo di ConfirmationData non specificato.");
                    }
                    //Confronto tra l'Id della Request e InResponseTo di SubjectConfirmationData
                    if (requestID.ToLower() != subjectConfirmationDataInResponseTo.ToLower())
                    {
                        throw new ArgumentException("Il valore di InResponseTo di SubjectConfirmationData non corrisponde all'Id della Request.");
                    }


                    XAttribute xRecipientSubjectConfirmationData = confirmationDataElement.Attribute("Recipient");
                    if (xRecipientSubjectConfirmationData == null)
                    {
                        throw new ArgumentException("Attributo Recipient di SubjectConfirmationData mancante.");
                    }
                    subjectConfirmationDataRecipient = confirmationDataElement.Attribute("Recipient").Value;
                    // Recupero metadata Conditions 
                    XElement conditions = null;
                    try
                    {
                        conditions = assertionElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Conditions").Single();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento Conditions dell'Assertion mancante.");
                    }
                    if (!conditions.HasElements && !conditions.HasAttributes)
                    {
                        throw new ArgumentException("Elemento Conditions dell'Assertion non specificato.");
                    }
                    XElement conditionsElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Conditions").Single();

                    #region CONTROLLI CONDITIONS NotBefore e NonOnOrAfet SPOSTATI ALLA FINE
                    //TODO: CONTROLLI NotBefore E NotOnOrAfter SPOSTATI ALLA FINE
                    ////Recupero e verifica di NotBefore
                    //try
                    //{
                    //    string checkCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    //}
                    //catch
                    //{
                    //    throw new ArgumentException("Attributo NotBefore di Conditions mancante.");
                    //}
                    //string valueCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    //if (string.IsNullOrEmpty(valueCondNotBefore))
                    //{
                    //    throw new ArgumentException("Attributo NotBefore di Conditions non specificato.");
                    //}


                    //conditionsNotBefore = DateTimeOffset.Parse(conditionsElement.Attribute("NotBefore").Value);
                    //string notBefore = conditionsElement.Attribute("NotBefore").Value;
                    ////Verifica formato UTC di NotBefore
                    //match = regex.Match(notBefore);
                    //if (!match.Success)
                    //{
                    //    throw new ArgumentException("Formato dell'attributo NotBefore di Conditions non corretto.");
                    //}
                    //DateTime dtnotBefore = Convert.ToDateTime(notBefore);
                    ////DateTime RespIssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
                    ////NotBefore non deve essere maggiore dell'istante di ricezione della Response
                    //if (dtnotBefore > dtReceipt)
                    //{
                    //    throw new ArgumentException("Valore dell'attributo NotBefore di Conditions maggiore dell'istante di ricezione della Response.");
                    //}

                    //XAttribute xNotOnOrAfterConditions = conditionsElement.Attribute("NotOnOrAfter");
                    //if (xNotOnOrAfterConditions==null)
                    //{
                    //    throw new ArgumentException("Attributo NotOnOrAfter di Conditions mancante.");
                    //}
                    //conditionsNotOnOrAfter = DateTimeOffset.Parse(conditionsElement.Attribute("NotOnOrAfter").Value);
                    //string notOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    ////Verifica formato UTC di NotOnOrAfter
                    //match = regex.Match(notOnOrAfter);
                    //if (!match.Success)
                    //{
                    //    throw new ArgumentException("Formato dell'attributo NotOnOrAfter di Conditions non corretto."); 
                    //}
                    //DateTime dtnotOnOrAfter = Convert.ToDateTime(notOnOrAfter);
                    //if (dtnotOnOrAfter < dtReceipt)//TODO: Verificare
                    //{
                    //    throw new ArgumentException("Valore dell'attributo NotOnOrAfter di Conditions minore dell'istante di ricezione della Response.");
                    //}
                    #endregion CONTROLLI CONDITIONS NotBefore e NonOnOrAfet SPOSTATI ALLA FINE

                    //Recupero e verifica dell'elemento Audience
                    try
                    {
                        string checkAudienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single().Value.Trim();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AudienceRestriction di Conditions mancante.");
                    }
                    XElement xAudienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single();
                    audienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single().Value.Trim();
                    if (string.IsNullOrEmpty(audienceRestriction) && !xAudienceRestriction.HasElements)
                    {
                        throw new ArgumentException("Elemento AudienceRestriction di Conditions non specificato.");
                    }
                    try
                    {
                        string checkAudience = xAudienceRestriction.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Audience").Single().Value.Trim();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento Audience di Conditions mancante.");
                    }
                    audience = xAudienceRestriction.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Audience").Single().Value.Trim();
                    if (string.IsNullOrEmpty(audience))
                    {
                        throw new ArgumentException("Elemento Audience di AudienceRestriction di Conditions non specificato.");
                    }


                    // Recupero metadata AuthnStatement 
                    try
                    {
                        string checkAuthnStatement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single().Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AuthnStatement di Assertion mancante.");
                    }


                    XElement authnStatementElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single();
                    string valueAuthnStatement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single().Value;

                    if (string.IsNullOrEmpty(valueAuthnStatement) && !authnStatementElement.HasElements)
                    {
                        throw new ArgumentException("Elemento AuthnStatement di Assertion non specificato.");
                    }
                    try
                    {
                        string checkAuthnContext = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single().Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AuthnContext di AuthStatement dell'Assertion mancante.");
                    }
                    string valueAuthnContext = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single().Value;
                    if (string.IsNullOrEmpty(valueAuthnContext))
                    {
                        throw new ArgumentException("Elemento AuthnContext di AuthStatement dell'Assertion non specificato.");
                    }
                    XElement authnContextElement = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single();
                    //Recupero e verifica elemento AuthnContextClassRef
                    //che contiene il livello di sicurezza
                    try
                    {
                        string checkAuthnContextClassRef = authnContextElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContextClassRef").Single().Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AuthContextClassRef di AuthnContext di AuthStatement dell'Assertion mancante");
                    }

                    XElement authnContextClassRefElement = authnContextElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContextClassRef").Single();
                    string authnContextClassRefValue = authnContextClassRefElement.Value;
                    //Verifica presenza valore di AuthnContextClassRef
                    if (string.IsNullOrEmpty(authnContextClassRefValue))
                    {
                        throw new ArgumentException("Elemento AuthContextClassRef di AuthnContext di AuthStatement dell'Assertion non specificato.");
                    }
                    //Verifica che il valore di AuthnContextClassRef
                    //corrisponda a uno dei tre ammessi Test 97
                    if (!string.IsNullOrEmpty(authnContextClassRefValue))
                    {
                        if ((authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_1.ToLower()) &&
                            (authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_2.ToLower()) &&
                            (authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_3.ToLower()))
                        {
                            throw new ArgumentException("Il valore di AuthnContextClassRef non corrisponde a nessuno di quelli consentiti.");
                        }
                    }
                    //-------Verifica che il valore di AuthnContextClassRef corrisponda a quello della Request
                    //Test 94-95-96
                    //bool checkLevel = false;
                    //if (!string.IsNullOrEmpty(authnContextClassRefValue))
                    //{

                    //    if (!string.IsNullOrEmpty(requestSecurity))
                    //    {
                    //        if (requestSecurity.IndexOf('|') !=-1)
                    //        {
                    //            string[] splittedSecurity = requestSecurity.Split('|');
                    //            if (splittedSecurity.Length > 0)
                    //            {
                    //                int count = splittedSecurity.Length;
                    //                for (int x = 0; x < count; x++)
                    //                {
                    //                    if (!checkLevel)
                    //                    {
                    //                        if (splittedSecurity[x].ToLower() == authnContextClassRefValue.ToLower())
                    //                        {
                    //                            checkLevel = true;
                    //                            break;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //if (!checkLevel)
                    //{
                    //    throw new ArgumentException("Il valore di AuthnContextClassRef non corrisponde a quello della Request.");
                    //}
                    //-----------------------------------------------------------------------------------------

                    //Recupero e verifica elemento AttributeStatement
                    if (assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement") == null)
                    {
                        throw new ArgumentException("Elemento AttributeStatement di Assertion mancante.");
                    }
                    XElement attributeStatementElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement").Single();
                    XAttribute xAuthnInstant = authnStatementElement.Attribute("AuthnInstant");
                    if (xAuthnInstant == null)
                    {
                        throw new ArgumentException("Attributo AuthnInstant di AttributeStatement mancante.");
                    }
                    authnStatementAuthnInstant = DateTimeOffset.Parse(authnStatementElement.Attribute("AuthnInstant").Value);
                    XAttribute xSessionIndex = authnStatementElement.Attribute("SessionIndex");
                    if (xSessionIndex == null)
                    {
                        throw new ArgumentException("Attributo SessionIndex di AttributeStatement mancante.");
                    }
                    authnStatementSessionIndex = authnStatementElement.Attribute("SessionIndex").Value;
                    string spidUserFields = string.Empty;
                    string attrNameFormat = string.Empty;

                    // Recupero elemento AttributeStatement
                    //e dei nodi Attribute che contengono le informazioni
                    //dell'utente SPID
                    foreach (XElement attribute in xdoc.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement").Elements())
                    {
                        try
                        {
                            string valueAttribute = attribute.Elements().Single(a => a.Name == "{urn:oasis:names:tc:SAML:2.0:assertion}AttributeValue").Value.Trim();
                        }
                        catch (Exception)
                        {

                            throw new ArgumentException("Elemento AttributeStatement presente, ma sottoelemento Attribute non specificato.");
                        }
                        //TODO: Controllo rimosso Test 109
                        //Verifica presenza dell'attributo NameFormat nell'elemnto Attribute
                        //if (attribute.Attribute("NameFormat")==null)
                        //{
                        //    throw new ArgumentException("Attributi senza NameFormat.");
                        //}
                        //attrNameFormat = attribute.Attribute("NameFormat").Value;
                        spidUserInfo.Add(
                            attribute.Attribute("Name").Value,
                            attribute.Elements().Single(a => a.Name == "{urn:oasis:names:tc:SAML:2.0:assertion}AttributeValue").Value.Trim()
                        );
                    }
                    //Confronto tra le informazioni dell'utente SPID Test 103
                    //recuperate e quelle impostate nella Request------------------------------------------------------------------
                    //TODO: Verificare come recuperare dalla configurazione
                    //if (ConfigurationManager.AppSettings["SPID_USER_INFO"] != null)
                    if (!string.IsNullOrEmpty(SPID_USER_INFO))
                    {
                        spidUserFields = SPID_USER_INFO;// ConfigurationManager.AppSettings["SPID_USER_INFO"];
                        string[] spidFieldsSplitted = spidUserFields.Split('|');
                        bool check = false;
                        if (spidFieldsSplitted.Length > 0)
                        {
                            int count = spidFieldsSplitted.Length;
                            if (count != spidUserInfo.Count)
                            {
                                throw new ArgumentException("Numero di Attributi della Response non corispondente alla Request.");
                            }
                            foreach (string f in spidFieldsSplitted)
                            {
                                check = spidUserInfo.ContainsKey(f);
                                if (!check)
                                {
                                    throw new ArgumentException("Attributo della Response non presente o diverso dalla Request.");
                                }
                            }
                        }
                    }
                    //CONTROLLI CONDITIONS NotBefore E NotOnOrAfter
                    //Recupero e verifica di NotBefore
                    //Test 75-76-77-78 - 79-80-81-82
                    try
                    {
                        string checkCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    }
                    catch
                    {
                        throw new ArgumentException("Attributo NotBefore di Conditions mancante.");
                    }
                    string valueCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    if (string.IsNullOrEmpty(valueCondNotBefore))
                    {
                        throw new ArgumentException("Attributo NotBefore di Conditions non specificato.");
                    }
                    conditionsNotBefore = DateTimeOffset.Parse(conditionsElement.Attribute("NotBefore").Value);
                    string notBefore = conditionsElement.Attribute("NotBefore").Value;
                    //Verifica formato UTC di NotBefore
                    match = regex.Match(notBefore);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato dell'attributo NotBefore di Conditions non corretto.");
                    }
                    DateTime dtnotBefore = Convert.ToDateTime(notBefore);
                    //DateTime RespIssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
                    //NotBefore non deve essere maggiore dell'istante di ricezione della Response

                    //TODO: CONTROLLO DA RIPRISTINARE  SUCCESSIVAMENTE
                    //if (dtnotBefore > dtReceipt)
                    //{
                    //    throw new ArgumentException("Valore dell'attributo NotBefore di Conditions maggiore dell'istante di ricezione della Response.");
                    //}

                    //XAttribute xNotOnOrAfterConditions = conditionsElement.Attribute("NotOnOrAfter");
                    try
                    {
                        string checkCondNotOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di Conditions mancante.");
                    }
                    string valueCondNotOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    if (string.IsNullOrEmpty(valueCondNotOnOrAfter))
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di Condition dell'Assertion non specificato.");
                    }

                    conditionsNotOnOrAfter = DateTimeOffset.Parse(conditionsElement.Attribute("NotOnOrAfter").Value);
                    string notOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    //Verifica formato UTC di NotOnOrAfter
                    match = regex.Match(notOnOrAfter);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato dell'attributo NotOnOrAfter di Conditions non corretto.");
                    }
                    DateTime dtnotOnOrAfter = Convert.ToDateTime(notOnOrAfter);
                    if (dtnotOnOrAfter < dtReceipt)//TODO: Verificare
                    {
                        throw new ArgumentException("Valore dell'attributo NotOnOrAfter di Conditions minore dell'istante di ricezione della Response.");
                    }

                    //Test 39-40
                    //39) In Assertion IssueInstant non può essere minore della Request
                    //40) In Assertion IssueInstant non può essere maggiore oltre 5 min della Request
                    //if (request != null)
                    //{
                    DateTime reqIssueInstantASS = Convert.ToDateTime(requestIssueInstant);
                    DateTime IssueInstantAss = Convert.ToDateTime(assertionElement.Attribute("IssueInstant").Value);
                    //TODO: Verificare IssueIstant Assertion
                    if (IssueInstantAss < reqIssueInstantASS)
                    {
                        throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion minore della Request.");
                    }
                    if (IssueInstantAss > reqIssueInstantASS.AddMinutes(5))
                    {
                        throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion maggiore della Request oltre il massimo consentito.");
                    }
                    //}
                    //Recupero e verifica di NotOnOrAfter di SubjectConfirmationData
                    //TODO:CONTROLLI DI NotOnOrAfter di SubjectConfirmationData SPOSTATI ALLA FINE
                    //Test 63-64-65-66
                    XAttribute xNotOnOrAfter = confirmationDataElement.Attribute("NotOnOrAfter");
                    if (xNotOnOrAfter == null)
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di SubjectConfirmationData mancante.");
                    }
                    string ConfDataNotOnOrAfter = confirmationDataElement.Attribute("NotOnOrAfter").Value;
                    if (string.IsNullOrEmpty(ConfDataNotOnOrAfter))
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di SubjectConfirmationData dell'Assertion non specificato. ");
                    }
                    subjectConfirmationDataNotOnOrAfter = DateTimeOffset.Parse(confirmationDataElement.Attribute("NotOnOrAfter").Value);
                    match = regex.Match(ConfDataNotOnOrAfter);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato dell'attributo NotOnOrAfter di SubjectConfirmationData non corretto.");
                    }
                    DateTime dtConfDataNotOnOrAfter = Convert.ToDateTime(ConfDataNotOnOrAfter);
                    if (dtConfDataNotOnOrAfter < dtReceipt)
                    {
                        throw new ArgumentException("Valore dell'attributo NotOnOrAfter di SubjectConfirmationData precedente all'istante di ricezione della Response.");
                    }
                    //------------------------------------------------------------------------------------------------------------------

                }

                return new IdpAuthnResponse(destination, id, inResponseTo, issueInstant, version, issuer,
                                            statusCodeValue, statusCodeInnerValue, statusMessage, statusDetail,
                                            assertionId, assertionIssueInstant, assertionVersion, assertionIssuer,
                                            subjectNameId, subjectConfirmationMethod, subjectConfirmationDataInResponseTo,
                                            subjectConfirmationDataNotOnOrAfter, subjectConfirmationDataRecipient,
                                            conditionsNotBefore, conditionsNotOnOrAfter, audience,
                                            authnStatementAuthnInstant, authnStatementSessionIndex,
                                            spidUserInfo);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format(ErrorMessage, ex.Message), ex);
            }
        }

        public static IdpAuthnResponse GetAuthnResponse_OK(string base64Response, DateTime dtReceipt, out string userError, AuthnRequestType request = null, string idpName = "", string idpCertificate = "")
        {
            const string VALUE_NOT_AVAILABLE = "N/A";
            string idpResponse;
            string OK_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
            string OK_Sub_NameID_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";
            string OK_Sub_Method = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
            string OK_Ass_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
            userError = string.Empty;
            string ErrorMessage = "Impossibile leggere gli attributi AttributeStatement dal documento SAML2. {0}";
            //Verifica base64 Response
            if (String.IsNullOrEmpty(base64Response))
            {
                throw new ArgumentNullException("Il parametro base64Response non può essere nullo o vuoto.");
            }
            //Verifica ID Response
            try
            {
                idpResponse = Encoding.UTF8.GetString(Convert.FromBase64String(base64Response));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Impossibile convertire la risposta base64 in stringa ASCII.", ex);
            }

            try
            {
                // Verifica signature
                XmlDocument xml = new XmlDocument { PreserveWhitespace = true };
                xml.LoadXml(idpResponse);
                if (!XmlSigningHelper.VerifySignature(xml, idpResponse, idpCertificate))
                {
                    throw new Exception("Errore nella verificare la firma della risposta dell'IdP.");
                }

                // Parse XML document
                XDocument xdoc = new XDocument();
                xdoc = XDocument.Parse(idpResponse);

                string destination = VALUE_NOT_AVAILABLE;
                string id = VALUE_NOT_AVAILABLE;
                string inResponseTo = VALUE_NOT_AVAILABLE;
                DateTimeOffset issueInstant = DateTimeOffset.MinValue;
                string version = VALUE_NOT_AVAILABLE;
                string statusCodeValue = VALUE_NOT_AVAILABLE;
                string statusCodeInnerValue = VALUE_NOT_AVAILABLE;
                string statusMessage = VALUE_NOT_AVAILABLE;
                string statusDetail = VALUE_NOT_AVAILABLE;
                string assertionId = VALUE_NOT_AVAILABLE;
                DateTimeOffset assertionIssueInstant = DateTimeOffset.MinValue;
                string assertionVersion = VALUE_NOT_AVAILABLE;
                string assertionIssuer = VALUE_NOT_AVAILABLE;
                string subjectNameId = VALUE_NOT_AVAILABLE;
                string subjectConfirmationMethod = VALUE_NOT_AVAILABLE;
                string subjectConfirmationDataInResponseTo = VALUE_NOT_AVAILABLE;
                DateTimeOffset subjectConfirmationDataNotOnOrAfter = DateTimeOffset.MinValue;
                string subjectConfirmationDataRecipient = VALUE_NOT_AVAILABLE;
                DateTimeOffset conditionsNotBefore = DateTimeOffset.MinValue;
                DateTimeOffset conditionsNotOnOrAfter = DateTimeOffset.MinValue;
                string audience = VALUE_NOT_AVAILABLE;
                string audienceRestriction = VALUE_NOT_AVAILABLE;
                DateTimeOffset authnStatementAuthnInstant = DateTimeOffset.MinValue;
                string authnStatementSessionIndex = VALUE_NOT_AVAILABLE;
                Dictionary<string, string> spidUserInfo = new Dictionary<string, string>();

                //Regex per la verifica di date UTC con e senza millisecondi
                Regex regex = new Regex(@"\d{4}-[01]{1}\d{1}-[0-3]{1}\d{1}T[0-2]{1}\d{1}:[0-6]{1}\d{1}:[0-6]{1}\d{1}(\.\d{3})?Z");
                // Recupero metadata Response
                XElement responseElement = xdoc.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}Response").Single();
                if (responseElement == null)
                {
                    throw new ArgumentException("La Response è null.");
                }

                XAttribute xDestination = responseElement.Attribute("Destination");
                if (xDestination == null)
                {
                    throw new ArgumentException("Attributo Destination mancante.");
                }

                destination = responseElement.Attribute("Destination").Value;
                if (string.IsNullOrEmpty(destination))
                {
                    throw new ArgumentException("Attributo Destination non specificato.");
                }
                //Test 09
                XAttribute xId = responseElement.Attribute("ID");
                if (xId == null)
                {
                    throw new ArgumentException("Attributo ID mancante.");
                }
                //Test 08
                id = responseElement.Attribute("ID").Value;
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentException("Attributo ID non specificato.");
                }
                XAttribute xResponseTo = responseElement.Attribute("InResponseTo");
                if (xResponseTo == null)
                {
                    throw new ArgumentException("Attributo InResponseTo mancante.");
                }
                inResponseTo = responseElement.Attribute("InResponseTo").Value;
                if (string.IsNullOrEmpty(inResponseTo))
                {
                    throw new ArgumentException("Attributo InResponseTo non specificato.");
                }
                //TODO: IssueInstant Response
                //Test 12
                XAttribute xIssueInstant = responseElement.Attribute("IssueInstant");
                if (xIssueInstant == null)
                {
                    throw new ArgumentException("Attributo IssueInstant mancante.");
                }
                //Verifica presenza IssueInstant. Test 11                 
                issueInstant = DateTimeOffset.Parse(responseElement.Attribute("IssueInstant").Value);

                //Recupero valore IssueInstant
                string issueInstantValue = responseElement.Attribute("IssueInstant").Value;
                if (string.IsNullOrEmpty(issueInstantValue))
                {
                    throw new ArgumentException("Attributo IssueInstant non specificato.");
                }
                //Verifica formato UTC IssueInstant Test 13
                //Rimuovo eventuali spazi
                issueInstantValue = issueInstantValue.Replace(" ", "");
                Match match = regex.Match(issueInstantValue);
                if (!match.Success)
                {
                    throw new ArgumentException("Formato IssueInstant della Response non corretto.");
                }
                //Verifica versione (2.0)-Test 10
                XAttribute xVersion = responseElement.Attribute("Version");
                if (xVersion == null)
                {
                    throw new ArgumentException("Attributo Version mancante.");
                }
                version = responseElement.Attribute("Version").Value;
                if ((string.IsNullOrEmpty(version)) || (version != "2.0"))
                {
                    throw new ArgumentException("Attributo Version non specificato o diverso da 2.0.");
                }

                //Recupero nodo Issuer
                XElement issuerElelemnt = null;
                try
                {
                    issuerElelemnt = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single();
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento Issuer mancante.");
                }
                if (issuerElelemnt == null)
                {
                    throw new ArgumentException("Elemento Issuer mancante.");
                }
                //Verifica presenza e valore dell'attributo Format (Issuer)
                //PRECEDENTE FORMA DI CONTROLLO
                //XAttribute xFormat = issuerElelemnt.Attribute("Format");
                //if (xFormat == null)
                //{
                //    throw new ArgumentException("Attributo Format di Issuer mancante.");
                //}
                //string format = issuerElelemnt.Attribute("Format").Value;

                //if (format.ToLower() != OK_Issuer_Format.ToLower())
                //{
                //    throw new ArgumentException("Attributo Format non corretto.");
                //}

                XAttribute xFormat = issuerElelemnt.Attribute("Format");
                if (xFormat != null)
                {
                    string format = issuerElelemnt.Attribute("Format").Value;

                    if (format.ToLower() != OK_Issuer_Format.ToLower())
                    {
                        throw new ArgumentException("Attributo Format non corretto.");
                    }
                }

                //Verifica valore Issuer
                string issuer = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();
                if (string.IsNullOrEmpty(issuer))
                {
                    throw new ArgumentException("Valore Issuer non specificato.");
                }
                //Verifica uguaglianza Issuer rispetto all'idp della Request
                if (issuer.ToLower() != idpName.ToLower())
                {
                    throw new ArgumentException("Valore Issuer non corrisponde all'Idp della Request.");

                }

                // Recupero metdata Status
                XElement StatusElement = null;
                if (responseElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}Status") == null)
                {
                    throw new ArgumentException("Elemento Status della Response mancante.");
                }
                try
                {
                    StatusElement = responseElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}Status").Single();
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento Status mancante.");
                }
                if (StatusElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}StatusCode") == null)
                {
                    throw new ArgumentException("Elemento StatusCode mancante.");
                }
                IEnumerable<XElement> statusCodeElements = StatusElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}StatusCode");

                try
                {
                    statusCodeValue = statusCodeElements.First().Attribute("Value").Value;
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento StatusCode mancante.");
                }

                try
                {
                    statusCodeValue = statusCodeElements.First().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "");
                }
                catch (Exception)
                {

                    throw new ArgumentException("Elemento Status non specificato.");
                }
                statusCodeInnerValue = statusCodeElements.Count() > 1 ? statusCodeElements.Last().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "") : VALUE_NOT_AVAILABLE;
                statusMessage = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusMessage").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;
                statusDetail = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusDetail").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;

                //Verifica errori/anomalie utente
                //Se il volore è diverso da "Success" recupero lo status code
                if (string.IsNullOrEmpty(statusCodeValue))
                {
                    throw new ArgumentException("Elemento StatusCode non specificato.");
                }
                if (statusCodeValue.ToLower() != "success")
                {
                    userError = SPID_ERR_CODE_CUSTOM_18;

                }
                //TODO: Verificare valore IssueInstant Test 14-15
                DateTime IssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
                if (request != null)
                {
                    DateTime reqIssueInstant = Convert.ToDateTime(request.IssueInstant);
                    if (IssueInstant < reqIssueInstant)
                    {
                        throw new ArgumentException("Valore di IssueInstant della Response inferiore alla Request");
                    }
                    if (issueInstant > reqIssueInstant.AddMinutes(5))
                    {
                        throw new ArgumentException("Valore di IssueInstant della Response maggiore alla data di ricezione.");
                    }
                }
                //TODO: Non da eseguire sulla data di ricezione
                //if (issueInstant < dtReceipt)
                //{
                //    throw new ArgumentException("Valore di IssueInstant della Response inferiore alla Request");
                //}
                //if (issueInstant > dtReceipt.AddMinutes(5))
                //{
                //    throw new ArgumentException("Valore di IssueInstant della Response maggiore alla data di ricezione.");
                //}
                //Il valore dello status è Success
                //la Response è corretta inizia la verifica dell'Assertion
                if (statusCodeValue == "Success")
                {
                    // Recupero metadata dell'Assertion 
                    XElement assertionElement = null;
                    try
                    {
                        assertionElement = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Assertion").Single();
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Elemento Assertion della Response mancante.");
                    }
                    if (responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Assertion") == null)
                    {
                        throw new Exception("Elemento Assertion della Response mancante.");
                    }
                    // Verifica della Signature dell'Assertion
                    xml.LoadXml(idpResponse);
                    if (!XmlSigningHelper.VerifyAssertionSignature(xml))
                    {
                        throw new ArgumentException("Impossibile verificare la firma dell'Assertion.");
                    }

                    XAttribute xAssertionId = assertionElement.Attribute("ID");
                    if (xAssertionId == null)
                    {
                        throw new ArgumentException("Attributo ID dell'Assertion mancante.");
                    }
                    assertionId = assertionElement.Attribute("ID").Value;
                    //Verifica Id dell'Assertion
                    if (string.IsNullOrEmpty(assertionId))
                    {
                        throw new ArgumentException("Attributo ID dell'Assertion non specificato.");
                    }
                    //Verifica IssueInstant dell'Assertion
                    XAttribute xAssertionIssueInstant = assertionElement.Attribute("IssueInstant");
                    if (xAssertionIssueInstant == null)
                    {
                        throw new ArgumentException("Attributo IssueInstant dell'Assertion mancante.");
                    }
                    try
                    {
                        assertionIssueInstant = DateTimeOffset.Parse(assertionElement.Attribute("IssueInstant").Value);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Attributo IssueInstant dell'Assertion non specificato.");
                    }

                    string issueAss = assertionElement.Attribute("IssueInstant").Value;
                    //Verifica formato UTC dell'IssueInstant dell'Assertion
                    //Rimuovo eventuali spazi
                    issueAss = issueAss.Replace(" ", "");
                    match = regex.Match(issueAss);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato attributo IssueInstant dell'Assertion non corretto.");
                    }
                    #region CONTROLLI ASSERTION IssueInstant SPOSTATI ALLA FINE
                    //TODO: SPOSTATO ALLA FINE 13/11/2020
                    ////Test 39-40
                    ////39) In Assertion IssueInstant non può essere minore della Request
                    ////40) In Assertion IssueInstant non può essere maggiore oltre 5 min della Request
                    //if (request != null)
                    //{
                    //    DateTime reqIssueInstant = Convert.ToDateTime(request.IssueInstant);
                    //    DateTime IssueInstantAss = Convert.ToDateTime(assertionElement.Attribute("IssueInstant").Value);
                    //    //TODO: Verificare IssueIstant Assertion
                    //    if (IssueInstantAss < reqIssueInstant)
                    //    {
                    //        throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion minore della Request.");
                    //    }
                    //    if (IssueInstantAss > reqIssueInstant.AddMinutes(5))
                    //    {
                    //        throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion maggiore della Request oltre il massimo consentito.");
                    //    }
                    //}
                    #endregion CONTROLLI ASSERTION IssueInstant SPOSTATI ALLA FINE
                    //Verifica versione dell'Assertion (2.0)
                    XAttribute xAssertionVersion = assertionElement.Attribute("Version");
                    if (xAssertionVersion == null)
                    {
                        throw new ArgumentException("Attributo Version dell'Assertion manacante.");
                    }
                    assertionVersion = assertionElement.Attribute("Version").Value;
                    if ((string.IsNullOrEmpty(assertionVersion)) || (assertionVersion != "2.0"))
                    {
                        throw new ArgumentException("Attributo Version dell'Assertion non specificato o diverso da 2.0");
                    }
                    //Verifica presenza Issuer dell'Assertion                    
                    try
                    {
                        assertionIssuer = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento Issuer dell'Assertion mancante.");
                    }
                    assertionIssuer = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();

                    // Recupero metadata Subject
                    try
                    {
                        if (assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Subject").Single() == null)
                        {
                            throw new ArgumentException("Elemento Subject dell'Assertion mancante.");
                        }
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Elemento Subject dell'Assertion mancante.");
                    }
                    XElement subjectElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Subject").Single();
                    if (!subjectElement.HasElements)
                    {
                        throw new ArgumentException("Elemento Subject dell'Assertion non specificato.");
                    }
                    //Verifica NameId Subject                  
                    try
                    {
                        subjectNameId = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}NameID").Single().Value.Trim();
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Elemento NameID di Subject dell'Assertion mancante.");
                    }
                    //Verifica valore SubjectId
                    if (string.IsNullOrEmpty(subjectNameId) || subjectNameId.ToUpper() == "N/A")
                    {
                        throw new ArgumentException("Valore di NameID di Subject non specificato.");
                    }
                    //Recupero e verifica NameId
                    XElement AssNameId = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}NameID").Single();
                    if (AssNameId == null)
                    {
                        throw new ArgumentException("Impossibile recuperare l'elemento NameID di Subject.");
                    }
                    //Recupero e verifica Attributo Format di NameId
                    XAttribute xFormatNameId = AssNameId.Attribute("Format");
                    if (xFormatNameId == null)
                    {
                        throw new ArgumentException("Attributo Format di NameID mancante.");
                    }
                    string formatNameId = AssNameId.Attribute("Format").Value;
                    if (string.IsNullOrEmpty(formatNameId))
                    {
                        throw new ArgumentException("Attributo Format dell'elemento NameID dell'Assertion non specificato.");
                    }
                    if (formatNameId.ToLower() != OK_Sub_NameID_Format.ToLower())
                    {
                        throw new ArgumentException("Valore dell'Attributo Format di NameID non corretto.");
                    }
                    //Recupero e verifica Attributo Format di NameQualifier
                    XAttribute xNameQualifier = AssNameId.Attribute("NameQualifier");
                    if (xNameQualifier == null)
                    {
                        throw new ArgumentException("Attributo NameQualifier di NameID mancante.");
                    }
                    string nameQualifier = AssNameId.Attribute("NameQualifier").Value;
                    if (string.IsNullOrEmpty(nameQualifier))
                    {
                        throw new ArgumentException("Valore dell'attributo NameQualifier di NameID non specificato.");
                    }
                    //Recupero e verifica SubjectConfirmation
                    try
                    {
                        XElement xSubjectConfirmation = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmation").Single();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento SubjectConfirmation mancante.");
                    }
                    try
                    {
                        XElement checkConfirmationElement = subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData").Single();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento SubjectConfirmation non specificato.");
                    }


                    try
                    {
                        subjectConfirmationMethod = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmation").Single().Attribute("Method").Value;
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Attributo Method di SubjectConfirmation dell'Assertion mancante..");
                    }

                    //Verifica valore di SubjectConfirmation
                    if (subjectConfirmationMethod.ToLower() != OK_Sub_Method.ToLower())
                    {
                        throw new ArgumentException("Attributo Method di SubjectConfirmation dell'Assertion non corretto.");
                    }
                    //Recupero elemento SubjectConfirmationData
                    if (subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData") == null)
                    {
                        throw new ArgumentException("Elemento SubjectConfirmationData mancante.");
                    }
                    XElement confirmationDataElement = subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData").Single();

                    //Recupero Attributo Recipient di SubjectConfirmationData                    
                    try
                    {
                        string recipientValue = confirmationDataElement.Attribute("Recipient").Value;
                    }
                    catch (Exception)
                    {

                        throw new ArgumentException("Attributo Recipient di SubjectConfirmationData mancante.");
                    }

                    XAttribute xRecipient = confirmationDataElement.Attribute("Recipient");
                    string recipient = confirmationDataElement.Attribute("Recipient").Value;
                    if (string.IsNullOrEmpty(recipient))
                    {
                        throw new ArgumentException("Attributo Recipient di SubjectConfirmationData non specificato.");
                    }

                    //Recupero elemento Issuer dell'Assertion
                    if (assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer") == null)
                    {
                        throw new ArgumentException("Elemento Issuer dell'Assertion mancante.");
                    }
                    XElement issuerAss = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single();
                    if (issuerAss == null)
                    {
                        throw new ArgumentException("Elemento Issuer dell'Assertion mancante.");
                    }
                    //Verifica valore dell'attributo Format di Issuer
                    XAttribute xFormatAssertionIssuer = issuerAss.Attribute("Format");
                    if (xFormatAssertionIssuer == null)
                    {
                        throw new ArgumentException("Attributo Format di Issuer dell'Assertion mancante.");
                    }
                    string formatIssuer = issuerAss.Attribute("Format").Value;
                    if (string.IsNullOrEmpty(formatIssuer))
                    {
                        throw new ArgumentException("Attributo Format di Issuer dell'Assertion non specificato.");
                    }
                    if (formatIssuer.ToLower() != OK_Ass_Issuer_Format.ToLower())
                    {
                        throw new ArgumentException("Valore dell'attributo Format di Issuer dell'Assertion non corretto.");
                    }
                    //Recupero e verifica dell'attributo InResponseTo di SubjectConfirmationData
                    XAttribute xInResponseToConfirmationData = confirmationDataElement.Attribute("InResponseTo");
                    if (xInResponseToConfirmationData == null)
                    {
                        throw new ArgumentException("Attributo InResponseTo di ConfirmationData mancante.");
                    }

                    subjectConfirmationDataInResponseTo = confirmationDataElement.Attribute("InResponseTo").Value;
                    if (string.IsNullOrEmpty(subjectConfirmationDataInResponseTo))
                    {
                        throw new ArgumentException("Valore dell'attributo InResponseTo di ConfirmationData non specificato.");
                    }
                    //Confronto tra l'Id della Request e InResponseTo di SubjectConfirmationData
                    if (request != null)
                    {
                        if (request.ID.ToLower() != subjectConfirmationDataInResponseTo.ToLower())
                        {
                            throw new ArgumentException("Il valore di InResponseTo di SubjectConfirmationData non corrisponde all'Id della Request.");
                        }
                    }


                    XAttribute xRecipientSubjectConfirmationData = confirmationDataElement.Attribute("Recipient");
                    if (xRecipientSubjectConfirmationData == null)
                    {
                        throw new ArgumentException("Attributo Recipient di SubjectConfirmationData mancante.");
                    }
                    subjectConfirmationDataRecipient = confirmationDataElement.Attribute("Recipient").Value;
                    // Recupero metadata Conditions 
                    XElement conditions = null;
                    try
                    {
                        conditions = assertionElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Conditions").Single();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento Conditions dell'Assertion mancante.");
                    }
                    if (!conditions.HasElements && !conditions.HasAttributes)
                    {
                        throw new ArgumentException("Elemento Conditions dell'Assertion non specificato.");
                    }
                    XElement conditionsElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Conditions").Single();

                    #region CONTROLLI CONDITIONS NotBefore e NonOnOrAfet SPOSTATI ALLA FINE
                    //TODO: CONTROLLI NotBefore E NotOnOrAfter SPOSTATI ALLA FINE
                    ////Recupero e verifica di NotBefore
                    //try
                    //{
                    //    string checkCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    //}
                    //catch
                    //{
                    //    throw new ArgumentException("Attributo NotBefore di Conditions mancante.");
                    //}
                    //string valueCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    //if (string.IsNullOrEmpty(valueCondNotBefore))
                    //{
                    //    throw new ArgumentException("Attributo NotBefore di Conditions non specificato.");
                    //}


                    //conditionsNotBefore = DateTimeOffset.Parse(conditionsElement.Attribute("NotBefore").Value);
                    //string notBefore = conditionsElement.Attribute("NotBefore").Value;
                    ////Verifica formato UTC di NotBefore
                    //match = regex.Match(notBefore);
                    //if (!match.Success)
                    //{
                    //    throw new ArgumentException("Formato dell'attributo NotBefore di Conditions non corretto.");
                    //}
                    //DateTime dtnotBefore = Convert.ToDateTime(notBefore);
                    ////DateTime RespIssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
                    ////NotBefore non deve essere maggiore dell'istante di ricezione della Response
                    //if (dtnotBefore > dtReceipt)
                    //{
                    //    throw new ArgumentException("Valore dell'attributo NotBefore di Conditions maggiore dell'istante di ricezione della Response.");
                    //}

                    //XAttribute xNotOnOrAfterConditions = conditionsElement.Attribute("NotOnOrAfter");
                    //if (xNotOnOrAfterConditions==null)
                    //{
                    //    throw new ArgumentException("Attributo NotOnOrAfter di Conditions mancante.");
                    //}
                    //conditionsNotOnOrAfter = DateTimeOffset.Parse(conditionsElement.Attribute("NotOnOrAfter").Value);
                    //string notOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    ////Verifica formato UTC di NotOnOrAfter
                    //match = regex.Match(notOnOrAfter);
                    //if (!match.Success)
                    //{
                    //    throw new ArgumentException("Formato dell'attributo NotOnOrAfter di Conditions non corretto."); 
                    //}
                    //DateTime dtnotOnOrAfter = Convert.ToDateTime(notOnOrAfter);
                    //if (dtnotOnOrAfter < dtReceipt)//TODO: Verificare
                    //{
                    //    throw new ArgumentException("Valore dell'attributo NotOnOrAfter di Conditions minore dell'istante di ricezione della Response.");
                    //}
                    #endregion CONTROLLI CONDITIONS NotBefore e NonOnOrAfet SPOSTATI ALLA FINE

                    //Recupero e verifica dell'elemento Audience
                    try
                    {
                        string checkAudienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single().Value.Trim();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AudienceRestriction di Conditions mancante.");
                    }
                    XElement xAudienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single();
                    audienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single().Value.Trim();
                    if (string.IsNullOrEmpty(audienceRestriction) && !xAudienceRestriction.HasElements)
                    {
                        throw new ArgumentException("Elemento AudienceRestriction di Conditions non specificato.");
                    }
                    try
                    {
                        string checkAudience = xAudienceRestriction.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Audience").Single().Value.Trim();
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento Audience di Conditions mancante.");
                    }
                    audience = xAudienceRestriction.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Audience").Single().Value.Trim();
                    if (string.IsNullOrEmpty(audience))
                    {
                        throw new ArgumentException("Elemento Audience di AudienceRestriction di Conditions non specificato.");
                    }


                    // Recupero metadata AuthnStatement 
                    try
                    {
                        string checkAuthnStatement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single().Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AuthnStatement di Assertion mancante.");
                    }


                    XElement authnStatementElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single();
                    string valueAuthnStatement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single().Value;

                    if (string.IsNullOrEmpty(valueAuthnStatement) && !authnStatementElement.HasElements)
                    {
                        throw new ArgumentException("Elemento AuthnStatement di Assertion non specificato.");
                    }
                    try
                    {
                        string checkAuthnContext = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single().Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AuthnContext di AuthStatement dell'Assertion mancante.");
                    }
                    string valueAuthnContext = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single().Value;
                    if (string.IsNullOrEmpty(valueAuthnContext))
                    {
                        throw new ArgumentException("Elemento AuthnContext di AuthStatement dell'Assertion non specificato.");
                    }
                    XElement authnContextElement = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single();
                    //Recupero e verifica elemento AuthnContextClassRef
                    //che contiene il livello di sicurezza
                    try
                    {
                        string checkAuthnContextClassRef = authnContextElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContextClassRef").Single().Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Elemento AuthContextClassRef di AuthnContext di AuthStatement dell'Assertion mancante");
                    }

                    XElement authnContextClassRefElement = authnContextElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContextClassRef").Single();
                    string authnContextClassRefValue = authnContextClassRefElement.Value;
                    //Verifica presenza valore di AuthnContextClassRef
                    if (string.IsNullOrEmpty(authnContextClassRefValue))
                    {
                        throw new ArgumentException("Elemento AuthContextClassRef di AuthnContext di AuthStatement dell'Assertion non specificato.");
                    }
                    //Verifica che il valore di AuthnContextClassRef
                    //corrisponda a uno dei tre ammessi Test 97
                    if (!string.IsNullOrEmpty(authnContextClassRefValue))
                    {
                        if ((authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_1.ToLower()) &&
                            (authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_2.ToLower()) &&
                            (authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_3.ToLower()))
                        {
                            throw new ArgumentException("Il valore di AuthnContextClassRef non corrisponde a nessuno di quelli consentiti.");
                        }
                    }
                    //-------Verifica che il valore di AuthnContextClassRef corrisponda a quello della Request
                    //Test 94-95-96
                    LogWriter log = new LogWriter("AVVIO CONTROLLO");
                    bool checkLevel = false;
                    log.LogWrite("Il valore SECURITY SPID DELLA REQUEST: " + authnContextClassRefValue.ToLower());
                    if (!string.IsNullOrEmpty(authnContextClassRefValue))
                    {
                        if (request == null)
                        {
                            log.LogWrite("LA REQUEST E' null.");
                        }
                        if (request != null)
                        {
                            if (request.RequestedAuthnContext == null)
                            {
                                log.LogWrite("RequestedAuthnContext E' null: ");
                            }
                            if (request.RequestedAuthnContext != null)
                            {
                                if (request.RequestedAuthnContext.Items.Length < 0)
                                {
                                    log.LogWrite("RequestedAuthnContext.Items.Length minore di 0 ");
                                }
                                if (request.RequestedAuthnContext.Items.Length == 0)
                                {
                                    log.LogWrite("RequestedAuthnContext.Items.Length = 0 ");
                                }
                                if (request.RequestedAuthnContext.Items.Length > 0)
                                {
                                    int count = request.RequestedAuthnContext.Items.Length;
                                    for (int x = 0; x < count; x++)
                                    {
                                        if (!checkLevel)
                                        {
                                            log.LogWrite("Valore RequestedAuthnContext.Items[" + request.RequestedAuthnContext.Items[x].ToLower() + "]");
                                            if (request.RequestedAuthnContext.Items[x].ToLower() == authnContextClassRefValue.ToLower())
                                            {
                                                checkLevel = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!checkLevel)
                    {
                        throw new ArgumentException("Il valore di AuthnContextClassRef non corrisponde a quello della Request.");
                    }
                    //-----------------------------------------------------------------------------------------

                    //Recupero e verifica elemento AttributeStatement
                    if (assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement") == null)
                    {
                        throw new ArgumentException("Elemento AttributeStatement di Assertion mancante.");
                    }
                    XElement attributeStatementElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement").Single();
                    XAttribute xAuthnInstant = authnStatementElement.Attribute("AuthnInstant");
                    if (xAuthnInstant == null)
                    {
                        throw new ArgumentException("Attributo AuthnInstant di AttributeStatement mancante.");
                    }
                    authnStatementAuthnInstant = DateTimeOffset.Parse(authnStatementElement.Attribute("AuthnInstant").Value);
                    XAttribute xSessionIndex = authnStatementElement.Attribute("SessionIndex");
                    if (xSessionIndex == null)
                    {
                        throw new ArgumentException("Attributo SessionIndex di AttributeStatement mancante.");
                    }
                    authnStatementSessionIndex = authnStatementElement.Attribute("SessionIndex").Value;
                    string spidUserFields = string.Empty;
                    string attrNameFormat = string.Empty;

                    // Recupero elemento AttributeStatement
                    //e dei nodi Attribute che contengono le informazioni
                    //dell'utente SPID
                    foreach (XElement attribute in xdoc.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement").Elements())
                    {
                        try
                        {
                            string valueAttribute = attribute.Elements().Single(a => a.Name == "{urn:oasis:names:tc:SAML:2.0:assertion}AttributeValue").Value.Trim();
                        }
                        catch (Exception)
                        {

                            throw new ArgumentException("Elemento AttributeStatement presente, ma sottoelemento Attribute non specificato.");
                        }
                        //TODO: Controllo rimosso Test 109
                        //Verifica presenza dell'attributo NameFormat nell'elemnto Attribute
                        //if (attribute.Attribute("NameFormat")==null)
                        //{
                        //    throw new ArgumentException("Attributi senza NameFormat.");
                        //}
                        //attrNameFormat = attribute.Attribute("NameFormat").Value;
                        spidUserInfo.Add(
                            attribute.Attribute("Name").Value,
                            attribute.Elements().Single(a => a.Name == "{urn:oasis:names:tc:SAML:2.0:assertion}AttributeValue").Value.Trim()
                        );
                    }
                    //Confronto tra le informazioni dell'utente SPID Test 103
                    //recuperate e quelle impostate nella Request------------------------------------------------------------------
                    //TODO: Verificare come recuperare dalla configurazione
                    //if (ConfigurationManager.AppSettings["SPID_USER_INFO"] != null)
                    if (!string.IsNullOrEmpty (SPID_USER_INFO))
                    {
                        spidUserFields = SPID_USER_INFO;// ConfigurationManager.AppSettings["SPID_USER_INFO"];
                        string[] spidFieldsSplitted = spidUserFields.Split('|');
                        bool check = false;
                        if (spidFieldsSplitted.Length > 0)
                        {
                            int count = spidFieldsSplitted.Length;
                            if (count != spidUserInfo.Count)
                            {
                                throw new ArgumentException("Numero di Attributi della Response non corispondente alla Request.");
                            }
                            foreach (string f in spidFieldsSplitted)
                            {
                                check = spidUserInfo.ContainsKey(f);
                                if (!check)
                                {
                                    throw new ArgumentException("Attributo della Response non presente o diverso dalla Request.");
                                }
                            }
                        }
                    }
                    //CONTROLLI CONDITIONS NotBefore E NotOnOrAfter
                    //Recupero e verifica di NotBefore
                    //Test 75-76-77-78 - 79-80-81-82
                    try
                    {
                        string checkCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    }
                    catch
                    {
                        throw new ArgumentException("Attributo NotBefore di Conditions mancante.");
                    }
                    string valueCondNotBefore = conditionsElement.Attribute("NotBefore").Value;
                    if (string.IsNullOrEmpty(valueCondNotBefore))
                    {
                        throw new ArgumentException("Attributo NotBefore di Conditions non specificato.");
                    }
                    conditionsNotBefore = DateTimeOffset.Parse(conditionsElement.Attribute("NotBefore").Value);
                    string notBefore = conditionsElement.Attribute("NotBefore").Value;
                    //Verifica formato UTC di NotBefore
                    match = regex.Match(notBefore);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato dell'attributo NotBefore di Conditions non corretto.");
                    }
                    DateTime dtnotBefore = Convert.ToDateTime(notBefore);
                    //DateTime RespIssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
                    //NotBefore non deve essere maggiore dell'istante di ricezione della Response
                    if (dtnotBefore > dtReceipt)
                    {
                        throw new ArgumentException("Valore dell'attributo NotBefore di Conditions maggiore dell'istante di ricezione della Response.");
                    }

                    //XAttribute xNotOnOrAfterConditions = conditionsElement.Attribute("NotOnOrAfter");
                    try
                    {
                        string checkCondNotOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di Conditions mancante.");
                    }
                    string valueCondNotOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    if (string.IsNullOrEmpty(valueCondNotOnOrAfter))
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di Condition dell'Assertion non specificato.");
                    }

                    conditionsNotOnOrAfter = DateTimeOffset.Parse(conditionsElement.Attribute("NotOnOrAfter").Value);
                    string notOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
                    //Verifica formato UTC di NotOnOrAfter
                    match = regex.Match(notOnOrAfter);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato dell'attributo NotOnOrAfter di Conditions non corretto.");
                    }
                    DateTime dtnotOnOrAfter = Convert.ToDateTime(notOnOrAfter);
                    if (dtnotOnOrAfter < dtReceipt)//TODO: Verificare
                    {
                        throw new ArgumentException("Valore dell'attributo NotOnOrAfter di Conditions minore dell'istante di ricezione della Response.");
                    }

                    //Test 39-40
                    //39) In Assertion IssueInstant non può essere minore della Request
                    //40) In Assertion IssueInstant non può essere maggiore oltre 5 min della Request
                    if (request != null)
                    {
                        DateTime reqIssueInstant = Convert.ToDateTime(request.IssueInstant);
                        DateTime IssueInstantAss = Convert.ToDateTime(assertionElement.Attribute("IssueInstant").Value);
                        //TODO: Verificare IssueIstant Assertion
                        if (IssueInstantAss < reqIssueInstant)
                        {
                            throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion minore della Request.");
                        }
                        if (IssueInstantAss > reqIssueInstant.AddMinutes(5))
                        {
                            throw new ArgumentException("Valore dell'attributo della IssueInstant dell'Assertion maggiore della Request oltre il massimo consentito.");
                        }
                    }
                    //Recupero e verifica di NotOnOrAfter di SubjectConfirmationData
                    //TODO:CONTROLLI DI NotOnOrAfter di SubjectConfirmationData SPOSTATI ALLA FINE
                    //Test 63-64-65-66
                    XAttribute xNotOnOrAfter = confirmationDataElement.Attribute("NotOnOrAfter");
                    if (xNotOnOrAfter == null)
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di SubjectConfirmationData mancante.");
                    }
                    string ConfDataNotOnOrAfter = confirmationDataElement.Attribute("NotOnOrAfter").Value;
                    if (string.IsNullOrEmpty(ConfDataNotOnOrAfter))
                    {
                        throw new ArgumentException("Attributo NotOnOrAfter di SubjectConfirmationData dell'Assertion non specificato. ");
                    }
                    subjectConfirmationDataNotOnOrAfter = DateTimeOffset.Parse(confirmationDataElement.Attribute("NotOnOrAfter").Value);
                    match = regex.Match(ConfDataNotOnOrAfter);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Formato dell'attributo NotOnOrAfter di SubjectConfirmationData non corretto.");
                    }
                    DateTime dtConfDataNotOnOrAfter = Convert.ToDateTime(ConfDataNotOnOrAfter);
                    if (dtConfDataNotOnOrAfter < dtReceipt)
                    {
                        throw new ArgumentException("Valore dell'attributo NotOnOrAfter di SubjectConfirmationData precedente all'istante di ricezione della Response.");
                    }
                    //------------------------------------------------------------------------------------------------------------------

                }

                return new IdpAuthnResponse(destination, id, inResponseTo, issueInstant, version, issuer,
                                            statusCodeValue, statusCodeInnerValue, statusMessage, statusDetail,
                                            assertionId, assertionIssueInstant, assertionVersion, assertionIssuer,
                                            subjectNameId, subjectConfirmationMethod, subjectConfirmationDataInResponseTo,
                                            subjectConfirmationDataNotOnOrAfter, subjectConfirmationDataRecipient,
                                            conditionsNotBefore, conditionsNotOnOrAfter, audience,
                                            authnStatementAuthnInstant, authnStatementSessionIndex,
                                            spidUserInfo);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format(ErrorMessage, ex.Message), ex);
            }
        }


        #region PRECEDENTE VERIFICA
        //public static IdpAuthnResponse GetAuthnResponse(string base64Response, DateTime dtReceipt, out string userError, AuthnRequestType request = null, string idpName = "")
        //{
        //    const string VALUE_NOT_AVAILABLE = "N/A";
        //    string idpResponse;
        //    string OK_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
        //    string OK_Sub_NameID_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient";
        //    string OK_Sub_Method = "urn:oasis:names:tc:SAML:2.0:cm:bearer";
        //    string OK_Ass_Issuer_Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity";
        //    userError = string.Empty;
        //    //Verifica base64 Response
        //    if (String.IsNullOrEmpty(base64Response))
        //    {
        //        throw new ArgumentNullException("Il parametro base64Response non può essere nullo o vuoto.");
        //    }
        //    //Verifica ID Response
        //    try
        //    {
        //        idpResponse = Encoding.UTF8.GetString(Convert.FromBase64String(base64Response));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException("Impossibile convertire la risposta base64 in stringa ASCII.", ex);
        //    }

        //    try
        //    {
        //        // Verifica signature
        //        XmlDocument xml = new XmlDocument { PreserveWhitespace = true };
        //        xml.LoadXml(idpResponse);
        //        if (!XmlSigningHelper.VerifySignature(xml))
        //        {
        //            throw new Exception("Impossibile verificare la firma della risposta dell'IdP.");
        //        }

        //        // Parse XML document
        //        XDocument xdoc = new XDocument();
        //        xdoc = XDocument.Parse(idpResponse);

        //        string destination = VALUE_NOT_AVAILABLE;
        //        string id = VALUE_NOT_AVAILABLE;
        //        string inResponseTo = VALUE_NOT_AVAILABLE;
        //        DateTimeOffset issueInstant = DateTimeOffset.MinValue;
        //        string version = VALUE_NOT_AVAILABLE;
        //        string statusCodeValue = VALUE_NOT_AVAILABLE;
        //        string statusCodeInnerValue = VALUE_NOT_AVAILABLE;
        //        string statusMessage = VALUE_NOT_AVAILABLE;
        //        string statusDetail = VALUE_NOT_AVAILABLE;
        //        string assertionId = VALUE_NOT_AVAILABLE;
        //        DateTimeOffset assertionIssueInstant = DateTimeOffset.MinValue;
        //        string assertionVersion = VALUE_NOT_AVAILABLE;
        //        string assertionIssuer = VALUE_NOT_AVAILABLE;
        //        string subjectNameId = VALUE_NOT_AVAILABLE;
        //        string subjectConfirmationMethod = VALUE_NOT_AVAILABLE;
        //        string subjectConfirmationDataInResponseTo = VALUE_NOT_AVAILABLE;
        //        DateTimeOffset subjectConfirmationDataNotOnOrAfter = DateTimeOffset.MinValue;
        //        string subjectConfirmationDataRecipient = VALUE_NOT_AVAILABLE;
        //        DateTimeOffset conditionsNotBefore = DateTimeOffset.MinValue;
        //        DateTimeOffset conditionsNotOnOrAfter = DateTimeOffset.MinValue;
        //        string audience = VALUE_NOT_AVAILABLE;
        //        string audienceRestriction = VALUE_NOT_AVAILABLE;
        //        DateTimeOffset authnStatementAuthnInstant = DateTimeOffset.MinValue;
        //        string authnStatementSessionIndex = VALUE_NOT_AVAILABLE;
        //        Dictionary<string, string> spidUserInfo = new Dictionary<string, string>();
        //        //Verifica formato date
        //        Regex regex = new Regex(@"\d{4}-(?:0[1-9]|1[0-2])-(?:0[1-9]|[1-2]\d|3[0-1])T(?:[0-1]\d|2[0-3]):[0-5]\d:[0-5]\d(?:\.\d+|)(?:Z|(?:\+|\-)(?:\d{2}):?(?:\d{2}))");

        //        // Recupero metadata Response
        //        XElement responseElement = xdoc.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}Response").Single();

        //        destination = responseElement.Attribute("Destination").Value;
        //        id = responseElement.Attribute("ID").Value;
        //        inResponseTo = responseElement.Attribute("InResponseTo").Value;

        //        //Verifica presenza IssueInstant 
        //        issueInstant = DateTimeOffset.Parse(responseElement.Attribute("IssueInstant").Value);
        //        if (issueInstant == null)
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (IssueInstant) attributes from SAML2 document.");
        //        }
        //        //Recupero valore IssueInstant
        //        string issue = responseElement.Attribute("IssueInstant").Value;
        //        //Verifica formato UTC IssueInstant
        //        Match match = regex.Match(issue);
        //        if (!match.Success)
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (Format issueInstant) attributes from SAML2 document.");
        //        }
        //        //Verifica versione (2.0)
        //        version = responseElement.Attribute("Version").Value;
        //        if ((string.IsNullOrEmpty(version)) || (version != "2.0"))
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (Version) attributes from SAML2 document.");
        //        }

        //        //Recupero nodo Issuer
        //        XElement issuerElelemnt = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single();
        //        if (issuerElelemnt == null)
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (Issuer) attributes from SAML2 document.");
        //        }
        //        //Verifica presenza e valore dell'attributo Format (Issuer)
        //        string format = issuerElelemnt.Attribute("Format").Value;
        //        if (format.ToLower() != OK_Issuer_Format.ToLower())
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (Issuer Format) attributes from SAML2 document.");
        //        }
        //        //Verifica valore Issuer
        //        string issuer = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();
        //        if (string.IsNullOrEmpty(issuer))
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (Issuer) attributes from SAML2 document.");
        //        }
        //        //Verifica uguaglianza Issuer rispetto all'idp della Request
        //        if (issuer.ToLower() != idpName.ToLower())
        //        {
        //            throw new ArgumentException("Unable to read AttributeStatement (Issuer) attributes from SAML2 document.");

        //        }

        //        // Recupero metdata Status
        //        XElement StatusElement = responseElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}Status").Single();
        //        IEnumerable<XElement> statusCodeElements = StatusElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}StatusCode");
        //        statusCodeValue = statusCodeElements.First().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "");
        //        statusCodeInnerValue = statusCodeElements.Count() > 1 ? statusCodeElements.Last().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "") : VALUE_NOT_AVAILABLE;
        //        statusMessage = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusMessage").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;
        //        statusDetail = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusDetail").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;

        //        //Verifica errori/anomalie utente
        //        //Se il volore è diverso da "Success" recupero lo status code
        //        if (statusCodeValue.ToLower() != "success")
        //        {
        //            userError = statusMessage;

        //        }
        //        //TODO: Verificare valore IssueInstant
        //        DateTime IssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
        //        if (request != null)
        //        {
        //            DateTime reqIssueInstant = Convert.ToDateTime(request.IssueInstant);
        //            if (IssueInstant < reqIssueInstant)
        //            {
        //                throw new ArgumentException("Unable to read Response (IssueInstant) attributes from SAML2 document.");
        //            }
        //        }
        //        if (issueInstant > dtReceipt)
        //        {
        //            throw new ArgumentException("Unable to read Response (IssueInstant) attributes from SAML2 document.");
        //        }
        //        //Il valore dello status è Success
        //        //la Response è corretta inizia la verifica dell'Assertion
        //        if (statusCodeValue == "Success")
        //        {
        //            // Verifica della Signature dell'Assertion
        //            xml.LoadXml(idpResponse);
        //            if (!XmlSigningHelper.VerifyAssertionSignature(xml))
        //            {
        //                throw new Exception("Unable to verify the Assertion signature of the IdP response.");
        //            }

        //            // Recupero metadata dell'Assertion 
        //            XElement assertionElement = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Assertion").Single();
        //            assertionId = assertionElement.Attribute("ID").Value;
        //            //Verifica Id dell'Assertion
        //            if (string.IsNullOrEmpty(assertionId))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion ID) attributes from SAML2 document.");
        //            }
        //            //Verifica IssueInstant dell'Assertion
        //            assertionIssueInstant = DateTimeOffset.Parse(assertionElement.Attribute("IssueInstant").Value);
        //            if (assertionIssueInstant == null)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (IssueInstant) attributes from SAML2 document.");
        //            }
        //            string issueAss = assertionElement.Attribute("IssueInstant").Value;
        //            //Verifica formato UTC dell'IssueInstant dell'Assertion
        //            match = regex.Match(issueAss);
        //            if (!match.Success)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Assertion IssueInstant) attributes from SAML2 document.");
        //            }

        //            //Test 39-40
        //            //39) In Assertion IssueInstant non può essere minore della Request
        //            //40) In Assertion IssueInstant non può essere maggiore oltre 5 min della Request
        //            if (request != null)
        //            {
        //                DateTime reqIssueInstant = Convert.ToDateTime(request.IssueInstant);
        //                DateTime IssueInstantAss = Convert.ToDateTime(assertionElement.Attribute("IssueInstant").Value);
        //                //TODO: Verificare IssueIstant Assertion
        //                if (IssueInstantAss < reqIssueInstant)
        //                {
        //                    throw new ArgumentException("Unable to read AttributeStatement (Format Assertion IssueInstant) attributes from SAML2 document.");
        //                }
        //                if (IssueInstantAss > reqIssueInstant.AddMinutes(5))
        //                {
        //                    throw new ArgumentException("Unable to read AttributeStatement (Format Assertion IssueInstant) attributes from SAML2 document.");
        //                }
        //            }
        //            //Verifica versione dell'Assertion (2.0)
        //            assertionVersion = assertionElement.Attribute("Version").Value;
        //            if ((string.IsNullOrEmpty(assertionVersion)) || (assertionVersion != "2.0"))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion Version) attributes from SAML2 document.");
        //            }
        //            //Verifica presenza Issuer dell'Assertion
        //            assertionIssuer = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();

        //            // Recupero metadata Subject
        //            XElement subjectElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Subject").Single();
        //            //Verifica Id Subject
        //            subjectNameId = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}NameID").Single().Value.Trim();
        //            //Verifica valore SubjectId
        //            if (string.IsNullOrEmpty(subjectNameId) || subjectNameId.ToUpper() == "N/A")
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion Subject NameId) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica NameId
        //            XElement AssNameId = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}NameID").Single();
        //            if (AssNameId == null)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion NameID) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica Attributo Format di NameId
        //            string formatNameId = AssNameId.Attribute("Format").Value;
        //            if (string.IsNullOrEmpty(formatNameId))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion NameID Format) attributes from SAML2 document.");
        //            }
        //            if (formatNameId.ToLower() != OK_Sub_NameID_Format.ToLower())
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion NameID Format) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica Attributo Format di NameQualifier
        //            string nameQualifier = AssNameId.Attribute("NameQualifier").Value;
        //            if (string.IsNullOrEmpty(nameQualifier))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion NameQualifier Format) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica SubjectConfirmation
        //            subjectConfirmationMethod = subjectElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmation").Single().Attribute("Method").Value;
        //            if (string.IsNullOrEmpty(subjectConfirmationMethod))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (SubjectConfirmation) attributes from SAML2 document.");
        //            }
        //            //Verifica valore di SubjectConfirmation
        //            if (subjectConfirmationMethod.ToLower() != OK_Sub_Method.ToLower())
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (SubjectConfirmation) attributes from SAML2 document.");
        //            }
        //            //Recupero elemento SubjectConfirmationData
        //            XElement confirmationDataElement = subjectElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}SubjectConfirmationData").Single();
        //            //Recupero Attributo Recipient di SubjectConfirmationData
        //            string recipient = confirmationDataElement.Attribute("Recipient").Value;
        //            if (string.IsNullOrEmpty(recipient))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion Recipient) attributes from SAML2 document.");
        //            }

        //            //Recupero elemento Issuer dell'Assertion
        //            XElement issuerAss = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single();
        //            if (issuerAss == null)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion Issuer) attributes from SAML2 document.");
        //            }
        //            //Verifica valore dell'attributo Format di Issuer
        //            string formatIssuer = issuerAss.Attribute("Format").Value;
        //            if (string.IsNullOrEmpty(formatIssuer))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion Issuer Format) attributes from SAML2 document.");
        //            }
        //            if (formatIssuer.ToLower() != OK_Ass_Issuer_Format.ToLower())
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Assertion Issuer Format) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica dell'attributo InResponseTo di SubjectConfirmationData
        //            subjectConfirmationDataInResponseTo = confirmationDataElement.Attribute("InResponseTo").Value;
        //            if (string.IsNullOrEmpty(subjectConfirmationDataInResponseTo))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (SubjectConfirmation) attributes from SAML2 document.");
        //            }
        //            //Confronto tra l'Id della Request e InResponseTo di SubjectConfirmationData
        //            if (request != null)
        //            {
        //                if (request.ID.ToLower() != subjectConfirmationDataInResponseTo.ToLower())
        //                {
        //                    throw new ArgumentException("Unable to read AttributeStatement (SubjectConfirmation) attributes from SAML2 document.");
        //                }
        //            }
        //            //Recupero e verifica di NotOnOrAfter di SubjectConfirmationData
        //            subjectConfirmationDataNotOnOrAfter = DateTimeOffset.Parse(confirmationDataElement.Attribute("NotOnOrAfter").Value);
        //            subjectConfirmationDataRecipient = confirmationDataElement.Attribute("Recipient").Value;

        //            // Recupero metadata Conditions 
        //            XElement conditionsElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Conditions").Single();
        //            //Recupero e verifica di NotBefore
        //            conditionsNotBefore = DateTimeOffset.Parse(conditionsElement.Attribute("NotBefore").Value);
        //            string notBefore = conditionsElement.Attribute("NotBefore").Value;
        //            //Verifica formato UTC di NotBefore
        //            match = regex.Match(notBefore);
        //            if (!match.Success)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition NotBefore) attributes from SAML2 document.");
        //            }
        //            DateTime dtnotBefore = Convert.ToDateTime(notBefore);
        //            DateTime RespIssueInstant = Convert.ToDateTime(responseElement.Attribute("IssueInstant").Value);
        //            //NotBefore non deve essere maggiore del valore di IssueInstant della Response
        //            if (dtnotBefore > RespIssueInstant)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition NotBefore) attributes from SAML2 document.");
        //            }

        //            conditionsNotOnOrAfter = DateTimeOffset.Parse(conditionsElement.Attribute("NotOnOrAfter").Value);
        //            string notOnOrAfter = conditionsElement.Attribute("NotOnOrAfter").Value;
        //            //Verifica formato UTC di NotOnOrAfter
        //            match = regex.Match(notOnOrAfter);
        //            if (!match.Success)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition NotOnOrAfter) attributes from SAML2 document.");
        //            }
        //            DateTime dtnotOnOrAfter = Convert.ToDateTime(notOnOrAfter);
        //            if (dtnotOnOrAfter < RespIssueInstant)//TODO: Verificare
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition NotOnOrAfter) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica dell'elemento Audience
        //            audience = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}Audience").Single().Value.Trim();
        //            if (string.IsNullOrEmpty(audience))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition Audience) attributes from SAML2 document.");
        //            }
        //            //Recupero e verifica dell'elemento AudienceRestriction
        //            audienceRestriction = conditionsElement.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AudienceRestriction").Single().Value.Trim();
        //            if (string.IsNullOrEmpty(audienceRestriction))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition AudienceRestriction) attributes from SAML2 document.");
        //            }
        //            // Recupero metadata AuthnStatement 
        //            XElement authnStatementElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnStatement").Single();
        //            //Recupero e verifica elemento AuthnContext
        //            XElement authnContextElement = authnStatementElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContext").Single();
        //            //Recupero e verifica elemento AuthnContextClassRef
        //            //che contiene il livello di sicurezza
        //            XElement authnContextClassRefElement = authnContextElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AuthnContextClassRef").Single();
        //            string authnContextClassRefValue = authnContextClassRefElement.Value;
        //            //Verifica presenza valore di AuthnContextClassRef
        //            if (string.IsNullOrEmpty(authnContextClassRefValue))
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition AuthnContextClassRef) attributes from SAML2 document.");
        //            }
        //            //Verifica che il valore di AuthnContextClassRef
        //            //corrisponda a uno dei tre ammessi
        //            if (!string.IsNullOrEmpty(authnContextClassRefValue))
        //            {
        //                if ((authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_1.ToLower()) &&
        //                    (authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_2.ToLower()) &&
        //                    (authnContextClassRefValue.ToLower() != SamlConsts.SPID_SECURITY_LEVEL_3.ToLower()))
        //                {
        //                    throw new ArgumentException("Unable to read AttributeStatement (Format Condition AuthnContextClassRef) attributes from SAML2 document.");
        //                }
        //            }
        //            //-------Verifica che il valore di AuthnContextClassRef corrisponda a quello della Request
        //            bool checkLevel = false;
        //            if (!string.IsNullOrEmpty(authnContextClassRefValue))
        //            {
        //                if (request != null)
        //                {
        //                    if (request.RequestedAuthnContext != null)
        //                    {
        //                        if (request.RequestedAuthnContext.Items.Length > 0)
        //                        {
        //                            int count = request.RequestedAuthnContext.Items.Length;
        //                            for (int x = 0; x < count; x++)
        //                            {
        //                                if (!checkLevel)
        //                                {
        //                                    if (request.RequestedAuthnContext.Items[x].ToLower() == authnContextClassRefValue.ToLower())
        //                                    {
        //                                        checkLevel = true;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            if (!checkLevel)
        //            {
        //                throw new ArgumentException("Unable to read AttributeStatement (Format Condition AuthnContextClassRef) attributes from SAML2 document.");
        //            }
        //            //-----------------------------------------------------------------------------------------

        //            //Recupero e verifica elemento AttributeStatement
        //            XElement attributeStatementElement = assertionElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement").Single();

        //            authnStatementAuthnInstant = DateTimeOffset.Parse(authnStatementElement.Attribute("AuthnInstant").Value);
        //            authnStatementSessionIndex = authnStatementElement.Attribute("SessionIndex").Value;
        //            string spidUserFields = string.Empty;
        //            string attrNameFormat = string.Empty;

        //            // Recupero elemento AttributeStatement
        //            //e dei nodi Attribute che contengono le informazioni
        //            //dell'utente SPID
        //            foreach (XElement attribute in xdoc.Descendants("{urn:oasis:names:tc:SAML:2.0:assertion}AttributeStatement").Elements())
        //            {
        //                //Verifica presenza dell'attributo NameFormat nell'elemnto Attribute
        //                attrNameFormat = attribute.Attribute("NameFormat").Value;
        //                spidUserInfo.Add(
        //                    attribute.Attribute("Name").Value,
        //                    attribute.Elements().Single(a => a.Name == "{urn:oasis:names:tc:SAML:2.0:assertion}AttributeValue").Value.Trim()
        //                );
        //            }
        //            //Confronto tra le informazioni dell'utente SPID
        //            //recuperate e quelle impostate nella Request------------------------------------------------------------------
        //            if (ConfigurationManager.AppSettings["SPID_USER_INFO"] != null)
        //            {
        //                spidUserFields = ConfigurationManager.AppSettings["SPID_USER_INFO"];
        //                string[] spidFieldsSplitted = spidUserFields.Split('|');
        //                bool check = false;
        //                if (spidFieldsSplitted.Length > 0)
        //                {

        //                    int count = spidFieldsSplitted.Length;
        //                    if (count != spidUserInfo.Count)
        //                    {
        //                        throw new ArgumentException("Unable to read AttributeStatement attributes from SAML2 document.");
        //                    }
        //                    foreach (string f in spidFieldsSplitted)
        //                    {
        //                        check = spidUserInfo.ContainsKey(f);
        //                        if (!check)
        //                        {
        //                            throw new ArgumentException("Unable to read AttributeStatement attributes from SAML2 document.");
        //                        }
        //                    }
        //                }
        //            }
        //            //------------------------------------------------------------------------------------------------------------------

        //        }

        //        return new IdpAuthnResponse(destination, id, inResponseTo, issueInstant, version, issuer,
        //                                    statusCodeValue, statusCodeInnerValue, statusMessage, statusDetail,
        //                                    assertionId, assertionIssueInstant, assertionVersion, assertionIssuer,
        //                                    subjectNameId, subjectConfirmationMethod, subjectConfirmationDataInResponseTo,
        //                                    subjectConfirmationDataNotOnOrAfter, subjectConfirmationDataRecipient,
        //                                    conditionsNotBefore, conditionsNotOnOrAfter, audience,
        //                                    authnStatementAuthnInstant, authnStatementSessionIndex,
        //                                    spidUserInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException("Unable to read AttributeStatement attributes from SAML2 document.", ex);
        //    }
        //}

        #endregion PRECEDENTE VERIFICA

        /// <summary>
        /// Check the validity of IdP authentication response
        /// </summary>
        /// <param name="idpAuthnResponse"></param>
        /// <param name="spidRequestId"></param>
        /// <param name="route"></param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool ValidAuthnResponse(IdpAuthnResponse idpAuthnResponse, string spidRequestId, string route)
        {
            return (idpAuthnResponse.InResponseTo == "_" + spidRequestId) && (idpAuthnResponse.SubjectConfirmationDataRecipient == route);
        }

        /// <summary>
        /// Build a signed SAML logout request.
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="destination"></param>
        /// <param name="consumerServiceURL"></param>
        /// <param name="certificate"></param>
        /// <param name="identityProvider"></param>
        /// <param name="subjectNameId"></param>
        /// <param name="authnStatementSessionIndex"></param>
        /// <returns></returns>
        public static string BuildLogoutPostRequest(string uuid, string consumerServiceURL, X509Certificate2 certificate,
                                                        IdentityProvider identityProvider, string subjectNameId, string authnStatementSessionIndex)
        {
            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ArgumentNullException("The uuid parameter can't be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(consumerServiceURL))
            {
                throw new ArgumentNullException("The consumerServiceURL parameter can't be null or empty.");
            }

            if (certificate == null)
            {
                throw new ArgumentNullException("The certificate parameter can't be null.");
            }

            if (identityProvider == null)
            {
                throw new ArgumentNullException("The identityProvider parameter can't be null.");
            }

            if (string.IsNullOrWhiteSpace(subjectNameId))
            {
                throw new ArgumentNullException("The subjectNameId parameter can't be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(identityProvider.SingleLogoutServiceUrl))
            {
                throw new ArgumentNullException("The LogoutServiceUrl of the identity provider is null or empty.");
            }

            DateTime now = DateTime.UtcNow;

            LogoutRequestType logoutRequest = new LogoutRequestType
            {
                ID = "_" + uuid,
                Version = "2.0",
                IssueInstant = identityProvider.Now(now),
                Destination = identityProvider.EntityID,
                Issuer = new NameIDType
                {
                    Value = consumerServiceURL.Trim(),
                    Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:entity",
                    NameQualifier = consumerServiceURL
                },
                Item = new NameIDType
                {
                    NameQualifier = consumerServiceURL,
                    Format = "urn:oasis:names:tc:SAML:2.0:nameid-format:transient",
                    Value = identityProvider.SubjectNameIdFormatter(subjectNameId)
                },
                NotOnOrAfterSpecified = true,
                NotOnOrAfter = now.AddMinutes(10),
                Reason = "urn:oasis:names:tc:SAML:2.0:logout:user",
                SessionIndex = new string[] { authnStatementSessionIndex }
            };

            try
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");
                ns.Add("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");

                StringWriter stringWriter = new StringWriter();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = true,
                    Encoding = Encoding.UTF8
                };

                XmlWriter responseWriter = XmlTextWriter.Create(stringWriter, settings);
                XmlSerializer responseSerializer = new XmlSerializer(logoutRequest.GetType());
                responseSerializer.Serialize(responseWriter, logoutRequest, ns);
                responseWriter.Close();

                string samlString = stringWriter.ToString();
                stringWriter.Close();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(samlString);

                XmlElement signature = XmlSigningHelper.SignXMLDoc(doc, certificate, "_" + uuid);
                doc.DocumentElement.InsertBefore(signature, doc.DocumentElement.ChildNodes[1]);

                return Convert.ToBase64String(Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + doc.OuterXml));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the IdP Logout Response and extract metadata to the returned DTO class
        /// </summary>
        /// <param name="base64Response"></param>
        /// <returns></returns>
        public static IdpLogoutResponse GetLogoutResponse(string base64Response)
        {
            const string VALUE_NOT_AVAILABLE = "N/A";
            string idpResponse;

            if (String.IsNullOrEmpty(base64Response))
            {
                throw new ArgumentNullException("The base64Response parameter can't be null or empty.");
            }

            try
            {
                idpResponse = Encoding.UTF8.GetString(Convert.FromBase64String(base64Response));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to converto base64 response to ascii string.", ex);
            }

            try
            {
                // Verify signature
                XmlDocument xml = new XmlDocument { PreserveWhitespace = true };
                xml.LoadXml(idpResponse);
                if (!XmlSigningHelper.VerifySignature(xml))
                {
                    throw new Exception("Unable to verify the signature of the IdP response.");
                }

                // Parse XML document
                XDocument xdoc = new XDocument();
                xdoc = XDocument.Parse(idpResponse);

                string destination = VALUE_NOT_AVAILABLE;
                string id = VALUE_NOT_AVAILABLE;
                string inResponseTo = VALUE_NOT_AVAILABLE;
                DateTimeOffset issueInstant = DateTimeOffset.MinValue;
                string version = VALUE_NOT_AVAILABLE;
                string statusCodeValue = VALUE_NOT_AVAILABLE;
                string statusCodeInnerValue = VALUE_NOT_AVAILABLE;
                string statusMessage = VALUE_NOT_AVAILABLE;
                string statusDetail = VALUE_NOT_AVAILABLE;

                // Extract response metadata
                XElement responseElement = xdoc.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}LogoutResponse").Single();
                destination = responseElement.Attribute("Destination").Value;
                id = responseElement.Attribute("ID").Value;
                inResponseTo = responseElement.Attribute("InResponseTo").Value;
                issueInstant = DateTimeOffset.Parse(responseElement.Attribute("IssueInstant").Value);
                version = responseElement.Attribute("Version").Value;

                // Extract Issuer metadata
                string issuer = responseElement.Elements("{urn:oasis:names:tc:SAML:2.0:assertion}Issuer").Single().Value.Trim();

                // Extract Status metadata
                XElement StatusElement = responseElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}Status").Single();
                IEnumerable<XElement> statusCodeElements = StatusElement.Descendants("{urn:oasis:names:tc:SAML:2.0:protocol}StatusCode");
                statusCodeValue = statusCodeElements.First().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "");
                statusCodeInnerValue = statusCodeElements.Count() > 1 ? statusCodeElements.Last().Attribute("Value").Value.Replace("urn:oasis:names:tc:SAML:2.0:status:", "") : VALUE_NOT_AVAILABLE;
                statusMessage = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusMessage").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;
                statusDetail = StatusElement.Elements("{urn:oasis:names:tc:SAML:2.0:protocol}StatusDetail").SingleOrDefault()?.Value ?? VALUE_NOT_AVAILABLE;

                return new IdpLogoutResponse(destination, id, inResponseTo, issueInstant, version, issuer,
                                             statusCodeValue, statusCodeInnerValue, statusMessage, statusDetail);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to read AttributeStatement attributes from SAML2 document.", ex);
            }
        }

        /// <summary>
        /// Check the validity of IdP logout response
        /// </summary>
        /// <param name="idpLogoutResponse"></param>
        /// <param name="spidRequestId"></param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool ValidLogoutResponse(IdpLogoutResponse idpLogoutResponse, string spidRequestId)
        {
            return (idpLogoutResponse.InResponseTo == "_" + spidRequestId);
        }

    }
}
