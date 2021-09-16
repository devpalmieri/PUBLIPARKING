using Publisoftware.Data.LinqExtended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabIngiunzioniIspezioneBD : EntityBD<tab_ingiunzioni_ispezione> 
    {
        public TabIngiunzioniIspezioneBD()
        {

        }

        public static IQueryable<tab_ingiunzioni_ispezione> GetListJoinValidoByIspezione(decimal p_idIspezione, dbEnte p_context)
        {
            return JoinIspezioniIngiunzioniBD.GetList(p_context).WhereCodStato(CodStato.VAL_VAL).Where(ii => ii.id_tab_ispezione_coattivo == p_idIspezione).Select(ii => ii.tab_ingiunzioni_ispezione);
        }
    }
}
