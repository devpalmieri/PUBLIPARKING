using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_doc_input_light : BaseEntity<tab_doc_input_light>
    {
        public int id_tab_doc_input { get; set; }
        public string NumeroIstanza { get; set; }
        public string TipoIstanza { get; set; }
        public int IdTipoDoc { get; set; }
        public int TipoGlobal { get; set; }
        public string DataPresentazione { get; set; }
        public DateTime? data_presentazione { get; set; }
        public string identificativo_doc_input { get; set; }
        public string identificativo_avviso { get; set; }
        public string contribuente_nominativo { get; set; }
        public string TipoAvviso { get; set; }
        public string NumeroAvviso { get; set; }
        public string rgr { get; set; }
        public DateTime? data_iscrizione_ruolo { get; set; }
        public string data_iscrizione_ruolo_String { get; set; }
        public string sezione_giudicante { get; set; }
        public string statoIstanza { get; set; }
        public string cod_stato { get; set; }
        public string risorsaAcquisizione { get; set; }
        public int idRisorsaLavorazione { get; set; }
        public string risorsaLavorazione { get; set; }
        public string risorsaControllo { get; set; }
        public string statoDoc { get; set; }
        public bool isLavorabile { get; set; }
        public bool isStampaCaricata { get; set; }
        public string terzo_nominativo { get; set; }
        public int anno { get; set; }
        public string codTipoDocEntrate { get; set; }

        public int idFascicolo { get; set; }
        public string Descr_Ricorso { get; set; }
        public string Cod_Tipo_Entrata { get; set; }
        public int Id_Ricorso { get; set; }

        public int id_join_avv_pag_doc_input { get; set; }
        public string tipoRateizzazione { get; set; }
        public bool isConsegnabileSportello { get; set; }
        public string organo_giudicante { get; set; }
        
        public decimal? importo_ricorso { get; set; }

        public string importo_ricorso_Euro { get; set; }
        public string soggetto_ricorrente { get; set; }
        public string cfPIva_soggetto_ricorrente { get; set; }
        public int Id_Autorita_Giudiziaria { get; set; }
        public DateTime? data_scadenza_memorie_ricorso { get; set; }
        public DateTime? data_scadenza_costituzione_giudizio_resistente { get; set; }
        public DateTime? data_scadenza_costituzione_giudizio_ricorrente { get; set; }
        public string data_scadenza_memorie_ricorso_string { get; set; }
        public string data_scadenza_costituzione_giudizio_resistente_string { get; set; }
        public string data_scadenza_costituzione_giudizio_ricorrente_string { get; set; }
        public string data_scadenza_mediazione_string { get; set; }
        public DateTime? data_scadenza_appello { get; set; }
        public string data_scadenza_appello_String { get; set; }
    }
}
