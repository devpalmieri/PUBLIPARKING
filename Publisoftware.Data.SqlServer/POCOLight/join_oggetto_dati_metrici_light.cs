using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class join_oggetto_dati_metrici_light : BaseEntity<join_oggetto_dati_metrici_light>
    {
        public int id_join_oggetto_catasto { get; set; }
        public int? id_dati_metrici_catastali { get; set; }
        public int? id_dati_metrici_dichiarati { get; set; }
        public string Categoria { get; set; }
        public string Superficie { get; set; }
        public string Foglio { get; set; }
        public string Numero { get; set; }
        public string Particella { get; set; }
        public string Subalterno { get; set; }
    }
}
