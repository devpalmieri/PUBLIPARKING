using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.LinqExtended
{
    public static class TabUtentiLinq
    {
        public static bool AnyUserName(this IQueryable<tab_utenti> p_query, string p_userName)
        {
            return p_query.Any(d => d.nome_utente.Equals(p_userName));
        }

        public static bool AnyEmail(this IQueryable<tab_utenti> p_query, string p_email)
        {
            return p_query.Any(d => d.email != null && d.email.Equals(p_email));
        }

        public static bool AnyCodiceFiscale(this IQueryable<tab_utenti> p_query, string p_codiceFiscale)
        {
            return p_query.Any(d => d.codice_fiscale.Equals(p_codiceFiscale));
        }

        public static bool AnyCodiceFiscaleUserName(this IQueryable<tab_utenti> p_query, string p_codiceFiscale)
        {
            return p_query.Any(d => d.codice_fiscale.Equals(p_codiceFiscale) && !string.IsNullOrEmpty(d.nome_utente));
        }

        public static IQueryable<tab_utenti> WhereByCodiceFiscaleUserNameNull(this IQueryable<tab_utenti> p_query, string p_codiceFiscale)
        {
            return p_query.Where(d => d.codice_fiscale.Equals(p_codiceFiscale) && string.IsNullOrEmpty(d.nome_utente));
        }

        public static bool AnyPIva(this IQueryable<tab_utenti> p_query, string p_pIva)
        {
            return p_query.Any(d => d.p_iva.Equals(p_pIva));
        }

        public static IQueryable<tab_utenti> WhereByUserName(this IQueryable<tab_utenti> p_query, string p_userName)
        {
            return p_query.Where(d => d.nome_utente.Equals(p_userName));
        }

        public static IQueryable<tab_utenti> WhereByEmail(this IQueryable<tab_utenti> p_query, string p_email)
        {
            return p_query.Where(d => d.email.Equals(p_email));
        }

        public static IQueryable<tab_utenti> WhereByCodiceFiscale(this IQueryable<tab_utenti> p_query, string p_codiceFiscale)
        {
            return p_query.Where(d => d.codice_fiscale.ToUpper().Equals(p_codiceFiscale.ToUpper()));
        }

        public static IQueryable<tab_utenti> WhereByPIva(this IQueryable<tab_utenti> p_query, string p_pIva)
        {
            return p_query.Where(d => d.p_iva.Equals(p_pIva));
        }

        public static IQueryable<tab_utenti> WhereByUtenteAttivo(this IQueryable<tab_utenti> p_query)
        {
            return p_query.Where(d => d.flag_utente_attivo == true);
        }

        public static IQueryable<tab_utenti> WhereByUtenteAttivoNot(this IQueryable<tab_utenti> p_query)
        {
            return p_query.Where(d => d.flag_utente_attivo == false);
        }

        public static IQueryable<tab_utenti> WhereByIdUtente(this IQueryable<tab_utenti> p_query, int p_idUtente)
        {
            return p_query.Where(d => d.id_utente == p_idUtente);
        }

        public static IQueryable<tab_utenti> WhereByUserNameNull(this IQueryable<tab_utenti> p_query)
        {
            return p_query.Where(d => string.IsNullOrEmpty(d.nome_utente));
        }

        public static IQueryable<tab_utenti> WhereByCodStato(this IQueryable<tab_utenti> p_query, string p_codStato)
        {
            return p_query.Where(d => d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_utenti> WhereByCodStato(this IQueryable<tab_utenti> p_query, List<string> p_codStato)
        {
            return p_query.Where(d => p_codStato.Contains(d.cod_stato));
        }

        public static IQueryable<tab_utenti> WhereByCodStatoNot(this IQueryable<tab_utenti> p_query, string p_codStato)
        {
            return p_query.Where(d => !d.cod_stato.StartsWith(p_codStato));
        }

        public static IQueryable<tab_utenti> WhereByCodStatoNot(this IQueryable<tab_utenti> p_query, List<string> p_codStato)
        {
            return p_query.Where(d => !p_codStato.Contains(d.cod_stato));
        }

        public static IQueryable<tab_utenti> OrderByDefault(this IQueryable<tab_utenti> p_query)
        {
            return p_query.OrderBy(d => d.cognome).ThenBy(d => d.nome);
        }
    }
}
