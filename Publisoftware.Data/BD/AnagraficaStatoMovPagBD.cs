using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoMovPagBD : EntityBD<anagrafica_stato_mov_pag>
    {
        public AnagraficaStatoMovPagBD()
        {

        }    

        public static anagrafica_stato_mov_pag GetByCodStato(string p_cod_stato, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(d => d.cod_stato_mov_pag == p_cod_stato).FirstOrDefault();
        }

        public static int GetIdFromCodStato(string p_cod_stato, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(d => d.cod_stato_mov_pag == p_cod_stato).Select(d => d.id_stato_mov_pag).FirstOrDefault();
        }
    }
}
