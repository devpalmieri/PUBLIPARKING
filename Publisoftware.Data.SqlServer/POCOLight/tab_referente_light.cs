using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_referente_light
    {        
        public string tipoContribuenteSigla { get; set; }
        public int id_tipo_referente { get; set; }
        public string cod_fiscale { get; set; }
        public string nome { get; set; }
        public string cognome { get; set; }
        public int cod_comune_nas { get; set; }
        public string comune_nas { get; set; }
        public string stato_nas { get; set; }
        public int id_sesso { get; set; }
        public string data_nas_string { get; set; }
        public string data_morte_string { get; set; }
        public string p_iva { get; set; }
        public string denominazione_commerciale { get; set; }
        public string rag_sociale { get; set; }
        public string stato { get; set; }
        public string cap { get; set; }
        public int? nr_civico { get; set; }
        public string sigla_civico { get; set; }
        public string colore { get; set; }
        public decimal? km { get; set; }
        public string condominio { get; set; }
        public string interno { get; set; }
        public string scala { get; set; }
        public string piano { get; set; }
        public string cell { get; set; }
        public string pec { get; set; }
        public string fax { get; set; }
        public string tel { get; set; }
        public string e_mail { get; set; }
        public string note { get; set; }
        public int? id_toponimo { get; set; }
        public int? id_strada_db_poste { get; set; }
        public int? cod_citta { get; set; }
        public string citta { get; set; }
        public string prov { get; set; }
        public string frazione { get; set; }
        public string indirizzo { get; set; }
        public string tipologiaReferente { get; set; }
        public bool isPersonaFisica { get; set; }
        public string indirizzoTotaleDisplay { get; set; }
        public string relazione { get; set; }
    }
}
