using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_sentenze_light : BaseEntity<tab_sentenze_light>
    {
        public int id_tab_sentenze { get; set; }
        public int id_tab_ricorso { get; set; }
        public string nominativoContribuente { get; set; }
        public string data_sentenza_String { get; set; }
        public string data_deposito_sentenza_String { get; set; }
        public string data_scadenza_appello_String { get; set; }
        public string identificativo_ricorso { get; set; }
        public string identificativo_avviso { get; set; }
        public string descrizione_avviso { get; set; }
        public string rgr { get; set; }
        public string numeroSentenza { get; set; }
        public string sezione_giudicante { get; set; }
        public string nominativo_giudice { get; set; }
    }
}
