using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    
    public enum PeriodicitaEnum
    {
        [Display(Name = "Nessuna")]
        NO,
        [Display(Name = "Giornaliera")]
        GIO,
        [Display(Name = "Settimanale")]
        SET,
        [Display(Name = "Quindicinale")]
        QUI,
        [Display(Name = "Mensile")]
        MEN,
        [Display(Name = "Bimestrale")]
        BIM,
        [Display(Name = "Trimestrale")]
        TRI,
        [Display(Name = "Semestrale")]
        SEM,
        [Display(Name = "Annuale")]
        ANN
    }

    public enum PeriodicitaRateEnum
    {
        [Display(Name = "Nessuna")]
        Nessuna = 0,
        [Display(Name = "Mensile")]
        Mensile = 1,
        [Display(Name = "Bimestrale")]
        Bimestrale = 2,
        [Display(Name = "Trimestrale")]
        Trimestrale = 3,
        [Display(Name = "Semestrale")]
        Semestrale = 6
    }
}
