using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Utility
{
    public interface IMailSender : IDisposable
    {
        void sendMail(string pFrom, string pA, string pCC, string pBCC, string pOggetto, string pBodyTitle, string pBody);
        void sendMail(string pFrom, List<string> pA, List<string> pCC, List<string> pBCC, string pOggetto, string pBodyTitle, string pBody);
        void sendMailHtml(string pFrom, string pA, string pCC, string pBCC, string pOggetto, string pBodyTitle, string pBody, string p_templateHtml = "", Stream p_imgLogo = null);
        void sendMailHtml(string pFrom, List<string> pA, List<string> pCC, List<string> pBCC, string pOggetto, string pBodyTitle, string pBody, string p_templateHtml = "", Stream p_imgLogo = null);
        void sendMailWithAttach(string pFrom, string pA, string pCC, string pBCC, string pOggetto, string pBodyTitle, string pBody, List<string> pFileNameAttach, Boolean v_boxed = true);
        void sendMailWithAttach(string pFrom, List<string> pA, List<string> pCC, List<string> pBCC, string pOggetto, string pBodyTitle, string pBody, List<string> pFileNameAttach, Boolean v_boxed = true);
    }
}
