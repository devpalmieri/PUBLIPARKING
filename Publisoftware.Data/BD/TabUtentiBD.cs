using Publisoftware.Data.LinqExtended;
using Publisoftware.Utility;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabUtentiBD : EntityBD<tab_utenti>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabUtentiBD");
        #endregion Private Members
        public TabUtentiBD()
        {

        }

        public static bool HasEmailByCFPIVA(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                if (GetList(p_dbContext).AnyCodiceFiscale(p_codiceFiscalePIVA))
                {
                    return GetList(p_dbContext).WhereByCodiceFiscale(p_codiceFiscalePIVA).FirstOrDefault().email != null;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (GetList(p_dbContext).AnyPIva(p_codiceFiscalePIVA))
                {
                    return GetList(p_dbContext).WhereByPIva(p_codiceFiscalePIVA).FirstOrDefault().email != null;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool HasEmailByUserName(string p_userName, dbEnte p_dbContext)
        {
            if (GetList(p_dbContext).AnyUserName(p_userName))
            {
                return GetList(p_dbContext).WhereByUserName(p_userName).FirstOrDefault().email != null;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se esiste un utente con il Codice Fiscale o la Partita IVA indicato
        /// </summary>
        /// <param name="p_codiceFiscalePIVA">Codice Fiscale o la Partita IVA ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckCodiceFiscalePIVA(string p_codiceFiscalePIVA, dbEnte p_dbContext)
        {
            if (p_codiceFiscalePIVA.Length == 16)
            {
                return GetList(p_dbContext).AnyCodiceFiscale(p_codiceFiscalePIVA);
            }
            else
            {
                return GetList(p_dbContext).AnyPIva(p_codiceFiscalePIVA);
            }
        }

        /// <summary>
        /// Verifica se il Codice Reset Password corrisponde per l'utente indicato
        /// </summary>
        /// <param name="p_idUtente">ID Utente ricercato</param>
        /// <param name="p_codiceResetPassword">Codice Reset Password da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckCodiceResetPassword(int p_idUtente, string p_codiceResetPassword, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(d => d.id_utente == p_idUtente && d.codice_reset_password.Equals(p_codiceResetPassword));
        }

        public static tab_utenti GetUtenteByUsernameEmail(string p_username, string p_email, dbEnte p_dbContext)
        {
            if (!string.IsNullOrEmpty(p_username) && GetList(p_dbContext).Any(d => d.nome_utente.Equals(p_username)))
            {
                return GetList(p_dbContext).WhereByUserName(p_username)
                                           .WhereByUtenteAttivo()
                                           .WhereByCodStato(tab_utenti.ATT_ATT)
                                           .FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(p_email) && GetList(p_dbContext).Any(d => d.email.Equals(p_email)))
            {
                return GetList(p_dbContext).WhereByEmail(p_email)
                                           .WhereByUtenteAttivo()
                                           .WhereByCodStato(tab_utenti.ATT_ATT)
                                           .FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// restituisce l'entità login utente partendo dal codice fiscale
        /// </summary>
        /// <param name="p_codiceFiscalePartitaIva">Codice Fiscale o Partita IVA ricercato</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static tab_utenti GetByCodiceFiscalePartitaIva(string p_codiceFiscalePartitaIva, dbEnte p_dbContext)
        {
            tab_utenti v_utente = null;
            if (p_codiceFiscalePartitaIva.Length == 16)
            {
                v_utente = GetList(p_dbContext).WhereByCodiceFiscale(p_codiceFiscalePartitaIva)
                                               .WhereByUtenteAttivo()
                                               .WhereByCodStato(tab_utenti.ATT_ATT)
                                               .SingleOrDefault();
            }
            else if (p_codiceFiscalePartitaIva.Length == 11)
            {
                v_utente = GetList(p_dbContext).WhereByPIva(p_codiceFiscalePartitaIva)
                                               .WhereByUtenteAttivo()
                                               .WhereByCodStato(tab_utenti.ATT_ATT)
                                               .SingleOrDefault();
            }
            return v_utente;
        }

        /// <summary>
        /// verifica le credenziali nomeUtente e password (md5Hash) associate ad un utente
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_password">Password da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckUserNameAndPassword(string p_userName, string p_password, dbEnte p_dbContext)
        {
            tab_utenti v_utente = GetList(p_dbContext).WhereByUserName(p_userName)
                                                      .WhereByUtenteAttivo()
                                                      .WhereByCodStato(tab_utenti.ATT_ATT)
                                                      .SingleOrDefault();

            return v_utente != null && CryptMD5.VerifyMd5Hash(p_password, v_utente.password_utente);
        }

        /// <summary>
        /// verifica le credenziali nomeUtente e codice fiscale associate ad un utente
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_codiceFiscale">Codice Fiscale da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckUserNameCodiceFiscale(string p_userName, string p_codiceFiscale, dbEnte p_dbContext)
        {
            tab_utenti v_utente = GetList(p_dbContext).WhereByUserName(p_userName)
                                                      .WhereByCodiceFiscale(p_codiceFiscale)
                                                      .WhereByUtenteAttivo()
                                                      .WhereByCodStato(tab_utenti.ATT_ATT)
                                                      .SingleOrDefault();

            return v_utente != null ? true : false;
        }

        /// <summary>
        /// verifica le credenziali nomeUtente e codice fiscale associate ad un utente
        /// </summary>
        /// <param name="p_userName">UserName da verificare</param>
        /// <param name="p_partitaIva">Partita IVA da verificare</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool checkUserFromPIva(string p_userName, string p_partitaIva, dbEnte p_dbContext)
        {
            tab_utenti v_utente = GetList(p_dbContext).WhereByUserName(p_userName)
                                                      .WhereByPIva(p_partitaIva)
                                                      .WhereByUtenteAttivo()
                                                      .WhereByCodStato(tab_utenti.ATT_ATT)
                                                      .SingleOrDefault();

            return v_utente != null ? true : false;
        }

        /// <summary>
        /// Cambia la password di un utente
        /// </summary>
        /// <param name="p_idUtente">ID Utente al quale cambiare la password</param>
        /// <param name="p_oldPassword">Vecchia password</param>
        /// <param name="p_newPassword">Nuova password</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool changePassword(int p_idUtente, string p_oldPassword, string p_newPassword, dbEnte p_dbContext)
        {
            bool risp = false;

            tab_utenti v_utente = GetList(p_dbContext).WhereByIdUtente(p_idUtente)
                                                      .SingleOrDefault();

            if (CryptMD5.VerifyMd5Hash(p_oldPassword, v_utente.password_utente))
            {
                v_utente.password_utente = CryptMD5.getMD5(p_newPassword);
                risp = true;
            }

            return risp;
        }
    }
}
