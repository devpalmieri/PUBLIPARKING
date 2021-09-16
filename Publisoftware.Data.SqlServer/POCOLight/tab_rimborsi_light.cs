using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_rimborsi_light : BaseEntity<tab_rimborsi_light>
    {
        public int id_tab_rimborsi { get; set; }
        public int id_tab_doc_input { get; set; }
        public string data_creazione_disposizione_rimborso { get; set; }
        public string data_presentazione { get; set; }
        public string nominativo_rag_soc_beneficiario { get; set; }
        public string iban_beneficiario_bonifico { get; set; }
        public decimal imp { get; set; }
        public string importo { get; set; }
        public string tipo_rimborso { get; set; }
    }
}
