using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_lista_voci_contribuzione_light : BaseEntity<join_lista_voci_contribuzione_light>
    {
        public int id_join_lista_voci_contribuzione { get; set; }
        public string TipoVoceContribuzione { get; set; }
        public string numero_accertamento_contabile { get; set; }
        public string data_accertamento_contabile_String { get; set; }
    }
}
