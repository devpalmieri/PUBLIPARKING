using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinIspezioniIngiunzioniBD : EntityBD<join_ispezioni_ingiunzioni>
    {
        public JoinIspezioniIngiunzioniBD()
        {

        }

        public static IQueryable<join_ispezioni_ingiunzioni> GetListAvvisiNonValidiByIspezione(decimal p_idIspezione, dbEnte p_context)
        {
            return GetList(p_context).Where(i => i.cod_stato == CodStato.VAL_VAL && i.id_tab_ispezione_coattivo == p_idIspezione &&
            (!i.tab_ingiunzioni_ispezione.tab_avv_pag.cod_stato.StartsWith(CodStato.VAL)));
        }
    }
}
