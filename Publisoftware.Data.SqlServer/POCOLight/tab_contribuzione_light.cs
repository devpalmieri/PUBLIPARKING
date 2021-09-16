using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_contribuzione_light
    {
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public string IdentificativoAttoPropedeutico { get; set; }
        public string IdentificativoArticoloCollegato { get; set; }
        public decimal ImportoResiduo { get; set; }
        public string ImportoResiduo_String { get; set; }
        public int PrioritaPagamento { get; set; }
    }
}
