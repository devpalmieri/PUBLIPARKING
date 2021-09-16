using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_doc_output_light : BaseEntity<tab_doc_output_light>
    {
        public int id_tab_doc_output { get; set; }
        public string DataEmissione { get; set; }
        public string statoEsito { get; set; }
        public string statoEsitoIstanza { get; set; }
        public string identificativo_doc_output { get; set; }
        public string contribuente_nominativo { get; set; }
        public int id_tab_doc_input { get; set; }
        public int id_tab_avv_pag { get; set; }
    }
}
