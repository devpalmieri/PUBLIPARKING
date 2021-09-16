using Publiparking.Core.Data.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.SqlServer.LinqExtended
{
    public static class TabUtentiLinq
    {
        public static bool AnyUserName(this IQueryable<tab_utenti> p_query, string p_userName)
        {
            return p_query.Any(d => d.nome_utente.Equals(p_userName));
        }
        public static IQueryable<tab_utenti> WhereByUserName(this IQueryable<tab_utenti> p_query, string p_userName)
        {
            return p_query.Where(d => d.nome_utente.Equals(p_userName));
        }

        public static IQueryable<tab_utenti> WhereByEmail(this IQueryable<tab_utenti> p_query, string p_email)
        {
            return p_query.Where(d => d.email.Equals(p_email));
        }
        public static IQueryable<tab_utenti> WhereByUtenteAttivo(this IQueryable<tab_utenti> p_query)
        {
            return p_query.Where(d => d.flag_utente_attivo == true);
        }

        public static IQueryable<tab_utenti> WhereByUtenteAttivoNot(this IQueryable<tab_utenti> p_query)
        {
            return p_query.Where(d => d.flag_utente_attivo == false);
        }
        public static IQueryable<tab_utenti> WhereByCodStato(this IQueryable<tab_utenti> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }
    }
}
