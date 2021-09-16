using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabAbilitazioneLinq
    {
        /// <summary>
        /// Restituisce le abilitazioni abilitate (flag_abiliato = true)
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEntrata">ID </param>
        /// <returns></returns>
        public static IQueryable<tab_abilitazione> Abilitate(this IQueryable<tab_abilitazione> p_query)
        {
            return p_query.Where(w => w.flag_abilitato);
        }

        public static IQueryable<tab_abilitazione> WhereByIdApplicazione(this IQueryable<tab_abilitazione> p_query, int p_idApplicazione)
        {
            return p_query.Where(d => d.id_tab_applicazioni == p_idApplicazione);
        }

        public static IQueryable<tab_abilitazione> WhereByIdRisorsa(this IQueryable<tab_abilitazione> p_query, int p_idRisorsa)
        {
            return p_query.Where(d => d.id_risorsa == p_idRisorsa);
        }

        public static IQueryable<tab_abilitazione> WhereByIdStruttura(this IQueryable<tab_abilitazione> p_query, int p_idStruttura)
        {
            return p_query.Where(d => d.id_struttura_aziendale == p_idStruttura);
        }

        public static IQueryable<tab_abilitazione> WhereByIdEnte(this IQueryable<tab_abilitazione> p_query, int p_idEnte)
        {
            return p_query.Where(d => d.id_ente == p_idEnte);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_abilitazione> OrderByDefault(this IQueryable<tab_abilitazione> p_query)
        {
            return p_query.OrderBy(o => o.id_tab_abilitazione);
        }
    }
}
