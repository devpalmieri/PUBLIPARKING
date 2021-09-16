using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_ente_codici.Metadata))]
    public partial class anagrafica_ente_codici : ISoftDeleted
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

            [DisplayName("Ente")]
            public int id_ente { get; set; }

            [DisplayName("Codice Ente")]
            [RegularExpression("[A-Za-z]{2}", ErrorMessage = "Formato Codice non valido (Es: AB)")]
            public string codice_ente { get; set; }

        }
    }
}
