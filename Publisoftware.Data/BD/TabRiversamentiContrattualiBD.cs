using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRiversamentiContrattualiBD : EntityBD<tab_riversamenti_contrattuali>
    {
        public TabRiversamentiContrattualiBD() { }

        public static IQueryable<tab_riversamenti_contrattuali> GetRiversamentiSuStessoEnte(int p_id_ente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(rc => rc.id_ente == p_id_ente && rc.anagrafica_ente_riversamento!=null && rc.anagrafica_ente_riversamento.id_tipo_ente == 1);
        }

        public static IQueryable<tab_riversamenti_contrattuali> GetRiversamentiSuAltroEnte(int p_id_ente, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(rc => rc.id_ente == p_id_ente && rc.anagrafica_ente_riversamento != null && rc.anagrafica_ente_riversamento.id_tipo_ente != 1);
        }
    }
}
