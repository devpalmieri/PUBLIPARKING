using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.Account
{
    public class RoleModel
    {
        public string ID { get; set; }
        [Display(Name = "Role Name")]
        public string Role { get; set; }
    }
}
