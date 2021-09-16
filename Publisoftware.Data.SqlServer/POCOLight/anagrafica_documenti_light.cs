using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_documenti_light : BaseEntity<anagrafica_documenti_light>
    {
        public int id_anagrafica_doc { get; set; }
        public string descrizione_doc { get; set; }
        public bool IsPresente { get; set; }
        public bool IsAsseverazione { get; set; }
        public bool IsSchemaPreliminareControdeduzioni { get; set; }
    }
}