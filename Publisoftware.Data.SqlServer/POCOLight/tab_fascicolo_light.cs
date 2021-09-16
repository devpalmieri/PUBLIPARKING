using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_fascicolo_light : BaseEntity<tab_fascicolo_light>
    {
        public int id_fascicolo { get; set; }
        public string identificativo_avv_pag { get; set; }
        public string identificativo_doc_input { get; set; }
        public string contribuente { get; set; }
        //public bool IsCopiaRicorsoImmagine { get; set; }
        //public bool IsDomandaConciliazioneImmagine { get; set; }
        //public bool IsCopiaProcuraImmagine { get; set; }
        //public bool IsAttoPignoramentoImmagine { get; set; }
        //public bool IsComunicazioneProcConcorsualeImmagine { get; set; }
        //public bool IsDomandaAmmPassivoImmagine { get; set; }
        //public bool IsCopiaSentenzaImmagine { get; set; }
    }
}
