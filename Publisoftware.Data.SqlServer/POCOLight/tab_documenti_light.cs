using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_documenti_light
    {
        public int id_tab_documenti { get; set; }
        public string num_documento { get; set; }
        public string descr_ente_rilascio_documento { get; set; }
        public string data_rilascio_documento_String { get; set; }
        public string data_validita_documento_da_String { get; set; }
        public string data_validita_documento_a_String { get; set; }
        public DateTime? data_rilascio_documento { get; set; }
        public DateTime? data_validita_documento_da { get; set; }
        public DateTime? data_validita_documento_a { get; set; }
        public string color { get; set; }
        public string tipo_documento { get; set; }
    }
}
