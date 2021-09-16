using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRegistroCookieBD : EntityBD<tab_registro_cookie>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabRegistroCookieBD");
        #endregion Private Members
        public TabRegistroCookieBD()
        {

        }

        public static bool SaveCookieUser(tab_registro_cookie v_cookie, dbEnte p_dbContext, out int p_id_rec_cookie)
        {
            p_id_rec_cookie = -1;

            try
            {
                if (v_cookie == null)
                {
                    return false;
                }
                p_dbContext.tab_registro_cookie.Add(v_cookie);
                p_dbContext.SaveChanges();

                p_id_rec_cookie = v_cookie.id_tab_registro_cookie;
                logger.LogInfoMessage(string.Format("Registrazione cookie utente. Prima visita.", v_cookie.session_id));
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                p_id_rec_cookie = -1;
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                p_id_rec_cookie = -1;
                logger.LogException("Errore in fase di salvataggio del cookie utente. ", ex, EnLogSeverity.Error);
                return false;
            }
        }

        public static tab_registro_cookie GetCookieByIdSessione(string p_idSessione, dbEnte p_dbContext)
        {
            logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => d.session_id.TrimEnd().Trim().TrimStart() == p_idSessione).FirstOrDefault();
        }
        public static tab_registro_cookie GetCookieByIPAddress(string p_ipaddress, dbEnte p_dbContext)
        {
            logger.LogMethod(System.Reflection.MethodBase.GetCurrentMethod(), EnLogSeverity.Debug);

            return GetList(p_dbContext).Where(d => d.indirizzo_ip.TrimEnd().Trim().TrimStart() == p_ipaddress).FirstOrDefault();
        }
    }
}
