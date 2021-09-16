using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabModelliFiscaliBD : EntityBD<tab_modelli_fiscali>
    {
        public TabModelliFiscaliBD()
        {

        }
       
        public static IQueryable<tab_modelli_fiscali> GetRecordExsist(String p_cod_fornitura, int? p_prog_fornituta
                                                            , int? p_anno_imposta, dbEnte p_dbContext, IEnumerable<string> p_includeEntities = null)
        {
            return GetList(p_dbContext, p_includeEntities).Where(w => w.codice_fornitura == p_cod_fornitura
                                                                && w.progressivo_fornitura == p_prog_fornituta
                                                                && w.anno_imposta == p_anno_imposta);
        }
    }
}
