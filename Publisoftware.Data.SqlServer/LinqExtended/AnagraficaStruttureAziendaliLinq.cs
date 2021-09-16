using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class AnagraficaStruttureAziendaliLinq
    {
        public static IQueryable<anagrafica_strutture_aziendali> WhereByIdEnteAppartenenza(this IQueryable<anagrafica_strutture_aziendali> p_query, int p_idEnteAppartenenza)
        {
            return p_query.Where(d => d.id_ente_appartenenza == p_idEnteAppartenenza);
        }

        public static IQueryable<anagrafica_strutture_aziendali> WhereByIdStruttura(this IQueryable<anagrafica_strutture_aziendali> p_query, int p_id)
        {
            return p_query.Where(d => d.id_struttura_aziendale == p_id);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_strutture_aziendali> OrderByDefault(this IQueryable<anagrafica_strutture_aziendali> p_query)
        {
            return p_query.OrderBy(o => o.descr_struttura);
        }

        public static IQueryable<anagrafica_strutture_aziendali_light> ToLight(this IQueryable<anagrafica_strutture_aziendali> iniziale)
        {
            return iniziale.ToList().Select(d => new anagrafica_strutture_aziendali_light
            {
                id_struttura_aziendale = d.id_struttura_aziendale,
                descr_struttura = d.descr_struttura,
                codice_struttura_aziendale = d.codice_struttura_aziendale,
                provincia = d.provincia
            }).AsQueryable();
        }
    }
}
