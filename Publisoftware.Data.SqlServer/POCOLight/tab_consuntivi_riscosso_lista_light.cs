using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_consuntivi_riscosso_lista_light
    {
        public string descr_ente_riv { get; set; }
        public string cod_voce_bilancio { get; set; }
        public string voce_contrib { get; set; }

        public string anno_riscossione { get; set; }
        public string anno_emissione { get; set; }
        public string anno_rif { get; set; }
        public decimal totale_imponibile_a { get; set; }
        public decimal totale_iva_a { get; set; }
        public decimal totale_imponibile_da { get; set; }
        public decimal totale_iva_da { get; set; }
        public decimal totale_imponibile_diff { get; set; }
        public string numero_accertamento_contabile { get; set; }
        public string data_accertamento_contabile { get; set; }
        public int ordine { get; set; }
    }
}
