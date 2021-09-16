using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabProgListaBD : EntityBD<tab_prog_lista>
    {
        public TabProgListaBD()
        {

        }

        public static tab_prog_lista GetInfoProgressivoFor(int p_id_ente, int p_id_entrata, int p_id_tipo_Lista, int p_anno_riferimento, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(p => p.id_ente == p_id_ente && p.id_tipo_lista == p_id_tipo_Lista)
                                .Where(p => p.id_entrata == p_id_entrata)
                                .Where(p => p.anno == p_anno_riferimento)
                                .SingleOrDefault();
        }

        public static int IncrementaProgressivoCorrente(int p_id_ente, int p_id_tipo_Lista, int p_anno_riferimento, dbEnte p_dbContext, bool v_saveInterno = true)
        {

            tab_prog_lista v_prog = GetList(p_dbContext).Where(p => p.id_ente == p_id_ente &&  p.id_tipo_lista == p_id_tipo_Lista)
                                                        //.Where(p => p.id_entrata == p_id_entrata)
                                                        .Where(p => p.anno == p_anno_riferimento).SingleOrDefault();

            if (v_prog == null)
            {
                v_prog = new tab_prog_lista()
                {
                    id_ente = p_id_ente,
                    id_entrata = anagrafica_entrate.NESSUNA_ENTRATA,
                    id_tipo_lista = p_id_tipo_Lista,
                    anno = p_anno_riferimento,
                    progr = 1
                };
                p_dbContext.tab_prog_lista.Add(v_prog);
            }
            else
            {
                v_prog.progr = v_prog.progr + 1;
            }
            if(v_saveInterno)
            {
                p_dbContext.SaveChanges();
            }           
            return v_prog.progr;
        }
    }
}
