using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.Account
{
    public class UserProfileViewModel
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Attivo")]
        public string Attivo { get; set; }
        [Required]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Cognome")]
        public string Cognome { get; set; }
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        public string CFPIva { get; set; }
        [Display(Name = "Ragione Sociale")]
        public string RagSociale { get; set; }

    }
}
