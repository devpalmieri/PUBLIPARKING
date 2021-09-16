using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabTitolaritaLinq
    {
        public static IQueryable<tab_titolarita> WhereByIdImmobile(this IQueryable<tab_titolarita> p_query, int p_idImmobile)
        {
            return p_query.Where(d => d.id_tab_immobili == p_idImmobile);
        }

        public static IQueryable<tab_titolarita> OrderByDefault(this IQueryable<tab_titolarita> p_query)
        {
            return p_query.OrderBy(d => d.tab_note1.data_efficacia).ThenBy(d => d.tab_note != null ? d.tab_note.data_efficacia : DateTime.MaxValue);
        }

        public static IQueryable<tab_titolarita_light> ToLight(this IQueryable<tab_titolarita> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_titolarita_light
            {
                id_tab_titolarita = d.id_tab_titolarita,
                Proprietario = d.tab_persone.Proprietario,
                Diritto = d.tab_tipi_diritti.Diritto,
                DataInizio = d.DataInizio,
                DataFine = d.DataFine,
                Quota = d.Quota
            }).AsQueryable();
        }
    }
}
