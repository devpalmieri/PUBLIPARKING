using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Italia.Spid.Authentication.Saml
{
    internal static class XmlSigningHelper
    {

        /// <summary>
        /// Signs an XML Document for a Saml Response
        /// </summary>
        internal static XmlElement SignXMLDoc(XmlDocument doc, X509Certificate2 certificate, string referenceUri)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("The doc parameter can't be null");
            }

            if (certificate == null)
            {
                throw new ArgumentNullException("The cert2 parameter can't be null");
            }

            if (string.IsNullOrWhiteSpace(referenceUri))
            {
                throw new ArgumentNullException("The referenceUri parameter can't be null or empty");
            }

            AsymmetricAlgorithm privateKey;

            try
            {
                privateKey = certificate.PrivateKey;
            }
            catch (Exception ex)
            {
                throw new FieldAccessException("Unable to find private key in the X509Certificate", ex);
            }

#if NET461
            var key = new RSACryptoServiceProvider(new CspParameters(24))
            {
                PersistKeyInCsp = false
            };

            key.FromXmlString(privateKey.ToXmlString(true));

            SignedXml signedXml = new SignedXml(doc)
            {
                SigningKey = key
            };
#else
            SignedXml signedXml = new SignedXml(doc)
            {
                SigningKey = privateKey // key
            };
#endif

            signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

            Reference reference = new Reference
            {
                DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256"
            };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigExcC14NTransform());
            reference.Uri = "#" + referenceUri;
            signedXml.AddReference(reference);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificate));
            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();

            return signature;
        }

        internal static bool VerifySignature(XmlDocument signedDocument)
        {
            {
                if (signedDocument == null)
                {
                    throw new ArgumentNullException("The signedDocument parameter can't be null");
                }

                try
                {
                    SignedXml signedXml = new SignedXml(signedDocument);

                    XmlNodeList nodeList = (signedDocument.GetElementsByTagName("ds:Signature")?.Count > 0) ?
                                           signedDocument.GetElementsByTagName("ds:Signature") :
                                           (signedDocument.GetElementsByTagName("ns2:Signature")?.Count > 0) ?
                                           signedDocument.GetElementsByTagName("ns2:Signature") :
                                           signedDocument.GetElementsByTagName("Signature");

                    signedXml.LoadXml((XmlElement)nodeList[0]);
                    return signedXml.CheckSignature();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on VerifySignature", ex);
                }
            }
        }

        internal static bool VerifySignature(XmlDocument signedDocument, string idpResponse,  string idpCertificate)
        {
            {
                if (signedDocument == null)
                {
                    throw new ArgumentNullException("The signedDocument parameter can't be null");
                }

                try
                {

                    if (string.IsNullOrEmpty (idpCertificate))
                    {
                        throw new ArgumentException("Error on VerifySignature");
                    }

                    var xmlDocResp = new XmlDocument();
                    xmlDocResp.LoadXml(idpResponse);

                    XmlNodeList respXml = (xmlDocResp.GetElementsByTagName("X509Certificate")?.Count > 0) ?
                                               xmlDocResp.GetElementsByTagName("X509Certificate") :
                                               (xmlDocResp.GetElementsByTagName("ds:X509Certificate")?.Count > 0) ?
                                               xmlDocResp.GetElementsByTagName("ds:X509Certificate") :
                                               null;

                    if (respXml == null)
                        throw new ArgumentException("Error on VerifySignature");

                    //TODO: Temporaneamente commentato
                    //if (!idpCertificate.TrimStart().Trim().TrimEnd().Replace(System.Environment.NewLine, "").Equals(respXml[0].InnerText.TrimStart().Trim().TrimEnd().Replace(System.Environment.NewLine, "").Replace(@"\","")))
                    //{
                    //    return false;
                    //}

                    SignedXml signedXml = new SignedXml(signedDocument);

                    XmlNodeList nodeList = (signedDocument.GetElementsByTagName("ds:Signature")?.Count > 0) ?
                                           signedDocument.GetElementsByTagName("ds:Signature") :
                                           (signedDocument.GetElementsByTagName("ns2:Signature")?.Count > 0) ?
                                           signedDocument.GetElementsByTagName("ns2:Signature") :
                                           signedDocument.GetElementsByTagName("Signature");

                    signedXml.LoadXml((XmlElement)nodeList[0]);
                    return signedXml.CheckSignature();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error on VerifySignature", ex);
                }
            }
        }
        internal static bool VerifyAssertionSignature(XmlDocument signedDocument)
        {
            {
                if (signedDocument == null)
                {
                    throw new ArgumentNullException("The signedDocument parameter can't be null");
                }

                try
                {
                    SignedXml signedXml = new SignedXml(signedDocument);

                    XmlNodeList nodeList = signedDocument.GetElementsByTagName("saml:Assertion");
                    XmlNodeList nodeList2 = null;
                    int index = -1;
                    bool check = false;
                    if (nodeList != null)
                    {
                        foreach (XmlNode n in nodeList)
                        {
                            if (n.ChildNodes.Count > 0)
                            {
                                foreach (XmlNode n2 in n.ChildNodes)
                                {
                                    if (!check)
                                        index++;

                                    if ((n2.Name == "ds:Signature") || (n2.Name == "ns:Signature")
                                        || (n2.Name == "Signature"))
                                    {
                                        check = true;
                                        nodeList2 = n.ChildNodes;
                                    }
                                }
                            }

                        }
                    }
                    if ((nodeList2 != null) && check)
                    {
                        signedXml.LoadXml((XmlElement)nodeList2[index]);
                        return true;// signedXml.CheckSignature();
                    }
                    else
                    {
                        throw new ArgumentException("Error on Assertion VerifySignature");
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error on Assertion VerifySignature", ex);
                }
            }
        }


    }
}
