using Publisoftware.Utility.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisoftware.Data.BD
{
    public class TabAutorizzazioniBD : EntityBD<tab_autorizzazioni>
    {
        #region Private Members
        private static ILogger logger = LoggerFactory.getInstance().getLogger<NLogger>("Publisoftware.Data.BD.TabAutorizzazioniBD");
        #endregion Private Members

        public TabAutorizzazioniBD()
        {

        }
        public static tab_autorizzazioni InsertSpidAutorizzazione(dbEnte p_dbContext, int p_idutente, decimal p_idcontribuente, int p_idstruttura, int p_idrisorsa, int p_idente)
        {
            try
            {
                tab_autorizzazioni v_autorizzazione = new tab_autorizzazioni
                {
                    id_utente_richiedente = p_idutente,
                    id_contribuente_richiedente = p_idcontribuente,
                    id_contribuente_richiesta_accesso = p_idcontribuente,
                    tipo_richiedente = tab_autorizzazioni.TIPO_AUTORIZZAZIONE_CONTRIBUENTE,
                    tipo_autorizzazione = tab_autorizzazioni.TIPO_AUTORIZZAZIONE_CONTRIBUENTE,
                    data_richiesta = DateTime.Now,
                    data_approvazione_richiesta = DateTime.Now,
                    id_struttura_stato = p_idstruttura,
                    id_risorsa_stato = p_idstruttura,
                    cod_stato = tab_autorizzazioni.ATT_ATT,
                    data_stato = DateTime.Now
                };
                p_dbContext.tab_autorizzazioni.Add(v_autorizzazione);
                p_dbContext.SaveChanges();
                logger.LogInfoMessage(string.Format("Aggiunta dell'autorizzaione per l'utente SPID {0}.", p_idutente));
                return v_autorizzazione;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
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
                        logger.LogException(string.Format(" - Proprietà: {0}, Errore: {1}", ve.PropertyName, ve.ErrorMessage), e, EnLogSeverity.Error);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogException("Errore in fase di salvataggio dell'autorizzazione per l'utente SPID. ", ex, EnLogSeverity.Error);
                return null;
            }
        }

        public static IQueryable<tab_autorizzazioni> GetAutorizzazioni(List<string> p_listTipoAutorizzazione,
                                                                       List<string> p_listCodStato,
                                                                       int p_idRisorsa,
                                                                       dbEnte p_dbContext)
        {
            IQueryable<tab_autorizzazioni> v_autorizzazioni = GetList(p_dbContext);

            v_autorizzazioni = v_autorizzazioni.Where(d => !d.id_risorsa_approvazione.HasValue || 
                                                            d.id_risorsa_approvazione == p_idRisorsa);

            if (p_listTipoAutorizzazione != null &&
                p_listTipoAutorizzazione.Count > 0)
            {
                v_autorizzazioni = v_autorizzazioni.Where(d => p_listTipoAutorizzazione.Contains(d.tipo_autorizzazione));
            }

            if (p_listCodStato != null &&
                p_listCodStato.Count > 0)
            {
                v_autorizzazioni = v_autorizzazioni.Where(d => p_listCodStato.Contains(d.cod_stato));
            }

            v_autorizzazioni = v_autorizzazioni.OrderByDescending(d => d.data_richiesta);

            return v_autorizzazioni;
        }
    }
}
