using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Configuration
{
    public class SpidConfig
    {
        public string VerificaSpid { get; set; }
        public string SpidLoginDestination { get; set; }
        public string SpidLogoutDestination { get; set; }
        public string IdpMetadataListUrl { get; set; }
        public string SpidCertificateName { get; set; }
        public string SpidUserInfo { get; set; }
        public string SpidDomainValue { get; set; }
        public string SpidEnvironment { get; set; }
        public string SpidCookie { get; set; }
    }
}
