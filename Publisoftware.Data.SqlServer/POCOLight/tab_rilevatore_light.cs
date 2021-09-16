using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_rilevatore_light
    {
        public int id_rilevatore { get; set; }
        public string flag_interna_esterna { get; set; }
        public string flag_PF_PG { get; set; }
        public string cognome { get; set; }
        public string nome { get; set; }
        public string rag_sociale { get; set; }
        public string indirizzo { get; set; }
        public string tel_cellulare { get; set; }
        public string tel_casa { get; set; }
        public string email { get; set; }
        public string cod_fiscale { get; set; }
        public string p_iva { get; set; }
        public string cod_stato { get; set; }
        public string nome_utente { get; set; }
        public string password { get; set; }
    }
}
