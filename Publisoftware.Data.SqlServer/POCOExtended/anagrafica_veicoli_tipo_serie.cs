using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(anagrafica_veicoli_tipo_serie.Metadata))]
    public partial class anagrafica_veicoli_tipo_serie : ISoftDeleted
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
            public int id_tipo_serie { get; set; }

            [DisplayName("Marca")]
            public int id_marca { get; set; }

            [Required]
            [DisplayName("Tipo")]
            public string tipo { get; set; }

            [Required]
            [DisplayName("Serie")]
            public string serie { get; set; }
        }
    }
}
