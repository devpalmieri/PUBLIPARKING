using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipo_input_output_funzione.Metadata))]
    public partial class tab_tipo_input_output_funzione : ISoftDeleted
    {
        public static int ISTANZA = 9;
        public static int RICORSO = 13;

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
            public int id_tipo_input_output_funzione { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z]{1,6}", ErrorMessage = "Formato non valido")]
            [DisplayName("Codice")]
            public string cod_tipo_input_output_funzione { get; set; }

            [Required]
            [RegularExpression(@"^[\w\s]{1,100}$", ErrorMessage = "Formato non valido")]
            [DisplayName("Descrizione")]
            public string desc_tipo_input_output_funzione { get; set; }
        }
    }
}
