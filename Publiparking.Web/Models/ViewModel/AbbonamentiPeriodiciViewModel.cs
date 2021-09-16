using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Publiparking.Web.Models.ViewModel
{
    public class AbbonamentiPeriodiciViewModel
    {
        [Display(Name = "Id")]
        public int IdAbbonamentoPeriodico { get; set; }
        [Display(Name = "Codice")]
        [Required]
        [StringLength(20)]
        public string codice { get; set; }
        [Display(Name = "Targa")]
        [Required]
        [StringLength(50)]
        public string targa { get; set; }
        [Display(Name = "Tariffa")]
        public int idTariffaAbbonamento { get; set; }
        [Display(Name = "Valido Dal")]
        [Column(TypeName = "datetime")]
        public DateTime validoDal { get; set; }
        [Display(Name = "valido Al")]
        [Column(TypeName = "datetime")]
        public DateTime? validoAl { get; set; }
        [Display(Name = "Cognome")]
        [Required]
        [StringLength(100)]
        public string cognome { get; set; }
        [Display(Name = "Nome")]
        [Required]
        [StringLength(100)]
        public string nome { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }
        [Display(Name = "E-Mail")]
        [Required]
        [StringLength(30)]
        public string email { get; set; }
        [StringLength(1)]
        public string flag_on_off { get; set; }

        public List<TariffeAbbonamenti> TariffeAbbonamenti { get; set; }
    }

}
