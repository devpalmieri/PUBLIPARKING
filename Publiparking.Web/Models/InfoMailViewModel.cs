using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models
{
    public class InfoMailViewModel
    {
        public Nullable<int> Id_Oggetto { get; set; }
        [Required(ErrorMessage = "Indirizzo email obbligatorio.")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Formato email non valido")]
        public string Mail { get; set; }

        public DateTime DataInvio { get; set; }
        public bool EsitoInvio { get; set; }
        [Required(ErrorMessage = "Messaggio obbligatorio.")]
        public string Messaggio { get; set; }
        public string EsitoMessage { get; set; }
        public string Oggetto
        {
            get
            {
                if (Id_Oggetto == 1)
                {
                    return "Informazioni";
                }
                else if (Id_Oggetto == 2)
                {
                    return "Richiesta";
                }
                else if (Id_Oggetto == 3)
                {
                    return "Critica";
                }
                else if (Id_Oggetto == 4)
                {
                    return "Lavoro";
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
