using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_tassonomia_pagopa_light : BaseEntity<tab_tassonomia_pagopa>
    {
        public int Id_tassonomia { get; set; }
        public int Id_tipo_ente { get; set; }
        public int Id_entrata { get; set; }
        public string Codice_tassonomia_pagopa { get; set; }
        public DateTime data_inizio_validita { get; set; }
        public DateTime data_fine_validita { get; set; }
    }
}
