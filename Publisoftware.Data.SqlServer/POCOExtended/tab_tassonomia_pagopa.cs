using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data
{
    [MetadataTypeAttribute(typeof(tab_tipi_diritti.Metadata))]
    public partial class tab_tassonomia_pagopa
    {
       
        internal sealed class Metadata
        {
            private Metadata()
            {
            }

            [DisplayName("Id")]
            public int Id_tassonomia { get; set; }

            [DisplayName("Tipo Ente")]
            public int Id_tipo_ente { get; set; }

            [DisplayName("Id Entrata")]
            public int Id_entrata { get; set; }
            
            [DisplayName("Codice Tassonomia")]
            public string Codice_tassonomia_pagopa { get; set; }

            [DisplayName("Data Inizio Validità")]
            public DateTime data_inizio_validita { get; set; }
            [DisplayName("Data Fine Validità")]
            public DateTime data_fine_validita { get; set; }


        }
    }
}
