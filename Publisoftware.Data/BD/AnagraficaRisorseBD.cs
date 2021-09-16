using Publisoftware.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class AnagraficaRisorseBD : EntityBD<anagrafica_risorse>
    {
        public AnagraficaRisorseBD()
        {

        }

        public static IQueryable<anagrafica_risorse> GetByRuoloMansioneRegioneProvinciaAndValidita(
            //string cod_stato,
            int cod_provincia,
            int cod_regione,
            DateTime dataQuery,
            //int id_ruolo_mansione,
            int id_tipo_doc_entrate,
            dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(y =>
                                              //y.id_ruolo_mansione == id_ruolo_mansione &&
                                              //y.cod_stato == cod_stato &&
                                              y.join_risorse_ser_comuni.Any(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                                                 (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                                                  x.data_inizio_validita <= dataQuery.Date &&
                                                                                  x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                                                  dataQuery.Date <= x.data_fine_validita));
        }

        //public static IQueryable<anagrafica_risorse> GetByRuoloMansioneRegioneAndValidita(
        //    string cod_stato,
        //    int cod_regione,
        //    DateTime dataQuery,
        //    int id_ruolo_mansione,
        //    int id_tipo_doc_entrate,
        //    dbEnte p_dbContext)
        //{
        //    return GetList(p_dbContext).Where(y =>
        //                                      //y.id_ruolo_mansione == id_ruolo_mansione &&
        //                                      //y.cod_stato == cod_stato &&
        //                                      y.join_risorse_ser_comuni.Any(x => x.cod_regione == cod_regione &&
        //                                                                         x.data_inizio_validita <= dataQuery.Date &&
        //                                                                         x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
        //                                                                         dataQuery.Date <= x.data_fine_validita));
        //}

        public static IQueryable<anagrafica_risorse> GetUfficialiRiscossioneByRegioneProvinciaAndValidita(
            //string cod_stato,
            int cod_provincia,
            int cod_regione,
            DateTime dataQuery,
            //int id_tipo_doc_entrate,
            dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(y =>
                                              //y.id_ruolo_mansione == id_ruolo_mansione &&
                                              //y.cod_stato == cod_stato &&
                                              y.join_risorse_ser_comuni.Any(x => (x.cod_provincia == cod_provincia || x.cod_provincia == null) &&
                                                                                 (x.cod_regione == cod_regione || x.cod_regione == null) &&
                                                                                  x.data_inizio_validita <= dataQuery.Date &&
                                                                                  //x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
                                                                                  dataQuery.Date <= x.data_fine_validita));
        }

        //public static IQueryable<anagrafica_risorse> GetUfficialiRiscossioneByRegioneAndValidita(
        //    string cod_stato,
        //    int cod_regione,
        //    DateTime dataQuery,
        //    //int id_tipo_doc_entrate,
        //    dbEnte p_dbContext)
        //{
        //    return GetList(p_dbContext).Where(y =>
        //                                      //y.id_ruolo_mansione == id_ruolo_mansione &&
        //                                      //y.cod_stato == cod_stato &&
        //                                      y.join_risorse_ser_comuni.Any(x => x.cod_regione == cod_regione &&
        //                                                                         x.data_inizio_validita <= dataQuery.Date &&
        //                                                                         //x.id_tipo_doc_entrate == id_tipo_doc_entrate &&
        //                                                                         dataQuery.Date <= x.data_fine_validita));
        //}

        public static anagrafica_risorse GetByUsername(string p_username, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.username != null && r.username.Equals(p_username)).FirstOrDefault();
        }

        public static anagrafica_risorse GetByEmail(string p_email, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => r.email != null && r.email.Equals(p_email)).FirstOrDefault();
        }

        public static bool CheckCodiceResetPassword(int p_idRisorsa, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Any(d => d.id_risorsa == p_idRisorsa);
        }

        public static anagrafica_risorse GetUtenteByUsernameEmail(string p_username, string p_email, dbEnte p_dbContext)
        {
            if (!string.IsNullOrEmpty(p_username) && GetList(p_dbContext).Any(d => d.username.Equals(p_username)))
            {
                return GetList(p_dbContext).Where(d => d.username.Equals(p_username)).FirstOrDefault();

            }
            else if (!string.IsNullOrEmpty(p_email) && GetList(p_dbContext).Any(d => d.email.Equals(p_email)))
            {
                return GetList(p_dbContext).Where(d => d.email.Equals(p_email)).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public static IQueryable<anagrafica_risorse> GetListByIdStruttura(int p_idStruttura, dbEnte p_dbContext)
        {
            return JoinRisorseStruttureBD.GetList(p_dbContext).Where(rs => rs.id_struttura_aziendale == p_idStruttura).Select(rs => rs.anagrafica_risorse);
        }

        /// <summary>
        /// Restituisce l'elenco delle risorse appartenenti ad una struttura escludendo il responsabile
        /// </summary>
        /// <param name="p_idStruttura">Id struttura di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_risorse> GetListByIdStrutturaNoResponsabile(int p_idStruttura, dbEnte p_dbContext)
        {
            anagrafica_strutture_aziendali struttura = AnagraficaStruttureAziendaliBD.GetById(p_idStruttura, p_dbContext);

            return GetListByIdStruttura(p_idStruttura, p_dbContext).Where(r => !struttura.id_risorsa.HasValue || r.id_risorsa != struttura.id_risorsa);
        }

        /// <summary>
        /// Restituisce l'elenco delle risorse non presenti nella struttura
        /// </summary>
        /// <param name="p_idStruttura">IS Struttura di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static IQueryable<anagrafica_risorse> GetListNotInStruttura(int p_idStruttura, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).Where(r => !r.join_risorse_strutture.Any(x => x.id_struttura_aziendale == p_idStruttura));
        }

        /// <summary>
        /// Verifica la correttezza di username e password della risorsa
        /// </summary>
        /// <param name="p_username">Username da verificare</param>
        /// <param name="p_password">Password da verificare (in chiaro)</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool CheckRisorsaFromPassword(string p_username, string p_password, dbEnte p_dbContext)
        {
            anagrafica_risorse v_risorsa = GetByUsername(p_username, p_dbContext);

            //la password viene confrontata localmente poichè SQL server effettua una compare case insensitive
            return v_risorsa != null && CryptMD5.VerifyMd5Hash(p_password, v_risorsa.password);
        }

        /// <summary>
        /// Cambia la password di una risorsa.
        /// </summary>
        /// <param name="p_idRisorsa">ID della risorsa su cui fare il cambio password</param>
        /// <param name="p_oldPassword">Vecchia password</param>
        /// <param name="p_newPassword">Nuova password</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool ChangePassword(int p_idRisorsa, string p_oldPassword, string p_newPassword, dbEnte p_dbContext)
        {
            bool risp = false;

            anagrafica_risorse risorsa = GetById(p_idRisorsa, p_dbContext);
            if (CryptMD5.VerifyMd5Hash(p_oldPassword, risorsa.password))
            {
                risorsa.password = CryptMD5.getMD5(p_newPassword);
                risorsa.data_password = DateTime.Now;
                risp = true;
            }

            return risp;
        }

        /// <summary>
        /// Restituisce la data in cui la risorsa ha fatto l'ultimo cambio password
        /// </summary>
        /// <param name="p_idRisorsa">ID Risorsa di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static DateTime? GetChangePasswordDate(int p_idRisorsa, dbEnte p_dbContext)
        {
            return GetById(p_idRisorsa, p_dbContext).data_password;
        }

        /// <summary>
        /// Numero di giorni di validità della password.
        /// Indica ogni quanti giorni l'utente è obbligato al cambio password
        /// </summary>
        /// <param name="p_idRisorsa">ID Risorsa di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static int GetChangePasswordExpirationDays(int p_idRisorsa, dbEnte p_dbContext)
        {
            int v_nrExpirationDays = -1;

            v_nrExpirationDays = GetById(p_idRisorsa, p_dbContext).validita_password;

            return v_nrExpirationDays;
        }

        /// <summary>
        /// Numero di giorni mancanti al cambio password
        /// </summary>
        /// <param name="p_idRisorsa">ID Risorsa di ricerca</param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static int GetExpirationDaysPassword(int p_idRisorsa, dbEnte p_dbContext)
        {
            DateTime? v_dataPassword = null;
            int v_nrDaysExpirationPassword = -1;

            v_dataPassword = GetChangePasswordDate(p_idRisorsa, p_dbContext);
            DateTime v_dataOdierna = DateTime.Now;

            if (v_dataPassword != null)
            {
                int v_nrDiffDays = (v_dataOdierna - v_dataPassword.Value).Days;
                v_nrDaysExpirationPassword = GetChangePasswordExpirationDays(p_idRisorsa, p_dbContext) - v_nrDiffDays;
            }

            return v_nrDaysExpirationPassword;
        }
    }
}
