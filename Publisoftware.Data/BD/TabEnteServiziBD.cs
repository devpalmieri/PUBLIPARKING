using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabEnteServiziBD : EntityBD<tab_ente_servizi>
    {
        public TabEnteServiziBD()
        {

        }

        public static IQueryable<tab_ente_servizi> GetByIdEnteEnteGestito(dbEnte dbCtx, int idEnte, int idEnteGestito = 0)
        {
            if (idEnteGestito > 0)
                return Search(dbCtx, idEnte, -1, -1, null, idEnteGestito);

            return Search(dbCtx, idEnte);
        }

        public static IQueryable<tab_ente_servizi> Search(dbEnte dbCtx, int idEnte, int idTipoServizio = -1, int idEntrata = -1, DateTime? dataValidita = null, int idEnteGestito = -1)
        {
            IQueryable<tab_ente_servizi> retLst = GetList(dbCtx).Where(es => es.id_ente == idEnte);

            if (idTipoServizio > 0)
                retLst = retLst.Where(es => es.id_tipo_servizio == idTipoServizio);
            if (idEntrata > 0)
                retLst = retLst.Where(es => es.id_entrata == idEntrata);
            if (dataValidita.HasValue)
            {
                dataValidita = dataValidita.Value.Date;
                retLst = retLst.Where(es => es.data_inizio_validita <= dataValidita && es.data_fine_validita <= dataValidita);
            }
            if (idEnteGestito > 0)
                retLst = retLst.Where(es => es.id_ente_gestito == idEnteGestito);

            return retLst;
        }
    }
}
