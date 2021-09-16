using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabCategoriaTariffariaCosapBD : EntityBD<tab_categoria_tariffaria_cosap>
    {
        public TabCategoriaTariffariaCosapBD()
        {

        }

        public static IQueryable<tab_categoria_tariffaria_cosap> GetTariffa(int p_idCategoria, DateTime p_periodoRifDa, DateTime p_periodoRifA, dbEnte p_dbContext)
        {
            return (GetList(p_dbContext)
                    .Where(tariffa => tariffa.periodo_rif_a >= p_periodoRifDa)
                    .Where(tariffa => tariffa.periodo_rif_da <= p_periodoRifA)
                    .Where(tariffa => tariffa.id_anagrafica_categoria == p_idCategoria)
                    .OrderBy(tariffa => tariffa.priorita));
        }

    }
}
