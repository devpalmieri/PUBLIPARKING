using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Token
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Key { get; set; }

        public int ExpirationInDays { get; set; }
    }
}
