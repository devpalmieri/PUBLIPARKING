using Publisoftware.Data;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    /// <summary>
    /// anagrafica_ente da utilizzare in "Sessione", senza alcuna proprietà navigabile
    /// </summary>
    public class anagrafica_ente_light_nonav
    {
        public int id_ente { get; set; }
        public string codice_ente { get; set; }
        public string descrizione_ente { get; set; }
        public Nullable<int> cod_regione { get; set; }
        public Nullable<int> cod_provincia { get; set; }
        public Nullable<int> cod_comune { get; set; }
        public string cap { get; set; }
        public string indirizzo { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public Nullable<int> id_tipo_ente { get; set; }
        public Nullable<int> id_stato_contatto { get; set; }
        public string cod_fiscale { get; set; }
        public string p_iva { get; set; }
        public Nullable<System.DateTime> data_ultimo_controllo_validita { get; set; }
        public string nome_db { get; set; }
        public string flag_Tipo_rendicontazione { get; set; }
        public string flag_sportello { get; set; }
        public string flag_idrico { get; set; }
        public string flag_presenze { get; set; }
        public string flag_sosta { get; set; }
        public string user_name_db { get; set; }
        public string password_db { get; set; }
        public string indirizzo_ip_db { get; set; }
        public string cod_ente { get; set; }
        public string codice_concessione { get; set; }
        public string tp_entity_id { get; set; }      

        public string password_dbD
        {
            get
            {
                return password_db != null ? CryptMD5.Decrypt(password_db) : null;
            }
        }


        public static anagrafica_ente_light_nonav FromAnagraficaEnte(anagrafica_ente item)
        {
            return new anagrafica_ente_light_nonav
            {
                id_ente = item.id_ente,
                codice_ente = item.codice_ente,
                descrizione_ente = item.descrizione_ente,
                cod_regione = item.cod_regione,
                cod_provincia = item.cod_provincia,
                cod_comune = item.cod_comune,
                cap = item.cap,
                indirizzo = item.indirizzo,
                tel1 = item.tel1,
                tel2 = item.tel2,
                id_tipo_ente = item.id_tipo_ente,
                id_stato_contatto = item.id_stato_contatto,
                cod_fiscale = item.cod_fiscale,
                p_iva = item.p_iva,
                data_ultimo_controllo_validita = item.data_ultimo_controllo_validita,
                nome_db = item.nome_db,
                flag_Tipo_rendicontazione = item.flag_Tipo_rendicontazione,
                flag_sportello = item.flag_sportello,
                flag_idrico = item.flag_idrico,
                flag_presenze = item.flag_presenze,
                flag_sosta = item.flag_sosta,
                user_name_db = item.user_name_db,
                password_db = item.password_db,
                indirizzo_ip_db = item.indirizzo_ip_db,
                cod_ente = item.cod_ente,
                codice_concessione = item.codice_concessione,
                tp_entity_id = item.tp_entity_id
            };
        }
    }
}
