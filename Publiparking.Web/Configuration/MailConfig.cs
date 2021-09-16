using Publiparking.Core.Data.SqlServer;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Configuration
{
    public class MailConfig
    {

        public string Mail { get; set; }
        public string MailBCC { get; set; }
        public string Password { get; set; }
        public string IsMailAbilitata { get; set; }
        public string ServerPosta { get; set; }

    }
}
