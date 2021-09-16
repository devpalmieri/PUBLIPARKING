using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabProgAvvPagBD : EntityBD<tab_prog_avvpag>
    {
        public TabProgAvvPagBD()
        {

        }

        public static int IncrementaProgressivoCorrente(int p_id_tipo_AvvPag, int p_anno_riferimento, int? p_idEntrata, dbEnte p_dbContext, bool v_saveInterno = true)
        {
            tab_prog_avvpag v_prog = null;

            v_prog = GetList(p_dbContext).Where(p => p.id_tipo_avvpag == p_id_tipo_AvvPag && p.anno == p_anno_riferimento).SingleOrDefault();

            if (v_prog == null)
            {
                v_prog = new tab_prog_avvpag()
                {
                    id_entrata = p_idEntrata.HasValue ? p_idEntrata.Value : anagrafica_entrate.NESSUNA_ENTRATA,
                    id_tipo_avvpag = p_id_tipo_AvvPag,
                    anno = p_anno_riferimento,
                    prog_tipo_avvpag = 1,
                    cronologico_sped_not = 1
                };
                p_dbContext.tab_prog_avvpag.Add(v_prog);
            }
            else
            {
                v_prog.prog_tipo_avvpag = v_prog.prog_tipo_avvpag + 1;
            }

            if (v_saveInterno)
            {
                p_dbContext.SaveChanges();
            }

            return v_prog.prog_tipo_avvpag;
        }

        public static int IncrementaProgressivoSpedNotCorrente(int p_id_tipo_AvvPag, int p_anno_riferimento, int? p_idEntrata, dbEnte p_dbContext, bool v_saveInterno = true)
        {
            tab_prog_avvpag v_prog = null;

            v_prog = GetList(p_dbContext).Where(p => p.id_tipo_avvpag == p_id_tipo_AvvPag && p.anno == p_anno_riferimento).SingleOrDefault();

            if (v_prog == null)
            {
                v_prog = new tab_prog_avvpag()
                {
                    id_entrata = p_idEntrata.HasValue ? p_idEntrata.Value : anagrafica_entrate.NESSUNA_ENTRATA,
                    id_tipo_avvpag = p_id_tipo_AvvPag,
                    anno = p_anno_riferimento,
                    prog_tipo_avvpag = 1,
                    cronologico_sped_not = 1
                };
                p_dbContext.tab_prog_avvpag.Add(v_prog);
            }
            else
            {
                if (v_prog.cronologico_sped_not == null)
                {
                    v_prog.cronologico_sped_not = 1;
                }
                else
                {
                    v_prog.cronologico_sped_not = v_prog.cronologico_sped_not + 1;
                }
            }

            if (v_saveInterno)
            {
                p_dbContext.SaveChanges();
            }

            return v_prog.cronologico_sped_not.Value;
        }
    }
}
