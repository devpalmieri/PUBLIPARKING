using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabCategoriaTariffariaIcpBD : EntityBD<tab_categoria_tariffaria_icp>
    {
        public TabCategoriaTariffariaIcpBD()
        {

        }

        public static tab_categoria_tariffaria_icp GetTariffa(int p_idCategoria, int p_anno, decimal p_superficeArr, dbEnte p_dbContext)
        {
            return (GetList(p_dbContext)
                    .Where(tariffa => tariffa.anno == p_anno)
                    .Where(tariffa => tariffa.id_anagrafica_categoria == p_idCategoria)
                    .Where(tariffa => tariffa.quantita_base_da <= p_superficeArr)
                    .Where(tariffa => tariffa.quantita_base_a >= p_superficeArr)
                    .OrderBy(tariffa => tariffa.id_anagrafica_categoria)).FirstOrDefault();
        }
    }
}
