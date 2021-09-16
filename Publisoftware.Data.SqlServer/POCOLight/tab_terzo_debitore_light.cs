using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_terzo_debitore_light
    {
        public int id_trz_debitore { get; set; }
        public string tipo_bene_descrizione { get; set; }
        public string cf_piva_soggetto_ispezione { get; set; }
        public string codiceFiscalePartitaIva { get; set; }
        public string nominativoRagioneSociale { get; set; }
        public string citta { get; set; }
        public string indirizzo { get; set; }
        public string dataAggiornamento { get; set; }
        public string codStato { get; set; }
    }
}
