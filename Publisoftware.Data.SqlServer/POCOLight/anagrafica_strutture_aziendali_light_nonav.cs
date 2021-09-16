using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.POCOLight
{
    public class anagrafica_strutture_aziendali_light_nonav
    {
        public int id_struttura_aziendale { get; set; }
        public string codice_struttura_aziendale { get; set; }
        public string descr_struttura { get; set; }
        public string tipo_struttura { get; set; }
        public Nullable<int> id_risorsa { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string indirizzo { get; set; }
        public string provincia { get; set; }
        public string cap { get; set; }
        public Nullable<int> prog_lav { get; set; }
        public Nullable<int> id_ente_appartenenza { get; set; }

        public static anagrafica_strutture_aziendali_light_nonav FromAnagraficaStruttureAziendali(anagrafica_strutture_aziendali item)
        {
            return new anagrafica_strutture_aziendali_light_nonav
            {
                id_struttura_aziendale = item.id_struttura_aziendale,
                codice_struttura_aziendale = item.codice_struttura_aziendale,
                descr_struttura = item.descr_struttura,
                tipo_struttura = item.tipo_struttura,
                id_risorsa = item.id_risorsa,
                telefono = item.telefono,
                email = item.email,
                indirizzo = item.indirizzo,
                provincia = item.provincia,
                cap = item.cap,
                prog_lav = item.prog_lav,
                id_ente_appartenenza = item.id_ente_appartenenza
            };
        }
    }
}
