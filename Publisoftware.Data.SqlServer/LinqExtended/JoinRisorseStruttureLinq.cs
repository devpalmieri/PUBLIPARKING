using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinRisorseStruttureLinq
    {
        /// <summary>
        /// Filtro per ID Struttura
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idStrutura">ID Struttura</param>
        /// <returns></returns>
        public static IQueryable<join_risorse_strutture> WhereByIdStruttura(this IQueryable<join_risorse_strutture> p_query, int p_idStruttura)
        {
            return p_query.Where(w => w.id_struttura_aziendale == p_idStruttura);
        }

        public static IQueryable<join_risorse_strutture> WhereByFlagIspettore(this IQueryable<join_risorse_strutture> p_query)
        {
            return p_query.Where(w => w.anagrafica_risorse.flag_ispettore == "1" || w.anagrafica_risorse.flag_ispettore == "2");
        }

        public static IQueryable<join_risorse_strutture> WhereByFlagResponsabilita(this IQueryable<join_risorse_strutture> p_query)
        {
            return p_query.Where(w => w.flag_responsabilita == "1");
        }

        public static IQueryable<join_risorse_strutture> WhereByIdRisorsa(this IQueryable<join_risorse_strutture> p_query, int p_idRisorsa)
        {
            return p_query.Where(w => w.id_risorsa == p_idRisorsa);
        }

        public static IQueryable<join_risorse_strutture> WhereByNotIdRisorsa(this IQueryable<join_risorse_strutture> p_query, int p_idRisorsa)
        {
            return p_query.Where(w => w.id_risorsa != p_idRisorsa);
        }

        public static IQueryable<join_risorse_strutture> OrderByResponsabile(this IQueryable<join_risorse_strutture> p_query)
        {
            return p_query.OrderByDescending(o => o.flag_responsabilita);
        }

        /// <summary>
        /// Ordina i record per risorsa
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<join_risorse_strutture> OrderByRisorse(this IQueryable<join_risorse_strutture> p_query)
        {
            return p_query.OrderBy(o => o.anagrafica_risorse.cognome).ThenBy(o => o.anagrafica_risorse.nome);
        }
    }
}
