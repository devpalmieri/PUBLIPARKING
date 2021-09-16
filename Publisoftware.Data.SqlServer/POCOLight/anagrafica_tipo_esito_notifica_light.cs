using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_tipo_esito_notifica_light : BaseEntity<anagrafica_tipo_esito_notifica_light>
    {
        public int id_tipo_esito_notifica { get; set; }
        public string descr_tipo_esito_notifica { get; set; }
        public string flag_esito { get; set; }
        public string fl_not_ok { get; set; }
        public string fl_not_nok { get; set; }
    }
}
