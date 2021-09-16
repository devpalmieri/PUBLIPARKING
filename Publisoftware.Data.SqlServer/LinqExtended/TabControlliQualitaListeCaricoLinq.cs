using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabControlliQualitaListeCaricoLinq
    {
        /// <summary>
        /// Filtro per id anagrafica controllo
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idAnagraficaControlliQualita"></param>
        /// <returns></returns>
        public static IQueryable<tab_controlli_qualita_liste_carico> WhereByIdAnagraficaControllo(this IQueryable<tab_controlli_qualita_liste_carico> p_query, int p_idAnagraficaControlliQualita)
        {
            return p_query.Where(ac => ac.id_anagrafica_controllo == p_idAnagraficaControlliQualita);
        }

        /// <summary>
        /// Filtro per id lista
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idLista"></param>
        /// <returns></returns>
        public static IQueryable<tab_controlli_qualita_liste_carico> WhereByIdLista(this IQueryable<tab_controlli_qualita_liste_carico> p_query, int p_idLista)
        {
            return p_query.Where(ac => ac.id_lista == p_idLista);
        }

        public static IQueryable<tab_controlli_qualita_liste_carico> WhereByCodStatoContains(this IQueryable<tab_controlli_qualita_liste_carico> p_query, string p_codStato)
        {
            return p_query.Where(ac => ac.cod_stato.Contains(p_codStato));
        }
    }
}
