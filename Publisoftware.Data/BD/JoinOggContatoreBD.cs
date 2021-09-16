using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinOggContatoreBD : EntityBD<join_ogg_contatore>
    {
        public JoinOggContatoreBD()
        {

        }

        public static IQueryable<join_ogg_contatore> getListByIdOggetto(decimal p_idOggetto, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(d => d.id_oggetto == p_idOggetto);
        }
    }
}
