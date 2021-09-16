using Publisoftware.Data.POCOLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabApplicazioniLinq
    {
        /// <summary>
        /// Filtro per ID Funzionalita
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idFunzionalita">ID Funzionalita</param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> WhereByIdFunzionalita(this IQueryable<tab_applicazioni> p_query, int p_idFunzionalita)
        {
            return p_query.Where(w => w.id_tab_funzionalita == p_idFunzionalita);
        }

        public static IQueryable<tab_applicazioni> WhereByNotSistema(this IQueryable<tab_applicazioni> p_query)
        {
            return p_query.Where(w => !w.flag_sistema);
        }

        public static IQueryable<tab_applicazioni> WhereByAssegnate(this IQueryable<tab_applicazioni> p_query, int p_idRisorsa, int p_idStruttura, int p_idEnte)
        {
            return p_query.Where(a => !a.tab_abilitazione.Any(ab => ab.id_risorsa == p_idRisorsa && ab.id_struttura_aziendale == p_idStruttura && ab.id_ente == p_idEnte));
        }

        public static IQueryable<tab_applicazioni> WhereByGruppo(this IQueryable<tab_applicazioni> p_query)
        {
            return p_query.Where(w => w.tipo_applicazione.Trim() == "G");
        }

        /// <summary>
        /// Filtro per ID Entrata o Entrata Generica
        /// </summary>
        /// <param name="p_query"></param>
        /// <param name="p_idEntrata">ID Entrata</param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> WhereByIdEntrataOrGenerica(this IQueryable<tab_applicazioni> p_query, int? p_idEntrata)
        {
            return p_query.Where(w => w.id_entrata == anagrafica_entrate.NESSUNA_ENTRATA || w.id_entrata == p_idEntrata);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> OrderByDefault(this IQueryable<tab_applicazioni> p_query)
        {
            return p_query.OrderBy(o => o.ordine);
        }

        /// <summary>
        /// Ordine di default
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IEnumerable<tab_applicazioni> OrderByDefault(this IEnumerable<tab_applicazioni> p_query)
        {
            return p_query.OrderBy(o => o.ordine);
        }

        /// <summary>
        /// Ordine per descrizione
        /// </summary>
        /// <param name="p_query"></param>
        /// <returns></returns>
        public static IQueryable<tab_applicazioni> OrderByDescrizione(this IQueryable<tab_applicazioni> p_query)
        {
            return p_query.OrderBy(o => o.descrizione);
        }

        public static IQueryable<tab_applicazioni_light> ToLight(this IQueryable<tab_applicazioni> iniziale)
        {
            return iniziale.ToList().Select(d => new tab_applicazioni_light
            {
                id_tab_applicazioni = d.id_tab_applicazioni,
                codice = d.codice,
                descrizione = d.descrizione,
                ordine = d.ordine,
                Abilitato = "No"
            }).AsQueryable();
        }
    }
}
