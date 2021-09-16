using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_sezioni_stampa.Metadata))]
    public partial class anagrafica_sezioni_stampa
    {
        public static int ID_TESTATA = 11;

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_sezione_stampa { get; set; }

            [Required]
            [DisplayName("Nome")]
            public string nome_sezione { get; set; }

            [Required]
            [DisplayName("Descrizione")]
            public string descrizione { get; set; }

            [Required]
            [DisplayName("Livello")]
            public string flag_livello_dato { get; set; }
        }
    }

}
