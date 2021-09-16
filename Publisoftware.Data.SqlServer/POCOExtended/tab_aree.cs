using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_aree.Metadata))]
    public partial class tab_aree : ISoftDeleted
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

            [Required]
            [DisplayName("ID")]
            public int id_area { get; set; }

            [Required]
            [DisplayName("Id Zona")]
            public int id_zona { get; set; }

            [Required]
            [DisplayName("Cod Area")]
            public int cod_area { get; set; }

            [Required]
            [DisplayName("Id Toponimo")]
            public int id_toponimo { get; set; }

            
            [DisplayName("Civico Da")]
            public int civico_da { get; set; }

            
            [DisplayName("Sigla Da")]
            [StringLength(2)]
            public string sigla_da { get; set; }

            [DisplayName("Civico a")]
            public int civico_a { get; set; }


            [DisplayName("Sigla a")]
            [StringLength(2)]
            public string sigla_a { get; set; }
            
            [DisplayName("Pari-Dispari")]
            [StringLength(2)]
            public string p_d { get; set; }
            
            [DisplayName("Frazione")]
            public string frazione { get; set; }


        }
    }
}
