using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_tipo_nucleo_immobiliare.Metadata))]
    public partial class anagrafica_tipo_nucleo_immobiliare : ISoftDeleted
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

            [DisplayName("ID")]
            public int id_tipo_nucleo_immobiliare { get; set; }
            [DisplayName("Codice Tipo Nucleo Immobiliare")]
            public string cod_tipo_nucleo_immobiliare { get; set; }
            [Required]
            [DisplayName("Descrizione Tipo Nucleo Immobiliare")]
            public string desc_tipo_nucleo_immobiliare { get; set; }

        }
    }
}
