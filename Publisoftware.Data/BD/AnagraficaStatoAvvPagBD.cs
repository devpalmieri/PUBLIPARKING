using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaStatoAvvPagBD : EntityBD<anagrafica_stato_avv_pag>
    {
        public AnagraficaStatoAvvPagBD()
        {

        }

        /// <summary>
        /// Ritorna lista con un record per ogni cod_stato_riferimento
        /// N.B: presuppone che nel DB tutti i dati *_riferimento siano allineati (stesso codice = stessi dati relativi)
        /// </summary>
        /// <param name="p_ctx"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_stato_avv_pag> GetListCodRiferimentoDistinct(dbEnte p_ctx)
        {
            return GetList(p_ctx).GroupBy(a => a.cod_stato_riferimento).Select(g => g.FirstOrDefault());
        }

        public static anagrafica_stato_avv_pag GetByCodStato(string p_cod_stato, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(sa => sa.cod_stato == p_cod_stato).FirstOrDefault();
        }

        public static int GetIdFromCodStato(string p_cod_stato, dbEnte p_ctx)
        {
            return GetList(p_ctx).Where(sa => sa.cod_stato == p_cod_stato).Select(sa => sa.id_anagrafica_stato).FirstOrDefault();
        }

    }
}
