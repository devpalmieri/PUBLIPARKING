using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_codici_tributi_ministeriali.Metadata))]
    public partial class tab_codici_tributi_ministeriali : ISoftDeleted
    {
        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_tributo_ministeriale { get; set; }

            [Required]
            [RegularExpression("[A-Za-z]{1,10}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string codice_tributo_ministeriale { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [Required]
            [RegularExpression("[0-1]{1,1}", ErrorMessage = "Formato non valido")]
            [DisplayName("Flag Spesa")]
            public string flag_spesa { get; set; }
        }
    }
}
