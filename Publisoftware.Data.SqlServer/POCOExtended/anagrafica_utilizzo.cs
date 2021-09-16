using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_utilizzo.Metadata))]
    public partial class anagrafica_utilizzo : ISoftDeleted
    {
        public static int ALTRO_ID = 5000;

        public bool IsSoftDeletable
        {
            get { return true; }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_utilizzo { get; set; }

            [DisplayName("Descrizione")]
            public string des_utilizzo { get; set; }

            [DisplayName("Flag Occupazione")]
            [RegularExpression("[a-zA-Z0-9]{1}", ErrorMessage = "Formato non valido: 1 carattere ammesso")]
            public string flag_occupazione { get; set; }

            [DisplayName("Flag Proprietà")]
            [RegularExpression("[a-zA-Z0-9]{1}", ErrorMessage = "Formato non valido: 1 carattere ammesso")]
            public string flag_proprieta { get; set; }

            [DisplayName("Flag Tipo Pubblicità")]
            [RegularExpression("[a-zA-Z0-9]{1}", ErrorMessage = "Formato non valido: 1 carattere ammesso")]
            public string flag_tipo_pubblicita { get; set; }
        }
    }
}
