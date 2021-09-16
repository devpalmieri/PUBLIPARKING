using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class tab_liste_light
    {
        public int id_lista { get; set; }
        public String numero_lista { get; set; }
        public string dt_approvazione_lista_String { get; set; }
        public string determina_approvazione { get; set; }
        public string identificativo_lista { get; set; }
        public string data_generazione_lista_String { get; set; }
        public string identificativo_lista_carico { get; set; }
        public string data_generazione_lista_carico_String { get; set; }
        public string stato { get; set; }
        public string anno_riferimento_lista { get; set; }
        public string descr_lista { get; set; }
        public string num_avvpag { get; set; }
        public string imp_tot_lista { get; set; }
        public string EntrataRiferimento { get; set; }
        public string StrutturaApprovazione { get; set; }
        public string RisorsaApprovazione { get; set; }
        public string RisorsaCreazione { get; set; }
        public int IdStrutturaApprovazione { get; set; }
        public int IdRisorsaApprovazione { get; set; }
        public int IdRisorsaCreazione { get; set; }
        public string DataApprovazione { get; set; }
        public string DecorrenzaInteressi { get; set; }
        public string data_lista_String { get; set; }
        public string isListaMassiva { get; set; }
    }
}
