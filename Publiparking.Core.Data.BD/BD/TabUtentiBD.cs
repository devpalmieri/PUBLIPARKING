using Publiparking.Core.Data.BD.Base;
using Publiparking.Core.Data.SqlServer;
using Publiparking.Core.Data.SqlServer.Entities;
using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publiparking.Core.Data.BD
{
    public class TabUtentiBD : EntityBD<tab_utenti>
    {
        public TabUtentiBD()
        {

        }
        /// <summary>
        /// Restituisce l'utente in base nomeUtente e password (md5Hash) associate
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_password">Password da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_utenti IsAuthenticated(DbParkContext p_dbContext, string p_username, string p_password)
        {
            tab_utenti user = GetList(p_dbContext)
                                .Where(d => d.nome_utente.Equals(p_username) &&
                                      d.flag_utente_attivo == true &&
                                      d.cod_stato == tab_utenti.ATT_ATT)
                                     .SingleOrDefault();

            if (user != null && CryptMD5.VerifyMd5Hash(p_password, user.password_utente))
                return user;

            return null;
        }
        /// <summary>
        /// verifica le credenziali nomeUtente e password (md5Hash) associate ad un utente
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_password">Password da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckUserNameAndPassword(DbParkContext p_dbContext, string p_username, string p_password)
        {
            tab_utenti user = GetList(p_dbContext)
                                .Where(d => d.nome_utente.Equals(p_username) &&
                                      d.flag_utente_attivo == true &&
                                      d.cod_stato == tab_utenti.ATT_ATT)
                                     .SingleOrDefault();


            return user != null && CryptMD5.VerifyMd5Hash(p_password, user.password_utente);
        }
        /// <summary>
        /// verifica le credenziali nomeUtente e codice fiscale associate ad un utente
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_codiceFiscale">Codice Fiscale da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckUserNameCodiceFiscale(DbParkContext p_dbContext, string p_userName, string p_codiceFiscale)
        {
            tab_utenti v_utente = GetList(p_dbContext)
                                                     .Where(d => d.nome_utente.Equals(p_userName) &&
                                                      d.codice_fiscale.ToUpper().Equals(p_codiceFiscale.ToUpper()) &&
                                                      d.flag_utente_attivo == true &&
                                                      d.cod_stato == tab_utenti.ATT_ATT)
                                                     .SingleOrDefault();

            return v_utente != null ? true : false;
        }
        public static bool AnyUserName(DbParkContext p_dbContext, string p_userName)
        {
            return GetList(p_dbContext).Any(d => d.nome_utente.Equals(p_userName));
        }
        public static bool AnyEmail(DbParkContext p_dbContext, string p_email)
        {
            return GetList(p_dbContext).Any(d => d.email != null && d.email.Equals(p_email));
        }
        public static bool AnyCodiceFiscaleUserName(DbParkContext p_dbContext, string p_codiceFiscale)
        {
            return GetList(p_dbContext).Any(d => d.codice_fiscale.Equals(p_codiceFiscale) && !string.IsNullOrEmpty(d.nome_utente));
        }
        public static tab_utenti WhereByCodiceFiscaleUserNameNull(DbParkContext p_dbContext, string p_codiceFiscale)
        {
            return GetList(p_dbContext).Where(d => d.codice_fiscale.Equals(p_codiceFiscale) && string.IsNullOrEmpty(d.nome_utente)).FirstOrDefault();
        }
        /// <summary>
        /// Verifica se il Codice Reset Password corrisponde per l'utente indicato
        /// </summary>
        /// <param name="p_idUtente">ID Utente ricercato</param>
        /// <param name="p_codiceResetPassword">Codice Reset Password da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckCodiceResetPassword(int p_idUtente, string p_codiceResetPassword, DbParkContext p_dbContext)
        {
            return GetList(p_dbContext).Any(d => d.id_utente == p_idUtente && d.codice_reset_password.Equals(p_codiceResetPassword));
        }

        public static tab_utenti GetUtenteByUsernameEmail(string p_username, string p_email, DbParkContext p_dbContext)
        {

            if (!string.IsNullOrEmpty(p_username) && GetList(p_dbContext).Any(d => d.nome_utente.Equals(p_username)))
            {
                return GetList(p_dbContext).Where(d => d.nome_utente.Equals(p_username) &&
                                                       d.flag_utente_attivo == true &&
                                                       d.cod_stato == tab_utenti.ATT_ATT)
                                                        .FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(p_email) && GetList(p_dbContext).Any(d => d.email.Equals(p_email)))
            {
                return GetList(p_dbContext).Where(d => d.email.Equals(p_email) &&
                                           d.flag_utente_attivo == true &&
                                           d.cod_stato == tab_utenti.ATT_ATT)
                                           .FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

    }
}
