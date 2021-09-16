using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.oAuth2
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
