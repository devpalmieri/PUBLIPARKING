using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRegistroUfficialiRiscossioneBD : EntityBD<tab_registro_ufficiali_riscossione>
    {
        public TabRegistroUfficialiRiscossioneBD()
        {

        }

        public static IQueryable<tab_registro_ufficiali_riscossione> GetListCitazioniAssegnate(dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(w => w.cod_stato == "ATT-ACQ" && w.id_tipo_atto_ufficiali_riscossione  == 13);
        }

        public static int GetMaxCronologico(int id_risorsa, int year, dbEnte p_dbContext)
        {
            return (GetList(p_dbContext).Where(w => w.id_risorsa == id_risorsa && w.anno_esercizio == DateTime.Now.Year).Select(s => s.progressivo_numero_cronologico.HasValue ? s.progressivo_numero_cronologico : s.numero_cronologico).Max() ?? 0);
        }

    }
}
