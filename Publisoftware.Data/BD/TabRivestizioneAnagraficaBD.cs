using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRivestizioneAnagraficaBD : EntityBD<tab_rivestizione_anagrafica>
    {
        //Rstituisce i parametri per le Visure ACI
        public static tab_rivestizione_anagrafica getVisureAciByIdEnte(dbEnte p_dbContext, int p_idEnte)
        {
            return GetList(p_dbContext).Where(c => c.id_ente == p_idEnte && c.tipo_rivestizione == "WS" && c.tipo_bd_esterna == "ACI" && c.codice_servizio == "VIS_ACI" && c.tipo_estrazione_dati == "VEI").SingleOrDefault();
        }
    }
}
