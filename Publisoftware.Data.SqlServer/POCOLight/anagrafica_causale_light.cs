using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_causale_light : BaseEntity<anagrafica_causale_light>
    {
        public int id_causale { get; set; }
        public string cod_causale { get; set; }
        public string descr_causale { get; set; }
        public string flag_trattamento { get; set; }
        public string sigla_tipo_causale { get; set; }
        public bool isButtonAbilitato { get; set; }
        public string messaggio { get; set; }
        public bool isButtonAbilitatoSanzioni { get; set; }
        public string messaggioSanzioni { get; set; }
    }
}
