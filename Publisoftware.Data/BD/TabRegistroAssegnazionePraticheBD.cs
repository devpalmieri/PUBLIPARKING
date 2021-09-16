using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRegistroAssegnazionePraticheBD : EntityBD<tab_registro_assegnazione_pratiche>
    {
        public static int GetMaxCronologico(int id_risorsa, int year, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(w => w.id_risorsa == id_risorsa && w.anno_esercizio == DateTime.Now.Year)
                                       .Select(s => s.numero_cronologico).Max() ?? 0;
        }
    }
}
