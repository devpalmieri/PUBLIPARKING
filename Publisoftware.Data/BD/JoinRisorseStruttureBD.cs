using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinRisorseStruttureBD : EntityBD<join_risorse_strutture>
    {
        public JoinRisorseStruttureBD()
        {

        }

        public static join_risorse_strutture GetByIdRisorsaIdStruttura(int p_idRisorsa, int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(rs => rs.id_risorsa == p_idRisorsa && rs.id_struttura_aziendale == p_idStruttura);
        }
    }
}
