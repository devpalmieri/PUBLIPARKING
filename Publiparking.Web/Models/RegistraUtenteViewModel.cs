using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models
{
    public class RegistraUtenteViewModel
    {
        public bool isUserWeb { get; set; }

        [Required(ErrorMessage = "Inserire una UserName")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Formato UserName non valido")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Inserire una password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confermare la password")]
        [Compare("Password", ErrorMessage = "La password e la conferma della password non coincidono.")]
        [Display(Name = "Conferma Password")]
        public string passwordConferma { get; set; }

        [Required(ErrorMessage = "Inserire il codice fiscale")]
        [RegularExpression("[a-zA-Z]{6}[a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{2}[a-zA-Z][a-zA-Z0-9]{3}[a-zA-Z]", ErrorMessage = "Formato Codice Fiscale non valido")]
        [Display(Name = "Codice Fiscale")]
        public string codFiscalePIVA { get; set; }

        [Required(ErrorMessage = "Inserire il nome")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Inserire il cognome")]
        [Display(Name = "Cognome")]
        public string cognome { get; set; }
        [Display(Name = "Ragione Sociale")]
        public string ragioneSociale { get; set; }

        //[EmailAddress]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Formato email non valido")]
        [Required(ErrorMessage = "Inserire un indirizzo email")]
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Nazionalità")]
        public string nazionalita { get; set; }

        [RegularExpression("[0-9]{0,8}", ErrorMessage = "Formato non valido")]
        [Display(Name = "Prefisso Mobile")]
        public string prefissoTelMobile { get; set; }

        [RegularExpression("[0-9]{0,14}", ErrorMessage = "Formato non valido")]
        [Display(Name = "Mobile")]
        public string TelMobile { get; set; }

        [RegularExpression("[0-9]{0,8}", ErrorMessage = "Formato non valido")]
        [Display(Name = "Prefisso Fisso")]
        public string prefissoTelFisso { get; set; }

        [RegularExpression("[0-9]{0,14}", ErrorMessage = "Formato non valido")]
        [Display(Name = "Telefono")]
        public string TelFisso { get; set; }

        //[Required(ErrorMessage = "Selezionare un ente per il riconoscimento")]
        //[Range(1, int.MaxValue, ErrorMessage = "Selezionare un ente per il riconoscimento")]
        //public int selEnteId { get; set; }

        //public List<anagrafica_ente> listEnte { get; set; }

        [Required(ErrorMessage = "Inserire la prima parte del codice del contribuente a 6 cifre")]
        //[RegularExpression("([1-9][0-9]*)", ErrorMessage = "Il codice del contribuente deve essere un numero positivo")]
        [RegularExpression("(^[0-9]{6})", ErrorMessage = "Inserire codice numerico a 6 cifre")]
        public string codiceEnte { get; set; }
       
        [Display(Name = "Domanda Segreta")]
        public int selDomandaSegretaId { get; set; }

        public List<tab_domande_segrete> listDomandaSegreta { get; set; }

        [Required(ErrorMessage = "Inserire una risposta segreta")]
        [Display(Name = "Risposta Segreta")]
        public string rispostaSegreta { get; set; }
        [Display(Name = "Sesso")]
        public int id_sesso { get; set; }

        [Display(Name = "Nazione di Nascita")]
        public string stato_nas { get; set; }
        public int? id_stato { get; set; }
        [Display(Name = "Comune di Nascita")]
        public string comune_nas { get; set; }

        public int? cod_comune_nas { get; set; }
        [Display(Name = "Data di Nascita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_nas { get; set; }
        [Display(Name = "Data di Nascita")]
        public string data_nas_String
        {
            get
            {
                if (data_nas.HasValue)
                {
                    return data_nas.Value.ToShortDateString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    data_nas = DateTime.Parse(value);
                }
                else
                {
                    data_nas = null;
                }
            }
        }
    }
}
