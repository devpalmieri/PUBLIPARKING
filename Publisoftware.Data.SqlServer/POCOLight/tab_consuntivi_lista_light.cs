using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_consuntivi_lista_light
    {
        public string altro_ente_desc { get; set; }
        public string cod_voce_bilancio { get; set; }
        public string descr_voce_bilancio { get; set; }
        public string voce_contrib { get; set; }
        public string anno_emissione { get; set; }
        public string anno_rif { get; set; }
        public decimal imp_da { get; set; }
        public decimal imp_a { get; set; }
        public decimal imp_diff { get; set; }
        public string numero_accertamento_contabile { get; set; }
        public int ordine { get; set; }
    }
}
