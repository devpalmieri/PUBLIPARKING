using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabProgCarrelloBD : EntityBD<tab_prog_carrello>
    {
        public TabProgCarrelloBD()
        {

        }
        //public static string IncrementaProgressivoCorrente(dbEnte p_dbContext, bool v_saveInterno = true)
        //{
        //    tab_prog_carrello v_prog = null;
        //    v_prog = GetList(p_dbContext).FirstOrDefault();
        //    if (v_prog == null)
        //    {
        //        v_prog = new tab_prog_carrello()
        //        {
        //            progressivo_carrello = "1".PadLeft(18,'0')
        //        };
        //        p_dbContext.tab_prog_carrello.Add(v_prog);
        //    }
        //    else
        //    {
        //        v_prog.progressivo_carrello =(Convert.ToInt32( v_prog.progressivo_carrello) + 1).ToString().PadLeft(18, '0');
        //    }

        //    if (v_saveInterno)
        //    {
        //        p_dbContext.SaveChanges();
        //    }

        //    return v_prog.progressivo_carrello;
        //}

        //public static string IncrementaProgressivoCorrente(dbEnte p_dbContext, out int intProgr, bool v_saveInterno = true)
        //{
        //    tab_prog_carrello v_prog = null;
        //    tab_prog_carrello v_progNext = null;
        //    intProgr = 1;
        //    v_prog = GetList(p_dbContext).FirstOrDefault();
        //    if (v_prog == null)
        //    {
        //        v_prog = new tab_prog_carrello()
        //        {
        //            progressivo_carrello = intProgr.ToString("X18")
        //        };
        //        p_dbContext.tab_prog_carrello.Add(v_prog);
        //    }
        //    else
        //    {
        //        v_progNext = new tab_prog_carrello()
        //        {
        //            progressivo_carrello = (v_prog.id_tab_prog_carrello + 1).ToString("X18")
        //        };
        //        intProgr = v_prog.id_tab_prog_carrello + 1;
        //    }

        //    if (v_saveInterno)
        //    {
        //        p_dbContext.tab_prog_carrello.Remove(v_prog);
        //        p_dbContext.tab_prog_carrello.Add(v_progNext);
        //        p_dbContext.SaveChanges();
        //    }

        //    return v_prog.progressivo_carrello;
        //}

        public static int ReturnProgressivoIncrementatoByIdEnteAnno(int p_idEnte, int p_anno,  dbEnte p_dbContext)
        {
            tab_prog_carrello v_tabProgCarrello = GetList(p_dbContext).WhereByIdEnte(p_idEnte)
                                                                      .WhereByAnno(p_anno)
                                                                      .FirstOrDefault();

            if (v_tabProgCarrello == null)
            {
                v_tabProgCarrello = new tab_prog_carrello();

                v_tabProgCarrello.anno = p_anno;
                v_tabProgCarrello.id_ente = p_idEnte;
                v_tabProgCarrello.progressivo_carrello = 0;

                p_dbContext.tab_prog_carrello.Add(v_tabProgCarrello);
            }

            v_tabProgCarrello.progressivo_carrello = v_tabProgCarrello.progressivo_carrello + 1;


            p_dbContext.SaveChanges();

            return v_tabProgCarrello.progressivo_carrello;
        }
    }
}
