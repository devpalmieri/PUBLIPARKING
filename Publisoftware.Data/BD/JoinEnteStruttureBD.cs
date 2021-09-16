using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinEnteStruttureBD : EntityBD<join_ente_strutture>
    {
        public JoinEnteStruttureBD()
        {

        }

        public static IQueryable<join_ente_strutture> GetListByIdEnte(int p_idEnte, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(x => x.id_ente == p_idEnte);
        }

        public static IQueryable<join_ente_strutture> GetListByIdStruttura(int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(x => x.id_struttura_aziendale == p_idStruttura);
        }

        public static bool HasIdStruttura(int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(x => x.id_struttura_aziendale == p_idStruttura);
        }

        public static bool CheckIfEnteIsRelatedtoStruttura(int p_idEnte, int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(x => x.id_ente == p_idEnte && x.id_struttura_aziendale == p_idStruttura);
        }
    }
}
