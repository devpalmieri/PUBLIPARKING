using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Publisoftware.Data
{
    public enum ModalitaOperativaEnum
    {
        [Display(Name="Front Office")]
        FrontOffice = 'F',
        [Display(Name = "Back Office")]
        BackOffice = 'B',
        [Display(Name = "Base")]
        ALL = 'A'
    }
}