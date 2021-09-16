using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabValidazioneApprovazioneListeBD :EntityBD<tab_validazione_approvazione_liste>
    {
        public TabValidazioneApprovazioneListeBD()
        {

        }

        public static tab_validazione_approvazione_liste GetValidForLista(decimal p_idLista, dbEnte ctx)
        {
            return TabValidazioneApprovazioneListeBD.GetList(ctx)
                .Where(v => v.id_lista == p_idLista && !v.cod_stato.StartsWith(CodStato.ANN))
                .OrderByDescending(v => v.id_validazione_approvazione_liste)
                .FirstOrDefault();
        }
    }
}
