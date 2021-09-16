using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabProgIuvBD : EntityBD<tab_prog_iuv>
    {
        public TabProgIuvBD()
        {

        }

        public static int IncrementaProgressivoCorrente(string p_cocide_avviso, int p_anno_riferimento, dbEnte p_dbContext, bool v_saveInterno = true)
        {
            tab_prog_iuv v_prog = null;

            v_prog = GetList(p_dbContext).Where(p => p.codice_atto_pagopa == p_cocide_avviso && p.anno == p_anno_riferimento).SingleOrDefault();

            if (v_prog == null)
            {
                v_prog = new tab_prog_iuv()
                {                    
                    codice_atto_pagopa = p_cocide_avviso,
                    anno = p_anno_riferimento,                    
                    progressivo_iuv = 1
                };
                p_dbContext.tab_prog_iuv.Add(v_prog);
            }
            else
            {
                v_prog.progressivo_iuv = v_prog.progressivo_iuv + 1;
            }

            if (v_saveInterno)
            {
                p_dbContext.SaveChanges();
            }
           
            return v_prog.progressivo_iuv;
        }
    }
}
