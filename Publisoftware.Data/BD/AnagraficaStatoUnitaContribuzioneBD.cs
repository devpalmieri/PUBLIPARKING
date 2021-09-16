using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoUnitaContribuzioneBD : EntityBD<anagrafica_stato_unita_contribuzione>
    {
        public AnagraficaStatoUnitaContribuzioneBD()
        {

        }

        public static anagrafica_stato_unita_contribuzione GetByCodStato(string p_cod_stato, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(sa => sa.cod_stato == p_cod_stato).FirstOrDefault();
        }

        public static int GetIdFromCodStato(string p_cod_stato, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(sa => sa.cod_stato == p_cod_stato).Select(sa => sa.id_anagrafica_stato).FirstOrDefault();
        }

    }
}
