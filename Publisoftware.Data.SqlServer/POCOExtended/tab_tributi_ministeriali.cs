using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tributi_ministeriali.Metadata))]
    public partial class tab_tributi_ministeriali
    {
        public const string CODICE_TRIBUTO_TASSA_AUTO = "713T";
        public const string CODICE_TRIBUTO_TASSA_AUTO_INTERESSI = "713I";
        public const string CODICE_TRIBUTO_TASSA_AUTO_SANZIONI = "713S";

        public string CodiceDescrizione
        {
            get
            {
                return codice_tributo + " / " + descrizione;
            }
        }

        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("ID")]
            public int id_tab_tributi_ministeriali { get; set; }

            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [Required]
            [DisplayName("Codice Tributo")]
            public string codice_tributo { get; set; }

            [Required]
            [DisplayName("Descrizione Tributo")]
            public string descrizione { get; set; }

            [Required]
            [Range(1, Int32.MaxValue, ErrorMessage = "E' necessario selezionare una voce di contribuzione")]
            [DisplayName("Voce di Contribuzione")]
            public int id_anagrafica_voce_contribuzione { get; set; }
        }
    }
}
