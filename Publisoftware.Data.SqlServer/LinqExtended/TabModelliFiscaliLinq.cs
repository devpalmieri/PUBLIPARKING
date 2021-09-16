using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publisoftware.Data.POCOLight;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabModelliFiscaliLinq
    {
        public static IQueryable<tab_modelli_fiscali> WhereExist(this IQueryable<tab_modelli_fiscali> p_query, string p_cod_fornitura, int p_prog_fornituta, int p_anno_imposta)
        {
            return p_query.Where(w => w.codice_fornitura == p_cod_fornitura &&
                                      w.progressivo_fornitura == p_prog_fornituta &&
                                      w.anno_imposta == p_anno_imposta);
        }
    }
}
