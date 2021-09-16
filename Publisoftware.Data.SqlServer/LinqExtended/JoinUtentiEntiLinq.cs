using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class JoinUtentiEntiLinq
    {
        public static IQueryable<join_utenti_enti> WhereByIdUtente(this IQueryable<join_utenti_enti> p_query, int p_idUtente)
        {
            return p_query.Where(w => w.id_utente == p_idUtente);
        }

        public static IQueryable<join_utenti_enti> WhereByIdEnte(this IQueryable<join_utenti_enti> p_query, int p_idEnte)
        {
            return p_query.Where(w => w.id_ente == p_idEnte);
        }

        public static IQueryable<join_utenti_enti> WhereByCodStato(this IQueryable<join_utenti_enti> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<join_utenti_enti> WhereByCodStatoNot(this IQueryable<join_utenti_enti> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }
    }
}
