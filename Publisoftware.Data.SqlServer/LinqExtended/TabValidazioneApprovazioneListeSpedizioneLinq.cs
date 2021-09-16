using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabValidazioneApprovazioneListeSpedizioneLinq
    {
        /// <summary>
        /// Filtro per id lista
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idLista"></param>
        /// <returns></returns>
        public static IQueryable<tab_validazione_approvazione_liste_spedizione> WhereByIdLista(this IQueryable<tab_validazione_approvazione_liste_spedizione> p_query, int p_idLista)
        {
            return p_query.Where(w => w.id_lista == p_idLista);
        }
    }
}
