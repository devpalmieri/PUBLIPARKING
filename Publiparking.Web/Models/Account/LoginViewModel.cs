using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    //public class SendCodeViewModel
    //{
    //    public string SelectedProvider { get; set; }
    //    public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    //    public string ReturnUrl { get; set; }
    //    public bool RememberMe { get; set; }
    //}

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Codice")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Memorizzare questo browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
    }

    public class AccountViewModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }

        public ICollection<RoleModel> Roles { get; set; }
    }

    public class LoginViewModel
    {


        //[Required]
        //[Display(Name = "Authentication Type")]
        ////public string grant_type { get; set; }
        string _grant = "password";
        [DefaultValue("password")]
        public string grant_type
        {
            get
            {
                return _grant;
            }
            set
            {
                _grant = value;
            }
        }

        [Required(ErrorMessage = "Username è obbligatorio.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Codice Fiscale")]
        [Required(ErrorMessage = "Inserire il Codice Fiscale o la P. IVA")]
        [RegularExpression("[a-zA-Z]{6}[a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{3}[a-zA-Z]|[0-9]{11}", ErrorMessage = "Formato Codice Fiscale/P. IVA non valido.")]
        public string codFiscalePIVA { get; set; }

        [Display(Name = "Tipo Ente")]
        [Required(ErrorMessage = "Selezionare il tipo Ente")]
        [Range(1, int.MaxValue, ErrorMessage = "Selezionare il tipo Ente")]
        public int selected_id_tipo_ente { get; set; }

        [Display(Name = "Seleziona l'Ente")]
        [Required(ErrorMessage = "Selezionare l'Ente")]
        [Range(1, int.MaxValue, ErrorMessage = "Selezionare l'Ente")]
        public int selEnteId { get; set; }

        public List<anagrafica_ente> listEnte { get; set; }
        public string Descrizione_Ente { get; set; }
        public string Url_Ente { get; set; }
        public List<tab_tipo_ente> listTipoEnte { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        string _role = "Operator";
        [Required]
        [Display(Name = "UserRoles")]
        [DefaultValue("Operator")]
        public string UserRoles
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }
        //public string UserRoles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "La password e la password di conferma non corrispondono.")]
        public string ConfirmPassword { get; set; }
    }
    public class RegisterAdminViewModel
    {
        [Key]
        [Required]
        [Display(Name = "ID User")]
        public string ID { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "La password e la password di conferma non corrispondono.")]
        public string ConfirmPassword { get; set; }
    }
    public class ListUserViewModel
    {
        [Key]
        [Required]
        [Display(Name = "ID User")]
        public string ID { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La lunghezza di {0} deve essere di almeno {2} caratteri.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Conferma password")]
        [Compare("Password", ErrorMessage = "La password e la password di conferma non corrispondono.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Posta elettronica")]
        public string Email { get; set; }
    }
}
