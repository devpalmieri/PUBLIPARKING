using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_oggetti_contribuzione_light : BaseEntity<tab_oggetti_contribuzione_light>
    {
        public decimal id_oggetto_contribuzione { get; set; }
        public decimal id_oggetto { get; set; }
        public string Ubicazione { get; set; }
        public string DataVariazione { get; set; }
        public string PeriodoContribuzione { get; set; }
        public string data_inizio_contribuzione_String { get; set; }
        public string descrizioneCategoria { get; set; }
        public string rendita { get; set; }
        public string possesso { get; set; }
        public string numTotOccupanti { get; set; }
        public string stato { get; set; }
        public string matrContatore { get; set; }
        public string numFacce { get; set; }
        public string dim { get; set; }
        public string sup { get; set; }
        public string tipo { get; set; }
        public string denuncia { get; set; }
    }
}
