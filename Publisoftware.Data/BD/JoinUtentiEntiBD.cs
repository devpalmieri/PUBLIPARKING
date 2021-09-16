using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class JoinUtentiEntiBD : EntityBD<join_utenti_enti>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.JoinUtentiEntiBD");
        #endregion Private Members
        public JoinUtentiEntiBD()
        {

        }
        public static bool InsertJoinUtenteEnte(join_utenti_enti utente_ente, dbEnte p_dbContext, out int p_id_join)
        {
            p_id_join = -1;
            try
            {
                if (utente_ente == null)
                {
                    return false;
                }
                p_dbContext.join_utenti_enti.Add(utente_ente);
                p_dbContext.SaveChanges();
                p_id_join = utente_ente.id_join_utenti_enti;
                return true;
            }
            catch (Exception ex)
            {
                p_id_join = -1;
                logger.LogException("Errore in fase di salvataggio della join_utenti_enti. ", ex, EnLogSeverity.Error);
                return false;
            }
        }
    }
}
