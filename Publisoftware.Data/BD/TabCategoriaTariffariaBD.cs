using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.LinqExtended;
using System.Data.Entity;

namespace Publisoftware.Data.BD
{
    public class TabCategoriaTariffariaBD : EntityBD<tab_categoria_tariffaria>
    {
        public TabCategoriaTariffariaBD()
        {

        }

        public static tab_categoria_tariffaria GetTariffa(int p_idCategoria, int p_anno, dbEnte p_dbContext)
        {
            return (GetList(p_dbContext)
                .Where(tariffa => tariffa.anno == p_anno) 
                .Where(tariffa => tariffa.id_anagrafica_categoria == p_idCategoria)
                .OrderBy(tariffa => tariffa.id_anagrafica_categoria)).FirstOrDefault();

        }

        public static tab_categoria_tariffaria GetTariffaTARIG(string p_tipoOccMercato, int p_anno, dbEnte p_dbContext)
        {
            return (GetList(p_dbContext)
                .Where(tariffa => tariffa.anno == p_anno)
                .Where(tariffa => tariffa.anagrafica_categoria.tipo_spazio == p_tipoOccMercato)
                .OrderBy(tariffa => tariffa.id_anagrafica_categoria)).FirstOrDefault();
        }

    }
}
