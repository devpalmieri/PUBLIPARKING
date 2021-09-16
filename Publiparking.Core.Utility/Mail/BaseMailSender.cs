using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Reflection;
using System.Text;

namespace Publiparking.Core.Utility
{
    public class BaseMailSender : IMailSender
    {
        private SmtpClient _client;

        public BaseMailSender(string SmtpHost, string Mail, string Password) { SetSmtpHost(SmtpHost, Mail, Password); }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
        }

        private void SetSmtpHost(string SmtpHost, string Mail, string Password)
        {
            _client = new SmtpClient(SmtpHost, 25);
            _client.UseDefaultCredentials = false;

            _client.Credentials = new NetworkCredential(Mail, Password);
        }

        public void sendMailHtml(string pFrom, string pA, string pCC, string pBCC, string pOggetto, string pBodyTitle, string pBody, string p_templateHtml = "",
            Stream p_imgLogo = null)
        {
            List<string> pAList = getAdressAsList(pA);
            List<string> pCCList = getAdressAsList(pCC);
            List<string> pBCCList = getAdressAsList(pBCC);

            sendMailHtml(pFrom, pAList, pCCList, pBCCList, pOggetto, pBodyTitle, pBody, p_templateHtml, p_imgLogo);
        }

        public void sendMail(string pFrom, string pA, string pCC, string pBCC, string pOggetto, string pBodyTitle, string pBody)
        {
            List<string> pAList = getAdressAsList(pA);
            List<string> pCCList = getAdressAsList(pCC);
            List<string> pBCCList = getAdressAsList(pBCC);

            sendMail(pFrom, pAList, pCCList, pBCCList, pOggetto, pBodyTitle, pBody);
        }

        public void sendMail(string pFrom, List<string> pA, List<string> pCC, List<string> pBCC, string pOggetto, string pBodyTitle, string pBody)
        {
            string wBodyMail = getBody(pBodyTitle, pBody);
            MailMessage wMail = new MailMessage();
            wMail.From = new MailAddress(pFrom);
            wMail.Subject = pOggetto;
            wMail.Body = wBodyMail;

            foreach (string pMailAddress in pA)
            {
                if (isValidMailAddress(pMailAddress))
                {
                    wMail.To.Add(new MailAddress(pMailAddress));
                }
            }

            if (wMail.To.Count <= 0)
            {
                throw new Exception("No recipients.");
            }

            foreach (string pMailAddress in pCC)
            {
                if (isValidMailAddress(pMailAddress))
                {
                    wMail.CC.Add(new MailAddress(pMailAddress));
                }
            }

            foreach (string pMailAddress in pBCC)
            {
                if (isValidMailAddress(pMailAddress))
                {
                    wMail.Bcc.Add(new MailAddress(pMailAddress));
                }
            }

            wMail.IsBodyHtml = true;

            _client.Send(wMail);
        }

        //Sandro 08/03/2019 
        // Pietro 08/03/2021 aggiunti Dispose
        public void sendMailHtml(string pFrom, List<string> pA, List<string> pCC, List<string> pBCC, string pOggetto, string pBodyTitle, string pBody,
            string p_templateHtml = "", Stream p_imgLogo = null)
        {
            Stream stream = null;

            try
            {
                using (MailMessage wMail = new MailMessage())
                {
                    string v_htmltext = string.Empty;
                    wMail.From = new MailAddress(pFrom);
                    wMail.Subject = pOggetto;
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    if (p_imgLogo == null)
                    {
                        stream = assembly.GetManifestResourceStream("Publisoftware.Utility.Mail.LogoMail.Logo-publiservizi_small.png");
                    }
                    else
                    {
                        stream = p_imgLogo;
                    }

                    using (LinkedResource LinkedImage = new LinkedResource(stream))
                    {
                        LinkedImage.ContentId = "PubliLogo";
                        LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                        if (p_templateHtml.Length == 0)
                        {
                            using (Stream streamhtml = assembly.GetManifestResourceStream("Publisoftware.Utility.Mail.Template.emailtemplate.html"))
                            {
                                using (var reader = new StreamReader(streamhtml))
                                {
                                    v_htmltext = reader.ReadToEnd();
                                }
                            }
                        }
                        else
                        {
                            v_htmltext = p_templateHtml;
                        }

                        v_htmltext = v_htmltext.Replace("PubliBody", pBody);

                        using (AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                            v_htmltext,
                            Encoding.UTF8, MediaTypeNames.Text.Html))
                        {
                            htmlView.LinkedResources.Add(LinkedImage);
                            wMail.AlternateViews.Add(htmlView);


                            foreach (string pMailAddress in pA)
                            {
                                if (isValidMailAddress(pMailAddress))
                                {
                                    wMail.To.Add(new MailAddress(pMailAddress));
                                }
                            }

                            if (wMail.To.Count <= 0)
                            {
                                throw new Exception("No recipients.");
                            }

                            foreach (string pMailAddress in pCC)
                            {
                                if (isValidMailAddress(pMailAddress))
                                {
                                    wMail.CC.Add(new MailAddress(pMailAddress));
                                }
                            }

                            foreach (string pMailAddress in pBCC)
                            {
                                if (isValidMailAddress(pMailAddress))
                                {
                                    wMail.Bcc.Add(new MailAddress(pMailAddress));
                                }
                            }

                            wMail.IsBodyHtml = true;

                            _client.Send(wMail);
                        }
                    }
                }
            }
            finally
            {
                stream?.Dispose();
            }
        }

        private List<string> getAdressAsList(string pStringList)
        {
            List<string> wList = new List<string>();

            if (pStringList != null && pStringList != string.Empty)
            {
                string[] wAddress = pStringList.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string wAdd in wAddress)
                {
                    wList.Add(wAdd);
                }
            }

            return wList;
        }

        private bool isValidMailAddress(string pMailAddress)
        {
            //if (pMailAddress != null && pMailAddress != string.Empty)
            //{
            //    Regex regex = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //    Match match = regex.Match(pMailAddress);
            //    if (match.Success)
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return true;
        }

        protected virtual string getBody(string pBodyTitle, string pBody)
        {
            return
                "<html>" +
                    "<head>" +
                        "<style type=\"text/css\">" +
                            "td" +
                            "{" +
                                "border: 1px solid black;" +
                                "padding-left: 10px;" +
                                "padding-right: 10px;" +
                                "padding-top: 10px;" +
                                "padding-bottom: 10px;" +
                                "font-size: 11pt;" +
                            "}" +
                            "table" +
                            "{" +
                                "width: 100%;" +
                            "}" +
                        "</style>" +
                    "</head>" +
                    "<body>" +
                    "<table cellpadding=''0'' cellspacing=''0'' border=''0''>" +
                        "<tr>" +
                            "<td width=\"150\" style=\"border-bottom:none; border-right:none; padding-right:0\">" +
                            "</td>" +
                            "<td style=\"border-bottom:none; border-left:none; color:#16518f; text-align:center; font-weight:bold; padding-left:0\">" +
                                pBodyTitle +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan=\"2\" style=\"padding:20; border-bottom:none; padding-bottom:10\">" +
                                pBody +
                                "<br />" +
                            "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td colspan=\"2\" style=\"padding: 0; border-top:none; border-bottom:none\">" +
                                "<hr />" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                "</body>" +
            "</html>";
        }

        //bisogna integrare la parte degli attach in sendMail. Provvisoriamente ho creato una nuova funzione
        public void sendMailWithAttach(string pFrom, string pA, string pCC, string pBCC, string pOggetto, string pBodyTitle, string pBody,
            List<string> pFileNameAttach, Boolean v_boxed = true)
        {
            List<string> pAList = getAdressAsList(pA);
            List<string> pCCList = getAdressAsList(pCC);
            List<string> pBCCList = getAdressAsList(pBCC);

            sendMailWithAttach(pFrom, pAList, pCCList, pBCCList, pOggetto, pBodyTitle, pBody, pFileNameAttach, v_boxed);
        }

        // Pietro 08/03/2021 aggiunti Dispose
        public void sendMailWithAttach(string pFrom, List<string> pA, List<string> pCC, List<string> pBCC, string pOggetto, string pBodyTitle, string pBody,
            List<string> pFileNameAttach, Boolean v_boxed = true)
        {
            string wBodyMail = pBody;

            if (v_boxed)
            {
                wBodyMail = getBody(pBodyTitle, pBody);
            }

            using (MailMessage wMail = new MailMessage())
            {
                wMail.From = new MailAddress(pFrom);
                wMail.Subject = pOggetto;
                wMail.Body = wBodyMail;

                foreach (string pMailAddress in pA)
                {
                    if (isValidMailAddress(pMailAddress))
                    {
                        wMail.To.Add(new MailAddress(pMailAddress));
                    }
                }

                if (wMail.To.Count <= 0)
                {
                    throw new Exception("No recipients.");
                }

                foreach (string pMailAddress in pCC)
                {
                    if (isValidMailAddress(pMailAddress))
                    {
                        wMail.CC.Add(new MailAddress(pMailAddress));
                    }
                }

                foreach (string pMailAddress in pBCC)
                {
                    if (isValidMailAddress(pMailAddress))
                    {
                        wMail.Bcc.Add(new MailAddress(pMailAddress));
                    }
                }

                foreach (string attachmentFilename in pFileNameAttach)
                {
                    if (attachmentFilename != null)
                    {
                        using (Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet))
                        {
                            ContentDisposition disposition = attachment.ContentDisposition;
                            disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                            disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                            disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                            disposition.FileName = Path.GetFileName(attachmentFilename);
                            disposition.Size = new FileInfo(attachmentFilename).Length;
                            disposition.DispositionType = DispositionTypeNames.Attachment;
                            wMail.Attachments.Add(attachment);
                        }
                    }
                }

                if (v_boxed)
                {
                    wMail.IsBodyHtml = true;
                }

                _client.Send(wMail);
            }
        }
    }
}