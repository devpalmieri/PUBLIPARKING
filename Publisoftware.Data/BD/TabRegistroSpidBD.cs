using Publisoftware.Data.POCOLight;
using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabRegistroSpidBD : EntityBD<tab_registro_spid>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabRegistroSpidBD");
        #endregion Private Members

        #region Costructor
        public TabRegistroSpidBD()
        {

        }
        #endregion Costructor

        #region Public Methods
        /// <summary>
        /// Restituisce la lista di tutte le entità
        /// </summary>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <param name="p_includeEntities">Elenco di tabelle collegate da includere durante la select</param>
        /// <returns></returns>
        public static new IQueryable<tab_registro_spid> GetList(dbEnte p_dbContext)
        {
            /// Ridefinisce la GetList per implementare la sicurezza di accesso sul contribuente
            return GetListInternal(p_dbContext).OrderByDescending(o => o.data_stato);
        }

        /// <summary>
        /// Restituisce l'entità a partire dalla chiave primaria
        /// </summary>
        /// <param name="p_id">Chiave primaria</param>
        /// <param name="p_dbContext">Context di esecuzione</param>
        /// <returns></returns>
        public static new tab_registro_spid GetById(Int32 p_id, dbEnte p_dbContext)
        {
            return GetList(p_dbContext).SingleOrDefault(c => c.id_tab_registro_spid == p_id);
        }
       
        
        /// <summary>
        /// Registra l'accesso dell'utente SPID
        /// </summary>
        /// <param name="carrello"></param>
        /// <param name="listRateCarrello"></param>
        /// <param name="p_dbContext"></param>
        /// <returns></returns>
        public static bool RegisterUser(tab_registro_spid p_user, dbEnte p_dbContext)
        {
                try
                {
                    if (p_user == null )
                    {
                        return false;
                    }
                    p_dbContext.tab_registro_spid.Add(p_user);
                    p_dbContext.SaveChanges();               

                    logger.LogInfoMessage(string.Format("Registrazione accesso utente SPID {0} eseguita con successo.", p_user.codice_spid));
                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException  e)
                {
                   
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("L'Entità di tipo \"{0}\" in stato \"{1}\" presenta i seguenti errori di validazione:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        logger.LogException(string.Format("L'Entità di tipo {0} in stato {1} presenta i seguenti errori di validazione: ", eve.Entry.Entity.GetType().Name, eve.Entry.State), e, EnLogSeverity.Error);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Proprietà: \"{0}\", Errore: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                            logger.LogException(string.Format (" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    logger.LogException("Errore in fase di registrazione dell'accesso dell'utente SPID.", ex, EnLogSeverity.Error);
                    return false;
                }
        }


        #endregion Public Methods
    }
}
