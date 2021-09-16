using Publisoftware.Data.CustomValidationAttrs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    //// Abilitare per la gestione dei controlli nelle View
    [MetadataTypeAttribute(typeof(join_entrate_macroentrate.Metadata))]
    public partial class join_entrate_macroentrate
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_join_entrate_macroentrate { get; set; }
            [DisplayName("Macro entrata")]
            [Required]
            public int id_tab_macroentrate { get; set; }
            [DisplayName("Entrata")]
            [Required]
            public int id_entrata { get; set; }
            [DisplayName("Da")]
            public Nullable<System.DateTime> da { get; set; }
            [DisplayName("A")]
            [IsDateAfter("da", true, ErrorMessage = "La data di fine deve essere maggiore della data di inizio")]
            public Nullable<System.DateTime> a { get; set; }
        }
    }
}
