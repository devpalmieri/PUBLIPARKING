using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_causali_estr_conto.Metadata))]
    public partial class tab_causali_estr_conto
    {
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int id_causale_estr_conto { get; set; }

            [DisplayName("ABI CC")]
            [StringLength(10)]
            public string abi_cc { get; set; }
            [Required]
            [DisplayName("Codice")]
            [StringLength(5)]
            public string codice_causale { get; set; }
            [Required]
            [DisplayName("Descrizione")]
            [StringLength(100)]
            public string descrizione_causale { get; set; }
            [Required]
            [DisplayName("Flag Spesa")]
            [RegularExpression("[0-1]{1}", ErrorMessage = "Formato non valido: 0 o 1")]
            public string fl_spesa { get; set; }
        }
    }
}